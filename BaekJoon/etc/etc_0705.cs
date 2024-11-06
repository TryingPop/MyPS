using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 18
이름 : 배성훈
내용 : GCD(n, k) = 1
    문제번호 : 11689번

    오일러 피 함수 문제다
    오일러 피 함수의 값은 서로 다른 약수의 개수와 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0705
    {

        static void Main705(string[] args)
        {

            long n = Convert.ToInt64(Console.ReadLine());

            long ret = 1;

            for (long i = 2; i <= 1_000_000; i++)
            {

                if (i * i > n) break;
                if (n % i != 0) continue;
                ret *= i - 1;
                n /= i;
                while(n % i == 0)
                {

                    ret *= i;
                    n /= i;
                }
            }

            if (n > 1) ret *= n - 1;
            Console.WriteLine(ret);
        }
    }

#if other
using System;
using System.IO;

namespace baekjoon.ECT
{
    internal class Practice_11689
    {
        public static void Main()
        {
            var sr = new StreamReader(Console.OpenStandardInput());
            var sw = new StreamWriter(Console.OpenStandardOutput());

            long n = long.Parse(sr.ReadLine());
            long result = n;

            for (long i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    result -= result / i;
                    while (n % i == 0)
                    {
                        n /= i;
                    }
                }
            }

            if (1 < n)
            {
                result -= result / n;
            }

            sw.Write(result);

            sr.Close();
            sw.Close();
        }
    }
}
#endif
}
