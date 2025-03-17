using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 15
이름 : 배성훈
내용 : The Chivalrous Cow
    문제번호 : 6004번

    BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1277
    {

        static void Main1277(string[] args)
        {

            int[] size;
            string[] board;
            int[][] move;
            Queue<(int r, int c)> q;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                q = new(size[0] * size[1]);

                (int r, int c) init = (-1, -1), end = (-1, -1);
                for (int r = 0; r < size[0]; r++)
                {

                    for (int c = 0; c < size[1]; c++)
                    {

                        if (board[r][c] == 'K') init = (r, c);
                        else if (board[r][c] == 'H') end = (r, c);
                    }
                }

                q.Enqueue(init);
                move[init.r][init.c] = 0;

                int[] dirR = { -1, -2, -2, -1, 1, 2, 2, 1 };
                int[] dirC = { -2, -1, 1, 2, 2, 1, -1, -2 };

                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    int cur = move[node.r][node.c];
                    for (int i = 0; i < 8; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC)
                            || move[nR][nC] != -1
                            || board[nR][nC] == '*') continue;

                        move[nR][nC] = cur + 1;
                        q.Enqueue((nR, nC));
                    }
                }

                Console.Write(move[end.r][end.c]);

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _r >= size[0] || _c < 0 || _c >= size[1];
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

                int temp = size[0];
                size[0] = size[1];
                size[1] = temp;

                board = new string[size[0]];
                move = new int[size[0]][];

                for (int i = 0; i < size[0]; i++)
                {

                    board[i] = sr.ReadLine();
                    move[i] = new int[size[0]];
                    Array.Fill(move[i], -1);
                }
            }
        }
    }
}
