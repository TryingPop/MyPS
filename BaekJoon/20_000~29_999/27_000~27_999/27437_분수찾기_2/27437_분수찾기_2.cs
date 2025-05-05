using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 3
이름 : 배성훈
내용 : 분수찾기 2
    문제번호 : 27437번

    수학, 이분 탐색 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1607
    {

        static void Main1607(string[] args)
        {

            long n = long.Parse(Console.ReadLine());

            // 행과 열의 합
            long rpc = BinarySearch(n);

            long dis = (rpc * (rpc + 1) >> 1) - n;
            long u, d;

            if ((rpc & 1L) == 0)
            {

                // 홀수번째 경우 끝지점은 1 / rpc다
                // 이동 방향은 ↗
                u = rpc - dis;
                d = 1 + dis;
            }
            else
            {

                // 짝수번째 경우 끝지점은 rpc / 1다.
                // 이동 방향은 ↙
                u = 1 + dis;
                d = rpc - dis;
            }

            Console.Write($"{u}/{d}");

            long BinarySearch(long _n)
            {

                _n <<= 1;

                long l = 1;
                long r = 1_000_000_000;

                while (l <= r)
                {

                    long mid = (l + r) >> 1;

                    if (mid * (mid + 1) < _n) l = mid + 1;
                    else r = mid - 1;
                }

                return r + 1;
            }
        }
    }
}
