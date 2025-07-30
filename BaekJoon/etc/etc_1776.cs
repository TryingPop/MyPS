using System;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 18
이름 : 배성훈
내용 : A = B ⊕ C 
    문제번호 : 33914번
 
    수학, dp, 조합론 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1776
    {

        static void Main1776(string[] args)
        {

            int x, y;

            Input();

            GetRet();

            void GetRet()
            {

                int MOD = 1_000_000_007;
                int n = (x + y) / 3;        // 3개씩 묶은 개수
                int r = x / 2;              // 1을 포함한 개수

                if (x % 2 == 1 || r > n)
                {

                    Console.Write(0);
                    return;
                }

                long ret = GetPow(3, x / 2);
                ret = (Comb(n, r) * ret) % MOD;

                Console.Write(ret);

                long Comb(int _n, int _r)
                {

                    // n C r 찾기
                    long[] cur = new long[_n + 1];
                    long[] next = new long[_n + 1];

                    cur[0] = 1;
                    cur[1] = 1;
                    for (int i = 2; i <= _n; i++)
                    {

                        next[0] = 1;
                        next[i] = 1;

                        for (int j = 1; j < i; j++)
                        {

                            next[j] = (cur[j] + cur[j - 1]) % MOD;
                        }

                        long[] temp = cur;
                        cur = next;
                        next = temp;
                    }

                    return cur[_r];
                }

                long GetPow(long _a, int _exp)
                {

                    // 3^r 값 찾기
                    long ret = 1;

                    while (_exp > 0)
                    {

                        if ((_exp & 1) == 1) ret = (ret * _a) % MOD;

                        _exp >>= 1;
                        _a = (_a * _a) % MOD;
                    }

                    return ret;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                x = int.Parse(temp[0]);
                y = int.Parse(temp[1]);
            }
        }
    }

#if other
// #include<stdio.h>
typedef long long ll;
const ll w=1000000007;
ll i,n,p,q,r,x,y;
int main(){
    scanf("%lld%lld",&x,&y);
    if((x+y)%3!=0||x%2!=0||x>2*y)puts("0");
    else{
        x/=2;
        y=(y-x)/3;
        n=x+y;
        p=q=r=1;
        for(i=1;i<=n;++i){
            p=p*i%w;
            if(i==x)q=p;
            if(i==y)r=p;
        }
        q=q*r%w;
        y=w-2;
        while(y>0){
            if(y&1LL)p=p*q%w;
            q=q*q%w;
            y>>=1;
        }
        q=3;
        y=x;
        while(y>0){
            if(y&1LL)p=p*q%w;
            q=q*q%w;
            y>>=1;
        }
        printf("%lld\n",p);
    }
    return 0;
}
#endif
}
