using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 23
이름 : 배성훈
내용 : 오민식
    문제번호 : 1630번

    에라토스테네스 체 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1725
    {

        static void Main1725(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            bool[] notPrime;

            SetPrime();

            GetRet();

            void GetRet()
            {

                long MOD = 987_654_321;
                long ret = 1;

                for (int i = 2; i <= n; i++)
                {

                    if (notPrime[i]) continue;

                    ChkPow(n, i);
                }

                Console.Write(ret);

                void ChkPow(int _n, int _div)
                {

                    while (_n >= _div)
                    {

                        ret = (ret * _div) % MOD;
                        _n /= _div;
                    }
                }
            }

            void SetPrime()
            {

                notPrime = new bool[n + 1];
                notPrime[0] = true;
                notPrime[1] = true;

                for (int i = 2; i <= n; i++)
                {

                    if (notPrime[i]) continue;

                    for (int j = i << 1; j <= n; j += i) 
                    {

                        notPrime[j] = true;
                    }
                }
            }
        }
    }
}
