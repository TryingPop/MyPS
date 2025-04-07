using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 5
이름 : 배성훈
내용 : 대기업 승범이네
    문제번호 : 17831번

    트리, dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1521
    {

        static void Main1521(string[] args)
        {

            int n;
            List<int>[] edge;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                // 0 : 현재 노드 선택 X
                // 1 : 현재 노드를 자식 노드와 잇기 시도
                // (해당 문제에서는 반드시 잇지만 음수인 경우 안 이을 수 있다.)
                long[][] dp = new long[2][];
                for (int i = 0; i < 2; i++)
                {

                    dp[i] = new long[n + 1];
                }

                DFS();
                Console.Write(Math.Max(dp[1][1], dp[0][1]));

                void DFS(int _cur = 1)
                {

                    long max = 0;
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        DFS(next);
                        
                        // 안 잇는 경우는 큰거 선택하면 된다.
                        dp[0][_cur] += Math.Max(dp[1][next], dp[0][next]);

                        // 잇는 경우 점수 확인
                        // 최댓값이 갱신되면 이전 최댓값을 빼주는 형식으로
                        // 연결부분을 바꾼다.
                        long chk = arr[_cur] * arr[next];

                        // 만약 자식 노드를 선택된 점수가 커서
                        // 선택안한 것을 이음으로써 생기는 손해부분 계산
                        long add = dp[0][next] - dp[1][next];

                        if (add > 0)
                        {

                            // 자식을 선택 안하는게 이득이므로 손해는 0
                            add = 0;
                            dp[1][_cur] += dp[0][next];
                        }
                        else
                            // 손해가 0이하면 일단 자식과 선택한 부분을 잇는다.
                            dp[1][_cur] += dp[1][next];

                        // 부모에서 자식을 잇는데
                        // 잇는데 선택된 자식은 잇지 않은 최댓값과 결합함으로써
                        // 자식이 중복 선택되는 경우 방지
                        chk += add;
                        if (max < chk)
                        {

                            dp[1][_cur] += chk - max;
                            max = chk;
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 2; i <= n; i++)
                {

                    int p = ReadInt();
                    edge[p].Add(i);
                }

                arr = new int[n + 1];
                for (int i = 1; i <= n; i++)
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
// #include <stdio.h>
// #include <vector>
using namespace std;

typedef long long LL;
// #define max(a, b) ((a)>(b)?(a):(b))
const int MAXN = 2e6+5;

int N, A[MAXN], P[MAXN], cnt[MAXN], Q[MAXN], fr, re;
LL dp[MAXN][2];

int main() {
	scanf("%d",&N);
	for (int i = 2; i <= N; i++) {
		scanf("%d",&P[i]);
		cnt[P[i]]++;
	}
	fr = 1;
	for (int i = 1; i <= N; i++) {
		scanf("%d",&A[i]);
		if (cnt[i] == 0) Q[++re] = i;
	}
	while (fr <= re) {
		int x = Q[fr++];
		if (x == 1) break;
		dp[P[x]][0] += dp[x][0]+dp[x][1];
		if (dp[x][1] == 0) dp[P[x]][1] = max(dp[P[x]][1], A[x]*A[P[x]]);
		else dp[P[x]][1] = max(dp[P[x]][1], -dp[x][1] + A[x]*A[P[x]]);
		if (--cnt[P[x]] == 0) Q[++re] = P[x];
	}
	printf("%lld\n", dp[1][0] + dp[1][1]);
}
#endif
}
