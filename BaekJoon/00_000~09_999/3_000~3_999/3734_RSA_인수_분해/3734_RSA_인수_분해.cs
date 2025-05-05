using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 3
이름 : 배성훈
내용 : RSA 인수 분해
	문제번호 : 3734번

	브루트포스 알고리즘, 수학 문제다.
	Fermat attack 다른 사람글을 보니 해당 아이디어를 본적이 있어야 한다고 한다.
	질문 게시판의 힌트를 통해 풀었다.

	다음 식을 이용해야 한다.
	(q - kp)^2 + 4 pqk = (q + kp)^2
	n = pq이므로, (q + kp)^2 = (q - kp)^2 + 4 nk을 얻는다.

	그러면 q - kp = x, q + kp = y라 하자.
	|q - kp| <= 10^5으로 |x| <= 10^5이된다.
	그리고 x^2 + 4nk = y^2이된다.

	x = 0, 1, ..., 10만까지 넣어가며 y를 찾는다.
	그리고 y가 제곱수인지 판별한다.
	제곱 수인 경우 x, y를 알기에 연립방정식으로 q, p를 찾을 수 있다.
	해당 값이 문제 조건을 만족하는지 확인해 만족하면 제출했다.

	BigInteger는 Sqrt연산을 지원하지 않는다.
	이분 탐색을 이용해 Sqrt를 찾아갔다.

	또 다른 방법으로 수열의 극한을 이용해 근사치를 찾아간다.
	참고한 사이트는 링크를 남긴다.
	적당히 몇 번 시도한 결과 해당 문제는 200번에 통과된다.
	(초기에는 800번 돌려 찾았다!)

	이후 생각해보니 1씩 증가라 매번 이분 탐색으로 루트를 찾을 필요가 없었다.
	해당 부분을 이분탐색을 1번만 하게끔 개선하니 4배이상 빨라졌다.
*/

namespace BaekJoon.etc
{
    internal class etc_1611
    {

        static void Main1611(string[] args)
        {

            BigInteger n, k;

            Input();

            GetRet();

            void GetRet()
            {

                BigInteger R = 4 * n * k;
                BigInteger p, q, y = MySqrt(R), pow = y * y;
                for (long x = 0; x <= 100_000; x++)
                {

					if (pow < x * x + R)
					{

						y++;
						pow = y * y;
					}

					if (pow != x * x + R) continue;

                    q = (x + y) / 2;
                    p = (y - x) / (2 * k);

                    if (q - k * p == x && q + k * p == y && p <= q && 1 < p)
                    {

                        Console.Write($"{p} * {q}");
                        return;
                    }

                    q = (-x + y) / 2;
                    p = (y + x) / (2 * k);

                    if (q - k * p == -x && q + k * p == y && p <= q && 1 < p)
                    {

                        Console.Write($"{p} * {q}");
                        return;
                    }
                }

#if first

                BigInteger MySqrt(BigInteger _chk)
                {

					// https://blog.naver.com/dltkdehstm12/222251914399
					BigInteger x = 1;

                    for (int i = 0; i < 200; i++)
                    {

                        x = (x + _chk / x) >> 1;
                    }

                    return x;
                }
#else

                BigInteger MySqrt(BigInteger _chk)
                {

                    BigInteger l = 0, r = R, mid, pow;

                    while (l <= r)
                    {

                        mid = (l + r) >> 1;
                        pow = mid * mid;

                        if (pow < _chk) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }
#endif
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = BigInteger.Parse(temp[0]);
                k = BigInteger.Parse(temp[1]);
            }
            
        }
    }

#if other
// #include <iostream>
// #include <sstream>
// #include <iomanip>
// #include <string>
// #include <cstring>
// #include <vector>
// #include <cmath>
// #include <cstdio>
using namespace std;
// #define M 1000000000
// #define MAXD 30
typedef long long ll;

struct bignum {
	int sz;
	int sign; 
	int dig[MAXD];
	
	bignum(int v=0) {
		for(int i=2;i<MAXD;i++) dig[i] = 0;
		sign = 1;
		sz = 2;
		if(v < 0) {
			sign = -1;
			v = -v;
		}
		dig[0] = v % M;
		dig[1] = v / M;
		if(dig[1] == 0) sz--;
	}

