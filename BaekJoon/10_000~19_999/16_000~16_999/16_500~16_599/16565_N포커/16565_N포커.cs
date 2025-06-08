using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 11
이름 : 배성훈
내용 : N포커
    문제번호 : 16565번

    수학, dp, 포함 배제의 원리 문제다.
    밴다이어그램으로 그래프를 그려보면 홀수 일 때는 홀수번 포함되고
    짝수일 때는 짝수번 포함된다. 그래서 포함 배제의 원리로 홀수는 더하고 짝수는 빼준다.
*/

namespace BaekJoon.etc
{
    internal class etc_1176
    {

        static void Main1176(string[] args)
        {

            int MOD = 10_007;

            int n;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                n = int.Parse(Console.ReadLine());

                dp = new int[53][];
                for (int i = 0; i <= 52; i++)
                {

                    dp[i] = new int[i + 1];
                    dp[i][i] = 1;
                    dp[i][0] = 1;
                }

                for (int i = 2; i <= 52; i++)
                {

                    for (int j = 1; j < i; j++)
                    {

                        dp[i][j] = (dp[i - 1][j - 1] + dp[i - 1][j]) % MOD;
                    }
                }
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = 1; 4 * i <= n; i++)
                {

                    if ((i & 1) == 1) ret = (ret + dp[13][i] * dp[52 - 4 * i][n - 4 * i]) % MOD;
                    else ret = (ret - dp[13][i] * dp[52 - 4 * i][n - 4 * i]) % MOD;
                }

                if (ret < 0) ret += MOD;
                Console.Write(ret);
            }
        }
    }
}
