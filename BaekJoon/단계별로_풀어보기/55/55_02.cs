using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 15
이름 : 배성훈
내용 : 이동
    문제번호 : 1067번

    수학, 고속 푸리에 변환 문제다

    먼저 X를 생성함수로 만든다
    X = { 2, 3, 5 }를 생성함수로 만든다고 하면, f(x) = 2 * x^2 + 3 * x + 5 의형태이다
    Y = { 3, 1, 4 }라하면, Y' 를 Y의 반대 순서로 뒤집은 수열이라 보면 Y' = { 4, 1, 3 }이 된다
    이 Y'에 대해 생성함수를 만든다 g(x) = 4 * x^2 + 1 * x + 3

    그리고 f(x) * g(x) 를 하면
    차수가 n - 1인 항을 보면 분배법칙에 의해 다음 값이 된다
        5 * 4 + 3 * 1 + 2 * 3
    이는 우리가 찾는 S가 된다 n - 1번이 보기 안예뻐서
    Y부분은 차수를 1차씩 높여 생성함수로 만들었다 그러면 n차 항을 보면 된다

    그러면 g를 1차수씩 회전 시켜 문제를 풀 수 있지만, 회전시키는데 N의 연산을 필요로하거나
    나머지 연산을 필요로 한다

    프로그래머스에서 A라는 문자를 회전시켜, B라는 문자를 만들 수 있는지 확인하는 문제에서
    다른 사람 풀이의 A를 바로 뒤에 이어 붙이는 아이디어를 인용해서 풀었다
    해당 아이디어를 쓰면 길이는 2배가 되지만, 다른 별도의 연산을 필요로하지 않는다

    해당 방법을 이용하면 n차보다 크거나 같은 항들은 S가 가질 수 있는 값들이 된다
    여기서 최대값을 찾으면 정답이 된다
*/

namespace BaekJoon._55
{
    internal class _55_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr;
            Complex ONE = new Complex(1.0, 0.0);
            Complex[] p, q;
            int n, ret;

            Solve();

            void Solve()
            {

                Input();

                Multiply(p, q, p);

                GetRet();

                Console.Write(ret);
            }

            void GetRet()
            {

                ret = 0;
                for (int i = n; i < p.Length; i++)
                {

                    ret = Math.Max(ret, (int)Math.Round(p[i].Real));
                }
            }

            void Input()
            {
                
                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                p = new Complex[1 << (log + 1)];
                q = new Complex[1 << (log + 1)];

                Complex temp;
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    temp = new Complex(cur, 0.0);
                    p[i] = temp;
                    p[i + n] = temp;
                }

                for (int i = n; i >= 1; i--)
                {

                    int cur = ReadInt();
                    temp = new Complex(cur, 0.0);
                    q[i] = temp;
                }

                sr.Close();
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

