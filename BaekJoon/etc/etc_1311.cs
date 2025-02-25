using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 3
이름 : 배성훈
내용 : 폴리오미노
    문제번호 : 1343번

    구현, 그리디 문제다.
    A를 사용할 수 있으면 먼저 사용하는게 사전순으로 아펭 선다.
*/

namespace BaekJoon.etc
{
    internal class etc_1311
    {

        static void Main1311(string[] args)
        {

            char[] ret;

            Solve();
            void Solve()
            {

                GetRet();

                Output();
            }

            void Output()
            {

                if (ret[0] == 'X')
                {

                    Console.Write(-1);
                    return;
                }

                for (int i = 0; i < ret.Length; i++)
                {

                    Console.Write(ret[i]);
                }
            }

            void GetRet()
            {

                string str = Console.ReadLine();
                ret = new char[str.Length];
                int idx = 0, cnt = 0;

                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] == '.')
                    {

                        if (ChkOdd())
                        {

                            ret[0] = 'X';
                            return;
                        }

                        Fill();
                        ret[idx++] = '.';
                    }
                    else cnt++;
                }

                if (ChkOdd())
                {

                    ret[0] = 'X';
                    return;
                }

                Fill();

                bool ChkOdd() => (cnt & 1) == 1;
                
                void Fill()
                {

                    if (cnt == 0) return;

                    int add = cnt / 4;
                    for (int j = 0; j < add; j++)
                    {

                        for (int k = 0; k < 4; k++)
                        {

                            ret[idx++] = 'A';
                        }
                    }

                    cnt %= 4;
                    if (cnt > 0)
                    {

                        ret[idx++] = 'B';
                        ret[idx++] = 'B';
                    }

                    cnt = 0;
                }
            }
        }
    }
}
