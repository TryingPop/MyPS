using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 14
이름 : 배성훈
내용 : 
    문제번호 : 

    수학, 고속 푸리에 변환 문제다

    쏠 수 있는 길이를 n이라 하고, 찾는 길이를 m이라 하자
    n의 아무 2개를 더하면 m이 나오는지 찾는 문제다

    n = { 2, 3, 4 }, m = { 10, 5, 7 }
    이라 할 때,

    5는 2 + 3, 7 = 3 + 4 총 2개가 나온다

    이는 f(x) = x^4 + x^3 + x^2 로 보고 g(x) = f(x) * f(x) 를 연산했을 때 
    g(x)의 m차 항이 0이 아니면 적당한 두 수를 더해서 m으로 만들 수 있다와 동형이 된다
    이는 곱이므로 고속 푸리에 변환을 해야한다

    내용을 읽는거보다 일단 해당 함수를 익히는데 초점을 둬서
    이해없이 보고 따라만 쳤다...;

    FFT알고리즘 설명은 해당 사이트에 되어져 있다
    https://infograph.tistory.com/331
    보니깐 분할 정복 아이디어로 접근하는거 같다

    (푸리에 급수는 대학생일 당시 임용 범위를 벗어나 생략했던 파트다, 
    미적분학, 복소해석학 부분에서 언급 내용을 보긴 했다 
    -> 간단하게 일반 함수를 sin과 cos 급수로 표현하는게 푸리에 급수였다
    해당 급수가 갖는 이점은 복소해석학에서 연속된 함수로 만들어준다고 들은거 같다)

    일단 내일 C 2회전이 끝나니 빨리 끝내서 FFT알고리즘 내용을 찾아 봐야겠다
*/

namespace BaekJoon._55
{
    internal class _55_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr;
            int n, m, ret;
            Complex[] p;
            Complex ONE = new Complex(1.0, 0.0);

            Solve();
            void Solve()
            {


                Input();

                Multiply();

                GetRet();

                Console.WriteLine(ret);
            }

            void GetRet()
            {

                ret = 0;
                m = ReadInt();

                for (int i = 0; i < m; i++)
                {

                    int cur = ReadInt();

                    if (Math.Round(p[cur].Real) > 0) ret++;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                p = new Complex[1 << 19];
                n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    p[cur] = ONE;
                }

                p[0] = ONE;
            }

            void FFT(Complex[] _arr, bool _inv)
            {

                int n = _arr.Length;
                Complex temp;
                for (int i = 1, j = 0; i < n; i++)
                {

                    int bit = (n >> 1);

                    while (((j ^= bit) & bit) == 0) 
                    { 
                        
                        bit >>= 1; 
                    }

                    if (i < j)
                    {

                        temp = _arr[i];
                        _arr[i] = _arr[j];
                        _arr[j] = temp;
                    }
                }

                Complex p, w;
                for (int i = 1; i < n; i <<= 1)
                {

                    double x = _inv ? Math.PI / i : -Math.PI / i;

                    w = new Complex(Math.Cos(x), Math.Sin(x));

                    for (int j = 0; j < n; j += (i << 1))
                    {

                        p = ONE;

                        for (int k = 0; k < i; k++)
                        {

                            temp = _arr[i + j + k] * p;
                            _arr[i + j + k] = _arr[j + k] - temp;
                            _arr[j + k] += temp;
                            p *= w;
                        }
                    }
                }

                if (_inv)
                {

                    for (int i = 0; i < n; i++)
                    {

                        _arr[i] /= n;
                    }
                }
            }

