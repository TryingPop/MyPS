using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 9
이름 : 배성훈
내용 : 조 짜기
    문제번호 : 2229번

    dp 문제다.
    dp[i]를 1 ~ i번 사람까지 조를 짰을 때 최대 점수가 담기게 한다.
    그러면 그리디로 dp[n]이 전체를 최대로 하는 조짜기 점수가 된다.

    그리고 dp[i]의 값은 j < i에 대해 dp[j] + j + 1 ~ i 번 사람이 조를 짰을 때의 최댓값이 된다.
    해당 방법으로 접근하는 경우 O(n^2)의 시간이 걸린다.
    n이 1000이므로 유효한 방법이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1878
    {

        static void Main1878(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int[] arr = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {

                arr[i] = ReadInt();
            }

            int[] dp = new int[n + 1];

            for (int j = 1; j <= n; j++)
            {

                // 혼자 조를 짜는경우다.
                dp[j] = dp[j - 1];
                int min = arr[j], max = arr[j];

                for (int i = j - 1; i > 0; i--)
                {

                    // 함께 조를 짜는 경우임
                    max = Math.Max(max, arr[i]);
                    min = Math.Min(min, arr[i]);

                    dp[j] = Math.Max(dp[j], max - min + dp[i - 1]);
                }
            }

            Console.Write(dp[n]);

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
// #include<cstdio>
// #include<algorithm>
using namespace std;
int main() {
	int n;
	int d[1001] = { 0 }, a[1001];
	scanf("%d", &n);
	for (int i = 1; i <= n; i++)
		scanf("%d", &a[i]);
	for (int i = 2; i <= n; i++)
	{
		int Min = 99999, Max = -1;
		for (int j = i; j >=1; j--)
		{
			Min = min(Min, a[j]);
			Max = max(Max, a[j]);
			d[i] = max(d[i], Max - Min + d[j - 1]);
		}
	}
	printf("%d", d[n]);
}
#endif
}
