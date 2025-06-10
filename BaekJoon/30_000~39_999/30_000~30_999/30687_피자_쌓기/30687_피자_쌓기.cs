using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 10
이름 : 배성훈
내용 : 피자 쌓기
    문제번호 : 30687번

    수학, 조합론 문제다.
    전체 경우에서 i크기의 피자가 보이는 경우를 확인한다.
    이는 자기보다 큰 것들보다 앞에 있어야 한다.
    
    그래서 에 대해 전체 경우 중 ai / (∑aj) (i ≤ j)만 크기 i가 보인다.
    s = ∑ai (1 ≤ i ≤ n)이라하면
    전체 경우는 s! / (a1! x a2! x a3! x ... x an!)이 된다.

    이 둘을 이용해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1692
    {

        static void Main1692(string[] args)
        {

            int MOD = 1_000_000_007;
            int n, sum;
            int[] cnt;
            long[] fac;

            Input();

            GetRet();

            void GetRet()
            {

                long s = fac[sum];
                for (int i = 0; i < n; i++)
                {

                    long inv = GetInv(fac[cnt[i]]);
                    s = (s * inv) % MOD;
                }

                long ret = 0;
                for (int i = 0; i < n; i++)
                {

                    long inv = GetInv(sum);
                    long cur = s * ((inv * cnt[i]) % MOD) % MOD;
                    ret = (ret + cur) % MOD; 
                    sum -= cnt[i];
                }

                Console.Write(ret);

                long GetInv(long _a)
                    => GetPow(_a, MOD - 2);


                long GetPow(long _a, int _exp)
                {

                    long ret = 1;
                    while (_exp > 0)
                    {

                        if ((_exp & 1) == 1) ret = (ret * _a) % MOD;
                        _a = (_a * _a) % MOD;
                        _exp >>= 1;
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                sum = 0;

                cnt = new int[n];
                for (int i = 0; i < n; i++)
                {

                    cnt[i] = ReadInt();
                    sum += cnt[i];
                }

                fac = new long[sum + 1];
                fac[0] = 1;
                for (int i = 1; i <= sum; i++)
                {

                    fac[i] = (i * fac[i - 1]) % MOD;
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
// #define int long long
namespace math{
	constexpr int MOD = (int)1e9 + 7;
	constexpr int MN = 500'050;
	struct mint {
		int n;
		mint(int n_): n(n_) { }
		mint(): n(0){}
		mint operator+(mint o) {
			int ret = n + o.n;
			if (ret >= MOD) ret -= MOD;
			return ret;
		}
		mint operator-(mint o) {
			int ret = n - o.n;
			if (ret < 0) ret += MOD;
			return ret;
		}
		mint operator*(mint o){return (int)((1LL * n * o.n) % MOD);}
		mint operator/(mint o);
	};
	mint fact[MN], inv_fact[MN];
	mint power(mint a, int n) {
		mint res = 1;
		for (;n; n >>= 1, a = a * a) if (n & 1) res = res * a;
		return res;
	}
	mint inv(int a) { return inv_fact[a]*fact[a-1]; }
	mint mint::operator/(mint o){return o.n >= MN ? *this * power(o.n, MOD - 2) : *this * inv(o.n);}
	void init_fact() {
		fact[0]=1;
		for (int i=1;i<MN;++i) fact[i] = fact[i-1]*i;
		inv_fact[MN-1] = power(fact[MN-1], MOD - 2);
		for (int i=MN-1; i>0;--i) inv_fact[i-1]=inv_fact[i]*i;
	}
	mint C(int n, int r) { return r < 0 || n < r ? 0 : fact[n] * inv_fact[n - r] * inv_fact[r]; }
};
using namespace math;
signed main() {
	cin.tie(0)->sync_with_stdio(false);
	init_fact();
	int n;
	cin >> n;
	vector<int> a(n);
	for (int i = 0; i < n; ++i) {
		cin >> a[i];
	}
	mint S = 0;
	mint psum = 0;
	mint if_mul = 1;
	for (int i = n - 1; i >= 0; --i) {
		S = S + a[i];
		mint prob = mint(a[i]) / S;
		psum = psum + prob;
		if_mul = if_mul * inv_fact[a[i]];
	}
	mint ans = psum * fact[S.n] * if_mul;
	cout << ans.n << '\n';
	return 0;
}
#endif
}
