using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

/*
날짜 : 2024. 4. 15
이름 : 배성훈
내용 : A + B
    문제번호 : 9267번

    확장 유클리드 호제법, 수학, 정수론 문제다

    해당 사이트 풀이 참고해서 코드를 짰다
    https://rkm0959.tistory.com/96

    아이디어는 다음과 같다
    a += b, b += a형태로 a, b에 값이 담긴다
    여기서 0이 담기는 경우가 있어 반례처리를 먼저 한다

    해당 셀을 보면 s = aX + bY 중 하나이다
    그래서 s가 gcd(a, b)에 무조건 나눠 떨어져야한다!
    그래서 s가 gcd(a, b)에 나눠 떨어지는지 먼저 검증했다

    그리고 입력은 한 번씩 이뤄지므로 명령 입력은
    확장된 유클리드 호제법 연산과 같음을 알 수 있다

    수가 크기에 gcd를 모두 나눠 크기를 줄였다
    여기서부터는 이제 s = aX + bY의 해가 존재한다!
    안하는 경우는 결론 내고 탈출한다

    이제 적당한 하나의 해 x0, y0를 찾는다
    그리고 x0, y0를 b보다 작은 음이아닌 정수해가 되게 세팅한다
    해를 찾을 때 오버플로우가 발생할 확률이 높아 BigInteger로 연산했다

    이제 y0의 값을 a씩 줄여가면서 양수해가 존재하는지 카운팅 했다
    해당 부분은 long으로 연산하게 만들 수 있어 long 자료형으로 다시 돌아왔다
    그리고 명령어 인지 확인은 유클리드 호제법 연산과 같기에 gcd로 판별해도 충분하다

    그러면 드는 의문은 시간 복잡도 인데, 이는 2^15을 넘지 않는다고 한다
    참고한 사이트에 증명되어져 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0524
    {

        static void Main524(string[] args)
        {

            string YES = "YES";
            string NO = "NO";

            BigInteger a = 0;
            BigInteger b = 0;
            BigInteger s = 0;

            long x = 0;
            long y = 0;
            long z = 0;

            Solve();

            bool PreCalc()
            {

                if (Input()) return true;

                long gcd = GetLongGCD(x, y);
                if (z % gcd != 0)
                {

                    Console.WriteLine(NO);
                    return true;
                }

                x /= gcd;
                y /= gcd;
                z /= gcd;
                return false;
            }

            void GetMin(BigInteger _iA, BigInteger _iB, out long _x, out long _y)
            {

                _iA = _iA * s;
                _iB = _iB * s;

                BigInteger mul = 0;
                BigInteger calc = b;
                if (_iA < 0)
                {

                    mul = _iA / calc;
                    if (_iA % calc != 0) mul--;

                    _iA -= mul * calc;
                }
                else
                {

                    mul = _iA / calc;
                    _iA -= mul * calc;
                }

                calc = a;
                _iB += mul * calc;

                _x = (long)_iA;
                _y = (long)_iB;
            }

            void Solve()
            {

                if (PreCalc()) return;

                a = x;
                b = y;
                s = z;

                GetGCD(a, b, out BigInteger iA, out BigInteger iB);
                GetMin(iA, iB, out long x0, out long y0);

                while(GetLongGCD(x0, y0) != 1 && y0 >= 0)
                {

                    x0 += y;
                    y0 -= x;
                }

                if (y0 < 0) Console.WriteLine(NO);
                else Console.WriteLine(YES);
            }

            bool Input()
            {

                long[] input = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
                if (input[0] == 0)
                {

                    if (input[1] == 0)
                    {

                        Console.WriteLine(input[2] == 0 ? YES : NO);
                        return true;
                    }

                    long temp = input[1];
                    input[1] = input[0];
                    input[0] = temp;
                }

                if (input[1] == 0)
                {

                    Console.WriteLine(input[2] % input[0] == 0 ? YES : NO);
                    return true;
                }

                x = input[0];
                y = input[1];
                z = input[2];

                return false;
            }

            long GetLongGCD(long _x, long _y)
            {

                long temp;
                while(_y > 0)
                {

                    temp = _x % _y;
                    _x = _y;
                    _y = temp;
                }

                return _x;
            }

            BigInteger GetGCD(BigInteger _a, BigInteger _b, out BigInteger _iA, out BigInteger _iB)
            {

                BigInteger s1 = 1;
                BigInteger s2 = 0;

                BigInteger t1 = 0;
                BigInteger t2 = 1;

                BigInteger q, temp;
                while (_b != 0)
                {

                    temp = _a % _b;
                    q = (_a - temp) / _b;
                    _a = _b;
                    _b = temp;

                    temp = -q * s2 + s1;
                    s1 = s2;
                    s2 = temp;

                    temp = -q * t2 + t1;
                    t1 = t2;
                    t2 = temp;
                }

                _iA = s1;
                _iB = t1;
                return _a;
            }
        }
    }

#if other
import java.util.*;
import java.io.*;
import java.math.*;

public class Main {
	static BigInteger x,y;
	final static BigInteger zero = BigInteger.ZERO;
	final static BigInteger one = BigInteger.ONE;
	final static BigInteger mone = new BigInteger("-1");
	
	static BigInteger egcd(BigInteger a, BigInteger b){
		BigInteger q = a.divide(b), r = a.mod(b);
		if( r.equals(zero) ){
			x = zero; y = one; return b;
		}
		BigInteger g = egcd( b, r ), t = x;
		x = y; y = t.subtract( q.multiply( y ) );
		return g;
	}

    public static void main(String[] args) {
        Scanner cin = new Scanner(System.in);
		BigInteger A,B,S;
		
		A = cin.nextBigInteger();
		B = cin.nextBigInteger();
		S = cin.nextBigInteger();
		
		if( solve(A,B,S) == true ) System.out.println("YES");
		else System.out.println("NO");
	}
	
	public static boolean solve( BigInteger A, BigInteger B, BigInteger S ){
		if( A.equals(S) || B.equals(S) ) return true;
		if( S.equals(zero) ) return false;
		if( A.equals(zero) && B.equals(zero) ) return false;
		if( A.equals(zero) ) A = B;
		if( B.equals(zero) ) B = A;

		BigInteger g = egcd(A,B);
		if( !zero.equals( S.mod(g) ) ) return false;
		A = A.divide( g ); B = B.divide( g ); S = S.divide( g );
		boolean tr = false;
		x = x.mod(B).multiply(S.mod(B)).mod(B).add(B).mod(B);
		y = S.subtract( A.multiply(x) ).divide(B);
		
		while( y.compareTo(one) >= 0 ){
			if( x.gcd(y).equals(one) ){
				tr = true; break;
			}
			x = x.add(B); y = y.subtract(A);
		}
		return tr;
	}
}
#elif other2
// #include<cstdio>
// #include<algorithm>
using namespace std;
using ll = __int128;

ll abs_(ll x) { return x < 0? -x : x; }
ll gcd(ll x, ll y) { return y ? gcd(y, x%y) : x; }
ll egcd(ll a, ll b, ll& x, ll &y) {
    if (!b) { x = 1; y = 0; return a; }
    ll d = egcd(b, a%b, y, x); y -= x * (a/b);
    return d;
}
bool canon_egcd(ll a, ll b, ll c, ll& x, ll& y) {
    ll d = egcd(a, b, x, y), z = abs_(b/d);
    if (c%d) return false;
    x = (((x%z)*((c/d)%z))%z+z)%z, y = (c - a*x)/b;
    return true;
}
int main()
{
    int64_t _a, _b, _c;
	scanf("%lld%lld%lld", &_a, &_b, &_c);
	ll a = _a, b = _b, c = _c, x, y;
	if (a == 0) {
		puts((b == 0 || c % b) && c ? "NO" : "YES");
	} else if (b == 0) {
		puts(c % a ? "NO" : "YES");
	} else if (a > c) {
		puts(b == c ? "YES" : "NO");
	} else if (b > c) {
		puts(a == c ? "YES" : "NO");
	} else if (a == c || b == c) {
		puts("YES");
	} else if (c % gcd(a, b)) {
		puts("NO");
	} else {
		canon_egcd(a, b, c, x, y);
		ll g = gcd(a, b);
		ll ag = a / g;
		ll bg = b / g;
		while (y > 0) {
			if (x > 0 && y > 0 && gcd(x, y) == 1) {
				puts("YES");
				return 0;
			}
			x += bg;
			y -= ag;
		}
		puts("NO");
	}
	return 0;
}
#elif other3
// #include <bits/stdc++.h>
using namespace std;
typedef long long ll;
typedef pair<int,int> pii;
typedef pair<ll,ll> pll;
typedef vector<int> vi;
typedef vector<ll> vl;
typedef vector<pii> vpii;
typedef vector<pll> vpll;
typedef vector<vi> vvi;
typedef vector<vl> vvl;
typedef vector<vpii> vvpii;
typedef vector<vpll> vvpll;
// #define pb push_back
// #define mp make_pair
// #define ff first
// #define ss second
// #define MOD 998244353

ll n,m,k;

// find a pair (c, d) s.t. ac + bd = gcd(a, b)
pair<ll, ll> extended_gcd(ll a, ll b) {
	if (b == 0) return { 1, 0 };
	auto t = extended_gcd(b, a % b);
	return { t.second, t.first - t.second * (a / b) };
}

ll amel(const ll &v, ll p)
{
	if(!p) return 0;
	return ((amel(v, p>>1)<<1)%m+(p&1ll?v:0))%m;
}

void Solve()
{
	cin>>n>>m>>k;
	if(n==k||m==k) {cout<<"YES"; return;}
	if(!n&&!m) {cout<<"NO"; return;}
	if(!m) swap(n,m);
	ll tmp = __gcd(n,m);
	if(k%tmp) {cout<<"NO"; return;}
	n/=tmp; m/=tmp; k/=tmp;
	auto t=extended_gcd(n,m);
	tmp=t.ff%m; if(tmp<=0) tmp+=m;
	tmp=amel(tmp,k);
	if(n &&tmp>k/n) {cout<<"NO"; return;}
	for(ll i = tmp, j = (k-tmp*n)/m; j>0; i+=m,j-=n)
		if(__gcd(i,j)==1)
			{cout<<"YES"; return;}
	cout<<"NO";
}

void Init(){
	ios::sync_with_stdio(false); cin.tie(NULL);
}
int main(){ Init();
	// int tc; cin>>tc; for(int i=1;i<=tc;i++)
	// cout<<"Case #"<<i<<": ",
	Solve();
	return 0;
}
#elif other4
// #include <stdio.h>
// #include <algorithm>
// #include <assert.h>
// #include <bitset>
// #include <cmath>
// #include <complex>
// #include <deque>
// #include <functional>
// #include <iostream>
// #include <limits.h>
// #include <map>
// #include <math.h>
// #include <queue>
// #include <set>
// #include <stdlib.h>
// #include <string.h>
// #include <string>
// #include <time.h>
// #include <unordered_map>
// #include <unordered_set>
// #include <vector>

// #pragma warning(disable:4996)
// #pragma comment(linker, "/STACK:336777216")
using namespace std;

// #define mp make_pair
// #define all(x) (x).begin(), (x).end()
// #define ldb ldouble

typedef tuple<int, int, int> t3;
typedef long long ll;
typedef unsigned long long ull;
typedef double db;
typedef long double ldb;
typedef pair <int, int> pii;
typedef pair <ll, ll> pll;
typedef pair <ll, int> pli;
typedef pair <db, db> pdd;

int IT_MAX = 1 << 17;
int MOD = 1000000007;
const int INF = 0x3f3f3f3f;
const ll LL_INF = 0x3f3f3f3f3f3f3f3f;
const db PI = acos(-1);
const db ERR = 1e-10;
// #define szz(x) (int)(x).size()
// #define Se second
// #define Fi first

ll gcd(ll a, ll b) {
	return (a == 0)? b : gcd(b%a, a);
}

ll mymul(ll a, ll b, ll m) {
	ll rv = 0;
	while(b) {
		if(b%2) rv = (rv+a)%m;
		a = (a+a)%m;
		b /= 2;
	}
	return rv;
}

ll mymul2(ll a, ll b) {
	if(LL_INF / b < a) return LL_INF;
	return min(a*b, LL_INF);
}

ll mul_inv(ll a, ll b) {
	ll t1 = a, t2 = b, t3;
	ll v1 = 1, v2 = 0, v3;
	while(t2 != 1) {
		ll x = t1 / t2;
		t3 = t1 - x*t2;
		v3 = v1 - x*v2;
		t1 = t2, t2 = t3;
		v1 = v2, v2 = v3;
	}
	return (v2+b)%b;
}

int main() {
	ll A, B, S;
	scanf("%lld %lld %lld", &A, &B, &S);

	if(A == 0 && B == 0) {
		if(S == 0) printf("YES\n");
		else printf("NO\n");
		return 0;
	}
	if(A > B) swap(A, B);

	if(A == 0) {
		if(S % B == 0) printf("YES\n");
		else printf("NO\n");
		return 0;
	}
	if(S == 0) return !printf("NO\n");

	ll G = gcd(A, B);
	if(S%G) return !printf("NO\n");

	A /= G, B /= G, S /= G;
	ll v1 = mul_inv(A, B);

	v1 = mymul(v1, S, B);
	if(mymul2(v1, A) > S) return !printf("NO\n");
	ll v2 = (S-v1*A) / B;

	while(clock() < CLOCKS_PER_SEC * 1.9) {
		if(gcd(v1, v2) == 1) return !printf("YES\n");
		v1 += B, v2 -= A;
		if(v2 < 0) break;
	}
	return !printf("NO\n");
}


#endif
}
