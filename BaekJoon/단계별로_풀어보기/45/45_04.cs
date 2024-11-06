using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 31
이름 : 배성훈
내용 : ATM
    문제번호 : 4013번

    2% 에서 Fomat에러 뜬다;
    ... Format 에러 부분 찾아보니, 입출력 문제라고 한다
    혹시 데이터가 많아서 입출력을 하나씩 읽는걸로 바꿨다
    그랬더니 3.4초로 맞췄다

    통과는 했는데, 해당 DFS 메서드가 완전탐색으로 하고 있어, 비효율적이라 느꼈다
    다른 질문 게시판에는 시간초과라고 나왔다
    그래서 최대 비용으로 가는데 다익스트라 아이디어를 이용했다

    다익스트라는 최소 비용으로 이동하는 것인데, 여기서는 최대비용으로 이동하게 했다
    따로 우선순위 큐와 비슷한 클래스를 정의하거나, 우선순위 연산을 따로 저장할 수 있지만
    여기서는 음수로 넣어 해결했다

    주된 아이디어는 다음과 같다
    교차로 a를 지날 때, a를 포함하는 SCC를 A라하자
    그러면 SCC A안의 모든 교차로를 지나야 최대로 인출하는 경우가 된다!

    그래서 시작지점을 기준으로 SCC를 찾았다
    해당 지점에 포함 안되는 노드들은 시작지점에서 갈 수 없는 지역들이다!
    그래서 해당 지점의 SCC는 안찾아도 된다 (전체를 해도 3492ms -> 3420ms 차이밖에 안난다;)

    그리고 SCC들 간에 최대한 비싸게 해당 지점으로 이동하는 경로를 찾았다
    방법은 DFS로 했으나 이후 우선순위 큐로 방법을 바꿨다
*/

namespace BaekJoon._45
{
    internal class _45_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 노드 수, 간선 수(단방향)
            int[] info1 = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // 간선
            List<int>[] lines = new List<int>[info1[0] + 1];

            for(int i = 1; i <= info1[0]; i++)
            {

                lines[i] = new();
            }

            // 간선 입력
            for (int i = 0; i < info1[1]; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int f = int.Parse(temp[0]);
                int b = int.Parse(temp[1]);
                lines[f].Add(b);
            }

            // 해당 교차로에 있는 atm의 출력 가능한 금액
            int[] atm = new int[info1[0] + 1];
            for (int i = 1; i <= info1[0]; i++)
            {

                atm[i] = int.Parse(sr.ReadLine());
            }

            // 시작지점과 목표지점의 수
            int[] info2 = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // 목표지점
            // 해당 코드는 Format에러 일으킨다
            // int[] targets = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int[] targets = new int[info2[1]];
            for (int i = 0; i < info2[1]; i++)
            {

                // 하나씩 읽어온다    
                int c;
                while((c = sr.Read()) != '\n' && c != ' ' && c != -1)
                {

                    if (c == '\r') continue;
                    targets[i] *= 10;
                    targets[i] += c - '0';
                }
            }

            sr.Close();

            // 일단 SCC로 묶는데 타잔 알고리즘을 이용한다!
            int[] id = new int[info1[0] + 1];
            int[] group = new int[info1[0] + 1];
            Stack<int> s = new Stack<int>();

            int maxId = 0;
            int maxGroup = 0;

            /*
            // 시작 부분만 SCC를 만들면 된다!
            // 다른 SCC는 할 필요가 없다! -> 시작지점에서 가지 못하는 장소이기 때문에!
            for (int i = 0; i < info1[0]; i++)
            {

                TarjanDFS(lines, id, group, i + 1, ref maxId, ref maxGroup, s);
            }
            */

            TarjanDFS(lines, id, group, info2[0], ref maxId, ref maxGroup, s);

            // SCC간의 간선
            List<int>[] topoLines = new List<int>[maxGroup + 1];
            // 해당 SCC를 지나면 SCC안의 모든 원소들을 지난다고 생각한다!
            int[] groupMoney = new int[maxGroup + 1];


            for (int i = 1; i < maxGroup + 1; i++)
            {

                topoLines[i] = new();
            }

