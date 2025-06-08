using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 30
이름 : 배성훈
내용 : 피보나치 수의 제곱의 합
    문제번호 : 11440번

    수학 문제다.
    피보나치 수의 제곱합을 보면,
    0, 1, 2, 6, 15, 40, 104, ...이다.
    그리고 수를 보면
    0 = 0 x 1,
    1 = 1 x 1,
    2 = 1 x 2,
    6 = 2 x 3,
    15 = 3 x 5,
    40 = 5 x 8,
    104 = 8 x 13,
    ...
    이다.

    이로 i번째 피보나치 수를 Fi, 
    0부터 i까지 피보나치 제곱수의 합 Sn이라하면
    Sn = Fn x Fn+1 점화식을 추론했다.
    이후 수학적 귀납법으로 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1500
    {

        static void Main1500(string[] args)
        {

            long MOD = 1_000_000_007;
            long n = long.Parse(Console.ReadLine());

            Console.Write((Fibo(n) * Fibo(n + 1)) % MOD);

            long Fibo(long _n)
            {

                (long a11, long a12, long a21, long a22) ret = (1, 0, 0, 1), a = (1, 1, 1, 0);

                while (_n > 0)
                {

                    if ((_n & 1L) == 1L) MatMul(ref ret, ref a);

                    _n >>= 1;
                    MatMul(ref a, ref a);
                }

                return ret.a21;
            }

            void MatMul(ref (long a11, long a12, long a21, long a22) _a, ref (long a11, long a12, long a21, long a22) _b)
            {

                long a11 = (_a.a11 * _b.a11 + _a.a12 * _b.a21) % MOD;
                long a12 = (_a.a11 * _b.a12 + _a.a12 * _b.a22) % MOD;
                long a21 = (_a.a21 * _b.a11 + _a.a22 * _b.a21) % MOD;
                long a22 = (_a.a21 * _b.a12 + _a.a22 * _b.a22) % MOD;

                _a = (a11, a12, a21, a22);
            }
        }
    }
}
