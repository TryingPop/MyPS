using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 2
이름 : 배성훈
내용 : 2 - SAT - 3
    문제번호 : 11280번

    아무리 생각해도 떠오르지 않아서 해당 사이트를 참고했다
    https://blog.qwaz.io/problem-solving/scc%EC%99%80-2-sat

    (x or ~y) 인 경우 여기서 ~ 는 not이다
    ~x가 참이면, ~y가 참이될 수 밖에 없다
    이를 간선으로 표현하면 ~x -> ~y 이다
    대우로 y -> x인 간선도 만들어진다

    그리고 해당 간선들을 이용해 SCC를 만든다
    그러면 해당 SCC 안의 모든 원소들은 모두 참이거나 모두 거짓이다

    이에 x와 ~x가 같은 SCC에 속하면 답이 존재할 수 없다
    그리고 x -> ~x인 경로만 존재하는 경우 위상정렬 했을 때, 더 뒤쪽에 존재하는 정점들만 참이 된다
    코사라주 알고리즘으로 하면, 위상정렬의 순으로 나오기에 앞에 있는 그룹이 된다!
    (이상해서 찾아봤는데, 정방향이다. 여태 역방향 간선가지고 반대로 나온다고 인식했다;)
    마지막으로 간선이 없는 경우, 어느 쪽을 참으로 해도 상관없다

    그래서 코사라주 알고리즘으로 그룹을 만들고, 같은 그룹에 있는지 확인만 했다
    자신과, ~연산을 한 자신이 같은 그룹에 있는 원소가 하나라도 있으면, true가 되는 경우의 수가 없어 0
    모두 다른 경우는 존재하고, 이후에는 그룹 번호가 높은 걸로(위상 정렬 시 뒤에 있는 것으로) 출력하게 했다
    
    그러니 통과했다
*/

namespace BaekJoon._45
{
    internal class _45_06
    {

        static void Main6(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 노드 수, 2 - CNF 식의 개수
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // 예를들어 전체 노드는 3일 때
            // 1, 2, 3,  4,  5,  6   <- 실제 인덱스
            // 1, 2, 3, -1, -2, -3   <- 의도하는 인덱스
            // 이처럼 음수의 경우 양수 뒤에 이어붙일 생각이라 연산용으로 사용하는 변수
            int ADD = info[0];

            // 2 - CNF 식으로 만든 간선을 모을 리스트 배열
            // 음수, 양수를 표현해야해서 2 * info[0] + 1이다
            // 정방향
            List<int>[] lines = new List<int>[2 * info[0] + 1];
            // 코사라주 연산을 위한 역방향
            List<int>[] reverseLines = new List<int>[2 * info[0] + 1];
            // 초기 설정
            for (int i = 1; i < 2 * info[0] + 1; i++)
            {

                lines[i] = new();
                reverseLines[i] = new();
            }

            // 2 - CNF 식 입력 받고 간선으로 변환
            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                // 음수면, 인덱스 변형
                if (temp[0] < 0) temp[0] = -temp[0] + ADD;
                if (temp[1] < 0) temp[1] = -temp[1] + ADD;

                // x or y 인 경우
                // not x, 즉 ~x 가 만약 참인 경우
                // y가 참이 되어야한다
                // 그래서 이를 ~x -> y로 가는 간선으로 간주하고 연결
                lines[Not(temp[0], ADD)].Add(temp[1]);
                
                // 교환 법칙이 성립하기에
                // x or y == y or x로 볼 수 있다
                // 그래서 다음 ~y -> x간선도 추가
                lines[Not(temp[1], ADD)].Add(temp[0]);

                // 코사라주 역방향 간선용 저장
                reverseLines[temp[1]].Add(Not(temp[0], ADD));
                reverseLines[temp[0]].Add(Not(temp[1], ADD));
            }

            sr.Close();

            // 코사라주 방법으로 SCC를 찾는다
            // 코사라주 정방향 연산
            // 왜냐하면 코사라주를 이용하면 자동으로 위상정렬이 되어져 있기 때문에!
            Stack<int> s = new Stack<int>(2 * info[0]);
            bool[] visit = new bool[2 * info[0] + 1];

            for (int i = 1; i < 2 * info[0] + 1; i++)
            {

                if (visit[i]) continue;
                BeforeDFS(lines, visit, i, s);
            }

            // 코사라주 역방향 연산 여기서 SCC가 만들어진다
            // 그리고 가는 SCC끼리 가는 길이 있다면 위상 정렬 방향 순서로 만들어진다
            int[] groups = new int[2 * info[0] + 1];
            int maxGroups = 1;
            while (s.Count > 0)
            {

                var node = s.Pop();
                if (!visit[node]) continue;

                AfterDFS(reverseLines, visit, groups, node, ref maxGroups);

                maxGroups++;
            }

            // 이제 만든 SCC로 정답 찾기
            bool[] solve = new bool[info[0] + 1];
            bool failed = false;

