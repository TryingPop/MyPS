using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 2
이름 : 배성훈
내용 : 조별과제 멈춰
    문제번호 : 23034번

    최소 신장 트리 문제다.
    아이디어는 다음과 같다.
    각 팀장을 뽑았을 거리가 최소가 되어야 한다.
    이는 최소 신장 트리에서 두 노드로 가는 경로 상 가장 긴 길이를 빼는것과 같다.
    그래서 먼저 최소신장 트리를 찾았다.

    n의 크기가 1000으로 작아 아직 탐험안된 노드라면 
    다익스트라로 경로를 탐색해 가장 긴 경로를 빼는 방법을 택했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1142
    {

        static void Main1142(string[] args)
        {

            int n, m;
            StreamReader sr;
            PriorityQueue<(int idx1, int idx2, int dis), int> pq;
            Queue<int> q;
            List<(int dst, int dis)>[] edge;
            int total;
            bool[] visit;

            Solve();
            void Solve()
            {

                Input();

                MST();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                q = new(n);
                visit = new bool[n];
                int[][] ret = new int[n][];
                int query = ReadInt();

                for (int i = 0; i < query; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;

                    if (t < f)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    if (ret[f] == null) ret[f] = BFS(f);
                    sw.Write($"{total - ret[f][t]}\n");
                }

                sw.Close();
            }

            int[] BFS(int _s)
            {

                int[] ret = new int[n];
                Array.Fill(ret, -1);
                Array.Fill(visit, false);
                visit[_s] = true;
                ret[_s] = 0;
                q.Enqueue(_s);

                while (q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i].dst;
                        if (visit[next]) continue;
                        visit[next] = true;
                        ret[next] = Math.Max(ret[node], edge[node][i].dis);
                        q.Enqueue(next);
                    }
                }


                return ret;
            }

            void MST()
            {

                int[] group = new int[n];
                edge = new List<(int dst, int dis)>[n];
                for (int i = 0; i < n; i++)
                {

                    group[i] = i;
                    edge[i] = new();
                }

                int[] stk = new int[n];
                total = 0;
                while(pq.Count > 0)
                {

                    var node = pq.Dequeue();
                    int f = Find(node.idx1);
                    int b = Find(node.idx2);

                    if (f == b) continue;
                    
                    if (b < f)
                    {

                        int temp = f;
                        f = b;
                        b = temp;
                    }

                    group[b] = f;
                    edge[node.idx1].Add((node.idx2, node.dis));
                    edge[node.idx2].Add((node.idx1, node.dis));
                    total += node.dis;
                }

                int Find(int _chk)
                {

                    int len = 0;
                    while (group[_chk] != _chk)
                    {

                        stk[len++] = _chk;
                        _chk = group[_chk];
                    }

                    while (len > 0)
                    {

                        group[stk[--len]] = _chk;
                    }

                    return _chk;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                pq = new(m);

                for (int i = 0; i < m; i++)
                {

                    int idx1 = ReadInt() - 1;
                    int idx2 = ReadInt() - 1;
                    int dis = ReadInt();

                    pq.Enqueue((idx1, idx2, dis), dis);
                }
            }

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

#if other
// # include <bits/stdc++.h>
using namespace std;

template <typename T, typename F = less<T>>
class disjoint_set {
	const T e = 0x3f3f3f3f;
	const F cmp {};
	const int n;
	vector<int> par;
	vector<T> weight;

public:
	disjoint_set(int n) : n(n), par(n, -1), weight(n, e) {}
	int find(int u) {
		while (par[u] >= 0) u = par[u];
		return u;
	}
	bool unite(int u, int v, T w) {
		u = find(u), v = find(v);
		if (u == v) return false;
		if (par[u] > par[v]) swap(u, v);
		par[u] += par[v];
		par[v] = u;
		weight[v] = w;
		return true;
	}
	T query(int u, int v) {
		T ret = e;
		for (; u != v; u = par[u]) {
			if (cmp(weight[v], weight[u])) swap(u, v);
			ret = weight[u];
		}
		return ret;
	}
};

int main() {
	cin.tie(nullptr)->sync_with_stdio(false);
    int n, m;
    cin >> n >> m;
	vector<tuple<int, int, int>> e(m);
	for (auto& [u, v, w] : e) {
		cin >> u >> v >> w, --u, --v;
	}
sort(e.begin(), e.end(), [&](const auto& lhs, const auto& rhs) {
    return get < 2 > (lhs) < get < 2 > (rhs);
});
disjoint_set<int> dsu(n);
int mst = 0;
for (const auto& [u, v, w] : e) {
    if (dsu.unite(u, v, w)) mst += w;
}
cin >> m;
for (int u, v; m--;)
{
    cin >> u >> v, --u, --v;
    cout << mst - dsu.query(u, v) << '\n';
}
}
#endif
}
