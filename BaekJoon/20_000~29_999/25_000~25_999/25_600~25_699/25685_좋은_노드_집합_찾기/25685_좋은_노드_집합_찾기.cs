using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 12
이름 : 배성훈
내용 : 좋은 노드 집합 찾기
    문제번호 : 25685번

    dp, 트리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1697
    {

        static void Main1697(string[] args)
        {

            long INF = -1_000_000_000_000_000_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();
            int n, root;
            int[] arr;
            long[][] dp;
            List<int>[] edge;

            Init();

            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                DFS(root);

                sw.Write(Math.Max(dp[root][0], dp[root][1]));
                sw.Write('\n');

                void DFS(int _idx)
                {

                    ref long ret = ref dp[_idx][0];
                    if (ret != INF) return;

                    ret = 0;
                    dp[_idx][1] = arr[_idx];
                    if (edge[_idx].Count == 0) return;

                    long sub = INF;
                    bool flag = true;
                    for (int i = 0; i < edge[_idx].Count; i++)
                    {

                        int next = edge[_idx][i];
                        DFS(next);

                        dp[_idx][1] += dp[next][0];
                        
                        if (dp[next][0] > dp[next][1])
                        {

                            ret += dp[next][0];
                            sub = Math.Max(sub, dp[next][1] - dp[next][0]);
                        }
                        else
                        {

                            flag = false;
                            ret += dp[next][1];
                        }
                    }

                    if (flag)
                        ret += sub;
                }
            }

            void Input()
            {
                
                n = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    edge[i].Clear();
                    dp[i][0] = INF;
                    dp[i][1] = INF;
                }

                for (int i = 1; i <= n; i++)
                {

                    int p = ReadInt();
                    if (p == 0) root = i;
                    else edge[p].Add(i);
                }
            }

            void Init()
            {

                root = 0;
                int MAX_N = 100_000;
                arr = new int[MAX_N + 1];
                edge = new List<int>[MAX_N + 1];
                dp = new long[MAX_N + 1][];
                for (int i = 1; i <= MAX_N; i++)
                {

                    edge[i] = new();
                    dp[i] = new long[2]; 
                }
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
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <vector>

using namespace std;

typedef long long ll;
typedef pair<ll, ll> pll;
const ll inf = 123456789123456789;

int n;
ll a[100005];
vector<ll> g[100005];

ll dp[100005][2];
void solve(int cur) {
    for(int nxt : g[cur]) {
        solve(nxt);
    }

    ll zsum = 0;
    for(int nxt : g[cur])
        zsum += dp[nxt][0];
    dp[cur][1] = zsum + a[cur];
    
    ll psum = 0, ocnt = 0;
    for(int nxt : g[cur]) {
        if(dp[nxt][1] >= dp[nxt][0]) {
            psum += dp[nxt][1];
            ocnt += 1;
        } else {
            psum += dp[nxt][0];
        }
    }

    if(ocnt == 0) {
        ll diff = -inf;
        for(int nxt : g[cur]) {
            if(dp[nxt][1] < dp[nxt][0]) {
                diff = max(diff, dp[nxt][1]-dp[nxt][0]);
            }
        }
        psum += diff;
    }
    if(g[cur].size() == 0)
        dp[cur][0] = 0;
    else
        dp[cur][0] = psum;
    return;
}

void solve() {
    cin >> n;
    for(int i = 1; i <= n; ++i)
        cin >> a[i];
    
    int rt = -1;
    for(int i = 1; i <= n; ++i) {
        ll x;
        cin >> x;
        if(x == 0)
            rt = i;
        else
            g[x].push_back(i);
    }
    
    solve(rt);
    for(int i = 1; i <= n; ++i)
        g[i].clear();

    cout << max(dp[rt][0], dp[rt][1]) << "\n";

    return;
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);

    int tc;
    cin >> tc;
    while(tc--) {
        solve();
    }

    return 0;
}
#endif
}
