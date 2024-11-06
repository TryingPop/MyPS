using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 25
이름 : 배성훈
내용 : 학교 가지마!
    문제번호 : 1420

    최대 유량, 최대 유량 최소 컷 정리 문제다
    아이디어는 다음과 같다

    그리고 택시거리가 1인 두 빈점에 한해 간선을 연결해줬다
    이후 노드간의 유량은 10만씩 흐르게하고 노드를 분할해서 자신에게 이동할 때는
    1만의 유량이 흐르게했다

    그러면 최대유량은 A -> B로 가는데 서로 다른 정점으로 이동하는 경로의 수와 같다
    이 말은 해당 경로의 유일점?이 각각 존재한다는 말과 같고 여기에 벽을 세우면 최소가 된다고 볼 수 있다
    그래서 경로 수를 제출하니 이상없이 통과했으나 메모리를 엄청 쓴다 100mb정도?
    다른 사람 코드를 살펴보며 메모리를 줄여봐야겠다 -> 아마 c, f 가 1만 단위니 1개의 공간을 할당해야하는 문제로 보인다
    찾아보니, c, f를 2 * n + 2 개를 할당하는게 아닌, 10개(자기자신, 4방향에 왕복이므로 전체 2배)만 할당하면 될거같이 보인다

    c, f 배열을 노드당 크기를 2 * n + 1 할당하는게 아닌 10개로 줄이니 속도도 52ms 빨라지고, 메모리도 10배 가까이 줄였다
*/

namespace BaekJoon._51
{
    internal class _51_06
    {

        static void Main6(string[] args)
        {

            int INF = 100_000;
            int source;
            int sink;
            StreamReader sr;
            int row, col;
            int[][] board;
            int n;

            List<int>[] line;
            int[] dirR, dirC;

            Queue<int> q;
            int[] lvl;
            List<int>[] c, f;
            int[] d;
            Solve();

            void Solve()
            {

                Input();

                LinkLine();

                int ret = 0;
                Init();

                while (BFS())
                {

                    Array.Fill(d, 0);

                    while(true)
                    {

                        int flow = DFS(source, INF);
                        if (flow == 0) break;
                        ret += flow;
                    }
                }

                Console.WriteLine(ret >= INF ? -1 : ret);
            }

            void Init()
            {

                q = new(2 * n + 2);
                lvl = new int[2 * n + 2];
                d = new int[2 * n + 2];
            }

            bool BFS()
            {

                Array.Fill(lvl, -1);
                lvl[source] = 0;

                q.Enqueue(source);
                while (q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];

                        if (lvl[next] == -1 && c[node][i] - f[node][i] > 0)
                        {

                            lvl[next] = lvl[node] + 1;
                            q.Enqueue(next);
                        }
                    }
                }

                return lvl[sink] != -1;
            }
            
            int DFS(int _n, int _flow)
            {

                if (_n == sink) return _flow;

                for (; d[_n] < line[_n].Count; d[_n]++)
                {

                    int next = line[_n][d[_n]];
                    if (lvl[next] == lvl[_n] + 1 && c[_n][d[_n]] - f[_n][d[_n]] > 0)
                    {

                        int ret = DFS(next, Math.Min(c[_n][d[_n]] - f[_n][d[_n]], _flow));

                        if (ret > 0)
                        {

                            f[_n][d[_n]] += ret;

                            for (int i = 0; i < f[next].Count; i++)
                            {

                                if (line[next][i] != _n) continue;
                                f[next][i] -= ret;
                            }
                            // f[d[_n]][_n] -= ret;
                            return ret;
                        }
                    }
                }

                return 0;
            }

