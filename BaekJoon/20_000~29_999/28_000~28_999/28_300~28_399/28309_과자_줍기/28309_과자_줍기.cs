using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 19
이름 : 배성훈
내용 : 과자 줍기
    문제번호 : 28309번

    dp, 조합론, 페르마 소정리 문제다.
    N이 100이므로 N^3이 유효하다.
    그래서 벨만 포드 방법으로 각 정점에 대해 가장 많은 과자를 먹는 경우의 수를 찾는다.
    연산의 편의상 마지막 지점에 과자를 1개 놓는다.

    이후 끝지점의 과자의 갯수를 d라고하자.
    그러면 d - 1이고 끝지점에 도착가능한 노드에 대해 이동가능한 경우의 수를 찾는다.
    이렇게 역으로 이동하며 경우의 수를 누적해갔다.

    각 경우 N^3의 시간이 걸린다.

    경우의 수는 행의 차이를 r, 열의 차이를 c라하면
    (r + c)! / (r! c!)이 된다.

    1_000_003이 소수이다.(런타임 전에 확인했다.)
    그래서 페르마 소정리로 (r!)^(1_000_003 - 2) = (r!)^-1과 같다.
    이를 이용해 경우의 수를 분할 정복을 이용한 거듭제곱으로 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1715
    {

        static void Main1715(string[] args)
        {

            int MOD = 1_000_003;
            int S = 0, E = 1;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int row, col, n;
            (int r, int c)[] snacks;
            int[] cnt;
            long[] ret, fac;

            Init();

            int t = ReadInt();

            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 2; i < n; i++)
                {

                    for (int j = 2; j < n; j++)
                    {

                        for (int k = 2; k < n; k++)
                        {

                            if (j == k || NotMove(j, k)) continue;

                            cnt[k] = Math.Max(cnt[k], cnt[j] + 1);
                        }
                    }
                }

                for (int i = 2; i < n; i++)
                {

                    cnt[1] = Math.Max(cnt[1], cnt[i] + 1);
                }

                for (int d = cnt[1]; d > 0; d--)
                {

                    for (int cur = 1; cur < n; cur++)
                    {

                        if (cnt[cur] != d) continue;

                        for (int prev = 0; prev < n; prev++)
                        {

                            if (cnt[prev] + 1 != cnt[cur] || NotMove(prev, cur)) continue;
                            ret[prev] = (ret[prev] + (ret[cur] * Cnt(prev, cur)) % MOD) % MOD;
                        }
                    }
                }

                sw.Write(ret[0]);
                sw.Write('\n');

                long Cnt(int _f, int _t)
                {

                    int subR = snacks[_t].r - snacks[_f].r;
                    int subC = snacks[_t].c - snacks[_f].c;

                    long ret = fac[subR + subC];
                    long div1 = Pow(fac[subR], MOD - 2);
                    long div2 = Pow(fac[subC], MOD - 2);

                    ret = (ret * div1) % MOD;
                    return (ret * div2) % MOD;
                }

                long Pow(long _a, int _exp)
                {

                    long ret = 1;

                    while (_exp > 0)
                    {

                        if ((_exp & 1) == 1) ret = (ret * _a) % MOD;

                        _exp >>= 1;
                        _a = (_a * _a) % MOD;
                    }

                    return ret;
                }

                bool NotMove(int _f, int _t)
                    => snacks[_t].r < snacks[_f].r 
                    || snacks[_t].c < snacks[_f].c;
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();
                n = ReadInt() + 2;
                snacks[1] = (row, col);

                cnt[1] = 0;
                ret[0] = 0;
                ret[1] = 1;

                for (int i = 2; i < n; i++)
                {

                    snacks[i] = (ReadInt(), ReadInt());
                    cnt[i] = 1;
                    ret[i] = 0;
                }
            }

            void Init()
            {

                int MAX = 102;
                snacks = new (int r, int c)[MAX];

                cnt = new int[MAX];
                ret = new long[MAX];
                snacks[0] = (1, 1);

                fac = new long[2_000_001];
                fac[0] = 1;
                for (int i = 1; i < fac.Length; i++)
                {

                    fac[i] = (fac[i - 1] * i) % MOD;
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
using ll = long long;

void fast_io() {
  cin.tie(nullptr)->sync_with_stdio(false);
}

const int MAXN = 100, MAXV = 1e6 + 2, MOD = 1e6 + 3;
int X[MAXN], Y[MAXN], N, M, L;
ll dp1[MAXN], dp2[MAXN][MAXN + 1];
ll fac[MAXV + 1], ifac[MAXV + 1];

ll C(int n, int k) {
  if (n < 0 || k < 0 || n < k) return 0;
  if (n >= MOD) {
    return C(n / MOD, k / MOD) * C(n % MOD, k % MOD) % MOD;
  }
  return fac[n] * ifac[k] % MOD * ifac[n - k] % MOD;
}

ll lpow(ll x, ll y, ll m) {
  ll r = 1;
  x %= m;
  while (y) {
    if (y & 1) r = (r * x) % m;
    x = (x * x) % m;
    y >>= 1;
  }
  return r;
}

int rec1(int v) {
  if (dp1[v] != -1) return dp1[v];

  int ret = 1;
  for (int u = 0; u < L; ++u) {
    if (u == v || X[u] < X[v] || Y[u] < Y[v]) continue;
    ret = max(ret, rec1(u) + 1);
  }
  return dp1[v] = ret;
}

ll rec2(int v, int t) {
  if (t == 0) {
    int xd = N - X[v], yd = M - Y[v];
    return C(xd + yd, xd);
  }
  if (dp2[v][t] != -1) return dp2[v][t];

  ll ret = 0;
  for (int u = 0; u < L; ++u) {
    if (u == v || X[u] < X[v] || Y[u] < Y[v]) continue;

    ll add = rec2(u, t - 1) * C(X[u] - X[v] + Y[u] - Y[v], X[u] - X[v]) % MOD;
    ret = (ret + add) % MOD;
  }
  return dp2[v][t] = ret;
}

void solve() {
  cin >> N >> M >> L;
  for (int i = 0; i < L; ++i) cin >> X[i] >> Y[i];

  memset(dp1, -1, sizeof(dp1)); int maxl = 0;
  for (int i = 0; i < L; ++i) maxl = max(maxl, rec1(i));

  memset(dp2, -1, sizeof(dp2)); ll ans = 0;
  for (int i = 0; i < L; ++i) {
    ll add = rec2(i, maxl - 1) * C(X[i] + Y[i] - 2, X[i] - 1) % MOD;
    ans = (ans + add) % MOD;
  }
  cout << ans << '\n';
}

int main() {
  fast_io();

  fac[0] = ifac[0] = 1;
  for (int i = 1; i <= MAXV; i++) fac[i] = fac[i - 1] * i % MOD;
  ifac[MAXV] = lpow(fac[MAXV], MOD - 2, MOD);
  for (int i = MAXV - 1; i >= 1; i--) ifac[i] = ifac[i + 1] * (i + 1) % MOD;

  int t = 1;
  cin >> t;
  while (t--) solve();
}

#endif
}
