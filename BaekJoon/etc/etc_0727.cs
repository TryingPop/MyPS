using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 26
이름 : 배성훈
내용 : 넴모넴모 (Easy, Hard)
    문제번호 : 14712번, 14700번

    dp, 비스마스킹 문제다
    dp 부분을 더 연습해야할거 같다
    풀기는 풀었으나 시간이 오래 걸렸다;

    아이디어는 다음과 같다
    2 * 2 사각형이 되면 못놓는 경우라 판단했다
    돌을 놓을 수 있으면 돌을 놓고, 
    모든 경우에서 돌을 안 놓을 수 있으므로 놓지 않는다

    이렇게 DFS로 일일히 돌을 놓으면서 진행했다
    이렇게 푸니 2152ms에 메모리는 305084kb를 썼다

    다른 사람의 풀이를 보니, 이중 포문으로 해결했고
    DFS보다 빨라 보였다

    그래서 DFS를 이중 포문으로 바꿔 푸니 2154ms -> 868ms로 줄었고
    다시 포문을 보니 굳이 많은 메모리를 써야하나 의문이들어 2개로 스와핑? 해주면서 하니
    30만 kb에서 7588 kb이하로 줄였다

    메모리 할당에 시간이 줄어들기에 속도도 868ms -> 680ms로 줄었다
    자세한건 first에 주석을 달아 놓는다
*/

namespace BaekJoon.etc
{
    internal class etc_0727
    {

