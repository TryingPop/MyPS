using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 9
이름 : 배성훈
내용 : 인덕이와 보드게임
    문제번호 : 33926번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1813
    {

        static void Main1813(string[] args)
        {

            int row, col;
            int[][] board;
            bool[][] color;

            Input();

            GetRet();

            void GetRet()
            {

                long[] max = new long[col], min = new long[col];

                Array.Fill(max, long.MinValue);
                Array.Fill(min, long.MaxValue);
                max[0] = board[0][0];
                min[0] = board[0][0];

                CalcCol(0);

                for (int r = 1; r < row; r++)
                {

                    CalcRow(r);
                    CalcCol(r);
                }

                Console.Write(max[col - 1]);

                long Calc(long _a, long _b, bool _c)
                {

                    long ret = _a + _b;
                    if (_c) ret = -ret;
                    return ret;
                }

                void CalcCol(int _r)
                {

                    for (int c = 1; c < col; c++)
                    {

                        long chk1 = Calc(max[c - 1], board[_r][c], color[_r][c]);
                        long chk2 = Calc(min[c - 1], board[_r][c], color[_r][c]);
                        
                        max[c] = Math.Max(max[c], Math.Max(chk1, chk2));
                        min[c] = Math.Min(min[c], Math.Min(chk1, chk2));
                    }
                }

                void CalcRow(int _r)
                {

                    for (int c = 0; c < col; c++)
                    {

                        long chk1 = Calc(max[c], board[_r][c], color[_r][c]);
                        long chk2 = Calc(min[c], board[_r][c], color[_r][c]);

                        max[c] = Math.Max(chk1, chk2);
                        min[c] = Math.Min(chk1, chk2);
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                color = new bool[row][];
                for (int r = 0; r < row; r++)
                {

                    color[r] = new bool[col];
                    for (int c = 0; c < col; c++)
                    {

                        color[r][c] = ReadInt() == 1;
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
}
