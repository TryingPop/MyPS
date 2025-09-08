using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

/*
날짜 : 2025. 9. 8
이름 : 배성훈
내용 : 조합
    문제번호 : 2407번

    조합 문제다.
    100C50까지들어오므로 Numerics의 BigInteger를 이용했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1876
    {

        static void Main1876(string[] args)
        {

            // 2407 - 조합
            int n, m;

            Input();

            GetRet();

            void GetRet()
            {

                BigInteger[][] dp = new BigInteger[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new BigInteger[i + 1];
                }

                dp[0][0] = 1;
                for (int i = 1; i <= n; i++)
                {

                    dp[i][0] = 1;
                    dp[i][i] = 1;
                    for (int j = 1; j < i; j++)
                    {

                        dp[i][j] = dp[i - 1][j - 1] + dp[i - 1][j];
                    }
                }

                Console.Write(dp[n][m]);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
            }
        }
    }
}
