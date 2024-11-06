using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 24
이름 : 배성훈
내용 : Money for Nothing
    문제번호 : 14636번

    분할정복 최적화, dp 문제다
    아이디어는 다음과 같다
    판매자들 중 이전에 판매자중 자기보다 싸게 파는 사람이 있으면
    그 사람은 최대 효율을 내게 해주는 사람이 아니다,
    즉 없어도 되는 사람이다

    그리고 구매자 중 끝이 자기보다 크거나 같은데 더 비싸게 사는 사람이 있으면
    그 사람은 최대 효율을 내게 해주는 사람이 아니다
    
    그래서 스택 문제처럼 없어도 되는 사람들을 제거해준다

    이후 i번 구매자를 이용해 이익이 최대가 되는 빨간 점의 번호를 opt(i)라하면
    그림으로 opt(i) <= opt(i + 1) 임을 알 수 있다
    이후에 남은 것에 대해 DNC를 진행해주면 된다
    
    DNC에서 이익이 음수인 경우 0으로 했는데
    이러니 분할할 opt(i)를 못찾아 계속해서 틀렸다
    이후 최소값 -4 * 10^18로 바꾸니 통과했다
*/

namespace BaekJoon._58
{
    internal class _58_07
    {

        static void Main7(string[] args)
        {

            StreamReader sr;
            int n, m;
            List<(int p, int d)> producer, consumer;
            long ret;

            Solve();
            void Solve()
            {

                Input();
                ret = 0;
                DNC(0, n - 1, 0, m - 1);

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                List<(int p, int d)> temp = new(500_000);

                for (int i = 0; i < n; i++)
                {

                    temp.Add((ReadInt(), ReadInt()));
                }

                temp.Sort((x, y) =>
                {

                    int ret = x.d.CompareTo(y.d);
                    if (ret != 0) return ret;

                    return x.p.CompareTo(y.p);
                });

                producer = new(n);
                producer.Add(temp[0]);
                int idx = 0;

                for (int i = 1; i < n; i++)
                {

                    if (producer[idx].p <= temp[i].p) continue;
                    producer.Add(temp[i]);
                    idx++;
                }

                temp.Clear();
                for (int i = 0; i < m; i++)
                {

                    temp.Add((ReadInt(), ReadInt()));
                }

                temp.Sort((x, y) =>
                {

                    int ret = y.d.CompareTo(x.d);
                    if (ret != 0) return ret;

                    return y.p.CompareTo(x.p);
                });

                consumer = new(m);
                consumer.Add(temp[0]);
                idx = 0;

                for (int i = 1; i < m; i++)
                {

                    if (temp[i].p <= consumer[idx].p) continue;
                    consumer.Add(temp[i]);
                    idx++;
                }

                consumer.Reverse();

                n = producer.Count;
                m = consumer.Count;

                sr.Close();
            }

            long Calc(int _idx1, int _idx2)
            {

                long w = consumer[_idx2].d - producer[_idx1].d;
                long h = consumer[_idx2].p - producer[_idx1].p;

                if (w <= 0 && h <= 0) return 0L;
                else return w * h;
            }

            void DNC(int _s, int _e, int _l, int _r)
            {

                if (_s > _e) return;
                int mid = (_s + _e) >> 1;
                int k = _l;

                long chk = -4_000_000_000_000_000_000;
                for (int i = _l; i <= _r; i++)
                {

                    long temp = Calc(mid, i);
                    if (chk < temp)
                    {

                        chk = temp;
                        k = i;
                    }
                }

                DNC(_s, mid - 1, _l, k);
                DNC(mid + 1, _e, k, _r);

                ret = Math.Max(ret, chk);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #pragma GCC optimize("O3")
const char nl = '\n';
using namespace std;
using ll = long long;
using ld = long double;
const ll INF = 2e18;

ll ans = 0;
vector<pair<int, int>> ps, cs;
void rec(int pl, int pr, int cl, int cr) {
  if (pl > pr) return;
  int m = (pl+pr)/2;
  // get optimal for m
  ll curans = -INF;
  int besti = cl;
  for (int i = cl; i <= cr; i++) {
    //if (cs[i].first <= ps[m].first) continue;
    if (cs[i].second <= ps[m].second) break;
    ll cur = 1LL*(cs[i].first-ps[m].first)*(cs[i].second-ps[m].second);
    if (cur > curans) {
      curans = cur;
      besti = i;
    }
  }
  ans = max(ans, curans);
  rec(pl, m-1, besti, cr);
  rec(m+1, pr, cl, besti);
}

char get() {
  static char buf[1000000], *S = buf, *T = buf;
  if (S == T) {
    T = (S = buf) + fread(buf, 1, 1000000, stdin);
    if (S == T) return EOF;
  }
  return *S++;
}
void read(int& x) {
  static char c; x = 0;
  for (c = get(); c < '0'; c = get());
  for (; c >= '0'; c = get()) x = x * 10 + c - '0';
}

int main() {
  cin.tie(0)->sync_with_stdio(0);
  auto read_vec = [](int n, auto cmp) {
    vector<pair<int, int>> v(n);
    for (auto& [p, q] : v) {
      read(p);
      read(q);
    }
    sort(begin(v), end(v), cmp);
    int top = 0;
    for (auto [p, q] : v) {
      while (top > 0 && !cmp(q, v[top-1].second)) top--;
      v[top++] = {p, q};
    }
    v.resize(top);
    return v;
  };
  int m, n; read(m); read(n);
  // max_{i, j} (q_j-p_i)(e_j-d_i)
  ps = read_vec(m, greater<>());
  cs = read_vec(n, less<>());
  //reverse(begin(ps), end(ps));
  rec(0, ps.size()-1, 0, cs.size()-1);
  cout << ans << nl;
}

#endif
}
