using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 19
이름 : 배성훈
내용 : 마법의 나침반
    문제번호 : 32372번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1829
    {

        static void Main1829(string[] args)
        {

            int[] R = { 123, -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] C = { 123, 0, 1, 1, 1, 0, -1, -1, -1 };

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();

            bool[][] board = new bool[n + 1][];
            for (int r = 1; r <= n; r++)
            {

                board[r] = new bool[n + 1];
            }

            int minR = 1, minC = 1, maxR = n, maxC = n;

            for (int i = 0; i < k; i++)
            {

                int r = ReadInt();
                int c = ReadInt();
                int dir = ReadInt();

                board[r][c] = true;

                int chk = R[dir];
                if (chk == 0)
                {

                    minR = r;
                    maxR = r;
                }
                else if (chk < 0)
                    maxR = Math.Min(maxR, r - 1);
                else
                    minR = Math.Max(minR, r + 1);

                chk = C[dir];
                if (chk == 0)
                {

                    minC = c;
                    maxC = c;
                }
                else if (chk < 0)
                    maxC = Math.Min(maxC, c - 1);
                else
                    minC = Math.Max(minC, c + 1);
            }

            for (int r = minR; r <= maxR; r++)
            {

                for (int c = minC; c <= maxC; c++)
                {

                    if (board[r][c]) continue;
                    Console.Write($"{r} {c}");
                    return;
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
                    if (c == '\n' || c == -1) return true;

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
