using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 14
이름 : 배성훈
내용 : 배열 구간합 놀이
    문제번호 : 24838번
   
    그리디, 정렬, 누적합, 조합론 문제다.
    아이디어는 다음과 같다.

    최대 합은 그리디로 가장 많은걸 가장 큰 것을 더해주면 된다.
    그리고 방법은 해당 경우의 수 팩토리얼만큼 존재한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1112
    {

        static void Main1112(string[] args)
        {

            int MOD = 1_000_000_007;

            StreamReader sr;
            StreamWriter sw;

            Comparer<int> comp;

            int n, m, t;
            int[] A;
            int[] idx, cnt;
            long[] factorial;
            Solve();
            void Solve()
            {

                Init();

                for (int i = 0; i < t; i++)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                A = new int[50_001];
                idx = new int[50_002];
                t = ReadInt();
                cnt = new int[200_001];

                comp = Comparer<int>.Create((x, y) => y.CompareTo(x));
                factorial = new long[200_001];
                factorial[0] = 1;
                for (int i = 1; i <= 200_000; i++)
                {

                    factorial[i] = (factorial[i - 1] * i) % MOD;
                }
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    A[i] = ReadInt();
                }

                for (int i = 0; i < m; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    idx[x]++;
                    idx[y + 1]--;
                }
            }

            void GetRet()
            {

                for (int i = 1; i <= n; i++)
                {

                    idx[i] += idx[i - 1];
                    cnt[idx[i]]++;
                }

                Array.Sort(A, 1, n, comp);

                long ret1 = 0, ret2 = 1;
                int idx1 = 1;
                for (int i = m; i >= 1; i--)
                {

                    if (cnt[i] == 0) continue;
                    ret2 = (ret2 * factorial[cnt[i]]) % MOD;
                    while (cnt[i] > 0)
                    {

                        cnt[i]--;
                        ret1 += 1L * i * A[idx1++];
                    }
                }

                ret2 = (ret2 * factorial[cnt[0]]) % MOD;
                cnt[0] = 0;
                sw.Write($"{ret1} {ret2}\n");

                for (int i = 1; i <= n + 1; i++)
                {

                    idx[i] = 0;
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
using ll = long long;


int main() {
	cin.tie(0)->sync_with_stdio(0);

	ll fac[50001]{ 1 }, mod = 1e9 + 7;
	for (int i = 1; i <= 50000; i++)	fac[i] = fac[i - 1] * i % mod;

	int T;
	for (cin >> T; T--;) {
		int n, m;
		cin >> n >> m;
		vector<ll> arr(n);
		for (ll& i : arr)	cin >> i;
		sort(arr.begin(), arr.end(), greater<>());

		vector<int> s(n + 1), c(m + 1);
		for (int i = 0; i < m; i++) {
			int x, y;
			cin >> x >> y;
			s[--x]++, s[y]--;
		}
		int v = 0;
		for (int i = 0; i < n; i++) {
			v += s[i];
			c[v]++;
		}

		ll ans = 0, x = 0, cnt = 1;
		for (int i = m; i >= 0; i--) {
			if (!c[i])	continue;
			cnt = (cnt * fac[c[i]]) % mod;
			while (c[i]--)	ans += arr[x++] * i;
		}
		cout << ans << ' ' << cnt << '\n';
	}

}
#endif
}