            void LinkLine()
            {

                line = new List<int>[2 * n + 2];
                c = new List<int>[2 * n + 2];
                f = new List<int>[2 * n + 2];
                for (int i = 0; i < line.Length; i++)
                {

                    line[i] = new(10);
                    c[i] = new(10);
                    f[i] = new(10);
                }

                source = 0;
                sink = 2 * n + 1;

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                for (int i = 0; i < row; i++)
                {

                    for (int j = 0; j < col; j++)
                    {

                        if (board[i][j] == -1) continue;
                        int from = SetIdx(board[i][j], true);

                        for (int dir = 0; dir < 4; dir++)
                        {

                            int nextR = i + dirR[dir];
                            int nextC = j + dirC[dir];

                            if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] == -1) continue;
                            int to = SetIdx(board[nextR][nextC], false);
                            line[from].Add(to);
                            line[to].Add(from);

                            c[from].Add(INF);
                            c[to].Add(0);

                            f[from].Add(0);
                            f[to].Add(0);
                        }
                    }
                }

                for (int i = 1; i <= n; i++)
                {

                    line[i].Add(i + n);
                    line[i + n].Add(i);

                    c[i].Add(1);
                    c[i + n].Add(0);

                    f[i].Add(0);
                    f[i + n].Add(0);
                }
            }

            int SetIdx(int _n, bool _isFrom)
            {

                if (_n == source || _n == sink) return _n;

                return _isFrom ? _n + n : _n;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                n = 0;
                int endR = -1;
                int endC = -1;

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();

                        if (cur == '#') board[r][c] = -1;
                        else if (cur == 'K') board[r][c] = 0;
                        else if (cur == 'H')
                        {

                            endR = r;
                            endC = c;
                        }
                        else board[r][c] = ++n;
                    }

                    if (sr.Read() == '\r') sr.Read(); 
                }

                board[endR][endC] = 2 * n + 1;

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System.Linq;
using System.IO;

namespace 학교_가지마 {
    class Endpoint {
        public int Number { get; init; }
        public List<DirectedEdge> Outgoing { get; set; }

        public Endpoint(int num) {
            Number = num;
            Outgoing = new List<DirectedEdge>();
        }
    }

    class DirectedEdge {
        public static readonly DirectedEdge Dummy = new DirectedEdge();

        public Endpoint Source { get; init; }
        public Endpoint Destination { get; init; }
        public long Capacity { get; set; } = 0;
        public long Flow { get; private set; } = 0;
        public DirectedEdge Inverse { get; private set; }

        public long Spare => Capacity - Flow;

        public DirectedEdge(Endpoint src, Endpoint dst, long capacity) {
            Source = src;
            Destination = dst;
            Capacity = capacity;
            Inverse = new DirectedEdge(this);

            Source.Outgoing.Add(this);
        }

        public DirectedEdge(Vertex src, Vertex dst, long capacity)
            : this(src.Out, dst.In, capacity) {}

        private DirectedEdge(DirectedEdge inverse) {
            Source = inverse.Destination;
            Destination = inverse.Source;
            Inverse = inverse;

            Source.Outgoing.Add(this);
        }

        private DirectedEdge() {}

        public void AddFlow(long add) {
            Flow += add;
            Inverse.Flow -= add;
        }
    }

    class Vertex {
        public int Number { get; init; }
        public Endpoint In { get; init; }
        public Endpoint Out { get; init; }
        public DirectedEdge InnerEdge { get; init; }

        public Vertex(int num, long capacity = long.MaxValue/2) {
            Number = num;
            In = new Endpoint(num*2);
            Out = new Endpoint(num*2+1);
            InnerEdge = new DirectedEdge(In, Out, capacity);
        }
    }

    class Graph {
        private Vertex[] vertices;

        public Graph(int verCount) {
            vertices = new Vertex[verCount];
            for (int i = 0; i < verCount; ++i) {
                vertices[i] = new Vertex(i);
            }
        }

        public void SetInnerCapacity(int verIdx, long capacity) {
            vertices[verIdx].InnerEdge.Capacity = capacity;
        }

        public void AddEdge(int src, int dst, long capacity) {
            new DirectedEdge(vertices[src], vertices[dst], capacity);
        }

        private bool FindAugmentingPath(Vertex src, Vertex dst, out DirectedEdge[] prev) {
            prev = new DirectedEdge[vertices.Length*2];
            Queue<Endpoint> q = new Queue<Endpoint>();
            q.Enqueue(src.In);
            prev[src.In.Number] = DirectedEdge.Dummy;

            while (q.Count > 0) {
                var curr = q.Dequeue();

                foreach (var edge in curr.Outgoing) {
                    if (0 < edge.Spare && prev[edge.Destination.Number] == null) {
                        q.Enqueue(edge.Destination);
                        prev[edge.Destination.Number] = edge;
                    }
                    if (prev[dst.Out.Number] != null) {
                        return true;
                    }
                }
            }

            return false;
        }

