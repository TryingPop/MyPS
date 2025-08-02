using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 10
이름 : 배성훈
내용 : 우표 구매하기 (Easy)
    문제번호 : 27715번

    조합론 문제다.
    Combination을 팩토리얼을 이용해 구현하니 91%에서 통과 못한다.
    로직을 분석하니 n >= p인 경우 n! = 0이 되어 0 x ? x ? 형태이므로 자명했다.
    반례로 p = 3일 때 7 C 3 = 35 = 2인데,
    팩토리얼 방법을 이용하니 0이된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1532
    {

        static void Main1532(string[] args)
        {

            int n, m, k, p;
            int[][] C;

            Input();

            SetC();

            GetRet();

            void GetRet()
            {

                long ret = 0;
                for (int i = 0; i <= k; i += 2)
                {

                    int two = i >> 1;
                    int one = k - i;

                    if (one < 0) break;

                    long chk = (H(n, one) * H(m, two)) % p;

                    ret = (ret + chk) % p;
                }

                Console.Write(ret);
            }

            void SetC()
            {

                int MAX = 1_000;
                int LEN = 1 + MAX << 1;
                C = new int[LEN][];
                for (int i = 0; i < C.Length; i++)
                {

                    C[i] = new int[LEN];
                }

                C[0][0] = 1;
                for (int i = 1; i < LEN; i++)
                {

                    C[i][0] = 1;
                    C[i][i] = 1;
                    for (int j = 1; j < i; j++)
                    {

                        C[i][j] = (C[i - 1][j - 1] + C[i - 1][j]) % p;
                    }
                }
            }

            long H(int _n, int _r)
            {

                if (_n == 0) return _r == 0 ? 1 : 0;

                return C[_n + _r - 1][_r];
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                k = int.Parse(temp[2]);
                p = int.Parse(temp[3]);
            }
        }
    }

#if other
using ProblemSolving.Templates.Utility;
using System;
using System.IO;
using System.Linq;
using System.Numerics;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.IO {}
namespace System.Linq {}
namespace System.Numerics {}

// #nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var (n, m, k, p) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var fac = new BigInteger[2010];
        fac[0] = 1;
        for (var v = 1; v < fac.Length; v++)
            fac[v] = fac[v - 1] * v;

        var l = new long[1010];
        var r = new long[1010];

        l[0] = 1;
        if (n != 0)
        {
            for (var idx = 0; idx < l.Length; idx++)
                l[idx] = (long)(fac[n - 1 + idx] / fac[idx] / fac[n - 1] % p);
        }

        r[0] = 1;
        if (m != 0)
        {
            for (var idx = 0; 2 * idx < r.Length; idx++)
                r[2 * idx] = (long)(fac[m - 1 + idx] / fac[idx] / fac[m - 1] % p);
        }

        // k=a+2b
        var sum = 0L;
        for (var b = k / 2; b >= 0; b--)
        {
            var a = k - 2 * b;
            sum = (sum + l[a] * r[2 * b]) % p;
        }

        sw.WriteLine(sum);
    }
}

namespace ProblemSolving.Templates.Utility
{
    public static class DeconstructHelper
    {
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
    }
}

#endif
}
