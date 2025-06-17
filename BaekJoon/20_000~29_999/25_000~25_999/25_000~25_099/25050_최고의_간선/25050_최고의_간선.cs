using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 31
이름 : 배성훈
내용 : 최고의 간선
    문제번호 : 25050번

    다익스트라 문제다.
    다익스트라로 최단경로를 찾은 뒤 사용된 간선에 한해 역조사하면서 갯수를 누적하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1661
    {

        static void Main1661(string[] args)
        {

            int n, m;
            List<(int dst, int dis, int idx)>[] edge;

            Input();

            GetRet();

            void GetRet()
            {

                long INF = 1_000_000_000_000_000_000;
                int[] score = new int[m + 1];
                long[] dis = new long[n + 1];
                bool[] visit = new bool[n + 1];
                bool[] use = new bool[m + 1];
                PriorityQueue<(int dst, int idx), long> pq = new(m);

                for (int i = 1; i <= n; i++)
                {

                    Dijkstra(i);

                    DFS(i);
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                // 최댓값과 최댓값 갯수 찾기
                int max = 0;
                int ret1 = 1;
                for (int i = 1; i <= m; i++)
                {

                    if (max < score[i])
                    {

                        max = score[i];
                        ret1 = 1;
                    }
                    else if (max == score[i]) ret1++;
                }

                // 출력
                sw.Write($"{ret1}\n");
                for (int i = 1; i <= m; i++)
                {

                    if (max != score[i]) continue;
                    sw.Write($"{i} ");
                }

                // 다익스트라로 최단 경로 찾기
                // 사용된 경로 기록
                void Dijkstra(int _s)
                {

                    Array.Fill(dis, INF);
                    Array.Fill(visit, false);

                    dis[_s] = 0;

                    pq.Enqueue((_s, 0), 0);

                    while (pq.Count > 0)
                    {

                        (int c, int idx) = pq.Dequeue();

                        if (visit[c]) continue;
                        visit[c] = true;
                        use[idx] = true;

                        for (int i = 0; i < edge[c].Count; i++)
                        {

                            int next = edge[c][i].dst;
                            if (visit[next]) continue;
                            long nDis = dis[c] + edge[c][i].dis;

                            if (dis[next] < nDis) continue;
                            dis[next] = nDis;
                            pq.Enqueue((next, edge[c][i].idx), nDis);
                        }
                    }
                }

                // 사용된 경로에 점수 계산
                int DFS(int _s)
                {

                    int ret = 1;

                    for (int i = 0; i < edge[_s].Count; i++)
                    {

                        (int next, int _, int idx) = edge[_s][i];
                        if (!use[idx]) continue;
                        use[idx] = false;
                        int chk = DFS(next);
                        score[idx] += chk;
                        ret += chk;
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                edge = new List<(int dst, int dis, int idx)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i <= m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int dis = ReadInt();

                    edge[f].Add((t, dis, i));
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
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
    }

#if other
// #include <bits/stdc++.h>
// #define sz size()
// #define bk back()
// #define fi first
// #define se second

using namespace std;
typedef long long ll;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;

struct Node {
    ll x, d;
    bool operator<(const Node &o) const { return d > o.d; }
};

ll dfs(int cur, int prv, vector<vector<pair<pll, int>>> &graph, vector<ll> &score, vector<ll> &dist, vector<ll> &sub) {
    sub[cur] = 1;

    for (auto nxt : graph[cur]) {
        if (nxt.fi.fi == prv || dist[cur] + nxt.fi.se != dist[nxt.fi.fi])
            continue;

        ll k = dfs(nxt.fi.fi, cur, graph, score, dist, sub);
        score[nxt.se] += k;
        sub[cur] += k;
    }

    return sub[cur];
}

int main() {
    ios::sync_with_stdio(0);
    cin.tie(0);
    cout.tie(0);

    int n, m;
    cin >> n >> m;

    vector<vector<pair<pll, int>>> graph(n + 1);

    for (int i = 1; i <= m; i++) {
        ll u, v, w;
        cin >> u >> v >> w;

        graph[u].push_back({{v, w}, i});
    }

    vector<ll> score(m + 1);
    vector<ll> dist(n + 1);
    vector<ll> sub(n + 1);

    for (int i = 1; i <= n; i++) {
        fill(dist.begin(), dist.end(), LLONG_MAX);
        dist[i] = 0;

        priority_queue<Node> pq;
        pq.push({i, 0});

        while (pq.sz) {
            Node cur = pq.top();
            pq.pop();

            if (dist[cur.x] != cur.d)
                continue;

            for (auto &nxt : graph[cur.x]) {
                if (cur.d + nxt.fi.se < dist[nxt.fi.fi]) {
                    dist[nxt.fi.fi] = cur.d + nxt.fi.se;
                    pq.push({nxt.fi.fi, dist[nxt.fi.fi]});
                }
            }
        }

        fill(sub.begin(), sub.end(), 0);

        dfs(i, -1, graph, score, dist, sub);
    }

    ll mx = *max_element(score.begin(), score.end());
    vector<int> ans;

    for (int i = 1; i <= m; i++)
        if (score[i] == mx)
            ans.push_back(i);

    cout << ans.sz << '\n';
    for (int x : ans)
        cout << x << ' ';
}
#endif
}
