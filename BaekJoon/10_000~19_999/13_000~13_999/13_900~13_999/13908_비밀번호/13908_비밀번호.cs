using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. -
이름 : 배성훈
내용 : 비밀번호
    문제번호 : 13908번

    자리수가 7자리이므로 많아야 1000만번이다
    그래서 브루트포스 알고리즘을 이용했다

    만약 8 ~ 9자리면 경우를 보면서 수학적으로 풀것이다
    브루트 포스로 296ms로 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0096
    {

        static void Main96(string[] args)
        {

#if first
            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            bool noChk = info[1] == 0;
            int[] include = null;
            if (!noChk) include = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int ret;
            if (noChk)
            {

                ret = 1;
                for (int i = 0; i < info[0]; i++)
                {

                    ret *= 10;
                }

                Console.WriteLine(ret);
            }
            else
            {

                bool[] chk = new bool[10];
                ret = 0;
                int[] calc = new int[info[0] + 1];
                while (true)
                {

                    calc[0]++;

                    int idx = 0;
                    while (calc[idx] == 10)
                    {

                        calc[idx] = 0;
                        idx++;
                        calc[idx]++;
                    }

                    for (int i = 0; i < info[0]; i++)
                    {

                        chk[calc[i]] = true;
                    }

                    bool find = false;
                    ret++;
                    for (int i = 0; i < info[1]; i++)
                    {

                        if (chk[include[i]]) continue;
                        ret--;
                        break;
                    }

                    for (int i = 0; i < 10; i++)
                    {

                        chk[i] = false;
                    }

                    if (calc[info[0]] == 1) break;
                }

                Console.WriteLine(ret);
            }
#endif
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
typedef long long ll;

// 제곱 함수
ll ipow(int v,int x){
	ll r=1;
	for(int i=0;i<x;i++)r*=v;
	return r;
}

// 
ll ic(int n,int k){
	ll r=1;
	for(int i=1;i<=k;i++){
		r*=(n+1-i);
		r/=i;
	}
	return r;
}

int main(){
	cin.tie(0);
	ios::sync_with_stdio(false);
	int n,m;cin>>n>>m;
	ll r=0;
	for(int i=0;i<=m;i++)
		r+=ipow(10-i,n)*ic(m,i)*(i&1?-1:1);
	cout<<r;
}
#endif
}
