using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 13
이름 : 배성훈
내용 : 사탕 가게
    문제번호 : 4781번

    dp, 배낭문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0966
    {

        static void Main966(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m;

            int[] dp;
            (int k, int m)[] candy;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                dp[0] = 0;
                for (int i = 0; i < n; i++)
                {

                    int e = Math.Max(m - candy[i].m, 0);
                    for (int j = 0; j <= e; j++)
                    {

                        if (dp[j] == -1) continue;
                        dp[j + candy[i].m] = Math.Max(dp[j] + candy[i].k, dp[j + candy[i].m]);
                    }
                }

                int ret = 0;
                for (int i = 0; i <= m; i++)
                {

                    ret = Math.Max(ret, dp[i]);
                }

                sw.Write($"{ret}\n");
        }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                candy = new (int k, int m)[5_000];

                dp = new int[10_001];
            }

            bool Input()
            {

                n = ReadInt();
                m = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    candy[i] = (ReadInt(), ReadInt());
                }

                for (int i = 0; i <= m; i++)
                {

                    dp[i] = -1;
                }
                return n != 0;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r' || c == '.') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>

using namespace std;

int main() {
	ios::sync_with_stdio(false);
	cin.tie(0);

	int n, m;
	double _m;
	while (cin >> n >> _m) {
		if (!n) break;
		m = _m * 100 + 0.5;
		vector<pair<int, int>> a(n);
		for(int i =0; i<n; ++i) {
			int x;
			double y;
			cin >> x >> y;
			a[i] = {x, y * 100 + 0.5};
		}
		sort(a.begin(), a.end(), [](auto& a, auto& b) {
			return a.second < b.second || a.second == b.second && a.first > b.first;
		});
		vector<pair<int, int>> c = {a.front()};
		for(int i =1; i<n; ++i) {
			if (a[i].second != a[i - 1].second && c.back().first < a[i].first)
				c.push_back(a[i]);
		}
		vector<int> d(m + 1, -1);
		d[0] = 0;
		for(int j =0; j<=m; ++j) {
			for(auto & i : c) {
				if (j < i.second) break;
				if (~d[j - i.second]) d[j] = max(d[j], d[j - i.second] + i.first);
			}
		}
		cout << *max_element(d.begin(), d.end()) << '\n';
	}
}
#endif
}
