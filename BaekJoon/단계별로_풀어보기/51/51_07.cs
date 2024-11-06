using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 27
이름 : 배성훈
내용 : 숫자판 만들기
    문제번호 : 2365번

    최대 유량, 이분 탐색, 매개변수 탐색 문제다
    설마 i 행의 합 sriR과 j 열의 scj으로 간선을이어야 하나? 
    해서 해당 간선을 노드로 설정해 그래프를 만들었다

    처음에는 그냥 sri, scj를 i -> j, j -> i 에 흐르는 용량으로 설정해 네트워크 플로우 알고리즘을 돌렸다 
    이후 돌려보니 그냥 (행 또는 열의) 총합만 흐르게하는 엉뚱한 결과만 내놓았다
    이게 맞나 하고 의문이 들었고, 고민하다가 방법을 몰라 결국 검색을 했다

    그래서 아래와 같은 풀이가 나왔다
    그런데 이분 탐색 후 찾은 결과값으로 다시 유량 검색을 해주지 않아 2번 틀렸다
        2
        10000 10000
        10000 10000
    을 돌려보니, 만약 최대 유량인 r을 찾았지만 아직 l <= r이라 이분 탐색을 더 하는데 l값 조정한다고 f의 값이 변하기 때문이다

    그리고 c에는 n + 1개의 간선 정보만 저장하고 싶은데
    이 방법을 쓸려면 역간선의 정보도 저장해줘야한다
    역간선 정보는 포인터형으로 해줘야하는데, 
    다음 번에는 unsafe 키워드를 이용해 한 번 제출해봐야겠다
    https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/pointer-related-operators

    다른 사람 풀이는 역간선으로 풀었는데, 6초대 시간이 걸린다(6400ms에 육박)
*/

namespace BaekJoon._51
{
    internal class _51_07
    {

        static void Main7(string[] args)
        {

            int INF = 1_000_000;
            StreamReader sr;
            StreamWriter sw;

            int n;

            int source, sink;
            List<int>[] line;
            int[,] c, f;

            Queue<int> q;
            int[] lvl;
            int[] d;

            int total;
            int ret;

            Solve();
            void Solve()
            {

                Input();

                lvl = new int[sink + 1];
                d = new int[sink + 1];
                q = new(sink + 1);
                int l = 0;
                int r = 10_000;
                while(l <= r)
                {

                    int mid = (l + r) / 2;

                    int chk = MaxFlow(mid);

                    if (chk == total) r = mid - 1;
                    else l = mid + 1;
                }

                ret = r + 1;
                MaxFlow(ret);
                Output();
            }

            int MaxFlow(int _capacity)
            {

                int ret = 0;
                Init(_capacity);
                while (BFS())
                {

                    Array.Fill(d, 0);

                    while (true)
                    {

                        int flow = DFS(source, INF);
                        if (flow == 0) break;
                        ret += flow;
                    }
                }

                return ret;
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{ret}\n");

                for (int i = 1; i <= n; i++)
                {

                    for (int j = n + 1; j <= 2 * n; j++)
                    {

                        sw.Write($"{f[i, j]} ");
                    }

                    sw.Write('\n');
                }

                sw.Close();
            }

            bool BFS()
            {

                Array.Fill(lvl, -1);
                lvl[source] = 0;

                q.Enqueue(source);
                while(q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];

                        if (lvl[next] == -1 && c[node, next] - f[node, next] > 0)
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

                    if (lvl[next] == lvl[_n] + 1 && c[_n, next] - f[_n, next] > 0)
                    {

                        int ret = DFS(next, Math.Min(c[_n, next] - f[_n, next], _flow));

                        if (ret > 0)
                        {

                            f[_n, next] += ret;
                            f[next, _n] -= ret;

                            return ret;
                        }
                    }
                }

                return 0;
            }

