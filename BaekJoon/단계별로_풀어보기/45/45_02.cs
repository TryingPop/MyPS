using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 30
이름 : 배성훈
내용 : 도미노
    문제번호 : 4196번

    코라사주 알고리즘을 적용

    해당 문제를 푸는 방법이 안떠올라 질문 게시판에 다른 사람 팁을 활용했다
        https://www.acmicpc.net/board/view/41426
        https://www.acmicpc.net/board/view/80148

    해당 댓글들을 참고해서 코라사주 알고리즘으로 SCC를 구하고,
    다른 간선을 통해 올 수 있는 그룹을 카운트 안하는 방식으로 했다

    SCC의 특징 상 안에 하나의 도미노가 쓰러지면 같은 그룹은 모두 쓰러진다!
    그리고 다른 B그룹에서 A그룹으로 올 수 있다는 말은 B그룹의 도미노를 하나 쓰러뜨리면 A도 함께 쓰러진다는 말이 된다

    코사라주는 왜 SCC가 되는지 모르겠다;
*/

namespace BaekJoon._45
{
    internal class _45_02
    {

        static void Main2(string[] args)
        {

            int MAX = 100_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // 재활용할 변수들
            List<int>[] lines = new List<int>[MAX + 1];
            List<int>[] reverseLines = new List<int>[MAX + 1];
            Stack<int> s = new Stack<int>(MAX);

            int[] group = new int[MAX + 1];
            bool[] visit = new bool[MAX + 1];

            // 케이스 수
            int test = int.Parse(sr.ReadLine());

            for (int t = 0; t < test; t++)
            {

                // 노드 수, 간선 수
                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                // 초기화
                for (int i = 1; i <= info[0]; i++)
                {

                    if (lines[i] == null) lines[i] = new();
                    else lines[i].Clear();

                    if (reverseLines[i] == null) reverseLines[i] = new();
                    else reverseLines[i].Clear();

                    group[i] = 0;
                    visit[i] = false;
                }

                // 간선 입력
                for (int i = 0; i < info[1]; i++)
                {

                    int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                    lines[temp[0]].Add(temp[1]);
                    reverseLines[temp[1]].Add(temp[0]);
                }

                // 먼저 코라사주 알고리즘의 정방향 DFS 탐색
                // 타잔과 다른 점은 탐색이 끝난 순으로 스택에 저장한다
                // 고유 번호를 붙이는 과정이라 한다
                for (int i = 1; i <= info[0]; i++)
                {

                    FindDFS(lines, visit, i, s);
                }

                // 코라사주 알고리즘의 역방향!
                // 고유번호가 큰 순서대로 SCC를 만든다..?
                // 왜 만들어지는지는 모르겠다
                // 그냥 사례 몇개 보니 만들어져서 쓴다 느낌?이다

                // 느낌상 정방향에서
                // 그러면 SCC는 다음처럼 저장될 것이다 ()는 하나의 그룹 
                // ((((()()))()()))
                // 형태를 만들고
                // 역방향? 에서 뒤에서부터 껍질을 까는 과정인거처럼 보인다

                // Stack이 아닌 Queue로 해버리면, 정방향 과정을 2번하는 꼴이되어버린다!
                int groupIdx = 0;
                int result = 0;
                while (s.Count > 0)
                {

                    int next = s.Pop();

                    if (group[next] != 0) continue;

                    // 역방향 과정에서 다른 그룹으로 이어져 있는지 판별한다!
                    result += GroupDFS(reverseLines, visit, group, next, ++groupIdx);
                }

                sw.WriteLine(result);
            }

            sr.Close();
            sw.Close();
        }

        static void FindDFS(List<int>[] _lines, bool[] _visit, int _cur, Stack<int> _calc)
        {

            if (_visit[_cur]) return;
            _visit[_cur] = true;

            for (int i = 0; i < _lines[_cur].Count; i++)
            {

                int next = _lines[_cur][i];

                if (_visit[next]) continue;

                FindDFS(_lines, _visit, next, _calc);
            }

            _calc.Push(_cur);
        }

