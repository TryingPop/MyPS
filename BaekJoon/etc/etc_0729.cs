using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 26
이름 : 배성훈
내용 : 컨닝
    문제번호 : 1014

    dp, 비스마스킹, 최대 유량 문제다
    dp 비스마스킹으로 해결했다

    etc_0727에서 하나씩 놓는건 dfs보다는
    이중 for문이 압도적으로 빠르고 메모리도 적게 먹게 할 수 있어 해당 방법을 채용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0729
    {

        static void Main729(string[] args)
        {

            int BLOCK_L;
            int BLOCK_R;

            StreamReader sr;
            StreamWriter sw;

            bool[] board;
            int row, col;
            int test;

            int[][] dp;
            int len1, len2;

            Solve();

            void Solve()
            {

                Init();

                while(test-- > 0)
                {

                    Input();
                    int ret = GetRet();

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            int GetRet()
            {

                int[] temp;
                int ret = 0;

                dp[1][0] = 0;

                for (int i = row * col - 1; i >= 0; i--)
                {

                    temp = dp[0];
                    dp[0] = dp[1];
                    dp[1] = temp;

                    int bMax = ret;
                    for (int j = 0; j < len2; j++)
                    {

                        if (dp[0][j] == -1) continue;

                        int next = j >> 1;
                        dp[1][next] = Math.Max(dp[1][next], dp[0][j]);
                        
                        if (board[i] && IsBlockL(i, next) && IsBlockR(i, next))
                        {

                            next = next | (1 << (col + 1));
                            dp[1][next] = Math.Max(dp[1][next], dp[0][j] + 1);
                        }

                        dp[0][j] = -1;
                    }
                }

                for (int j = 0; j < len2; j++)
                {

                    ret = Math.Max(ret, dp[1][j]);
                }
                return ret;
            }

            bool IsBlockL(int _idx, int _state)
            {

                if (_idx % col == 0) return true;
                if ((_state & BLOCK_L) == 0) return true;

                return false;
            }

            bool IsBlockR(int _idx, int _state)
            {

                if (_idx % col == col - 1) return true;
                if ((_state & BLOCK_R) == 0) return true;

                return false;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                test = ReadInt();
                dp = new int[2][];
                dp[0] = new int[1 << 12];
                dp[1] = new int[1 << 12];

                board = new bool[10 * 10];
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();

                len1 = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        board[len1++] = sr.Read() == '.';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                len2 = 1 << (col + 2);
                Array.Fill(dp[1], -1, 0, len2);
                Array.Fill(dp[0], -1, 0, len2);

                BLOCK_L = 1 << 2;
                BLOCK_R = 1 << 0 | 1 << col;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public enum GraphNodeType
{
    Source,
    Sink,
    Seat,
}

public record struct GraphNode(GraphNodeType type, int y, int x);

public class Flow
{
    public int CurrFlow;
    public int MaxFlow;

    public bool CanFlow => CurrFlow < MaxFlow;

    public override string ToString() => $"{CurrFlow}/{MaxFlow}";
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var t = Int32.Parse(sr.ReadLine());
        while (t-- > 0)
        {
            var nm = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];

            var sourceNode = new GraphNode(GraphNodeType.Source, 0, 0);
            var sinkNode = new GraphNode(GraphNodeType.Sink, 0, 0);

            var map = new string[n];
            for (var idx = 0; idx < n; idx++)
                map[idx] = sr.ReadLine();

            var nodeIdMap = new Dictionary<GraphNode, int>();
            nodeIdMap.Add(sourceNode, nodeIdMap.Count);
            nodeIdMap.Add(sinkNode, nodeIdMap.Count);

            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x++)
                    if (map[y][x] == '.')
                        nodeIdMap.Add(new GraphNode(GraphNodeType.Seat, y, x), nodeIdMap.Count);

            var flowgraph = new Dictionary<int, Dictionary<int, Flow>>();
            var graph = new List<int>[nodeIdMap.Count];

            for (var idx = 0; idx < graph.Length; idx++)
                graph[idx] = new List<int>();

            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x += 2)
                {
                    if (map[y][x] == 'x')
                        continue;

                    var node = nodeIdMap[new GraphNode(GraphNodeType.Seat, y, x)];

                    foreach (var dx in new[] { -1, 1 })
                        foreach (var dy in new[] { -1, 0, 1 })
                        {
                            var targetNode = new GraphNode(GraphNodeType.Seat, y + dy, x + dx);
                            if (!nodeIdMap.ContainsKey(targetNode))
                                continue;

                            var target = nodeIdMap[targetNode];

                            EnsureExists(flowgraph, node, target);

                            flowgraph[node][target] ??= new Flow();
                            flowgraph[target][node] ??= new Flow();

                            graph[node].Add(target);
                            graph[target].Add(node);
                            flowgraph[node][target].MaxFlow++;
                        }
                }

            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x++)
                {
                    if (map[y][x] == 'x')
                        continue;

                    var target = nodeIdMap[new GraphNode(GraphNodeType.Seat, y, x)];
                    if (x % 2 == 0)
                    {
                        var source = nodeIdMap[sourceNode];

                        EnsureExists(flowgraph, source, target);

                        graph[source].Add(target);
                        graph[target].Add(source);
                        flowgraph[source][target].MaxFlow++;
                    }
                    else
                    {
                        var sink = nodeIdMap[sinkNode];

                        EnsureExists(flowgraph, target, sink);

                        graph[target].Add(sink);
                        graph[sink].Add(target);
                        flowgraph[target][sink].MaxFlow++;
                    }
                }

            var maxflow = Dinic(nodeIdMap.Count, flowgraph, graph, nodeIdMap[sourceNode], nodeIdMap[sinkNode]);
            var seatCount = map.Sum(l => l.Count(ch => ch == '.'));

            sw.WriteLine(seatCount - maxflow);
        }
    }

    private static void EnsureExists(Dictionary<int, Dictionary<int, Flow>> flowgraph, int src, int dst)
    {
        if (!flowgraph.ContainsKey(src))
            flowgraph.Add(src, new Dictionary<int, Flow>());

        if (!flowgraph.ContainsKey(dst))
            flowgraph.Add(dst, new Dictionary<int, Flow>());

        flowgraph[src][dst] = flowgraph[src].GetValueOrDefault(dst) ?? new Flow();
        flowgraph[dst][src] = flowgraph[dst].GetValueOrDefault(src) ?? new Flow();
    }

    private static int Dinic(int n, Dictionary<int, Dictionary<int, Flow>> flowgraph, List<int>[] graph, int source, int sink)
    {
        var levelgraph = new int?[n];
        var isDeadEnd = new bool[n];

        var maxflow = 0;
        while (true)
        {
            Array.Clear(levelgraph);
            Array.Clear(isDeadEnd);

            var isChanged = false;
            BuildLevelGraph(flowgraph, graph, levelgraph, source, sink);

            while (true)
            {
                var f = MakeFlow(flowgraph, graph, levelgraph, isDeadEnd, source, sink, Int32.MaxValue);
                if (f == 0)
                    break;

                maxflow += f;
                isChanged = true;
            }

            if (!isChanged)
                break;
        }

        return maxflow;
    }

    private static void BuildLevelGraph(Dictionary<int, Dictionary<int, Flow>> flowgraph, List<int>[] graph, int?[] levelgraph, int source, int sink)
    {
        var q = new Queue<(int pos, int level)>();
        q.Enqueue((source, 0));

        while (q.TryDequeue(out var s))
        {
            var (pos, level) = s;

            if (levelgraph[pos].HasValue)
                continue;

            levelgraph[pos] = level;

            if (pos == sink)
                continue;

            foreach (var dst in graph[pos])
            {
                if (!flowgraph[pos][dst].CanFlow)
                    continue;

                if (levelgraph[dst].HasValue)
                    continue;

                q.Enqueue((dst, 1 + level));
            }
        }
    }

    private static int MakeFlow(Dictionary<int, Dictionary<int, Flow>> flowgraph, List<int>[] graph, int?[] levelgraph, bool[] isDeadEnd, int pos, int sink, int currFlow)
    {
        if (pos == sink)
            return currFlow;

        foreach (var dst in graph[pos])
        {
            if (isDeadEnd[dst])
                continue;

            var forward = flowgraph[pos][dst];
            if (!forward.CanFlow)
                continue;

            if (!levelgraph[pos].HasValue || !levelgraph[dst].HasValue || levelgraph[pos] >= levelgraph[dst])
                continue;

            var f = MakeFlow(flowgraph, graph, levelgraph, isDeadEnd, dst, sink, Math.Min(forward.MaxFlow - forward.CurrFlow, currFlow));
            if (f == 0)
                continue;

            var reverse = flowgraph[dst][pos];
            forward.CurrFlow += f;
            reverse.CurrFlow -= f;

            return f;
        }

        isDeadEnd[pos] = true;
        return 0;
    }
}

