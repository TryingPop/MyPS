using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.15
 * 내용 : 백준 3단계 13번 문제
 * 
 * 더하기 사이클
 */

namespace BaekJoon._03
{
    internal class _03_13
    {
        static void Main13(string[] args)
        {
            int num0 = int.Parse(Console.ReadLine());
            int num1 = num0;
            int i = 0;

            do
            {
                if (num1 < 10)
                {
                    num1 = 11 * num1;
                }
                else
                {
                    num1 = ((num1 / 10) + (num1 % 10)) % 10 + ((num1 % 10) * 10);
                }
                i++;
            }
            while (num0 != num1);
            Console.WriteLine(i);
        }
    }
}