            void Multiply()
            {

                FFT(p, false);

                for (int i = 0; i < p.Length; i++)
                {

                    p[i] = p[i] * p[i];
                }

                FFT(p, true);
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

#if other
// #nullable disable

using System;
using System.Collections;
using System.Collections.Generic;

public static class Program
{
    const long _p = 2281701377L;
    const long _w = 3;

    public static void Main()
    {
        var n = Int32.Parse(Console.ReadLine());
        var k = new List<int>(200000);
        for (var idx = 0; idx < n; idx++)
            k.Add(Int32.Parse(Console.ReadLine()));

        var m = Int32.Parse(Console.ReadLine());
        var d = new List<int>(200000);
        for (var idx = 0; idx < m; idx++)
            d.Add(Int32.Parse(Console.ReadLine()));

        var len = 524288;
        var kFFT = new List<long>(len);

        for (var idx = 0; idx < len; idx++)
            kFFT.Add(0);

        kFFT[0] = 1;
        for (var idx = 0; idx < n; idx++)
            kFFT[k[idx]] = 1;

        FFT(kFFT);

        for (var idx = 0; idx < len; idx++)
            kFFT[idx] = (kFFT[idx] * kFFT[idx]) % _p;

        FFT(kFFT, true);
        var count = 0;
        foreach (var dvalue in d)
        {
            if (kFFT[dvalue] > 0)
                count++;
        }

        Console.WriteLine(count);
    }

    public static long ModInv(long x, long mod)
    {
        GCD(x, mod, out var a, out var b);
        return (a % mod + mod) % mod;
    }
    public static long GCD(long a, long b, out long x, out long y)
    {
        if (a == 0)
        {
            x = 0;
            y = 1;
            return b;
        }

        var gcd = GCD(b % a, a, out var x1, out var y1);

        x = y1 - (b / a) * x1;
        y = x1;

        return gcd;
    }

    public static long FastPow(long a, long n, long mod)
    {
        var bits = new BitArray(BitConverter.GetBytes(n));

        var pow = 1L;
        var aPowI = a;
        for (var idx = 0; idx < bits.Length; idx++)
        {
            if (bits[idx])
                pow = (pow * aPowI) % mod;

            aPowI = (aPowI * aPowI) % mod;
        }

        return pow;
    }

    public static void FFT(List<long> x, bool inv = false)
    {
        var n = x.Count;

        var j = 0;
        for (var i = 1; i < n; i++)
        {
            var bit = n / 2;

            while (j >= bit)
            {
                j -= bit;
                bit /= 2;
            }

            j += bit;

            if (i < j)
            {
                var tmp = x[i];
                x[i] = x[j];
                x[j] = tmp;
            }
        }

        for (var k = 1; k < n; k *= 2)
        {
            // w^(p-1) = 1 = e^2ipi
            var dir = FastPow(_w, (_p - 1) / k / 2, _p);
            if (inv)
                dir = ModInv(dir, _p);

            for (var i = 0; i < n; i += k * 2)
            {
                var c = 1L;

                for (j = 0; j < k; j++)
                {
                    var a = x[i + j];
                    var b = (x[i + j + k] * c) % _p;

                    x[i + j] = (a + b) % _p;
                    x[i + j + k] = (a + _p - b) % _p;

                    c = (c * dir) % _p;
                }
            }
        }

        if (inv)
        {
            var invn = ModInv(n, _p);

            for (var i = 0; i < n; i++)
                x[i] = (x[i] * invn) % _p;
        }
    }
}

#elif other2
// cs10531 - rby
// 2023-06-28 10:01:07
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace cs1067
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static Complex[] pow;
        static int N = 1;

        static void Main(string[] args)
        {
            int M = int.Parse(sr.ReadLine());
            int[] bot = new int[M + 1];
            for (int i = 0; i < M; i++)
                bot[i] = int.Parse(sr.ReadLine());

            int len = 200000;
            while (N < len * 2)
                N *= 2;

            pow = new Complex[N];
            pow[0] = 1;
            Complex w = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne / N);
            for (int i = 1; i < N; i++)
            {
                pow[i] = pow[i - 1] * w;
            }

            Complex[] A = new Complex[N];
            Complex[] B = new Complex[N];

            A[0] = 1;
            B[0] = 1;

            foreach(var item in bot)
            {
                A[item] = 1;
                B[item] = 1;
            }

            Complex[] Afft = FFT(A);
            Complex[] Bfft = FFT(B);

            Complex[] C = new Complex[N];
            for (int i = 0; i < N; i++)
                C[i] = Afft[i] * Bfft[i];

            Complex[] Cfft = IFFT(C);


            int[] num = new int[N + 1];
            for (int i = N - 1; i >= 0; i--)
                num[i] = (int)Math.Round(Cfft[i].Real / N);

            M = int.Parse(sr.ReadLine());
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < M; i++)
                set.Add(int.Parse(sr.ReadLine()));

            for(int i = 0; i < N; i++)
            {
                if (num[i] != 0)
                    set.Remove(i);
            }

            sb.Append((M - set.Count).ToString());


            sw.Write(sb);
            sw.Close();
            sr.Close();
        }

        static Complex[] FFT(Complex[] P)
        {
            int n = P.Length;
            if (n == 1)
                return P;

            Complex[] Pe = new Complex[n / 2];
            Complex[] Po = new Complex[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                Pe[i] = P[i * 2];
                Po[i] = P[i * 2 + 1];
            }

            Complex[] ye = FFT(Pe);
            Complex[] yo = FFT(Po);

            Complex[] y = new Complex[n];
            for (int j = 0; j < n / 2; j++)
            {
                y[j] = ye[j] + pow[N / n * j] * yo[j];
                y[j + n / 2] = ye[j] - pow[N / n * j] * yo[j];
            }
            return y;
        }

        static Complex[] IFFT(Complex[] P)
        {
            int n = P.Length;
            if (n == 1)
                return P;

            Complex[] Pe = new Complex[n / 2];
            Complex[] Po = new Complex[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                Pe[i] = P[i * 2];
                Po[i] = P[i * 2 + 1];
            }

            Complex[] ye = IFFT(Pe);
            Complex[] yo = IFFT(Po);

            Complex[] y = new Complex[n];
            for (int j = 0; j < n / 2; j++)
            {
                y[j] = ye[j] + yo[j] / pow[N / n * j];
                y[j + n / 2] = ye[j] - yo[j] / pow[N / n * j];
            }
            return y;
        }
    }
}

#endif
}
