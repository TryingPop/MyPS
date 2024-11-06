using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 8단계 4번 문제
 * 
 * 소수 구하기
 */

namespace BaekJoon._08
{
    internal class _08_04
    {
        static void Main4(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(" ");

            int min = int.Parse(inputs[0]);
            int max = int.Parse(inputs[1]);

            StringBuilder sb = new StringBuilder();

            bool chk = true;

            if (min == 1)
            {
                min = 2;
            }

            for (int i = min; i <= max; i++)
            {
                int n = ((int)Math.Sqrt(i)) + 1;
                chk = true;
                for (int j = 2; j <= n; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    if (i % j == 0)
                    {
                        chk = false;
                        break;
                    }
                }

                if (chk)
                {
                    sb.AppendLine(i.ToString());
                }
            }

            Console.WriteLine(sb);
        }
    }
}