            for (int i = 1; i <= info1[0]; i++)
            {

                int curGroup = group[i];
                // 시작지점에서 갈 수 없는 곳은 따로 연산을 안한다!
                if (curGroup == 0) continue;
                groupMoney[curGroup] += atm[i];
                for (int j = 0; j < lines[i].Count; j++)
                {

                    int nextGroup = group[lines[i][j]];
                    if (curGroup == nextGroup || topoLines[curGroup].Contains(nextGroup)) continue;

                    topoLines[curGroup].Add(nextGroup);
                }
            }

            // 시작지점에서 해당 지점으로 가는데 최대 groupMoney가 담긴다
            int[] maxMoney = new int[maxGroup + 1];

#if first
            // 당장은 완전 탐색 아이디어 밖에 안떠오른다;
            // 처음은 DFS를 써서 했다
            // 이러면 가는 곳이 많아 탐색 시간 많이 걸린다
            // 3.4초 걸렸다;
            DFS(topoLines, groupMoney, maxMoney, group[info2[0]], 0);
#elif !first

            // DFS 완전 탐색을 하면 쓸모없이 자주 가는 길이 많아진다!
            // 그래서 우선순위 큐를 이용해 반복을 줄이니 1.1초 단축되어 2.3초다!

            // 재방문 방지로 -1값으로 채운다
            Array.Fill(maxMoney, -1);
            int start = group[info2[0]];
            maxMoney[start] = groupMoney[start];

            PriorityQueue<(int dst, int money), int> q = new PriorityQueue<(int dst, int money), int>();
            q.Enqueue((start, groupMoney[start]), 0);

            while(q.Count > 0)
            {

                var node = q.Dequeue();

                // 이미 더 비싼 경로로 간 경우!
                if (maxMoney[node.dst] > node.money) continue;

                for (int i = 0; i < topoLines[node.dst].Count; i++)
                {

                    int nextDst = topoLines[node.dst][i];
                    int nextMoney = groupMoney[nextDst] + node.money;

                    // 더 비싼 경로가 이미 있는 경우
                    if (nextMoney <= maxMoney[nextDst]) continue;

                    maxMoney[nextDst] = nextMoney;
                    q.Enqueue((nextDst, nextMoney), -nextMoney);
                }
            }
#endif

            
            int result = 0;
            for(int i = 0; i < targets.Length; i++)
            {

                // 시작 지점에서 갈 수 있는 모든 목적지를 조사한다
                // target이 갈 수 없는 곳이면, scc그룹은 0이고
                // 이때의 maxMoney는 -1이다
                int curMoney = maxMoney[group[targets[i]]];
                if (result < curMoney) result = curMoney;
            }

            Console.Write(result);
        }


        static int TarjanDFS(List<int>[] _lines, int[] _id, int[] _group, int _cur, ref int _curId, ref int _curGroup, Stack<int> _calc)
        {

            if (_id[_cur] != 0) return -1;
            int result = _cur;
            _id[_cur] = ++_curId;
            _calc.Push(_cur);

            for (int i = 0; i < _lines[_cur].Count; i++)
            {

                int next = _lines[_cur][i];
                if (_id[next] != 0) 
                {

                    if (_group[next] == 0 && _id[next] < _id[result]) result = next;
                    continue; 
                }

                int chk = TarjanDFS(_lines, _id, _group, next, ref _curId, ref _curGroup, _calc);

                if (_id[chk] < _id[result]) result = chk;
            }

            if (result == _cur)
            {

                _curGroup++;
                while(_calc.Count > 0)
                {

                    int next = _calc.Pop();
                    _group[next] = _curGroup;
                    if (next == result) break;
                }
            }

            return result;
        }

#if first
        static void DFS(List<int>[] _topoLines, int[] _groupMoney, int[] _maxMoney, int _cur, int _curMoney)
        {

            _curMoney += _groupMoney[_cur];
            if (_maxMoney[_cur] < _curMoney) _maxMoney[_cur] = _curMoney;
            // 재탐색은 끊는다
            else return;

            for (int i = 0; i < _topoLines[_cur].Count; i++)
            {

                int next = _topoLines[_cur][i];
                DFS(_topoLines, _groupMoney, _maxMoney, next, _curMoney);
            }
        }
#endif
    }

#if other1
namespace Baekjoon;

public record struct Node(List<int> Children, int Id, int SCC)
{
    public Node() : this(new(), -1, -1) { }

    public readonly bool HasId => Id >= 0;
    public readonly bool Finished => SCC >= 0;

    public void Clear()
    {
        Children.Clear();
        Id = -1;
        SCC = -1;
    }
}

