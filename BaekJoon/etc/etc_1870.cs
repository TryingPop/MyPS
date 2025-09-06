using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 6
이름 : 배성훈
내용 : 캐슬 디펜스
    문제번호 : 24893번

    누적 합 문제다.
    아이디어는 다음과 같다.
    쏘는 간격을 보면 1, 2, 3, ..., n - 1, 10억이다.
    그래서 쏘는 간격에 따라 고용할 최소 인원을 찾아 풀어주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1870
    {

        static void Main1870(string[] args)
        {

            int n, e;
            long a, b;
            long[] arr;

            Input();

            GetRet();


            void GetRet()
            {

                if (arr[n] < e)
                {

                    // 안쏘는 경우
                    Console.Write(b * -1_000_000_000);
                    return;
                }

                // t 는 텀
                long ret = long.MaxValue;

                for (int term = 1; term < n; term++)
                {

                    long k = 0;
                    for (int time = term; time < n; time += term)
                    {

                        // 제거할 적
                        k = Math.Max(k, FindMinK(arr[time], time / term));
                    }

                    // n일 때 확인
                    k = Math.Max(k, FindMinK(arr[n], 1 + ((n - 1) / term)));

                    ret = Math.Min(ret, k * a - term * b);
                }

                {

                    // 1번만 쏘는 경우 생각!
                    long term = 1_000_000_000;
                    long k = arr[n] - e + 1;
                    ret = Math.Min(ret, k * a - term * b);
                }

                Console.Write(ret);

                long FindMinK(long totEnemy, long shot)
                {

                    long killEnemy = totEnemy - e + 1;
                    if (killEnemy <= 0) return 0;
                    killEnemy--;
                    return 1 + killEnemy / shot;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                a = ReadInt();
                b = ReadInt();
                e = ReadInt();

                arr = new long[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt() + arr[i - 1];
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
