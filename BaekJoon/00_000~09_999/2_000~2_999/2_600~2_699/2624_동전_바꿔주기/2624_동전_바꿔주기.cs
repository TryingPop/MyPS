using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 13
이름 : 배성훈
내용 : 동전 바꿔주기
    문제번호 : 2624번

    dp, 배낭 문제다.
    가능한 경우의 수가 2^32 이하로 단순 배낭으로 풀린다.
*/

namespace BaekJoon.etc
{
    internal class etc_1763
    {

        static void Main1763(string[] args)
        {

            int n, k;
            int[] p, t;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dp = new int[n + 1];
                Array.Fill(dp, 0);

                int e = 0;
                dp[0] = 1;

                for (int i = 0; i < k; i++)
                {

                    for (int cur = e; cur >= 0; cur--)
                    {

                        if (dp[cur] == 0) continue;

                        for (int cnt = 1, next = cur + p[i]; cnt <= t[i] && next <= n; cnt++, next += p[i])
                        {

                            dp[next] += dp[cur];
                            e = Math.Max(next, e);
                        }
                    }
                }

                Console.Write(dp[n]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();
                p = new int[k];
                t = new int[k];

                for (int i = 0; i < k; i++)
                {

                    p[i] = ReadInt();
                    t[i] = ReadInt();
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
    }

#if other
// #include <algorithm>
// #include <cstdio>

int main() {
	int T, K;
	scanf("%d%d", &T, &K);
	int DP[T+1];
	std::fill_n(DP, T+1, 0);
	DP[0] = 1;
	for (int i = 0; i < K; ++i) {
		int p, n;
		scanf("%d%d", &p, &n);
		int pn = p * n;
		for (int j = T; j >= p; --j)
			for (int k = p; k <= pn && k <= j; k += p)
				DP[j] += DP[j-k];
	}
	printf("%d", DP[T]);
}
#endif
}
