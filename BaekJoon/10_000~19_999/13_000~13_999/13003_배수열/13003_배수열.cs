using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 2
이름 : 배성훈
내용 : 배수열
    문제번호 : 13003번

    수학, 정수론, dp문제다
    점화식을 찾아 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1091
    {

        static void Main1091(string[] args)
        {

            int MOD = 1_000_000_007;
            int n, l;

            int[] cur, next;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Fill(cur, 1);

                for (int len = 1; len < l; len++)
                {

                    for (int i = 1; i <= n; i++)
                    {

                        for (int j = i; j <= n; j += i)
                        {

                            next[i] = (next[i] + cur[j]) % MOD;
                        }
                    }

                    for (int i = 1; i <= n; i++)
                    {

                        cur[i] = next[i];
                        next[i] = 0;
                    }
                }

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    ret = (ret + cur[i]) % MOD;
                }

                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                l = int.Parse(temp[1]);

                cur = new int[n + 1];
                next = new int[n + 1];
            }
        }
    }

#if other
// #pragma GCC optimize("O3")
// #pragma GCC target("arch=skylake")
// #include <cstdio>

using namespace std;

const int MOD = 1000000007;

int dp[100];
int n;
int l;
int dpcnt;
int rtn;

inline int get_dpi(int v) {
    if (v > rtn) return n / v - 1;
    return dpcnt - v;
}

int main() {
    scanf("%d%d", &n, &l);
    rtn = n;
    for (;;) {
        int y = (n / rtn + rtn) / 2;
        if (y >= rtn) break;
        rtn = y;
    }
    dpcnt = rtn * 2 - (n < rtn * rtn + rtn);

    for (int i = 0; i < dpcnt; ++i) dp[i] = 1;

    for (int il = 0; il < l; ++il)
    for (int ub = 0; ub < n; ) {
        int lb = ub;
        int v = n / (lb + 1);
        ub = n / v;

        int n2 = v;
        int s = 0;
        for (int ub2 = 0; ub2 < v; ) {
            int lb2 = ub2;
            int v2 = n2 / (lb2 + 1);
            ub2 = n2 / v2;

            s += 1LL * dp[get_dpi(v2)] * (ub2 - lb2) % MOD;
            s %= MOD;
        }
        dp[get_dpi(n2)] = s;
    }

    int ans = dp[0];
    printf("%d\n", ans);
}
#endif
}
