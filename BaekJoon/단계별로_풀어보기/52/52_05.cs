using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 2
이름 : 배성훈
내용 : 두부장수 장홍준 2
    문제번호 : 11111번

    MCMF 문제다
    idx 문제로 예제부터 막혔었다;
    idx + 1로가는 간선이 존재해야하는데;
    idx - 1로 연결해서 idx - 1로 가는 간선이 2개 였다;
    ...

    그리고 이를 수정하니 아슬아슬하게 1892ms에 통과했다
    이후 다른 사람 풀이를 보고 조금 더 생각해보니
    source와 노드 sink와 노드로 가는 최대 용량을 1로 설정해서
    따로 자신을 in, out 노드 두 개로 나눌 필요가 없음을 알아차렸다
    그러니 1892ms -> 1104ms로 줄었다

    이후 SPFA의 부분을 보니 거리 추가를 매번 for문에서 해줄 필요가 없고
    dis의 값에 저장했으니 dis에서 한번만 연산해도 됨을 알았다
    이렇게 하니 1104ms -> 808ms로 줄었다

    또한 min으로 거리 부호를 바꿔 max를 했는데 확인에서 부호를 바꿔 해결할 수 있었다
    해당 부분은 주석으로 남긴다

    아이디어는 다음과 같다
    최대 비용이 나오는 두부를 만들어야한다
    즉 최대 비용이 되게 두부를 잘라야한다

    2 * 1모양으로만 자를 수 있는데, 해당 두부는 체스판처럼 그룹이 분할 될 수 있다
    그래서 체스판 처럼 분할 해서 간선을 이어줬다
    그리고 source에서 검정색 판(여기서는 열 번호 + 짝 번호가 짝수)에 간선을 만들고
    흰색 판에서 sink로 간선을 이어 최대 유량을 구하면
    흰색판과 검정 판을 최대한 묶어 주는 것과 동치이다

    여기서 source와 sink를 제외한 노드는 한 번만 지나야한다
    그래서 노드에 in, out을 해도 되고,
    검정 노드끼리는 독립서로 연결되는 경우가 없으므로! 그냥 source의 최대 용량을 1로 설정해도 된다!
    흰색노드도 마찬가지로 독립이므로 1로 sink에 연결해줘도 된다

    그런데 예제를 보면 버리는 경우, 즉 잇지 않는 경우도 고려해줘야한다
    이에 검정색 판에서도 sink로 가게 이어주면 2 * 1이 아닌 1 * 1로도 쓸 수 있음과 동형이다
    이렇게 간선을 잇고 최대 유량을 구하면 두부 자르는 경우로 된다

    이제 거리를 두부 가격으로 설정해 SPFA알고리즘을 적용하면 MCMF가 된다!
*/

namespace BaekJoon._52
{
    internal class _52_05
    {

        static void Main5(string[] args)
        {

            int INF = 100_000;
            StreamReader sr;

            int[][] score = new int[6][];
            score[0] = new int[6] { 10, 8, 7, 5, 0, 1 };
            score[1] = new int[6] { 8, 6, 4, 3, 0, 1 };
            score[2] = new int[6] { 7, 4, 3, 2, 0, 1 };
            score[3] = new int[6] { 5, 3, 2, 2, 0, 1 };
            score[4] = new int[6] { 0, 0, 0, 0, 0, 0 };
            score[5] = new int[6] { 1, 1, 1, 1, 0, 0 };

            int row, col;
            int[,] board;

            int[] dis, before;
            int[,] c, f, d;
            bool[] inQ;
            Queue<int> q;

            List<int>[] line;
            int source, sink;

            int ret;

            Solve();

            void Solve()
            {

                Input();

                ConnLine();

                MCMF();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row, col];
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        board[r, c] = sr.Read() - 'A';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
            }