        public long EdmondsKarp(int src, int dst) {
            Vertex vs = vertices[src], vd = vertices[dst];
            long result = 0;
            DirectedEdge[] prev;

            while (FindAugmentingPath(vs, vd, out prev)) {
                long add = long.MaxValue;

                for (var edge = prev[vd.Out.Number]; edge != DirectedEdge.Dummy; edge = prev[edge.Source.Number]) {
                    add = Math.Min(add, edge.Spare);
                }
                for (var edge = prev[vd.Out.Number]; edge != DirectedEdge.Dummy; edge = prev[edge.Source.Number]) {
                    edge.AddFlow(add);
                }

                result += add;
            }

            return result;
        }
    }

    class Program {
        static void Main(string[] args) {
            var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var (n, m) = (tokens[0], tokens[1]);
            int verCount = n*m;
            Graph g = new Graph(verCount);
            string[] map = new string[n];
            
            for (int i = 0; i < n; ++i) {
                map[i] = Console.ReadLine();
            }

            int src = 0, dst = 0;
            int srcRow = 0, srcCol = 0;
            int dstRow = 0, dstCol = 0;

            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    if (map[i][j] == '#') {
                        continue;
                    }
                    int currIdx = i*m + j;

                    int[] dr = { 0, 0, 1, -1 };
                    int[] dc = { 1, -1, 0, 0 };
                    for (int k = 0; k < 4; ++k) {
                        int nr = dr[k] + i;
                        int nc = dc[k] + j;
                        int nextIdx = nr*m + nc;
                        if (0 <= nr && nr < n && 0 <= nc && nc < m && map[nr][nc] != '#') {
                            g.AddEdge(currIdx, nextIdx, 1);
                        }
                    }

                    switch (map[i][j]) {
                        case 'H':
                            dst = currIdx;
                            dstRow = i; dstCol = j;
                            break;
                        case 'K':
                            src = currIdx;
                            srcRow = i; srcCol = j;
                            break;
                        default:
                            g.SetInnerCapacity(currIdx, 1);
                            break;
                    }
                }
            }
            
            if (Math.Abs(srcRow-dstRow) + Math.Abs(srcCol-dstCol) == 1) {
                Console.WriteLine("-1");
            } else {
                Console.WriteLine(g.EdmondsKarp(src, dst));
            }
        }
    }
}
#elif other2
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;
import java.util.StringTokenizer;

public class Main {
	private static StringBuffer ret = new StringBuffer();
	private static BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
	private static StringTokenizer st;
	private static int n_cnt, N, M, source, sink;
	private static Edge prev[];
	private static char map[][];
	private static List<Edge> edge[];

	private static class Edge {
		int from, to, capa, flow;
		Edge rev;

		public Edge(int from, int to, int capa) {
			this.from = from;
			this.to = to;
			this.capa = capa;
		}
	}

	@Override
	public void finalize() throws Exception {
		br.close();
	}

	public static void main(String[] args) throws Exception {
		st = new StringTokenizer(br.readLine());
		N = Integer.parseInt(st.nextToken());
		M = Integer.parseInt(st.nextToken());
		n_cnt = (N * M) * 2;
		edge = new List[n_cnt];

		for (int i = 0; i < n_cnt; i++) {
			edge[i] = new ArrayList<>();
		}
		map = new char[N][M];
		String line;
		for (int i = 0; i < N; i++) {
			line = br.readLine();
			for (int j = 0; j < M; j++) {
				map[i][j] = line.charAt(j);
			}
		}

		Edge e, re;
		for (int i = 0; i < N; i++) {
			for (int j = 0; j < M; j++) {
				if (map[i][j] == '#')
					continue;
				if (map[i][j] == 'K') {
					source = node_idx(i, j, 1);
					continue;
				}
				if (map[i][j] == 'H')
					sink = node_idx(i, j, 0);
				e = new Edge(node_idx(i, j, 0), node_idx(i, j, 1), 1);
				edge[node_idx(i, j, 0)].add(e);
				re = new Edge(node_idx(i, j, 1), node_idx(i, j, 0), 0);
				edge[node_idx(i, j, 1)].add(re);
				e.rev = re;
				re.rev = e;
				for (int k = 0; k < 4; k++) {
					if (i + dx[k] < 0 || i + dx[k] >= N || j + dy[k] < 0 || j + dy[k] >= M
							|| map[i + dx[k]][j + dy[k]] == '#')
						continue;
					e = new Edge(node_idx(i + dx[k], j + dy[k], 1), node_idx(i, j, 0), Integer.MAX_VALUE);
					re = new Edge(node_idx(i, j, 0), node_idx(i + dx[k], j + dy[k], 1), 0);
					e.rev = re;
					re.rev = e;
					edge[node_idx(i + dx[k], j + dy[k], 1)].add(e);
					edge[node_idx(i, j, 0)].add(re);
				}
			}
		}
		solve();

		System.out.println(ret);
	}

