using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 한강 (small)
    문제번호 : 12435번

    수학, 브루트포스, 정수론, 소수판정, 에라토스테네스의 체 문제다
    다 풀고나서 보니 조건대로 풀었다 
    준비과저잉 n root n 과, 매번 n의 탐색을해서 
    n * t + n root n 의 시간이 걸린다
    실제로 통과 시간이 1700ms이다

    아이디어는 다음과 같다
    찾아야할게 자기보다 작은 수들에 대해 약수의 개수와, 최소 약수를 알아야한다
    그래서 먼저 범위 안의 수들을 에라토스테네스의 체 이론으로 다 찾아놓고 배열에 저장했다

    이후에 매 케이스마다 저장된 수들을 찾아 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0552
    {

        static void Main552(string[] args)
        {

            StreamReader sr = new (new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 8);
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 1024 * 4);

            int[] minChild = new int[1_000_001];
            int[] numChild = new int[1_000_001];
            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                SetChild();
                int test = ReadInt();
                for (int t = 1; t <= test; t++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    int ret = 0;
                    int num = numChild[f];
                    
                    for (int i = b; i < f; i++)
                    {

                        if (numChild[i] == num && minChild[i] >= b) ret++;
                    }

                    sw.Write($"Case #{t}: {ret}\n");
                }
            }

            void SetChild()
            {

                bool[] setMin = new bool[1_000_001];
                for (int i = 2; i <= 1_000_000; i++)
                {

                    for (int j = i * 2; j <= 1_000_000; j += i)
                    {

                        numChild[j]++;
                        if (setMin[j]) continue;
                        setMin[j] = true;
                        minChild[j] = i;
                    }
                }
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
// #define endl '\n'
// #define prec(x)    \
    cout << fixed; \
    cout.precision(x);
typedef long long ll;
typedef vector<int> vi;
typedef vector<ll> vl;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;
typedef vector<pii> vpii;
typedef vector<pll> vpll;
// #define all(v) v.begin(), v.end()
// #define minpq(T) priority_queue<T, vector<T>, greater<T> >
// #define For(i, j, k) for (int i = (int)(j); i <= (int)(k); i++)
// #define Rep(i, j, k) for (int i = (int)(j); i >= (int)(k); i--)
// #define debug1(x, y) cout << x << " :: " << y << " "
// #define coutn cout << "\n"

ll factornum[1000005], minfactor[1000005];
vl samefactornum[1000005];

ll n, m;

void SolveProblem(int tc) {
    cin >> n >> m;
    ll result = 0;
    for(int i : samefactornum[factornum[n]]) {
        if(i == n) break;
        result += (minfactor[i] != i && minfactor[i] >= m);
    }
    cout << "Case #" << tc << ": " << result << "\n";
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);

    for(ll i=2; i<=1000000; i++) {
        if(!minfactor[i]) minfactor[i] = i;
        for(ll j=i*i; j<=1000000; j+=i) if(!minfactor[j]) minfactor[j] = i;
    }

    factornum[1] = 1;
    for(ll i=2; i<=1000000; i++) {
        ll cur = i; int cnt = 0;
        while(cur % minfactor[i] == 0) { cur /= minfactor[i]; cnt++; }
        factornum[i] = factornum[cur] * (cnt + 1);
        samefactornum[factornum[i]].push_back(i);
    }

    int test_case = 1;
    cin >> test_case;
    for (int tc = 1; tc <= test_case; tc++) {
        SolveProblem(tc);
    }
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;

const int MAX = 1e6 + 5;

int main() {
	cin.tie(0);
	ios_base::sync_with_stdio(0);

	vector<int> divno(MAX), mindiv(MAX);
	for (int i = 2; i < MAX; i++) {
		for (int j = i; j < MAX; j += i) {
			divno[j]++;	
			if (mindiv[j] == 0) mindiv[j] = i;
		}
	}

	vector<vector<int>> sameno(MAX);
	for (int i = 2; i < MAX; i++) {
		sameno[divno[i]].push_back(i);
	}

	int t; cin >> t;
	for (int T = 1; T <= t; T++) {
		cout << "Case #" << T << ": ";
		int n, m;
		cin >> n >> m;
		int ans = 0;
		if (divno[n] > 1) {
			for (auto x : sameno[divno[n]]) {
				if (x < n) {
					if (mindiv[x] >= m) ans++;
				}
				else {
					break;
				}
			}
		}
		cout << ans << '\n';
	}
	return 0;
}
#elif other3
import math
mil=1000001
minbro=[-1 for _ in range(mil)]
siscount=[0 for _ in range(mil)]
for i in range(2,mil):
    for j in range(i*2,mil,i):
        if minbro[j]==-1:
            minbro[j]=i
            siscount[j]+=1
        else:
            siscount[j]+=1
sistercountdivision=[[] for _ in range(mil)]
for i in range(2,mil):
    sistercountdivision[siscount[i]].append(i)

t=int(input())
for i in range(1,t+1):
    answer=0
    n,m=map(int,input().split())
    for j in (sistercountdivision[siscount[n]]):
        if j>=n:
            break
        if minbro[j]==-1:
            continue
        if minbro[j]>=m:
            answer+=1
    print("Case #", end="")
    print(i,end="")
    print(": ",end="")
    print(answer)
#endif
}