public class SCCNode
{
    public readonly HashSet<int> Children = new();
    public bool CanBeEnd;
    public int Money;
    public int GainableMaxMoney = -1;
}

public class SCCTree
{
    readonly Node[] _nodes;
    readonly Stack<int> _stack = new();
    readonly List<SCCNode> _sccNodes = new();
    readonly int[] _maxValueOf;

    int _curId = 0;
    int _sccCount;

    public SCCTree(int nodeCount)
    {
        _nodes = new Node[nodeCount + 1];
        for (int i = 1; i <= nodeCount; i++)
            _nodes[i] = new();
        _maxValueOf = new int[nodeCount + 1];
        Array.Fill(_maxValueOf, -1);
    }

    public void Add(int from, int to) => _nodes[from].Children.Add(to);

    public void Build()
    {
        for (int node = 1; node < _nodes.Length; node++)
        {
            if (!_nodes[node].Finished)
                DPS(node);
        }
        for (int node = 1; node < _nodes.Length; node++)
        {
            foreach (var child in _nodes[node].Children)
            {
                if (_nodes[node].SCC != _nodes[child].SCC)
                    GetSCCOf(node).Children.Add(_nodes[child].SCC);
            }
        }
    }

    public void SetMoney(int node, int money)
    {
        GetSCCOf(node).Money += money;
    }

    public int GetMax(int start) => GetMaxDPS(_nodes[start].SCC);

    int GetMaxDPS(int sccIndex)
    {
        var scc = _sccNodes[sccIndex];
        if (scc.GainableMaxMoney == -1)
        {
            var childMax = scc.CanBeEnd ? 0 : -2;
            foreach (var childScc in _sccNodes[sccIndex].Children)
            {
                childMax = Math.Max(GetMaxDPS(childScc), childMax);
            }
            scc.GainableMaxMoney = childMax == -2 ? -2 : childMax + scc.Money;
        }
        return scc.GainableMaxMoney;
    }

    public void SetAsEnd(int node) => GetSCCOf(node).CanBeEnd = true;

    private SCCNode GetSCCOf(int node) => _sccNodes[_nodes[node].SCC];

    int DPS(int nodeIndex)
    {
        ref var node = ref _nodes[nodeIndex];
        var parent = node.Id = _curId++;
        _stack.Push(nodeIndex);
        foreach (var childIndex in node.Children)
        {
            ref var child = ref _nodes[childIndex];
            var childParent = child.Id;
            if (!child.HasId) childParent = DPS(childIndex);
            if (child.Finished) continue;
            if (parent > childParent)
                parent = childParent;
        }
        if (parent == node.Id)
        {
            int elem;
            do
            {
                elem = _stack.Pop();
                _nodes[elem].SCC = _sccCount;
            } while (elem != nodeIndex);
            _sccNodes.Add(new());
            _sccCount++;
        }
        return parent;
    }
}

public class Program
{
    static readonly int _start;
    static readonly SCCTree _tree;

