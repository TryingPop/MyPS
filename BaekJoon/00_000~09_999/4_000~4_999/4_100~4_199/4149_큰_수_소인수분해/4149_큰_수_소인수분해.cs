using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 19
이름 : 배성훈
내용 : 큰 수 소인수 분해
    문제번호 : 4149번
    
    플라드 로 알고리즘과, 밀러 라빈 알고리즘 문제다

    gcd(n, k) = 1 문제를 풀기 위해 먼저 풀었다
    플라드 로 알고리즘으로 해당 수의 인수를 얻고
    밀러 라빈 알고리즘으로 해당 수가 소수인지 판정한다

    플라이드 로 알고리즘을 봤을 때, random 값을 써서 Rho 모양을 만들기에
    랜덤성의 문제로 시간초과인가 했다 10번 가까이 틀리고, 오버플로우가 문제임을 알았다
    그래서 이전에 BigIntger 자료구조를 이용해 푼 기억이 있어 해당 코드로 바꾸니 이상없이 통과했다

    다른 사람의 풀이를 보니, long으로만 해결했는데, 모듈러 곱셈을 분할 정복 방법으로 직접 정의해 풀었다

    플라드 로 알고리즘은 다음 글들을 보고 구현해 제출했다
    https://blog.naver.com/PostView.nhn?blogId=jinhan814&logNo=222138500193
    https://restudycafe.tistory.com/451
    https://yabitemporary.tistory.com/entry/5-%EB%B0%80%EB%9F%AC-%EB%9D%BC%EB%B9%88-%EC%86%8C%EC%88%98%ED%8C%90%EC%A0%95%EB%B2%95%EA%B3%BC-%ED%8F%B4%EB%9D%BC%EB%93%9C-%EB%A1%9C-%EC%86%8C%EC%9D%B8%EC%88%98%EB%B6%84%ED%95%B4
*/

namespace BaekJoon.etc
{
    internal class etc_0200
    {

        static void Main200(string[] args)
        {

            BigInteger n = Convert.ToInt64(Console.ReadLine());

            int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };
            List<BigInteger> div = new(64);
            Random rand = new();

            PollardRho(n);

            div.Sort();

            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                for (int i = 0; i < div.Count; i++)
                {

                    sw.Write($"{div[i]}\n");
                }
            }

            bool MillerRabin(BigInteger _x, BigInteger _a)
            {

                if (_x % _a == 0) return false;
                BigInteger d = _x - 1;
                BigInteger temp;
                while (true)
                {

                    temp = GetPow(_a, d, _x);
                    if ((d & 1L) == 1) return temp != 1 && temp != _x - 1;
                    else if (temp == _x - 1) return false;
                    d /= 2;
                }
            }

            bool IsPrime(BigInteger _x)
            {

                for (int i = 0; i < primes.Length; i++)
                {

                    if (_x == primes[i]) return true;
                    if (_x > 40 && MillerRabin(_x, primes[i])) return false;
                }

                if (_x <= 40) return false;
                return true;
            }

            BigInteger GetPow(BigInteger _a, BigInteger _b, BigInteger _mod)
            {

                BigInteger ret = 1;

                while (_b > 0)
                {

                    if ((_b & 1L) == 1) ret = (ret * _a) % _mod;

                    _a = (_a * _a) % _mod;
                    _b = _b / 2;
                }

                return ret;
            }

            BigInteger GetGCD(BigInteger _a, BigInteger _b)
            {

                BigInteger temp;
                while (_b > 0)
                {

                    temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            BigInteger F(BigInteger _x, BigInteger _c, BigInteger _mod)
            {

                return (((_x * _x) % _mod) + _c) % _mod;
            }

            void PollardRho(BigInteger _n)
            {

                if (_n == 1) return;
                if (_n % 2 == 0)
                {

                    div.Add(2);
                    PollardRho(_n / 2);
                    return;
                }
                if (IsPrime(_n))
                {

                    div.Add(_n);
                    return;
                }

                BigInteger x = 1;
                BigInteger y = 1;
                BigInteger c = 1;
                BigInteger gcd = _n;

                BigInteger temp;
                do
                {

                    if (gcd == _n)
                    {

                        x = rand.NextInt64() % (_n - 2) + 2;
                        y = x;
                        c = rand.NextInt64() % (_n - 1) + 1;
                    }

                    x = F(x, c, _n);
                    y = F(F(y, c, _n), c, _n);
                    temp = x - y;
                    temp = temp < 0 ? -temp : temp;
                    gcd = GetGCD(temp, _n);
                } while (gcd == 1);

                PollardRho(gcd);
                PollardRho(_n / gcd);
            }
        }
    }

#if other
#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = UInt64.Parse(sr.ReadLine());
        var factors = new List<ulong>();
        var rd = new Random();

