using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 5
이름 : 배성훈
내용 : 로스팅하는 엠마도 바리스타입니다.
    문제번호 : 15647번

    트리, dp 문제다.
    아이디어는 다음과 같다.
    먼저 루트를 아무거나 잡는다 여기서는 편의상 1번 노드를 루트로 했다.

    각 노드 A를 루트로 서브트리로 만들었을 때
    서브트리에서 노드 A로 향하는 거리의 총합을 배열에 찾아 저장한다.
    모든 노드에 대해 해당 값들을 찾아 배열 sums에 저장하고,
    해당 서브트리에 포함된 노드의 수를 child에 저장했다.

    이후 1에서 1의 자식 B로 바꿀 때, 총 거리는
    1에서 B로 향하는 간선에 향하는 노드의 갯수만 변할 뿐,
    나머지 간선에 대해서는 변함이 없다.
    변하는 양은 (n - sums[B] * 2) x (해당 간선의 거리)가 된다.

    이렇게 자식으로 1칸씩 이동하면서 값을 변화시키며 찾아갔다.
*/

namespace BaekJoon.etc
{
    internal class etc_1520
    {

        static void Main1520(string[] args)
        {

            int n;
            List<(int dst, int dis)>[] edge;
            int[] child;
            long[] sums;

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                long[] ret = new long[n + 1];

                ret[1] = sums[1];
                DFS();
                void DFS(int _cur = 1, int _prev = 0)
                {

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (next == _prev) continue;

                        // 루트를 자식으로 바꿨을 때, 거리들의 총합의 변화량은 다음과 같다.
                        ret[next] = ret[_cur] - (2 * child[next] - n) * edge[_cur][i].dis;
                        DFS(next, _cur);
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= n; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }
            }

            void SetArr()
            {

                // 서브트리에 포함된 노드의 갯수
                child = new int[n + 1];
                // 서브트리에 대해 자식 노드들이 루트로 향하는 거리의 총합
                sums = new long[n + 1];

                DFS();
                void DFS(int _cur = 1, int _prev = 0)
                {

                    child[_cur] = 1;
                    sums[_cur] = 0;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (next == _prev) continue;

                        DFS(next, _cur);

                        child[_cur] += child[next];
                        sums[_cur] += sums[next] + child[next] * edge[_cur][i].dis;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                edge = new List<(int dst, int dis)>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int dis = ReadInt();

                    edge[f].Add((t, dis));
                    edge[t].Add((f, dis));
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
                        if (c == ' ' || c == '\n') return true;
                        ret = c - '0';

                        while((c =sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #include <bits/stdc++.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
using namespace std;

// #define int int64_t

using pii = pair<int, int>;

vector<pii> adj[300001];
int n, sz[300001], DP1[300001], DP2[300001];

void DFS1(int cur, int prev) {
	sz[cur] = 1;
	for (const auto& [nxt, cost] : adj[cur]) {
		if (nxt == prev) continue;
		DFS1(nxt, cur); sz[cur] += sz[nxt];
		DP1[cur] += DP1[nxt] + sz[nxt] * cost;
	}
}

void DFS2(int cur, int prev) {
	for (const auto& [nxt, cost] : adj[cur]) {
		if (nxt == prev) continue;
		DP2[nxt] = DP2[cur] + DP1[cur] - DP1[nxt] + (n - 2 * sz[nxt]) * cost;
		DFS2(nxt, cur);
	}
}

int32_t main() {
    struct stat st; fstat(0, &st);
	char* p = (char*)mmap(0, st.st_size, PROT_READ, MAP_SHARED, 0, 0);
	auto ReadInt = [&]() {
		int ret = 0;
		for (char c = *p++; c & 16; ret = 10 * ret + (c & 15), c = *p++);
		return ret;
	};
    
	n = ReadInt();
	for (int i = 1; i < n; i++) {
		int a = ReadInt(), b = ReadInt(), c = ReadInt();
		adj[a].push_back({ b, c });
		adj[b].push_back({ a, c });
	}
	DFS1(1, -1); DFS2(1, -1);
	for (int i = 1; i <= n; i++) cout << DP1[i] + DP2[i] << '\n';
}
#endif
}
