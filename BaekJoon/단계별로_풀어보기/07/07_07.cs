using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.20
 * 내용 : 백준 7단계 7번 문제
 * 
 * 설탕 배달
 */

namespace BaekJoon._07
{
    internal class _07_07
    {
        static void Main7(string[] args)
        {
            int inputs = int.Parse(Console.ReadLine());
            int assi;
            int result = 0;
            int[] chkarr = { 2, 3, 2, 3, 4, 3, 4, 3, 4, 5, 4, 5, 4, 5, 6 };

            if (inputs <= 7)
            {
                if (inputs == 3 || inputs == 5)
                {
                    result = 1;
                }
                else if (inputs == 6) 
                {
                    result = 2;
                }
                else
                {
                    result = -1;
                }
            }
            else
            {
                inputs -= 8;
                assi = inputs / 15;
                inputs %= 15;

                result = (3 * assi) + (chkarr[inputs]);
            }

            Console.WriteLine(result);
        }
    }
}