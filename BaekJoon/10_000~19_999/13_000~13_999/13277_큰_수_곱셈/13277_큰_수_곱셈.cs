using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

/*
날짜 : 2024. 2. 10
이름 : 배성훈
내용 : 큰 수 곱셈
    문제번호 : 13277번

    시간 초과 뜬다
    고속 푸리에변환 알고리즘을 써야 해결된다고 한다

    문자열 길이가 30만이므로,
    5만 * 5만? 25억 이다;

    당장은 파이썬은 큰 수의 곱셈을 지원하기에 파이썬으로 그냥 풀었다

    ///////////////////////////////////////////////////////////////
    # 파이썬 코드 #은 <- 파이썬에서 사용하는 주석이다

        a, b = map(int, input().split())
        ret = a * b
        print(ret)
    ///////////////////////////////////////////////////////////////
    시간은 5.2초 나왔다 

    어차피 고속 푸리에변환은 2달 안으로 만날거 같으니 그때가서 C++로 풀어야겠다
    2달안에 안됐다; -> 3달째인데도 안됐다;; -> 2024. 6. 20에 고속 푸리에 변환으로 해결완료
*/

namespace BaekJoon.etc
{
    internal class etc_0010
    {

        static void Main10(string[] args)
        {

#if WrongTimeOut
            // 시간 초과나는 방법이다
            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StringBuilder sb = new StringBuilder(300_000);

            ReadStr(sr, sb);
            string str1 = sb.ToString();
            sb.Clear();

            ReadStr(sr, sb);
            string str2 = sb.ToString();
            sb.Clear();
            sr.Close();
            // 연산
            int len1 = str1.Length;
            int len2 = str2.Length;

            len2 = 1 + len2 / 7;
            len1 = 1 + len1 / 7;
            long[] result = new long[len1 + len2 + 3];
            long MAX = 10_000_000;

            for (int i = 0; i < len2; i++)
            {

                long num2 = ReadLong(str2, i);

                for (int j = 0; j < len1; j++)
                {

                    long num1 = ReadLong(str1, j);
                    
                    long mul = num1 * num2;
                    long fir = mul % MAX;
                    long sec = mul / MAX;

                    result[j + i] += fir;
                    result[j + i + 1] += sec;

                    while (result[j + i] >= MAX)
                    {

                        result[j + i] -= MAX;
                        result[j + i + 1] += 1;
                    }

                    while (result[j + i + 1] >= MAX)
                    {

                        result[j + i + 1] -= MAX;
                        result[j + i + 2] += 1;
                    }
                }
            }

            bool start = false;
            for (int i = result.Length - 1; i >= 0; i--)
            {

                if (!start) 
                {

                    if (result[i] != 0)
                    {

                        sb.Append(result[i]);
                        start = true;
                    }
                    continue;
                }

                sb.Append($"{result[i]:D7}");
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            sw.Write(sb);
            sw.Close();

#endif

            Complex ONE = new(1.0, 0.0);
            StreamReader sr;
            Complex[] x, y;

            Solve();
            void Solve()
            {

                Init();
                
                MyMultiply();

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 8);

                int[] ret = new int[x.Length + 1];
                int add = 0;
                for (int i = 0; i < x.Length; i++)
                {

                    int cur = (int)Math.Round(x[i].Real);
                    cur += add;
                    add = cur / 10;
                    cur %= 10;
                    if (cur == 0) continue;
                    ret[i] = cur;
                }

                bool zero = true;
                for (int i = x.Length - 1; i >= 0; i--)
                {

                    if (ret[i] == 0) continue;
                    zero = false;
                    for (int j = i; j >= 0; j--)
                    {

                        sw.Write(ret[j]);
                    }

                    break;
                }

                if (zero) sw.Write(0);
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                string[] temp = sr.ReadLine().Split();
                sr.Close();

                int len1 = temp[0].Length;
                int len2 = temp[1].Length;

                int newSize = 2;
                while (newSize < len1 + len2) 
                { 
                    
                    newSize <<= 1; 
                }

                x = new Complex[newSize];
                y = new Complex[newSize];

                for (int i = 0; i < len1; i++)
                {

                    int idx = len1 - 1 - i;
                    x[idx] = temp[0][i] - '0';
                }

                for (int i = 0; i < len2; i++)
                {

                    int idx = len2 - 1 - i;
                    y[idx] = temp[1][i] - '0';
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
                FFT(y, false);

                for (int i = 0; i < x.Length; i++)
                {

                    x[i] = x[i] * y[i];
                }

                FFT(x, true);
            }
        }

        static void ReadStr(StreamReader _sr, StringBuilder _sb)
        {

            int c;
            while((c = _sr.Read()) != '\n' && c != ' ' && c != -1)
            {

                if (c == '\r') continue;

                _sb.Append((c - '0'));
            }
        }

        static long ReadLong(string _str, int _n)
        {

            int end = (_str.Length) - _n * 7;
            int start = Math.Max(end - 7, 0);

            long ret = 0;
            for (int i = start; i < end; i++)
            {

                ret *= 10;
                ret += _str[i] - '0';
            }

            return ret;
        }
    }

#if other
using System.Numerics;
using System.Text;

const int X = 2;
long TenPowX = (long)(Math.Pow(10, X));

var strings = Console.ReadLine().Split(' ');
var s1 = strings[0];
var s2 = strings[1];

var x = ToLongArray(s1);
var y = ToLongArray(s2);

var len = 1;
while (len < x.Length + y.Length)
    len *= 2;

// padding
var fx = x.Select(v => (Complex)v).Concat(new Complex[len - x.Length]).ToArray();
var fy = y.Select(v => (Complex)v).Concat(new Complex[len - y.Length]).ToArray();

FFT(fx, false);
FFT(fy, false);

var fxy = fx.Zip(fy, (l, r) => l * r).ToArray();
FFT(fxy, true);

var xy = fxy.Select(v => (long)Math.Round(v.Real)).ToArray();

var carry = 0L;
for (var idx = 0; idx < xy.Length; idx++)
{
    xy[idx] += carry;

    carry = xy[idx] / TenPowX;
    xy[idx] = xy[idx] % TenPowX;
}

var sb = new StringBuilder();
foreach (var v in xy.Reverse())
    sb.Append(v.ToString().PadLeft(X, '0'));

var s = sb.ToString().TrimStart('0');

if (String.IsNullOrEmpty(s))
    Console.WriteLine(0);
else
    Console.WriteLine(s);

return;

static long[] ToLongArray(string s)
{
    var list = new List<long>();
    var ms = new MemoryStream(s.Reverse().Select(v => (byte)v).ToArray());
    var br = new BinaryReader(ms);

    while (true)
    {
        var bytes = br.ReadBytes(X);
        if (bytes.Length == 0)
            break;

        var ss = new string(bytes.Reverse().Select(v => (Char)v).ToArray());
        list.Add(Int64.Parse(ss));
    }

    return list.ToArray();
}

static void FFT(Complex[] f, bool inv)
{
    int n = f.Length;

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
            var t = f[i];
            f[i] = f[j];
            f[j] = t;
        }
    }

