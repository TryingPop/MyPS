using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 26
이름 : 배성훈
내용 : 리그 오브 레전설 (Small)
    문제번호 : 17271번

    dp 문제다.
    끝에 A 또는 B로 이어붙인다고 생각하면 모두 다른 경우이고!
    dp[i] = dp[i - 1] + dp[i - m]의 점화식을 얻을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1839
    {

        static void Main1839(string[] args)
        {

            int MOD = 1_000_000_007;
            int n, m;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dp = new int[n + 1];
                dp[0] = 1;
                for (int i = 1; i <= n; i++)
                {

                    dp[i] = dp[i - 1];
                    if (i >= m) dp[i] = (dp[i] + dp[i - m]) % MOD;
                }

                Console.Write(dp[n]);
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
