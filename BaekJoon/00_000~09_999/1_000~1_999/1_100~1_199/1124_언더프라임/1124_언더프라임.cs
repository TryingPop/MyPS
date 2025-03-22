using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : 언더프라임
    문제번호 : 1124번

    수학, 에라토스테네스의 체 문제다.
    아이디어는 다음과 같다.
    먼저 소수를 모두 찾는다.
    이후 소수에 대해 에라토스테네스 체 방법처럼 
    소인수 갯수를 모두 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1273
    {

        static void Main1273(string[] args)
        {

            int A, B;

            bool[] notPrime;
            int[] div;

            Input();

            SetPrime();

            SetDiv();

            GetRet();

            void GetRet()
            {

                int ret = 0;
                for (int i = A; i <= B; i++)
                {

                    int cur = div[i];
                    if (notPrime[cur]) continue;
                    ret++;
                }

                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                A = int.Parse(temp[0]);
                B = int.Parse(temp[1]);
            }

            void SetPrime()
            {

                notPrime = new bool[B + 1];
                notPrime[1] = true;
                notPrime[0] = true;

                for (int i = 2; i <= B; i++)
                {

                    if (notPrime[i]) continue;

                    for (int j = i + i; j <= B; j += i)
                    {

                        notPrime[j] = true;
                    }
                }
            }

            void SetDiv()
            {

                div = new int[B + 1];
                for (int i = 2; i <= B; i++)
                {

                    if (notPrime[i]) continue;

                    long j = i;

                    while (j <= B)
                    {

                        for (long k = j; k <= B; k += j)
                        {

                            div[k]++;
                        }

                        j *= i;
                    }
                }
            }
        }
    }
}
