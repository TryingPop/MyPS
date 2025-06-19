using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 9
이름 : 배성훈
내용 : 피타! 피타! 피타츄!
    문제번호 : 28683번

    피타고라스 정리, 브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1265
    {

        static void Main1265(string[] args)
        {

            long n = long.Parse(Console.ReadLine());

            HashSet<long> pow = new(1_000_000);
            for (long i = 1; i <= 1_000_000; i++)
            {

                pow.Add(i * i);
            }

            // 제곱수 판별
            if (pow.Contains(n))
            {

                Console.Write(-1);
                return;
            }

            int ret = 0;
            // n이 빗변
            for (long i = 1; i * i * 2 <= n; i++)
            {

                long cur = n - i * i;
                if (pow.Contains(cur)) ret++;
            }

            // n이 빗변이 아닌 경우
            for (long i = 1; i * i <= n; i++)
            {

                if (n % i != 0) continue;
                long j = n / i;
                if (j < i) break;

                // i = a - b
                // j = a + b
                long a = j + i;
                long b = j - i;

                if (a % 2 > 0 || b % 2 > 0) continue;
                ret++;
            }

            Console.Write(ret);
        }
    }
}
