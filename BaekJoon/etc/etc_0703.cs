using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 17
이름 : 배성훈
내용 : Exponial
    문제번호 : 13358번

    수학, 정수론, 오일러 피 함수, 분할 정복을 이용한 거듭 제곱 문제다
    phi 함수 1인 경우 0으로 처리해 한번 틀렸다

    아이디어는 다음과 같다
    5에서는 long의 범위를 벗어나기에 입력값 mod의 범위보다 항상 크다
    계산기로 5이하인 경우는 4가 26만 조금 넘고, 3, 2, 1은 1자리 수이므로 직접 구했다
    그래서 5 이상이면 phi보다는 항상 크다고 결론을 냈고 5 미만에서만 비교했다
    이외는 한단계씩 직접 연산하면서 찾아갔다


    https://cp-algorithms.com/algebra/phi-function.html#generalization
*/

namespace BaekJoon.etc
{
    internal class etc_0703
    {

        static void Main703(string[] args)
        {

            Read(out long a, out int mod);
            SetArr(out long[] arr);

            Console.WriteLine(DFS(a, mod));

            bool Chk(long _a, int _phi)
            {

                if (_a <= 1) return 1 >= _phi;

                if (_a >= 5) return true;
                return arr[_a] >= _phi;
            }

            long DFS(long _a, int _mod)
            {

                if (_mod == 1) return 0L;
                if (_a < 5) return arr[_a] % _mod;
                int phi = GetPhi(_mod);

                if (GetGCD(_a, _mod) == 1) return GetPow(_a, DFS(_a - 1, phi), _mod);
                else return GetPow(_a, DFS(_a - 1, phi) + (Chk(_a, phi) ? 1 : 0) * phi, _mod);
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

            int GetPhi(int _n)
            {

                int ret = 1;
                for (int i = 2; i <= _n; i++)
                {

                    if (i * i > _n) break;
                    if (_n % i > 0) continue;

                    _n /= i;
                    ret *= (i - 1);

                    while(_n % i == 0)
                    {

                        _n /= i;
                        ret *= i;
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

            void SetArr(out long[] arr)
            {

                arr = new long[5];
                arr[1] = 1;
                arr[2] = 2;
                arr[3] = 9;
                arr[4] = 262_144;
            }

            void Read(out long _a, out int _b)
            {

                // int.Parse는 null일 때 Exception 일으키나, 
                // toint는 0으로 반환
                // 그리고 toint는 char형도 변환 가능하다!
                string[] temp = Console.ReadLine().Split();
                _a = Convert.ToInt64(temp[0]);
                _b = Convert.ToInt32(temp[1]);
            }
        }
    }

#if other
using System;

public static class SweetieMahiro
{
	public static long phi(long m)
	{
		long res = 1;
		for (long p = 2; p * p <= m; ++p)
		{
			if (m % p == 0)
			{
				res *= p - 1;
				m /= p;
				while (m % p == 0)
				{
					res *= p;
					m /= p;
				}
			}
		}
		if (m > 1)
		{
			res *= m - 1;
		}
		return res;
	}

	public static long expmod(long b, long e, long m)
	{
		long res = 1;
		while (e != 0)
		{
			if ((e & 1) != 0)
			{
				res = (res * b) % m;
			}
			e /= 2;
			b = (b * b) % m;
		}
		return res;
	}

	public static long expo_trunc(long n)
	{
		return n < 4 ? n < 3 ? n : 9 : 100000;
	}

	public static long expo_mod_m(long n, long m)
	{
		if (m == 1)
		{
			return 0;
		}
		if (n == 1)
		{
			return 1;
		}
		long m2 = phi(m);
		long e = expo_trunc(n - 1);
		if (e == 100000)
		{
			e = m2 + expo_mod_m(n - 1, m2);
		}
		return expmod(n, e, m);
	}

	internal static void Main()
	{
		int n;
		int m;
        n = NextInt();
        m = NextInt();
        Console.Write("{0:D}\n", expo_mod_m(n, m));

	}
	static int NextInt()
	{
		int ret = 0;
		int c;
		do
		{
			c = Console.Read();
		} while (c < '0' || c > '9');
		while (c >= '0' && c <= '9')
		{
			ret = ret * 10 + (c - '0');
			c = Console.Read();
		}
		return ret;
	}
}
#elif other2
// #include<stdio.h>
typedef long long ll;
ll fpow(ll n,ll k,ll mod){
	ll s=1;
	for(;k;k>>=1){
		if(k&1) s=s*n%mod;
		n=n*n%mod;
	}
	return s;
}
ll phi(ll n){
	ll i=2,s=1;
	while(i*i<=n){
		if(n%i==0){
			n/=i;
			s*=i-1;
			while(n%i==0){
				n/=i;
				s*=i;
			}
		}
		if(i==2) i=1;
		i+=2;
	}
	if(n-1) s*=n-1;
	return s;
}
ll cut(ll v,ll mod){
	if(v==1) return 1;
	return fpow(v,cut(v-1,mod),mod);
}
ll f(ll v,ll mod){
	if(mod==1) return 0;
	if(v==1) return 1;
	if(v<=4) return cut(v,mod);
	return fpow(v,f(v-1,phi(mod))+phi(mod),mod);
}
int main(){
	ll n,i,mod;
	scanf("%lld %lld",&n,&mod);
	printf("%lld",f(n,mod));
}
#elif other3
/**
 * Written by 0xc0de1dea
 * Email : 0xc0de1dea@gmail.com
 */

//import java.io.FileInputStream;

public class Main {
    /*static int n;
    static long[] tower;
    static final int MAX = 1 << 20;
    static int[] phi = new int[MAX];*/

    static long phi(long n){
        long ret = n;

        for (long i = 2; i * i <= n; i++){
            if (n % i == 0){
                while (n % i == 0) n /= i;
                ret -= ret / i;
            }
        }
        if (n > 1) ret -= ret / n;

        return ret;
    }

    static long mod(long a, long b){
        if (a > b) return b + a % b;
        else return a;
    }

    static long fastPow(long x, long y, long mod){
        long ret = 1;

        while (y > 0){
            if ((y & 1) == 1) ret = mod(ret * x, mod);
            x = mod(x * x, mod); y >>= 1;
        }

        return ret;
    }

    static long g(long x, long m, long depth){
        if (depth == 0) return 1;
        return fastPow(x, g(x - 1, m, depth - 1), m);
    }

    static long f(long x, long m){
        if (m == 1) return 1;
        if (x <= 5) return g(x, m, x);
        long phi = phi(m);
        return fastPow(x, f(x - 1, phi) + phi, m);
    }

    public static void main(String[] args) throws Exception {
        Reader in = new Reader();
        StringBuilder sb = new StringBuilder();

        //for (int i = 1; i < MAX; i++) phi[i] = i;
        //for (int i = 1; i < MAX; i++) for (int j = i + i; j < MAX; j += i) phi[j] -= phi[i];

        int n = in.nextInt();
        int m = in.nextInt();

        long res = f(n, m) % m;
        System.out.print(res);
    }
}

class Reader {
    final int SIZE = 1 << 13;
    byte[] buffer = new byte[SIZE];
    int index, size;

    char nextChar() throws Exception {
        char ch = ' ';
        byte c;
        while ((c = read()) <= 32);
        do ch = (char)c;
        while (isAlphabet(c = read()));
        return ch;
    }
    
    int nextInt() throws Exception {
        int n = 0;
        byte c;
        boolean isMinus = false;
        while ((c = read()) <= 32); //{ if (size < 0) return -1; }
        if (c == 45) { c = read(); isMinus = true; }
        do n = (n << 3) + (n << 1) + (c & 15);
        while (isNumber(c = read()));
        return isMinus ? ~n + 1 : n;
    }

    long nextLong() throws Exception {
        long n = 0;
        byte c;
        boolean isMinus = false;
        while ((c = read()) <= 32);
        if (c == 45) { c = read(); isMinus = true; }
        do n = (n << 3) + (n << 1) + (c & 15);
        while (isNumber(c = read()));
        return isMinus ? ~n + 1 : n;
    }

    double nextDouble() throws Exception {
        double n = 0, div = 1;
        byte c;
        boolean isMinus = false;
        while ((c = read()) <= 32);
        if (c == 45) { c = read(); isMinus = true; }
        else if (c == 46) { c = read(); }
        do n = (n * 10) + (c & 15);
        while (isNumber(c = read()));
        if (c == 46) { while (isNumber(c = read())){ n += (c - 48) / (div *= 10); }}
        return isMinus ? -n : n;
    }

    boolean isNumber(byte c) {
        return 47 < c && c < 58;
    }

    boolean isAlphabet(byte c){
        return 96 < c && c < 123;
    }

    byte read() throws Exception {
        if (index == size) {
            size = System.in.read(buffer, index = 0, SIZE);
            if (size < 0) buffer[0] = -1;
        }
        return buffer[index++];
    }
}
#endif
}
