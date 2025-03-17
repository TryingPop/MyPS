using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 29
이름 : 배성훈
내용 : 박 터뜨리기
    문제번호 : 19939번

    수학, 그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1301
    {

        static void Main1301(string[] args)
        {

            int n, k;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);
            }

            void GetRet()
            {

                int min = (k * (k + 1)) >> 1;
                
                if (n < min)
                {

                    Console.Write(-1);
                    return;
                }

                n -= min;

                if (n % k == 0)
                    Console.Write(k - 1);
                else
                    Console.Write(k);
            }
        }
    }
}
