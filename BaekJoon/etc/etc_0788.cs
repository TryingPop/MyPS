using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 3
이름 : 배성훈
내용 : 수 분해
    문제번호 : 1437번

    수학, 그리디 문제다
    etc_0787과 같은 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0788
    {

        static void Main788(string[] args)
        {

            int MOD = 10_007;

            Solve();
            void Solve()
            {

                int n = int.Parse(Console.ReadLine());

                int ret = GetRet(n);

                Console.Write(ret % MOD);
            }

            int GetRet(int _n)
            {

                if (_n <= 1) return _n;

                int three, two;
                three = _n / 3;
                int r = _n % 3;
                if (r == 0) two = 0;
                else if (r == 2) two = 1;
                else
                {

                    two = 2;
                    three--;
                }

                int ret = (GetPow(2, two) * GetPow(3, three)) % MOD;
                return ret;
            }

            int GetPow(int _a, int _exp)
            {

                int ret = 1;
                while(_exp > 0)
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
