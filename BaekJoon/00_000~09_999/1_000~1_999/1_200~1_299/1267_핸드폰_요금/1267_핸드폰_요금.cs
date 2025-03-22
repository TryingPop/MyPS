using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : 핸드폰 요금
    문제번호 : 1267번

    사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1431
    {

        static void Main1431(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int y = 0, m = 0;
            for (int i = 0; i < n; i++)
            {

                y += 1 + arr[i] / 30;
                m += 1 + arr[i] / 60;
            }

            y *= 10;
            m *= 15;

            if (y == m) Console.Write($"Y M {y}");
            else if (y < m) Console.Write($"Y {y}");
            else Console.Write($"M {m}");

            
        }
    }
}