            void ConnLine()
            {

                source = 0;


#if first

                int n = row * col;
                sink = n * 2 + 1;

                line = new List<int>[sink + 1];
                c = new int[sink + 1, sink + 1];
                f = new int[sink + 1, sink + 1];
                d = new int[sink + 1, sink + 1];

                for (int i = source; i <= sink; i++)
                {

                    line[i] = new();
                }

                for (int i = 1; i <= n; i++)
                {

                    line[i].Add(i + n);
                    line[i + n].Add(i);
                    c[i, i + n] = 1;
                }

                for (int i = 0; i < row; i++)
                {

                    for (int j = 0; j < col; j++)
                    {

                        int idx = i * col + j + 1;
                        line[idx + n].Add(sink);
                        line[sink].Add(idx + n);
                        c[idx + n, sink] = 1;

                        if ((i + j) % 2 == 0) continue;

                        line[source].Add(idx);
                        line[idx].Add(source);
                        c[source, idx] = 1;

                        if (j > 0)
                        {

                            int cur = score[board[i, j]][board[i, j - 1]];

                            line[n + idx].Add(idx - 1);
                            line[idx - 1].Add(n + idx);

                            c[n + idx, idx - 1] = 1;
                            d[n + idx, idx - 1] = -cur;
                            d[idx - 1, n + idx] = cur;
                        }

                        if (j < col - 1)
                        {

                            int cur = score[board[i, j]][board[i, j + 1]];

                            line[n + idx].Add(idx + 1);
                            line[idx + 1].Add(n + idx);

                            c[n + idx, idx + 1] = 1;
                            d[n + idx, idx + 1] = -cur;
                            d[idx + 1, n + idx] = cur;
                        }

                        if (i > 0)
                        {

                            int cur = score[board[i, j]][board[i - 1, j]];

                            line[n + idx].Add(idx - col);
                            line[idx - col].Add(n + idx);

                            c[n + idx, idx - col] = 1;
                            d[n + idx, idx - col] = -cur;
                            d[idx - col, n + idx] = cur;
                        }

                        if (i < row - 1)
                        {

                            int cur = score[board[i, j]][board[i + 1, j]];

                            line[n + idx].Add(idx + col);
                            line[idx + col].Add(n + idx);

                            c[n + idx, idx + col] = 1;
                            d[n + idx, idx + col] = -cur;
                            d[idx + col, n + idx] = cur;
                        }
                    }
                }

#else

                sink = row * col + 1;

                line = new List<int>[sink + 1];
                c = new int[sink + 1, sink + 1];
                f = new int[sink + 1, sink + 1];
                d = new int[sink + 1, sink + 1];

                for (int i = source; i <= sink; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < row; i++)
                {

                    for (int j = 0; j < col; j++)
                    {

                        int idx = i * col + j + 1;
                        line[idx].Add(sink);
                        line[sink].Add(idx);

                        c[idx, sink] = 1;

                        if ((i + j) % 2 == 0) continue;

                        line[source].Add(idx);
                        line[idx].Add(source);

                        c[source, idx] = 1;

                        if (j > 0)
                        {

                            int cur = score[board[i, j]][board[i, j - 1]];

                            line[idx].Add(idx - 1);
                            line[idx - 1].Add(idx);

                            c[idx, idx - 1] = 1;
                            d[idx, idx - 1] = -cur;
                            d[idx - 1, idx] = cur;
                        }

                        if (j < col - 1)
                        {

                            int cur = score[board[i, j]][board[i, j + 1]];

                            line[idx].Add(idx + 1);
                            line[idx + 1].Add(idx);

                            c[idx, idx + 1] = 1;
                            d[idx, idx + 1] = -cur;
                            d[idx + 1, idx] = cur;
                        }

                        if (i > 0)
                        {

                            int cur = score[board[i, j]][board[i - 1, j]];

                            line[idx].Add(idx - col);
                            line[idx - col].Add(idx);

                            c[idx, idx - col] = 1;
                            d[idx, idx - col] = -cur;
                            d[idx - col, idx] = cur;
                        }

                        if (i < row - 1)
                        {

                            int cur = score[board[i, j]][board[i + 1, j]];

                            line[idx].Add(idx + col);
                            line[idx + col].Add(idx);

                            c[idx, idx + col] = 1;
                            d[idx, idx + col] = -cur;
                            d[idx + col, idx] = cur;
                        }
                    }
                }
#endif
            }

