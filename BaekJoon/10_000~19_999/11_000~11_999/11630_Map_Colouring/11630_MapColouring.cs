using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 5
이름 : 배성훈
내용 : Map Colouring
    문제번호 : 11630번

    비트필드를 이용한 dp 문제다.
    어떻게 구현할지 몰라 답지를 찾아봤다.
    그러니 dp[idx] = val를 다음과 같이 설정하면 되었다.
    idx : 현재 선택된 노드들, val : 현재 선택된 노드를 칠하는데 필요한 최소 색깔
*/

namespace BaekJoon.etc
{
    internal class etc_1153
    {

        static void Main1153(string[] args)
        {

            int MAX = 16;
            int INF = 100_000;
            StreamReader sr;
            StreamWriter sw;

            int n, m;

            bool[][] adj;
            int[] dp;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                while (t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sw.Close();
                sr.Close();
            }

            void GetRet()
            {

                int e = 1 << MAX;
                for (int state = 1; state < e; state++)
                {

                    // 이어진게 있는지 확인
                    bool flag = true;
                    for (int x = 0; x < n; x++)
                    {

                        if ((state & (1 << x)) == 0) continue;
                        for (int y = 0; y < n; y++)
                        {

                            if ((state & (1 << y)) == 0) continue;
                            if (adj[x][y]) flag = false;

                            if (!flag) break;
                        }

                        if (!flag) break;
                    }

                    if (flag)
                        // 이어진게 없다면 1
                        // 해당 노드를 처음 방문하면 여기로 온다.
                        dp[state] = 1;
                    else
                    {

                        // 이어진게 있다면 이어진 경로를 조사
                        dp[state] = INF;
                        // 이어진 경로 조사이다.
                        for (int nState = (state - 1) & state; nState > 0; nState = (nState - 1) & state)
                        {

                            // 인접한 것 중 아직 이어지지 않은게 있다면 + 1해서 이어준다.
                            if (dp[nState] == 1) dp[state] = Math.Min(dp[state], dp[state - nState] + 1);
                        }
                    }
                }

                if (dp[(1 << n) - 1] <= 4) sw.Write($"{dp[(1 << n) - 1]}\n");
                else sw.Write("many\n");
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    dp[i] = 0;
                    Array.Fill(adj[i], false, 0, n);
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    adj[f][t] = true;
                    adj[t][f] = true;
                }
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\r' && c != '\n')
                    {

                        ret = ret * 10 + c - '0';
                    }

                    if (c == '\r') sr.Read();
                    return false;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                adj = new bool[MAX][];
                dp = new int[1 << MAX];
                for (int i = 0; i < MAX; i++)
                {

                    adj[i] = new bool[MAX];
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <algorithm>
// #include <cstring>
// #include <random>

using namespace std;

random_device rd;
mt19937_64 seed(rd());

int N, M;
int graph[17][17];
int od[17], c[17];
bool v[6];

void solve() {
    scanf("%d %d", &N, &M);
    
    for(int i = 0; i < N; ++i)
        fill(graph[i], graph[i]+N, 0);
    for(int i = 0; i < N; ++i)
        od[i] = i;
    for(int i = 0; i < M; ++i)
    {
        int s, e;
        scanf("%d %d", &s, &e);
        graph[s][e] = graph[e][s] = 1;
    }

    int res = N;
    for(int k = 1; k <= N*7; ++k) {
        fill(c, c+N, 0);
        int mx = 0;
        shuffle(od, od+N, mt19937(rd()));
        for(int i = 0; i < N; ++i) {
            fill(v, v+6, false);
            for(int j = 0; j < N; ++j)
                if(graph[od[i]][j])
                    v[c[j]] = true;
            c[od[i]] = 5;
            for(int j = 5; j >= 1; --j)
                if(!v[j])
                    c[od[i]] = j;
            mx = max(mx, c[od[i]]);
            if(mx == 5)
                break;
        }
        res = min(res, mx);
    }

    if(res > 4)
        printf("many\n");
    else
        printf("%d\n", res);
    return;
}

int main()
{
    int TC;
    scanf("%d", &TC);
	while(TC--)
        solve();
	return 0;
}
#elif other2
// #include <stdio.h>
// #include <algorithm>
// #include <vector>

using namespace std;

const int MAX_N = 16;

int N, M;
vector<int> Ed[MAX_N];
int Color[MAX_N]; bool Chk[MAX_N];

bool dfs(int v, int c) {
	if(Color[v] != 0) {
		if(Color[v] == c) return true;
		else return false;
	}
	Color[v] = c;
	int nc = 3 - c;
	for(int w : Ed[v]) if(Chk[w] == true && dfs(w, nc) == false) return false;
	return true;
}
int coloring(int s) {
	for(int i=0; i<N; i++) Color[i] = 0, Chk[i] = false;
	for(int i=0; i<N; i++) if(s & (1<<i)) Chk[i] = true;

	for(int i=0; i<N; i++) if(s & (1<<i)) if(Color[i] == 0)
		if(dfs(i, 1) == false) return 10;
	int maxC = -1;
	for(int i=0; i<N; i++) maxC = max(maxC, Color[i]);
	return maxC;
}
int main() {
	int TC; scanf("%d", &TC);
	for(int tc=1; tc<=TC; tc++) {
		scanf("%d%d", &N, &M);
		for(int i=0, x, y; i<M; i++) scanf("%d%d", &x, &y), Ed[x].push_back(y), Ed[y].push_back(x);
		int val = coloring((1<<N) - 1);
		if(val <= 2) printf("%d\n", val);
		else {
			for(int s=0; s<(1<<(N-1)); s++) {
				int nowVal = coloring(s) + coloring((1<<N) - 1 - s);
				val = min(val, nowVal);
				if(val == 3) break;
			}
			if(val <= 4) printf("%d\n", val);
			else puts("many");
		}
		for(int i=0; i<N; i++) Ed[i].clear(), Ed[i].shrink_to_fit();
	}
	return 0;
}
#endif
}
