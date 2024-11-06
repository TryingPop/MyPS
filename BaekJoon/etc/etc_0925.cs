using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 30 
이름 : 배성훈
내용 : Bessie's Birthday Buffet
    문제번호 : 10763번

    dp, bfs 문제다
    풀의 가치가 높은 순으로 정렬한다
    그리고 가치가 높은거 부터 벨만 포드형식으로 길을 이어 
    에너지가 증가할 수 있는지 확인한다
*/

namespace BaekJoon.etc
{
    internal class etc_0925
    {

        static void Main925(string[] args)
        {

            StreamReader sr;
            int[][] edge;
            int[] energy;
            int[] p;

            int[] dp;
            int n, e;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dp = new int[n];
                p = new int[n];
                for (int i = 0; i < n; i++)
                {

                    p[i] = i;
                }

                // 풀의 가치가 높은 순으로 정렬
                Array.Sort(p, Comparer<int>.Create((x, y) => energy[x].CompareTo(energy[y])));

                int ret = 0;
                Queue<int> q = new(n);
                int[] dis = new int[n];

                for (int i = n - 1; i >= 0; i--)
                {

                    int u = p[i];
                    Array.Fill(dis, -1);
                    q.Enqueue(u);
                    dis[u] = 0;

                    // 거리 계산
                    while(q.Count > 0)
                    {

                        int cur = q.Dequeue();
                        for (int j = 0; j < edge[cur].Length; j++)
                        {

                            int next = edge[cur][j];
                            if (dis[next] != -1) continue;
                            dis[next] = dis[cur] + 1;
                            q.Enqueue(next);
                        }
                    }

                    // 벨만 포드 형식으로 다음지점을 탐색한다
                    // 다음 지점으로 이엇을 때
                    int chk = energy[u];
                    for (int j = 0; j < n; j++)
                    {

                        if (dis[j] == -1) continue;
                        chk = Math.Max(chk, energy[u] + dp[j] - e * dis[j]);
                    }

                    // 현재지점을 가장 큰 곳으로 이엇을 때 최대 값을 산정
                    dp[u] = chk;

                    ret = Math.Max(chk, ret);
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                e = ReadInt();

                edge = new int[n][];
                energy = new int[n];
                for (int i = 0; i < n; i++)
                {

                    energy[i] = ReadInt();
                    edge[i] = new int[ReadInt()];
                    for (int j = 0; j < edge[i].Length; j++)
                    {

                        edge[i][j] = ReadInt() - 1;
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
// #include <iostream>
// #include <vector>
// #include <algorithm>
// #include <cstdio>
// #include <queue>
// #include <cstring>

using namespace std;

// #define MAXN 1010

int Q[MAXN];
int DP[MAXN];
int D[MAXN];
vector<int> E[MAXN];

int main() {
  freopen("buffet.in", "r", stdin);
  freopen("buffet.out", "w", stdout);

  int N, ECST;
  cin >> N >> ECST;
  for (int i = 0; i < N; i++) {
    int D;
    cin >> Q[i] >> D;
    for (int j = 0; j < D; j++) {
      int v;
      cin >> v;
      E[i].push_back(v - 1);
    }
  }

  vector<int> PI;
  for (int i = 0; i < N; i++) {
    PI.push_back(i);
  }
  sort(PI.begin(), PI.end(), [&](int x, int y) {
    return Q[x] < Q[y];
  });

  int result = 0;
  for (int i = N - 1; i >= 0; i--) {
    int u = PI[i];

    queue<int> q;
    memset(D, -1, sizeof(D));
    q.push(u);
    D[u] = 0;
    while (!q.empty()) {
      int v = q.front();
      q.pop();
      for (int i = 0; i < E[v].size(); i++) {
        int nv = E[v][i];
        if (D[nv] == -1) {
          D[nv] = D[v] + 1;
          q.push(nv);
        }
      }
    }

    int res = Q[u];
    for (int j = 0; j < N; j++) {
      if (D[j] != -1) {
        res = max(res, Q[u] + DP[j] - ECST * D[j]);
      }
    }
    DP[u] = res;
    result = max(result, res);
  }

  cout << result << endl;
  return 0;
}
#elif other2
// #include <cstdio>
// #include <cstring>
// #include <algorithm>
// #include <vector>
// #include <queue>
using namespace std;

int n, e, q[1111], dp[1111], dst[1111], idx[1111];
vector<int> gph[1111];

int main() {
    scanf("%d %d", &n, &e);
    for (int i = 1; i <= n; i++) {
        int d; scanf("%d %d", &q[i], &d);
        while (d--) {
            int j; scanf("%d", &j);
            gph[i].push_back(j);
        }
        idx[i] = i;
    }

    sort(idx+1, idx+1+n, [&](int i, int j){ return q[i]>q[j];});
    int dap = -1;
    for (int i = 1; i <= n; i++) {
        int now = idx[i];
        queue<int> que;
        memset(dst, 0x3f, sizeof(dst));
        dst[now] = 0; que.push(now);
        while (!que.empty()) {
            int now = que.front(); que.pop();
            for (int nxt : gph[now]) {
                if (dst[nxt] > dst[now]+1) {
                    dst[nxt] = dst[now]+1;
                    que.push(nxt);
                }
            }
        }

        int res = q[now];
        for (int j = 1; j <= n; j++) {
            if (dst[j] == 0x3f3f3f3f) continue;
            res = max(res, q[now]+dp[j]-dst[j]*e);
        }
        dp[now] = res;
        dap = max(dap, res);
    }

    printf("%d\n", dap);

    return 0;
}
#endif
}
