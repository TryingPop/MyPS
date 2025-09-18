using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 9
이름 : 배성훈
내용 : 옥수수밭
    문제번호 : 30024번

    우선순위 큐, BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1877
    {

        static void Main1877(string[] args)
        {

            int row, col, k;
            int[][] board;
            PriorityQueue<(int r, int c), int> pq;

            Input();

            Init();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                while (k-- > 0)
                {

                    (int r, int c) = pq.Dequeue();
                    sw.Write($"{r + 1} {c + 1}\n");

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = r + dirR[i];
                        int nC = c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || ChkVisit(nR, nC)) continue;
                        Enqueue(nR, nC);
                    }
                }
            }

            void Init()
            {

                pq = new(row * col);

                for (int r = 0; r < row; r++)
                {

                    Enqueue(r, 0);
                    Enqueue(r, col - 1);
                }

                for (int c = 1; c < col - 1; c++)
                {

                    Enqueue(0, c);
                    Enqueue(row - 1, c);
                }
            }

            bool ChkInvalidPos(int r, int c)
                => r < 0 || c < 0 || r >= row || c >= col;

            bool ChkVisit(int r, int c)
                => board[r][c] == 0;

            void Enqueue(int r, int c)
            {

                pq.Enqueue((r, c), -board[r][c]);
                board[r][c] = 0;
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

                k = ReadInt();

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
