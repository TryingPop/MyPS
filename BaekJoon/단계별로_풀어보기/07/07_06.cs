using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.20
 * 내용 : 백준 7단계 6번 문제
 * 
 * 부녀회장이 될테야
 */

namespace BaekJoon._07
{
    internal class _07_06
    {
        static void Main6(string[] args)
        {
            int length = int.Parse(Console.ReadLine());
            int width;
            int height;

            for (int i = 0; i < length; i++)
            {
                height = -1 + int.Parse(Console.ReadLine());
                width = int.Parse(Console.ReadLine());

                int[] chkarr = new int[width];
                for (int j = 0; j < width; j++)
                {
                    chkarr[j] = 1;
                }

                while (height > 0)
                {
                    for (int j = 0; j < width - 1; j++)
                    {
                        chkarr[j] = chkarr[j..].Sum();
                    }
                    height--;
                }

                for (int l = 0; l < width; l++)
                {
                    chkarr[l] *= (l + 1);
                }

                Console.WriteLine(chkarr.Sum());
            }
        }
    }
}