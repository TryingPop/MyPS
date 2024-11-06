using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 22
이름 : 배성훈
내용 : 팩토리얼 2
    문제번호 : 27433번
*/

namespace BaekJoon._24
{
    internal class _24_01
    {

        static void Main1(string[] args)
        {


            int num = int.Parse(Console.ReadLine());

            long result = Factorial(num);

            Console.WriteLine(result);
        }

        // 팩토리얼 함수
        static long Factorial(int value)
        {

            if (value < 2) return 1;
            return value * Factorial(value - 1);
        }
    }
}