    static Program()
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        int nodeCount = ScanInt(sr), edgeCount = ScanInt(sr);
        _tree = new(nodeCount);
        for (int i = 0; i < edgeCount; i++)
            _tree.Add(ScanInt(sr), ScanInt(sr));
        _tree.Build();
        for (int i = 1; i <= nodeCount; i++)
            _tree.SetMoney(i, ScanInt(sr));
        _start = ScanInt(sr);
        var endCount = ScanInt(sr);
        for (int i = 0; i < endCount; i++)
            _tree.SetAsEnd(ScanInt(sr));
    }

    private static void Main(string[] args)
    {
        Console.Write(_tree.GetMax(_start));
    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n'))
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
#elif other2
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace ThisIsCShaps
{
    class Program
    {
        static StreamReader sr = new StreamReader(Console.OpenStandardInput(), Encoding.Default);
        static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
        static int n,m;
        static int[] id;
        static int idCount;
        static List<List<int>> scc = new List<List<int>>();
        static List<long> sccMax = new List<long>();
        static List<int>[] tree;
        static string[] str = null;
        static Stack<int> st = new Stack<int>();
        static bool[] isFinish;
        static int[] indegree;
        static int[] atm;
        static bool[] restaurant;
        static List<int>[] finalTree;
        static long answer = 0;
        static long[] dp;
        //레스토랑이 scc에서 있는지 없는 지 판단해서 체크해둔다.
        static bool[] isSccResaurant;
        static int StronglyConnectedComponent(int idx)
        {
            id[idx] = ++idCount;
            int parent = id[idx];
            st.Push(idx);
            if (tree[idx] != null)
            {
                for (int i = 0; i < tree[idx].Count; i++)
                {
                    int next = tree[idx][i];
                    if (id[next].Equals(0))
                    {
                        parent = Math.Min(parent, StronglyConnectedComponent(next));
                    }
                    else if (!isFinish[next])
                    {
                        parent = Math.Min(parent, id[next]);
                    }
                }
            }
            if ((parent).Equals(id[idx]))
            {
                List<int> scc2 = new List<int>();
                int cnt = scc.Count;
                int total = 0;
                while (st.Count > 0)
                {
                    int current = st.Pop();
                    scc2.Add(current);
                    isFinish[current] = true;
                    indegree[current] = cnt;
                    if(restaurant[current])
                    {
                        isSccResaurant[cnt] = true;
                    }
                    total += atm[current];
                    if (current.Equals(idx))
                    {
                        break;
                    }
                }
                scc.Add(scc2);
                sccMax.Add(total);
            }
            return parent;
        }
        static void Main(string[] args)
        {
            str = sr.ReadLine().Split();
            n = int.Parse(str[0]);
            m = int.Parse(str[1]);
            tree = new List<int>[n];
            isFinish = new bool[n];
            indegree = new int[n];
            idCount = 0;
            id = new int[n];
            restaurant = new bool[n];
            atm = new int[n];
            isSccResaurant = new bool[n];
            for (int i = 0; i < m; i++)
            {
              str=  sr.ReadLine().Split();
                int a = int.Parse(str[0])-1;
                int b = int.Parse(str[1])-1;
                if(tree[a]==null)
                {
                    tree[a] = new List<int>();
                }
                tree[a].Add(b);
            }
            for (int i = 0; i < n; i++)
            {
              atm[i]=  int.Parse(sr.ReadLine());
            }
            str = sr.ReadLine().Split();
            int s= int.Parse(str[0]) - 1;
            int p = int.Parse(str[1]);
            str = sr.ReadLine().Split();
            for (int i = 0; i < p; i++)
            {
                restaurant[int.Parse(str[i]) - 1] = true;
            }
            for (int i = 0; i < n; i++)
            {
                if(id[i].Equals(0))
                {
                    StronglyConnectedComponent(i);
                }
            }
            finalTree = new List<int>[scc.Count];
            dp = new long[scc.Count];

            for (int i = 0; i < scc.Count; i++)
            {
                dp[i] = -1;
            }
            for (int i = 0; i < n; i++)
            {
                if(tree[i]==null)
                {
                    continue;
                }
                for (int j = 0; j < tree[i].Count; j++)
                {
                    int num = tree[i][j];
                    if(!indegree[i].Equals(indegree[num]))
                    {
                        if(finalTree[indegree[i]]==null)
                        {
                            finalTree[indegree[i]] = new List<int>();
                        }
                        finalTree[indegree[i]].Add(indegree[num]);
                    }
                }
            }
            //dp[indegree[s]] = sccMax[indegree[s]];
          answer= DFS(indegree[s]);
            sw.Write(answer);
            sw.Close();
        }

        private static long DFS(int start)
        {
            if (!dp[start].Equals(-1))// 한번 더 왔을 경우.
            {
                return dp[start];
            }
            dp[start] = sccMax[start];
            long total = 0;
            if (finalTree[start] != null)
            {
                for (int i = 0; i < finalTree[start].Count; i++)
                {
                    int next = finalTree[start][i];
                    total= Math.Max(total, DFS(next));
                }
            }
            if (total.Equals(0) && !isSccResaurant[start])
            {
                dp[start] = 0;
                return 0;
            }
            dp[start] += total;
            return dp[start];
        }
//        static void DFS(int start)
//        {
//            if (isSccResaurant[start])
//            {
//                answer = Math.Max(answer, dp[start]);
//            }
//            if (finalTree[start] != null)
//            {
//                for (int i = 0; i < finalTree[start].Count; i++)
//                {
//                    int next = finalTree[start][i];
//                    if(dp[next]<dp[start]+sccMax[next])
//                    {
//                        dp[next] = dp[start] + sccMax[next]
//;                    DFS(next);
//                    }
//                }
//            }
//        }
    }
}
#endif
}
