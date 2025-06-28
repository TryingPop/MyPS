using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 5
이름 : 배성훈
내용 : 농장 관리
    문제번호 : 5916번

    느리게 갱신되는 세그먼트 트리 + HLD 문제다.
    HLD 연습용으로 풀었다.
    Update에서 chkS, chkE를 chkS, chkS로 잘못 작성해 1번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1249
    {

        static void Main1249(string[] args)
        {

            StreamReader sr;

            int P = 32, Q = 33;

            int S, E;

            int n, m;
            int[] child;
            List<int>[] edge;
            (int parent, int head, int dep, int num)[] chain;
            (long val, int lazy)[] lazySeg;
            
            Solve();
            void Solve()
            {

                Input();

                SetChild();

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

                    if (chain[f].dep > chain[t].dep)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    if (op == P)
                    {

                        while (chain[f].dep < chain[t].dep)
                        {

                            Update(S, E, chain[chain[t].head].num, chain[t].num);
                            t = chain[t].parent;
                        }

                        while (chain[f].head != chain[t].head)
                        {

                            Update(S, E, chain[chain[f].head].num, chain[f].num);
                            Update(S, E, chain[chain[t].head].num, chain[t].num);

                            f = chain[f].parent;
                            t = chain[t].parent;
                        }

                        if (chain[f].num > chain[t].num)
                        {

                            int temp = f;
                            f = t;
                            t = temp;
                        }

                        Update(S, E, chain[f].num + 1, chain[t].num);
                    }
                    else
                    {

                        long ret = 0;
                        while (chain[f].dep < chain[t].dep)
                        {

                            ret += GetVal(S, E, chain[chain[t].head].num, chain[t].num);
                            t = chain[t].parent;
                        }

                        while (chain[f].head != chain[t].head)
                        {

                            ret += GetVal(S, E, chain[chain[f].head].num, chain[f].num);
                            ret += GetVal(S, E, chain[chain[t].head].num, chain[t].num);

                            f = chain[f].parent;
                            t = chain[t].parent;
                        }

                        if (chain[f].num > chain[t].num)
                        {

                            int temp = f;
                            f = t;
                            t = temp;
                        }

                        ret += GetVal(S, E, chain[f].num + 1, chain[t].num);

                        sw.Write($"{ret}\n");
                    }
                }
            }

            void SetSeg()
            {

                int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9)) + 1;
                lazySeg = new (long val, int lazy)[1 << log];
                S = 1;
                E = n;
            }

            void Update(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate();

                if (_e < _chkS || _chkE < _s) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    lazySeg[_idx].lazy++;
                    LazyUpdate();
                    return;
                }

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);

                lazySeg[_idx].val = lazySeg[_idx * 2 + 1].val + lazySeg[_idx * 2 + 2].val;

                void LazyUpdate()
                {

                    int lazy = lazySeg[_idx].lazy;
                    if (lazy == 0) return;
                    lazySeg[_idx].lazy = 0;

                    lazySeg[_idx].val += (_e - _s + 1L) * lazy;

                    if (_s == _e) return;
                    lazySeg[_idx * 2 + 1].lazy += lazy;
                    lazySeg[_idx * 2 + 2].lazy += lazy;
                }
            }

            long GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate();

                if (_e < _chkS || _chkE < _s) return 0;
                else if (_chkS <= _s && _e <= _chkE) return lazySeg[_idx].val;

                int mid = (_s + _e) >> 1;

                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1) 
                    + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);

                void LazyUpdate()
                {

                    int lazy = lazySeg[_idx].lazy;
                    if (lazy == 0) return;
                    lazySeg[_idx].lazy = 0;

                    lazySeg[_idx].val += (_e - _s + 1L) * lazy;

                    if (_s == _e) return;
                    lazySeg[_idx * 2 + 1].lazy += lazy;
                    lazySeg[_idx * 2 + 2].lazy += lazy;
                }
            }

            void SetChain()
            {

                chain = new (int parent, int head, int dep, int num)[n + 1];
                int cnt = 1;
                chain[1].head = 1;
                DFS();

                void DFS(int _cur = 1, int _prev = 0, int _dep = 1)
                {

                    chain[_cur].num = cnt++;
                    chain[_cur].dep = _dep;

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
                }
            }

            void SetChild()
            {

                child = new int[n + 1];
                DFS();

                int DFS(int _cur = 1, int _prev = 0)
                {

                    ref int ret = ref child[_cur];
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        ret += DFS(next, _cur);

                        if (child[edge[_cur][0]] < child[next] || edge[_cur][0] == _prev)
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

                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

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
                    if (c == '\n' || c == ' ') return true;
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
// #include <iostream>
// #include <vector>
using namespace std;

typedef pair<int, int> pi;
const int M = 100001;
vector<int> link[M];

// 헤비라이트 분해
int heavy_child[M];
bool visit[M], visit2[M];

int make_tree(int a) {
	visit[a] = 1;
	int C = 0, ans = 0, tot = 1;

	for (int b : link[a]) {
		if (visit[b]) continue;

		int t = make_tree(b);
		tot += t;

		if (C < t) {
			C = t;
			ans = b;
		}
	}
	heavy_child[a] = ans;
	return tot;
}
int idx = 0, cnt = 0;
vector<int> segtree[M];
pi pos[M], parent_pos[M];

void make_segtree(int a) {
	pos[a] = { idx, cnt };
	cnt++;
	visit2[a] = 1;

	if (heavy_child[a] != 0) {
		make_segtree(heavy_child[a]);

		for (int b : link[a]) {
			if (!visit2[b]) {
				parent_pos[idx] = pos[a];
				make_segtree(b);
			}
		}
	}
	else {
		int s = 1;
		while (s < cnt) s *= 2;
		s *= 2;

		segtree[idx].resize(s);
		idx++; cnt = 0;
	}
}

// 세그먼트 트리
void add(int k, int a, int w) {
	int s = segtree[k].size();
	a += s / 2;

	for (; a >= 1; a /= 2)
		segtree[k][a] += w;
}
int sum(int k, int a) {
	int s = segtree[k].size();
	a += s / 2; int b = s - 1;
	int ret = 0;

	while (a <= b) {
		if (a & 1) ret += segtree[k][a++];
		if (!(b & 1)) ret += segtree[k][b--];

		a /= 2; b /= 2;
	}
	return ret;
}

int main()
{
	cin.tie(0);
	ios_base::sync_with_stdio(0);

	int N, M; cin >> N >> M;

	for (int i = 1; i < N; i++) {
		int u, v; cin >> u >> v;
		link[u].push_back(v);
		link[v].push_back(u);
	}
	// 헤비라이트 분해
	make_tree(1);
	make_segtree(1);

	// 쿼리
	while (M--) {
		char w; int u, v;
		cin >> w >> u >> v;

		int x = pos[u].first, y = pos[v].first;
		int a = pos[u].second, b = pos[v].second;

		if (w == 'P') {
			while (x != y) {
				if (x < y) {
					add(y, b, 1);
					b = parent_pos[y].second;
					y = parent_pos[y].first;
				}
				else {
					add(x, a, 1);
					a = parent_pos[x].second;
					x = parent_pos[x].first;
				}
			}
			if (a != b) {
				if (a > b) { int t = a; a = b; b = t; }
				add(x, b, 1);
				add(x, a, -1);
			}
		}
		else {
			if (x < y)
				cout << segtree[y][1];
			else if (x > y)
				cout << segtree[x][1];
			else
				cout << sum(x, max(a, b));
			cout << '\n';
		}
	}
	return 0;
}
#endif
}
