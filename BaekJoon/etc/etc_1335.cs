using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 14
이름 : 배성훈
내용 : 나누기
    문제번호 : 1075번

    수학, 브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1335
    {

        static void Main1335(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            n = (n / 100) * 100;
            n %= k;
            n = (k - n) % k;
            Console.Write($"{n:00}");
        }
    }
}
