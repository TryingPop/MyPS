using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 10
이름 : 배성훈
내용 : Decelerating Jump
    문제번호 : 23378번

    dp 문제다.
    점화식을 생각없이 세우니 시간이 O(N^3)이 나왔고;
    시간초과로 몇번 틀렸다.

    dp[i][j] = val를 i번째 까지 택하고 이전에 j거리만큼 뛰었을 때
    최대값을 val로 담기게 dp를 설정한다.

    그러면 dp[i][j] = arr[i] + max(dp[i - k][k]);
    k >= j 의 점화식을 얻는다.

    max에 대한 처리가 필요하다.
    진행과정으로 점화식을 구현하니,
    max 처리가 필요없었고 dp도 1차원 배열로 해결되었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1266
    {

        static void Main1266(string[] args)
        {

            long INF = -(long)1e15;
            int n;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                // long[][] dp = new long[n + 1][];
                long[] dp = new long[n + 1];

                for (int i = 0; i <= n; i++)
                {

                    // dp[i] = new long[n + 1];
                    // Array.Fill(dp[i], INF);
                    dp[i] = INF;
                }

                // Array.Fill(dp[n], arr[n]);
                // Array.Fill(dp[1], arr[1]);
                dp[1] = arr[1];
                for (int jump = n - 1; jump >= 1; jump--)
                {

                    dp[1 + jump] = Math.Max(dp[1 + jump], dp[1] + arr[jump + 1]);

                    for (int idx = jump + 1; idx <= n; idx++)
                    {

                        int next = jump + idx;
                        if (n < next) break;
                        dp[next] = Math.Max(dp[next], dp[idx] + arr[next]);
                    }
                }

                Console.Write(dp[n]);

                // Console.Write(DFS(1, n));

                long DFS(int _idx, int _prev)
                {

                    // ref long ret = ref dp[_idx][_prev];
                    long ret = 0;
                    if (ret != INF) return ret;

                    for (int i = 1; i <= _prev; i++)
                    {

                        int next = _idx + i;
                        if (next > n) break;
                        ret = Math.Max(ret, DFS(next, i));
                    }

                    ret += arr[_idx];
                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
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
                        if (c == ' ' || c == '\n') return true;

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
// #include <cstring>
// #include <algorithm>
using namespace std;

// #define N 3001
typedef long long int ll;
int n;
ll dp[N][N], arr[N];
ll f(int x, int y){
    if(dp[x][y] != -1085102592571150096LL) return dp[x][y];
    if(x == n) return dp[x][y] = arr[x];
    ll answer = -(1LL << 50);
    if(x + y <= n) answer = max(answer, f(x + y, y) + arr[x]);
    if(y - 1 >= 1) answer = max(answer, f(x, y - 1));
    return dp[x][y] = answer;
}
int main(void){
    cin.tie(0);
    ios::sync_with_stdio(0);
    
    cin >> n;
    for(int i = 1; i <= n; i++) cin >> arr[i];
    memset(dp, 0xf0, sizeof(dp));
    cout << f(1, n - 1);
    return 0;
}

#endif
}
