using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 20
이름 : 배성훈
내용 : 알약
    문제번호 : 4811번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1718
    {

        static void Main1718(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            long[][] dp;
            int n;

            Init();

            while ((n = ReadInt()) != 0)
            {

                sw.Write($"{dp[n][0]}\n");
            }

            void Init()
            {

                int NOT_VISIT = -1;

                dp = new long[31][];
                for (int i = 0; i <= 30; i++)
                {

                    dp[i] = new long[31];
                    Array.Fill(dp[i], NOT_VISIT);
                }

                dp[0][1] = 1;

                for (int i = 1; i <= 30; i++)
                {

                    DFS(i, 0);
                }

                long DFS(int _w, int _h)
                {

                    ref long ret = ref dp[_w][_h];
                    if (ret != NOT_VISIT) return ret;
                    ret = 0;

                    if (_w > 0) ret += DFS(_w - 1, _h + 1);
                    if (_h > 0) ret += DFS(_w, _h - 1);
                    return ret;
                }
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
