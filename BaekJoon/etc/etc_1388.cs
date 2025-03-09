using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaekJoon.etc
{
    internal class etc_1388
    {

        static void Main1388(string[] args)
        {

            // 21151
            char[][] board;
            int row, col, curve;
            string find;

            Input();

            GetRet();

            void GetRet()
            {

                int IMPO = 1_234_567;
                int[] dirR = { -1, 0, 1, 1, 1, 0, -1, -1 };
                int[] dirC = { -1, -1, -1, 0, 1, 1, 1, 0 };

                int[,,,] dp;

                SetDp();

                GetRet();

                void GetRet()
                {


                }

                void SetDp()
                {

                    dp = new int[curve + 1, row, col, 8];

                    for (int r = 0; r < row; r++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            if (find[0] != board[r][c]) continue;
                            for (int dir = 0; dir < 8; dir++)
                            {

                                dp[0, r, c, dir] = 1;
                            }
                        }
                    }
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || row <= _r || _c < 0 || col <= _c;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                int[] size = Array.ConvertAll(sr.ReadLine().Split(),int.Parse);
                row = size[0];
                col = size[1];
                board = new char[row][];
                for (int r = 0; r < row; r++)
                {

                    string[] input = sr.ReadLine().Split();
                    board[r] = new char[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = input[c][0];
                    }
                }

                curve = int.Parse(sr.ReadLine());
                find = sr.ReadLine().Trim();
            }
        }
    }
}
