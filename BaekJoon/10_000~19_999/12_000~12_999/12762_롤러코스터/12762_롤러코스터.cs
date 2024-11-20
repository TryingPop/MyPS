using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 21
이름 : 배성훈
내용 : 롤러코스터
    문제번호 : 12762번

    dp? 문제다
    lis를 이용해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1127
    {

        static void Main1127(string[] args)
        {

            int n;
            int[] arr;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[] lis = new int[n];
                int[] left = new int[n];

                // 왼쪽 하이라이트 찾기
                int len = 0;
                left[0] = 1;
                lis[0] = arr[0];
                for (int i = 1; i < n; i++)
                {

                    int idx = GetLeft(arr[i]);
                    lis[idx] = arr[i];
                    left[i] = idx + 1;  // 자기자신 1개 추가

                    len = Math.Max(idx, len);
                }

                int ret = left[n - 1];
                len = 0;
                lis[0] = arr[n - 1];
                for (int i = n - 2; i >= 0; i--)
                {

                    int idx = GetLeft(arr[i]);
                    lis[idx] = arr[i];
                    len = Math.Max(idx, len);
                    if (left[i] == 1) continue; // 왼쪽 하이라이트가 없으면 무시
                    ret = Math.Max(idx + left[i], ret);
                }

                Console.Write(ret);

                int GetLeft(int _val)
                {

                    int l = 0;
                    int r = len;
                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (_val < lis[mid]) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
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
    }

#if other
// #include <cstdio>
// #include <algorithm>
using namespace std;
int N;
int p[1001];
int dp1[1001];
int dp2[1001];
int main() {
	scanf("%d", &N);
	for (int n = 0; n < N; n++) scanf("%d", &p[n]);

	for (int n = 0; n < N; n++) {
		dp1[n] = 1;
		for (int m = 0; m < n; m++) {
			if (p[m] > p[n] && dp1[n] < dp1[m] + 1) {
				dp1[n] = dp1[m] + 1;
			}
		}
	}

	for (int n = N - 1; n >= 0; n--) {
		dp2[n] = 1;
		for (int m = N - 1; m > n; m--) {
			if (p[m] > p[n] && dp2[n] < dp2[m] + 1) {
				dp2[n] = dp2[m] + 1;
			}
		}
	}

	int ans = 0;
	for (int n = 0; n < N; n++) ans = max(ans, dp1[n] + dp2[n] - 1);
	printf("%d\n", ans);
	return 0;
}
#endif
}