            void MCMF()
            {
                
                q = new(sink + 1);
                inQ = new bool[sink + 1];
                dis = new int[sink + 1];
                before = new int[sink + 1];

                ret = 0;
                while (true)
                {

                    Array.Fill(before, -1);
                    Array.Fill(dis, INF);
                    Array.Fill(inQ, false);

                    dis[source] = 0;
                    q.Enqueue(source);
                    inQ[source] = true;
                    before[source] = source;

                    while(q.Count > 0)
                    {

                        int node = q.Dequeue();
                        inQ[node] = false;

                        for (int i = 0; i < line[node].Count; i++)
                        {

                            int next = line[node][i];
                            int nDis = d[node, next];

                            ///
                            /// 여기 부등호 바꿔도 된다
                            /// if (c[node, next] - f[node, next] > 0 && dis[next] < dis[node] + nDis)
                            /// 해당 경우
                            /// q while문 바로 위의 Array.Fill(dis, INF)를 -> Array.Fill(dis, -INF)로 수정해야한다
                            /// 그리고  ret -= dis[sink]를  -> ret += dis[sink]로 수정해야한다!
                            /// 마찬가지로 Conn 메서드에서 거리 부분도 cur 부분들의 부호를 모두 바꿔줘야한다!
                            /// 

                            if (c[node, next] - f[node, next] > 0 && dis[next] > dis[node] + nDis)
                            {

                                dis[next] = dis[node] + nDis;
                                before[next] = node;

                                if (!inQ[next])
                                {

                                    inQ[next] = true;
                                    q.Enqueue(next);
                                }
                            }
                        }
                    }

                    if (before[sink] == -1) break;
                    int flow = INF;

                    for (int i = sink; i != source; i = before[i])
                    {

                        flow = Math.Min(flow, c[before[i], i] - f[before[i], i]);
                    }

                    for (int i = sink; i != source; i = before[i])
                    {

                        // ret -= flow * d[before[i], i];
                        f[before[i], i] += flow;
                        f[i, before[i]] -= flow;
                    }

                    ret -= dis[sink];
                }
                Console.Write(ret);
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
using System.Linq;
using System.IO;

namespace MCMFImplementation {
    class Edge {
        public int Source { get; }
        public int Destination { get; }
        public int Flow { get; private set; }
        public int Cost { get; private set; }
        public int Capacity { get; private set; }
        public Edge Opposite { get; set; }

        public int Spare => Capacity - Flow;

        public Edge(int src, int dst, int cost, int capa) {
            Source = src;
            Destination = dst;
            Flow = 0;
            Cost = cost;
            Capacity = capa;
        }

        public void AddFlow(int add) {
            Flow += add;
            if (Opposite != null) {
                Opposite.Flow -= add;
            }
        }
    }

    public record MCMF {
        public int Flowed { get; set; }
        public int TotalCost { get; set; }
    }

    class Graph {
        public static readonly int Inf = int.MaxValue/2;
        private List<Edge>[] graph;
        
        public Graph(int vertexCount) {
            graph = new List<Edge>[vertexCount];
            for (int i = 0; i < vertexCount; ++i) {
                graph[i] = new List<Edge>();
            }
        }

        public void AddEdge(int src, int dst, int cost, int capa) {
            Edge curr = new Edge(src, dst, cost, capa);
            Edge oppo = new Edge(dst, src, -cost, 0);
            curr.Opposite = oppo;
            oppo.Opposite = curr;

            graph[src].Add(curr);
            graph[dst].Add(oppo);
        }

        private List<Edge> FindAugPath(int src, int dst) {
            Edge[] prev = new Edge[graph.Length];
            int[] dist = new int[graph.Length];
            bool[] inQueue = new bool[graph.Length];
            Array.Fill(dist, Inf);
            Queue<int> q = new();

            dist[src] = 0;
            inQueue[src] = true;
            q.Enqueue(src);

            while (q.Count > 0) {
                int u = q.Dequeue();
                inQueue[u] = false;
                
                foreach (Edge e in graph[u]) {
                    if (e.Spare <= 0) {
                        continue;
                    }
                    int v = e.Destination;
                    if (dist[u] + e.Cost < dist[v]) {
                        dist[v] = dist[u] + e.Cost;
                        prev[v] = e;
                        if (!inQueue[v]) {
                            inQueue[v] = true;
                            q.Enqueue(v);
                        }
                    }
                }
            }

            if (prev[dst] == null) {
                return null;
            }

            List<Edge> path = new();
            for (Edge e = prev[dst]; e != null; e = prev[e.Source]) {
                path.Add(e);
            }

            return path;
        }

        public MCMF GetMinCostMaxFlow(int src, int dst) {
            MCMF result = new MCMF { Flowed = 0, TotalCost = 0 };
            List<Edge> path;

            while ((path = FindAugPath(src, dst)) != null) {
                int add = int.MaxValue;
                int costSum = 0;

                foreach (Edge e in path) {
                    add = Math.Min(add, e.Spare);
                }
                foreach (Edge e in path) {
                    e.AddFlow(add);
                    costSum += e.Cost;
                }

                result.Flowed += add;
                result.TotalCost += costSum * add;
            }

            return result;
        }
    }

    public record Line(int Axis, int Start, int End, int Weight);

    class Program {
        static int[,] priceTable = {
            { 10, 8, 7, 5, 0, 1 },
            {  8, 6, 4, 3, 0, 1 },
            {  7, 4, 3, 2, 0, 1 },
            {  5, 3, 2, 2, 0, 1 },
            {  0, 0, 0, 0, 0, 0 },
            {  1, 1, 1, 1, 0, 0 },
        };

        static void Main(string[] args) {
            var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var (n, m) = (tokens[0], tokens[1]);

            int vertexCount = n*m+3;
            int src = vertexCount-3;
            int srcNeck = vertexCount-2;
            int dst = vertexCount-1;
            Graph graph = new Graph(vertexCount);

            string[] map = new string[n];
            for (int i = 0; i < n; ++i) {
                map[i] = Console.ReadLine();
            }

            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    char c1 = map[i][j], c2;
                    int n1 = i*m+j, n2;
                    bool even = (i+j) % 2 == 0;

                    if (j+1 < m) {
                        c2 = map[i][j+1];
                        n2 = i*m+j+1;
                        if (even) {
                            graph.AddEdge(n1, n2, -priceTable[c1-'A', c2-'A'], 1);
                        } else {
                            graph.AddEdge(n2, n1, -priceTable[c1-'A', c2-'A'], 1);
                        }
                    }

                    if (i+1 < n) {
                        c2 = map[i+1][j];
                        n2 = (i+1)*m+j;
                        if (even) {
                            graph.AddEdge(n1, n2, -priceTable[c1-'A', c2-'A'], 1);
                        } else {
                            graph.AddEdge(n2, n1, -priceTable[c1-'A', c2-'A'], 1);
                        }
                    }

                    if (even) {
                        graph.AddEdge(srcNeck, n1, 0, 1);
                    } else {
                        graph.AddEdge(n1, dst, 0, 1);
                    }
                }
            }

            graph.AddEdge(src, srcNeck, 0, n*m/2);
            graph.AddEdge(srcNeck, dst, 0, Graph.Inf);

            Console.WriteLine(-graph.GetMinCostMaxFlow(src, dst).TotalCost);
        }
    }
}

#elif other2
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.*;

import java.math.*;
import java.text.DecimalFormat;

public class Main {

