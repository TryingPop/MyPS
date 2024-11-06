using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 30
이름 : 배성훈
내용 : 타일 위의 원
    문제번호 : 1709번

    기하학, 애드 혹, 피타고라스 정리 문제다
    아이디어는 다음과 같다
    원을 접하게 한다고 했으므로 원의 중심을 좌표축을 중심으로 하고
    겹치는 면을 계산했다 원은 x, y축에 대칭이므로 1 / 4만 확인해 4배를 하면 된다
    1사분면을 기준으로 겹치는 면들은 x축에서 시작해서 y를 1씩 늘려가면서 y축에 도착할 때까지
    몇 개의 면과 만나는지 확인했다

    1억 5천만 들어오는데, 반지름 7500만까지 확인하므로 for문으로 해볼만하다고 판단하고
    제출했다 그러니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0781
    {

        static void Main781(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            long r = n / 2;
            long rr = r * r;

            long bw = r, w;
            long hh, ww;

            long ret = 0;
            for (long i = 1; i <= r; i++)
            {

                hh = i * i;
                long chk = rr - hh;
                w = (long)Math.Sqrt(chk);
                ww = w * w;
                
                ret += (bw - w);
                if (chk != ww) ret++;

                bw = w;
            }

            ret *= 4;
            Console.Write(ret);
        }
    }

#if other
// #pragma GCC optimize("Ofast")
// #include <bits/stdc++.h>
using namespace std;
typedef long long ll;

ll f(ll x){
	ll cnt=0;
	ll i=1,j=x-1;
	for(;i<=j;){
		if(i*i+j*j<x*x){
			i++;
		}
		else if(i*i+j*j>x*x){
			j--;
		}
		else{
			if(i<j)
				cnt+=2;
			else
				cnt++;
			i++;
			j--;
		}
	}
	return 2*x-1-cnt;
}

int main(){
	cin.tie(0);
	ios::sync_with_stdio(false);
	ll n;cin>>n;
	cout<<f(n/2)*4;
}
#elif other2
// #include<iostream>
using namespace std;
using ll=long long;
int main()
{
    ll n,r,rs,xs,ans=0;
    cin>>n;
    r=n>>1;
    rs=r*r;
    for(ll x=r-1,pre=0;x>=0;--x)
    {
        xs=x*x;
        ll ys=rs-xs;
        while((pre+1)*(pre+1)<=ys)
        {
            if((pre+1)*(pre+1)!=ys) ++ans;
            ++pre;
        }
        ++ans;
    }
    cout<<ans*4L;
    return 0;
}
#endif
}
