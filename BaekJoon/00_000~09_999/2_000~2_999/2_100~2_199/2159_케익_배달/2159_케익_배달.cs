using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 3
이름 : 배성훈
내용 : 케익 배달
    문제번호 : 2159번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1669
    {

        static void Main1669(string[] args)
        {

            long INF = 1_000_000_000_000_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int[] dirX = { 0, -1, 0, 1, 0 }, dirY = { 0, 0, -1, 0, 1 };

            long[] dp = new long[5];
            long[] next = new long[5];
            Array.Fill(next, INF);
            int n = ReadInt();

            int sX = ReadInt();
            int sY = ReadInt();

            int eX = ReadInt();
            int eY = ReadInt();
            Calc(sX, sY, eX, eY, 0);
            Next();

            for (int i = 1; i < n; i++)
            {

                eX = ReadInt();
                eY = ReadInt();

                for (int j = 0; j < 5; j++)
                {

                    int chkX = sX + dirX[j];
                    int chkY = sY + dirY[j];

                    Calc(chkX, chkY, eX, eY, dp[j]);
                }

                Next();
            }

            long ret = INF;
            for (int i = 0; i < 5; i++)
            {

                ret = Math.Min(dp[i], ret);
            }

            Console.Write(ret);

            void Next()
            {

                for (int i = 0; i < 5; i++)
                {

                    dp[i] = next[i];
                    next[i] = INF;
                }

                sX = eX;
                sY = eY;
            }

            void Calc(int _sX, int _sY, int _eX, int _eY, long add)
            {

                for (int i = 0; i < 5; i++)
                {

                    int eX = _eX + dirX[i];
                    int eY = _eY + dirY[i];

                    long dis = GetDis(_sX, _sY, eX, eY) + add;
                    next[i] = Math.Min(next[i], dis);
                }
            }

            long GetDis(int _sX, int _sY, int _eX, int _eY)
                => Math.Abs(_sX - _eX) + Math.Abs(_sY - _eY);

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
