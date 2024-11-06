using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 25
이름 : 배성훈
내용 : 쿠키의 신체 측정
    문제번호 : 20125번

    구현 문제다
    StreamReader로 입력 데이터를 받아오는데
    Console.ReadLine으로 처음 숫자를 시도해 4번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1082
    {

        static void Main1082(string[] args)
        {

            int[][] board;
            (int r, int c) heart;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int r = heart.r;
                int c = heart.c - 1;

                int[] ret = new int[5];
                while (ChkValidPos(r, c) && board[r][c] == 1)
                {

                    ret[0]++;
                    c--;
                }

                r = heart.r;
                c = heart.c + 1;

                while (ChkValidPos(r, c) && board[r][c] == 1)
                {

                    ret[1]++;
                    c++;
                }

                r = heart.r + 1;
                c = heart.c;

                while (ChkValidPos(r, c) && board[r][c] == 1)
                {

                    ret[2]++;
                    r++;
                }

                int nR = r;

                r = nR;
                c = heart.c - 1;

                while (ChkValidPos(r, c) && board[r][c] == 1)
                {

                    ret[3]++;
                    r++;
                }

                r = nR;
                c = heart.c + 1;

                while (ChkValidPos(r, c) && board[r][c] == 1)
                {

                    ret[4]++;
                    r++;
                }

                Console.Write($"{heart.r + 1} {heart.c + 1}\n{ret[0]} {ret[1]} {ret[2]} {ret[3]} {ret[4]}\n");
            }

            bool ChkValidPos(int _r, int _c)
            {

                return _r >= 0 && _c >= 0 && _r < n && _c < n;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                heart = (-1, -1);
                bool flag = false;
                board = new int[n][];
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        int cur = sr.Read();
                        if (cur == '_') continue;
                        board[r][c] = 1;
                        if (flag) continue;
                        flag = true;
                        heart = (r + 1, c);
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
            }
        }
    }
}