            void Init(int _maxCapacity)
            {

                for (int i = 1; i <= n; i++) 
                {

                    for (int j = n + 1; j <= 2 * n; j++)
                    {

                        c[i, j] = _maxCapacity;
                    }
                }


                for (int i = 0; i <= sink; i++)
                {

                    for (int j = 0; j <= sink; j++)
                    {

                        f[i, j] = 0;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                source = 0;
                sink = 2 * n + 1;
                line = new List<int>[sink + 1];
                for (int i = 0; i < line.Length; i++)
                {

                    line[i] = new(n);
                }

                c = new int[sink + 1, sink + 1];
                f = new int[sink + 1, sink + 1];

                total = 0;
                for (int i = 1; i <= n; i++)
                {

                    int cur = ReadInt();
                    line[source].Add(i);
                    line[i].Add(source);

                    total += cur;
                    c[source, i] = cur;
                }

                for (int i = n + 1; i <= 2 * n; i++)
                {

                    line[i].Add(sink);
                    line[sink].Add(i);

                    c[i, sink] = ReadInt();
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = n + 1; j <= 2 * n; j++)
                    {

                        line[i].Add(j);
                        line[j].Add(i);
                    }
                }

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

namespace 숫자판_만들기 {
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

        public void ClearFlow() {
            Flow = 0;
            Inverse.Flow = 0;
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

        public long GetInnerFlow(int verIdx) {
            return vertices[verIdx].InnerEdge.Flow;
        }

        public void SetInnerCapacity(int verIdx, long capacity) {
            vertices[verIdx].InnerEdge.Capacity = capacity;
        }

        public void AddEdge(int src, int dst, long capacity = long.MaxValue/2) {
            new DirectedEdge(vertices[src], vertices[dst], capacity);
        }

        private bool FindAugmentingPath(Vertex src, Vertex dst, ref DirectedEdge[] prev) {
            Array.Fill(prev, null);
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
            ClearFlows();
            Vertex vs = vertices[src], vd = vertices[dst];
            DirectedEdge[] prev = new DirectedEdge[vertices.Length * 2];
            long result = 0;

            while (FindAugmentingPath(vs, vd, ref prev)) {
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

        public void ClearFlows() {
            foreach (var v in vertices) {
                foreach (var e in v.In.Outgoing) {
                    e.ClearFlow();
                }
                foreach (var e in v.Out.Outgoing) {
                    e.ClearFlow();
                }
            }
        }
    }

    class Program {
        static void Main(string[] args) {
            int n = int.Parse(Console.ReadLine());
            var rows = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var cols = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int sum = rows.Sum();

            // [0, n*n) = 숫자판의 각 자리
            // [n*n, n*n + n) = 행 별 합
            // [n*n + n, n*n + 2*n) = 열 별 합
            // n*n + 2*n = Global Source
            // n*n + 2*n + 1 = Global Destination
            Graph g = new Graph(n*n + 2*n + 2);
            Func<int, int, int> ToIdx = (r, c) => r*n + c;
            int src = n*n + 2*n;
            int dst = n*n + 2*n + 1;
            
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < n; ++j) {
                    // 행 별 합
                    g.AddEdge(n*n+i, ToIdx(i, j));
                    // 열 별 합
                    g.AddEdge(ToIdx(i, j), n*n+n+j);
                }
            }

            for (int i = 0; i < n; ++i) {
                g.SetInnerCapacity(n*n + i, rows[i]);
                g.SetInnerCapacity(n*n + n + i, cols[i]);

                // Global Source와 행 별 합 Vertices 연결
                g.AddEdge(src, n*n + i);
                // 열 별 합 Vertices와 Global Destination 연결
                g.AddEdge(n*n + n + i, dst);
            }
            
            long left = 0, right = 10000;
            while (left < right) {
                long mid = (left+right) / 2;
                
                for (int i = 0; i < n*n; ++i) {
                    g.SetInnerCapacity(i, mid);
                }

                if (g.EdmondsKarp(src, dst) == sum) {
                    right = mid;
                } else {
                    left = mid+1;
                }
            }

            for (int i = 0; i < n*n; ++i) {
                g.SetInnerCapacity(i, left);
            }
            g.EdmondsKarp(src, dst);

            using (StreamWriter writer = new StreamWriter(Console.OpenStandardOutput())) {
                writer.Write($"{left}\n");
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < n; ++j) {
                        writer.Write($"{g.GetInnerFlow(ToIdx(i, j))} ");
                    }
                    writer.Write("\n");
                }
            }
        }
    }
}
#elif other2
import io, os, sys
input=io.BytesIO(os.read(0,os.fstat(0).st_size)).readline

import collections
def dinic(C, src, snk) :
  V = len(C)
  F = [[0] * V for _ in range(V)]
  ret = 0

  def bfs(lvl) :
    Q = collections.deque()
    Q.append(src)
    lvl[src] = 0
    while Q :
      u = Q.popleft()
      for v in range(V) :
        if lvl[v] == -1 and C[u][v] - F[u][v] > 0 :
          lvl[v] = lvl[u] + 1
          Q.append(v)
    return lvl[snk] != -1

  def dfs(W, lvl, u, cur = 1 << 63) :
    if u == snk : return cur
    while W[u] < V :
      v = W[u]
      if lvl[v] == lvl[u] + 1 and C[u][v] > F[u][v] :
        nxt = min(cur, C[u][v] - F[u][v])
        tmp = dfs(W, lvl, v, nxt)
        if tmp > 0 :
          F[u][v] += tmp
          F[v][u] -= tmp
          return tmp
      W[u] += 1
    return 0

  while True :
    lvl = [-1] * V
    W = [0] * V
    if not bfs(lvl) : break
    while True:
      tmp = dfs(W, lvl, src)
      if tmp == 0 : break
      ret += tmp
  return ret, F

def validate(F, L):
  for i, l in enumerate(F) :
    sum = 0
    for v in l :
      sum += v
    if sum != L[i] : 
      return False
  return True

def sol():
  N = int(input())
  L1 = [*map(int, input().split())]
  L2 = [*map(int, input().split())]
  
  V = N * 2 + 2
  src = V - 2
  snk = V - 1
  C = [[0] * V for _ in range(V)]
  for i in range(N) :
    C[src][i] = L1[i]
    C[N+i][snk] = L2[i]

  def desc(c):
    for i in range(N) :
      for j in range(N) :
        C[i][N+j] = c
    
    res, F = dinic(C, src, snk)
    res = [[0] * N for _ in range(N)]

    for i in range(N) : 
      for j in range(N) :
        if not F[i][N+j]: continue
        res[i][j] = F[i][N+j]
    
    return (validate(res, L1) and validate(zip(*res), L2)), res
  
  lo = 0
  hi = ans = 10000
// # 이분탐색으로 플로우를 log(N)번 반복해서 최소값을 찾는다.
  while lo + 1 < hi: 
    mid = (lo + hi) // 2
    valid, res = desc(mid)
    if valid:
      ans = mid
      ans2 = res
      hi = mid
    else:
      lo = mid

  sys.stdout.write(str(ans) + '\n')
  for l in ans2 :
    sys.stdout.write(' '.join(map(str, l)) + '\n')

sol()
#endif
}
