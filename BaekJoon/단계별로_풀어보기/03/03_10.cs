using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 3단계 10번 문제
 * 
 * 별 찍기 - 2
 */

namespace BaekJoon._03
{
    internal class _03_10
    {
        static void Main10(string[] args)
        {
            int len = int.Parse(Console.ReadLine());

            for (int i = 0; i < len; i++)
            {
                for (int k = 0; k <= len - 2 - i; k++)
                {
                    Console.Write(" ");
                }
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
    }
}
