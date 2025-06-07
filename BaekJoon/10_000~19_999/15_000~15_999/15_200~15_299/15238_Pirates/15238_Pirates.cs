using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 24
이름 : 배성훈
내용 : Pirates
    문제번호 : 15238번

    구현, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1293
    {

        static void Main1293(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            string str = Console.ReadLine();
            int[] cnt = new int[128];

            for (int i = 0; i < n; i++)
            {

                cnt[str[i]]++;
            }

            int max = 0;
            for (int i = 0; i < cnt.Length; i++)
            {

                max = Math.Max(cnt[i], max);
            }

            for (int i = 0; i < cnt.Length; i++)
            {

                if (max != cnt[i]) continue;
                Console.Write($"{(char)i} {max}");
            }
        }
    }
}
