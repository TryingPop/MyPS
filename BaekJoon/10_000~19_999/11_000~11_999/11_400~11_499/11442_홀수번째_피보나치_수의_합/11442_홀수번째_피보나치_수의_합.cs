using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 24
이름 : 배성훈
내용 : 홀수번째 피보나치 수의 합
    문제번호 : 11442번

    수학, 분할 정복을 이용한 거듭제곱 문제다
    아이디어는 다음과 같다
    F(n)을 n번째 피보나치 수열의 값이라 하면
    F(1) = F(2)이므로
    F(1) + F(3) + F(5) + ... + F(2k - 1) 
        = F(2) + F(3) + F(5) + ... + F(2k - 1)
        = F(4) + F(5) + ... + F(2k - 1)
        = F(6) + F(7) + ... + F(2k - 1)
        ...
        = F(2k)
    를 얻을 수 있다

    그래서 큰 항의 피보나치 수열의 값을 찾아 주기만 하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1074
    {

        static void Main1074(string[] args)
        {

            int MOD = 1_000_000_007;
            long n = long.Parse(Console.ReadLine());
            if ((n & 1L) == 1L) n++;

            long ret = GetFibo(n);
            Console.Write(ret);
            long GetFibo(long _exp)
            {

                (long a11, long a12, long a21, long a22) ret = (1L, 0L, 0L, 1L);
                (long a11, long a12, long a21, long a22) a = (1L, 1L, 1L, 0);

                while (_exp > 0)
                {

                    if ((_exp & 1L) == 1L) MatMul(ref ret, ref a);

                    MatMul(ref a, ref a);
                    _exp >>= 1;
                }

                return ret.a21;
            }

            void MatMul(ref (long a11, long a12, long a21, long a22) _a, ref (long a11, long a12, long a21, long a22) _b)
            {

                long a11 = (_a.a11 * _b.a11 + _a.a12 * _b.a21) % MOD;
                long a12 = (_a.a11 * _b.a12 + _a.a12 * _b.a22) % MOD;
                long a21 = (_a.a21 * _b.a11 + _a.a22 * _b.a21) % MOD;
                long a22 = (_a.a21 * _b.a12 + _a.a22 * _b.a22) % MOD;

                _a = (a11, a12, a21, a22);
            }
        }
    }

#if other
// #include<stdio.h>
// #define mod 1000000007

typedef long long lld;

struct ar {
	lld a, b, c, d;
	ar operator* (const ar& t) const {
		return {(a*t.a+b*t.c)%mod, (a*t.b+b*t.d)%mod, (c*t.a+d*t.c)%mod, (c*t.b+d*t.d)%mod};
	}
};

ar fib(lld t){
	if(t<=1)return {1,t,t,1-t};
	ar im=fib(t/2), x={1,1,1,0};
	im=im*im;
	if(t%2)im=im*x;
	return im;
}

int main(){
	lld n;
	scanf("%lld", &n);
	printf("%lld", fib(n+n%2).b);
	return 0;
}

#endif
}
