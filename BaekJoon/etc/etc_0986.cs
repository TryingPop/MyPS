using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 5
이름 : 배성훈
내용 : 제곱수의 합 (More Huge)
    문제번호 : 17633번

    수학, 정수론, 소수 판정, 플라드 로, 밀러 라빈 소수 판별 문제다
    라그랑주 네 제곱수 정리와 르장드르 세 제곱수 정리, 페르마의 두 제곱수 정리를 이용하면 된다
    
    라그랑주 네 제곱수 정리는 
    모든 자연수는 4개 이하의 제곱수의 합으로 표현할 수 있다
    그래서 최댓값은 4이다

    르장드르 세 제곱수 정리는 
    (4^a) x (8b + 7)의 형태가 아닌 경우 3개 이하의 제곱수의 합으로 표현될 수 있다
    그래서 (4^a) x (8b + 7)해당 형태면 4개로 판정했다

    페르마의 두 제곱수 정리는
    https://en.wikipedia.org/wiki/Fermat%27s_theorem_on_sums_of_two_squares
    디오판투스 항등성으로 4n + 1 형태의 수들로만 이루어지면 2개로 표현할 수 있다
    즉 4n + 3 형태의 소수의 지수가 홀수면 3개이다

    그리고 제곱수면 1개가 자명하므로 1개로 판별했다
    이외는 2개로 제출하니 통과한다

    페르마의 두 제곱수 정리에서 소인수 분해가 필요하다
    이 때 플라드 로 알고리즘과 밀러 라빈 판정법을 써야한다

    시간초과 나서 이전 플라드 로와 밀러 라빈 판정법을 썼던
    gcd(n, k) = 1 문제의 코드를 가져왔고 통과했다

    div를 정렬 안했는데도 운 좋게 통과되었다;
    이후 정렬시켜 찾았다

    제출 코드와 비교하면서 시간초과 원인을 분석하니
    오버플로우 문제였고 해당 부분을 수정하니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_0986
    {

        static void Main986(string[] args)
        {

#if FIRST
            double E = 1e-9;
            long n;
            long[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };
            List<long> div = new(64);
            Random rand;

            Solve();
            void Solve()
            {

                n = long.Parse(Console.ReadLine());
                rand = new();

                if (ChkFour(n))
                    Console.Write(4);
                else if (ChkThree(n))
                    Console.Write(3);
                else if (ChkOne(n))
                    Console.Write(1);
                else
                    Console.Write(2);
            }

            bool ChkFour(long _n)
            {

                while (_n % 4 == 0)
                {

                    _n /= 4;
                }

                return _n % 8 == 7;
            }

            bool ChkThree(long _n)
            {

                PollardRho(_n);

                div.Sort();
                int cnt = 0;
                long before = 1;
                for (int i = 0; i < div.Count; i++)
                {

                    if (before != div[i])
                    {

                        if ((cnt & 1) == 1) return true;
                        before = div[i];
                        cnt = 0;
                    }

                    if (div[i] % 4 == 3) cnt++;
                }

                return (cnt & 1) == 1;
            }

            bool ChkOne(long _n)
            {

                long sqrt = (long)(Math.Sqrt(_n) + E);
                return sqrt * sqrt == _n;
            }

            long GetMul(long _a, long _b, long _mod)
            {

                long ret = 0;

                while (_b > 0)
                {

                    if ((_b & 1L) == 1L) ret = (ret + _a) % _mod;

                    _a = (_a + _a) % _mod;
                    _b >>= 1;
                }

                return ret;
            }

            long GetPow(long _a, long _b, long _mod)
            {

                long ret = 1;
                while (_b > 0)
                {

                    if ((_b & 1L) == 1L) ret = GetMul(ret, _a, _mod);

                    _a = GetMul(_a, _a, _mod);
                    _b >>= 1;
                }

                return ret;
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
                    d >>= 1;
                }
            }

            void PollardRho(long _n)
            {

                if (_n == 1) return;

                if (_n % 2 == 0)
                {

                    div.Add(2);
                    PollardRho(_n >> 1);
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
#else

            double E = 1e-9;
            long n;
            long[] chk = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };
            List<long> div;
            Random rand;

            Solve();
            void Solve()
            {

                n = long.Parse(Console.ReadLine());
                rand = new();
                div = new(64);
                if (ChkFour(n))
                    Console.Write(4);
                else if (ChkThree(n))
                    Console.Write(3);
                else if (ChkOne(n))
                    Console.Write(1);
                else
                    Console.Write(2);
            }

            bool ChkFour(long _n)
            {

                while (_n % 4 == 0)
                {

                    _n /= 4;
                }

                return _n % 8 == 7;
            }

            bool ChkThree(long _n)
            {

                while (_n > 1)
                {

                    long d = PollardRho(_n);
                    _n /= d;
                    div.Add(d);
                }

                div.Sort();
                long before = 1;
                int cnt = 0;

                for (int i = 0; i < div.Count; i++)
                {

                    if (div[i] != before)
                    {

                        if ((cnt & 1) == 1) return true;
                        before = div[i];
                        cnt = 0;
                    }

                    if (div[i] % 4 == 3) cnt++;
                }

                div.Clear();
                return (cnt & 1) == 1;
            }

            bool ChkOne(long _n)
            {

                long sqrt = (long)(Math.Sqrt(_n) + E);
                return sqrt * sqrt == _n;
            }

            long GetMul(long _a, long _mul, long _mod)
            {

                long ret = 0;
                while (_mul > 0)
                {

                    if ((_mul & 1L) == 1L) ret = (ret + _a) % _mod;
                    _a = (_a + _a) % _mod;
                    _mul >>= 1;
                }

                return ret;
            }

            long GetPow(long _a, long _exp, long _mod)
            {

                long ret = 1;
                while (_exp > 0)
                {

                    if ((_exp & 1L) == 1L) ret = GetMul(ret, _a, _mod);

                    _a = GetMul(_a, _a, _mod);
                    _exp >>= 1;
                }

                return ret;
            }

            bool MillerRabin(long _n)
            {

                for (int i = 0; i < chk.Length; i++)
                {

                    if (chk[i] == _n) return true;
                    else if (_n > 40 && ChkNotPrime(_n, chk[i])) return false;
                }

                if (_n <= 40) return false;
                return true;
            }

            bool ChkNotPrime(long _n, long _a)
            {

                if (_n <= 1) return true;

                long d = _n - 1;
                while (true)
                {

                    long temp = GetPow(_a, d, _n);

                    if (temp == _n - 1) return false;
                    else if (temp != 1) return true;

                    if ((d & 1L) == 1L) return temp != 1 && temp != _n - 1;
                    d >>= 1;
                }
            }

            long PollardRho(long _n)
            {

                if (_n == 1) return _n;
                else if ((_n & 1L) == 0L) return 2;
                else if (MillerRabin(_n)) return _n;


                long x = rand.NextInt64() % (_n - 2) + 2;
                long y = x;
                long c = rand.NextInt64() % (_n - 1) + 1;
                long d = 1;

                while (d == 1)
                {

                    x = F(x, c, _n);
                    y = F(y, c, _n);
                    y = F(y, c, _n);

                    d = GetGCD(Math.Abs(x - y), _n);

                    if (d == _n) return PollardRho(_n);
                }

                if (MillerRabin(d)) return d;
                return PollardRho(d);
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

            long F(long _a, long _c, long _mod)
            {

                return (GetMul(_a, _a, _mod) + _c) % _mod;
            }
#endif
        }
    }

#if other
// #include<bits/stdc++.h>
using namespace std;
typedef long long int ll;
using ull = unsigned long long;
ull gcd(ull x,ull y)
{
    if(x>y) swap(x,y); if(x==0) return y; return gcd(x,y%x);
}
ull mod_mul(ull a, ull b, ull M){
	long long res = a * b - M * ull(1.L / M * a * b);
	return res + M * (res < 0) - M * (res >= (long long)M);
}
ull mod_pow(ull b, ull e, ull mod){
	ull res = 1;
	for(; e; b = mod_mul(b, b, mod), e >>= 1) if(e & 1) res = mod_mul(res, b, mod);
	return res;
}
// Millar Rabin Primality Test
// 7 times slower than a^b mod c
bool isprime(ull n){
	if(n < 2 || n % 6 % 4 != 1) return (n | 1) == 3;
	ull s = __builtin_ctzll(n - 1), d = n >> s;
	for(ull a: {2, 325, 9375, 28178, 450775, 9780504, 1795265022}){
		ull p = mod_pow(a, d, n), i = s;
		while(p != 1 && p != n - 1 && a % n && i --) p = mod_mul(p, p, n);
		if(p != n - 1 && i != s) return false;
	}
	return true;
}
// Pollard rho algorithm
// O(n^1/4)
ull get_factor(ull n){
	auto f = [n](ull x){ return mod_mul(x, x, n) + 1; };
	ull x = 0, y = 0, t = 30, prd = 2, i = 1, q;
	while(t ++ % 40 || gcd(prd, n) == 1){
		if(x == y) x = ++ i, y = f(x);
		if(q = mod_mul(prd, max(x, y) - min(x, y), n)) prd = q;
		x = f(x), y = f(f(y));
	}
	return gcd(prd, n);
}
// Returns the prime factors in arbitrary order
vector<ll> factorize(ull n){
	if(n == 1) return {};
	if(isprime(n)) return {ll(n)};
	ull x = get_factor(n);
	auto l = factorize(x), r = factorize(n / x);
	l.insert(l.end(), r.begin(), r.end());
	return l;
}
int main()
{
    ll n,i,t,x,y;
    scanf("%lld",&n);
    vector<ll> v=factorize(n);
    sort(v.begin(),v.end());
    if(v.size()%2==0)
    {
        t=1;
        for(i=0;i<v.size();i+=2)
        {
            if(v[i]!=v[i+1])
                t=0;
        }
        if(t)
        {
            printf("1");
            return 0;
        }
    }
    x=0;
    y=0;
    t=1;
    for(i=0;i<v.size();i++)
    {
        if(v[i]==x)
            y++;
        else
        {
            if(x%4==3&&y%2==1)
                t=0;
            y=1;
            x=v[i];
        }
        if(v[i]==2)
            continue;
    }
    if(x%4==3&&y%2==1)
        t=0;
        if(t==1)
        {
            printf("2");
            return 0;
        }
    while(n%4==0)
    {
        n/=4;
    }
    if(n%8!=7)
    {
        printf("3");
        return 0;
    }
    printf("4");
}

#endif
}
