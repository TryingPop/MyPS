using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 27
이름 : 배성훈
내용 : 약수 게임
    문제번호 : 16894번

    소수 판정, 게임 이론 문제다
    소수나 1을 부르게하면 자기가 무조건 진다!
    이를 상대 관점에서 보면, 소수나 1을 부르게 해야한다
    그래서 약수 중 서로 다른 두 소수를 곱한 값이나
    한 소수의 제곱을 다음 상대에게 넘어주면 무조건 이긴다
*/

namespace BaekJoon.etc
{
    internal class etc_0914
    {

        static void Main914(string[] args)
        {

            long n = long.Parse(Console.ReadLine());

            int cnt = 0;
            for (long i = 2; i * i <= n; i++)
            {

                while (n % i == 0)
                {

                    n /= i;
                    cnt++;
                }
            }

            if (n > 1) cnt++;

            if (cnt == 2) Console.Write("cubelover");
            else Console.Write("koosaga");
        }
    }

#if other
// #include <bits/stdc++.h>
// #define sz(v) ((int)(v).size())
// #define all(v) (v).begin(), (v).end()
using namespace std;
using pi = pair<int, int>;
using lint = long long;
const int MAXN = 1000005;


namespace miller_rabin{
    lint mul(lint x, lint y, lint mod){ return (__int128) x * y % mod; }
	lint ipow(lint x, lint y, lint p){
		lint ret = 1, piv = x % p;
		while(y){
			if(y&1) ret = mul(ret, piv, p);
			piv = mul(piv, piv, p);
			y >>= 1;
		}
		return ret;
	}
	bool miller_rabin(lint x, lint a){
		if(x % a == 0) return 0;
		lint d = x - 1;
		while(1){
			lint tmp = ipow(a, d, x);
			if(d&1) return (tmp != 1 && tmp != x-1);
			else if(tmp == x-1) return 0;
			d >>= 1;
		}
	}
	bool isprime(lint x){
		for(auto &i : {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37}){
			if(x == i) return 1;
			if(x > 40 && miller_rabin(x, i)) return 0;
		}
		if(x <= 40) return 0;
		return 1;
	}
}

namespace pollard_rho{
	lint f(lint x, lint n, lint c){
		return (c + miller_rabin::mul(x, x, n)) % n;
	}
	void rec(lint n, vector<lint> &v){
		if(n == 1) return;
		if(n % 2 == 0){
			v.push_back(2);
			rec(n/2, v);
			return;
		}
		if(miller_rabin::isprime(n)){
			v.push_back(n);
			return;
		}
		lint a, b, c;
		while(1){
			a = rand() % (n-2) + 2;
			b = a;
			c = rand() % 20 + 1;
			do{
				a = f(a, n, c);
				b = f(f(b, n, c), n, c);
			}while(gcd(abs(a-b), n) == 1);
			if(a != b) break;
		}
		lint x = gcd(abs(a-b), n);
		rec(x, v);
		rec(n/x, v);
	}
	vector<lint> factorize(lint n){
		vector<lint> ret;
		rec(n, ret);
		sort(ret.begin(), ret.end());
		return ret;
	}
};

int main(){
	lint n; cin >> n;
		auto x = pollard_rho::factorize(n);
		if(sz(x) == 2) puts("cubelover");
		else puts("koosaga");
}

#endif
}
