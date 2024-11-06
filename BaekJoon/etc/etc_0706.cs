using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 18
이름 : 배성훈
내용 : gcd(n, k) = 1
    문제번호 : 13926번

    오일러 피 함수 문제다
    폴라드 로 알고리즘으로 인수 분해를 하고, 밀러 라빈 알고리즘으로 소수 판정을 한다
    큰 수의 소인수 분해에서 다른 사람이 long 곱셈을 정의한 것을 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0706
    {

        static void Main706(string[] args)
        {

            long n = Convert.ToInt64(Console.ReadLine());

            List<long> div = new(64);
            int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };

            Random rand = new();

            PollardRho(n);

            div.Sort();

            long before = 0;
            long ret = 1;

            for (int i = 0; i < div.Count; i++)
            {

                if (before != div[i])
                { 
                    
                    ret *= (div[i] - 1);
                    before = div[i];
                }
                else ret *= div[i];
            }

            Console.WriteLine(ret);

            long GetMul(long _a, long _b, long _mod)
            {

                long ret = 0;

                while(_b > 0)
                {

                    if ((_b & 1L) == 1) ret = (ret + _a) % _mod;

                    _a = (_a + _a) % _mod;
                    _b /= 2;
                }

                return ret;
            }

            long GetPow(long _a, long _b, long _mod)
            {

                long ret = 1;
                while(_b > 0)
                {

                    if ((_b & 1L) == 1) ret = GetMul(ret, _a, _mod);

                    _a = GetMul(_a, _a, _mod);
                    _b = _b / 2;
                }

                return ret;
            }

            long GetGCD(long _a, long _b)
            {

                while(_b > 0)
                {

                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            long F(long _x, long _c, long _mod)
            {

                long ret = GetMul(_x, _x, _mod);
                ret = (ret + _c) % _mod;
                return ret;
            }

            bool IsPrime(long _x)
            {

                for (int i = 0; i < primes.Length; i++)
                {

                    if (_x == primes[i]) return true;
                    if (_x > 40 && MillerRabin(_x, primes[i])) return false;
                }

                if (_x <= 40) return false;
                return true;
            }

            bool MillerRabin(long _x, long _a)
            {

                if (_x % _a == 0) return false;
                long d = _x - 1;

                while (true)
                {

                    long temp = GetPow(_a, d, _x);
                    if ((d & 1L) == 1) return temp != 1 && temp != _x - 1;
                    else if (temp == _x - 1) return false;
                    d /= 2;
                }
            }

            void PollardRho(long _n)
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

                long x = 1L;
                long y = 1L;
                long c = 1L;
                long gcd = _n;

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
                    gcd = GetGCD(Math.Abs(x - y), _n);
                } while (gcd == 1);

                PollardRho(gcd);
                PollardRho(_n / gcd);
            }
        }
    }

#if other
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

Random random = new Random();
long gcd(long a, long b)
{
    if (b == 0)
        return a;
    return gcd(b, a % b);
}

List<int> millerPrime = new() { 2, 325, 9375, 28178, 450775, 9780504, 1795265022 };

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

Dictionary<long, int> factors2 = new();
foreach(long p in factors)
{
    if (factors2.ContainsKey(p))
        factors2[p]++;
    else
        factors2[p] = 1;
}

long ans = 1;
foreach (var kvp in factors2)
{
    if (kvp.Key == 1)
        continue;
    ans *= (kvp.Key - 1);
    for (int i = 0; i < kvp.Value - 1; i++)
        ans *= kvp.Key;
}

sw.WriteLine(ans);

sw.Flush();
sr.Close();
sw.Close();
#elif other2
// #nullable disable

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

        if (n == 1)
        {
            sw.WriteLine(1);
            return;
        }

        var phi = n;
        var factors = new HashSet<ulong>();
        var rd = new Random();
        while (n != 1)
        {
            var f = Rho(n, rd);

            factors.Add(f);
            n /= f;
        }

        foreach (var p in factors)
            phi = phi / p * (p - 1);

        sw.WriteLine(phi);
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
// #include <bits/stdc++.h>
using namespace std;
using ll = long long;
using ull = unsigned long long;

// kactl
// #define rep(i, a, b) for(int i = a; i < (b); ++i)
// #define all(x) begin(x), end(x)
// #define sz(x) (int)(x).size()
typedef pair<int, int> pii;
typedef vector<int> vi;
ull modmul(ull a, ull b, ull M) {
ll ret = a * b - M * ull(1.L / M * a * b);
return ret + M * (ret < 0) - M * (ret >= (ll)M);
}
ull modpow(ull b, ull e, ull mod) {
ull ans = 1;
for (; e; b = modmul(b, b, mod), e /= 2)
if (e & 1) ans = modmul(ans, b, mod);
return ans;
}

bool isPrime(ull n) {
if (n < 2 || n % 6 % 4 != 1) return (n | 1) == 3;
ull A[] = {2, 325, 9375, 28178, 450775, 9780504, 1795265022},
s = __builtin_ctzll(n-1), d = n >> s;
for (ull a : A) {
ull p = modpow(a%n, d, n), i = s;
while (p != 1 && p != n - 1 && a % n && i--)
p = modmul(p, p, n);
if (p != n-1 && i != s) return 0;
}
return 1;
}

ull pollard(ull n) {
auto f = [n](ull x) { return modmul(x, x, n) + 1; };
ull x = 0, y = 0, t = 30, prd = 2, i = 1, q;
while (t++ % 40 || __gcd(prd, n) == 1) {
if (x == y) x = ++i, y = f(x);
if ((q = modmul(prd, max(x,y) - min(x,y), n))) prd = q;
x = f(x), y = f(f(y));
}
return __gcd(prd, n);
}
vector<ull> factor(ull n) {
if (n == 1) return {};
if (isPrime(n)) return {n};
ull x = pollard(n);
auto l = factor(x), r = factor(n / x);
l.insert(l.end(), all(r));
return l;
}

int main() {
    cin.tie(0)->sync_with_stdio(0);
    ll N;
    cin >> N;
    auto a = factor(N);
    sort(all(a));
    a.erase(unique(all(a)), a.end());
    ll ans = N;
    for (auto p : a) {
        ans /= p;
        ans *= p - 1;
    }
    cout << ans << '\n';
    return 0;
}

#endif
}
