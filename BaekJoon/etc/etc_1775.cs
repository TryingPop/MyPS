using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 18
이름 : 배성훈
내용 : 스위치
    문제번호 : 30460번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1775
    {

        static void Main1775(string[] args)
        {

            long NOT_VISIT = -777_777_777_777;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();

            long[][] dp = new long[2][];
            for (int i = 0; i < 2; i++)
            {

                dp[i] = new long[3];
                Array.Fill(dp[i], NOT_VISIT);
            }

            dp[0][0] = ReadInt();
            dp[0][2] = dp[0][0] * 2;

            for (int i = 1; i < n; i++)
            {

                int cur = ReadInt();

                dp[1][0] = dp[0][0] + cur;
                dp[1][2] = dp[0][0] + cur * 2;

                for (int j = 2; j > 0; j--) 
                {

                    if (dp[0][j] == NOT_VISIT) continue;
                    dp[1][j - 1] = Math.Max(dp[0][j] + cur * 2, dp[1][j - 1]);
                }

                for (int j = 0; j < 3; j++)
                {

                    dp[0][j] = dp[1][j];
                    dp[1][j] = NOT_VISIT;
                }
            }

            long ret = NOT_VISIT;
            for (int i = 0; i < 3; i++)
            {

                ret = Math.Max(dp[0][i], ret);
            }

            Console.Write(ret);

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }
}
