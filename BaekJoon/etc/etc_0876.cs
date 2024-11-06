using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

/*
날짜 : 2024. 8. 13
이름 : 배성훈
내용 : 샘터
    문제번호 : 18513번

    BFS 탐색 문제다
    누적합을 이용해 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0876
    {

        static void Main876(string[] args)
        {

            StreamReader sr;
            int n, k;
            int[] arr, cnt;

            Solve();
            void Solve()
            {

                Input();

                SetCnt();

                GetRet();
            }

            void GetRet()
            {

                long ret = 0;
                for (int i = 1; i < cnt.Length; i++)
                {

                    int pop = Math.Min(cnt[i], k);
                    k -= pop;
                    ret += pop * i;
                    if (k == 0) break;
                }

                Console.Write(ret);
            }

            void SetCnt()
            {

                Array.Sort(arr);
                int len = 1 + (k + 1) / 2;
                cnt = new int[len + 1];

                cnt[0] = 2;
                cnt[len] = -2;
                for (int i = 1; i < n; i++)
                {

                    cnt[0] += 2;
                    int diff = arr[i] - arr[i - 1] - 1;

                    int chk1 = 1 + diff / 2;
                    if (len < chk1) chk1 = len;
                    int chk2 = 1 + (diff + 1) / 2;
                    if (len < chk2) chk2 = len;

                    cnt[chk1]--;
                    cnt[chk2]--;
                }

                int add = 0;
                for (int i = 0; i <= len; i++)
                {

                    add += cnt[i];
                    cnt[i] = add;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
// #define X first
// #define Y second
// #define sz(x) (int)x.size()
// #define all(x) x.begin(), x.end()
// #define ini(x, y) memset(x, y, sizeof(x))
// #define endl '\n'
// #define fastio cin.sync_with_stdio(!cin.tie(nullptr))
using namespace std;

using ll = long long;
using pii = pair<int, int>;
using pll = pair<ll, ll>;
const int MOD = 1e9 + 7;
const int dx[] = { -1, 0, 1, 0, -1, 1, 1, -1 };
const int dy[] = { 0, 1, 0, -1, 1, 1, -1, -1 };

int main() {
	fastio;
	int N, K;
	cin >> N >> K;

	int pos[100000];
	for (int i = 0; i < N; ++i) cin >> pos[i];

	sort(pos, pos + N);

	int s = 1, e = 100000;
	while (s <= e) {
		int m = s + e >> 1;
		ll cnt = 0;

		for (int i = 1; i < N; ++i) {
			int len = pos[i] - pos[i - 1] - 1;
			cnt += min(len, 2 * m);
		}

		cnt += 2 * m;

		if (cnt >= K) e = m - 1;
		else s = m + 1;
	}

	ll ans = 0;
	for (int i = 1; i < N; ++i) {
		int len = pos[i] - pos[i - 1] - 1;
		if (len >= 2 * e) {
			ans += (ll)e * (e + 1);
			K -= 2 * e;
		}
		else {
			int a = len >> 1, b = len - a;
			ans += (ll)a * (a + 1) / 2;
			ans += (ll)b * (b + 1) / 2;
			K -= len;
		}
	}

	K -= 2 * e;
	ans += (ll)e * (e + 1);

	cout << ans + K * s;

	return 0;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;

typedef long long ll;

ll  n, k;
ll pos[100000];
priority_queue<ll, vector<ll>, greater<ll> > pq;

int main(void) {
  ios::sync_with_stdio(false);
  cin.tie(nullptr);

  cin >> n >> k;
  for(int i = 0; i < n; i++) cin >> pos[i];
  sort(pos, pos + n);

  pq.push(100000);
  for(int i = 0; i < n - 1; i++) {
	ll cur_right = (pos[i + 1] - pos[i] - 1) / 2;
	ll next_left = (pos[i + 1] - pos[i] - 1) - cur_right;
	pq.push(cur_right);
	pq.push(next_left);
  }
  pq.push(100000);

  ll tmp = 0;
  ll res = 0;
  ll cnt = 2 * n;
  while(true) {
	ll cur = pq.top();
	if(k - (cur - tmp) * cnt <= 0) break;
	pq.pop();
	k -= (cur - tmp) * cnt;
	res += cnt * ((cur + tmp + 1) * (cur - tmp)) / 2;
	cnt -= 1;
	tmp = cur;
  }

  for(int i = 0; i < k; i++) {
	if(i%cnt == 0) tmp += 1;
	res += tmp;
  }
  cout << res << "\n";
  return 0;
}
#elif other3
// #include <bits/stdc++.h>
// #define fastio ios::sync_with_stdio(0), cin.tie(0), cout.tie(0);
using namespace std;
using pii = pair<int, int>;
using i64 = long long;

int main() {
    fastio;

    int tmp = 100'050'000;
    int n, k;
    cin >> n >> k;
    vector<bool> vis(200'100'001);
    queue<pii> Q;
    for (int i = 0; i < n; ++i) {
        int srcLoc;
        cin >> srcLoc;
        srcLoc += tmp;
        vis[srcLoc] = true;
        Q.push({srcLoc, 0});
    }
    int cnt = 0;
    i64 unhappiness = 0;
    while (true) {
        int now = Q.front().first;
        int dist = Q.front().second;
        Q.pop();
        for (auto& next : {now+1, now-1}) {
            if (vis[next]) continue;
            vis[next] = true;
            cnt++;
            unhappiness += (dist+1);
            if (cnt == k) {
                cout << unhappiness;
                return 0;
            }
            Q.push({next, dist+1});
        }
    }
}
#endif
}
