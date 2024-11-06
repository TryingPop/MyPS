using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 8단계 6번 문제
 * 
 * 골드바흐의 추측
 */

namespace BaekJoon._08
{
    internal class _08_06
    {
        static void Main6(string[] args)
        {
            int len = int.Parse(Console.ReadLine());


            for (int i = 0; i < len; i++)
            {
                int num = int.Parse(Console.ReadLine());

                int min = 2;
                int max = 2;
                for (int j = (num / 2); j >= 2; j--)
                {
                    bool pchk1 = true;
                    bool pchk2 = true;
                    while (pchk1)
                    {
                        for (int k = 2; k <= ((int)Math.Sqrt(j)) + 1; k++)
                        {
                            if (j == k)
                            {
                                continue;
                            }
                            if (j % k == 0)
                            {
                                pchk1 = false;
                            }
                        }
                        break;
                    }
                    if (pchk1)
                    {
                        min = j;
                        while (pchk2)
                        {
                            for (int k = 2; k <= ((int)Math.Sqrt(num - j)) + 1; k++)
                            {
                                if ((num - j) == k)
                                {
                                    continue;
                                }
                                if ((num - j) % k == 0)
                                {
                                    pchk2 = false;
                                }
                            }
                            break;
                        }
                        if (pchk2)
                        {
                            max = (num - j);
                            break;
                        }
                    }

                }
                Console.WriteLine("{0} {1}", min, max);

            }
        }
    }
}
