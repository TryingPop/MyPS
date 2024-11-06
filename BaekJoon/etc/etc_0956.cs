using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 9
이름 : 배성훈
내용 : 쿨한 물건 구매
    문제번호 : 1214번

    수학, 정수론, 브루트포스 문제다
    아이디어는 다음과 같다 
    편의상 p, q 중 큰 값을 q라 한다
    lcm을 p, q의 최소 공배수라하면
    
    그리디나 수학으로 lcm / q 와 1 + d / q 중에 
    작은걸 min이라 하면 택해 0 ~ min까지 q를 선택하고 
    최소가 되는 가격을 찾으면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0956
    {

        static void Main956(string[] args)
        {

            int d, p, q;

            Solve();
            void Solve()
            {

                Input();

                if (d % p == 0 || d % q == 0)
                {

                    Console.Write(d);
                    return;
                }

                int gcd = GetGCD(p, q);

                int len = Math.Min(q / gcd, (d / q) + 1);

                long ret = 2_000_000_000;

                for (long i = 0; i <= len; i++)
                {

                    long chk = i * q;
                    long r = d - chk;

                    if (r > 0)
                    {

                        long cnt = r / p;
                        if (r % p > 0) cnt++;
                        chk = cnt * p + i * q;
                    }

                    ret = Math.Min(ret, chk);
                }

                Console.Write(ret);
            }

            int GetGCD(int _a, int _b)
            {

                while (_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            void Input()
            {

                string[] input = Console.ReadLine().Split();

                d = int.Parse(input[0]);
                p = int.Parse(input[1]);
                q = int.Parse(input[2]);

                if (p > q)
                {

                    int temp = p;
                    p = q;
                    q = temp;
                }
            }
        }
    }

#if other
// #include<cstdio>
// #include<algorithm>
int D,P,Q,m,ans;
int gcd(int a,int b) { return b ? gcd(b, a % b) : a; }
int main(){
	scanf("%d%d%d",&D,&P,&Q);
	if(Q < P) P = P + Q - (Q = P);
	for(m = std::min( P / gcd(P, Q), --D / Q + 1); m--;) ans = std::max(ans, (D - m * Q) % P);
	printf("%d",D + std::min(P - ans, Q - D % Q));
	return 0;
}
#elif other2
// #nullable disable

using System;
using System.IO;
using System.Linq;

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

public record struct Point(long Y, long X);

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var (d, p, q) = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
        var sf = SolveFast(d, p, q);
        sw.WriteLine(sf);

        //var rd = new Random();
        //while (true)
        //{
        //    var (d, p, q) = (rd.Next(1, 123123123), rd.Next(1, 123), rd.Next(1, 1231));

        //    var sf = SolveFast(d, p, q);
        //    var ss = SolveSlow(d, p, q);

        //    if (sf != ss)
        //        sw.WriteLine($"{d} {p} {q}: {sf} {ss}");
        //}
    }

    private static long SolveFast(long d, long p, long q)
    {
        if (p < q)
            (p, q) = (q, p);

        var v = 1 + (d - 1) / p;
        v *= p;
        v = Math.Min(v, q * p);

        var min = Int64.MaxValue;
        for (var psum = 0L; psum <= v; psum += p)
        {
            if (d - psum > 0)
            {
                var qcount = 1 + (d - psum - 1) / q;
                min = Math.Min(min, psum + q * qcount);
            }
            else
            {
                min = Math.Min(min, psum);
            }
        }

        return min;
    }
    private static long SolveSlow(long d, long p, long q)
    {
        if (p < q)
            (p, q) = (q, p);

        var v = 1 + (d - 1) / p;
        v *= p;
        var min = v;

        for (var psum = 0L; psum < v; psum += p)
        {
            var qcount = 1 + (d - psum - 1) / q;
            min = Math.Min(min, psum + q * qcount);

            if (min == d)
                break;
        }

        return min;
    }
}

#endif
}
