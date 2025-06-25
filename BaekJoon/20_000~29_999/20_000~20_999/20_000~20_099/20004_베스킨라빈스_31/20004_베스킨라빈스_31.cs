using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 베스킨라빈스 31
    문제번호 : 20004번

    수학, 게임이론 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0278
    {

        static void Main278(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            bool[] ret = new bool[n + 1];

            for (int i = 1; i <= n; i++)
            {

                if (31 % (i + 1) == 1) ret[i] = true;
            }

            for (int i = 1; i <= n; i++)
            {

                if (ret[i]) Console.Write($"{i}\n");
            }
        }
    }
}
