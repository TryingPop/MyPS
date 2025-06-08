using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 5.
이름 : 배성훈
내용 : 조합 (Combination)
    문제번호 : 16134번

    조합을 구하는 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1152
    {

        static void Main1152(string[] args)
        {

            int MOD = 1_000_000_007;
            int n, k;

            Solve();
            void Solve()
            {

                Input();

                Console.Write(Comb());
            }

            long Comb()
            {

                int a = n;
                int b = k;
                int c = n - k;

                if (b < c)
                {

                    int temp = c;
                    c = b;
                    b = temp;
                }

                long fac = 1;
                for (int i = 2; i <= c; i++)
                {

                    fac = (fac * i) % MOD;
                }

                long chk1 = fac;
                for (int i = c + 1; i <= b; i++)
                {

                    fac = (fac * i) % MOD;
                }

                long chk2 = fac;

                for (int i = b + 1; i <= a; i++)
                {

                    fac = (fac * i) % MOD;
                }

                long ret = fac;

                chk1 = GetPow(chk1, MOD - 2);
                chk2 = GetPow(chk2, MOD - 2);

                long chk = (chk1 * chk2) % MOD;
                return (ret * chk) % MOD;

                long GetPow(long _a, long _exp)
                {

                    long ret = 1;
                    while(_exp > 0)
                    {

                        if ((_exp & 1L) == 1L) ret = (ret * _a) % MOD;
                        _a = (_a * _a) % MOD;
                        _exp >>= 1;
                    }

                    return ret;
                }
            }

            void Input()
            {

                string[] input = Console.ReadLine().Split();
                n = int.Parse(input[0]);
                k = int.Parse(input[1]);
            }
        }
    }

#if other
using System;
using System.IO;
using System.Linq;

// #nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var mod = 1_000_000_007L;

        var fac = new long[2_000_001];
        fac[0] = 1;
        for (var idx = 1; idx < fac.Length; idx++)
            fac[idx] = (idx * fac[idx - 1]) % mod;

        var nr = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nr[0];
        var r = nr[1];

        var comb = fac[n] * FastPow(fac[n - r], mod - 2, mod);
        comb %= mod;

        comb *= FastPow(fac[r], mod - 2, mod);
        comb %= mod;

        sw.WriteLine(comb);
    }

    public static long FastPow(long a, long p, long mod)
    {
        var rv = 1L;

        while (p > 0)
        {
            if (p % 2 == 1)
                rv = (rv * a) % mod;

            a = (a * a) % mod;
            p /= 2;
        }

        return rv;
    }
}

#endif
}