    for (var k = 1; k < n; k *= 2)
    {
        var angle = inv ? Math.PI / k : -Math.PI / k;
        var dir = new Complex(Math.Cos(angle), Math.Sin(angle));

        for (var i = 0; i < n; i += k * 2)
        {
            var unit = Complex.One;

            for (j = 0; j < k; j++)
            {
                var u = f[i + j];
                var v = f[i + j + k] * unit;

                f[i + j] = u + v;
                f[i + j + k] = u - v;

                unit *= dir;
            }
        }
    }

    if (inv)
        for (var i = 0; i < n; i++)
            f[i] /= n;
}
#elif other2
using System;
using System.IO;
using System.Numerics;

namespace MyApp {
    static class FFT {
        private static int CountTrailingZeros(int n) {
            for (int i = 0; i < 31; ++i) {
                if ((n & (1<<i)) > 0) {
                    return i;
                }
            }
            throw new ArgumentException();
        }

        private static int ReverseBits(int n, int k) {
            int r, i;
            for (r = 0, i = 0; i < k; ++i) {
                r |= ((n >> i) & 1) << (k - i - 1);
            }
            return r;
        }

        private static void _FFT(IList<Complex> a, bool isReversed = false) {
            int n = a.Count;
            int k = CountTrailingZeros(n);

            for (int i = 0; i < n; i++) {
                int j = ReverseBits(i, k);
                if (i < j) {
                    var t = a[i];
                    a[i] = a[j];
                    a[j] = t;
                }
            }
            for (int s = 2; s <= n; s *= 2) {
                List<Complex> w = new List<Complex>(s / 2);
                for (int i = 0; i < s / 2; i++) {
                    double t = 2 * Math.PI * i / s * (isReversed ? -1 : 1);
                    w.Add(new Complex(Math.Cos(t), Math.Sin(t)));
                }
                for (int i = 0; i < n; i += s) {
                    for (int j = 0; j < s / 2; j++) {
                        Complex tmp = a[i + j + s / 2] * w[j];
                        a[i + j + s / 2] = a[i + j] - tmp;
                        a[i + j] += tmp;
                    }
                }
            }
            if (isReversed) {
                for (int i = 0; i < n; i++) {
                    a[i] /= n;
                }
            }
        }