        static void Main727(string[] args)
        {

            int MOD = 1_000_000_007;
            int BOOM;
            int row, col;
            int[][] dp;

#if first
			Solve();

            void Solve()
            {

				Init();

                int ret = DFS(0, 0);

                Console.WriteLine(ret);
            }

            int DFS(int _n, int _state)
            {

                // 누적해서 경우의 수를 기록해간다 
                // 여기서 탈출이 돌을 놓는 것과 같다
                if (_n >= row * col) return 1;

                // 처음 진입하는건지 확인
                if (dp[_n][_state] != -1) return dp[_n][_state];
                int ret = 0;
                dp[_n][_state] = 0;

                int nextState = _state >> 1;

                // 현재 놓았을 때, 돌이 2 * 2모양이 되는지 확인한다
                if (_n % col == 0 || (_state & BOOM) != BOOM) ret = (ret + DFS(_n + 1, nextState | (1 << col))) % MOD;
                
                // 돌을 안놓으면 2 * 2가 되지 않기에 안놓는건 따로 판별할 필요 X
                ret = (ret + DFS(_n + 1, nextState)) % MOD;

                // 찾은 값 기록
                dp[_n][_state] = ret;

                return ret;
            }
#elif second

			Solve();

			void Solve()
			{

				Init();

				dp[0][0] = 1;
				dp[0][1 << col] = 1;

				int n = row * col;
				int len = 1 << (col + 1);
				

				for (int f = n - 1; f >= 0; f--)
				{

					for (int b = 0; b < len; b++)
					{

						dp[f][b] = dp[f + 1][b >> 1];
						if (f % col != 0 && (b & BOOM) == BOOM) continue;

						dp[f][b] = (dp[f][b] + dp[f + 1][(b >> 1) | (1 << col)]) % MOD;
					}
				}

				Console.WriteLine(dp[0][0]);
			}

#else

            Solve();

            void Solve()
            {

                Init();

                dp[0][0] = 1;
                dp[0][1 << col] = 1;

                int n = row * col;
                int len = 1 << (col + 1);

				int[] temp;

                for (int f = n - 1; f >= 0; f--)
                {

					temp = dp[0];
					dp[0] = dp[1];
					dp[1] = temp;

                    for (int b = 0; b < len; b++)
                    {

                        dp[1][b] = dp[0][b >> 1];
                        if (f % col != 0 && (b & BOOM) == BOOM) continue;

                        dp[1][b] = (dp[1][b] + dp[0][(b >> 1) | (1 << col)]) % MOD;
                    }
                }

                Console.WriteLine(dp[1][0]);
            }
#endif

            void Init()
			{

                Read();

#if first
                // 앞 인덱스는 현재 돌을 놓을 위치
                dp = new int[row * col][];
                for (int i = 0; i < dp.Length; i++)
                {

                    // 뒤 인덱스는 현재 돌의 상태이다
                    // 2 * 5의 맵에서

                    // 현재 .을 놓는 상태라 가정 즉, 앞의 인덱스는 7이고
                    // 8번째 돌을 놓을려고 한다

                    // O X O O X
                    // O O . 

                    // 그러면 .의 왼쪽 위인 X를 나타내는게 (1 << 0) 으로 상태를 기록한다
                    // 그리고 바로 왼쪽의 인덱스는 (1 << col)으로 상태가 저장되어져 있다

                    // 그래서 뒤의 인덱스 b에 비트 연산을 하면
                    // 8번째 상태에서 왼쪽 위의 값이 X이므로 (1 << 0) & b == 0 이다
                    // 왼쪽에는 돌이 놓여져 있으므로 (1 << 5) & b != 0 이다
                    dp[i] = new int[1 << (col + 1)];
					Array.Fill(dp[i], -1);
                }
#elif second

				dp = new int[row * col + 1][];
				for (int i = 0; i < dp.Length; i++)
				{

					dp[i] = new int[1 << (col + 1)];
				}

				Array.Fill(dp[row * col], 1);
#else

				dp = new int[2][];
				dp[0] = new int[1 << (col + 1)];
				dp[1] = new int[1 << (col + 1)];

				Array.Fill(dp[1], 1);
#endif
			}
			
			void Read()
            {

                string[] temp = Console.ReadLine().Split();

                row = Convert.ToInt32(temp[0]);
                col = Convert.ToInt32(temp[1]);

                if (row < col)
                {

                    int min = row;
                    row = col;
                    col = min;
                }

                BOOM = (1 << 0) | (1 << 1) | (1 << col);
                
            }
		}
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
typedef long long int lld;
typedef pair<int,int> pi;
typedef pair<lld,lld> pl;
typedef pair<int,lld> pil;
typedef pair<lld,int> pli;
typedef vector<int> vit;
typedef vector<vit> vitt;
typedef vector<lld> vlt;
typedef vector<vlt> vltt;
typedef vector<pi> vpit;
typedef vector<vpit> vpitt;
typedef long double ld;
// #define x first
// #define y second
// #define pb push_back
// #define all(v) v.begin(), v.end()
// #define sz(x) (int)x.size()
// #define mk(a,b) make_pair(a,b)
bool isrange(int y,int x,int n,int m){
	 if(0<=y&&y<n&&0<=x&&x<m) return true;
	 return false;
}
int dy[4] = {1,0,-1,0},dx[4]={0,1,0,-1},ddy[8] = {1,0,-1,0,1,1,-1,-1},ddx[8] = {0,1,0,-1,1,-1,1,-1};
const int MAX = 17;
const lld mod = 1e9 + 7;
lld bit[(1<<MAX)],dp[(1<<MAX)],nbit[(1<<MAX)];
void solve(int tc){
	int n,m;
	scanf("%d%d",&n,&m);
	if(n>m) swap(n,m);
	if(n==1){
		lld xx = 1;
		for(int e=0;e<m;e++) xx = (xx*2)%mod;
		printf("%lld\n",xx);
	}else{
		for(int e=0;e<(1<<n);e++){
			int nb = 0;
			for(int p=1;p<n;p++){
				int k1 = (e&(1<<p))&&(e&(1<<(p-1)));
				nb += (1<<(p-1))*k1;
			}
			dp[nb]++;
			bit[nb]++;
		}
		int lim = (1<<(n-1)) - 1;
		for(int e=0;e<n-1;e++){
			for(int p=0;p<=lim;p++){
				if(p&(1<<e)) bit[p] = (bit[p]+bit[p^(1<<e)])%mod;
			}
		}
		for(int e=1;e<m;e++){
			memset(nbit,0,sizeof(nbit));
			for(int p=0;p<=lim;p++){
				int xbit = lim^p;
				nbit[p] = (nbit[p]+bit[xbit]*dp[p])%mod;
			}
			memset(bit,0,sizeof(bit));
			for(int p=0;p<=lim;p++) bit[p] = nbit[p];
			for(int p=0;p<n-1;p++){
				for(int q=0;q<=lim;q++){
					if(q&(1<<p)) bit[q] = (bit[q]+bit[q^(1<<p)])%mod;
				}
			}
		}
		printf("%lld",bit[lim]);	
	}
}


int main(void){
	/*
	ios_base :: sync_with_stdio(false);
	cin.tie(NULL);
	cout.tie(NULL);
	*/
	int tc = 1;
	/*
		scanf("%d",&tc);
	*/
	for(int test_number=1;test_number<=tc;test_number++){
		solve(test_number);
	}
	return 0;
}
#elif other2
// #include <bits/stdc++.h>
typedef long long ll;
long long newDP[1<<16];
long long DP[301][1<<17];
// #define MOD 1000000007
int M, N;

int main() {
	scanf("%d %d", &M, &N);
	int restrict = 0;
	if(M > N) {
		int tmp = M;
		M = N;
		N = tmp;
	}
	for(int i = 0; i < (M-1); i++) {
		restrict <<= 1;
		restrict |= 1;
	}
	for(int i = 0; i < 1<< M; i++) {
		DP[1][i] = 1;
	}
	for(int i = 2; i <= N; i++) {
		for(int j = 0; j < 1<<(M-1); j++) {
			newDP[j] = 0;
		}
		for(int j = 0; j < 1<<M; j++) {
			newDP[j&(j>>1)]=(newDP[j&(j>>1)]+DP[i-1][j])%MOD;
		}
		int a = 1;
		for(int j = 0; j < M-1; j++) {
			for(int k = 0; k < 1<<(M-1); k++) {
				if(!(k&a)) {
					newDP[k|a]=(newDP[k|a]+newDP[k])%MOD;
				}
			}
			a<<=1;
		}
		for(int j = 0; j < 1<<M; j++) {
			DP[i][j] = newDP[restrict&(~(j&(j>>1)))];
		}
	}
	ll res = 0;
	for(int i = 0; i < 1<<M; i++) {
		res = (res + DP[N][i])%MOD;
	}
	printf("%lld", res);
}
#elif other3
// #include <bits/stdc++.h>
// #define gibon ios::sync_with_stdio(false); cin.tie(0);
// #define fir first
// #define sec second
// #define pii pair<int, int>
// #define pll pair<ll, ll>
// #define pdd pair<long double, long double>
// #pragma GCC optimize("O3")
// #pragma GCC optimize("Ofast")
// #pragma GCC optimize("unroll-loops")
typedef long long ll;
using namespace std;
int dx[4]={0, 1, 0, -1}, dy[4]={1, 0, -1 , 0};
const int mxN=100020;
const int mxM=90;
const ll MOD=1000000007;
const ll INF=100000000000001;
int N, M;
ll dp[2][(1<<18)+5];
ll ans;
int main()
{
    gibon
    cin >> N >> M;
    if(N<M) swap(N, M);
    if(M==1)
    {
        ans=1;
        for(int i=0;i<N;i++)    ans*=2, ans%=MOD;
        cout << ans;
        return 0;
    }
    for(int i=0;i<(1<<M+1);i++)   dp[0][i]=1;
    for(int i=1;i<(N-1)*M;i++)
    {
        for(int j=0;j<(1<<M+1);j++)
        {
            dp[1][j]+=dp[0][j>>1];
            if(i%M==0 || (j&(1<<M))==0 || (j&1)==0 || (j&2)==0) dp[1][j]+=dp[0][(j>>1)+(1<<M)];
            dp[1][j]%=MOD;
        }
        for(int j=0;j<(1<<M+1);j++)
        {
            dp[0][j]=dp[1][j];
            dp[1][j]=0;
        }
    }
    for(int i=0;i<(1<<M+1);i++)   ans+=dp[0][i], ans%=MOD;
    cout << ans;
}

#elif other4
N, M = map(int, input().split())
if M > N:
    N, M = M, N
mod = 10 ** 9 + 7

if M == 17 and M == 17: print(967515437)
elif M == 16:
    if N == 16: print(452650629)
    elif N == 17: print(441264798)
    else: print(220968284)

elif M == 1:
    v = [0] * 9
    v[0] = 2
    for _ in range(1, 9):
        v[_] = v[_-1]*v[_-1] % mod

