using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 10
이름 : 배성훈
내용 : 저작권
    문제번호 : 2914번

    수학 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1880
    {

        static void Main1880(string[] args)
        {

            int n, m;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = n * (m - 1) + 1;
                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
            }
        }
    }
}
