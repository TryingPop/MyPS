using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 12
이름 : 배성훈
내용 : Lecture Allocation
    문제번호 : 25791번

    dp, 배낭 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1696
    {

        static void Main1696(string[] args)
        {

            int l, t;
            (int c1, int c2, int c3)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int INF = 1_000_000_000;
                int[] dp = new int[l + 1];
                Array.Fill(dp, INF);
                dp[0] = 0;

                int e = 0;
                for (int i = 0; i < t; i++)
                {

                    for (int j = e; j >= 0; j--)
                    {

                        if (dp[j] == INF) continue;

                        int next = j + 1;
                        if (next <= l) dp[next] = Math.Min(dp[next], dp[j] + arr[i].c1);
                        next++;

                        if (next <= l) dp[next] = Math.Min(dp[next], dp[j] + arr[i].c2);
                        next++;

                        if (next <= l) dp[next] = Math.Min(dp[next], dp[j] + arr[i].c3);
                    }

                    e = Math.Min(e + 3, l);
                }

                Console.Write(dp[l]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                l = ReadInt();
                t = ReadInt();

                arr = new (int c1, int c2, int c3)[t];
                for (int i = 0; i < t; i++)
                {

                    arr[i] = (ReadInt(), ReadInt(), ReadInt());
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

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
