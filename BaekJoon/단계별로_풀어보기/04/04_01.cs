using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.15
 * 내용 : 백준 4단계 1번 문제
 * 
 * 최소, 최대
 */

namespace BaekJoon._03
{
    internal class _04_01
    {
        static void Main1(string[] args)
        {
            int len = int.Parse(Console.ReadLine());

            string[] strinput = Console.ReadLine().Split(" ");
            int[] input = Array.ConvertAll(strinput, int.Parse);

            int min = input[0];
            int max = input[0];
            for (int i = 0; i < len; i++)
            {
                if (min > input[i])
                {
                    min = input[i];
                }
                if (max < input[i])
                {
                    max = input[i];
                }
            }

            Console.WriteLine("{0} {1}", min, max);
        }
    }
}
