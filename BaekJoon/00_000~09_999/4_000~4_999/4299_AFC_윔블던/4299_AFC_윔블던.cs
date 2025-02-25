using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 25
이름 : 배성훈
내용 : AFC 윔블던
    문제번호 : 4299번

    수학, 사칙연산 문제다.
    글은 많은데, 
*/

namespace BaekJoon.etc
{
    internal class etc_1360
    {

        static void Main1360(string[] args)
        {

            string[] temp = Console.ReadLine().Split();
            int add = Convert.ToInt32(temp[0]);
            int sub = Convert.ToInt32(temp[1]);

            int a = (add + sub) / 2;
            int b = (add - sub) / 2;

            bool flag = a >= 0 && b >= 0 && (a + b == add) && (a - b == sub);
            if (flag) Console.Write($"{a} {b}");
            else Console.Write(-1);
        }
    }
}
