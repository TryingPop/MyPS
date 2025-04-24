using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 25
이름 : 배성훈
내용 : 2의 멱수의 합
    문제번호 : 2410번

    배낭 dp 문제다.
    1으로만 채울 수 있는 경우는 명백히 1이다.
    이후 2를 추가해서 채울 수 있는 경우를 찾는다.
    다음으로 4, 8, ..., 2^19까지하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1575
    {

        static void Main1575(string[] args)
        {

            int MOD = 1_000_000_000;
            int n = int.Parse(Console.ReadLine());
            int[] dp = new int[n + 1];

            Array.Fill(dp, 1);

            for (int j = 1; j <= 19; j++)
            {

                int add = 1 << j;

                for (int i = 0; i <= n - add; i++)
                {

                    dp[i + add] = (dp[i + add] + dp[i]) % MOD;
                }
            }

            Console.Write(dp[n]);
        }
    }
}