	static class Reader {
		BufferedReader rr;
		StringTokenizer st;

		public Reader() {
			rr = new BufferedReader(new InputStreamReader(System.in));
		}

		String rstr() {
			while (st == null || !st.hasMoreElements()) {
				try {
					st = new StringTokenizer(rr.readLine());
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
			return st.nextToken();
		}

		int rint() {
			return Integer.parseInt(rstr());
		}

		long rlong() {
			return Long.parseLong(rstr());
		}

		short rshort() {
			return Short.parseShort(rstr());
		}

		byte rbyte() {
			return Byte.parseByte(rstr());
		}

		char rchar() {
			return rstr().charAt(0);
		}

		double rdouble() {
			return Double.parseDouble(rstr());
		}

		int[] rintar(int len) {
			int[] ii = new int[len];
			for (int i = 0; i < len; i++)
				ii[i] = rint();
			return ii;
		}

		long[] rlongar(int len) {
			long[] ii = new long[len];
			for (int i = 0; i < len; i++)
				ii[i] = rlong();
			return ii;
		}

		short[] rshortar(int len) {
			short[] ii = new short[len];
			for (int i = 0; i < len; i++)
				ii[i] = rshort();
			return ii;
		}

		char[] rcharar() {
			return rstr().toCharArray();
		}

		double[] rdoublear(int len) {
			double[] ii = new double[len];
			for (int i = 0; i < len; i++)
				ii[i] = rdouble();
			return ii;
		}

		String[] rstrar() {
			try {
				st = new StringTokenizer(rr.readLine());
			} catch (IOException e) {
				e.printStackTrace();
			}
			String[] ss = new String[st.countTokens()];
			for (int i = 0; i < ss.length; i++)
				ss[i] = st.nextToken();
			return ss;
		}
	}

	static BufferedWriter ww = new BufferedWriter(new OutputStreamWriter(System.out));

	static class Edge {
		int b, res, co;
		Edge duel;

		Edge(int a1, int a2, int a3) {
			b = a1;
			res = a2;
			co = a3;
		}
	}

	static int st = 0, en = 1, N, ecost = 0, eflow = 0, n, m;

	static ArrayList<Edge>[] gr;
	static final int inf = 10000;
	static boolean end = false;
	static byte[][] map;
	static int[][] grade = { { 10, 8, 7, 5, 1 }, { 8, 6, 4, 3, 1 }, { 7, 4, 3, 2, 1 }, { 5, 3, 2, 2, 1 },
			{ 1, 1, 1, 1, 0 } };

	static int[] cost, work;
	static boolean[] visit;

	public static void main(String[] args) throws IOException {
		Reader s = new Reader();
		n = s.rint();
		m = s.rint();
		init();
		for (int i = 0; i < n; i++) {
			char[] c = s.rcharar();
			for (int j = 0; j < m; j++) {
				byte cur = (byte) (c[j] - 'A');
				if (cur == 5)
					cur--;
				map[i][j] = cur;
			}
		}
		for (int i = 0; i < n; i++) {
			boolean odd = (i % 2 == 0) ? false : true;
			for (int j = 0; j < m; j++) {
				int in = (i * m + j) + 2;
				if (odd) {
					ae(st, in, 1, 0, false);
					int cmap = map[i][j];
					ade(in, i - 1, j, cmap);
					ade(in, i, j - 1, cmap);
					ade(in, i + 1, j, cmap);
					ade(in, i, j + 1, cmap);
				}
				ae(in, en, 1, 0, false);
				odd = !odd;
			}
		}

		mcmf();
		wien(-ecost);
		ww.flush();
		ww.close();
	}

	static void init() {
		N = n * m + 2;
		map = new byte[n][m];
		gr = new ArrayList[N];
		for (int i = 0; i < N; i++)
			gr[i] = new ArrayList<>(2);
		cost = new int[N];
		work = new int[N];
		visit = new boolean[N];
	}

	static void ade(int in, int i, int j, int cmap) {
		if (i < 0 || j < 0 || i >= n || j >= m)
			return;
		int in2 = (i * m + j) + 2;
		int nmap = map[i][j], cost = grade[cmap][nmap];
		ae(in, in2, 1, cost, true);
	}

	static void ae(int i, int j, int k, int co, boolean hasd) {
		Edge e1 = new Edge(j, k, -co);
		gr[i].add(e1);
		if (!hasd)
			return;
		Edge e2 = new Edge(i, 0, co);
		gr[j].add(e2);
		e1.duel = e2;
		e2.duel = e1;
	}

	static void spfa() {
		boolean[] hasq = new boolean[N];
		int[] cnt = new int[N];
		Deque<Integer> q = new ArrayDeque<>();
		Arrays.fill(cost, inf);
		cost[st] = 0;
		int cur = st;
		while (true) {
			for (Edge edge : gr[cur]) {
				if (edge.res <= 0)
					continue;
				int next = edge.b;
				if (cost[next] > cost[cur] + edge.co) {
					cost[next] = cost[cur] + edge.co;
					if (hasq[next])
						continue;
					q.add(next);
					hasq[next] = true;
				}
			}
			if (q.isEmpty())
				return;
			cur = q.poll();
			hasq[cur] = false;
		}
	}

	static void mcmf() {
		spfa();
		while (true) {
			Arrays.fill(work, 0);
			Arrays.fill(visit, false);
			while(true) {
				int cur = dfs(st, inf);
				if (cur * cost[en] >= 0) break;
				// for minimum cost, use <= 0
				ecost += cur * cost[en];
				eflow += cur;
				Arrays.fill(visit, false);
			}
			if(!update()) break;
		}
	}
	
	static boolean update() {
		int nv = inf;
		for (int i = 0; i < N; i++) {
			if(!visit[i]) continue;
			for (Edge e : gr[i]) {
				if (e.res > 0 && !visit[e.b]) nv = Math.min(nv, cost[i] + e.co - cost[e.b]);
			}
		}
		if (nv == inf) return false;
		for (int i = 0; i < N; i++) {
			if (!visit[i]) cost[i] += nv;
		}
		return true;
	}

	static int dfs(int i, int fl) {
		if (i == en)
			return fl;
		visit[i] = true;
		for (; work[i] < gr[i].size(); work[i]++) {
			Edge e = gr[i].get(work[i]);
			if (visit[e.b] || cost[e.b] != cost[i] + e.co || e.res <= 0)
				continue;
			int newf = dfs(e.b, Math.min(fl, e.res));
			if (newf > 0) {
				e.res -= newf;
				if(e.duel != null) e.duel.res += newf;
				return newf;
			}
		}
		return 0;
	}

	static void wits(long i) throws IOException {
		ww.write(Long.toString(i));
	}

	static void wen() throws IOException {
		ww.write("\n");
	}

	static void wien(long i) throws IOException {
		ww.write(i + "\n");
	}

	static void wis(long i) throws IOException {
		ww.write(i + " ");
	}
}

#elif other3
import java.io.*;
import java.util.*;

public class Main {
    private static final int INF = Integer.MAX_VALUE >> 4;
    private static final int[][] score = {
        {10, 8, 7, 5, 0, 1}
        , {8, 6, 4, 3, 0, 1}
        , {7, 4, 3, 2, 0, 1}
        , {5, 3, 2, 2, 0, 1}
        , {0, 0, 0, 0, 0, 0}
        , {1, 1, 1, 1, 0, 0}
    };
    private static final int[] rowDi = {-1, 0, 1, 0}, colDi = {0, -1, 0, 1};
    private static class Edge {
        int to, cap, flow, cost;
        Edge reverse;
        Edge (int to, int cap, int cost) {
            this.to = to;
            this.cap = cap;
            this.flow = 0;
            this.cost = cost;
        }
        