    ans = 1
    for _ in range(9):
        if N & (1 << _):
            ans *= v[_]
            ans %= mod
    print(ans)

else:
    L = 1 << M
    adj = [[] for _ in range(L >> 1)]

    bi = {0: [2, 1], 1: [0, 1]}

    for k in range(M-2):
        temp = {}

        for i in bi:
            x, y = bi[i]
            if x == 0 and y == 0:
                continue
            b = i << 1
            temp[b] = [0, 0]
            temp[b + 1] = [0, 0]
            if i & 1 == 0:
                temp[b][0] = x + y
                temp[b][1] = x
            else:
                temp[b][0] = y
            if i & 3 == 2:
                continue
            temp[b + 1][1] = y
        bi = temp

    d = {}
    for i in bi:
        x, y = bi[i]
        if x == 0 and y == 0:
            continue
        d[i] = x+y

    t = {i: d[i] for i in d}
    k = sorted(list(d))
    v = [0] * (L >> 1)

    memo = {}

    def g(num):
        start = 0
        a = 0
        while num:
            if num & 1:
                a += 1 << (M-2-start)
            start += 1
            num >>= 1
        return a

    for i in range(len(k)):
        x = k[i]
        if v[x]:
            for j in range(i + 1, len(k)):
                y = k[j]
                if v[y]:
                    continue
                if x & y == 0:
                    adj[y].append(x)
            continue
        jj = g(x)
        memo[x] = jj
        memo[jj] = x
        v[jj] = 1
        if x == 0:
            adj[x].append(x)
        for j in range(i+1, len(k)):
            y = k[j]
            if x & y == 0:
                adj[x].append(y)
                adj[y].append(x)

    for k in range(N-1):
        temp = {}
        for i in d:
            if i in temp:
                continue
            cnt = 0
            for j in adj[i]:
                cnt += t[j]
            temp[i] = (d[i] * cnt) % mod
            temp[memo[i]] = temp[i]
        t = temp
    print(sum(t.values()) % mod)

#endif
}
