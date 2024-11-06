using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 1
이름 : 배성훈
내용 : Work or Sleep!
    문제번호 : 24853번

    수학, 삼분탐색 문제다
    미분써서 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0785
    {

        static void Main785(string[] args)
        {

            decimal[] input;
            Solve();

            void Solve()
            {

                input = Array.ConvertAll(Console.ReadLine().Split(), decimal.Parse);
                decimal ret;
                if (input[0] == 100)
                {

                    ret = Func1(input[1]);
                    Console.Write($"{ret:0.00000000}");
                    return;
                }


                if (input[0] == 0)
                {

                    ret = Func2(input[1]);
                    Console.Write($"{ret:0.00000000}");
                    return;
                }


                decimal chk1 = Func1(input[1]);

                decimal chk2 = Func2(input[1]);
                ret = Math.Max(chk1, chk2);

                if (ChkMax(out decimal chk3)) ret = Math.Max(ret, Func2(chk3));
                Console.Write($"{ret:0.00000000}");
            }

            decimal Func1(decimal _x)
            {

                if (_x > (input[1] / 6)) _x = (input[1] / 6);
                decimal e = _x * input[0] * (6 / input[1]);

                decimal ret = e * (input[1] - _x);
                return ret;
            }

            decimal Func2(decimal _x)
            {

                if (_x > (input[1] / 3)) _x = (input[1] / 3);
                else if (_x < (input[1] / 6)) _x = (input[1] / 6);

                decimal e = _x * ((600 - 6 * input[0]) / input[1]) + 2 * input[0] - 100;

                decimal ret = e * (input[1] - _x);
                return ret;
            }

            bool ChkMax(out decimal _x)
            {

                _x = input[1] * (700 - 8 * input[0]);
                _x = _x / (1200 - 12 * input[0]);

                return (input[1] / 6) <= _x && _x <= (input[1] / 3);
            }
        }
    }

#if other
// #include <stdio.h>

int main(void) {
    double p, t, lo, hi, mid1, mid2, eff1, eff2;
    scanf("%lf %lf", &p, &t);
    lo = 0, hi = t / 3.0;
    for (int i = 0; i < 401; i++) {
        mid1 = (lo * 2 + hi) / 3;
        mid2 = (lo + hi * 2) / 3;
        if (mid1 < t / 6.0) eff1 = p * (mid1 / (t / 6.0)) / 100.0;
        else eff1 = (p + (mid1 - t / 6.0) / (t / 6.0) * (100.0 - p)) / 100.0;
        if (mid2 < t / 6.0) eff2 = p * (mid2 / (t / 6.0)) / 100.0;
        else eff2 = (p + (mid2 - t / 6.0) / (t / 6.0) * (100.0 - p)) / 100.0;

        if ((t - mid1) * eff1 < (t - mid2) * eff2) lo = mid1;
        else hi = mid2;
    }
    printf("%.9f", (t - mid1) * eff1 * 100.0);
    return 0;
}
#elif other2
#endif
}
