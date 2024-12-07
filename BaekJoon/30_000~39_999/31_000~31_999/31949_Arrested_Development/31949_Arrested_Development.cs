using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 7
이름 : 배성훈
내용 : Arrested Development
    문제번호 : 31949번

    dp 문제다.
    아이디어가 안떠올라 힌트를 보았고, 배낭으로 접근했다.
    그러면 dp[i][j] = val를
    i번째까지 물건을 택했을 때, a가 j시간 만큼 일하고 b가 val 시간만큼 일한 경우가 되게 했다.
    그러면 val는 최소 시간을 찾기에 최소값으로 담는다.
    이렇게 진행하니 1.5초가 걸렸고, 다른 사람 풀이를 보니
    2차원 점화식 할 필요없이 1차원 배열으로 풀 수 있음을 알았다.
    그리고 매번 500만초를 확인하는게 아닌 확인된 누적 시간으로 확인하면 시간을 더 줄일 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1167
    {

        static void Main1167(string[] args)
        {

            int MAX = 5_000_000;
            int INF = 10_000_000;
            StreamReader sr;
            int n;
            (int a, int b)[] times;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

#if first

                int[][] dp = new int[2][];
                dp[0] = new int[MAX + 1];
                dp[1] = new int[MAX + 1];
                Array.Fill(dp[0], INF);
                Array.Fill(dp[1], INF);

                dp[0][0] = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = MAX; j >= 0; j--)
                    {

                        if (dp[0][j] == INF) continue;
                        dp[1][j + times[i].a] = Math.Min(dp[1][j + times[i].a], dp[0][j]);
                        dp[1][j] = Math.Min(dp[1][j], dp[0][j] + times[i].b);
                    }

                    for (int j = 0; j <= MAX; j++)
                    {

                        dp[0][j] = dp[1][j];
                        dp[1][j] = INF;
                    }
                }

                int ret = INF;
                for (int j = 0; j <= MAX; j++)
                {

                    int chk = Math.Max(dp[0][j], j);
                    ret = Math.Min(ret, chk);
                }

                Console.Write(ret);
#else

                int[] dp = new int[MAX + 1];

                int e = 0;
                for (int i = 0; i < n; i++)
                {

                    e += times[i].a;
                    for (int j = e; j >= times[i].a; j--)
                    {

                        dp[j] = Math.Min(dp[j] + times[i].b, dp[j - times[i].a]);
                    }

                    for (int j = 0; j < times[i].a; j++)
                    {

                        dp[j] += times[i].b;
                    }
                }

                int ret = INF;

                for (int i = 0; i <= e; i++)
                {

                    ret = Math.Min(ret, Math.Max(i, dp[i]));
                }

                Console.Write(ret);
#endif
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                times = new (int a, int b)[n];
                
                for (int i = 0; i < n; i++)
                {

                    times[i] = (ReadInt(), ReadInt());
                }

                sr.Close();
                int ReadInt()
                {

                    int ret = 0;
                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;
                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
// #include<bits/stdc++.h>
using namespace std;
int main() {
	int n; cin >> n;
	vector<pair<int,int>> v(n);
	int ta=0, tb=0;
	for(int i=0; i<n; ++i) {
		cin >> v[i].first >> v[i].second;
		ta += v[i].first;
		tb += v[i].second;
	}
	int ans = 2e9;
	sort(v.begin(),v.end(),[](auto& a, auto& b){return a.first>b.first;});
	vector<pair<int,int>> candi;
	candi.push_back({v[0].first,0});
	candi.push_back({0,v[0].second});
	ta -= v[0].first;
	tb -= v[0].second;
	for(int i=1; i<n; ++i) {
		vector<pair<int,int>> nxt;
		int lc = candi.size();
		for(int j=0; j<lc; ++j) {
			nxt.push_back({candi[j].first+v[i].first,candi[j].second});
			nxt.push_back({candi[j].first,candi[j].second+v[i].second});
		}
		sort(nxt.begin(),nxt.end());
		candi.clear();
		candi.push_back(nxt[0]);
		int ln = nxt.size(), now = nxt[0].second;
		for (int j=0; j<ln; ++j) {
			if (nxt[j].second >= now) continue;
			else if (nxt[j].first - nxt[j].second >= tb || nxt[j].second - nxt[j].first >= ta){
				ans = min ( ans , max(nxt[j].first,nxt[j].second) );
			} else {
				candi.push_back(nxt[j]);
				now = nxt[j].second;
			}
		}
		ta -= v[i].first;
		tb -= v[i].second;
	}
	for(auto& x : candi) {
	ans = min ( ans , max(x.first,x.second));
	}
	cout << ans;
}
#elif other2
// #include <bits/stdc++.h>
const char nl = '\n';
using namespace std;
using ll = long long;
using ld = long double;
const int T = 5e6+1;

int dp[T];

int main() {
  cin.tie(0)->sync_with_stdio(0);
  int n; cin >> n;
  int suma = 0;
  for (int tt = 0; tt < n; tt++) {
    int p, q; cin >> p >> q;
    suma += p;
    for (int i = suma; i >= p; i--) {
      dp[i] = min(dp[i-p], dp[i] + q);
    }
    for (int i = 0; i < p; i++) {
      dp[i] += q;
    }
  }
  int ans = T;
  for (int i = 0; i <= suma; i++) {
    ans = min(ans, max(i, dp[i]));
  }
  cout << ans << nl;
}

#endif
}
