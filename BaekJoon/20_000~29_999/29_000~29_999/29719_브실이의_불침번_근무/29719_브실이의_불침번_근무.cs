using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 27
이름 : 배성훈
내용 : 브실이의 불침번 근무
    문제번호 : 29719번

    수학, 조합론 문제다.
    브실이가 서는 경우는 전체 경우 - 브실이가 서지 않는 경우로
    즉, M^N - (M - 1)^N 이된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1133
    {

        static void Main1133(string[] args)
        {

            int MOD = 1_000_000_007;
            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();

            long ret = GetPow(input[1], input[0]) - GetPow(input[1] - 1, input[0]);
            Console.Write(ret < 0 ? ret + MOD : ret);

            long GetPow(long _a, int _exp)
            {

                if (_a <= 1) return _a;

                long ret = 1L;
                while (_exp > 0)
                {

                    if ((_exp & 1) == 1) ret = (ret * _a) % MOD;

                    _a = (_a * _a) % MOD;
                    _exp >>= 1;
                }

                return ret;
            }
        }
    }
}
