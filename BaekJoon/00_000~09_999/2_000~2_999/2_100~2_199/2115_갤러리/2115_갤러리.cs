using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 19
이름 : 배성훈
내용 : 갤러리
    문제번호 : 2115번

    구현 문제다.
    4방향을 브루트포스로 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1421
    {

        static void Main1421(string[] args)
        {

            char EMPTY = '.';
            char WALL = 'X';

            int row, col;
            string[] board;
            Input();

            GetRet();

            void GetRet()
            {

                int ret = 0;
                // UP
                for (int r = 1; r < row; r++)
                {

                    for (int c = 1; c < col; c++)
                    {

                        if (board[r][c] == EMPTY && board[r][c - 1] == EMPTY
                            && board[r - 1][c] == WALL && board[r - 1][c - 1] == WALL)
                        {

                            ret++;
                            c++;
                        }
                    }
                }

                // DOWN
                for (int r = row - 2; r >= 0; r--)
                {

                    for (int c = 1; c < col; c++)
                    {

                        if (board[r][c] == EMPTY && board[r][c - 1] == EMPTY
                            && board[r + 1][c] == WALL && board[r + 1][c - 1] == WALL)
                        {

                            ret++;
                            c++;
                        }
                    }
                }

                // LEFT
                for (int c = 1; c < col; c++)
                {

                    for (int r = 1; r < row; r++)
                    {

                        if (board[r][c] == EMPTY && board[r - 1][c] == EMPTY
                            && board[r][c - 1] == WALL && board[r - 1][c - 1] == WALL)
                        {

                            ret++;
                            r++;
                        }
                    }
                }

                // RIGHT
                for (int c = col - 2; c >= 0; c--)
                {

                    for (int r = 1; r < row; r++)
                    {

                        if (board[r][c] == EMPTY && board[r - 1][c] == EMPTY
                            && board[r][c + 1] == WALL && board[r - 1][c + 1] == WALL)
                        {

                            ret++;
                            r++;
                        }
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string[] temp = sr.ReadLine().Split();
                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);

                board = new string[row];
                for (int r = 0; r < row; r++)
                {

                    board[r] = sr.ReadLine();
                }
            }
        }
    }
}
