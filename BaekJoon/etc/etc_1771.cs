using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 16
이름 : 배성훈
내용 : 너 재능 있어
    문제번호 : 31929번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1771
    {

        static void Main1771(string[] args)
        {

            int NOT_VISIT = -123_456_789;

            int n, m, k;
            int[] add, pop;

            Input();

            GetRet();

            void GetRet()
            {

                int[][] dp = new int[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new int[m + 1];
                    Array.Fill(dp[i], NOT_VISIT);
                }

                dp[0][0] = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < m; j++)
                    {

                        // 승리한 경우
                        dp[i + 1][j] = Math.Max(dp[i + 1][j], dp[i][j] + add[i]);

                        // 패배 경우
                        int r = dp[i][j] % k;
                        int chk = dp[i][j] - pop[j];
                        if (r == 0) r = pop[j];
                        else
                        {

                            if (r < 0) r += k;
                            r = Math.Min(r, pop[j]);
                        }

                        chk = dp[i][j] - r;
                        dp[i][j + 1] = Math.Max(dp[i][j + 1], chk);
                    }

                    dp[i + 1][m] = Math.Max(dp[i + 1][m], dp[i][m] + add[i]);
                }

                for (int j = 0; j < m; j++)
                {

                    // 패배 경우
                    int r = dp[n][j] % k;
                    int chk = dp[n][j] - pop[j];
                    if (r == 0) r = pop[j];
                    else
                    {

                        if (r < 0) r += k;
                        r = Math.Min(r, pop[j]);
                    }

                    chk = dp[n][j] - r;
                    dp[n][j + 1] = Math.Max(dp[n][j + 1], chk);
                }

                Console.Write(dp[n][m]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                add = new int[n];
                for (int i = 0; i < n; i++)
                {

                    add[i] = ReadInt();
                }

                m = ReadInt();
                pop = new int[m];
                for (int i = 0; i < m; i++)
                {

                    pop[i] = ReadInt();
                }

                k = ReadInt();

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;

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
}
