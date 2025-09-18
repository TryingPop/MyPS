using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 18
이름 : 배성훈
내용 : N결수
    문제번호 : 27965번

    수학, 정수론 문제다.
    Zn의 곱셈은 잘 정의되어져 있다.
    따라서 (ab + c) % k = ab % k + c % k로 찾아갔다.
*/

namespace BaekJoon.etc
{
    internal class etc_1901
    {

        static void Main1901(string[] args)
        {

            long n, k;

            Input();

            GetRet();

            void GetRet()
            {

                long[] tPow = new long[10];
                tPow[0] = 1;
                for (int i = 1; i < 10; i++)
                {

                    tPow[i] = tPow[i - 1] * 10;
                }

                long ret = 0;
                int idx = 1;

                for (int i = 1; i <= n; i++)
                {

                    if (tPow[idx] <= i) idx++;
                    ret = (ret * tPow[idx] + i);
                    ret %= k;
                }

                Console.Write(ret);
            }

            void Input()
            {

                string[] input = Console.ReadLine().Split();
                n = long.Parse(input[0]);
                k = long.Parse(input[1]);
            }
        }
    }
}