	private static int node_idx(int x, int y, int in_out) {
		return (x * M + y) * 2 + in_out;
	}

	private static int dx[] = new int[] { 1, -1, 0, 0 };
	private static int dy[] = new int[] { 0, 0, 1, -1 };

	public static void solve() throws Exception {
		int sol = 0;
		while (bfs()) {

			int min_flow = Integer.MAX_VALUE;

			for (Edge e = prev[sink]; e != null; e = prev[e.from]) {
				min_flow = Math.min(min_flow, e.capa - e.flow);
			}

			if (min_flow == Integer.MAX_VALUE) {
				ret.append(-1);
				return;
			}

			for (Edge e = prev[sink]; e != null; e = prev[e.from]) {
				e.flow += min_flow;
				e.rev.flow -= min_flow;
			}
			sol += min_flow;
		}
		ret.append(sol);
	}

	private static boolean bfs() {
		prev = new Edge[n_cnt];
		prev[source] = null;
		Queue<Integer> q = new LinkedList<>();
		q.add(source);

		int n;
		while (!q.isEmpty()) {
			n = q.poll();
			for (Edge e : edge[n]) {
				if (e.capa - e.flow > 0 && e.to != source && prev[e.to] == null) {
					prev[e.to] = e;
					q.add(e.to);
				}
			}
		}
		return prev[sink] != null;
	}
}
#elif other3
using ProblemSolving.Templates;
using ProblemSolving.Templates.MaxFlow;
using ProblemSolving.Templates.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates {}
namespace ProblemSolving.Templates.MaxFlow {}
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Collections.Generic {}
namespace System.IO {}
namespace System.Linq {}

#nullable disable

public record struct Node(bool IsInNode, int Y, int X);

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var (n, m) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var kpos = (y: 0, x: 0);
        var hpos = (y: 0, x: 0);

        var map = new Char[n, m];
        for (var y = 0; y < n; y++)
        {
            var s = sr.ReadLine();
            for (var x = 0; x < m; x++)
            {
                map[y, x] = s[x];

                if (s[x] == 'K')
                    kpos = (y, x);
                if (s[x] == 'H')
                    hpos = (y, x);
            }
        }

        if (Math.Abs(hpos.y - kpos.y) + Math.Abs(hpos.x - kpos.x) == 1)
        {
            sw.WriteLine(-1);
            return;
        }

        var flow = new MaxFlowHelper<Node>();
        for (var y = 0; y < n; y++)
            for (var x = 0; x < m; x++)
            {
                if (map[y, x] == '#')
                    continue;

                flow.AddEdge(new Node(true, y, x), new Node(false, y, x), 1, 0);

                foreach (var (dy, dx) in Moveset.Moveset4)
                {
                    var (ny, nx) = (y + dy, x + dx);
                    if (ny < 0 || nx < 0 || ny >= n || nx >= m)
                        continue;

                    if (map[ny, nx] == '#')
                        continue;

                    flow.AddEdge(new Node(false, y, x), new Node(true, ny, nx), 1, 0);
                }
            }

        var f = flow.FindMaxFlow(new Node(false, kpos.y, kpos.x), new Node(true, hpos.y, hpos.x));
        sw.WriteLine(f);
    }
}

namespace ProblemSolving.Templates.Utility
{
    public static class DeconstructHelper
    {
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
    }
}


