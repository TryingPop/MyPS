using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 6
이름 : 배성훈
내용 : 소수의 자격
    문제번호 : 6219번

    수학, 에라토스테네스의 체 문제다
    버스 안에서 폰으로 풀어서, 문법 오류를 캐치 못해 3번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1096
    {

        static void Main1096(string[] args)
        {

            bool[] notPrimes;
            int a, b, d;

            Input();

            SetPrime();

            GetRet();

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                a = int.Parse(temp[0]);
                b = int.Parse(temp[1]);
                d = int.Parse(temp[2]);

                notPrimes = new bool[b + 1];
                notPrimes[0] = true;
                notPrimes[1] = true;
            }

            void SetPrime()
            {

                for (int i = 2; i <= b; i++)
                {

                    if (notPrimes[i]) continue;

                    for (int j = i << 1; j <= b; j += i)
                    {

                        notPrimes[j] = true;
                    }
                }
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = a; i <= b; i++)
                {

                    if (notPrimes[i]) continue;

                    int cur = i;
                    while (cur > 0)
                    {

                        int chk = cur % 10;
                        if (chk == d)
                        {

                            ret++;
                            break;
                        }

                        cur /= 10;
                    }
                }

                Console.Write(ret);
            }
        }
    }
}
