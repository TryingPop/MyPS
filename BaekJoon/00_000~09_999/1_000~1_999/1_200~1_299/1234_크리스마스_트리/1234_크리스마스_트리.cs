using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 2
이름 : 배성훈
내용 : 크리스마스 트리
    문제번호 : 1234번

    dp, 조합론 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1308
    {

        static void Main1308(string[] args)
        {

            int n, r, g, b;
            long[][][][] dp;

            Solve();
            void Solve()
            {

                Input();

                SetDp();

                GetRet();
            }

            void GetRet()
            {

                long[] fac = new long[n + 1];
                fac[0] = 1L;
                for (int i = 1; i <= n; i++)
                {

                    fac[i] = fac[i - 1] * i;
                }

                Console.Write(DFS(1, r, g, b));

                long DFS(int _dep, int _r, int _g, int _b)
                {

                    if (_r < 0 || _g < 0 || _b < 0) return 0L;
                    else if (_dep > n) return 1L;

                    ref long ret = ref dp[_dep][_r][_g][_b];
                    if (ret != -1L) return ret;

                    ret = 0L;

                    // 1 color
                    ret += DFS(_dep + 1, _r - _dep, _g, _b);
                    ret += DFS(_dep + 1, _r, _g - _dep, _b);
                    ret += DFS(_dep + 1, _r, _g, _b - _dep);

                    // 2 colors
                    if (_dep % 2 == 0)
                    {

                        int half = _dep / 2;
                        long comb = Comb(_dep, half, half, 0);
                        ret += DFS(_dep + 1, _r - half, _g - half, _b) * comb;
                        ret += DFS(_dep + 1, _r, _g - half, _b - half) * comb;
                        ret += DFS(_dep + 1, _r - half, _g, _b - half) * comb;
                    }

                    // 3 colors
                    if (_dep % 3 == 0)
                    {

                        int ot = _dep / 3;
                        long comb = Comb(_dep, ot, ot, ot);
                        ret += DFS(_dep + 1, _r - ot, _g - ot, _b - ot) * comb;
                    }

                    return ret;
                }

                long Comb(int _n, int _a, int _b, int _c)
                {

                    return fac[_n] / (fac[_a] * fac[_b] * fac[_c]);
                }
            }

            void SetDp()
            {

                dp = new long[n + 1][][][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new long[r + 1][][];
                    for (int j = 0; j <= r; j++)
                    {

                        dp[i][j] = new long[g + 1][];
                        for (int k = 0; k <= g; k++)
                        {

                            dp[i][j][k] = new long[b + 1];
                            Array.Fill(dp[i][j][k], -1L);
                        }
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                r = int.Parse(temp[1]);
                g = int.Parse(temp[2]);
                b = int.Parse(temp[3]);

                int INF = 55;
                r = r > INF ? INF : r;
                g = g > INF ? INF : g;
                b = b > INF ? INF : b;
            }
        }
    }
}
