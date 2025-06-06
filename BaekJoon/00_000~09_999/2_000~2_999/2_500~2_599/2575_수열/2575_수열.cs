using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 6
이름 : 배성훈
내용 : 수열
    문제번호 : 2575번

    수학 문제다.
    덧셈은 최대한 3으로 나타내는게 좋다.
    log 3 > (3/2) * log 2다!
    그래서 3을 최대한 쓰는게 좋다.

    이외의 경우는 log m <= (m/n) * log n이다.

    곱셈 부분은 최대 길이를 찾아서 여러 번 틀렸다.
    문제를 다시 읽어보니 최단 길이이다.

    합이 최소가 되는 것은 소인수들로 하는게 좋은데 2는 최대한 4로 만들면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1681
    {

        static void Main1681(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            Console.Write($"{GetPlus()} {GetMul()}");

            int GetMul()
            {

                int ret = 0;
                for (int i = 3; i * i <= n; i++)
                {

                    while (n % i == 0)
                    {

                        ret++;
                        n /= i;
                    }
                }

                while (n % 4 == 0)
                {

                    ret++;
                    n /= 4;
                }

                if (n % 2 == 0)
                {

                    ret++;
                    n /= 2;
                }

                if (n > 1 || ret == 0) ret++;
                return ret;
            }

            int GetPlus()
            {

                int ret = n / 3;
                if (n % 3 > 0) ret++;

                return ret;
            }
        }
    }
}