            void Multiply(Complex[] _a, Complex[] _b, Complex[] _ret)
            {

                FFT(_a, false);
                FFT(_b, false);

                for (int i = 0; i < _a.Length; i++)
                {

                    _ret[i] = _a[i] * _b[i];
                }

                FFT(_a, true);
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    const long _p = 2281701377L;
    const long _w = 3;

    public static void Main()
    {
        var n = Int32.Parse(Console.ReadLine());
        var a = Console.ReadLine().Split(' ').Select(Int64.Parse).ToList();
        var b = Console.ReadLine().Split(' ').Select(Int64.Parse).ToList();

        var len = 1;
        while (len <= 3 * n)
            len *= 2;

        for (var idx = 0; idx < n; idx++)
            a.Add(a[idx]);

        while (a.Count < len)
            a.Add(0L);

        b.Reverse();
        while (b.Count < len)
            b.Add(0L);

        FFT(a);
        FFT(b);

        for (var idx = 0; idx < len; idx++)
            a[idx] = (a[idx] * b[idx]) % _p;

        FFT(a, true);

        var max = a.Max();
        Console.WriteLine(max);
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
// This program multiplies two 300,000-size decimal integers
// Utilizing the Fast Number Theoretic Transform

// # ifdef __GNUC__
// #pragma GCC target("avx2")
// #endif

// #if __cpp_if_constexpr >= 201606
// #define CONSTIF if constexpr
// #else
// #define CONSTIF if
// #endif

// # include <immintrin.h>
// # include <cstdint>
// # include <cstdio>
// # include <cstring>
// # include <algorithm>

    constexpr int LOG_MAX_SZ = 17;
    constexpr int MAX_SZ = 1 << LOG_MAX_SZ;

    // Helpers
    constexpr int32_t modpow(int32_t x, int y, int32_t P)
    {
        int32_t r = x;
        for (--y; y; y >>= 1)
        {
            if (y & 1) r = int64_t(r) * x % P;
            x = int64_t(x) * x % P;
        }
        return r;
    }

    constexpr int32_t modinv(int32_t x, int32_t P)
    {
        return modpow(x, P - 2, P);
    }

    // Modular Arithmetic
    // P: modulus, R: primitive root of P
    // MR: Montgomery number, MRR : Inverse of MR
    template<int32_t P_, int32_t R_>
struct Ring
    {

        // Convert to Montgomery form and back
        static constexpr int32_t montify(int32_t x)
        {
            return (int64_t(x) << 32) % P;
        }

        static constexpr int32_t unmontify(int32_t x)
        {
            return int64_t(x) * MRR % P;
        }

        static constexpr int32_t add(int32_t a, int32_t b)
        {
            int32_t c = a + b;
            return c < P ? c : c - P;
        }

        static constexpr int32_t sub(int32_t a, int32_t b)
        {
            int32_t c = P + a - b;
            return c < P ? c : c - P;
        }

        static constexpr int32_t mul(int32_t a, int32_t b)
        {
            int64_t x = int64_t(a) * b;
            int64_t s = ((x & (MR - 1)) * K) & (MR - 1);
            int64_t u = (x + s * P) >> 32;
            int32_t up = u - P;
            return up < 0 ? u : up;
        }

        static constexpr int32_t pow(int32_t a, int n)
        {
            int32_t r = ONE;
            for (; n; n >>= 1)
            {
                if (n & 1) r = mul(r, a);
                a = mul(a, a);
            }
            return r;
        }

        static constexpr int32_t inv(int32_t a)
        {
            return pow(a, P - 2);
        }

        __attribute__((target("avx2")))
    static __m256i add(__m256i a, __m256i b)
        {
            auto add = _mm256_add_epi32(a, b);
            auto mmP = _mm256_set1_epi32(P);
            auto cmp = _mm256_cmpgt_epi32(mmP, add);
            return _mm256_sub_epi32(add, _mm256_andnot_si256(cmp, mmP));
        }

        __attribute__((target("avx2")))
    static __m256i sub(__m256i a, __m256i b)
        {
            auto sub = _mm256_sub_epi32(a, b);
            auto cmp = _mm256_cmpgt_epi32(_mm256_setzero_si256(), sub);
            auto mmP = _mm256_set1_epi32(P);
            return _mm256_add_epi32(sub, _mm256_and_si256(cmp, mmP));
        }

        __attribute__((target("avx2")))
    static __m256i mul(__m256i a, __m256i b)
        {
            auto mmK64 = _mm256_set1_epi64x(K);
            auto mmP64 = _mm256_set1_epi64x(P);
            auto shft_a = _mm256_bsrli_epi128(a, 4);
            auto shft_b = _mm256_bsrli_epi128(b, 4);
            auto ab_hi = _mm256_mul_epu32(shft_a, shft_b);
            auto s_hi = _mm256_mul_epu32(ab_hi, mmK64);
            auto u_hi = _mm256_add_epi64(_mm256_mul_epu32(s_hi, mmP64), ab_hi);

            auto ab_lo = _mm256_mul_epu32(a, b);
            auto s_lo = _mm256_mul_epu32(ab_lo, mmK64);
            auto u_lo = _mm256_add_epi64(_mm256_mul_epu32(s_lo, mmP64), ab_lo);

            auto mask = _mm256_setr_epi32(0, -1, 0, -1, 0, -1, 0, -1);
            auto u = _mm256_blendv_epi8(_mm256_bsrli_epi128(u_lo, 4), u_hi, mask);
            auto mmP32 = _mm256_set1_epi32(P);
            auto cmp = _mm256_cmpgt_epi32(mmP32, u);
            return _mm256_sub_epi32(u, _mm256_andnot_si256(cmp, mmP32));
        }

        // N-th Primitive root of unity
        template<int f>
    static constexpr int32_t proot(int32_t N)
        {
            CONSTIF(f > 0) return pow(RRI, P / N);
        else return pow(RR, P / N);
        }

        /*
        * input : vector of 4 32-bit ints in Montgomery form.
        * output : Montgomery-form NTT in `a`.
        */
        template<int f>
        __attribute__((target("avx2")))
    static void mont_NTT(__m256i* x, int N)
        { // verified
            auto w = _mm256_set1_epi32(proot<f>(N));
            for (int k = N; k > 1; k >>= 1)
            {
                auto t = _mm256_set1_epi32(ONE);
                for (int l = 0; l < k / 2; ++l)
                {
                    for (int a = l; a < N; a += k)
                    {
                        int b = a + k / 2;
                        auto u = sub(x[a], x[b]);
                        x[a] = add(x[a], x[b]);
                        x[b] = mul(t, u);
                    }
                    t = mul(t, w);
                }
                w = mul(w, w);
            }
            for (int i = 1, j = 0; i < N; i++)
            {
                int b = N >> 1;
                for (; j >= b; b >>= 1) j -= b;
                j += b;
                if (i < j) std::swap(x[i], x[j]);
            }
            CONSTIF(f < 0) {
                auto z = _mm256_set1_epi32(inv(montify(N)));
                for (int i = 0; i < N; i++)
                    x[i] = mul(x[i], z);
            }
        }

        template<int f>
        __attribute__((target("avx2")))
    static __m256i roots(int N)
        {
            alignas(32) int32_t t[8] = { ONE };
            int32_t root = proot<f>(N);
            for (int i = 1; i < 8; ++i) t[i] = mul(t[i - 1], root);
            return _mm256_load_si256(reinterpret_cast<__m256i*>(t));
        }

        template<int f>
        __attribute__((target("avx2")))
    static void ntt8(__m256i* a, int N)
        {
            constexpr auto w4 = proot<f>(4), w8 = proot<f>(8), w4w8 = mul(w4, w8);
            const auto f1 = _mm256_setr_epi32(ONE, ONE, ONE, w4, ONE, ONE, ONE, w4);
            const auto f2 = _mm256_setr_epi32(ONE, ONE, ONE, ONE, ONE, w8, w4, w4w8);
            const auto mmP = _mm256_set1_epi32(P);
            const auto mP = _mm_set1_epi32(P);
            for (int i = 0; i < N; ++i)
            {
                a[i] = _mm256_permutevar8x32_epi32(a[i], _mm256_setr_epi32(0, 4, 2, 6, 1, 5, 3, 7));
                auto mm1 = _mm256_hadd_epi32(a[i], _mm256_setzero_si256());
                auto cmp = _mm256_cmpgt_epi32(mmP, mm1);
                mm1 = _mm256_sub_epi32(mm1, _mm256_andnot_si256(cmp, mmP));
                auto mm2 = _mm256_hsub_epi32(a[i], _mm256_setzero_si256());
                cmp = _mm256_cmpgt_epi32(_mm256_setzero_si256(), mm2);
                mm2 = _mm256_add_epi32(mm2, _mm256_and_si256(cmp, mmP));
                a[i] = _mm256_unpacklo_epi32(mm1, mm2);
                a[i] = mul(a[i], f1);
                auto s1 = _mm256_bsrli_epi128(a[i], 8);
                auto s2 = add(a[i], s1);
                auto s3 = sub(a[i], s1);
                auto s4 = _mm256_bslli_epi128(s3, 8);
                a[i] = _mm256_blend_epi32(s2, s4, 204);
                a[i] = mul(a[i], f2);
                auto m1 = _mm256_extracti128_si256(a[i], 0);
                auto m2 = _mm256_extracti128_si256(a[i], 1);
                auto m3 = _mm_add_epi32(m1, m2);
                auto c = _mm_cmpgt_epi32(mP, m3);
                m3 = _mm_sub_epi32(m3, _mm_andnot_si128(c, mP));
                auto m4 = _mm_sub_epi32(m1, m2);
                c = _mm_cmpgt_epi32(_mm_setzero_si128(), m4);
                m4 = _mm_add_epi32(m4, _mm_and_si128(c, mP));
                a[i] = _mm256_inserti128_si256(_mm256_castsi128_si256(m3), (m4), 1);
            }
        }

        // Performs the 8-step Number Theoretic Transform
        // transposed as a 8 x N/8 matrix in parallel.
        __attribute__((target("avx2")))
    static void NTT(int32_t* a, int N)
        {
            auto* va = reinterpret_cast<__m256i*>(a);
            mont_NTT < 1 > (va, N / 8);
            // Apply twiddle factors and perform 8-point fft
            const __m256i wN = roots < 1 > (N);
            auto w = _mm256_set1_epi32(ONE);
            for (int i = 0; i < N / 8; ++i)
            {
                va[i] = mul(va[i], w);
                w = mul(w, wN);
            }
            ntt8 < 1 > (va, N / 8);
        }

        __attribute__((target("avx2")))
    static void iNTT(int32_t* a, int N)
        {
            // Perform a 8-point FFT and apply twiddle factors
            auto* va = reinterpret_cast<__m256i*>(a);
            ntt8 < -1 > (va, N / 8);
            const __m256i wN = roots < -1 > (N);
            auto w = _mm256_set1_epi32(ONE);
            for (int i = 0; i < N / 8; ++i)
            {
                va[i] = mul(va[i], w);
                w = mul(w, wN);
            }
            mont_NTT < -1 > (va, N / 8);
            // Normalize
            auto z = _mm256_set1_epi32(montify(modinv(8, P)));
            for (int i = 0; i < N / 8; i++)
                va[i] = mul(va[i], z);
        }

        __attribute__((target("avx2")))
    static void polymul_ring(int32_t* f, int fn, int32_t* g, int gn)
        {
            int N = 8;
            while (N < fn + gn) N *= 2;
            for (int i = 0; i < N; ++i) f[i] = montify(f[i]);
            for (int i = 0; i < N; ++i) g[i] = montify(g[i]);
            NTT(f, N);
            NTT(g, N);
            auto* va = reinterpret_cast<__m256i*>(f);
            auto* vb = reinterpret_cast<__m256i*>(g);
            for (int i = 0; i < N / 8; ++i) va[i] = mul(va[i], vb[i]);
            iNTT(f, N);
            for (int i = 0; i < N; ++i) f[i] = unmontify(f[i]);
        }

        static constexpr int64_t MR = 1LL << 32;
    static constexpr int32_t P = P_, R = R_;
    static constexpr int32_t MRR = modinv(MR % P, P);
        static constexpr int32_t K = (int64_t(MR)* MRR - 1) / P;
    static constexpr int32_t ONE = MR % P;
    static constexpr int32_t RR = int64_t(R) * MR % P;
    static constexpr int32_t RRI = inv(RR);
    };
using R = Ring<754974721, 11>;

alignas(32) int32_t A[1 << 17], B[1 << 17];
    int main()
    {
        int n, size = 1;
        scanf("%d", &n);
        while (size < n * 2) size <<= 1;
        for (int i = 0, a; i < n; ++i)
        {
            scanf("%d", &a);
            A[i] = a;
        }
        for (int i = 0, b; i < n; ++i)
        {
            scanf("%d", &b);
            B[n - i] = b;
        }
        R::polymul_ring(A, n, B, n);
        int max = 0;
        for (int i = 0; i < n; ++i)
            max = std::max(max, A[i] + A[i + n]);
        printf("%d\n", max);
        return 0;
    }

#endif
}
