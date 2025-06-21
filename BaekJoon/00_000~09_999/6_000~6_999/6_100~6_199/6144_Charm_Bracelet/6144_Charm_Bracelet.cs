using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 21
이름 : 배성훈
내용 : Charm Bracelet
    문제번호 : 6144번

    dp, 배낭 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1719
    {

        static void Main1719(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            int m = ReadInt();

            int[] dp = new int[m + 1];
            Array.Fill(dp, -1);
            int e = 0;
            dp[e] = 0;

            for (int i = 0; i < n; i++)
            {

                int w = ReadInt();
                int d = ReadInt();

                for (int cur = e; cur >= 0; cur--)
                {

                    if (dp[cur] == -1) continue;
                    int next = cur + w;
                    if (next > m) continue;

                    if (dp[next] < dp[cur] + d) dp[next] = dp[cur] + d;
                }

                e = Math.Min(e + w, m);
            }

            int ret = 0;
            for (int i = 0; i <= m; i++)
            {

                ret = Math.Max(ret, dp[i]);
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
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
