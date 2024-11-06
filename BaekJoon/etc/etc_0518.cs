using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 13
이름 : 배성훈
내용 : 역원(Inverse) 구하기
    문제번호 : 14565번

    수학, 정수론, 확장 유클리드 호제법 문제다
    음수 처리를 안해서 곱셈 역원으로 한 번 틀렸다

    이후에는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0518
    {

        static void Main518(string[] args)
        {

            long[] arr = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

            long ret1 = arr[0] - arr[1];

            long gcd = GetMulInv(arr[0], arr[1], out long ret2);

            Console.Write($"{ret1} ");

            ret2 %= arr[0];
            ret2 = ret2 < 0 ? ret2 + arr[0] : ret2;
            if (gcd == 1) Console.Write($"{ret2}");
            else Console.Write("-1");

            long GetMulInv(long _a, long _b, out long _bInv)
            {

                long s1 = 1;
                long s2 = 0;

                long t1 = 0;
                long t2 = 1;

                long q = 0;
                while(_b > 0)
                {

                    long r = _a % _b;
                    q = (_a - r) / _b;
                    _a = _b;
                    _b = r;

                    long temp = -q * s2 + s1;
                    s1 = s2;
                    s2 = temp;

                    temp = -q * t2 + t1;
                    t1 = t2;
                    t2 = temp;
                }

                _bInv = t1;
                return _a;
            }
        }
    }
}
