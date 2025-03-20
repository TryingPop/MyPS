using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 1
이름 : 배성훈
내용 : 수열과 쿼리 22
    문제번호 : 16978번

    세그먼트 트리, 오프라인 쿼리 문제다.
    쿼리를 뒤에 실행하면 된다.(오프라인 쿼리)

    즉, 2번 쿼리를 1번 쿼리의 진행횟수에 따라 정렬하고
    정렬된 2번 쿼리를 순차적으로 진행하면 단순 세그먼트 트리를 문제로 바뀐다.
    그리고 기존 순서에 따라 2번 쿼리를 출력한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1236
    {

        static void Main1236(string[] args)
        {

            int S, E;
            int n, m;
            long[] seg;

            List<(int idx, int val)> q1;
            List<(int idx, int s, int e, int order)> q2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                q2.Sort((x, y) => x.idx.CompareTo(y.idx));

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int chkIdx = 0;
                long[] ret = new long[q2.Count];

                for (int i = 0; i < q2.Count; i++)
                {

                    while (chkIdx < q2[i].idx)
                    {

                        Update(S, E, q1[chkIdx].idx, q1[chkIdx].val);
                        chkIdx++;
                    }

                    long val = GetVal(S, E, q2[i].s, q2[i].e);
                    ret[q2[i].order] = val;
                }

                for (int i = 0; i < ret.Length; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] = _val;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chk) Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);
                else Update(_s, mid, _chk, _val, _idx * 2 + 1);

                seg[_idx] = seg[_idx * 2 + 1] + seg[_idx * 2 + 2];
            }

            long GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return 0L;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                SetSeg();

                S = 1;
                E = n;
                for (int i = 1; i <= n; i++)
                {

                    Update(S, E, i, ReadInt());
                }

                m = ReadInt();
                q1 = new(m);
                q2 = new(m);

                int ord = 0;
                for (int i = 0; i < m; i++)
                {

                    int type = ReadInt();

                    if (type == 1)
                    {

                        int idx = ReadInt();
                        int val = ReadInt();

                        q1.Add((idx, val));
                    }
                    else
                    {

                        int idx = ReadInt();
                        int s = ReadInt();
                        int e = ReadInt();

                        q2.Add((idx, s, e, ord++));
                    }
                }

                sr.Close();

                void SetSeg()
                {

                    int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9)) + 1;
                    seg = new long[1 << log];
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
    }

#if other
// #include <bits/stdc++.h>
const char nl = '\n';
using namespace std;
using ll = long long;
using ld = long double;
const int N = 1e5+1;
const ll INFLL = 1e18;

ll bit[N];
void upd(int x, int v) {
  for (; x < N; x += x&-x) bit[x] += v;
}
ll query(int x) {
  ll ans = 0;
  for (; x; x -= x&-x) ans += bit[x];
  return ans;
}
ll query(int l, int r) {
  return query(r) - query(l-1);
}

char get() {
  static char buf[100000], *S = buf, *T = buf;
  if (S == T) {
    T = (S = buf) + fread(buf, 1, 100000, stdin);
    if (S == T) return EOF;
  }
  return *S++;
}
void read(int& x) {
  static char c; int sgn = 0; x = 0;
  for (c = get(); c < '0'; c = get()) if (c == '-') sgn = 1;
  for (; c >= '0'; c = get()) x = x * 10 + c - '0';
  if (sgn) x = -x;
}

int main() {
  cin.tie(0)->sync_with_stdio(0);
  int n; read(n);
  vector<int> a(n);
  for (int& i : a) read(i);
  for (int i = 1; i <= n; i++) {
    bit[i] = a[i-1];
  }
  for (int i = 1; i <= n; i++) {
    if (i + (i&-i) < N) bit[i + (i&-i)] += bit[i];
  }
  int q; read(q);
  vector<vector<tuple<int, int, int>>> qs(q+1);
  vector<pair<int, int>> upds;
  for (int i = 1; i <= q; i++) {
    int t; read(t);
    if (t == 1) {
      int x, v; read(x); read(v);
      upds.emplace_back(x, v-a[x-1]);
      a[x-1] = v;
    } else {
      int k, l, r; read(k); read(l); read(r);
      qs[k].emplace_back(l, r, i);
    }
  }
  vector<ll> ans(q+1, -1);
  for (int k = 0; k <= upds.size(); k++) {
    for (auto [l, r, i] : qs[k]) {
      ans[i] = query(l, r);
    }
    if (k == upds.size()) break;
    auto [x, v] = upds[k];
    upd(x, v);
  }
  for (ll i : ans) if (i != -1) cout << i << nl;
}

#endif
}