            for (int i = 1; i < info[0] + 1; i++)
            {

                int p = groups[i];
                int n = groups[i + ADD];

                // 만들어진 SCC에서는 모든 원소가 true false를 공유하는데,
                // x와 ~x가 한 그룹에 있을 경우,
                // x가 true, false가 되어야한다 이 건 존재할 수 없는 경우이다
                if (p == n)
                {

                    // 그래서 탈출
                    failed = true;
                    break;
                }

                // 다른 경우 위상 정렬 시 뒤에 있는 것을 넣어줘야한다
                // 서로 가는 길목이 없는 SCC인 경우 true, false 중 아무거나 넣어도 된다
                // 그래서 positive가 작다면 true, negative가 작다면 false를 넣었다
                if (p > n) solve[i] = true;
                else solve[i] = false;
            }

            // 출력
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            if (failed) sw.WriteLine(0);
            else 
            { 
                
                sw.WriteLine(1); 
                for (int i = 1; i < info[0] + 1; i++)
                {

                    // 1번부터 true, false 출력
                    sw.Write(solve[i] ? 1 : 0);
                    sw.Write(' ');
                }
            }
            sw.Close();
        }

        static int Not(int _n, int _add)
        {

            // 일단 음수이면, 양수로
            // 양수면 음수로
            if (_n > _add) _n -= _add;
            else _n += _add;

            return _n;
        }

        static void BeforeDFS(List<int>[] _lines, bool[] _visit, int _cur, Stack<int> _calc)
        {

            _visit[_cur] = true;

            for (int i = 0; i < _lines[_cur].Count; i++)
            {

                int next = _lines[_cur][i];
                if (_visit[next]) continue;

                BeforeDFS(_lines, _visit, next, _calc);
            }

            _calc.Push(_cur);
        }

        static void AfterDFS(List<int>[] _reverseLines, bool[] _visit, int[] _groups, int _cur, ref int _groupIdx)
        {

            _visit[_cur] = false;
            _groups[_cur] = _groupIdx;

            for (int i = 0; i < _reverseLines[_cur].Count; i++)
            {

                int next = _reverseLines[_cur][i];

                if (!_visit[next]) continue;

                AfterDFS(_reverseLines, _visit, _groups, next, ref _groupIdx);
            }
        }
    }
#if other
namespace Baekjoon;

using System.Text;

public class Node
{
    public int Id = -1;
    public readonly List<int> Children = new();
    public int SCC = -1;

    public void AddChild(int node)
    {
        Children.Add(node);
    }
}

public class Graph
{
    private readonly Dictionary<int, Node> _nodes;
    private int _curDFSCount = 0;
    private int _sccCount = 0;
    private readonly Stack<int> _stack = new();

    public Graph(int nodeCount)
    {
        _nodes = new(2 * nodeCount);
        for (int i = 1; i <= nodeCount; i++)
        {
            _nodes[i] = new();
            _nodes[-i] = new();
        }
    }

    public void Link(int parent, int child)
    {
        _nodes[parent].AddChild(child);
    }

    public void Build()
    {
        foreach ((var key, _) in _nodes)
        {
            if (_nodes[key].SCC == -1)
                DFS(key);
        }
    }

    private int DFS(int nodeIndex)
    {
        var node = _nodes[nodeIndex];
        var parent = node.Id = _curDFSCount++;
        _stack.Push(nodeIndex);
        foreach (var childIndex in node.Children)
        {
            var child = _nodes[childIndex];
            if (child.Id == -1) parent = Math.Min(DFS(childIndex), parent);
            else if (child.SCC == -1) parent = Math.Min(child.Id, parent);
        }
        if (node.Id == parent)
        {
            int popedIndex;
            do
            {
                popedIndex = _stack.Pop();
                var popedNode = _nodes[popedIndex];
                popedNode.SCC = _sccCount;
            } while (popedIndex != nodeIndex);
            _sccCount++;
        }
        return parent;
    }

    public int GetSCC(int node)
    {
        return _nodes[node].SCC;
    }
}

public class Program
{
    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        int nodeCount = ScanSignedInt(sr), clauseCount = ScanSignedInt(sr);
        var graph = new Graph(nodeCount);
        for (int i = 0; i < clauseCount; i++)
        {
            int a = ScanSignedInt(sr), b = ScanSignedInt(sr);
            graph.Link(-a, b);
            graph.Link(-b, a);
        }

        graph.Build();
        for (int i = 1; i <= nodeCount; i++)
        {
            if (graph.GetSCC(i) == graph.GetSCC(-i))
            {
                Console.Write('0');
                return;
            }
        }

        var sb = new StringBuilder();
        sb.Append('1').Append('\n');
        for (int i = 1; i <= nodeCount; i++)
            sb.Append(graph.GetSCC(i) < graph.GetSCC(-i) ? '1' : '0').Append(' ');
        sb.Length--;
        Console.Write(sb);
    }

    static int ScanSignedInt(StreamReader sr)
    {
        int c = sr.Read(), n = 0;
        if (c == '-')
            while (!((c = sr.Read()) is ' ' or '\n'))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n - c + '0';
            }
        else
        {
            n = c - '0';
            while (!((c = sr.Read()) is ' ' or '\n'))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n + c - '0';
            }
        }
        return n;
    }
}
#endif
}