	bignum(char *c) {
		int i,q,k,v,pw;
		for(i=0;i<MAXD;i++) dig[i] = 0;
		k = strlen(c);
		if(c[0] == '-') sign = -1;
		else sign = 1;

		sz = 0;
		v = 0;
		q = 0;
		pw = 1;
		for(i=k-1;i>=0;i--){
			if(c[i] == '-') break;
			v += pw*(c[i] - '0');
			q++;
			pw *= 10;
			if(q >= 9){
				pw = 1;
				q = 0;
				dig[sz++] = v;
				v = 0;    
			}    
		}

		dig[sz++] = v;
		this->justify();
	}
	
	void justify() {
		int i;
		for(i=sz-1;i>=0;i--) if(dig[i]) break;
		sz = 1+i;
		if(!sz) sz=1;
		if(sz == 1 && dig[0] == 0) sign = 1;
	}
	
	void shift(int k){
		if(sz == 1 && dig[0] == 0) return ;
		for(int i=sz-1;i>=0;i--) dig[i+k] = dig[i];
		for(int i=k-1;i>=0;i--) dig[i] = 0;
		sz += k;
	}
	
	void negate() { if(*this != 0) this->sign = -this->sign; }
	
    friend ostream& operator <<(ostream& o, bignum b) { 
		if(b.sign < 0) o<<"-";
		o<<b.dig[b.sz-1];
		for(int i=b.sz-2;i>=0;i--) o<<setfill('0')<<setw(9)<<b.dig[i];
		return o;
	}
	
	int cmp(bignum b) const {
		if(sign > 0 && b.sign < 0) return 1;
		if(sign < 0 && b.sign > 0) return -1;
		if(sz > b.sz) return sign;
		if(sz < b.sz) return -sign;
		for(int i=sz-1;i>=0;i--){
			if(dig[i] > b.dig[i]) return sign;
			if(dig[i] < b.dig[i]) return -sign;    
		}
		return 0;
	}
	
	bool operator<(const bignum &b) { return (cmp(b) < 0); }
	bool operator<=(const bignum &b) { return (cmp(b) <= 0); }
	bool operator>(const bignum &b) { return (cmp(b) > 0); }
	bool operator>=(const bignum &b) { return (cmp(b) >= 0); }
	bool operator==(const bignum &b) { return (cmp(b) == 0); }
	bool operator!=(const bignum &b) { return (cmp(b) != 0); }
	
	bignum operator+(bignum b) {
		if(sign != b.sign) {
			if(sign == -1) {
				sign = 1;
				bignum ret = b - *this;
				sign = -1;
				return ret;
			}
			b.sign = 1;
			return *this - b;
		}
		bignum ret;
		ret.sign = sign;
		ret.sz = 1 + max(sz,b.sz);
		int c = 0;
		for(int i=0;i<ret.sz;i++) {
			ret.dig[i] = c + dig[i] + b.dig[i];
			c = 0;
			if(ret.dig[i] >= M) {
				c = 1;
				ret.dig[i] -= M;
			}
		}
		ret.justify();
		return ret;   
	}
	
	bignum operator-(bignum b) {
		bignum ret;
		if(sign < 0 || b.sign < 0) {
			b.sign = -b.sign;
			return *this+b;
		}
		if(*this<b){
			ret = b-*this;
			ret.sign = -1;
			return ret;    
		}
		ret.sign = 1;
		ret.sz = max(sz,b.sz);
		int brw = 0;
		for(int i=0;i<ret.sz;i++) {
			int k = dig[i] - b.dig[i] - brw;
			if(k>=0) brw=0;
			else {
				k += M;
				brw = 1;
			}
			ret.dig[i] = k;
		}
		ret.justify();
		return ret;
	}
	
	bignum operator*(bignum b){
		bignum ret, buf;
		for(int i=0;i<b.sz;i++){
			buf.sz = 1+sz;
			int c=0;
			for(int j=0;j<buf.sz;j++){
				long long hlp = 1ll*b.dig[i]*dig[j] + c;
				c = hlp/M;
				buf.dig[j] = hlp%M;
			}
			buf.shift(i);
			ret = ret + buf;
		}
		ret.sign = sign*b.sign;
		ret.justify();
		return ret;    
	}
	
	bignum operator*(int v) {
		bignum ret;
		ret.sz = 1+sz;
		int c=0;
		for(int i=0;i<ret.sz;i++) {
			long long hlp = 1ll*v*dig[i] + c;
			c = hlp/M;
			ret.dig[i] = hlp%M;
		}
		ret.justify();
		return ret;        
	}
	
