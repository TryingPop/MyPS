using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.18
 * 내용 : 백준 5단계 2번 문제
 * 
 * 셀프 넘버
 */

namespace BaekJoon._05
{
    internal class _05_02
    {
        public static void d()
        {
            int[] intarr = new int[10000];
            for (int i = 0; i < intarr.Length; i++)
            {
                if (intarr[i] == 1)
                {
                    continue;
                }
                intarr[i] = 1;
                int j = i+1;
                while (true)
                {

                    int sum = j;

                    for (int k = 0; k < j.ToString().Length; k++) 
                    {
                        sum += int.Parse((j.ToString()[k]).ToString());
                    }
                    
                    if (sum > 10000) 
                    {
                        break;
                    }

                    intarr[sum-1] = 1;
                    j = sum;
                }

                Console.WriteLine(i+1);
            }
        }

        static void Main2(string[] args)
        {
            d();
        }
    }
}
