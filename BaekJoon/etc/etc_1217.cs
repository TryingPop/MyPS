using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 25
이름 : 배성훈
내용 : Receptet
    문제번호 : 26933번

    사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1217
    {

        static void Main1217(string[] args)
        {

            int n, h, b, k;
            int ret = 0;

            n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {

                string[] temp = Console.ReadLine().Split();
                h = int.Parse(temp[0]);
                b = int.Parse(temp[1]);
                if (h >= b) continue;
                k = int.Parse(temp[2]);

                ret += (b - h) * k;
            }

            Console.Write(ret);
        }
    }
}
