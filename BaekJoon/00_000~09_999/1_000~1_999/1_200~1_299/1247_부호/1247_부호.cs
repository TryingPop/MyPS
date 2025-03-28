using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 부호
    문제번호 : 1247번

    사칙연산, 큰 수 연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1483
    {

        static void Main1483(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            long[] plus = new long[3], minus = new long[3];
            long MOD = 1_000_000_000L;

            for (int i = 0; i < 3; i++)
            {

                int n = int.Parse(sr.ReadLine());

                Array.Fill(plus, 0);
                Array.Fill(minus, 0);

                for (int j = 0; j < n; j++)
                {

                    long cur = long.Parse(sr.ReadLine());
                    if (cur == 0) continue;
                    else if (cur > 0)
                    {

                        plus[0] += cur % MOD;
                        plus[1] += cur / MOD;
                    }
                    else
                    {

                        cur = -cur;
                        minus[0] += cur % MOD;
                        minus[1] += cur / MOD;
                    }
                }

                long add = plus[0] / MOD;
                plus[1] += add;
                plus[0] %= MOD;
                add = plus[1] / MOD;
                plus[1] %= MOD;
                plus[2] += add;

                add = minus[0] / MOD;
                minus[1] += add;
                minus[0] %= MOD;
                add = minus[1] / MOD;
                minus[1] %= MOD;
                minus[2] += add;

                int ret = Comp();
                if (ret == 0) sw.Write("0\n");
                else if (ret > 0) sw.Write("+\n");
                else sw.Write("-\n");
            }

            int Comp()
            {

                int ret = plus[2].CompareTo(minus[2]);
                if (ret == 0) ret = plus[1].CompareTo(minus[1]);
                if (ret == 0) ret = plus[0].CompareTo(minus[0]);
                return ret;
            }
        }
    }
}