        if (n == 1)
            factors.Add(1);

        while (n != 1)
        {
            var f = Rho(n, rd);

            factors.Add(f);
            n /= f;
        }

        foreach (var v in factors.OrderBy(v => v))
            sw.WriteLine(v);
    }

    public static ulong Rho(ulong n, Random rd)
    {
        if (n == 1 || MillerRabin(n))
            return n;

        if (n % 2 == 0)
            return 2;

        while (true)
        {
            var c = (ulong)rd.NextInt64(1, (long)n);
            var d = 1UL;
            var x = (ulong)rd.NextInt64(2, (long)n);
            var y = x;

            while (d == 1)
            {
                x = SafeAdd(SafeMult(x, x, n), c, n);
                y = SafeAdd(SafeMult(y, y, n), c, n);
                y = SafeAdd(SafeMult(y, y, n), c, n);

                d = GCD(n, Math.Max(x, y) - Math.Min(x, y));
            }

            if (MillerRabin(d))
                return d;
            else
                return Rho(d, rd);
        }
    }
    public static ulong GCD(ulong a, ulong b)
    {
        while (a != 0 && b != 0)
            if (a > b)
                a %= b;
            else
                b %= a;

        return Math.Max(a, b);
    }
    public static ulong SafeAdd(ulong a, ulong b, ulong mod)
    {
        var v = a + b;
        if (v > mod)
            v -= mod;

        return v;
    }
    public static ulong SafeMult(ulong a, ulong b, ulong mod)
    {
        var rv = 0UL;
        while (b > 0)
        {
            if (b % 2 == 1)
                rv = SafeAdd(rv, a, mod);

            a = (a * 2) % mod;
            b /= 2;
        }

        return rv;
    }
    public static ulong FastPow(ulong a, ulong v, ulong mod)
    {
        var rv = 1UL;

        while (v > 0)
        {
            if ((v & 1) == 1)
                rv = SafeMult(rv, a, mod);

            a = SafeMult(a, a, mod);
            v /= 2;
        }

        return rv;
    }
    public static bool MillerRabin(ulong p)
    {
        if (p == 1)
            return false;

        var q = p - 1;
        var k = 0;
        while (q % 2 == 0)
        {
            q /= 2;
            k++;
        }

        foreach (var a in new ulong[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41 })
        {
            if (a >= p)
                break;

            var aq = FastPow(a, q, p);
            if (aq == 1 || aq == p - 1)
                continue;

            var pass = false;
            for (var step = 0; step < k; step++)
            {
                aq = SafeMult(aq, aq, p);
                if (aq == p - 1)
                {
                    pass = true;
                    break;
                }
            }

            if (!pass)
                return false;
        }

        return true;
    }
}

