using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 7
이름 : 배성훈
내용 : 삼각형 외우기
    문제번호 : 10101번

    세 각의 크기가 모두 60이면, Equilateral
    세 각의 합이 180이고, 두 각이 같은 경우에는 Isosceles
    세 각의 합이 180이고, 같은 각이 없는 경우에는 Scalene
    세 각의 합이 180이 아닌 경우에는 Error
    
    인 프로그램을 만드시오
*/

namespace BaekJoon._11
{
    internal class _11_06
    {

        static void Main6(string[] args)
        {

            int A, B, C;
            A = int.Parse(Console.ReadLine());
            B = int.Parse(Console.ReadLine());
            C = int.Parse(Console.ReadLine());

            if (A + B + C == 180)
            {

                if (A == B && B == C)
                {

                    Console.WriteLine("Equilateral");
                }
                else if (A == B || B == C || C == A)
                {

                    Console.WriteLine("Isosceles");
                }
                else
                {

                    Console.WriteLine("Scalene");
                }
            }
            else
            {

                Console.WriteLine("Error");
            }
        }
    }
}