        public static List<Complex> GetConvolution(IList<Complex> a, IList<Complex> b) {
            var aCopy = new List<Complex>(a);
            var bCopy = new List<Complex>(b);
            int n = 1;
            while (n < a.Count + b.Count) {
                n *= 2;
            }

            aCopy.Capacity = n;
            while (aCopy.Count < n) {
                aCopy.Add(new Complex(0, 0));
            }
            _FFT(aCopy);

            bCopy.Capacity = n;
            while (bCopy.Count < n) {
                bCopy.Add(new Complex(0, 0));
            }
            _FFT(bCopy);

            List<Complex> result = new List<Complex>(n);
            for (int i = 0; i < n; ++i) {
                result.Add(aCopy[i] * bCopy[i]);
            }
            _FFT(result, true);
            return result;
        }
    }

    internal class Program {
        static void Main(string[] args) {
            var tokens = Console.ReadLine().Split();
            var a = tokens[0];
            var b = tokens[1];

            List<Complex> aExp = new List<Complex>(b.Length);
            List<Complex> bExp = new List<Complex>(b.Length);

            for (int i = 0; i < a.Length; ++i) {
                aExp.Add(new Complex((double)(a[i] - '0'), 0));
            }
            aExp.Reverse();
            for (int i = 0; i < b.Length; ++i) {
                bExp.Add(new Complex((double)(b[i] - '0'), 0));
            }
            bExp.Reverse();

            var result = FFT.GetConvolution(aExp, bExp).Select((x) => (long)Math.Round(x.Real)).ToList();
            for (int i = 0; i < result.Count-1; ++i) {
                if (result[i] > 9) {
                    result[i+1] = result[i+1] + result[i] / 10;
                    result[i] = result[i] % 10;
                }
            }

            using (StreamWriter writer = new StreamWriter(Console.OpenStandardOutput())) {
                bool print = false;
                for (int i = result.Count-1; i >= 0; --i) {
                    if (print) {
                        writer.Write(result[i]);
                    } else if (result[i] != 0) {
                        print = true;
                        ++i;
                        continue;
                    }
                }

                if (!print) {
                    writer.Write("0");
                }
            }
        }
    }
}
#elif other3
using System.Numerics;
string[] str = Console.ReadLine().Split();
Console.WriteLine(BigInteger.Parse(str[0]) * BigInteger.Parse(str[1]));
#endif
}
