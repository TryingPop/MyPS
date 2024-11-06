using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 22
이름 : 배성훈
내용 : 피보나치 비스무리한 수열
    문제번호 : 14495번

    dp 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0599
    {

        static void Main599(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            ulong[] dp = new ulong[Math.Max(n + 1, 4)];

            dp[1] = 1;
            dp[2] = 1;
            dp[3] = 1;

            for (int i = 4; i <= n; i++)
            {

                dp[i] = dp[i - 1] + dp[i - 3];
            }

            Console.WriteLine(dp[n]);
        }
    }

#if other
using System;

class Program
{
    public static void Main()
    {
        var N = int.Parse(Console.ReadLine());
        var dp = new long[120];

        dp[1] = 1;
        dp[2] = 1;
        dp[3] = 1;

        for (int i = 4; i <= N; i++)
        {
            dp[i] = dp[i - 1] + dp[i - 3];
        }

        Console.WriteLine(dp[N]);
    }
}
#endif
}
