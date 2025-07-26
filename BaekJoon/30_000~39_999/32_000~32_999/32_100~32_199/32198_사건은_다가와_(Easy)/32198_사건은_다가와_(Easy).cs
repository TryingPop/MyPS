using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 26
이름 : 배성훈
내용 : 사건은 다가와 (Easy)
    문제번호 : 32198번

    dp 문제다.
    시간의 범위와 좌표의 범위가 1000단위로 작아
    시간에 따른 최소 이동좌표를 찾아 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1731
    {

        static void Main1731(string[] args)
        {

            int[][] dp;

            Input();

            SetDp();

            GetRet();

            void GetRet()
            {

                int ret = -1;
                for (int i = 0; i <= 2_000; i++)
                {

                    if (dp[1_000][i] < 0) continue;
                    if (ret == -1 || dp[1_000][i] < ret) 
                        ret = dp[1_000][i];
                }

                Console.Write(ret);
            }

            void SetDp()
            {

                dp[0][1_000] = 0;

                for (int i = 1; i <= 1_000; i++)
                {

                    for (int j = 0; j <= 2_000; j++)
                    {

                        if (dp[i - 1][j] < 0) continue;

                        SetVal(i, j, dp[i - 1][j]);
                        if (j > 0) SetVal(i, j - 1, dp[i - 1][j] + 1);
                        if (j < 2_000) SetVal(i, j + 1, dp[i - 1][j] + 1);
                    }
                }

                void SetVal(int _i, int _j, int _val)
                {

                    if (dp[_i][_j] == -2) return;
                    else if (dp[_i][_j] == -1 || _val < dp[_i][_j])
                        dp[_i][_j] = _val;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                dp = new int[1_001][];
                for (int i = 0; i <= 1_000; i++)
                {

                    dp[i] = new int[2_001];
                    Array.Fill(dp[i], -1);
                }

                int n = ReadInt();
                int OFFSET = 1_000;
                for (int i = 0; i < n; i++)
                {

                    int t = ReadInt();
                    int a = ReadInt() + OFFSET + 1;
                    int b = ReadInt() + OFFSET - 1;

                    for (int j = a; j <= b; j++)
                    {

                        dp[t][j] = -2;
                    }
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
// # include <bits/stdc++.h>

using namespace std;
typedef long long ll;

constexpr int INF = 1e9 + 100;

array<int, 3> A[101];
int dp[101][3];

int main(){
	int n;
	scanf("%d", &n);
	for (int i=1;i<=n;i++) scanf("%d %d %d", &A[i][0], &A[i][1], &A[i][2]);
	sort(A+1, A+n+1);

	int ans = INF;

	for (int i=1;i<=n;i++){
		for (int zi=1;zi<=2;zi++){
			dp[i][zi] = INF;
			for (int j=0;j<i;j++){
				for (int zj=1;zj<=2;zj++){
					int x1 = A[j][zj], t1 = A[j][0];
					int x2 = A[i][zi], t2 = A[i][0];

					if (abs(x1-x2) > t2-t1) continue;

					int flag = 0;
					for (int k=j+1;k<i;k++){
						if (A[k][0] >= t1 + abs(x1-x2)){
							if (A[k][1] < x2 && x2 < A[k][2]) flag = 1;
						}
						else{
							int pos = x1 + (A[k][0] - t1);
							if (x2 < x1) pos = x1 - (A[k][0] - t1);
							if (A[k][1] < pos && pos < A[k][2]) flag = 1;
						}
					}

					if (!flag) dp[i][zi] = min(dp[i][zi], dp[j][zj] + abs(x1-x2));
				}
			}

			int flag = 0;
			for (int j=i+1;j<=n;j++) if (A[j][1] < A[i][zi] && A[i][zi] < A[j][2]) flag = 1;
			if (!flag) ans = min(ans, dp[i][zi]);
		}
	}

	int flag = 0;
	for (int j=1;j<=n;j++) if (A[j][1] < 0 && 0 < A[j][2]) flag = 1;
	if (!flag) ans = min(ans, 0);

	if (ans==INF) printf("-1\n");
	else printf("%d\n", ans);
}
#endif
}
