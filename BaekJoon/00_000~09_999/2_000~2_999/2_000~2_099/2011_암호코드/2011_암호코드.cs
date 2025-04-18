using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 14
이름 : 배성훈
내용 : 암호코드
    문제번호 : 2011번

    dp 문제다.
    시작이 0이면 불가능하다.
    해당 부분을 캐치 못해 계속해서 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1334
    {

        static void Main1334(string[] args)
        {

            int MOD = 1_000_000;

            string str = Console.ReadLine();
            if (str[0] == '0')
            {

                Console.Write(0);
                return;
            }

            int[] dp = new int[str.Length + 2];
            dp[0] = dp[1] = 1;

            for (int i = 2; i <= str.Length; i++)
            {

                if (str[i - 1] != '0') dp[i] = dp[i - 1] % MOD;

                int chk = (str[i - 2] - '0') * 10 + str[i - 1] - '0';
                if (10 <= chk && chk <= 26) dp[i] = (dp[i] + dp[i - 2]) % MOD;
            }

            Console.Write(dp[str.Length]);
        }
    }
}
