using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 8단계 5번 문제
 * 
 * 베르트랑 공준
 */

namespace BaekJoon._08
{
    internal class _08_05
    {
        static void Main5(string[] args)
        {
            while (true)
            {
                int min = int.Parse(Console.ReadLine());
                if (min == 0)
                {
                    break;
                }

                int max = min * 2;

                bool chk = true;
                int result = 0;
                for (int i = min + 1; i <= max; i++)
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
                        result++;
                    }
                }
                Console.WriteLine(result);
            }
        }
    }
}
