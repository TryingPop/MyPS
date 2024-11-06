using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 3단계 9번 문제
 * 
 * 별 찍기 - 1
 */

namespace BaekJoon._03
{
    internal class _03_09
    {
        static void Main9(string[] args)
        {
            int len = int.Parse(Console.ReadLine());

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
    }
}