        static int GroupDFS(List<int>[] _reverseLines, bool[] _visit, int[] _group, int _cur, int _groupIdx)
        {

            if (!_visit[_cur]) return 0;
            _visit[_cur] = false;
            _group[_cur] = _groupIdx;
            int result = 1;

            for (int i = 0; i < _reverseLines[_cur].Count; i++)
            {

                int next = _reverseLines[_cur][i];
                
                if (!_visit[next]) 
                {

                    // 현재 노드는 다른 그룹에서 오는 길이 있다!
                    if (_group[next] != _groupIdx) result = 0;
                    continue; 
                }

                // 다른 그룹에서 오는 길이 있는 경우 0으로 한다!
                // 해당 그룹의 노드 중 다른 그룹에 오는 길이 있는지 판별용도!
                int chk = GroupDFS(_reverseLines, _visit, _group, next, _groupIdx);
                if (chk != 1) result = 0;
            }

            return result;
        }
    }

#if other

    using System.Text;

    public record struct Node(List<int> Children, int Id, bool Finished, int Scc)
    {
        public Node() : this(new(), 0, false, 0) { }
        public void Reset()
        {
            Id = 0;
            Finished = false;
            Scc = 0;
            Children.Clear();
        }
    }

    public class Program
    {
        private static readonly Node[] _nodes = new Node[100_000 + 1];
        private static readonly List<int> _indegree = new();

        private static int _curId;
        private static readonly Stack<int> _stack = new();

        static Program()
        {
            for (int i = 1; i < _nodes.Length; i++)
            {
                _nodes[i] = new();
            }
        }

        private static void Main(string[] args)
        {
            for (int i = 1; i < _nodes.Length; i++)
                _nodes[i] = new();

            using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            var t = ScanInt(sr);
            var ret = new StringBuilder();
            while (--t >= 0)
            {
                int n = ScanInt(sr), m = ScanInt(sr);
                for (int i = 0; i < m; i++)
                {
                    int x = ScanInt(sr), y = ScanInt(sr);
                    _nodes[x].Children.Add(y);
                }
                for (int i = 1; i <= n; i++)
                {
                    if (!_nodes[i].Finished)
                        DFS(i);
                }
                for (int i = 1; i <= n; i++)
                {
                    var node = _nodes[i];
                    foreach (var childIndex in node.Children)
                    {
                        var child = _nodes[childIndex];
                        if (node.Scc != child.Scc)
                            _indegree[child.Scc]++;
                    }
                }
                var zeroDegree = 0;
                foreach (var cur in _indegree)
                    if (cur == 0) zeroDegree++;
                ret.Append(zeroDegree).Append('\n');
                for (int i = 1; i <= n; i++)
                    _nodes[i].Reset();
                _indegree.Clear();
            }
            Console.Write(ret);
        }

        private static int DFS(int nodeIndex)
        {
            ref var node = ref _nodes[nodeIndex];
            var parent = node.Id = ++_curId;
            _stack.Push(nodeIndex);
            foreach (var childIndex in node.Children)
            {
                var child = _nodes[childIndex];
                if (child.Id == 0) parent = Math.Min(parent, DFS(childIndex));
                else if (!child.Finished) parent = Math.Min(parent, child.Id);
            }
            if (parent == node.Id)
            {
                int itemIndex;
                do
                {
                    itemIndex = _stack.Pop();
                    _nodes[itemIndex].Finished = true;
                    _nodes[itemIndex].Scc = _indegree.Count;
                } while (itemIndex != nodeIndex);
                _indegree.Add(0);
            }
            return parent;
        }

        static int ScanInt(StreamReader sr)
        {
            int c, n = 0;
            while (!((c = sr.Read()) is ' ' or '\n' or -1))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n + c - '0';
            }
            return n;
        }
    }
#endif
}
