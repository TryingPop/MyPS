using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 진법 변환
    문제번호  : 2745번
*/

namespace BaekJoon._23
{
    internal class _23_01
    {

        static void Main1(string[] args)
        {

            // 0 : 진법 변환된 숫자, 1 : 진법 단위
            string[] input = Console.ReadLine().Split(' ');


            // Console.WriteLine((int)'A');    // 65
            // Console.WriteLine((int)'9');    // 57
            // Console.WriteLine((int)'0');    // 48

            int nums = int.Parse(input[1]);     // 진법 확인용
            int result = 0;
            
            for (int i = 0; i < input[0].Length; i++)
            {

                // A : 10, B : 11, C : 12, ..., Z : 35
                // 인 경우
                if (input[0][i] > 57)   // input[0][i] > '9'
                {

                    result += (input[0][i] - 55) * (int)Math.Pow(nums, input[0].Length - 1 - i);    // input[0][i] - 'A' + 10
                }
                // 숫자 0, 1, 2, ... , 9인 경우
                else
                {

                    result += (input[0][i] - 48) * (int)Math.Pow(nums, input[0].Length - 1 - i);    // input[0][i] - '0'
                }
            }

            Console.WriteLine(result);
        }
    }
}
