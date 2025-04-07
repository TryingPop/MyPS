using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 5
이름 : 배성훈
내용 : 음식 평론가
    문제번호 : 1188번

    유클리드 호제법 문제다.
    아이디어는 다음과 같다.
    n 과 m이 서로 소인 경우 m - 1,
    n과 m이 서로 소가 아닌 경우 gcd를 찾고
    서로 소인 경우로 해결하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1522
    {

        static void Main1522(string[] args)
        {

            int n, m;

            Input();

            GetRet();

            void GetRet()
            {

                n %= m;

                int ret;
                if (n == 0)
                {

                    // 나눠떨어지는 경우 gcd(0, m) = m이 자명하므로 
                    // 반례처리
                    ret = 0;
                }
                else
                {

                    int gcd = GetGCD(n, m);
                    m /= gcd;
                    ret = (m - 1) * gcd;
                }

                Console.Write(ret);

                int GetGCD(int _f, int _t)
                {

                    while (_t > 0)
                    {

                        int temp = _f % _t;
                        _f = _t;
                        _t = temp;
                    }

                    return _f;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
            }
        }
    }
}
