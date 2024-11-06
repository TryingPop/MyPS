using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 21
이름 : 배성훈
내용 : 하와와 대학생쨩 하와이로 가는 거시와요~
    문제번호 : 16456번
*/
namespace BaekJoon.etc
{
    internal class etc_0898
    {

        static void Main898(string[] args)
        {

            int MOD = 1_000_000_009;
            int n;
            int[] dp;

            Solve();
            void Solve()
            {

                n = int.Parse(Console.ReadLine());

                dp = new int[Math.Max(n, 4) + 1];

                SetDp();
                Console.Write(dp[n]);
            }

            void SetDp()
            {

                dp[0] = 1;
                dp[1] = 1;
                dp[2] = 1;
                dp[3] = 2;
                dp[4] = dp[3] + 1;
                if (n < 5) return;

                for (int i = 5; i <= n; i++)
                {

                    // 앞으로 한 칸 이동, 2 -1 2 이동한 경우
                    // 이렇게 두 가지 경로로 나뉜다
                    dp[i] = (dp[i - 1] + dp[i - 3]) % MOD;
                }
            }
        }
    }

#if other
using System;

public class Program
{
    const int Mod = 1000000009;
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] dp = new int[n + 1];
        dp[0] = dp[1] = 1;
        for (int i = 2; i <= n; i++)
        {
            dp[i] += dp[i - 1];
            if (i >= 3)
                dp[i] += dp[i - 3];
            dp[i] %= Mod;
        }
        Console.Write(dp[n]);
    }
}
#endif
}
