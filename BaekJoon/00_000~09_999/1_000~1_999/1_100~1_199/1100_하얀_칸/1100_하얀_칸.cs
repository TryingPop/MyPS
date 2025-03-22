using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 18
이름 : 배성훈
내용 : 하얀 칸
    문제번호 : 1100번

    구현, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1282
    {

        static void Main1282(string[] args)
        {

            int ret = 0;
            for (int i = 0; i < 8; i++)
            {

                string input = Console.ReadLine();
                int s = (i & 1);
                for (int j = s; j < 8; j += 2)
                {

                    if (input[j] == 'F') ret++;
                }
            }

            Console.Write(ret);
        }
    }
}
