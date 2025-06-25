using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 24
이름 : 배성훈
내용 : 수도배관공사
    문제번호 : 2073번

    dp, 배낭 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1727
    {

        static void Main1727(string[] args)
        {

            int d, p;
            (int l, int c)[] pipe;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dp = new int[d + 1];
                
                Array.Fill(dp, -1);
                dp[0] = 123_456_789;

                int e = 0;
                for (int i = 0; i < p; i++)
                {

                    for (int j = e; j >= 0; j--)
                    {

                        if (dp[j] == -1) continue;
                        int next = j + pipe[i].l;
                        if (next > d) continue;
                        int cur = Math.Min(dp[j], pipe[i].c);
                        if (dp[next] < cur) dp[next] = cur;
                    }

                    e = Math.Min(d, e + pipe[i].l);
                }

                Console.Write(dp[d]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                d = ReadInt();
                p = ReadInt();

                pipe = new (int l, int c)[p];
                for (int i = 0; i < p; i++)
                {

                    pipe[i] = (ReadInt(), ReadInt());
                }

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
}
