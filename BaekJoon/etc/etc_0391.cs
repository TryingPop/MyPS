using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : 1, 2, 3 더하기, 3
    문제번호 : 9095번, 15988번

    dp 문제다
    아이디어는 다음과 같다
    그리디하게 접근해서 앞에 1, 2, 3가 오는 경우로 보니
     n > 3에서 dp[n] = dp[n - 1] + dp[n - 2] + dp[n - 3]이 된다

    해당 아이디어로 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0391
    {

        static void Main391(string[] args)
        {

            long MOD = 1_000_000_009;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            long[] dp = new long[1_000_001];

            dp[1] = 1;
            dp[2] = 2;
            dp[3] = 1;
            dp[3] += dp[1];
            dp[3] += dp[2];

            for (int i = 4; i < dp.Length; i++)
            {

                dp[i] = dp[i - 1] + dp[i - 2] + dp[i - 3];
                dp[i] %= MOD;
            }

            int test = ReadInt();

            while(test-- > 0)
            {

                int cur = ReadInt();

                sw.WriteLine(dp[cur]);
            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
