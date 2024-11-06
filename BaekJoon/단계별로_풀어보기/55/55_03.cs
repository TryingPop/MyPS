using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 16
이름 : 배성훈
내용 : Needle
    문제번호 : 20176번

    수학, 고속 푸리에 변환 문제다
    접근을 잘못해서 엄청나게 틀렸다

    아이디어는 다음과 같다
    x, y, z라할 때, x - y == y - z인 경우를 모두 세어야한다
    일단 식을 변형하면 x + z = 2 * y가 나온다

    그래서 x + z 의 개수 구한 뒤 y에 2배한 경우가 몇 개 있는지 세어주면 된다
    ... 그냥 1개씩 더해서 엄청나게 틀렸고 이를 수정하니 이상없이 통과했다;
*/

namespace BaekJoon._55
{
    internal class _55_03
    {

        static void Main3(string[] args)
        {

            Complex ONE = new Complex(1.0, 0.0);
            int ADD = 30_000;

            StreamReader sr;

            Complex[] x, z;
            int[] y;
            int ret;

            Solve();

            void Solve()
            {

                Input();

                MyMultiply();

                GetRet();

                Console.Write(ret);
            }

            void GetRet()
            {

                ret = 0;
                for (int i = 0; i <= 60_000; i++)
                {

                    ret += y[i] * (int)Math.Round(x[2 * i].Real);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                x = new Complex[1 << 17];
                z = new Complex[1 << 17];

                y = new int[1 << 17];

                FillArr(x, ReadInt());

                int len = ReadInt();
                for (int i = 0; i < len; i++)
                {

                    int idx = ReadInt() + ADD;
                    y[idx]++;
                }
                FillArr(z, ReadInt());

                sr.Close();
            }

            void FillArr(Complex[] _arr, int _len)
            {

                for (int i = 0; i < _len; i++)
                {

                    int cur = ReadInt() + ADD;
                    _arr[cur] = ONE;
                }
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

            void MyMultiply()
            {

                FFT(x, false);
                FFT(z, false);

                for (int i = 0; i < x.Length; i++)
                {

                    x[i] = x[i] * z[i];
                }

                FFT(x, true);
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if ntt
            // NTT는 추후에 다시 도전!
            int ADD = 30_000;
            long MOD = 998_244_353L;
            long w = 3;

            long[] x, y, z;
            long[] calc1, calc2;
            StreamReader sr;

            int ret;

            Solve();

            void Solve()
            {

                Input();

                MyMultiply();

                GetRet();

                Console.Write(ret);
            }

            long GetPow(long _a, long _b)
            {

                long ret = 1;

                while(_b > 0)
                {

                    if ((_b & 1L) == 1) ret = (ret * _a) % MOD;

                    _a = (_a * _a) % MOD;
                    _b /= 2;
                }

                return ret;
            }

            void NTT(long[] _arr, bool _inv)
            {

                int n = _arr.Length;

                Array.Fill(calc1, 0);

                for (int i = 0; i < n; i++)
                {

                    calc1[i] = calc1[i >> 1] >> 1;
                    if ((i & 1) == 1) calc1[i] |= n >> 1;
                    
                    if (i < calc1[i])
                    {

                        long temp = _arr[i];
                        _arr[i] = _arr[calc1[i]];
                        _arr[calc1[i]] = temp;
                    }
                }

                long x = GetPow(w, (MOD - 1) / n);
                if (_inv) x = GetPow(x, MOD - 2);

                calc2[0] = 1;
                for (int i = 1; i <= n; i++)
                {

                    calc2[i] = (calc2[i - 1] * x) % MOD;
                }

                int idx = 2;

                while (idx <= n)
                {

                    int step = n / idx;

                    for (int i = 0; i < n; i += idx)
                    {

                        for (int j = 0; j < (idx >> 1); j++)
                        {

                            long u = _arr[i | j];
                            long v = (_arr[i | j | (idx >> 1)] * calc2[step * j]) % MOD;

                            _arr[i | j] = (u + v) % MOD;
                            _arr[i | j | (idx >> 1)] = (u - v) % MOD;
                            if (_arr[i | j | (idx >> 1)] < 0) _arr[i | j | (idx >> 1)] += MOD;
                        }
                    }

                    idx <<= 1;
                }

                if (_inv)
                {

                    long t = GetPow(n, MOD - 2);
                    for (int i = 0; i < n; i++)
                    {

                        _arr[i] = (_arr[i] * t) % MOD;
                    }
                }
            }

            void MyMultiply()
            {

                NTT(x, false);
                NTT(y, false);
                NTT(z, false);

                for (int i = 0; i < x.Length; i++)
                {

                    x[i] = (x[i] * z[i]) % MOD;
                    y[i] = (y[i] * y[i]) % MOD;
                }

                NTT(x, true);
                NTT(y, true);
            }

            void GetRet()
            {

                ret = 0;

                for (int i = 0; i < x.Length; i++)
                {

                    if (x[i] > 0 && y[i] > 0) ret++;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                int len = 1 << 17;
                x = new long[len];
                y = new long[len];
                z = new long[len];
                calc1 = new long[len];
                calc2 = new long[len + 1];

                FillArr(x, ReadInt());
                FillArr(y, ReadInt());
                FillArr(z, ReadInt());

                sr.Close();
            }

            void FillArr(long[] _arr, int _len)
            {

                for (int i = 0; i < _len; i++)
                {

                    int idx = ReadInt() + ADD;
                    _arr[idx] = 1;
                }
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
#endif

#if other
// #nullable disable

using System;
using System.Linq;

public static class Program
{
    const long _p = 2483027969L;
    const long _w = 3;

    public static void Main()
    {
        // linear means (x1-x2) = (x2-x3) -> (x1+x3-2x2) = 0
        var x1 = new long[262144];
        var x2 = new long[262144];

        var nu = Int32.Parse(Console.ReadLine());
        foreach (var v in Console.ReadLine().Split(' ').Select(Int32.Parse))
            x1[30000 + v]++;

        FFT(x1, x1.Length, _p, _w);

        var nm = Int32.Parse(Console.ReadLine());
        foreach (var v in Console.ReadLine().Split(' ').Select(Int32.Parse))
            x2[2 * (30000 - v)]++;

        FFT(x2, x2.Length, _p, _w);
        for (var idx = 0; idx < x1.Length; idx++)
            x1[idx] = (x1[idx] * x2[idx]) % _p;

        var nl = Int32.Parse(Console.ReadLine());
        Array.Clear(x2);
        foreach (var v in Console.ReadLine().Split(' ').Select(Int32.Parse))
            x2[30000 + v]++;

        FFT(x2, x2.Length, _p, _w);
        for (var idx = 0; idx < x1.Length; idx++)
            x1[idx] = (x1[idx] * x2[idx]) % _p;

        FFT(x1, x1.Length, _p, _w, true);

        Console.WriteLine(x1[120000]);
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
        var ret = 1L;
        while (n > 0)
        {
            if ((n & 1) == 1)
                ret = (ret * a) % mod;

            n >>= 1;
            a = (a * a) % mod;
        }

        return ret;
    }

    public static void FFT(long[] x, int n, long p, long w, bool inv = false)
    {
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
            var dir = FastPow(w, (p - 1) / k / 2, p);
            // w^(p-1) = 1 = e^2ipi
            if (inv)
                dir = ModInv(dir, p);

            for (var i = 0; i < n; i += k * 2)
            {
                var c = 1L;

                for (j = 0; j < k; j++)
                {
                    var a = x[i + j];
                    var b = (x[i + j + k] * c) % p;

                    x[i + j] = (a + b) % p;
                    x[i + j + k] = (a + p - b) % p;

                    c = (c * dir) % p;
                }
            }
        }

        if (inv)
        {
            var invn = ModInv(n, p);

            for (var i = 0; i < n; i++)
                x[i] = (x[i] * invn) % p;
        }
    }
}

#elif other2
// #include <bits/stdc++.h>
// #ifdef SHARAELONG
// #include "../../cpp-header/debug.hpp"
// #endif
using namespace std;
    typedef long long ll;
    typedef pair<int, int> pii;

    namespace fio
    {
        const int BSIZE = 1 << 21;
        char buffer[BSIZE];
        char wbuffer[BSIZE];
        char ss[30];
        int pos = BSIZE;
        int wpos = 0;
        int cnt = 0;

        inline char readChar()
        {
            if (pos == BSIZE)
            {
                fread(buffer, 1, BSIZE, stdin);
                pos = 0;
            }
            return buffer[pos++];
        }

        inline int readInt()
        {
            char c = readChar();
            while ((c < '0' || c > '9') && (c ^ '-')) c = readChar();

            int res = 0;
            bool neg = (c == '-');
            if (neg) c = readChar();
            while (c > 47 && c < 58)
            {
                res = res * 10 + c - '0';
                c = readChar();
            }

            if (neg) return -res;
            else return res;
        }

        inline void writeChar(char x)
        {
            if (wpos == BSIZE)
            {
                fwrite(wbuffer, 1, BSIZE, stdout);
                wpos = 0;
            }
            wbuffer[wpos++] = x;
        }

        inline void writeInt(int x)
        {
            if (x < 0)
            {
                writeChar('-');
                x = -x;
            }
            if (!x)
            {
                writeChar('0');
            }
            else
            {
                cnt = 0;
                while (x)
                {
                    ss[cnt] = (x % 10) + '0';
                    cnt++;
                    x /= 10;
                }
                for (int j = cnt - 1; j >= 0; --j) writeChar(ss[j]);
            }
        }

        inline void my_flush()
        {
            if (wpos)
            {
                fwrite(wbuffer, 1, wpos, stdout);
                wpos = 0;
            }
        }
    }
    typedef complex<double> base;

    const double PI = acos(-1);

    void fft(vector<base>& a, bool inv)
    {
        int n = a.size();
        for (int dest = 1, src = 0; dest < n; ++dest)
        {
            int bit = n / 2;
            while (src >= bit)
            {
                src -= bit;
                bit /= 2;
            }
            src += bit;
            if (dest < src) { swap(a[dest], a[src]); }
        }

        for (int len = 2; len <= n; len *= 2)
        {
            double ang = 2 * PI / len * (inv ? -1 : 1);
            base unity(cos(ang), sin(ang));
            for (int i = 0; i < n; i += len)
            {
                base w(1, 0);
                for (int j = 0; j < len / 2; ++j)
                {
                    base u = a[i + j], v = a[i + j + len / 2] * w;
                    a[i + j] = u + v;
                    a[i + j + len / 2] = u - v;
                    w *= unity;
                }
            }
        }

        if (inv)
        {
            for (int i = 0; i < n; ++i) { a[i] /= n; }
        }
    }
    void multiply(const vector<int>& a, const vector<int>& b, vector<int>& result)
    {
        int n = 2;
        while (n < a.size() + b.size()) { n *= 2; }

        vector < base > p(a.begin(), a.end());
        p.resize(n);
        for (int i = 0; i < b.size(); ++i) { p[i] += base(0, b[i]); }
        fft(p, false);

        result.resize(n);
        for (int i = 0; i <= n / 2; ++i)
        {
            base u = p[i], v = p[(n - i) % n];
            p[i] = (u * u - conj(v) * conj(v)) * base(0, -0.25);
            p[(n - i) % n] = (v * v - conj(u) * conj(u)) * base(0, -0.25);
        }
        fft(p, true);
        for (int i = 0; i < n; ++i) { result[i] = (int)round(p[i].real()); }
    }

    const int MAX_X = 3e4;

    void solve()
    {
        int nu = fio::readInt();
        vector<int> a(2 * MAX_X + 1, 0);
        for (int i = 0; i < nu; ++i)
        {
            int x = fio::readInt();
            a[x + MAX_X] = 1;
        }
        int nm = fio::readInt();
        vector<int> xm(nm);
        for (int & x: xm)
        {
            x = fio::readInt();
            x += MAX_X;
        }
        int nl = fio::readInt();
        vector<int> b(2 * MAX_X + 1, 0);
        for (int i = 0; i < nl; ++i)
        {
            int x = fio::readInt();
            b[x + MAX_X] = 1;
        }

        vector<int> res;
        multiply(a, b, res);

        ll ans = 0;
        for (int x: xm) ans += res[2 * x];
        cout << ans;
    }

    int main()
    {
        ios_base::sync_with_stdio(false);
        cin.tie(nullptr);

        solve();
    }
#endif
}