	bignum operator/(bignum b) {
		bignum ret, row;
		ret.sign = sign*b.sign;
		int as = sign;
		int bs = b.sign;
		sign = 1;
		b.sign = -1;
		ret.sz = sz;
		int i, x, y, z;
		for(i=sz-1;i>=0;i--) {
			row.shift(1);
			row.dig[0] = dig[i];
			x = 0;
			y = M-1;
			while(1){
				z = (1+x+y)/2;
				if(x == y) break;
				if(b*z <= row) x = z;        
				else y = z-1;    
			}
			ret.dig[i] = z;
			row = b*z - row;
			row.negate();
		}
		sign = as;
		b.sign = bs;
		ret.justify();
		return ret;
	}
};

bignum sqrt(bignum b) {
	bignum m, i=0, f=b;
	while(1) {
		m = (i+f)/2;
		if(m*m>b) {
			if((m-1)*(m-1)<=b) return m-1;
			f=m-1;
		} else {
			if((m+1)*(m+1)>b) return m;
			i=m+1;
		}
	}
}

char c[10000];
int k;
bignum n, p, q;

int find(bignum x, bignum delta) {
	p = (delta-x)/(2*k);
	q = p*k+x;
	if(p>1 && q>1 && p*q == n) return 1;
	q = p*k-x;
	if(p>1 && q>1 && p*q == n) return 1;
	p = (delta+x)/(2*k);
	q = p*k+x;
	if(p>1 && q>1 && p*q == n) return 1;
	q = p*k-x;
	if(p>1 && q>1 && p*q == n) return 1;
	return 0;
}

int main() {
	while(scanf("%s %d", c, &k) != EOF) {
		n = bignum(c);
		if(n.sz == 1) {
			int num = n.dig[0];
			for(int i=2;i<=num;i++) if(num % i == 0) {
				cout<<i<<" * "<<num/i<<endl;
				break;
			}
		}
		else {
			bignum v = n * (4*k);
			bignum r = sqrt(v);
			for(int i=0;;i++) {
				bignum aux = (r+i)*(r+i) - v;
				if(aux<0) continue;
				bignum x = sqrt(aux);
				if(x*x == aux) {
					bignum delta = sqrt(x*x + v);
					if(find(x, delta)) {
						if(p>q) swap(p, q);
						cout<<p<<" * "<<q<<endl;
						break;
					}
				}
			}
		}
	}
	return 0;
}
#elif other2
import math
import sys
def calc(x):
    return x*x*y
x,y=input().split(' ')
x=int(x)
y=int(y)
// # print(float(x)/y)
p=0
list=[pow(10,i) for i in range(0,60)]
list.reverse()
while True:
    er=0
    for plus in list:
        if calc(p+plus)<x:
            p+=plus
            er=1
            break
    if er==0:
        break
if p<=1:
    p=2
tp=p
while True:
    q=x//p
    if y*p-q>100000 or p>q:
        break
    if p*q==x:
        print(f'{p} * {q}')
        sys.exit()
    p+=1
p=tp-1
while True:
    q=x//p
    if q-y*p>100000 or p<=0:
        break
    if p*q==x:
        print(f'{p} * {q}')
        sys.exit()
    p-=1
#elif other3
import java.math.*;
import java.util.*;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        BigInteger n = sc.nextBigInteger();
        BigInteger k = sc.nextBigInteger();
        BigInteger k2 = k.multiply(BigInteger.valueOf(2));
        for(int i=0 ; i<=100000 ; i++) {
            BigInteger I = BigInteger.valueOf(i);
            BigInteger T = BigInteger.valueOf(i);
            T = T.multiply(T);
            T = T.add(BigInteger.valueOf(4).multiply(k).multiply(n));
            
            
            BigInteger D = T.sqrt();
            if(!D.multiply(D).equals(T)) continue;
            BigInteger p = D.subtract(I);
            if (p.mod(k2).intValue() == 0) {
                p = p.divide(k2);
                BigInteger q = n.divide(p);
                if(!p.equals(BigInteger.ONE) && !q.equals(BigInteger.ONE)) {
                    System.out.println(p + " * " + q);
                    return;
                }
            }
            
            p = D.add(I);
            if (p.mod(k2).intValue() == 0) {
                p = p.divide(k2);
                BigInteger q = n.divide(p);
                if(!p.equals(BigInteger.ONE) && !q.equals(BigInteger.ONE)) {
                    System.out.println(p + " * " + q);
                    return;
                }
            }
        }
    }
}
#endif
}
