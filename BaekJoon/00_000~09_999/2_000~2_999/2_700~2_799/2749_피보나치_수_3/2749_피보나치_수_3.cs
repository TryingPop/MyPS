using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 11
이름 : 배성훈
내용 : 피보나치 수 3
    문제번호 : 2749번

    분할 정복을 이용한 거듭제곱 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1394
    {

        static void Main1394(string[] args)
        {

            long n = long.Parse(Console.ReadLine());
            Console.Write(GetFibo(n));

            long GetFibo(long _exp)
            {

                long MOD = 1_000_000;
                (long a11, long a12, long a21, long a22) a = (1, 1, 1, 0), ret = (1, 0, 0, 1);

                while (_exp > 0)
                {

                    if ((_exp & 1L) == 1L) MatMul(ref ret, ref a);

                    MatMul(ref a, ref a);
                    _exp >>= 1;
                }

                return ret.a21;

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
}
