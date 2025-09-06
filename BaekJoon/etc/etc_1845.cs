using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 28 
이름 : 배성훈
내용 : Subset Sums
    문제번호 : 27134번

    dp, 배낭 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1845
    {

        static void Main1845(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int sum = (n + 1) * n >> 1;

            if ((sum & 1) == 1) Console.Write(0);
            else
            {

                long[] dp = new long[sum + 1];
                dp[0] = 1;
                int e = 0;
                for (int i = 1; i <= n; i++)
                {

                    for (int j = e; j >= 0; j--)
                    {

                        if (dp[j] == 0) continue;
                        dp[j + i] += dp[j];
                    }

                    e += i;
                }

                Console.Write(dp[sum >> 1] >> 1);
            }
        }
    }
}
