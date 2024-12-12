using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 12
이름 : 배성훈
내용 : 이항 계수와 쿼리
    문제번호 : 13977번

    수학, 정수론, 분할 정복을 이용한 거듭제곰 문제다.
    factorial을 먼저 찾고 조합을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1180
    {

        static void Main1180(string[] args)
        {

            int MOD = 1_000_000_007;
            int MAX = 4_000_000;
            StreamReader sr;
            StreamWriter sw;

            long[] fac;
            int n, k;
            int len = 0;
            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();
                while(t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sw.Close();
                sr.Close();
            }

            void GetRet()
            {

                if (len < n)
                {

                    for (int i = len + 1; i <= n; i++)
                    {

                        fac[i] = (fac[i - 1] * i) % MOD;
                    }
                    len = n;
                }

                long a = fac[n];
                long b = GetPow((fac[k] * fac[n - k]) % MOD, MOD - 2);

                sw.Write($"{(a * b) % MOD}\n");

                long GetPow(long _a, int _exp)
                {

                    long ret = 1;

                    while(_exp > 0)
                    {

                        if ((_exp & 1) == 1) ret = (ret * _a) % MOD;
                        _a = (_a * _a) % MOD;
                        _exp >>= 1;
                    }

                    return ret;
                }
            }

            void Input()
            {

                n = ReadInt();
                k = ReadInt();
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                fac = new long[MAX + 1];

                Array.Fill(fac, -1);
                fac[0] = 1;
            }

        }
    }

#if other
using System;
using System.IO;

class Program
{
    const int p = 1000000007;

    static long Pow(long a, int n)
    {
        long res = 1;
        while (n != 0)
        {
            if ((n & 1) != 0)
                res = (res * a) % p;
            a = (a * a) % p;
            n >>= 1;
        }
        return res;
    }

    static void Main()
    {
        var factorials = new long[4000001];
        factorials[0] = 1;
        for (int i = 1; i < factorials.Length; i++)
            factorials[i] = (factorials[i - 1] * i) % p;

        var reader = Console.In;
        var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

        _ = int.TryParse(reader.ReadLine(), out int m);
        for (int i = 0; i < m; i++)
        {
            string[] str = reader.ReadLine().Split(' ');
            _ = int.TryParse(str[0], out var n);
            _ = int.TryParse(str[1], out var k);

            writer.WriteLine((factorials[n] * Pow(((factorials[k] * factorials[n - k]) % p), p - 2)) % p);
        }

        writer.Dispose();
    }
}

#elif other2
using System;
using System.IO;
using System.Linq;

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var mod = 1_000_000_007L;
        var fac = new long[4_000_001];

        fac[0] = 1;
        for (var v = 1; v < fac.Length; v++)
            fac[v] = (fac[v - 1] * v) % mod;

        var facinv = new long[4_000_001];
        facinv[facinv.Length - 1] = FastPow(fac[fac.Length - 1], mod - 2, mod);

        for (var v = facinv.Length - 2; v >= 0; v--)
            facinv[v] = (facinv[v + 1] * (v + 1)) % mod;

        var t = Int32.Parse(sr.ReadLine());
        while (t-- > 0)
        {
            var nk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nk[0];
            var k = nk[1];

            var comb = fac[n] * facinv[k];
            comb %= mod;
            comb *= facinv[n - k];
            comb %= mod;

            sw.WriteLine(comb);
        }
    }

    private static long FastPow(long a, long p, long mod)
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
