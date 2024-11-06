using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 31
이름 : 배성훈
내용 : 자동차경주대회
    문제번호 : 2651번

    dp 문제다
    DFS로 시작지점에서부터 시작해 끝지점부터 채워갔다
    해당 문제는 배낭문제처럼 푸는 것도 좋아 보인다

    난이도 기여를 보니 다익스트라도 된다고 한다
*/

namespace BaekJoon.etc
{
    internal class etc_0928
    {

        static void Main928(string[] args)
        {

            long INF = 1_000_000_000_000;

            StreamReader sr;
            int d, n, t;
            int[] dis, cost, go;
            long[] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                go = new int[n + 2];
                dp = new long[n + 2];
                Array.Fill(dp, -1);

                long ret1 = DFS(0);
                int ret2 = 0;
                int cur = go[0];
                while (cur != n + 1)
                {

                    cur = go[cur];
                    ret2++;
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    sw.Write($"{ret1}\n");
                    sw.Write($"{ret2}\n");
                    cur = go[0];
                    for (int i = 0; i < ret2; i++)
                    {

                        sw.Write($"{cur} ");
                        cur = go[cur];
                    }
                }
            }

            long DFS(int _n)
            {

                if (_n == n + 1) return 0;
                else if (dp[_n] != -1) return dp[_n];
                long ret = INF;

                for (int i = _n + 1; i <= n + 1; i++)
                {

                    int chkDis = dis[i] - dis[_n];
                    if (chkDis > d) break;

                    long chk = cost[i] + DFS(i);
                    if (chk <= ret)
                    {

                        ret = chk;
                        go[_n] = i;
                    }
                }

                return dp[_n] = ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                d = ReadInt();
                n = ReadInt();

                dis = new int[n + 2];
                cost = new int[n + 2];

                for (int i = 1; i <= n + 1; i++)
                {

                    dis[i] = ReadInt();
                }

                for (int i = 1; i <= n; i++)
                {

                    cost[i] = ReadInt();
                    dis[i] += dis[i - 1];
                }

                dis[n + 1] += dis[n];

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
// #include <cstdio>
// #include <climits>

int memo[105]={},r[105]={},d,n,a[105],t[105]={};

int f(int loc)
{
	if(loc==n)
		return 0;
	if(memo[loc]!=0)
		return memo[loc];
	int sum=0,min=INT_MAX;
	for(int i=loc+1; i<=n; i++)
	{
		sum+=a[i];
		if(sum>d)
			break;
		int k=f(i);
		if(k<=min)
		{
			r[loc]=i;
			min=k;
		}
	}
	memo[loc]=min+t[loc];
	return memo[loc];
}

int main()
{
	scanf("%d %d",&d,&n);
	n++;
	for(int i=1; i<=n; i++)
		scanf("%d",&a[i]);
	for(int i=1; i<n; i++)
		scanf("%d",&t[i]);
	printf("%d",f(0));
	int cnt=0;
	for(int i=r[0]; i!=0&&i!=n; i=r[i])
		cnt++;
	printf("\n%d\n",cnt);
	for(int i=0; r[i]!=0&&r[i]!=n; i=r[i])
		printf("%d ",r[i]);
	return 0;
}

#elif other2
// #include <iostream>
// #include <vector>
// #define ll long long
using namespace std;

ll dp[102];
ll preV[102];

int main() {
	cin.tie(NULL);
	ios_base::sync_with_stdio(false);
	
    ll D, N;
    cin >> D >> N;

    vector<ll> dist(N+2), time(N+2);
    for(int i=1; i<=N+1; i++) {
        ll a;
        cin >> a;
        dist[i] = a + dist[i-1];
        dp[i] = 1e18;
    }
    for(int i=1; i<=N; i++) cin >> time[i];

    for(int i=1; i<=N+1; i++) {
        for(int j=i-1; j>=0; j--) {
            if(dist[i] - dist[j] > D) break;

            if(dp[j] + time[i] < dp[i]) {
                preV[i] = j;
                dp[i] = dp[j] + time[i];
            }
        }
    }

    vector<int> ans;
    int idx = N+1;
    while(idx > 0) {
        idx = preV[idx];
        ans.push_back(idx);
    }

    
    cout << dp[N+1] << "\n";
    cout << ans.size()-1 << "\n";
    for(int i=ans.size()-2; i>=0; i--) 
        cout << ans[i] << " ";
    
	return 0;
}
#endif
}
