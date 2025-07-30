using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 20
이름 : 배성훈
내용 : 만두 가게 사장 박승원
    문제번호 : 14855번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1780
    {

        static void Main1780(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();

            (int c, int d) special = (ReadInt(), ReadInt());
            int[] dp = new int[n + 1];
            Array.Fill(dp, -1);
            dp[0] = 0;

            for (int i = 0; i < m; i++) 
            {

                int a = ReadInt();
                int b = ReadInt();
                int c = ReadInt();
                int d = ReadInt();

                int cnt = a / b;
                int e = n - c;
                for (int j = 0; j < cnt; j++)
                {

                    for (int k = e; k >= 0; k--)
                    {

                        if (dp[k] == -1) continue;
                        int next = k + c;
                        dp[next] = Math.Max(dp[next], dp[k] + d);
                    }
                }
            }

            int ret = 0;
            for (int i = 0; i <= n; i++)
            {

                if (dp[i] == -1) continue;
                int add = (n - i) / special.c;
                add *= special.d;

                ret = Math.Max(ret, add + dp[i]);
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