#elif other2
var solver = new Solver(new ConsoleReader());
var answerList = solver.Solve();
foreach (var answer in answerList)
    Console.WriteLine(answer);

public interface IReader
{
    string ReadLine();
}

public class ConsoleReader : IReader
{
    public string ReadLine() => Console.ReadLine()!;
}

public static class StringExtension
{
    public static int ToInt(this string s) => int.Parse(s);
    public static int[] ToIntArray(this string s) => s
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();
}

public class Solver
{
    private readonly CaseData[] _caseList;
    
    public Solver(IReader reader)
    {
        var n = reader.ReadLine().ToInt();
        _caseList = Enumerable
            .Range(0, n)
            .Select(_ => new CaseData(reader))
            .ToArray();
    }

    public IEnumerable<int> Solve()
    {
        foreach (var caseData in _caseList)
        {
            yield return Solve(caseData);
        }
    }

    private int Solve(CaseData caseData)
    {
        var graph = caseData.Graph;
        var match = new int[graph.Length];
        var visited = new bool[graph.Length];
        
        // FindMaximumMatching
        var matchCount = 0;
        Array.Fill(match, -1);
        foreach (var node in graph
                     .Where(n => n.AdjacencyList.Count > 0)
                     .Where(n => n.Id % caseData.W % 2 == 0))
        {
            Array.Fill(visited, false);
            if (Matching(caseData.Graph, node.Id, match, visited))
                ++matchCount;
        }
        
        return caseData.EmptyCount - matchCount;
    }

