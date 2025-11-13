using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 23
이름 : 배성훈
내용 : 필터
    문제번호 : 1895번

    구현, 브루트포스, 정렬 문제다.
    조건대로 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1948
    {

        static void Main1948(string[] args)
        {

            int row, col, t;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int[] arr = new int[9];
                int ret = 0;
                for (int r = 0; r < row - 2; r++)
                {

                    for (int c = 0; c < col - 2; c++)
                    {

                        if (Chk(r, c)) ret++;
                    }
                }
                Console.Write(ret);

                bool Chk(int sR, int sC)
                {

                    int idx = 0;
                    for (int r = 0; r < 3; r++)
                    {

                        for (int c = 0; c < 3; c++)
                        {

                            arr[idx++] = board[r + sR][c + sC];
                        }
                    }

                    Array.Sort(arr);
                    return arr[4] >= t;
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

                t = ReadInt();

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
