using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 13
이름 : 배성훈
내용 : 준오는 최종인재야!!
    문제번호 : 14657번

    트리 문제다
    트리의 지름이 최대 문제 갯수가 되고,
    지름 중 최단 경로를 찾아야 한다

    지름을 찾는데 최단 경로로 깊이가 가장 깊은걸 찾았다
    그리고 해당 노드에서 가장 긴 깊이에 가장 짧은 경로를 찾아 제출하니 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0965
    {

        static void Main965(string[] args)
        {

            StreamReader sr;

            int n, t;
            List<(int dst, int dis)>[] edge;
            bool[] visit;
            int[] depth, dis;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dis = new int[n + 1];
                depth = new int[n + 1];
                visit = new bool[n + 1];

                DFS(1);

                int mDis = 0;
                int mDep = 0;
                int s = 1;

                for (int i = 1; i <= n; i++)
                {

                    if (mDep < depth[i])
                    {

                        s = i;
                        mDep = depth[i];
                        mDis = dis[i];
                    }
                    else if (mDep == depth[i] && dis[i] < mDis)
                    {

                        s = i;
                        mDis = dis[i];
                    }
                }

                dis[s] = 0;
                depth[s] = 0;
                Array.Fill(visit, false);

                DFS(s);

                for (int i = 1; i <= n; i++)
                {

                    if (mDep < depth[i])
                    {

                        s = i;
                        mDep = depth[i];
                        mDis = dis[i];
                    }
                    else if (mDep == depth[i] && dis[i] < mDis)
                    {

                        s = i;
                        mDis = dis[i];
                    }
                }

                int ret = mDis / t;
                if (mDis % t > 0) ret++;

                Console.Write(ret);
            }

            void DFS(int _n)
            {

                if (visit[_n]) return;
                visit[_n] = true;

                for (int i = 0; i < edge[_n].Count; i++)
                {

                    int next = edge[_n][i].dst;
                    if (visit[next]) continue;

                    depth[next] = depth[_n] + 1;
                    dis[next] = dis[_n] + edge[_n][i].dis;
                    DFS(next);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                t = ReadInt();

                edge = new List<(int dst, int dis)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    int dis = ReadInt();

                    edge[f].Add((b, dis));
                    edge[b].Add((f, dis));
                }

                sr.Close();
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
// #include <bits/stdc++.h>
// #define ull unsigned long long
// #define endl '\n'
using namespace std;

const int dy[] = {-1, 1, 0, 0, -1, -1, 1, 1};
const int dx[] = {0, 0, -1, 1, -1, 1, -1, 1};

const int INF = 0x7f7f7f7f;
const int MAX = 987654321;

const long MOD = 1'000'000'007;

struct Coor {
    int y, x;
    Coor() {}
    Coor(int y, int x): y(y), x(x) {}
    bool operator==(Coor& a) { return y == a.y && x == a.x; }
    bool operator!=(Coor& a) { return !(*this == a); }
};

vector<vector<pair<int, int>>> adj;
pair<int, int> d;
bool visited[50001];
int maxDepth;
int minDist[50001];

void dfs(int node, int depth, int dist) {
    visited[node] = true;
    if (depth > d.second) {
        d.first = node;
        d.second = depth;
    }
    else if (depth == d.second && minDist[depth] > dist) d.first = node;
    minDist[depth] = min(minDist[depth], dist);
    for (auto& p : adj[node]) {
        if (visited[p.first]) continue;
        dfs(p.first, depth + 1, dist + p.second);
    }
}

void findDist(int node, int depth, int dist) {
    visited[node] = true;
    maxDepth = max(maxDepth, depth);
    minDist[depth] = min(minDist[depth], dist);
    for (auto& p : adj[node]) {
        if (visited[p.first]) continue;
        findDist(p.first, depth + 1, dist + p.second);
    }
}

int main() {
    cin.tie(0)->sync_with_stdio(0);

    if (getenv("REPL") != NULL) freopen("input.txt", "r", stdin);

    int n, t;
    cin >> n >> t;
    adj.resize(n + 1);
    
    for (int i = 0; i < n - 1; i++) {
        int a, b, c;
        cin >> a >> b >> c;
        adj[a].emplace_back(b, c);
        adj[b].emplace_back(a, c);
    }

    memset(minDist, INF, sizeof(minDist));
    dfs(1, 1, 0);
    memset(visited, 0, sizeof(visited));
    memset(minDist, INF, sizeof(minDist));
    findDist(d.first, 1, 0);

    cout << minDist[maxDepth] / t + (minDist[maxDepth] % t != 0);

    return 0;
}
#elif other2
// #include <bits/stdc++.h>

using namespace std;

int N, T;
vector<pair<int, int>> adj[50005];
int sum_ = 0;
int max_ = 0;
int node;

void sol(int cur, int par = 0, int dep = 0, int sum = 0) {
  if (dep > max_) {
    node = cur;
    max_ = dep;
    sum_ = sum;
  } else if (dep == max_ && sum < sum_) {
    node = cur;
    sum_ = sum;
  }

  for (auto [nxtdst, nxt]: adj[cur]) {
    if (nxt == par) continue;
    sol(nxt, cur, dep + 1, sum + nxtdst);
  }
}

int main(void) {
  ios::sync_with_stdio(0);
  cin.tie(0);

  cin >> N >> T;
  for (int i = 0; i < N - 1; i++) {
    int a, b, c;
    cin >> a >> b >> c;
    adj[a].push_back({ c, b });
    adj[b].push_back({ c, a });
  }

  sol(1);
  sum_ = 0;
  max_ = 0;
  sol(node);

  cout << (sum_ % T == 0 ? sum_ / T : sum_ / T + 1);

  return 0;
}

#endif
}
