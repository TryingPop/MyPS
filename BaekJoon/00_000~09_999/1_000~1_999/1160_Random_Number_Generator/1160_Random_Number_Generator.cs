using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 18
이름 : 배성훈
내용 : Random Number Generator
    문제번호 : 1160번

    수학 문제다.
    a, c가 10^18까지 오므로
    곱셈을 직접 정의했다.

    아이디어는 다음과 같다.
    f(n + 1) = a * f(n) + c
    f(1) = s이다.

    해당 식을 행렬로 만들면 다음과 같다.

    |a   c| |xn |   = |xn+1|
    |0   1| |1  |     |1   |

    이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1202
    {

        static void Main1202(string[] args)
        {

            long m, a, c, s, n, g;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                (long a11, long a12, long a21, long a22) mat = (a, c, 0, 1);
                long ret = MatPow(ref mat, n);
                Console.Write(ret % g);

                long MatPow(ref (long a11, long a12, long a21, long a22) _a, long _exp)
                {

                    (long a11, long a12, long a21, long a22) pow = (1, 0, 0, 1);

                    while (_exp > 0)
                    {

                        if ((_exp & 1L) == 1L) MatMul(ref pow, ref _a);
                        MatMul(ref _a, ref _a);
                        _exp >>= 1;
                    }

                    long ret = (Mul(pow.a11, s) + pow.a12) % m;
                    return ret;
                }

                void MatMul(ref (long a11, long a12, long a21, long a22) _a, ref(long a11, long a12, long a21, long a22) _b)
                {

                    long a11 = (Mul(_a.a11, _b.a11) + Mul(_a.a12, _b.a21)) % m;
                    long a12 = (Mul(_a.a11, _b.a12) + Mul(_a.a12, _b.a22)) % m;

                    long a21 = (Mul(_a.a21, _b.a11) + Mul(_a.a22, _b.a21)) % m;
                    long a22 = (Mul(_a.a21, _b.a12) + Mul(_a.a22, _b.a22)) % m;

                    _a = (a11, a12, a21, a22);
                }

                long Mul(long _a, long _b)
                {

                    long ret = 0;
                    while (_b > 0)
                    {

                        if ((_b & 1L) == 1L) ret = (ret + _a) % m;
                        _a = (_a + _a) % m;
                        _b >>= 1;
                    }

                    return ret;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                m = long.Parse(temp[0]);
                a = long.Parse(temp[1]) % m;
                c = long.Parse(temp[2]) % m;
                s = long.Parse(temp[3]) % m;
                n = long.Parse(temp[4]);
                g = long.Parse(temp[5]);
            }
        }
    }

#if other
// #include<stdio.h>
long long m,a,c,x,n,y;
__int128 m0,a0,c0,x0,n0,y0,z1=1;
__int128 f(__int128 num){
	if(num==1) return a0%m0;
	if(num%2==1) return ((f(num/2)*f(num/2))%m0*f(z1))%m0;
	return (f(num/2)*f(num/2))%m0;
}
__int128 g(__int128 num){
	if(num==1) return c0;
	if(num%2==0) return ((f(num/2)+z1)*g(num/2))%m0;
	return (((f(num/2)*a0)%m0+a0%m0)*g(num/2)+c0)%m0;
}
int main()
{
	scanf("%lld %lld %lld %lld %lld %lld",&m,&a,&c,&x,&n,&y);
	m0=m;a0=a;c0=c;x0=x;n0=n;y0=y;
	printf("%lld",(long long)(((f(n0)*(x0%m0)+g(n0))%m0)%y0));
	return 0;
}
#elif other2
// #include<cstdio>
typedef long long ll;
ll m, a, c, x0, n, g;
ll mul(ll x, ll y) {
    if (!y) return 0;
    return (mul(x, y / 2) * 2 + (y & 1 ? x : 0)) % m;
}
struct st {
    ll a, b, c, d;
    st(ll _a, ll _b, ll _c, ll _d) :a(_a), b(_b), c(_c), d(_d) {}
    st operator*(st i) {
        return{ (mul(a,i.a) + mul(b,i.c)) % m,(mul(a,i.b) + mul(b,i.d)) % m,
            (mul(c,i.a) + mul(d,i.c)) % m,(mul(c,i.b) + mul(d,i.d)) % m };
    }
};
st pow(ll x) {
    if (!x) return st(1, 0, 0, 1);
    st t = pow(x / 2);
    return t*t*(x & 1 ? st(a + 1, m - a%m, 1, 0) : st(1, 0, 0, 1));
}
int main() {
    scanf("%lld%lld%lld%lld%lld%lld", &m, &a, &c, &x0, &n, &g);
    st t = pow(n);
    printf("%lld", (mul(t.c, mul(a, x0) + c) + mul(t.d, x0)) % m % g);
    return 0;
}
#endif
}
