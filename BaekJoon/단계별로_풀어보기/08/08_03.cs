using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 8단계 3번 문제
 * 
 * 소인수분해
 */

namespace BaekJoon._08
{
    internal class _08_03
    {
        static void Main3(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            int input = int.Parse(Console.ReadLine());

            FindNum(input, sb);
            Console.WriteLine(sb);

        }

        static void FindNum(int num, StringBuilder sb)
        {
            for (int i = 2; i <= num; i++)
            {
                if (num == 1)
                {
                    break;
                }

                if (i > ((int)Math.Sqrt(num)) + 1)
                {
                    sb.AppendLine(num.ToString());
                    break;
                }

                if (num % i == 0)
                {
                    num /= i;
                    sb.AppendLine(i.ToString());
                    FindNum(num, sb);
                    break;
                }
            }
        }
    }
}
