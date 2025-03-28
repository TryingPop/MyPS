using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 농장 관리
    문제번호 : 1245번

    BFS, 그래프 탐색 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1482
    {

        static void Main1482(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int row = ReadInt();
            int col = ReadInt();

            int[][] board = new int[row][];
            bool[][] visit = new bool[row][];

            for (int r = 0; r < row; r++)
            {

                board[r] = new int[col];
                visit[r] = new bool[col];

                for (int c = 0; c < col; c++)
                {

                    board[r][c] = ReadInt();
                }
            }

            int[] dirR = { -1, 0, 1, 1, 1, 0, -1, -1 };
            int[] dirC = { -1, -1, -1, 0, 1, 1, 1, 0 };
            Queue<(int r, int c)> q = new(row * col);

            int ret = 0;
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    if (visit[r][c]) continue;
                    q.Enqueue((r, c));
                    visit[r][c] = true;

                    if (BFS(board[r][c])) ret++;
                }
            }

            Console.Write(ret);

            bool BFS(int _val)
            {

                bool ret = true;
                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    for (int i = 0; i < 8; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC)) continue;

                        if (_val == board[nR][nC] && !visit[nR][nC])
                        {

                            visit[nR][nC] = true;
                            q.Enqueue((nR, nC));
                        }
                        else if (_val < board[nR][nC])
                            ret = false;
                    }
                }

                return ret;

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1&& c != ' ' && c != '\n')
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
