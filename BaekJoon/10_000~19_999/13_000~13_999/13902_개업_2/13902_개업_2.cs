using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 14
이름 : 배성훈
내용 : 개업, 개업 2
    문제번호 : 13910번, 13902번

    dp 문제다
    처음에 접근을 어떻게 해야할지 몰라 힌트를 봤고
    dp가 있어 혹시나 하는 마음에 배낭 형식으로 제출해봤다
    N = 10_000 에 N^2 이라 불안했지만 이상없이 통과했다

    그리고 다른 사람의 풀이를 보니 큐를 이용하던가
    a = b + c 에대해 b, c 무게가 가능한 경우 최소값을 찾는 방법이 좋아보여
    해당 방법으로 제출하니 반 이상의 속도가 줄었다
*/

namespace BaekJoon.etc
{
    internal class etc_1060
    {

        static void Main1060(string[] args)
        {

            int INF = 50_000;
            int n, m;
            int[] arr, dp;
            bool[] size;

            Solve();
            void Solve()
            {

                Input();
#if first
                SetSize();
#endif

                GetRet();
            }

#if first

            void SetSize()
            {

                size = new bool[n + 1];

                for (int i = 0; i < m; i++)
                {

                    size[arr[i]] = true;
                    for (int j = i + 1; j < m; j++)
                    {

                        int chk = arr[i] + arr[j];
                        if (n < chk) continue;
                        size[chk] = true;
                    }
                }
            }

            void GetRet()
            {

                dp = new int[n + 1];
                Array.Fill(dp, INF);
                dp[0] = 0;
                for (int i = 0; i < n; i++)
                {

                    if (dp[i] == INF) continue;

                    for (int j = 1; j <= n; j++)
                    {

                        if (!size[j]) continue;
                        int chk = i + j;
                        if (n < chk) break;
                        dp[i + j] = Math.Min(dp[i] + 1, dp[i + j]);
                    }
                }

                if (dp[n] == INF) Console.Write(-1);
                else Console.Write(dp[n]);
            }
#else

            void GetRet()
            {

                dp = new int[n + 1];
                Array.Fill(dp, INF);

                for (int i = 0; i < m; i++)
                {

                    dp[arr[i]] = 1;
                    for (int j = i + 1; j < m; j++)
                    {

                        int chk = arr[i] + arr[j];
                        if (n < chk) continue;
                        dp[chk] = 1;
                    }
                }

                for (int i = 1; i <= n; i++)
                {

                    if (dp[i] == 1) continue;

                    int len = i >> 1;

                    for (int j = 1; j <= len; j++)
                    {

                        if (dp[j] == INF || dp[i - j] == INF) continue;
                        dp[i] = Math.Min(dp[i], dp[j] + dp[i - j]);
                    }
                }

                if (dp[n] == INF) Console.Write(-1);
                else Console.Write(dp[n]);
            }
#endif

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                arr = new int[m];
                for (int i = 0; i < m; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();

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
    }

#if other
// #include<stdio.h>
// #include<algorithm>
using namespace std;
int n, m;
int a[1009];
int b[1000009];
int bl;
int d[100009];
int queue[100009];
int mask[100009];
int main()
{
	int i, j, k, l;
	scanf("%d %d", &m, &n);
	bl = 0;
	for (i = 1; i <= n; i++) {
		scanf("%d", &a[i]);
		for (j = 0; j < i; j++) {
			if (a[i] + a[j] <= m) {
				if (mask[a[i] + a[j]] == 0) {
					b[bl++] = a[i] + a[j];
					mask[a[i] + a[j]] = 1;
				}
			}
		}
	}
	sort(b, b + bl);
	for (i = 1; i <= m; i++) {
		d[i] = -1;
		mask[i] = 0;
	}
	d[0] = 0;
	mask[0] = 1;
	queue[0] = 0;
	int q, r;
	q = 0; r = 1;
	while (q < r) {
		int qi = queue[q++];
		for (i = 0; i < bl; i++) {
			int ti = qi + b[i];
			if (ti > m)break;
			if (mask[ti] == 0) {
				mask[ti] = 1;
				queue[r++] = ti;
				d[ti] = d[qi] + 1;
				if (ti == m) {
					q = r = 0;
					break;
				}
			}
		}
	}
	printf("%d\n", d[m]);
}
#elif other2
include <iostream>
// #include <vector>
// #include <queue>
// #include <cmath>
// #include <algorithm>

using std::vector;

using std::cout;
using std::cin;
using std::fill;
using std::min;

int n; // 1 - 10000
int cnt; // 1 ~ 100
int dp[20001]{};
int INF = 100000000;
int wuk[101]{};

int main()
{
	cin.tie(NULL);
	std::ios_base::sync_with_stdio(false);
	fill(dp + 1, dp + 10001, INF);
	cin >> n >> cnt;
	for(int i = 1; i <=cnt; ++i) cin >> wuk[i];

	for(int i = 1; i <= cnt; ++i)
	{
		for(int j = 1; j <= cnt; ++j)
		{
			if (i == j) dp[wuk[i]] = 1;
			else dp[wuk[i] + wuk[j]] = 1;
		}
	}

	for( int i = 1; i <= n; ++i)
	{
		if (dp[i] == 1) continue;

		for (int j = i / 2; j > 0; --j)
			dp[i] = min(dp[i], dp[i - j] + dp[j]);
	}

	if (dp[n] >= INF)
		cout << -1;
	else
		cout << dp[n];

	return 0;
}
#endif
}
