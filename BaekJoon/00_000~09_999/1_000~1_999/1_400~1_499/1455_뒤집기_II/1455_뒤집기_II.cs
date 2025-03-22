using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 뒤집기 II
    문제번호 : 1455번

    그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1448
    {

        static void Main1448(string[] args)
        {

            int row, col;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = 0;
                for (int r = row - 1; r >= 0; r--)
                {

                    for (int c = col - 1; c >= 0; c--)
                    {

                        if (board[r][c] == 0) continue;
                        Change(r, c);
                        ret++;
                    }
                }

                Console.Write(ret);

                void Change(int _eR, int _eC)
                {

                    for (int r = 0; r <= _eR; r++)
                    {

                        for (int c = 0; c <= _eC; c++)
                        {

                            board[r][c] ^= 1;
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] size = sr.ReadLine().Split();
                row = int.Parse(size[0]);
                col = int.Parse(size[1]);

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    string input = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = input[c] - '0';
                    }
                }
            }
        }
    }
}
