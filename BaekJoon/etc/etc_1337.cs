using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 15
이름 : 배성훈
내용 : 시그마
    문제번호 : 2355번

    사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1337
    {

        static void Main1337(string[] args)
        {

            long a, b;
            Input();
            GetRet();

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                a = long.Parse(temp[0]);
                b = long.Parse(temp[1]);

                if (b < a)
                {
                    long swap = a;
                    a = b;
                    b = swap;
                }
            }

            void GetRet()
            {

                long ret;
                if ((a + b) % 2 == 0)
                {

                    ret = (a + b) / 2;
                    ret *= b - a + 1L;
                }
                else
                {

                    ret = (b - a + 1) / 2;
                    ret *= a + b;
                }
                

                Console.Write(ret);
            }
        }
    }
}
