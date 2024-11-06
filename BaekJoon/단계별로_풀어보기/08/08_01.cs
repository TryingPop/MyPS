using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.20
 * 내용 : 백준 8단계 1번 문제
 * 
 * 소수 찾기
 */

namespace BaekJoon._08
{
    internal class _08_01
    {
        static void Main2(string[] args)
        {
            int min = int.Parse(Console.ReadLine());
            int max = int.Parse(Console.ReadLine());


            int result1 = 0;
            int result2 = 0;

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
                    result1 += i;
                    if (result2 > i || result2 == 0)
                    {
                        result2 = i;
                    }
                }
            }
            if (result1 == 0)
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(result1);
                Console.WriteLine(result2);
            }
        }
    }
}
