using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 7단계 1번 문제
 * 
 * 손익분기점
 */

namespace BaekJoon._07
{
    internal class _07_01
    {
        static void Main1(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(" ");

            int A = int.Parse(inputs[0]);
            int B = int.Parse(inputs[1]);
            int C = int.Parse(inputs[2]);

            if (B >= C)
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine((A / (C - B) + 1));
            }
        }
    }
}
