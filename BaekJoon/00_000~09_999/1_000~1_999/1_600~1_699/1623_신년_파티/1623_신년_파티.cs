using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 18
이름 : 배성훈
내용 : 신년 파티
    문제번호 : 1623번

    dp, 트리 문제다.
    dp[i][j] = val를 i = 0은 j를 미포함한 경우를 뜻한다.
    i = 1은 j를 포함한 경루르 뜻한다.
    val는 해당 경우의 최대 점수이다.
    그리고 점수는 최대 -20억을 넘지 못하므로 방문 안한걸 -20억 1200만으로 표현했다.
    
    그리고 탐색은 DFS로 하는데
    현재 노드를 포함하는 경우
    자식노드는 포함못한다. 그런데 자식을 포함하지 않는 최댓값이 음수인 경우
    아에 포함안하는게 좋으므로 포함하지 않는다.
    
    반면 현재 노드를 포함하지 않는 경우
    자식을 포함한 최댓값, 미포함한 최댓값 중에서 택하면 된다.

    각 경우의 이렇게 최대 점수를 찾는다.
    이후 경로 역추적은 해당 조건으로 탐색하는데,
    위 조건대로 다시 재진행하며 리스트에 넣었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1414
    {

        static void Main1414(string[] args)
        {

            int NOT_VISIT = -2_012_000_000;
            int n;
            int[] arr;
            List<int>[] edge;
            List<int> ret1, ret2;
            int[][] dp;

            Input();

            SetDp();

            GetRet();

            void GetRet()
            {

                ret1 = new(n);
                ret2 = new(n);

                DpDFS(1);

                TraceDFS(1, true, ret1);
                TraceDFS(1, false, ret2);

                ret1.Sort();
                ret2.Sort();

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{dp[1][1]} {dp[0][1]}\n");
                for (int i = 0; i < ret1.Count; i++)
                {

                    sw.Write($"{ret1[i]} ");
                }

                sw.Write("-1\n");

                for (int i = 0; i < ret2.Count; i++)
                {

                    sw.Write($"{ret2[i]} ");
                }

                sw.Write("-1\n");

                void TraceDFS(int _cur, bool _use, List<int> _ret)
                {

                    if (_use) _ret.Add(_cur);

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (_use)
                        {

                            if (dp[0][next] > 0) TraceDFS(next, false, _ret);
                        }
                        else
                        {

                            if (dp[1][next] > dp[0][next])
                            {

                                if (dp[1][next] > 0) TraceDFS(next, true, _ret);
                            }
                            else
                            {

                                if (dp[0][next] > 0) TraceDFS(next, false, _ret);
                            }
                        }
                    }
                }

                void DpDFS(int _cur)
                {

                    if (dp[0][_cur] != NOT_VISIT) return;

                    if (edge[_cur].Count == 0)
                    {

                        dp[0][_cur] = 0;
                        dp[1][_cur] = arr[_cur];
                        return;
                    }

                    dp[0][_cur] = 0;
                    dp[1][_cur] = arr[_cur];

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        DpDFS(next);

                        int chk = dp[0][next];
                        if (chk > 0)
                            dp[1][_cur] += chk;

                        chk = Math.Max(dp[1][next], dp[0][next]);
                        if (chk > 0)
                            dp[0][_cur] += chk;
                    }
                }
            }

            void SetDp()
            {

                dp = new int[2][];
                for (int i = 0;  i < 2; i++)
                {

                    dp[i] = new int[n + 1];
                    Array.Fill(dp[i], NOT_VISIT);
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n + 1];
                edge = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    edge[i] = new();
                }

                for (int t = 2; t <= n; t++)
                {

                    int f = ReadInt();
                    edge[f].Add(t);
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
                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
    }

#if other
// #include <iostream>
// #include <vector>
// #include <cstring>

using namespace std;

int n, w[200001], dp1[200001], dp2[200001];
bool ans[200001];
vector<int> adj[200001];

void calc(int v) {
    for (int u : adj[v]) calc(u);

    int d1 = 0, d2 = 0;
    for (int u : adj[v]) d1 += max(0, dp2[u]);
    for (int u : adj[v]) d2 += max(0, max(dp1[u], dp2[u]));

    dp1[v] = w[v] + d1;
    dp2[v] = d2;
}

void find(int v, bool used) {
    ans[v] = used;

    if (used) {
        for (int u : adj[v]) 
            if (dp2[v] > 0)
                find(u, false);
    }
    else {
        for (int u : adj[v]) {
            if (max(dp1[u], dp2[u]) <= 0) continue;
            find(u, dp1[u] >= dp2[u]);
        }
    }
}

int main() {
    cin.tie(0);
    ios_base::sync_with_stdio(0);

    cin >> n;
    for (int i = 1; i <= n; i++) cin >> w[i];
    for (int i = 2, p; i <= n; i++) {
        cin >> p;
        adj[p].push_back(i);
    }

    calc(1);

    cout << dp1[1] << ' ' << dp2[1] << '\n';

    find(1, true);
    for (int i = 1; i <= n; i++) 
        if (ans[i])
            cout << i << ' ';
    cout << -1 << '\n';

    memset(ans, false, sizeof(ans));
    find(1, false);
    for (int i = 1; i <= n; i++) 
        if (ans[i])
            cout << i << ' ';
    cout << -1 << '\n';


    return 0;
}
#endif
}