namespace ProblemSolving.Templates.MaxFlow
{
    public class MaxFlowHelper<TNode>
        where TNode : struct
    {
        private struct FlowEdge
        {
            public int Dst;
            public int Opp;
            public long Flow;
            public long Capacity;

            public FlowEdge(int dst, int opp, long capacity)
            {
                this.Dst = dst;
                this.Opp = opp;
                this.Flow = 0;
                this.Capacity = capacity;
            }

            public bool CanFlow => Flow < Capacity;
        }

        private Dictionary<TNode, int> _map;
        private List<RefableList<FlowEdge>> _graph;

        public MaxFlowHelper()
        {
            _map = new Dictionary<TNode, int>();
            _graph = new List<RefableList<FlowEdge>>();
        }

        private int GetId(TNode node)
        {
            if (!_map.TryGetValue(node, out var v))
            {
                v = _map.Count;
                _map[node] = v;
                _graph.Add(new RefableList<FlowEdge>());
            }

            return v;
        }
        public void AddEdge(TNode src, TNode dst, long forwardCap, long backwardCap)
        {
            var srcId = GetId(src);
            var dstId = GetId(dst);

            var srcopp = _graph[dstId].Count;
            var dstopp = _graph[srcId].Count;

            _graph[srcId].Add(new(dstId, srcopp, forwardCap));
            _graph[dstId].Add(new(srcId, dstopp, backwardCap));
        }

        public long FindMaxFlow(TNode source, TNode destination)
        {
            var sourceId = GetId(source);
            var destinationId = GetId(destination);
            var flow = 0L;

            var n = _graph.Count;
            var levelGraph = new int[n];

            while (true)
            {
                var flowOccured = false;
                RebuildLevelGraph(sourceId, destinationId, levelGraph);

                while (true)
                {
                    var f = MakeFlow(sourceId, destinationId, levelGraph, Int64.MaxValue);
                    if (f == 0)
                        break;

                    flow += f;
                    flowOccured = true;
                }

                if (!flowOccured)
                    break;
            }

            return flow;
        }

        private long MakeFlow(int pos, int sink, int[] levelGraph, long currFlow)
        {
            if (currFlow == 0)
            {
                levelGraph[pos] = -1;
                return 0;
            }

            if (pos == sink)
                return currFlow;

            var currlevel = levelGraph[pos];
            var g = _graph[pos];
            var count = g.Count;
            for (var idx = 0; idx < count; idx++)
            {
                ref var forward = ref g[idx];

                if (!forward.CanFlow)
                    continue;

                if (currlevel >= levelGraph[forward.Dst])
                    continue;

                var flow = MakeFlow(forward.Dst, sink, levelGraph, Math.Min(forward.Capacity - forward.Flow, currFlow));
                if (flow == 0)
                    continue;

                ref var backward = ref _graph[forward.Dst][forward.Opp];
                forward.Flow += flow;
                backward.Flow -= flow;

                return flow;
            }

            levelGraph[pos] = -1;
            return 0;
        }

        private void RebuildLevelGraph(int source, int destination, int[] levelGraph)
        {
            var q = new Queue<(int pos, int level)>();
            q.Enqueue((source, 0));

            Array.Fill(levelGraph, -1);
            levelGraph[source] = 0;

            while (q.TryDequeue(out var state))
            {
                var (pos, level) = state;

                if (pos == destination)
                    continue;

                var g = _graph[pos];
                var count = g.Count;
                for (var idx = 0; idx < count; idx++)
                {
                    ref var e = ref g[idx];
                    if (e.CanFlow && levelGraph[e.Dst] == -1)
                    {
                        levelGraph[e.Dst] = level + 1;
                        q.Enqueue((e.Dst, level + 1));
                    }
                }
            }
        }
    }
}

namespace ProblemSolving.Templates
{
    public static class Moveset
    {
        public static readonly (int dy, int dx)[] Moveset4 = new (int dy, int dx)[]
        {
            (-1,0),(1,0),(0,-1),(0,1),
        };

        public static readonly (int dy, int dx)[] Moveset8 = new (int dy, int dx)[]
        {
            (-1,-1),(-1,0),(-1,1),(0,-1),(0,1),(1,-1),(1,0),(1,1)
        };
    }
}


namespace ProblemSolving.Templates
{
    internal class RefableList<T>
    {
        private T[] _arr;
        public int Count { get; private set; }

        public RefableList() : this(16)
        {
        }
        public RefableList(int capacity)
        {
            _arr = new T[capacity];
        }

        public void Add(T elem)
        {
            if (Count == _arr.Length)
            {
                var newarr = new T[_arr.Length * 2];
                Array.Copy(_arr, newarr, _arr.Length);
                _arr = newarr;
            }

            _arr[Count++] = elem;
        }

        public ref T this[int idx] => ref _arr[idx];
    }
}

// This is source code merged w/ template
// Timestamp: 2024-07-07 22:16:55 UTC+9

#endif
}