    private bool Matching(Node[] graph, int targetId, int[] match, bool[] visited)
    {
        if (visited[targetId]) { return false; }
        visited[targetId] = true;
        
        foreach (var id in graph[targetId].AdjacencyList)
        {
            if (match[id] < 0 || Matching(graph, match[id], match, visited))
            {
                match[id] = targetId;
                return true;
            }
        }
        return false;
    }
}

public struct Vector
{
    public readonly int X;
    public readonly int Y;

    public Vector(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Node
{
    public readonly int Id;
    public readonly List<int> AdjacencyList = new();

    public Node(int id) => Id = id;
    
    public void Add(int id)
    {
        if (AdjacencyList.Contains(id) == false)
            AdjacencyList.Add(id);
    }
}

public class CaseData
{
    public readonly int W;
    public readonly int H;
    public readonly int EmptyCount;

    public readonly Node[] Graph;

    private static readonly Vector[] _offsets = new Vector[]
    {
        new(-1, -1), new(1, -1),
        new(-1, 0), new(1, 0),
        new(-1, 1), new(1, 1)
    };

    public CaseData(IReader reader)
    {
        var param = reader.ReadLine().ToIntArray();
        H = param[0];
        W = param[1];

        var map = Enumerable
            .Range(0, H)
            .Select(_ => reader
                .ReadLine()
                .ToArray())
            .ToArray();

        Graph = Enumerable
            .Range(0, W * H)
            .Select(i => new Node(i))
            .ToArray();

        for (var y = 0; y < H; ++y)
        for (var x = 0; x < W; ++x)
        {
            var c = map[y][x];
            if (c == 'x')
                continue;

            ++EmptyCount;

            var currentId = GetId(x, y);
            foreach (var offset in _offsets)
            {
                var tx = x + offset.X;
                var ty = y + offset.Y;
                
                if (IsOutOfBoundary(tx, ty))
                    continue;

                var tc = map[ty][tx];
                if (tc == 'x')
                    continue;
                
                var adjacencyId = GetId(tx, ty);
                Graph[currentId].Add(adjacencyId);
            }
        }
    }

    private int GetId(int x, int y) => y * W + x;

    private bool IsOutOfBoundary(int x, int y)
        => x < 0 || x >= W || y < 0 || y >= H;
}
#endif
}
