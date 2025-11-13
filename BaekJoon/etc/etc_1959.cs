using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 1
이름 : 배성훈
내용 : 현명한 나이트
    문제번호 : 18404번

    BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1959
    {

        static void Main1959(string[] args)
        {

            int n, m;
            (int r, int c)[] pos;

            Input();

            GetRet();

            void GetRet()
            {

                int[][] board = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];
                    Array.Fill(board[i], -1);
                }

                BFS();

                Output();

                void Output()
                {

                    using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                    for (int i = 1; i <= m; i++)
                    {

                        sw.Write($"{board[pos[i].r][pos[i].c]} ");
                    }
                }

                bool ChkInvalidPos(int r, int c)
                    => r < 0 || c < 0 || r >= n || c >= n;

                void BFS()
                {

                    Queue<(int r, int c)> q = new(n * n);
                    int[] dirR = { -2, -1, 1, 2, 2, 1, -1, -2 };
                    int[] dirC = { -1, -2, -2, -1, 1, 2, 2, 1 };

                    q.Enqueue(pos[0]);
                    board[pos[0].r][pos[0].c] = 0;

                    while (q.Count > 0)
                    {

                        (int r, int c) = q.Dequeue();

                        for (int i = 0; i < 8; i++)
                        {

                            int nR = r + dirR[i];
                            int nC = c + dirC[i];

                            if (ChkInvalidPos(nR, nC) || board[nR][nC] != -1) continue;

                            board[nR][nC] = board[r][c] + 1;
                            q.Enqueue((nR, nC));
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                pos = new (int r, int c)[m + 1];
                for (int i = 0; i <= m; i++)
                {

                    pos[i] = (ReadInt() - 1, ReadInt() - 1);
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
