using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 6
이름 : 배성훈
내용 : 전쟁 - 전투
    문제번호 : 1303번

    BFS 문제다
    가로, 세로 크기가 주어지는데, 세로 가로로 받아서 1번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1033
    {

        static void Main1033(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] board;
            Queue<(int r, int c)> q;
            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                q = new(row * col);
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                int ret1 = 0, ret2 = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int val = board[r][c];
                        if (val == 0) continue;
                        board[r][c] = 0;
                        q.Enqueue((r, c));
                        
                        int cnt = BFS(val);

                        if (val == 1) ret1 += cnt * cnt;
                        else ret2 += cnt * cnt;
                    }
                }

                Console.Write($"{ret1} {ret2}");
            }

            int BFS(int _v)
            {

                int ret = 0;

                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    ret++;

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC] != _v) continue;
                        board[nR][nC] = 0;
                        q.Enqueue((nR, nC));
                    }
                }

                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                col = ReadInt();
                row = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = sr.Read() == 'W' ? 1 : 2;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