        int residual() {
            return cap-flow;
        }

        void setReverse(Edge reverse) {
            this.reverse = reverse;
        }
    }
    private static class MaxFlow {
        private final int size, source, sink;
        private final int[] dist, work, level;
        private final ArrayList<Edge>[] graph;
        private final Queue<Integer> que = new ArrayDeque<>();
        private final boolean[] inQue;
        MaxFlow(int size, int source, int sink) {
            this.size = size;
            this.source = source;
            this.sink = sink;
            
            dist = new int[size];
            work = new int[size];
            level = new int[size];
            inQue = new boolean[size];
            graph = new ArrayList[size];
            for (int i = 0; i < size; i++) {
                graph[i] = new ArrayList<>();
            }
        }

        void add(int from, int to, int cap, int cst) {
            Edge edge = new Edge(to, cap, cst);
            Edge reverse = new Edge(from, 0, -cst);
            graph[from].add(edge);
            graph[to].add(reverse);
            edge.setReverse(reverse);
            reverse.setReverse(edge);
        }

        int run() {
            int minCost = 0;
            while (spfa()) {
                int sumFlow = 0;
                Arrays.fill(work, 0);
                while (true) {
                    int flowVal = dfs(source, INF);
                    if (flowVal == 0) break;
                    sumFlow += flowVal;
                }
                minCost += dist[sink] * sumFlow;
            }
            return minCost;
        }

