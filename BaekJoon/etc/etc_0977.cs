using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 19
이름 : 배성훈
내용 : Pibonacci
    문제번호 : 1737번

    dp 문제다
    pi가 초월수이므로 a, b가 정수일 때 a + b * pi인 a, b는 유일하다
*/

namespace BaekJoon.etc
{
    internal class etc_0977
    {

        static void Main977(string[] args)
        {

            long MOD = 1_000_000_000_000_000_000L;
            long[][] dp;
            int n;

            Solve();
            void Solve()
            {

                Input();

                long ret = DFS(n, 0);
                Console.Write(ret);
            }

            long DFS(int _f, int _b)
            {

                if (dp[_f][_b] != -1) return dp[_f][_b];
                else if (_f - _b * Math.PI <= Math.PI)
                {

                    dp[_f][_b] = 1;
                    return 1;
                }

                long ret = (DFS(_f - 1, _b) + DFS(_f, _b + 1)) % MOD;
                return dp[_f][_b] = ret;
            }

            void Input()
            {

                n = int.Parse(Console.ReadLine());

                dp = new long[Math.Max(n + 1, 4)][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new long[333];
                    Array.Fill(dp[i], -1);
                }
                dp[0][0] = 1;
                dp[1][0] = 1;
                dp[2][0] = 1;
                dp[3][0] = 1;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;

public static class Program
{
    public struct Number : IEquatable<Number>
    {
        public int IntPart;
        public int PiPart;

        public Number(int intPart, int piPart)
        {
            IntPart = intPart;
            PiPart = piPart;
        }
        public bool Equals(Number other)
        {
            return IntPart == other.IntPart
                && PiPart == other.PiPart;
        }
    }

    public static void Main()
    {
        var n = Int32.Parse(Console.ReadLine());
        var memo = new Dictionary<Number, long>();

        var f = Fib(new Number(n, 0), memo);
        Console.WriteLine(f);
    }

    public static long Fib(Number n, Dictionary<Number, long> memo)
    {
        if (n.IntPart + n.PiPart * Math.PI < Math.PI)
            return 1;

        var n1 = new Number(n.IntPart - 1, n.PiPart);
        var n2 = new Number(n.IntPart, n.PiPart - 1);
        long v1, v2;

        if (memo.ContainsKey(n1))
            v1 = memo[n1];
        else
            v1 = Fib(n1, memo);

        if (memo.ContainsKey(n2))
            v2 = memo[n2];
        else
            v2 = Fib(n2, memo);

        var sum = (v1 + v2) % 1000000000000000000;
        memo.Add(n, sum);

        return sum;
    }
}

#elif other2
// #include <bits/stdc++.h>
// #define PI 3.14159265358979323846
// #define MOD 1000000000000000000
// #define MOD2 262144
// #define MOD5 3814697265625
// #define SIZE 1000
using namespace std;
typedef long long ll;
typedef pair<ll, ll> pi;
ll fact2[SIZE], invfact2[SIZE];
ll fact5[SIZE], invfact5[SIZE];
ll div2[SIZE], div5[SIZE];
ll fexp(ll a, ll b, ll m) {
    ll res = 1;
    for (; b > 0; b >>= 1) {
        if (b & 1) res = (__int128_t)res * a % m;
        a = (__int128_t)a * a % m;
    }
    return res;
}
pi euclid(ll a, ll b) { 
    if (b == 0) return {1, 0};
    pi ret = euclid(b, a % b);
    ll x = ret.first, y = ret.second;
    return {y, x - (a / b) * y};   
}
ll modinv(ll a, ll m) {
    pi res = euclid(a, m);
    return (res.first + m) % m;
}
void precompute() {
    fact2[0] = 1; fact5[0] = 1;
    div2[2] = 1, div5[5] = 1;
    for (int i = 3; i < SIZE; i++) {
        ll cnt2 = 0, cnt5 = 0, cur = i;
        while (cur % 2 == 0) {
            cur >>= 1;
            cnt2++;
        }
        while (cur % 5 == 0) {
            cur /= 5;
            cnt5++;
        }
        div2[i] = div2[i - 1] + cnt2;
        div5[i] = div5[i - 1] + cnt5;
    }
    for (int i = 1; i < SIZE; i++) {
        ll cur2 = i, cur5 = i;
        while (cur2 % 2 == 0) cur2 >>= 1;
        while (cur5 % 5 == 0) cur5 /= 5;
        fact2[i] = fact2[i - 1] * cur2 % MOD2;
        fact5[i] = fact5[i - 1] * cur5 % MOD5;
    }
    for (int i = 0; i < SIZE; i++) invfact2[i] = modinv(fact2[i], MOD2);
    for (int i = 0; i < SIZE; i++) invfact5[i] = modinv(fact5[i], MOD5);
}
ll combination(int n, int k) {
    ll p2 = div2[n] - div2[k] - div2[n - k];
    ll p5 = div5[n] - div5[k] - div5[n - k];
    ll res2 = fact2[n] * invfact2[k] % MOD2;
    res2 = res2 * invfact2[n - k] % MOD2;
    res2 = res2 * fexp(2, p2, MOD2) % MOD2;
    ll res5 = (__int128_t)fact5[n] * invfact5[k] % MOD5;
    res5 = (__int128_t)res5 * invfact5[n - k] % MOD5;
    res5 = (__int128_t)res5 * fexp(5, p5, MOD5) % MOD5;
    return ((__int128_t)res2 * MOD5 % MOD * 67177 % MOD + (__int128_t)res5 * MOD2 % MOD * 2837143256329LL % MOD) % MOD;
}
int main() {
	ios::sync_with_stdio(0); cin.tie(0); precompute();
	ll n, ans = 1; cin >> n; vector<ll> chks; chks.push_back(0);
    for (int i = 0; i < n; i++) if ((int)(i / PI) != (int)((i + 1) / PI)) chks.push_back(i + 1);
    for (int i = 1; i < chks.size(); i++) ans = (ans + combination(n - chks[i] + i, i)) % MOD;
    cout << ans << '\n';
	return 0;
}
#elif other3
// #define _CRT_SECURE_NO_WARNINGS
// #include <stdio.h>
// #define _USE_MATH_DEFINES
// #include <math.h>
// #include <stdlib.h>
// #define MOD 1000000000000000000

long long pibonacci(int n,long long** arr);
long long pibo(int n, int k, long long** arr);

int main()
{
	int n;
	long long** arr;

	scanf("%d", &n);

	if (n >= 4)
	{
		arr = (long long**)calloc(n + 1, sizeof(long long*));

		printf("%lld", pibonacci(n, arr) % MOD);
	}
	else
		printf("1");

	return 0;
}

long long pibonacci(int n, long long** arr)
{
	arr[3] = (long long*)calloc(1, sizeof(long long));
	arr[3][0] = { 1 };

	for (int i = 4; i <= n; i++)
		arr[i] = (long long*)calloc(int(i / M_PI)+1, sizeof(long long));

	for (int i = 4; i <= n; i++)
		arr[i][int(i/M_PI)] = 1;

	for (int i = 4; i <= n; i++)
		arr[i][0] = pibo(i, 0, arr);

	return arr[n][0]%MOD;
}

long long pibo(int n, int k, long long** arr)
{
	if (arr[n][k + 1] != 0)
	{
		return arr[n][k + 1]%MOD + arr[n - 1][k]%MOD;
	}

	else
	{
		arr[n][k+1] = pibo(n, k + 1, arr);

		return arr[n][k + 1] % MOD + arr[n - 1][k] % MOD;
	}
}
#endif
}
