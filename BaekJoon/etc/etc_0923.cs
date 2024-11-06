using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 29
이름 : 배성훈
내용 : 원숭이 타워
    문제번호 : 1607번

    수학, dp 문제다
    etc_0922와 같은 문제다
    다만 범위가 크고 모드 연산이 필요하다

    1개만 찾기에 dp를 안써서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0923
    {

        static void Main923(string[] args)
        {

            int MOD = 9901;
            double ERR = 1e-9;

            Solve();
            void Solve()
            {

                int n = int.Parse(Console.ReadLine());

                int ret = GetRet(n);

                Console.Write(ret);
            }

            int GetPow(int _a, int _exp)
            {

                int ret = 1;
                while (_exp > 0)
                {

                    if ((_exp & 1) == 1) ret = (ret * _a) % MOD;

                    _a = (_a * _a) % MOD;
                    _exp >>= 1;
                }

                return ret;
            }

            int GetRet(int _n)
            {

                if (_n <= 1) return _n;
                int k = _n + 1 - (int)(Math.Round(Math.Sqrt(2 * _n + 1) + ERR));

                return (2 * GetRet(k) + GetPow(2, _n - k) - 1) % MOD;
            }
        }
    }
}
