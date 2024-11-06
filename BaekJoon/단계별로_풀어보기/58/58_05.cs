using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 22
이름 : 배성훈
내용 : 김치
    문제번호 : 11001번

    dp, 분할정복을 사용한 최적화 문제다
    해당 사이트를 참조해 작성했다
    https://justicehui.github.io/ps/2019/07/17/BOJ11001/
    
    점화식을 세웠을 때
    dp[i, j] = min(dp[i - 1, k] + c[k, j])
    일 때, 쓸 수 있다

    시간은 O(kn^2) => O(kn log n)으로 줄어든다
    풀면서 감만 익히고 조건은 2회차에서 알아본다!
*/

namespace BaekJoon._58
{
    internal class _58_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr;

            int n, d;
            int[] t;
            int[] v;

            long ret;

            Solve();
            void Solve()
            {

                Input();

                DNC(0, n - 1, 0, n - 1);

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                d = ReadInt();

                t = new int[n];
                v = new int[n];
                for (int i = 0; i < n; i++)
                {

                    t[i] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    v[i] = ReadInt();
                }

                ret = 0;
                sr.Close();
            }

            void DNC(int _s, int _e, int _l, int _r)
            {

                if (_s > _e) return;
                int mid = (_s + _e) >> 1;
                long m = mid;
                int k = Math.Max(_l, mid - d);

                for (int i = k; i <= Math.Min(mid, _r); i++)
                {

                    long chk1 = (m - k) * t[mid] + v[k];
                    long chk2 = (m - i) * t[mid] + v[i];

                    if (chk1 < chk2) k = i;
                }

                long chk = (m - k) * t[mid] + v[k];
                ret = Math.Max(ret, chk);

                DNC(_s, mid - 1, _l, k);
                DNC(mid + 1, _e, k, _r);
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

// #define INF (1e9 + 5)
// #define FOR(i, a, b) for (int i = (a); i < (b); ++i)
// #define FOR_(i, a, b) for (int i = (a); i <= (b); ++i)

constexpr int N = 1e5 + 1; // 1e5;
int n, d;
int t[N], v[N];
long long maxVal;
//long long ans[N];

/*
// i 날 김치의 맛 최대값
// a[i] = max(v[k] + (i - k) * t[i]) for k that (i - d <= k <= i)
//
// a[i]를 결정하는 최소 k를 opt[i]라 하면
// v[k] + (i - k) * t[i] > v[k - x] + (i - k + x) * t[i] 이므로
// v[k] > v[k - x] + x * t[i] ... (1)
//
// a[i + 1]를 결정하는 opt[i + 1]를 고려하면 k = opt[i]일 때
// v[k] + (i - k + 1) * t[i + 1]과
// v[k - x] + (i - k + x + 1) * t[i + 1]를 비교하면
// (1)과 t[i] >= t[i + 1]에 의해
// v[k] > v[k - x] + x * t[i + 1]
//
// 즉 opt[i] <= opt[i + 1]
// 따라서 a[n / 2]와 opt[n / 2]를 O(d)만에 알면
// a[0] .. a[n / 2 -1]의 값은 opt[n / 2] 보다 작은 값 범위에서만 계산 해도 되고,
// a[n / 2 + 1] .. a[n]은 opt[n / 2] 보다 큰 범위에서만 계산해도 되므로
// 각 depth마다 값을 구하는데 총 O(d)만큼 걸리고
// depth는 log n 이므로
//
// time complexity 는 O(d * log n)
*/

// ans[s] 부터 ans[e] 까지 값을 구하고 값의 후보를 p부터 q까지만 확인한다
void work(int s, int e, int p, int q) {
    if (s > e)
        return;

    int mid = (s + e) / 2;

    //long long& ref = ans[mid];
    long long ans = -1;
    int opt = 0;

    FOR_(k, max(p, mid - d), min(mid, q)) {
        long long res = (long long)v[k] + (long long)(mid - k) * t[mid];
        if (res > ans) {
            opt = k;
            ans = res;
        }
    }

    maxVal = max<long long>(maxVal, ans);
    work(s, mid - 1, p, opt);
    work(mid + 1, e, opt, q);
}

int main() {
    //freopen("input.txt", "r", stdin);
    //freopen("output.txt", "w", stdout);
    ios_base::sync_with_stdio(false);
    cin.tie(0);

    cin >> n >> d;
    FOR(i, 0, n) {
        int x;
        cin >> x;
        t[i] = x;
    }
    FOR(i, 0, n) {
        int x;
        cin >> x;
        v[i] = x;
    }

    work(0, n - 1, 0, n - 1);

    cout << maxVal << '\n';

    return 0;
}
#endif
}
