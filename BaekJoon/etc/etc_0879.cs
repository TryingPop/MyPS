using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 14
이름 : 배성훈
내용 : 빠른 숫자 탐색
    문제번호 : 25416번

    BFS 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0879
    {

        static void Main879(string[] args)
        {

            int ROW = 5, COL = 5;
            StreamReader sr;

            int[][] board;
            (int r, int c) s, e;
            Solve();
            void Solve()
            {

                Input();

                BFS();

                Console.Write(board[e.r][e.c] - 1);
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= ROW || _c >= COL;
            }

            void BFS()
            {

                Queue<(int r, int c)> q = new(25);
                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                q.Enqueue(s);
                board[s.r][s.c] = 1;

                while(q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] != 0) continue;
                        board[nextR][nextC] = board[node.r][node.c] + 1;

                        q.Enqueue((nextR, nextC));
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                board = new int[ROW][];
                e = (-1, -1);
                for (int r = 0; r < ROW; r++)
                {

                    board[r] = new int[COL];
                    for (int c = 0; c < COL; c++)
                    {

                        int cur = ReadInt();
                        if (cur == -1) board[r][c] = -1;
                        else if (cur == 1) e = (r, c);
                    }
                }

                s = (ReadInt(), ReadInt());
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }
}
