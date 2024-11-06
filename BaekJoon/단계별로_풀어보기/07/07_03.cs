using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 7단계 3번 문제
 * 
 * 분수찾기
 */

namespace BaekJoon._07
{
    internal class _07_03
    {
        static void Main3(string[] args)
        {
            int x = 1;
            int y = 1;
            int num = int.Parse(Console.ReadLine());
            int n = 0;
            int end = 0;

            while (((n) * (n + 1)) < 2 * num)
            {
                n++;
            }
            end = (n * (n + 1)) / 2;

            if (n % 2 == 0)
            {
                y = n;
                num -= end;
                x -= num;
                y += num;
            }
            else
            {
                x = n;
                num -= end;
                x += num;
                y -= num;
            }
            Console.WriteLine("{0}/{1}", y, x);
        }
    }
}
