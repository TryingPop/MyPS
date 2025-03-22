using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 29
이름 : 배성훈
내용 : 그림
    문제번호 : 1926번

    BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1299
    {

        static void Main1299(string[] args)
        {

            int row, col;
            int[][] board;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret1 = 0, ret2 = 0;

                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                Queue<(int r, int c)> q = new(row * col);

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == 0) continue;
                        ret1++;
                        ret2 = Math.Max(ret2, BFS(r, c));
                    }
                }

                Console.Write($"{ret1}\n{ret2}");

                int BFS(int _r, int _c)
                {

                    int ret = 1;
                    q.Enqueue((_r, _c));
                    board[_r][_c] = 0;

                    while (q.Count > 0)
                    {

                        var node = q.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];

                            if (ChkInvalidPos(nR, nC) || board[nR][nC] == 0) continue;
                            board[nR][nC] = 0;
                            ret++;
                            q.Enqueue((nR, nC));
                        }
                    }

                    return ret;
                }
                bool ChkInvalidPos(int _r, int _c) => _r < 0 || _c < 0 || _r >= row || _c >= col;
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

                int ReadInt()
                {

                    int c, ret = 0;
                    while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }
}
