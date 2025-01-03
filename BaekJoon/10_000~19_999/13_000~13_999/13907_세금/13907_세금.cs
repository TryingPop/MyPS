using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 3
이름 : 배성훈
내용 : 세금
    문제번호 : 13907번

    다익스트라, dp 문제다.
    아이디어는 다음과 같다.

    다익스트라 알고리즘을 보면, 해당 좌표에서 다음 좌표로 가는데,
    총 거리가 짧은 것을 우선 방문해 도착시간을 채워가는 알고리즘이다.
    노드는 많이 이동해야 전체 노드의 개수만큼 이동하고,
    그 중에서 최솟값이 존재해 최소 거리를 찾는 것이다.

    이에 다익스트라로 찾은 최단 경로는 많아야 전체 노드만큼 이동한다.
    그래서 cost[i][j] = val를 s에서 시작해 i로 가는데 j 노드를 거쳐 이동한 최소 거리 val를 담게 설정했다.
    그래서 j는 n 이하로 잡으면 된다.
    n보다 큰 값은 n이하의 경로에서 다른 노드를 이용해 
    이동하는 방법들 뿐이므로 최소가 될 수 없다.
    
    이렇게 최소 거리를 찾고, 이제 모두 k만큼 가격이 오를때,
    cost[i][j] = val에 j개의 간선을 거쳐 이동하므로
    cost[i][j] = j * k + val로 갱신했다.

    이후 모든 경우를 일일히 탐색하며 정답을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1244
    {

        static void Main1244(string[] args)
        {

            long INF = 1_000_000_000_000_000;
            StreamReader sr;

            int n, m, k;
            List<(int dst, int cost)>[] edge;
            long[][] cost;
            int s, e;
            Solve();
            void Solve()
            {

                Input();

                SetDis();

                Dijkstra();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{GetMin(0)}\n");
                for (int i = 0; i < k; i++)
                {

                    int add = ReadInt();
                    sw.Write($"{GetMin(add)}\n");
                }

                long GetMin(long _add = 0)
                {

                    long ret = INF;
                    for (int i = 0; i <= n; i++)
                    {

                        cost[e][i] += _add * i;
                        ret = Math.Min(ret, cost[e][i]);
                    }

                    return ret;
                }
            }

            void SetDis()
            {

                cost = new long[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    cost[i] = new long[n + 1];
                    Array.Fill(cost[i], INF);
                }
            }

            void Dijkstra()
            {

                PriorityQueue<(int dst, long cost, int cnt), long> pq = new(n * n);

                cost[s][0] = 0;
                pq.Enqueue((s, 0, 0), 0);
                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();
                    int nCnt = node.cnt + 1;
                    for (int i = 0; i < edge[node.dst].Count; i++)
                    {

                        int next = edge[node.dst][i].dst;
                        long nCost = edge[node.dst][i].cost + node.cost;

                        if (cost[next][nCnt] <= nCost || n == nCnt) continue;

                        cost[next][nCnt] = nCost;
                        pq.Enqueue((next, nCost, nCnt), nCost);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                s = ReadInt();
                e = ReadInt();

                edge = new List<(int dst, int cost)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int c = ReadInt();

                    edge[f].Add((t, c));
                    edge[t].Add((f, c));
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;

// #nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nmk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nmk[0];
        var m = nmk[1];
        var k = nmk[2];

        var sd = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var s = sd[0] - 1;
        var d = sd[1] - 1;

        var graph = new List<(int dst, long cost)>[n];
        for (var idx = 0; idx < n; idx++)
            graph[idx] = new List<(int dst, long cost)>();

        while (m-- > 0)
        {
            var e = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            graph[e[0] - 1].Add((e[1] - 1, e[2]));
            graph[e[1] - 1].Add((e[0] - 1, e[2]));
        }

        // dist[m] = distance to d, with m movement
        var dist = new long?[n];
        var visited = new bool[n];
        var minmove = new int?[n];

        var q = new Queue<(int node, int level)>();
        q.Enqueue((d, 0));
        while (q.TryDequeue(out var state))
        {
            if (minmove[state.node].HasValue)
                continue;

            minmove[state.node] = state.level;

            foreach (var (dst, _) in graph[state.node])
                q.Enqueue((dst, 1 + state.level));
        }

        var pq = new PriorityQueue<(int node, int movecount), long>();
        for (var maxmove = 1; maxmove < n; maxmove++)
        {
            pq.Clear();
            Array.Clear(visited);

            pq.Enqueue((s, 0), 0);
            while (pq.TryDequeue(out var state, out var costsum))
            {
                var (node, movecount) = state;

                if (!minmove[node].HasValue || movecount + minmove[node].Value > maxmove)
                    continue;

                if (visited[node])
                    continue;

                visited[node] = true;

                if (node == d)
                {
                    if (maxmove == movecount)
                        dist[maxmove] = costsum;

                    break;
                }

                if (movecount < maxmove)
                {
                    foreach (var (dst, cost) in graph[node])
                        if (!visited[dst])
                            pq.Enqueue((dst, 1 + movecount), costsum + cost);
                }
            }
        }

        var p = new int[1 + k];
        for (var idx = 1; idx <= k; idx++)
            p[idx] = Int32.Parse(sr.ReadLine());

        foreach (var v in p)
        {
            var mincost = Int64.MaxValue;
            for (var movecount = 1; movecount < n; movecount++)
                if (dist[movecount].HasValue)
                {
                    dist[movecount] += movecount * v;
                    mincost = Math.Min(mincost, dist[movecount].Value);
                }

            sw.WriteLine(mincost);
        }
    }
}

#elif other2
// #include <iostream>
// #include <algorithm>
// #include <vector>
// #include <queue>
// #include <unordered_map>
// #define fastio ios::sync_with_stdio(false);cin.tie(0);cout.tie(0);
// #define INF 333333333

using namespace std;
int N, M, K, S, D;
typedef pair<int, int> edge;
vector<edge> edges[1001];
typedef struct element{
  int wgh, num, cnt;
}elm;
struct cmp{
  bool operator()(elm &a, elm &b){
    return a.wgh > b.wgh;
  }
};
priority_queue<elm, vector<elm>, cmp> pq;
queue<elm> q;
int dst[1001];
vector<edge> routes;

void input(){
  fastio
  cin >> N >> M >> K >> S >> D;
  int a, b, w;
  while (M --){
    cin >> a >> b >> w;
    edges[a].push_back({w, b});
    edges[b].push_back({w, a});
  }
}

void dijkstra(){

  for (int i = 1; i <= N; ++ i)
    dst[i] = INF;

  dst[S] = 0;
  q.push({0, S, 0});
  // pq.push({0, S, 0});

  while (q.size()){
    elm e = q.front();
    int cur = e.num;
    int cnt = e.cnt;
    int cw = e.wgh;
    q.pop();

    if (cur == D) routes.push_back({cw, cnt});

    for (edge e : edges[cur]){
      int nxt = e.second;
      int nw = e.first;
      if (dst[nxt] > cw + nw){
        dst[nxt] = cw + nw;
        q.push({dst[nxt], nxt, cnt + 1});
      }
    }
  }
}

int calculate(int p){
  int ret = INF;
  for (auto &e : routes){
    e.first += e.second * p;
    ret = min(ret, e.first);
  }
  return ret;
}

void solve(){
  dijkstra();
  
  cout << calculate(0) << '\n';

  int p;
  while (K --){
    cin >> p;
    cout << calculate(p) << '\n';
  }
}

int main(){
  input();
  solve();
  return 0;
}
#endif
}
