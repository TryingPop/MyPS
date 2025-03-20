using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 31
이름 : 배성훈
내용 : 오목
    문제번호 : 2615번

    브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1231
    {

        static void Main1231(string[] args)
        {

            int N = 19;
            int[][] board;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[] dirR = { 1, 0, 1, -1 }, dirC = { 0, 1, 1, 1 };

                for (int r = 0; r < N; r++)
                {

                    for (int c = 0; c < N; c++)
                    {

                        if (board[r][c] == 0) continue;
                        else if (Chk(r, c, board[r][c]))
                        {

                            Console.Write($"{board[r][c]}\n{r + 1} {c + 1}");
                            return;
                        }
                    }
                }

                Console.Write("0");

                // 가로, 세로, 좌대각, 우대각 4방향으로 오목이 되는지 확인
                // 육목은 오목으로 취급 X
                bool Chk(int _r, int _c, int _n)
                {

                    for (int i = 0; i < 4; i++)
                    {

                        if (ChkPos(_r, _c, i, _n)) continue;
                        int cnt = 1;
                        int cR = _r;
                        int cC = _c;

                        
                        for (int j = 0; j < 5; j++)
                        {

                            cR += dirR[i];
                            cC += dirC[i];

                            if (ChkValidPos(cR, cC) && board[cR][cC] == _n) cnt++; 
                            else break;
                        }

                        if (cnt == 5) return true;
                    }

                    return false;
                }

                // 유효한 좌표 확인
                bool ChkValidPos(int _r, int _c)
                    => _r >= 0 && _c >= 0 && _r < N && _c < N;

                // 이미 탐색한 방향인지 확인
                bool ChkPos(int _r, int _c, int _dir, int _chk)
                {

                    _r -= dirR[_dir];
                    _c -= dirC[_dir];
                    return ChkValidPos(_r, _c) && board[_r][_c] == _chk;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                board = new int[N][];

                for (int r = 0; r < N; r++)
                {

                    board[r] = new int[N];
                    for (int c = 0; c < N; c++)
                    {

                        board[r][c] = ReadInt();
                    }
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
}
