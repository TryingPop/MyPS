using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 7단계 4번 문제
 * 
 * 달팽이는 올라가고 싶다
 */

namespace BaekJoon._07
{
    internal class _07_04
    {
        static void Main4(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(" ");
            double A = double.Parse(inputs[0]);
            double B = double.Parse(inputs[1]);
            double C = double.Parse(inputs[2]);

            double result = 0;

            result = 1 + Math.Ceiling((C - A) / (A - B));
            Console.WriteLine(result);
        }
    }
}
