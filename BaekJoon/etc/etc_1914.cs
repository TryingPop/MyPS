using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 24
이름 : 배성훈
내용 : 피보나치 수 7
    문제번호 : 15624번

    dp 문제다.
    피보나치 점화식을 이용해 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1914
    {

        static void Main1914(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            if (n <= 1)
            {

                Console.Write(n);
                return;
            }

            int MOD = 1_000_000_007;
            int cur = 1;
            int prev = 0;
            for (int i = 2; i <= n; i++)
            {

                int temp = (cur + prev) % MOD;
                prev = cur;
                cur = temp;
            }

            Console.Write(cur);
        }
    }
}
