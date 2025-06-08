using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 6
이름 : 배성훈
내용 : 피보나치 수와 최대공약수
    문제번호 : 11778번

    수학, 정수론 문제다
    n번째 피보나치 수를 F(n)이라 하면
    gcd(F(n), F(m)) = F(gcd(n, m))이 되는 규칙을 확인했고
    해당 방법을 제출하니 이상없이 통과했다

        1. gcd(n, m) = gcd(n, m += n)
        2. gcd(F(n), F(n + 1)) = 1
        3. gcd(n, m) = 1 => gcd(n, mk) = gcd(n, k)
        4. F(n + m) = F(m)F(n + 1) + F(m - 1)F(n)
    으로 증명 가능하다

    gcd(F(n), F(m)) = gcd(F(n), F(nk + 1)F(r) + F(nk)F(r - 1))
    (r은 r = n - mq인 0과 m사이의 정수, 4에의해 성립)
    = gcd(F(n), F(nk + 1)F(r))
    (F(m) | F(mk) 가 성립하기 때문에)
    = gcd(F(m), F(r))
    (3에 의해 성립)

    이는 유클리드 호제법과 같고 계속 진행하면 원하는 결과를 얻을 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_1032
    {

        static void Main1032(string[] args)
        {

            long MOD = 1_000_000_007;
            long a, b;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long idx = GetGCD(a, b);
                long ret = GetFibo(idx);

                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                a = long.Parse(temp[0]);
                b = long.Parse(temp[1]);
            }

            long GetGCD(long _a, long _b)
            {

                while (_b > 0)
                {

                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            long GetFibo(long _idx)
            {

                (long x11, long x12, long x21, long x22) ret = (1, 0, 0, 1);
                (long x11, long x12, long x21, long x22) mat = (1, 1, 1, 0);

                while (_idx > 0)
                {

                    if ((_idx & 1L) == 1L) MatMul(ref ret, ref mat);
                    MatMul(ref mat, ref mat);
                    _idx >>= 1;
                }

                return ret.x21;
            }

            void MatMul(ref (long x11, long x12, long x21, long x22) _f, 
                ref (long x11, long x12, long x21, long x22) _b)
            {

                long x11 = (_f.x11 * _b.x11 + _f.x12 * _b.x21) % MOD;
                long x12 = (_f.x11 * _b.x12 + _f.x12 * _b.x22) % MOD;
                long x21 = (_f.x21 * _b.x11 + _f.x22 * _b.x21) % MOD;
                long x22 = (_f.x21 * _b.x12 + _f.x22 * _b.x22) % MOD;

                _f = (x11, x12, x21, x22);
            }
        }
    }

#if other
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

string[] ss = Console.ReadLine().Split();
long N = long.Parse(ss[0]);
long M = long.Parse(ss[1]);
long p = 1000000007;

M %= N;
while(M > 0)
{
    long tmp = N % M;
    N = M;
    M = tmp;
}

(long n0, long n1) fibo(long n)
{
    if (n == 1)
        return (1, 1);
    var half = fibo(n / 2);
    long v0 = (((half.n1 * 2 - half.n0 + p) % p) * half.n0) % p;
    long v1 = ((half.n1 * half.n1) % p + (half.n0 * half.n0) % p) % p;
    if (n % 2 == 0)
        return (v0, v1);
    else
        return (v1, (v0 + v1) % p);
}

sw.WriteLine(fibo(N).n0);

sr.Close();
sw.Close();
#elif other2
// #nullable disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

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

public struct Matrix
{
    public long A;
    public long B;
    public long C;
    public long D;

    public Matrix(long a, long b, long c, long d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public static Matrix operator *(Matrix lhs, Matrix rhs)
    {
        return new Matrix()
        {
            A = (lhs.A * rhs.A + lhs.B * rhs.C) % 1_000_000_007,
            B = (lhs.A * rhs.B + lhs.B * rhs.D) % 1_000_000_007,
            C = (lhs.C * rhs.A + lhs.D * rhs.C) % 1_000_000_007,
            D = (lhs.C * rhs.B + lhs.D * rhs.D) % 1_000_000_007,
        };
    }

    public override string ToString()
    {
        return $"[[{A}, {B}], [{C}, {D}]]";
    }
}
public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var (a, b) = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
        var n = (long)BigInteger.GreatestCommonDivisor(a, b);

        var dict = new Dictionary<long, Matrix>
        {
            { 0, new Matrix(1, 0, 0, 1) },
            { 1, new Matrix(0, 1, 1, 1) },
        };

        for (var pow = 1; pow < 63; pow++)
        {
            var m = dict[1L << (pow - 1)];
            dict.Add(1L << pow, m * m);
        }

        var bits = new BitArray(BitConverter.GetBytes(n));
        var mult = dict[0];
        for (var idx = 0; idx < 63; idx++)
        {
            if (bits[idx])
                mult *= dict[1L << idx];
        }

        Console.WriteLine(mult.B);
    }
}

#endif
}