        boolean spfa() {
            que.clear();
            Arrays.fill(inQue, false);
            Arrays.fill(dist, INF);
            Arrays.fill(level, 0);
            que.add(source);
            dist[source] = 0;
            inQue[source] = true;
            level[source] = 1;
            while (!que.isEmpty()) {
                int crnt = que.poll();
                inQue[crnt] = false;
                for (Edge next : graph[crnt]) {
                    if (dist[next.to] <= dist[crnt] + next.cost || next.residual() <= 0) continue;
                    dist[next.to] = dist[crnt] + next.cost;
                    level[next.to] = level[crnt] + 1;
                    if (!inQue[next.to]) {
                        inQue[next.to] = true;
                        que.add(next.to);
                    }
                }
            }
            return dist[sink] != INF;
        }

        int dfs(int crnt, int flowVal) {
            if (crnt == sink) return flowVal;
            for (; work[crnt] < graph[crnt].size(); work[crnt]++) {
                Edge next = graph[crnt].get(work[crnt]);
                if (level[next.to] != level[crnt]+1 || dist[next.to] != dist[crnt]+next.cost|| next.residual() <= 0) continue;
                int ret = dfs(next.to, Integer.min(flowVal, next.residual()));
                if (ret > 0) {
                    next.flow += ret;
                    next.reverse.flow -= ret;
                    return ret;
                }
            }
            return 0;
        }
    }

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
        StringTokenizer st = new StringTokenizer(br.readLine());
        int numRow = Integer.parseInt(st.nextToken());
        int numCol = Integer.parseInt(st.nextToken());
        char[][] board = new char[numRow][numCol];
        for (int i = 0; i < numRow; i++) {
            board[i] = br.readLine().toCharArray();
        }
        final int size = numRow*numCol+2;
        final int source = numRow*numCol;
        final int sink = numRow*numCol+1;
        MaxFlow maxFlow = new MaxFlow(size, source, sink);
        
        for (int i = 0; i < numRow; i++) {
            for (int j = 0; j < numCol; j++) {
                if (((i+j) & 1) == 0) {
                    maxFlow.add(source, i*numCol+j, 1, 0);
                    for (int k = 0; k < 4; k++) {
                        int adjRow = i + rowDi[k];
                        int adjCol = j + colDi[k];
                        if (adjRow < 0 || adjRow >= numRow || adjCol < 0 || adjCol >= numCol) continue;
                        maxFlow.add(i*numCol+j, adjRow*numCol+adjCol, 1, -score[board[i][j]-'A'][board[adjRow][adjCol]-'A']);
                    }
                }
                maxFlow.add(i*numCol+j, sink, 1, 0);
            }
        }
        bw.write(Integer.toString(-maxFlow.run()));
        bw.flush();
    }
}
#endif
}
