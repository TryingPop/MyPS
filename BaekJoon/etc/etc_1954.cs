using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 28
이름 : 배성훈
내용 : 셔틀런
    문제번호 : 13268번

    구현, 많은 조건 분기 문제다.
    반복되는 즉, 주기가 있는 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1954
    {

        static void Main1954(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            n %= 100;

            int ret;
            if (n <= 10)
            {

                if (n == 0 || n == 10) ret = 0;
                else ret = 1;
            }
            else if (n <= 30)
            {

                if (n == 30) ret = 0;
                else if (n <= 15) ret = 1;
                else if (n <= 24) ret = 2;
                else ret = 1;
            }
            else if (n <= 60)
            {

                if (n == 60) ret = 0;
                else if (n <= 35) ret = 1;
                else if (n <= 40) ret = 2;
                else if (n <= 49) ret = 3;
                else if (n <= 54) ret = 2;
                else ret = 1;
            }
            else
            {

                if (n <= 65) ret = 1;
                else if (n <= 70) ret = 2;
                else if (n <= 75) ret = 3;
                else if (n <= 84) ret = 4;
                else if (n <= 89) ret = 3;
                else if (n <= 94) ret = 2;
                else ret = 1;
            }

            Console.Write(ret);
        }
    }
}
