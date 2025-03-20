using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 31
이름 : 배성훈
내용 : 성냥
    문제번호 : 2029번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1232
    {

        static void Main1232(string[] args)
        {

            string[] init, input;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret1 = 0;
                for (int r = 0; r < 10; r++)
                {

                    for (int c = 0; c < init[r].Length; c++)
                    {

                        if (init[r][c] != input[r][c]) ret1++;
                    }
                }

                int ret2 = 0;
                int[] val = { 6, 3, 0 };
                int[] size = { 3, 6, 9 };
                for (int i = 0; i < 3; i++)
                {

                    for (int r = i; r < 3; r++)
                    {

                        for (int c = i; c < 3; c++)
                        {

                            if (Chk(val[r], val[c], size[i])) ret2++;
                        }
                    }
                }

                Console.Write($"{ret1 >> 1} {ret2}");

                bool Chk(int _r, int _c, int _size)
                {

                    for (int i = 1; i < _size; i++)
                    {

                        if (input[_r][_c + i] == init[0][i]             // UP
                            && input[_r + _size][_c + i] == init[0][i]  // DOWN
                            && input[_r + i][_c] == init[i][0]          // LEFT
                            && input[_r + i][_c + _size] == init[i][0]  // RIGHT
                            ) continue;

                        return false;
                    }

                    return true;
                }
            }

            void Input()
            {

                init = new string[10]
                {

                    "+--+--+--+",
                    "|..|..|..|",
                    "|..|..|..|",
                    "+--+--+--+",
                    "|..|..|..|",
                    "|..|..|..|",
                    "+--+--+--+",
                    "|..|..|..|",
                    "|..|..|..|",
                    "+--+--+--+"
                };

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                input = new string[10];
                for (int i = 0; i < 10; i++)
                {

                    input[i] = sr.ReadLine();
                }

                sr.Close();
            }
        }
    }
}
