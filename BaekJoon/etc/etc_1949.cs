using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 24
이름 : 배성훈
내용 : 스도쿠 채점
    문제번호 : 9291번

    구현, 브루트포스문제다.
    조건대로 구현하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1949
    {

        static void Main1949(string[] args)
        {

            int SIZE = 9;
            int SIZE_SQRT = 3;

            string C = "Case ";
            string M = ": ";
            string Y = "CORRECT\n";
            string N = "INCORRECT\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[][] board;
            int t;

            Init();

            for (int i = 1; i <= t; i++)
            {

                sw.Write(C);
                sw.Write(i);
                sw.Write(M);

                Input();

                GetRet();
            }

            void Init()
            {

                board = new int[SIZE][];
                for (int r = 0; r < SIZE; r++)
                {

                    board[r] = new int[SIZE];
                }

                t = ReadInt();
            }

            void Input()
            {

                for (int r = 0; r < SIZE; r++)
                {

                    for (int c = 0; c < SIZE; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }
            }

            void GetRet()
            {

                bool ret = Chk1() & Chk2() & Chk3();
                sw.Write(ret ? Y : N);

                // 가로
                bool Chk1()
                {

                    for (int r = 0; r < SIZE; r++)
                    {

                        int state = 0;
                        for (int c = 0; c < SIZE; c++)
                        {

                            int cur = 1 << board[r][c];
                            if ((state & cur) != 0) return false;
                            state |= cur;
                        }
                    }

                    return true;
                }

                bool Chk2() 
                { 
                
                    for (int c = 0; c < SIZE; c++)
                    {

                        int state = 0;
                        for (int r = 0; r < SIZE; r++)
                        {

                            int cur = 1 << board[r][c];
                            if ((state & cur) != 0) return false;
                            state |= cur;
                        }
                    }

                    return true;
                }

                bool Chk3()
                {

                    for (int sR = 0; sR < SIZE_SQRT; sR++)
                    {

                        for (int sC = 0; sC < SIZE_SQRT; sC++)
                        {

                            int state = 0;
                            for (int aR = 0; aR < SIZE_SQRT; aR++)
                            {

                                for (int aC = 0; aC < SIZE_SQRT; aC++)
                                {

                                    int cur = 1 << board[sR * 3 + aR][sC * 3 + aC];
                                    if ((state & cur) != 0) return false;
                                    state |= cur;
                                }
                            }
                        }
                    }

                    return true;
                }
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
