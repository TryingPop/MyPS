using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 3
이름 : 배성훈
내용 : 구간 나누기
    문제번호 : 2228번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1670
    {

        static void Main1670(string[] args)
        {

            int n, m;
            int[] arr;
            int[][] max;        // max[s][e] = val는 구간 [s, e]에서 연속한 최댓값

            Input();

            SetMax();

            GetRet();

            void GetRet()
            {

                // dp[i][j] = val는
                // 연속된 구간이 i개 선택이고 끝이 j인 경우
                // 총합의 최댓값이 val
                int[][] dp = new int[m + 1][];
                int INF = -1_000_000_000;
                for (int i = 1; i <= m; i++)
                {

                    dp[i] = new int[n];
                    Array.Fill(dp[i], INF);
                }

                for (int j = 0; j < n; j++)
                {

                    dp[1][j] = max[0][j];
                }

                for (int i = 2; i <= m; i++)
                {

                    for (int j = 2; j < n; j++)
                    {

                        for (int k = 0; k < j - 1; k++)
                        {

                            int chk = dp[i - 1][k] + max[k + 2][j];
                            dp[i][j] = Math.Max(dp[i][j], chk);
                        }
                    }
                }

                int ret = INF;
                for (int i = 0; i < n; i++)
                {

                    ret = Math.Max(ret, dp[m][i]);
                }

                Console.Write(ret);
            }

            void SetMax()
            {

                // Kadane으로 찾는다.
                max = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    max[i] = new int[n];

                    int cur = arr[i];
                    max[i][i] = cur;

                    for (int j = i + 1; j < n; j++)
                    {

                        cur = Math.Max(cur + arr[j], arr[j]);
                        max[i][j] = cur;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
    }

#if other
// #include<stdio.h>
// #include<algorithm>
using namespace std;
// #define inf -2147483647
int dp[2][51][2], s[100];
int main()
{
	int n, m;
	scanf("%d%d", &n, &m);
	for (int i = 0; i < n; i++) scanf("%d", s + i);
	if (n == 1)
	{
		printf("%d\n", s[0]);
		return 0;
	}
	for (int i = 0; i < 51; i++)
	{
		dp[0][i][0] = dp[0][i][1] = inf;
	}
	dp[0][0][1] = s[1] + (s[0]>0 ? s[0] : 0);
	dp[0][1][0] = s[0];
	for (int i = 2; i < n; i++)
	{
		dp[1][0][1] = s[i] + (dp[0][0][1]>0 ? dp[0][0][1] : 0);
		for (int j = 1; j <= m; j++)
		{
			dp[1][j][0] = max(dp[0][j][0],dp[0][j-1][1]);
			dp[1][j][1] = max(dp[0][j][1], dp[0][j][0]);
			if (dp[1][j][1] != inf) dp[1][j][1] += s[i];
		}
		for (int j = 0; j <= m; j++)
		{
			dp[0][j][0] = dp[1][j][0];
			dp[0][j][1] = dp[1][j][1];
		}
	}
	printf("%d\n", max(dp[0][m][0], dp[0][m - 1][1]));
}
#endif
}
