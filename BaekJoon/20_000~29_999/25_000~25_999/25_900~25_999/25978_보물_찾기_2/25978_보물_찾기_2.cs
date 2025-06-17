using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 17
이름 : 배성훈
내용 : 보물 찾기 2
    문제번호 : 27978번

    BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1709
    {

        static void Main1709(string[] args)
        {

            int row, col;
            bool[][] board;

            (int r, int c) s, e;

            Input();

            GetRet();

            void GetRet()
            {

                int NOT_VISIT = -1;
                int[][] move = new int[row][];
                for (int i = 0; i < row; i++)
                {

                    move[i] = new int[col];
                    Array.Fill(move[i], NOT_VISIT);
                }

                BFS();

                Console.Write(move[e.r][e.c]);

                void BFS()
                {

                    Queue<(int r, int c)> cur = new(8 * Math.Max(row, col)), next = new(8 * Math.Max(row, col));
                    int[] dirC1 = { -1, -1, -1, 0, 0 }, dirR1 = { -1, 0, 1, -1, 1 };
                    int[] dirC2 = { 1, 1, 1 }, dirR2 = { -1, 0, 1 };

                    cur.Enqueue(s);
                    move[s.r][s.c] = 0;

                    while (cur.Count > 0)
                    {

                        while (cur.Count > 0)
                        {

                            var node = cur.Dequeue();
                            int curMove = move[node.r][node.c];

                            for (int i = 0; i < 3; i++)
                            {

                                int nR = node.r + dirR2[i];
                                int nC = node.c + dirC2[i];

                                if (ChkInvalidPos(nR, nC) || board[nR][nC]) continue;
                                board[nR][nC] = true;
                                move[nR][nC] = curMove;
                                cur.Enqueue((nR, nC));
                            }

                            for (int i = 0; i < 5; i++)
                            {

                                int nR = node.r + dirR1[i];
                                int nC = node.c + dirC1[i];

                                if (ChkInvalidPos(nR, nC) || board[nR][nC]) continue;
                                board[nR][nC] = true;
                                move[nR][nC] = curMove + 1;
                                next.Enqueue((nR, nC));
                            }
                        }

                        var temp = cur;
                        cur = next;
                        next = temp;
                    }

                    bool ChkInvalidPos(int _r, int _c)
                        => _r < 0 || _c < 0 || _r >= row || _c >= col;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string input = sr.ReadLine();
                string[] temp = input.Split();
                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);

                board = new bool[row][];
                s = (0, 0);
                e = (0, 0);
                for (int i = 0; i < row; i++)
                {

                    board[i] = new bool[col];
                    input = sr.ReadLine();

                    for (int j = 0; j < col; j++)
                    {

                        if (input[j] == 'K') s = (i, j);
                        else if (input[j] == '*') e = (i, j);
                        else if (input[j] == '#') board[i][j] = true;
                    }
                }
            }
        }
    }
}
