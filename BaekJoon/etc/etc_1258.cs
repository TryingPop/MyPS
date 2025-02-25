using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 7
이름 : 배성훈
내용 : Cow Land
    문제번호 : 17033번

    HLD 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1258
    {

        static void Main1258(string[] args)
        {

            StreamReader sr;
            int n, m, bias;
            List<int>[] edge;
            int[] enjoy;
            int[] seg;
            (int idx, int head, int parent, int dep)[] chain;

            Solve();
            void Solve()
            {

                Input();

                SetWeight();

                SetSeg();

                SetChain();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                while (m-- > 0)
                {

                    int op = ReadInt();
                    int f = ReadInt();
                    int t = ReadInt();
                    if (op == 1) Update(f, t);
                    else
                    {

                        if (chain[f].dep > chain[t].dep)
                        {

                            int temp = f;
                            f = t;
                            t = temp;
                        }

                        int ret = 0;
                        while (chain[f].dep < chain[t].dep)
                        {

                            ret ^= GetVal(chain[chain[t].head].idx, chain[t].idx);
                            t = chain[t].parent;
                        }

                        while (chain[f].head != chain[t].head)
                        {

                            ret ^= GetVal(chain[chain[f].head].idx, chain[f].idx);
                            ret ^= GetVal(chain[chain[t].head].idx, chain[t].idx);

                            f = chain[f].parent;
                            t = chain[t].parent;
                        }

                        if (chain[f].idx > chain[t].idx)
                        {

                            int temp = f;
                            f = t;
                            t = temp;
                        }

                        ret ^= GetVal(chain[f].idx, chain[t].idx);
                        sw.Write($"{ret}\n");
                    }
                }

                sr.Close();
            }

            int GetVal(int _l, int _r)
            {

                int ret = 0;

                _l |= bias;
                _r |= bias;

                while (_l <= _r)
                {

                    if ((_l & 1) == 1) ret ^= seg[_l++];
                    if (((~_r) & 1) == 1) ret ^= seg[_r--];

                    _l >>= 1;
                    _r >>= 1;
                }

                return ret;
            }

            void Update(int _chk, int _val)
            {

                enjoy[_chk] = _val;
                int idx = chain[_chk].idx | bias;
                seg[idx] = _val;

                for (; idx > 1; idx >>= 1)
                {

                    seg[idx >> 1] = seg[idx] ^ seg[idx ^ 1];
                }
            }

            void SetChain()
            {

                chain = new (int idx, int head, int parent, int dep)[n + 1];
                chain[1].head = 1;
                int cnt = 1;
                DFS();

                for (int i = bias - 1; i > 0; i--)
                {

                    seg[i] = seg[i << 1] ^ seg[(i << 1) | 1];
                }

                void DFS(int _cur = 1, int _prev = 0, int _dep = 1)
                {

                    chain[_cur].dep = _dep;
                    chain[_cur].idx = cnt++;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        if (i == 0)
                        {

                            chain[next].head = chain[_cur].head;
                            chain[next].parent = chain[_cur].parent;

                            DFS(next, _cur, _dep);
                        }
                        else
                        {

                            chain[next].head = next;
                            chain[next].parent = _cur;

                            DFS(next, _cur, _dep + 1);
                        }
                    }

                    seg[bias | chain[_cur].idx] = enjoy[_cur];
                }
            }

            void SetSeg()
            {

                int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9));
                bias = 1 << log;
                seg = new int[1 << (log + 1)];
            }

            void SetWeight()
            {

                int[] weight = new int[n + 1];
                DFS();

                int DFS(int _cur = 1, int _prev = 0)
                {

                    ref int ret = ref weight[_cur];
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        ret += DFS(next, _cur);

                        if (weight[edge[_cur][0]] < weight[next] || edge[_cur][0] == _prev)
                        {

                            int temp = edge[_cur][0];
                            edge[_cur][0] = next;
                            edge[_cur][i] = temp;
                        }
                    }

                    return ret;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                enjoy = new int[n + 1];
                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    enjoy[i] = ReadInt();
                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    edge[f].Add(t);
                    edge[t].Add(f);
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
                    if (c == ' ' || c == '\n') return true;

                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #include <bits/stdc++.h>
// #include <unistd.h>
// #include <sys/mman.h>
using namespace std;

struct segtree { // 1-indexed
	segtree(int n) : n(n), lg(__lg(n - 1) + 1), sz(1 << lg), data(sz << 1, 0) {}
	void update(int i, const int& val) {
		data[--i |= sz] = val;
		while (i >>= 1) data[i] = data[i << 1] ^ data[i << 1 | 1];
	}
	int query(int l, int r) const {
		int res = 0;
		for (--l |= sz, --r |= sz; l <= r; l >>= 1, r >>= 1) {
			if (l & 1) res ^= data[l++];
			if (~r & 1) res ^= data[r--];
		}
		return res;
	}
private:
	const int n, lg, sz;
	vector<int> data;
};

// #define R(n) { n = 0; do n = 10 * n + *p - 48; while (*++p & 16); p++; }
// #define W(n) { int x = n; do *o++ = x % 10 | 48; while (x /= 10); do *u++ = *--o; while (o != t); *u++ = 10; }
int main() {
	char *p = (char*)mmap(0, 1 << 22, 1, 1, 0, 0);
	char w[1 << 20], t[20], *u = w, *o = t;
	int n, q; R(n) R(q)
	vector v(n + 1, 0);
	vector adj(n + 1, vector(0, 0));
	for (int i = 1; i <= n; i++) R(v[i])
	for (int i = 1; i < n; i++) {
		int a, b; R(a) R(b)
		adj[a].push_back(b);
		adj[b].push_back(a);
	}

	vector sz(n + 1, 1), dep(n + 1, 0), par(n + 1, 0);
	vector in(n + 1, 0), top(n + 1, 0);
	segtree tree(n);
	{
		auto dfs1 = [&](const auto& f, int cur, int prv) -> void {
			auto it = find(adj[cur].begin(), adj[cur].end(), prv);
			if (it != adj[cur].end()) adj[cur].erase(it);
			for (int& nxt : adj[cur]) {
				dep[nxt] = dep[cur] + 1;
				par[nxt] = cur;
				f(f, nxt, cur);
				sz[cur] += sz[nxt];
				if (sz[adj[cur][0]] < sz[nxt]) swap(adj[cur][0], nxt);
			}
		};
		auto dfs2 = [&](const auto& f, int cur) -> void {
			static int ord = 0;
			in[cur] = ++ord;
			tree.update(in[cur], v[cur]);
			for (int nxt : adj[cur]) {
				top[nxt] = nxt == adj[cur][0] ? top[cur] : nxt;
				f(f, nxt);
			}
		};
		dfs1(dfs1, 1, 0);
		dfs2(dfs2, top[1] = 1);
	}

	auto path_query = [&](int a, int b) -> int {
		int res = 0;
		for (; top[a] != top[b]; a = par[top[a]]) {
			if (dep[top[a]] < dep[top[b]]) swap(a, b);
			res ^= tree.query(in[top[a]], in[a]);
		}
		if (dep[a] > dep[b]) swap(a, b);
		res ^= tree.query(in[a], in[b]);
		return res;
	};

	while (q--) {
		int c, a, b; R(c) R(a) R(b)
		if (c == 1) tree.update(in[a], b);
		else W(path_query(a, b));
	}
	write(1, w, u - w);
}
#endif
}
