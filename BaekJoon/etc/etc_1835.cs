using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 25
이름 : 배성훈
내용 : 알고리즘 수업 - 피보나치 수 2
    문제번호 : 24417번

    코드 1의 값은 피보나치 수열, 코드 2의 값은 n - 2를 알 수 있다.
    n의 범위가 2억이라 분할정복을 이용한 거듭제곱으로 피보나치 수를 구했다.
    다른 사람 코드를 보니 그냥 for문을 이용해 구할 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1835
    {

        static void Main1835(string[] args)
        {

            int MOD = 1_000_000_007;
            int n = int.Parse(Console.ReadLine());

            Console.Write($"{GetPow(n)} {n - 2}");

            long GetPow(int _exp)
            {

                // 항등원
                (long x11, long x12, long x21, long x22) ret = (1, 0, 0, 1), a = (1, 1, 1, 0);


                while (_exp > 0)
                {

                    if ((_exp & 1) == 1) MatMul(ref ret, a);
                    _exp >>= 1;
                    MatMul(ref a, a);
                }

                return ret.x21;
            }

            void MatMul(ref (long x11, long x12, long x21, long x22) _a, (long x11, long x12, long x21, long x22) _b)
            {

                long x11 = ((_a.x11 * _b.x11) + (_a.x12 * _b.x21)) % MOD;
                long x12 = ((_a.x11 * _b.x12) + (_a.x12 * _b.x22)) % MOD;
                long x21 = ((_a.x21 * _b.x11) + (_a.x22 * _b.x21)) % MOD;
                long x22 = ((_a.x21 * _b.x12) + (_a.x22 * _b.x22)) % MOD;

                _a = (x11, x12, x21, x22);
            }
        }
    }
}
