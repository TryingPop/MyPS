using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 10
이름 : 배성훈
내용 : 퐁당퐁당 1
    문제번호 : 17944번

    수학 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1969
    {

        static void Main1969(string[] args)
        {

            int n, m;

            Input();

            GetRet();

            void GetRet()
            {

                int div = 2 * n - 1;
                m %= div * 2;

                if (m == 0) m = 2;
                int ret;
                if (m < n * 2) ret = m;
                else ret = 4 * n - m;

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
