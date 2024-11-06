using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 23
이름 : 배성훈
내용 : 탈옥
    문제번호 : 13261번

    dp, 분할정복 트릭 문제다
    우선 점화식을 세웠을 때,
    dp[i, j] = min {k < j | dp[i, k] + C[k, j] }
    형태일 때,
    min이 되는 k의 값 중 가장 작은 값을 P(i, j) 라 하면
    opt(i, j) <= opt(i, j + 1)
    조건이 성립해야 한다

    opt(i, j) <= opt(i, j + 1)조건은
    a <= b <= c <= d 에 대해
    C[a, c] + C[b, d] <= C[a, d] + C[b, c] 로 검증해도 된다 (역은 성립 X)
    https://blog.myungwoo.kr/99

    이 단조성 증명이 가장 어렵다고 한다
    https://cp-algorithms.com/dynamic_programming/divide-and-conquer-dp.html
*/

namespace BaekJoon._58
{
    internal class _58_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr;

            int n, k;
            long[] sum;
            long ret;
            long[][] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 0; i <= n; i++)
                {

                    dp[1][i] = (sum[i] - sum[0]) * i;
                }

                for (int i = 2; i <= k; i++)
                {

                    DNC(i, 0, n, 0, n);
                }

                Console.Write(dp[k][n]);
            }

            void DNC(int _m, int _s, int _e, int _l, int _r)
            {

                if (_s > _e) return;
                int mid1 = (_s + _e) >> 1;

                dp[_m][mid1] = -1;

                int mid2 = -1;
                for (int i = _l; i <= _r; i++)
                {

                    long temp = dp[_m - 1][i] + (sum[mid1] - sum[i]) * (mid1 - i);
                    if (dp[_m][mid1] == -1 || dp[_m][mid1] > temp)
                    {

                        dp[_m][mid1] = temp;
                        mid2 = i;
                    }
                }

                DNC(_m, _s, mid1 - 1, _l, mid2);
                DNC(_m, mid1 + 1, _e, mid2, _r);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                sum = new long[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    sum[i] = sum[i - 1] +  ReadInt();
                }

                dp = new long[k + 1][];
                for (int i = 0; i <= k; i++)
                {

                    dp[i] = new long[n + 1];
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
typedef long long ll;
ll ps[8080];
ll dp[8080];
int numOfInterval[8080];
int L,G;

ll func(int i,int j){return dp[i]-i*(ps[j]-ps[i])-j*ps[i];}
int Q[8080],candidateQueue[8080];
int cross(int i,int j){
	int l=j+1,r=L+1;
	while(l<r){
		int m = l+r>>1;
		if(func(i,m)<=func(j,m))l=m+1;
		else r=m;
	}
	return l-1;
}

void monotoneQueueOpt(ll m){
	int front=0,rear=1;
	Q[0]=0,candidateQueue[0]=L;
	for(int i=1; i<=L; ++i){
		while(candidateQueue[front]<i)front++;
		dp[i]=func(Q[front],i)+m+i*ps[i];
		numOfInterval[i]=numOfInterval[Q[front]]+1;
		while(front+1<rear&&candidateQueue[rear-2]>=cross(Q[rear-1],i))rear--;
		candidateQueue[rear-1]=cross(Q[rear-1],i),Q[rear]=i,candidateQueue[rear++]=L;
	}
}

ll binarySearch(ll l, ll r){
	if(l>=r)return l;
	ll m = l+r>>1;
	monotoneQueueOpt(m);
	if(numOfInterval[L]>=G)return binarySearch(m+1,r);
	else return binarySearch(l,m);
}

int main() {
	ios::sync_with_stdio(0),cin.tie(0);
	cin>>L>>G;
	for(int i=1; i<=L; ++i)cin>>ps[i],ps[i]+=ps[i-1];
	ll x = binarySearch(0,1e13);
	monotoneQueueOpt(x);
	cout<<dp[L]-x*G;
}
#endif
}
