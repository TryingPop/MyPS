using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 27
이름 : 배성훈
내용 : 조각 케이크
    문제번호 : 25212번

    브루트포스 문제다.
    값의 범위가 25 이하이고 가능한 케이크는 10개 이하이다.
    그래서 직접 분모, 분자 형태로 기약분수로 찾으면 long 범위 안에 든다.
    실제로 분모는 25, 24, 23, 19, 17, 13, 11, 7, 5를 곱한 값을 넘지 않음을 확인할 수 있다.
    해당 값을 곱하는 경우 10^15를 넘지 않는다.
*/

namespace BaekJoon.etc
{
    internal class etc_1953
    {

        static void Main1953(string[] args)
        {

            int n;
            long[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Console.Write(DFS());
                int DFS(int dep = 0, long up = 0, long down = 1)
                {

                    if (dep == n)
                        return Clamp(up, down) ? 1 : 0;

                    int ret = DFS(dep + 1, up, down);

                    long gcd = GetGCD(arr[dep], down);
                    long nDown = down * arr[dep] / gcd;
                    long nUp = up * (arr[dep]) / gcd + down / gcd;
                    ret += DFS(dep + 1, nUp, nDown);

                    return ret;
                }

                bool Clamp(long up, long down)
                {

                    long gcd = GetGCD(100, down);
                    long chkInf = 99 * down / gcd;
                    long chkSup = 101 * down / gcd;
                    up = up * 100 / gcd;
                    return chkInf <= up && up <= chkSup;
                }

                long GetGCD(long a, long b)
                {

                    while (b > 0)
                    {

                        long temp = a % b;
                        a = b;
                        b = temp;
                    }

                    return a;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = int.Parse(sr.ReadLine());
                arr = Array.ConvertAll(sr.ReadLine().Split(), long.Parse);
            }
        }
    }
}
