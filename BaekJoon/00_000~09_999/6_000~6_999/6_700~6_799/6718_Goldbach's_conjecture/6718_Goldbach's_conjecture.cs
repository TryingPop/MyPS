using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 9
이름 : 배성훈
내용 : Goldbach’s conjecture
    문제번호 : 6718번

    수학, 브루트포스, 에라토스테네스의 체 문제다
    모든 소수를 찾고, a + b = n에 대해
    a, b 모두 소수인지 확인한 뒤 카운트해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_1038
    {

        static void Main1038(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            bool[] primes;
            int n;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    sw.Write($"{GetRet()}\n");
                }

                sr.Close();
                sw.Close();
            }

            bool Input()
            {

                n = ReadInt();

                return n != 0;
            }

            int GetRet()
            {

                int ret = 0;
                int e = n >> 1;
                for (int i = 2; i <= e; i++)
                {

                    if (primes[i] && primes[n - i]) ret++;
                }

                return ret;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                Eratosthenes();
            }

            void Eratosthenes()
            {

                primes = new bool[(1 << 15) + 1];
                Array.Fill(primes, true);

                for (int i = 2; i < primes.Length; i++)
                {

                    if (!primes[i]) continue;

                    for (int j = i << 1; j < primes.Length; j += i)
                    {

                        primes[j] = false;
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
