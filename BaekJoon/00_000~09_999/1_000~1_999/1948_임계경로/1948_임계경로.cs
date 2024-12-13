using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 13
이름 : 배성훈
내용 : 임계경로
    문제번호 : 1948번

    위상정렬, 방향 비순환 그래프 문제다.
    아이디어는 다음과 같다.
    사이클이 없고 단방향 간선이라 했다.
    그리고 시작 노드에서 모든 노드로 갈 수 있고,
    모든 노드에서 도착노드로 갈 수 있다고 한다.
    그러면 원피스 만화에 나오는 위대한 항고 경로와 같다.
    시작지점을 A, 도착지점을 Z라 하면 다음과 같다.
        A
      /   \
    B       C
    |    /  | 
    D   E   F
        ...
      \ | /
        Z

    그래서 결국에 Z로 도착할 수 밖에 없다.
    이에 모든 간선에서 해당 노드로 도착했을 때 해당 노드를 탐색하며
    가장 늦은 도착 시간을 구하면 된다.

    이후 역방향 탐색은 역방향 간선으로 해당 노드로 가는 
    시간인지 확인하면 된다. 즉 해당 노드의 시간 - 이동거리 = 다음 노드 거리
    인 경우 해당 간선이 채택된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1184
    {

        static void Main1184(string[] args)
        {

            int n, m;
            int s, e;
            List<(int dst, int dis)>[] edge, revEdge;

            int[] time;
            int[] degree;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Queue<int> q = new(n);
                q.Enqueue(s);

                while(q.Count > 0)
                {

                    int node = q.Dequeue();
                    int cur = time[node];
                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i].dst;
                        int nDis = edge[node][i].dis + cur;
                        time[next] = Math.Max(time[next], nDis);
                        degree[next]--;

                        if (degree[next] == 0) q.Enqueue(next);
                    }
                }

                q.Enqueue(e);
                int edgeCnt = 0;
                bool[] visit = new bool[n + 1];
                visit[e] = true;
                while(q.Count > 0)
                {

                    int node = q.Dequeue();
                    int cur = time[node];

                    for (int i = 0; i < revEdge[node].Count; i++)
                    {

                        int next = revEdge[node][i].dst;
                        int popDis = revEdge[node][i].dis;

                        if (cur == time[next] + popDis) 
                        { 
                            
                            edgeCnt++;
                            if (!visit[next]) 
                            {

                                q.Enqueue(next); 
                                visit[next] = true;
                            }
                        }
                    }
                }

                Console.Write($"{time[e]}\n{edgeCnt}");
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                edge = new List<(int dst, int dis)>[n + 1];
                revEdge = new List<(int dst, int dis)>[n + 1];
                time = new int[n + 1];
                degree = new int[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                    revEdge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int dis = ReadInt();

                    edge[f].Add((t, dis));
                    revEdge[t].Add((f, dis));
                    degree[t]++;
                }

                s = ReadInt();
                e = ReadInt();

                sr.Close();
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
    }
#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// #nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var m = Int32.Parse(sr.ReadLine());

        var revgraph = new List<(int src, int cost)>[n];
        for (var idx = 0; idx < n; idx++)
            revgraph[idx] = new List<(int src, int cost)>();

        while (m-- > 0)
        {
            var edge = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            revgraph[edge[1] - 1].Add((edge[0] - 1, edge[2]));
        }

        var srcdst = sr.ReadLine().Split(' ').Select(s => Int32.Parse(s) - 1).ToArray();
        var (src, dst) = (srcdst[0], srcdst[1]);

        var dists = new long?[n];
        var prevs = new List<int>[n];
        for (var idx = 0; idx < n; idx++)
            prevs[idx] = new List<int>();

        dists[src] = 0;

        BuildDAG(revgraph, dists, prevs, dst);
        sw.WriteLine(dists[dst]);

        var visited = new bool[n, n];
        var roadCount = 0;
        var q = new Queue<(int src, int dst)>();

        foreach (var p in prevs[dst])
            q.Enqueue((p, dst));

        while (q.TryDequeue(out var state))
        {
            var (s, e) = state;

            if (visited[s, e])
                continue;

            visited[s, e] = true;
            roadCount++;

            foreach (var p in prevs[s])
                q.Enqueue((p, s));
        }

        sw.WriteLine(roadCount);
    }

    private static long BuildDAG(List<(int src, int cost)>[] revgraph, long?[] dists, List<int>[] prevs, int v)
    {
        if (dists[v].HasValue)
            return dists[v].Value;

        var maxcost = 0L;
        foreach (var (src, cost) in revgraph[v])
        {
            var costSum = BuildDAG(revgraph, dists, prevs, src) + cost;

            if (maxcost < costSum)
            {
                maxcost = costSum;
                prevs[v].Clear();
                prevs[v].Add(src);
            }
            else if (maxcost == costSum)
            {
                prevs[v].Add(src);
            }
        }

        dists[v] = maxcost;
        return maxcost;
    }
}

#elif other2
// #include <iostream>
// #include <vector>
// #include <queue>
// #include <algorithm>
// #include <string>
// #include <cmath>
using namespace std;
int N,M,S,E;

vector<pair<int,int>> edge[10001];
vector<int> path[10001];
int DP[10001];
bool visit[10001];
int ans;

int dp(int x) {
    if (DP[x] || x == S) return DP[x];

    int time = 0;
    for (int i=0;i<edge[x].size();i++) {
        int t = dp(edge[x][i].first);
        if (t+edge[x][i].second > time) {
            time = t+edge[x][i].second;
        }
    }

    for (int i=0;i<edge[x].size();i++) {
        int t = dp(edge[x][i].first);
        if (t+edge[x][i].second == time) {
            path[x].push_back(edge[x][i].first);
        }
    }

    DP[x] = time;
    return DP[x];
}

void dfs(int x) {
    for (int i=0;i<path[x].size();i++) {
        ans++;
        if (visit[path[x][i]] == false) {
            visit[path[x][i]] = true;
            dfs(path[x][i]);
        }
    }
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);
    cin >> N >> M;
    for (int i=0;i<M;i++) {
        int a,b,c;
        cin >> a >> b >> c;
        edge[b].push_back({a,c});
    }
    cin >> S >> E;

    DP[S] = 0;
    dp(E);
    dfs(E);

    cout << DP[E] << '\n' << ans << '\n';
    return 0;
}
#endif
}
