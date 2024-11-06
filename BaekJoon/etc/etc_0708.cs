using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. -
이름 : 배성훈
내용 : N과 M
    문제번호 : 16214번

    오일러 피 함수 문제다
    여기서는 계속해서 n승을 하기에 양의 무한대이다 
    그래서 gcd(n, k) != 1 이면 지수는 항상 phi값에 상관없이 크므로 phi를 더해주면 된다

    여전히 작은 경우 phi를 꼭 더해야만 되는지는 모르겠다;
    내일도 유사한 문제를 풀 것인데, 내일은 해당 식이 아닌 중국인의 나머지 정리를 이용해서 풀어야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0708
    {

        static void Main708(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m;
            long ret;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    n = ReadInt();
                    m = ReadInt();

                    ret = Find(n, m);
                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            int GetPhi(int _n)
            {

                int ret = 1;
                for (int i = 2; i <= _n; i++)
                {

                    if (i * i > _n) break;
                    if (_n % i != 0) continue;

                    ret *= i - 1;
                    _n /= i;
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

                    if ((_b & 1L) == 1L) ret = (ret * _a) % _mod;

                    _a = (_a * _a) % _mod;
                    _b /= 2;
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

            long Find(long _a, int _mod)
            {

                if (_mod == 1) return 0;
                if (_a == 1) return 1;
                int phi = GetPhi(_mod);

                if (GetGCD(_a, _mod) == 1) return GetPow(_a, Find(_a, phi), _mod);
                return GetPow(_a, Find(_a, phi) + phi, _mod);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c =sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
/**
 * Written by 0xc0de1dea
 * Email : 0xc0de1dea@gmail.com
 */

//import java.io.FileInputStream;

public class Main {
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

    static long pow(long x, long y, long mod){
        long ret = 1;

        while (y > 0){
            if ((y & 1) == 1) ret = mod((ret * x), mod);
            x = mod((x * x), mod); y >>= 1;
        }

        return ret;
    }

    static long f(long x, long m){
        if (x == 1 || m == 1) return 1;
        long phi = phi(m);
        return pow(x, f(x, phi), m);
    }

    public static void main(String[] args) throws Exception {
        Reader in = new Reader();
        StringBuilder sb = new StringBuilder();

        int t = in.nextInt();

        while (t-- > 0){
            int n = in.nextInt();
            int m = in.nextInt();
            sb.append(f(n, m) % m).append('\n');
        }

        System.out.print(sb);
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
#elif other2
import sys
from random import randrange
from math import gcd
input=sys.stdin.readline

def millerrabin(n,a):
    r=0
    d=n-1
    while not d%2:
        r+=1
        d//=2
    x=pow(a,d,n)
    if x==1 or x==n-1:return True
    for i in ' '*(r-1):
        x=pow(x,2,n)
        if x==n-1:return True
    return False

prime_mem = dict()
def isPrime(n):
    if n in prime_mem:return prime_mem[n]
    pList=[2, 7, 61] if n < 4759123141 else [2, 325, 9375, 28178, 450775, 9780504, 1795265022]
    if n==1:return False
    if n<4:return True
    if not n%2:return False
    res= n in pList or all(millerrabin(n, p) for p in pList)
    prime_mem[n]=res
    return res

def pollard(n):
    if n==1:return 1
    if n%2==0:return 2
    if isPrime(n):return n
    x=randrange(1,n)
    c=randrange(1,n)
    g,y=1,x
    while g==1:
        x=(x**2+c)%n
        y=(y**2+c)%n
        y=(y**2+c)%n
        g=gcd(abs(x-y),n)
    if g==n:return pollard(n)
    return g

facto_mem = dict()

def facto(n):
	if n in facto_mem:return facto_mem[n]
	if n==1:
		facto_mem[n]=set()
		return set()
	if n%2==0:
		s=facto(n//2).copy()
		s.add(2)
		facto_mem[n]=s
		return s
	if isPrime(n):
		facto_mem[n] = {n}
		return {n}
	k=pollard(n)
	s1=facto(k)
	s2=facto(n//k)
	s=s1|s2
	facto_mem[n]=s
	return s

def phi(n):
	res=n
	s=facto(n)
	for p in s:
		res*=p-1
		res//=p
	return res

def sol(n, m):
	if m==1:return 0
	return(pow(n, sol(n, phi(m))+phi(m), m))

for i in ' '*int(input()):
	n,m=map(int,input().split())
	print(sol(n,m)%m)
#elif other3
// #include <bits/stdc++.h>
using namespace std;
using lint = long long;

vector<int> p, e;
int vis[100'005];

void init(){
	for(int i = 2; i < 100'005; i++){
		if(!vis[i]){
			p.push_back(i);
			for(int j = i + i; j < 100'005; j += i) vis[j] = 1;
		}
	}
}

int phi(int n){
	int m = n, n_ = n;
	int i = 0;
	vector<int> temp;
	while(i < (int)p.size() and p[i] * p[i] <= n){
		if(n % p[i] == 0){
			while(n % p[i] == 0) n/= p[i];
			temp.push_back(p[i]);
		}
		i++;
	}
	if(n != 1) temp.push_back(n);
	for(auto i: temp){
		m = m / i * (i - 1);
	}
	return m;
}

lint power(lint n, lint m, lint p){
	lint rtn = 1;
	while(m){
		if(m&1) rtn = (rtn * n) % p;
		m >>= 1;
		n = (n * n) % p;
	}
	return rtn % p;
}

int main(){
	cin.tie(0)->sync_with_stdio(0);
	int m, T;
	lint n, ans;
	init();
	cin >> T;
	while(T--){
		cin >> n >> m;
		e.clear();
		if(m == 1){
			cout << 0 << '\n';
			continue;
		}
		while(m > 1){
			e.push_back(m);
			m = phi(m);
		}
		e.push_back(1);
		ans = n;
		for(int i = (int)e.size() - 1 ;i--;){
			ans = power(n, ans + e[i + 1], e[i]);
		}
		cout << ans << '\n';
	}
}
#endif
}
