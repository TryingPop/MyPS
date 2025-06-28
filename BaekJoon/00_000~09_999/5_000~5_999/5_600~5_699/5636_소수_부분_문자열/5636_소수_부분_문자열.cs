using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 8
이름 : 배성훈
내용 : 소수 부분 문자열
    문제번호 : 5636번

    문자열, 브루트포스, 에라토스테네스의 체 문제다.
    이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1320
    {

        static void Main1320(string[] args)
        {

            string ZERO = "0";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string str;
            int MAX = 100_000;
            bool[] notPrime;

            Solve();
            void Solve()
            {

                Init();

                while ((str = sr.ReadLine()) != ZERO)
                {

                    int ret = GetRet();

                    sw.Write($"{ret}\n");
                }
            }

            int GetRet()
            {

                int ret = 0;
                for (int len = 1; len < 6; len++)
                {

                    int e = str.Length - len;
                    for (int s = 0; s <= e; s++)
                    {

                        int num = GetNum(s, len);
                        if (notPrime[num]) continue;
                        ret = Math.Max(ret, num);
                    }
                }

                return ret;
            }

            int GetNum(int _s, int _len)
            {

                int ret = 0;
                int e = _s + _len;
                for (int i = _s; i < e; i++)
                {

                    ret = ret * 10 + str[i] - '0';
                }

                return ret;
            }

            void Init()
            {

                SetPrime();
            }

            void SetPrime()
            {

                notPrime = new bool[MAX + 1];
                notPrime[0] = true;
                notPrime[1] = true;

                for (int i = 2; i <= MAX; i++)
                {

                    if (notPrime[i]) continue;
                    
                    for (int j = (i << 1); j <= MAX; j += i)
                    {

                        notPrime[j] = true;
                    }
                }
            }
        }
    }
}
