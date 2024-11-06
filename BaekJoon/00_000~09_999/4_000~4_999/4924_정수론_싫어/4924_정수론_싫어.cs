using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 7
이름 : 배성훈
내용 : 정수론 싫어
    문제번호 : 4924번

    수학, 정수론, 에라토스테네스 체 문제다
    범위 합이 10억을 넘지 않는거 같다
    선형으로 찾아가는게 통과한다

    이후 선형과 비교하면서 log n시간으로 줄일려고 노력하니
    차이가 20 이하일 때만 반례가 존재해 해당 반례처리해 제출하니 이상없이 통과한다
    
*/

namespace BaekJoon.etc
{
    internal class etc_1034
    {

        static void Main1034(string[] args)
        {

            int S = 0;
            int E = 78_498;

            StreamReader sr;
            StreamWriter sw;

            int MAX = 1_000_000;
            int[] arr;
            int[] primes;
            int a, b;

            Solve();
            void Solve()
            {

                Init();
                int t = 0;

                while (Input())
                {

                    t++;
                    sw.Write($"{t}. {GetMax()}\n");
                }

                sr.Close();
                sw.Close();
            }

            int GetMax()
            {

                if (b - a <= 20)
                {

                    int ret = -123_456_789;
                    for (int i = a; i <= b; i++)
                    {

                        for (int j = i; j <= b; j++)
                        {

                            ret = Math.Max(ret, arr[j] - arr[i - 1]);
                        }
                    }

                    return ret;
                }

                int lidx = BinarySearch(a);
                int ridx = BinarySearch(b);
                if (b < primes[ridx]) ridx--;

                int l = a;
                int r = b;

                while (arr[primes[lidx]] <= arr[l - 1])
                {

                    l = primes[lidx] + 1;
                    if (r <= primes[lidx + 1]) break;
                    lidx++;
                }


                while (ridx >= 0 && arr[r] <= arr[primes[ridx] - 1])
                {

                    r = primes[ridx] - 1;
                    if (ridx == 0 || primes[ridx - 1] <= l) break;
                    ridx--;
                }

                return arr[r] - arr[l - 1];
            }

            int BinarySearch(int _val)
            {

                int l = S;
                int r = E;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (primes[mid] < _val) l = mid + 1;
                    else r = mid - 1;
                }

                return l;
            }

            bool Input()
            {

                a = ReadInt();
                b = ReadInt();

                if (a == -1 && b == -1) return false;
                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                SetArr();
            }

            void SetArr()
            {

                arr = new int[MAX + 1];
                primes = new int[E + 1];

                int idx = 0;
                for (int i = 2; i < MAX; i++)
                {

                    if (arr[i] != 0) continue;

                    long num = i;
                    while (num < MAX)
                    {

                        for (long j = num; j < MAX; j += num)
                        {

                            arr[j]++;
                        }

                        num *= i;
                    }

                    arr[i] = -1;
                    primes[idx++] = i;
                }

                primes[idx] = MAX - 1;

                for (int i = 1; i < MAX; i++)
                {

                    arr[i]--;
                    arr[i] += arr[i - 1];
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == -1 || c == ' ' || c == '\n') return true;

                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }

#if other
// #include <stdio.h>
// #define M 1000000

int L,U,p[M]={0,-1,},m;
long long i=2,j,k;
int main()
{
  for(;i<M;i++)
    if(p[i])
      p[i]--;
    else
    {
      for(j=i;j<M;j*=i)
        for(k=j;k<M;k+=j)
          p[k]++;
      p[i]=-2;
    }
  for(i=1;scanf("%d%d",&L,&U)&&U>0;
  printf("%lld. %d\n",i++,m))
    for(j=L,m=-2,k=0;j<=U;
    m=k>m?k:m,j++)
      k=k>0?k+p[j]:p[j];
  return 0;
}
#elif other2
// #include <iostream>
using namespace std;

int sieve[1000001];
int A[1000001];
void init() {
    for (int i = 2; i < 1000001; i++) {
        if (sieve[i]) continue;
        for (int j = i; j < 1000001; j += i) sieve[j] = i;
    }
    for (int i = 2; i < 1000001; i++) {
        A[i] = A[i / sieve[i]] + 1;
    }
    for (int i = 1; i < 1000001; i++) {
        if (A[i] == 1) A[i] = -1;
        A[i]--;
        A[i] += A[i - 1];
    }
}

int T, L, R;

void solve() {
    int ans = -2;
    for (int l = 0; l < 12; l++) {
        for (int r = 0; r < 12; r++) {
            if (L + l > R - r) continue;
            ans = max(ans, A[R - r] - A[L + l - 1]);
        }
    }
    cout << ans << '\n';
}

int main() {
    ios::sync_with_stdio(0);
    cin.tie(0);

    init();
    cin >> L >> R;
    while (L > 0) {
        T++;
        cout << T << ". ";
        solve();
        cin >> L >> R;
    }
}
#elif other3
// #include <cstdio>
// #include <algorithm>

using std::scanf;
using std::printf;
using std::min;
using std::max;

const int INF_INT = 1012345678;
const int MAXN = 1000000 + 10;
const int SGSZ = 1<<20;

int sv[MAXN];

struct SGNode {
    int s;
    int mx;
    int lfmx;
    int rgmx;
} sg[SGSZ * 2];

const SGNode IDENTITY = {
    0,
    -INF_INT,
    -INF_INT,
    -INF_INT,
};

SGNode promote_single(int v) {
    return {v, v, v, v};
}

SGNode merge(const SGNode& lf, const SGNode& rg) {
    int s = lf.s + rg.s;
    int mx = max({lf.mx, rg.mx, lf.rgmx + rg.lfmx});
    int lfmx = max(lf.lfmx, lf.s + rg.lfmx);
    int rgmx = max(rg.rgmx, rg.s + lf.rgmx);
    return {s, mx, lfmx, rgmx};
}

SGNode qry(int lo, int hi) {
    SGNode lres = IDENTITY;
    SGNode rres = IDENTITY;
    lo += SGSZ;
    hi += SGSZ;
    while (lo < hi) {
        if (lo % 2) {
            lres = merge(lres, sg[lo++]);
        }
        if (hi % 2) {
            rres = merge(sg[--hi], rres);
        }
        lo /= 2;
        hi /= 2;
    }
    return merge(lres, rres);
}

int readint() { signed n; scanf("%d", &n); return n; }

signed main() {
    sv[0] = sv[1] = 0;
    for (int i = 2; i < MAXN; ++i) {
        if (sv[i] == 0) {
            for (int j = i; j < MAXN; j += i) {
                sv[j] = i;
            }
        }
        sv[i] = sv[i / sv[i]] + 1;
    }
    for (int i = 0; i < MAXN; ++i) {
        if (sv[i] == 1) {
            sv[i] = -1;
        }
        --sv[i];
    }
    for (int i = 0; i < SGSZ; ++i) {
        if (i < MAXN) {
            sg[SGSZ + i] = promote_single(sv[i]);
        } else {
            sg[SGSZ + i] = IDENTITY;
        }
    }
    for (int i = SGSZ; i-- > 1; ) {
        sg[i] = merge(sg[i * 2], sg[i * 2 + 1]);
    }
    for (int itc = 1; ; ++itc) {
        int lo = readint();
        int hi = readint() + 1;
        if (lo == -1) break;
        int ans = qry(lo, hi).mx;
        printf("%d. %d\n", itc, ans);
    }

    return 0;
}

#endif
}
