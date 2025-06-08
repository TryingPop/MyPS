using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 2xn 타일링 2
    문제번호 : 11727번

    dp 문제다
    그리디하게 접근해서 앞에서부 이전 모양을 이어붙여 갔다
*/

namespace BaekJoon.etc
{
    internal class etc_0408
    {

        static void Main408(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int[] dp = new int[1_001];
            dp[1] = 1;
            dp[2] = 3;

            for (int i = 3; i <= 1_000; i++)
            {

                dp[i] = dp[i - 1];
                dp[i] += 2 * dp[i - 2];
                dp[i] %= 10_007;
            }

            Console.WriteLine(dp[n]);
        }
    }
}
