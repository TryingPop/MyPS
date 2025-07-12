using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 24
이름 : 배성훈
내용 : 가장 작은 수
    문제번호 : 26601번

    수학, 자료 구조, 그리디 알고리즘, 정수론, 우선순위 큐, 소수 판정, 에라토스테네스의 체 문제다
    아이디어는 다음과 같다
    n을 소인수 분해 했을 때
    n = (p_0 ^ k_0) * (p_1 ^ k_1) * ... (p_m ^ k_m)이라 하면 p_i는 소수이다
    약수의 개수는 (k_0 + 1) * (k_1 + 1) * ... * (k_m + 1)이 된다

    그래서 소수 p들을 넣고
    p^3, p^7, ... p^(2^k - 1) 승들을 넣어주면 된다
    150만까지 약 11만 4천개 존재해서 111222범위 안에 충분히 들어간다

    초기에는 pq로 넣어줬으나, 배열에 저장해 순차적으로 확인하면 pq는 필요없어 수정하니
    84ms -> 68ms로 줄었다
*/

namespace BaekJoon.etc
{
    internal class etc_1076
    {

        static void Main1076(string[] args)
        {

            int MOD = 2_000_003;
            int INF = 1_500_000;
            int n;

            bool[] chk = new bool[INF];

            Solve();
            void Solve()
            {

                n = int.Parse(Console.ReadLine());

                SetPrime();
                SetSq();

                GetRet();
            }

            void SetPrime()
            {

                chk[0] = true;
                chk[1] = true;

                for (int i = 2; i < INF; i++)
                {

                    if (chk[i]) continue;

                    for (int j = i << 1; j < INF; j += i)
                    {

                        chk[j] = true;
                    }
                }
            }

            void SetSq()
            {

                for (int i = 2; i < INF; i++)
                {

                    if (chk[i]) continue;

                    if (1_500 < i) continue;
                    long mul = i * i;

                    while (mul < INF)
                    {

                        chk[mul] = false;
                        mul *= mul;
                    }
                }
            }

            void GetRet()
            {

                long ret = 1;
                for (int i = 2; i < INF; i++)
                {

                    if (n == 0) break;
                    if (chk[i]) continue;
                    ret = (ret * i) % MOD;
                    n--;
                }

                Console.Write(ret);
            }
        }
    }
#if other
// #include <stdio.h>
// #include <vector>
// #include <algorithm>

using namespace std;

typedef long long lli;

const lli MOD = 2000003;

bool vis[1458320];

lli pw(lli x, lli n) {
	if (n == 0) return 1;
	if (n % 2) return x * pw(x, n - 1);
	else {
		lli ret = pw(x, n / 2);
		return ret * ret;
	}
}

int main() {
	int n;
	scanf("%d", &n);

	vector<int> v1;
	for (int i = 2; i <= 1458319; i++) {
		if (!vis[i]) {
			v1.push_back(i);
			for (int j = i + i; j <= 1458319; j += i)
				vis[j] = true;
		}
	}
	
	vector<int> v2;
	for (int i = 0; (lli)v1[i] * v1[i] <= v1.back(); i++) {
		int p = 2;
		while (true) {
			lli x = pw(v1[i], p);
			if (x > v1.back()) break;
			v2.push_back(x);
			p += p;
		}
	}

	for (int i = 0; i < v2.size(); i++)
		v1.push_back(v2[i]);

	sort(v1.begin(), v1.end());
	
	lli res = 1;
	for (int i = 0; i < n; i++)
		res = res * v1[i] % MOD;
	printf("%lld\n", res);
	return 0;
}
#elif other2
// #include<stdio.h>
// #include<vector>
// #include<queue>
// #define MOD 2000003
using namespace std;
typedef long long ll;
priority_queue<ll,vector<ll>,greater<ll> > Q;
char check[1500010]={0};
void find_prime(int num);
int main(void)
{
    int num;
    ll answer=1,now;
    find_prime(1500000);//소수들을 미리 넣는다.
    scanf("%d",&num);//확인할 수. 2^num 개의 약수를 갖는 가장 작은 수를 구할 것이다.
    for(int i=1; i<=num; i++)
    {//모든 소인수가 (2^n)-1의 형태여야 한다. 고로 현재 now에 곱할 수 있는 수들 중 가장 작은 것을 넣는다.
        now=Q.top(),Q.pop();//현재 가장 작은 소수의 거듭제곱. 소수별로 1+2+4+8...식의 형태로 쌓는다.
        answer*=now,answer%=MOD;
        Q.push(now*now);//현재 곱한 수를 제곱하고 큐에 넣는다.
    }
    printf("%lld",answer);//최종 정답을 출력한다.
    return 0;
}
void find_prime(int num)
{//소수들을 구하고 미리 우선순위 큐에 넣어준다.
    int cnt=0;
    for(int i=2; i<=num; i++)
    {
        if(check[i]==0)
        {
            cnt+=1,Q.push(i);
            for(int j=i+i; j<=num; j+=i)
            {
                check[j]=1;
            }
        }
    }
}

#endif
}
