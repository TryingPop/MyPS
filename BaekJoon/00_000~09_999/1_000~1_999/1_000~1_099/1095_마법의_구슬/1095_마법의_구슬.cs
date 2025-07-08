using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : 마법의 구슬
    문제번호 : 1095번

    수학, 소수판정 문제다
    (s + f) C s 를 나누는 m이하의 가장 큰 수를 찾는게 목표다
    여기서 C는 조합을 의미!
    
    아이디어는 다음과 같다
    (s + f)!을 소인수분해 했을 시, m이하 소수들의 개수를 모두 찾아야한다
    그리고 s!, f! 역시 m이하 소수들의 개수를 모두 찾는다

    개수를 모두 찾으면 (s + f) C s = (s + f)! / (s!) (f!) 이므로
    m이하의 모든 소인수들의 개수를 확인할 수 있다
    소인수들의 개수를 모아놓은 배열을 nums라 하자

    이제 해당 소인수들로 만들 수 있는 최대값을 찾아야한다
    해당 부분을 처음에 짤때 m에서 시작해서 각각의 소인수들을 모두 찾아
    소인수 개수가 nums안의 원소보다 많은 경우 탈출하는 식으로 짰다
    그러니, 60%대부터 느려지더니 1.3초대가 나왔다(Slow 부분)

    이후 다른 사람 풀이를 보고, 소인수들을 하나씩 제거해가면서
    가능한 숫자들을 늘려가는 식으로 짜니 20배 이상 속도가 빠르게 나왔다
*/

namespace BaekJoon.etc
{
    internal class etc_0400
    {

        static void Main400(string[] args)
        {

#if Slow
            int[] info = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            bool[] isNotPrime = new bool[info[2] + 1];
            ChkPrime();

            long[] nums = new long[info[2] + 1];
            CntPrime(info[0] + info[1], 1);
            CntPrime(info[0], -1);
            CntPrime(info[1], -1);

            int ret = -1;
            for (int i = info[2]; i >= 1; i--)
            {

                bool possible = true;
                for (int j = 2; j <= i; j++)
                {

                    if (isNotPrime[j]) continue;
                    if (i % j > 0) continue;

                    int cnt = 0;
                    int chk = i;
                    while(chk % j == 0)
                    {

                        chk /= j;
                        cnt++;
                    }

                    if (cnt <= nums[j]) continue;

                    possible = false;
                    break;
                }

                if (possible)
                {

                    ret = i;
                    break;
                }
            }

            Console.WriteLine(ret);

            void ChkPrime()
            {

                for (int i = 2; i <= info[2]; i++)
                {

                    for (int j = 2; j <= i; j++)
                    {

                        if (j * j > i) break;
                        if (i % j == 0) 
                        {

                            isNotPrime[i] = true;
                            break; 
                        }
                    }
                }
            }

            void CntPrime(int _nFac, int _add)
            {

                for (int i = 2; i <= info[2]; i++)
                {

                    if (isNotPrime[i]) continue;

                    int chk = _nFac;
                    while(chk > 0)
                    {

                        chk /= i;
                        nums[i] += (chk * _add);
                    }
                }
            }
#else
            int[] info = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            // 소수 판정
            bool[] isNotPrime = new bool[info[2] + 1];
            ChkPrime();

            // 조합의 소인수 찾기
            long[] nums = new long[info[2] + 1];
            CntPrime();

            // 가능한 수 찾기 -> 해당부분으로 속도가 느렸다;
            bool[] possible = new bool[info[2] + 1];
            possible[1] = true;
            for (int i = 2; i <= info[2]; i++)
            {

                while (nums[i] > 0)
                {

                    nums[i]--;
                    for (int j = info[2] / i; j >= 1; j--)
                    {

                        if (!possible[j]) continue;
                        possible[j * i] = true;
                    }
                }
            }

            // 가능한 수 중 가장 큰 값 찾기
            int ret = -1;
            for (int i = info[2]; i >= 1; i--)
            {

                if (!possible[i]) continue;
                ret = i;
                break;
            }

            Console.WriteLine(ret);

            void ChkPrime()
            {

                for (int i = 2; i <= info[2]; i++)
                {

                    if (isNotPrime[i]) continue;

                    for (int j = i * 2; j <= info[2]; j += i)
                    {

                        isNotPrime[j] = true;
                    }
                }
            }

            void CntPrime()
            {

                for (int i = 2; i <= info[2]; i++)
                {

                    if (isNotPrime[i]) continue;

                    long chk1 = info[0] + info[1];
                    long chk2 = info[0];
                    long chk3 = info[1];
                    while(chk1 + chk2 + chk3 > 0)
                    {

                        chk1 /= i;
                        chk2 /= i;
                        chk3 /= i;
                        nums[i] += chk1 - chk2 - chk3;
                    }
                }
            }
#endif
        }
    }

#if other
// #include <cstdio>
// #include <algorithm>
using namespace std;

int sieve[100003];
long long prcnt[100003];

long long countfactorial(long long lim, int p) {
	long long res = 0;
	while(lim) {
		res += lim / p;
		lim /= p;
	}
	return res;
}

int main(){
	int s,f,m;
	scanf("%d%d%d",&s,&f,&m);
	for (int i = 2; i <= m; i++) {
		if (sieve[i]) continue;
		for (int j = i; j <= m; j += i) {
			sieve[j] = i;
		}
		prcnt[i] = countfactorial(s+f, i) - countfactorial(s, i) -countfactorial(f, i);
	}
	for (int i = m; i >= 2; i--) {
		int num = i;
		int ok = 1;
		while(num > 1) {
			int div = sieve[num];
			int cnt = 0;
			for (;num % div == 0;) {
				num /= div;
				cnt++;
			}
			if (prcnt[div] < cnt) {
				ok = 0;
				break;
			}
		}
		if (ok) {
			printf("%d\n", i);
			return 0;
		}
	}
	printf("1\n");
	return 0;
}

#elif other2
// #include<bits/stdc++.h>

using namespace std;
typedef long long ll;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;

int main() {
    ios::sync_with_stdio(0);
    cin.tie(0);

    ll a,b,c;
    cin>>a>>b>>c;

    bool sieve[c+1];
    memset(sieve,0,sizeof(sieve));
    ll k[c+1];
    memset(k,0,sizeof(k));

    for(int i=2; i<=c; i++){
        if(sieve[i]) continue;
        for(int j=2*i; j<=c; j+=i) sieve[j]=true;
    }

    for(int i=2; i<=c; i++){
        if(!sieve[i]){
            ll t=a+b,p=a,q=b;
            while(t+p+q>0) {
                k[i]+=t/=i;
                k[i]-=p/=i;
                k[i]-=q/=i;
            }
        }
    }
    bool dp[c+1];
    memset(dp,0,sizeof(dp));
    dp[1]=true;
    for(int i=2; i<=c; i++){
        while(k[i]>0){
            k[i]--;
            for(ll j=c/i; j>=1; j--){
                if(dp[j]) dp[j*i]=true;
            }
        }
    }
    for(int i=c; i>=1; i--){
        if(dp[i]){
            cout<<i;
            return 0;
        }
    }
}
#endif
}
