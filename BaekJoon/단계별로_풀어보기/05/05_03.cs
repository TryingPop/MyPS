using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.18
 * 내용 : 백준 5단계 3번 문제
 * 
 * 한수
 */

namespace BaekJoon._05
{
    internal class _05_03
    {
        public static int FindSeries(int num)
        {
            if (num < 100)
            {
                return num;
            }
            else
            {
                for (int i = 0; i < num.ToString().Length - 2; i++)
                {
                    if ((num.ToString()[i] - num.ToString()[i + 1]) != (num.ToString()[i + 1] - num.ToString()[i + 2]))
                    {
                        return FindSeries(num - 1);
                    }
                }

                return 1 + FindSeries(num - 1);
            }
        }

        static void Main3(string[] args)
        {
            int input = int.Parse(Console.ReadLine());
            int num = 0;
            num = FindSeries(input);
            Console.WriteLine(num);
        }
    }
}
