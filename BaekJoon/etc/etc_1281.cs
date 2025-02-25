using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 18
이름 : 배성훈
내용 : 적어도 대부분의 배수
    문제번호 : 1145번

    브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1281
    {

        static void Main1281(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int e = 100 * 99 * 97;
            int ret = -1;
            for (int i = 1; i <= e; i++)
            {

                int cnt = 0;
                for (int j = 0; j < 5; j++)
                {

                    if (i % input[j] == 0) cnt++;
                }

                if (cnt < 3) continue;
                ret = i;
                break;
            }

            Console.Write(ret);
        }
    }
}
