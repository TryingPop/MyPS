using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 28
이름 : 배성훈
내용 : 가희와 고구마 먹방
    문제번호 : 21772번

    브루트포스, 백트래킹 문제다.
    모든 경로를 탐색해도 t가 10으로 4^10 = 2^20 으로 약 100만 경우다.
    그래서 브루트포스로 접근했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1586
    {

        static void Main1586(string[] args)
        {

            int row, col, t;
            int[][] board;

            Input();

            GetRet();

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string[] temp = sr.ReadLine().Split();
                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);
                t = int.Parse(temp[2]);

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];

                    string cur = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = cur[c];
                    }
                }
            }

            void GetRet()
            {

                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] != 'G') continue;
                        int ret = DFS(r, c);
                        Console.Write(ret);
                        return;
                    }
                }

                int DFS(int _r, int _c, int _dep = 0, int _eat = 0)
                {

                    if (_dep == t) return _eat;

                    int ret = 0;
                    for (int i = 0; i < 4; i++)
                    {

                        int nR = _r + dirR[i];
                        int nC = _c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC] == '#') continue;

                        int nEat = _eat;
                        bool flag = board[nR][nC] == 'S';
                        if (flag)
                        {

                            nEat++;
                            board[nR][nC] = '.';
                        }

                        ret = Math.Max(DFS(nR, nC, _dep + 1, nEat), ret);

                        if (flag)
                            board[nR][nC] = 'S';
                    }

                    return ret;
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= row || _c >= col;
            }
        }
    }
}
