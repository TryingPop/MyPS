using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 24
이름 : 배성훈
내용 : K번째 소수
    문제번호 : 15965번

    수학, 정수론, 에라토스테네스 체 이론 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0834
    {

        static void Main834(string[] args)
        {

            int[] arr;

            Solve();
            void Solve()
            {

                SetPrime();

                Console.Write(arr[int.Parse(Console.ReadLine())]);
            }

            void SetPrime()
            {

                int n = 0;

                arr = new int[500_001];
                bool[] notPrime = new bool[10_000_001];
                for (int i = 2; i <= 10_000_000; i++)
                {

                    if (notPrime[i]) continue;
                    n++;
                    arr[n] = i;
                    if (n == 500_000) break;

                    for (int j = i << 1; j <= 10_000_000; j += i)
                    {

                        notPrime[j] = true;
                    }
                }
            }
        }
    }
}
