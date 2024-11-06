using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 7
이름 : 배성훈
내용 : 삼각형과 세 변
    문제번호 : 5073번

    세 변의 길이가 모두 같은 경우 "Equilateral" 
    두 변의 길이만 같은 경우 "Isosceles" 
    세 변의 길이가 모두 다른 경우 "Scalene"
    
    단 세 변의 길이가 삼각형의 조건을 만족하지 못하는 경우에는 "Invalid"
    를 출력하는 프로그램 만들기
*/

namespace BaekJoon._11
{
    internal class _11_07
    {

        static void Main7(string[] args)
        {

            int a, b, c;
            int[] inputs;

            while (true)
            {

                inputs = Array.ConvertAll(Console.ReadLine().Split(' '),
                    input => int.Parse(input));

                a = inputs[0];
                b = inputs[1];
                c = inputs[2];

                if (a < b + c && b < c + a && c < a + b)
                {

                    if (a == b && b == c)
                    {

                        Console.WriteLine("Equilateral");
                    }
                    else if (a == b || b == c || c == a)
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

                    if (a == 0 && b == 0 && c == 0)
                    {

                        break;
                    }

                    Console.WriteLine("Invalid");
                }
            }
        }
    }
}