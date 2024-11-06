using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.15
 * 내용 : 백준 4단계 2번 문제
 * 
 * 최댓값
 */

namespace BaekJoon._03
{
    internal class _04_02
    {
        static void Main2(string[] args)
        {
            int max = 0;
            int j = -1;
            for (int i = 0; i < 9; i++)
            {
                int num = int.Parse(Console.ReadLine());
                if (i == 0 || num > max)
                {
                    max = num;
                    j = i + 1;
                }
            }
            Console.WriteLine(max);
            Console.WriteLine(j);
        }
    }
}
