using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : A + B - C
    문제번호 : 31403번

    수학, 문자열, 사칙연산 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1049
    {

        static void Main1049(string[] args)
        {

            string A = Console.ReadLine();
            string B = Console.ReadLine();
            string C = Console.ReadLine();

            int a = int.Parse(A);
            int b = int.Parse(B);
            int c = int.Parse(C);

            int ab = int.Parse(A + B);
            Console.Write($"{a + b - c}\n{ab - c}");
        }
    }
}
