using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 25
이름 : 배성훈
내용 : 간식 파티
    문제번호 : 20162번

    dp 문제다
    찾아보니 LIS 문제라 한다

    앞에서부터 하나씩 찾아가는 방법으로 정답을 찾았다
    N log N 으로 풀 수 있을거 같아 보이는데
    시간 효율을 줄이는 아이디어는 당장 안떠오른다
*/

namespace BaekJoon.etc
{
    internal class etc_0835
    {

        static void Main835(string[] args)
        {

            StreamReader sr;
            int n;
            int[] arr, dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dp[0] = arr[0];

                for (int i = 1; i < n; i++)
                {

                    int add = 0;

                    for (int j = i - 1; j >= 0; j--)
                    {

                        if (arr[i] <= arr[j]) continue;
                        if (add < dp[j]) add = dp[j];
                    }

                    dp[i] = arr[i] + add;
                }

                int ret = dp.Max();
                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                dp = new int[n];
                sr.Close();
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
// #include <stdio.h>
typedef struct {
	int like;
	int total;
}Review;

int main() {
	int n;
	scanf("%d", &n);
	
	Review view[1000];
	for (int i = 0; i < n; i++) {
		scanf("%d", &view[i].like);
		view[i].total = view[i].like;
	}

	int max = 0;
	for (int i = n - 1; i >= 0; i--) {
		int max_total = 0;
		for (int j = i + 1; j < n; j++) {
			if (view[i].like < view[j].like)
				max_total = max_total > view[j].total ? max_total : view[j].total;
		}

		view[i].total += max_total;
		max = max > view[i].total ? max : view[i].total;
	}

	printf("%d", max);

	return 0;
}
#elif other2
using StreamWriter wt = new(Console.OpenStandardOutput());
using StreamReader rd = new(Console.OpenStandardInput());
int n = int.Parse(rd.ReadLine());
var a = new int[n];
var dp = new long[n];

for (int i = 0; i < n; i++)
    dp[i] = a[i] = int.Parse(rd.ReadLine());

for (int i = 1; i < n; i++)
    for (int j = i - 1; j >= 0; j--)
        if (a[i] > a[j])
            dp[i] = Math.Max(dp[i], dp[j] + a[i]);

wt.Write(dp.Max());
#endif
}
