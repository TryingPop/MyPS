using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 30 
이름 : 배성훈
내용 : 最高のコーヒー (Small, Large)
    문제번호 : 12455번, 12456번

    그리디, 우선순위 큐 문제다
    
*/

namespace BaekJoon.etc
{
    internal class etc_0924
    {

        static void Main924(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int t, n;
            long k;
            PriorityQueue<(long c, long t, long s), (long c, long t, long s)> pq1;
            PriorityQueue<(long c, long t, long s), (long c, long t, long s)> pq2;

            Solve();
            void Solve()
            {

                Init();

                t = ReadInt();

                for (int i = 1; i <= t; i++)
                {

                    Input();
                    long ret = GetRet();

                    sw.Write($"Case #{i}: {ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                n = ReadInt();
                k = ReadLong();

                for (int i = 0; i < n; i++)
                {

                    (long c, long t, long s) cocoa = (ReadLong(), ReadLong(), ReadInt());
                    pq1.Enqueue(cocoa, cocoa);
                }
            }

            long GetRet()
            {

                long ret = 0;
                while (pq1.Count > 0)
                {

                    (long c, long t, long s) cur = pq1.Dequeue();
                    long r = pq1.Count > 0 ? cur.t - pq1.Peek().t : cur.t;

                    while (r > 0)
                    {

                        if (pq2.Count > 0 && pq2.Peek().s >= cur.s)
                        {

                            (long c, long t, long s) eat = pq2.Dequeue();
                            long pop = Math.Min(r, eat.c);
                            r -= pop;
                            eat.c -= pop;

                            ret += eat.s * pop;
                            if (eat.c > 0) pq2.Enqueue(eat, eat);
                        }
                        else
                        {

                            long pop = Math.Min(r, cur.c);
                            r -= pop;
                            cur.c -= pop;

                            ret += cur.s * pop;
                            if (cur.c == 0) break;
                        }
                    }

                    while (r > 0 && pq2.Count > 0)
                    {

                        (long c, long t, long s) eat = pq2.Dequeue();
                        long pop = Math.Min(r, eat.c);

                        r -= pop;
                        eat.c -= pop;

                        ret += eat.s * pop;
                        if (eat.c > 0) pq2.Enqueue(eat, eat);
                    }

                    if (cur.c > 0) pq2.Enqueue(cur, cur);
                }

                pq2.Clear();
                return ret;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                pq1 = new(100, Comparer<(long c, long t, long s)>.Create((x, y) =>
                {

                    int ret = y.t.CompareTo(x.t);
                    if (ret == 0) y.s.CompareTo(x.s);
                    return ret;
                }));

                pq2 = new(100, Comparer<(long c, long t, long s)>.Create((x, y) =>
                {

                    int ret = y.s.CompareTo(x.s);
                    if (ret == 0) return y.t.CompareTo(x.t);
                    return ret;
                }));
            }

            long ReadLong()
            {

                int c;
                long ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
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
using ll = long long;
struct coffee {
	ll c, t; int s;
};
struct elem {
	ll c; int s;
	elem(ll c, int s) : c(c), s(s) {}
	bool operator <(const elem& rhs) const {
		return s < rhs.s;
	}
};

const int mxN = 100;

coffee a[mxN];

ll solve() {
	ll N, K; cin >> N >> K;
	for (int i = 0; i < N; ++i)
		cin >> a[i].c >> a[i].t >> a[i].s;

	sort(a, a + N, [&](auto & a, auto & b) {
		return a.t > b.t;
	});

	priority_queue<elem> pq;
	pq.emplace(a[0].c, a[0].s);

	ll ret = 0, cur = a[0].t;
	for (int i = 1; i < N; ++i) {
		ll gap = cur - a[i].t;
		cur = a[i].t;

		while (gap && !pq.empty()) {
			auto [c, s] = pq.top(); pq.pop();
			ll drink = min(c, gap);

			gap -= drink, c -= drink, ret += s * drink;
			if (c) pq.emplace(c, s);
		}
		pq.emplace(a[i].c, a[i].s);
	}

	while (cur && !pq.empty()) {
		auto [c, s] = pq.top(); pq.pop();
		ll drink = min(c, cur);

		cur -= drink, c -= drink, ret += s * drink;
	}
	return ret;
}
int main() {
	cin.tie(0), cout.tie(0);
	ios::sync_with_stdio(0);

	int T; cin >> T;
	for (int t = 1; t <= T; ++t)
		cout << "Case #" << t << ": " << solve() << '\n';
	return 0;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;
using ll = long long;

void fast_io() {
  cin.tie(nullptr)->sync_with_stdio(false);
}

int tc;
void solve() {
  cout << "Case #" << ++tc << ": ";

  ll N, K; cin >> N >> K;
  vector<tuple<ll, ll, ll>> A;
  for (int i = 0; i < N; ++i) {
    ll ci, ti, si; cin >> ci >> ti >> si;
    A.emplace_back(ti, ci, si);
  }
  sort(A.begin(), A.end(), greater<>());

  ll ans = 0, i = 0;
  priority_queue<pair<ll, ll>> pq;
  while (K) {
    while (i < N) {
      auto& [ti, ci, si] = A[i];
      if (ti < K) break;
      pq.emplace(si, ci);
      ++i;
    }

    ll nK = i == N ? 0 : get<0>(A[i]);
    ll g = K - nK;

    while (g && !pq.empty()) {
      auto [si, ci] = pq.top(); pq.pop();
      ll take = min(g, ci);
      ans += take * si;

      ci -= take; g -= take;
      if (ci) pq.emplace(si, ci);
    }

    K = nK;
  }

  cout << ans << '\n';
}

int main() {
  fast_io();

  int t = 1;
  cin >> t;
  while (t--) solve();
}
#endif
}
