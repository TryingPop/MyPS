using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 27
이름 : 배성훈
내용 : 등비수열의 합
    문제번호 : 23848번

    수학, 브루트포스 알고리즘
    아이디어는 다음과 같다.
    등비수열의 합은 1 + r + r^2 + ... + r^k로 나눠 떨어지는지 확인했다.
    결과가 10^12이고 길이가 3 이상이므로 등비는 100만을 넘지 못한다.
    그리고 등비는 2이상이므로 길이합은 길어야 40이다

    그래서 pow를 구하면서 시뮬레이션 돌려 찾아갔다.
*/

namespace BaekJoon.etc
{
    internal class etc_1220
    {

        static void Main1220(string[] args)
        {

            long INF = 1_000_000_000_000;
            int E = 1_000_000;
            long n;
            long[] pow, sum;
            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                int e = E;
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int len = 3; len < 40; len++)
                {

                    for (int i = 2; i < e; i++)
                    {

                        pow[i] *= i;
                        if (pow[i] > INF)
                        {

                            e = i;
                            break;
                        }

                        sum[i] += pow[i];
                        if (n % sum[i] != 0) continue;
                        long a = n / sum[i];

                        sw.Write($"{len}\n");
                        long p = a * i;
                        for (int j = 0; j < len; j++)
                        {

                            sw.Write($"{p} ");
                            p *= i;
                        }

                        return;
                    }
                }

                sw.Write(-1);
            }

            void Init()
            {

                n = long.Parse(Console.ReadLine());

                pow = new long[E];
                sum = new long[E];

                for (int i = 2; i < E; i++)
                {

                    pow[i] = i;
                    sum[i] = 1 + i;
                }
            }
        }
    }
}
