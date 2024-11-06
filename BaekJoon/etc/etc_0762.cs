using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 20
이름 : 배성훈
내용 : 특별한 가지
    문제번호: 31668번

    수학, 사칙연산 문제다
    파묻튀밥 개수를 구하고, 가지교환 수를 곱하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0762
    {

        static void Main762(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            Console.WriteLine(k * m / n);
        }
    }
}
