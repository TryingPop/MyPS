using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 7단계 5번 문제
 * 
 * ACM 호텔
 */

namespace BaekJoon._07
{
    internal class _07_05
    {
        static void Main5(string[] args)
        {
            int length = int.Parse(Console.ReadLine());
            for (int i = 0; i < length; i++)
            {
                string[] inputs = Console.ReadLine().Split(" ");
                int A = int.Parse(inputs[0]);
                int B = int.Parse(inputs[1]);
                int C = int.Parse(inputs[2]);

                int x = 1 + (C / A);
                int y = (C % A);
                if (y == 0)
                {
                    y = A;
                    x -= 1;
                }

                if (x < 10)
                {
                    Console.WriteLine($"{y}0{x}");
                }
                else
                {
                    Console.WriteLine($"{y}{x}");
                }
            }
        }
    }
}