#elif other2
#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = UInt64.Parse(sr.ReadLine());
        var factors = new List<ulong>();
        var rd = new Random();

        if (n == 1)
            factors.Add(1);

        while (n != 1)
        {
            var f = Rho(n, rd);

            factors.Add(f);
            n /= f;
        }

        foreach (var v in factors.OrderBy(v => v))
            sw.WriteLine(v);
    }

    public static ulong Rho(ulong n, Random rd)
    {
        if (n == 1 || MillerRabin(n))
            return n;

        if (n % 2 == 0)
            return 2;

        while (true)
        {
            var c = (ulong)rd.NextInt64(1, (long)n);
            var d = 1UL;
            var x = (ulong)rd.NextInt64(2, (long)n);
            var y = x;

            while (d == 1)
            {
                x = SqAdd(x, c, n);
                y = SqAdd(y, c, n);
                y = SqAdd(y, c, n);

                d = GCD(n, Math.Max(x, y) - Math.Min(x, y));
            }

            if (MillerRabin(d))
                return d;
            else
                return Rho(d, rd);
        }
    }
    public static ulong SqAdd(ulong x, ulong c, ulong n)
    {
        var v = (BigInteger)x;
        v = v * v + c;
        return (ulong)(v % n);
    }
    public static ulong GCD(ulong a, ulong b)
    {
        while (a != 0 && b != 0)
            if (a > b)
                a %= b;
            else
                b %= a;

        return Math.Max(a, b);
    }
    public static ulong FastPow(BigInteger a, ulong v, ulong mod)
    {
        var rv = BigInteger.One;

        while (v > 0)
        {
            if ((v & 1) == 1)
                rv = (rv * a) % mod;

            a = (a * a) % mod;
            v /= 2;
        }

        return (ulong)rv;
    }
    public static ulong Sq(BigInteger a, ulong mod) => (ulong)((a * a) % mod);
    public static bool MillerRabin(ulong p)
    {
        if (p == 1)
            return false;

        var q = p - 1;
        var k = 0;
        while (q % 2 == 0)
        {
            q /= 2;
            k++;
        }

        foreach (var a in new ulong[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41 })
        {
            if (a >= p)
                break;

            var aq = FastPow(a, q, p);
            if (aq == 1 || aq == p - 1)
                continue;

            var pass = false;
            for (var step = 0; step < k; step++)
            {
                aq = Sq(aq, p);
                if (aq == p - 1)
                {
                    pass = true;
                    break;
                }
            }

            if (!pass)
                return false;
        }

        return true;
    }
}

#elif other3
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

Random random = new Random();
long gcd(long a, long b)
{
    if (b == 0)
        return a;
    return gcd(b, a % b);
}

List<int> millerPrime = new() {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41};

int N = 100000;
List<int> primes = new List<int>();
bool[] notPrime = new bool[N+5];

for (int i=1; i < N;)
{
    if (!notPrime[++i])
    {
        primes.Add(i);
        for (long j = (long)i * i; j <= N; j += i)
            notPrime[j] = true;
    }
}

long AddMod (long a, long b, long m)
{
    a %= m;
    b %= m;
    if (a >= m - b)
        return a + (b - m);
    else
        return a + b;
}

long MulMod (long a, long b, long m)
{
    a %= m;
    b %= m;
    long ret = 0;
    while(b > 0)
    {
        if (b % 2 == 1)
            ret = AddMod(ret, a, m);
        a = AddMod(a, a, m);
        b /= 2;
    }
    return ret;
}

long PowMod (long a, long b, long m)
{
    a %= m;
    long ret = 1;
    while (b > 0)
    {
        if (b % 2 == 1)
            ret = MulMod(ret, a, m);
        a = MulMod(a, a, m);
        b /= 2;
    }
    return ret;
}

bool isPrime(long n)
{
    if (n < N)
        return !notPrime[n];
    if (n < N*N)
    {
        foreach (long x in primes)
            if (n % x == 0)
                return false;
        return true;
    }

    foreach(long x in millerPrime)
    {
        long d = n - 1;
        bool isP = false;
        while(d%2 == 0)
        {
            if (PowMod(x, d, n) == n - 1)
            {
                isP = true;
            }
            d /= 2;
        }
        if(!isP && d%2 == 1)
        {
            long tmp = PowMod(x, d, n);
            isP = (tmp == 1) || (tmp == n - 1);
        }

        if (!isP)
            return false;
    }
    return true;
}

List<long> factors = new();

void pollard(long n)
{
    if(isPrime(n))
    {
        factors.Add(n);
        return;
    }

    long x = (long)random.Next() * random.Next() % n;
    long y = x;
    long count = 2;
    long d = 1;
    long i = 1;
    while(true)
    {
        i++;
        x = (MulMod(x, x, n) - 1 + n)%n;
        d = gcd(Math.Abs(x - y), n);
        if (d != 1)
            break;
        if(i > count)
        {
            count *= 2;
            y = x;
        }
    }
    if(d != n)
    {
        pollard(d);
        pollard(n / d);
        return;
    }
    if(n%2 == 0)
    {
        pollard(2);
        pollard(n / 2);
        return;
    }
    foreach(int p in primes)
    {
        if(n%p == 0)
        {
            pollard(p);
            pollard(n / p);
            return;
        }
    }
}

long input = long.Parse(sr.ReadLine());
pollard(input);
factors.Sort();
foreach (long p in factors)
    sw.WriteLine(p);

sw.Flush();
sr.Close();
sw.Close();
#endif
}
