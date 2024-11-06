using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 16
이름 : 배성훈
내용 : SUPER HATS
    문제번호 : 11030번

	/////////
    ㅁ[i] = ㅁ ^ (ㅁ[i - 1]), ㅁ[1] = ㅁ 라 하자
    그러면 a[b](mod 10^8) = a[b](mod 2^8) & a[b](mod 5^8)
    a를 2와 5에 관해 모듈러 연산 실행
    a = 2^i * 5^j * c 라하면
    (2^i) ^ (a[b - 1]) * (5^j) ^ (a[b - 1]) * c^(a[b - 1])
    b를 1 줄이는데, 3가지 경우가 생겼다;
    많으면 3^2만...?
    정확하게 구해보자!

    그러면 2 부분만 보자, (2^i) ^ (a[b - 1]) = 2^(i * a[b - 1])
    이고 ( i * a[b - 1] ) > 8 인지 확인해야한다!
    이건 2^2 = 4, 4 ^ 2 = 16 , 3만 되어도 바로 0 이나온다!
    ... ? b와 i 부분이 중요해진다!
    b * i > 4 -> 0이다!
    아니면 하나씩 계산!

    mod는 2이고, 5쪽도 보자 ( j * a[b - 1] ) 
    이는 2^7 - 1과 다시 비교..?
    c부분은 a[b - 1] % phi(10^8); 이된다
    phi(10^8) = phi(2^8) * phi(5^8)이고,
    phi(2^8) = 2^7, phi(5^8) = 4 * 5^7
    i.e phi(10^8) = 2^9 * 5^7이된다
    이렇게 1단계줄인 경우로 왔다

    일단 서로소, 서로소 X로 나눠서 해야할듯?
    서로소면 오일러 정리로 -> 하고 인덱스 1줄인다!

    다른 사람 풀이를 기반으로 풀어봐야겠다!;
    참고한 다른 사람 소스
	other : https://github.com/Joe2357/Baekjoon/blob/master/C/Code/11000/11030%20-%20SUPER%20HATS.c
	other2 : https://github.com/jinhan814/BOJ/blob/main/cpp/11030.cpp

	개념 설명부분은
	다음 사이트를 참고하면 된다
	https://rkm0959.tistory.com/181
	https://lego0901.tistory.com/14
	/////////

	수학, 정수론, 분할 정복을 이용한 거듭제곱, 오일러 피 함수 문제다
	2일간 하루 종일 고민 했고, 위에 적힌거까지 접근했으나, 더 이상 힘들어 깃허브를 찾아보면서 다른 사람 풀이를 봤다
	첫 번째 풀이는 조금 더 일반적인 경우라 생각했고, 두 번째 풀이는 왜 이렇게 되는지 몰라, 오일러 피함수 검색을 통해
	개념 설명 부분을 찾게 되었다
	이후 두 번째 방법으로 풀어 제출했다 (첫 번째가 중국인의 나머지정리를 쓰기에 역원을 찾아야하고 이로 연산이 많아 보였기 때문이다)

	아이디어는 다음과 같다
	밑과 n이 서로소이고 p > phi(n) 인 경우!, 합동식 mod n에서 지수의 경우 a^p = (a^ (p (mod phi(n)))) (mod n)으로 지수 부분의 크기를 줄일 수 있다
	그리고 밑과 n이 서로소가 아닌 경우 + phi(n)가 중국인의 나머지 정리 부분을 해결해주는거 같다 (other 의 분이 중국인의 나머지 정리로 해결하였다)
	이렇게 지수쪽 값을 줄여 찾는게 주된 문제다

	개념 설명 쪽 사이트를 보면, phi(?)이 적당한 시행에서 1이 나온다는 설명이 있다 
	phi(n) < n 이므로 페르마 강하법으로 쉽게 알 수 있다

	n이 1억에서 시작하면 phi(n) <= n / 2가 성립한다 한 번에 phi의 값이 절반 이상이 줄어든다
	그래서 1은 많아야 100번 안으로 나옴을 알 수 있다
	이렇게 분할 정복 거듭제곱을 진행하면서 값을 찾으면 된다

	1억이 넘는 경우 8자리를 표현해줘야한다;(앞에 비어있으면 0으로 채워야 맞는다!, 안하면 2%에서 틀린다!, 3번 틀렸다!)
	비슷한 문제를 Power towers, N과 M 문제를 풀어보면서 개념을 굳혀야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0700
    {

        static void Main700(string[] args)
        {

#if first
			int MOD1 = 1_000_000_000;
			int MOD2 = 100_000_000;
#else
			int MOD2 = 100_000_000;
#endif
			Read(out long a, out int b);

            long[] sq;
            SetSq();

            if (Chk(a, b, MOD2)) Console.WriteLine($"{Find(a, b, MOD2):D8}");
			else Console.WriteLine(Find(a, b, MOD2));

            void SetSq()
            {

                sq = new long[6];

                sq[0] = 1;
                sq[1] = a;
                sq[2] = CalcPow(a, a);
                sq[3] = CalcPow(a, sq[2]);
                sq[4] = CalcPow(a, sq[3]);
				sq[5] = CalcPow(a, sq[4]);
            }

            long CalcPow(long _a, long _b)
            {

                long ret = 1;
                if (_a >= 100_000_000 || _b >= 30) return 100_000_001;

                while (_b > 0 && _a < 100_000_000 && ret < 100_000_000)
                {

                    if ((_b & 1L) == 1) ret = (ret * _a);

                    _b /= 2;
                    if (b > 0) _a = (_a * _a);
                }

                if (_a >= 100_000_000 || ret >= 100_000_000) return 100_000_001;
                return ret;
            }

            int GetPhi(int _n)
            {

                int ret = 1;
                
                for (int i = 2; i <= _n; i++)
                {

                    if (i * i > _n) break;
                    if (_n % i != 0) continue;
                    _n /= i;
                    ret *= (i - 1);

                    while(_n % i == 0)
                    {

                        ret *= i;
						_n /= i;
                    }
                }

                if (_n > 1) ret *= _n - 1;
                return ret;
            }

            long GetPow(long _a, long _b, long _mod)
            {

                long ret = 1;

                while(_b > 0)
                {

                    if ((_b & 1L) == 1) ret = (ret * _a) % _mod;

                    _a = (_a * _a) % _mod;
                    _b /= 2;
                }

                return ret;
            }

            void Read(out long _a, out int _b)
            {

                string[] temp = Console.ReadLine().Split();
                _a = long.Parse(temp[0]);
                _b = int.Parse(temp[1]);
            }
#if first
			bool Chk(long _a, long _b, int _mod)
			{

				if (_a == 1 || _b == 0) return 1 >= _mod;
				long t = _a;

				while(-- _b > 0)
				{

					if (t >= 10) return true;
					t = GetPow(t, t, MOD1);
				}

				return t >= _mod;
			}
#else

            bool Chk(long _a, long _b, int _mod)
            {

                if (_a == 1 || _b == 0) return 1 >= _mod;

                if (_b > 5) return true;
                return sq[_b] >= _mod;
            }
#endif
            long Find(long _a, long _b, int _mod)
			{

				if (_mod == 1) return 0;
				if (_b == 1) return _a % _mod;
				int phi = GetPhi(_mod);

				if (GetGCD(_a, _mod) == 1) return GetPow(_a, Find(_a, _b - 1, phi), _mod);
				return GetPow(_a, Find(_a, _b - 1, phi) + (Chk(_a, _b - 1, phi) ? 1 : 0) * phi, _mod); 
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
        }
    }

#if other
// #include <stdio.h>
typedef long long ll;
// #define MAX_IDX (int)(20000 + 3)
ll arr[MAX_IDX];

ll phi(ll x) {
	ll retval = 1;
	for (ll i = 2; i * i <= x; ++i) {
		ll k = 0;
		ll p = 1;
		while (x % i == 0) {
			++k, p *= i;
			x /= i;
		}

		if (k > 0) {
			p /= i;
			retval *= (p * (i - 1));
		}
	}

	if (x > 1) {
		retval *= (x - 1);
	}
	return retval;
}
ll gcd(ll a, ll b) {
	ll temp = 0;
	while (b != 0) {
		temp = a % b;
		a = b;
		b = temp;
	}
	return a;
}
ll inverse(ll a, ll m) {
	ll m0 = m;
	ll y = 0;
	ll x = 1;

	if (m == 1) {
		return 0;
	}
	while (a > 1) {
		ll q = a / m;
		ll t = m;

		m = a % m;
		a = t;
		t = y;

		y = x - q * y;
		x = t;
	}

	if (x < 0) {
		x = x + m0;
	}

	return x;
}
ll chineseRemainder(ll mod, ll pe, ll r) { return ((mod / pe) * (inverse(mod / pe, pe) * r)); }
ll powWithModulo(ll x, ll y, ll mod) {
	ll ret = 0;
	if (y == 0) {
		return 1;
	} else if (y == 1) {
		ret = x;
	} else {
		ll xx = powWithModulo(x, y / 2, mod);
		if (y % 2 == 0) {
			ret = xx * xx;
		} else {
			ret = (xx * xx % mod) * x;
		}
	}
	return ret % mod;
}

// #define INF 31
ll powUntilMax(ll x, ll y) {
	ll ret = 0;
	if (y == 0) {
		return 1;
	} else if (y == 1) {
		ret = x;
	} else {
		ll xx = powUntilMax(x, y / 2);
		if (xx >= INF) {
			return INF;
		}
		if (y % 2 == 0) {
			ret = xx * xx;
		} else {
			ret = (xx * xx) * x;
		}
	}
	return ret;
}
ll getPower(ll s, ll e, ll cur) {
	if (s == e) {
		return 0;
	} else if (s + 1 == e) {
		return powUntilMax(arr[s], cur);
	}
	if (cur >= INF) {
		return INF;
	} else if (arr[s] >= INF) {
		return INF;
	} else if (arr[e - 1] >= INF) {
		return INF;
	}

	return getPower(s, e - 1, powUntilMax(arr[e - 1], cur));
}

ll solve(ll s, ll e, ll mod) {
	if (mod == 1) {
		return 0;
	} else if (arr[s] == 1) {
		return 1;
	} else if (s + 1 == e) {
		return arr[s] % mod;
	}

	if (gcd(arr[s], mod) == 1) {
		return powWithModulo(arr[s], solve(s + 1, e, phi(mod)), mod);
	}

	// ai and mod is not coprime
	// loop for mod's prime set -> chinese remainder theorem
	ll subMod = mod;
	ll ret = 0;

	for (ll i = 2; i * i <= subMod; ++i) {
		ll cnt = 0;
		ll pe = 1;
		while (subMod % i == 0) {
			++cnt, pe *= i;
			subMod /= i;
		}

		if (cnt > 0) {
			if (gcd(arr[s], i) == 1) {
				ret += chineseRemainder(mod, pe, powWithModulo(arr[s], solve(s + 1, e, phi(pe)), pe));
			} else {
				ll powerValue = getPower(s + 1, e, 1);
				if (powerValue < cnt) {
					ret += chineseRemainder(mod, pe, powWithModulo(arr[s], powerValue, pe));
				}
			}
		}
	}

	if (subMod > 1) {
		ll cnt = 1, pe = subMod;
		if (gcd(arr[s], subMod) == 1) {
			ret += chineseRemainder(mod, pe, powWithModulo(arr[s], solve(s + 1, e, phi(pe)), pe));
		}
	}

	return ret % mod;
}

ll main() {
	ll a, b;
	scanf("%lld %lld", &a, &b);
	for (int i = 0; i < b; ++i) {
		arr[i] = a;
	}

// #define STD (ll)(1e8)
	ll ans = solve(0, b, STD);
	// printf("ans : %lld\n", ans);

	if (a == 1) {
		printf("%lld", ans);
	} else if (a == 2) {
		if (b > 4) {
			printf("%08lld", ans);
		} else {
			printf("%lld", ans);
		}
	} else if (b == 1) {
		printf("%lld", ans);
	} else if (b == 2) {
		if (a < 9) {
			printf("%lld", ans);
		} else {
			printf("%08lld", ans);
		}
	} else {
		printf("%08lld", ans);
	}
	return 0;
}
#elif other2
// #include <bits/stdc++.h>
// #define fastio cin.tie(0)->sync_with_stdio(0)
using namespace std;

using i64 = long long;

int Pow(int x, int n, const int m) {
	int ret = 1; x %= m;
	for (; n; n >>= 1) {
		if (n & 1) ret = (i64)ret * x % m;
		x = (i64)x * x % m;
	}
	return ret;
}

int Phi(int n) {
	int ret = n;
	for (int i = 2; i * i <= n; i++) {
		if (n % i) continue;
		ret = ret / i * (i - 1);
		while (n % i == 0) n /= i;
	}
	if (n > 1) ret = ret / n * (n - 1);
	return ret;
}

bool Check(int x, int n, const int m) {
	if (x == 1 || n == 0) return 1 >= m;
	int t = x;
	while (--n) { if (t >= 10) return 1; t = Pow(t, t, 1e9); }
	return t >= m;
}

int F(const int x, const int n, const int m) {
	if (m == 1) return 0;
	if (n == 1) return x % m;
	const int phi = Phi(m);
	if (gcd(x, m) == 1) return Pow(x, F(x, n - 1, phi), m);
	return Pow(x, F(x, n - 1, phi) + Check(x, n - 1, phi) * phi, m);
}

int main() {
	fastio;
	int a, b; cin >> a >> b;
	if (Check(a, b, 1e8)) cout << setw(8) << setfill('0');
	cout << F(a, b, 1e8) << '\n';
}

#elif other3
// #include <stdio.h>
// #define R 100000000
// #define N 20010

typedef long long ld;
ld n, b, i, a[N], l;
ld pp(ld n, ld k){
    ld v = 1;
    while(k){
        if (k%2)
            v *= n, v %= R;
        n *= n, k /= 2;
        n %= R;
    }
    return v % R;
}

ld ll(ld n){
    ld c = 0;
    if (n) {
        while(n)
            c++, n /= 10;
    } else
        c = 1;
    return c;
}

int main() {
    a[0] = 1;
    scanf("%lld %lld", &n, &b);
    for (i=1; i<=b; i++)
        if (a[i-1])
            a[i] = pp(n, a[i-1]);
    l = ll(a[b]);
    if (2 < b && 2 < n || (b == 2 && 10 <= n))
        if (l < 8)
            for (i=0; i<8-l; i++)
                printf("0");
    printf("%lld", a[b]);
}
#elif other4
// #include <stdio.h>
// #define mod 100000000

long long PHI (long long X); // euler totient function

long long power_modular (long long X, long long Y, long long M); // X ^ Y mod M
long long tetration_modular (long long a, long long b, long long M); //a ^^ b mod M

int main() {
	long long a, b;
	scanf("%lld %lld", &a, &b);
		
	long long result = tetration_modular (a, b, mod);

	if (a == 1) {
		printf("1");
	}
	else if (a <= 2 && b < 5) {
		printf("%lld", result);
	}
	else if (a < 9 && b < 3) {
		printf("%lld", result);
	}
	else if (b == 1) {
		printf("%lld", result);
	}
	else {
		printf("%08lld", result);
	}
	return 0;
}

long long PHI (long long N) { // euler totient function, phi(N)
	long long result = N;
	for (long long i = 2; i * i <= N; i++) {
		if (N % i == 0) {
			while (N % i == 0) {
				N /= i;
			}
			result -= result / i;
		}
	}
	if (N > 1) {
		result -= result / N;
	}
	return result;
}

long long power_modular (long long X, long long Y, long long M) { // X ^ Y mod M
	long long result = 1;
	while (Y) {
		if (Y % 2) {
			result = (result * X) % M;
		}
		X = (X * X) % M;
		Y /= 2;
	}
	return result;
}

long long tetration_modular (long long a, long long b, long long M) { // a ^^ b mod M
	if (M == 1) {
		return 0;
	}
	else if (b == 1) {
		return a;
	}
	else {
		long long Power = tetration_modular (a, b-1, PHI(M));
		if (Power == 0) {
			Power += PHI(M);
		}
		long long result = power_modular (a, Power, M);
		return result;
	}
}
#endif
}
