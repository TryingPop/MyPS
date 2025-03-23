using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 23
이름 : 배성훈
내용 : Megalopolis
    문제번호 : 8145번

    HLD 문제다.
    경로의 상태를 시골길을 1, 고속도로를 0으로 했다.
    시골길을 카운팅하므로 누적합 세그먼트 트리를 만들었다.
    범위의 값을 0으로 만드는 연산을 해야 하기에, 레이지 세그먼트 트리를 써야 한다.

    그런데 HLD 의 성질과 문제 조건에서 2번의 수정은 없기에 
    값을 탐색할 때 범위를 0으로 갱신해버리고
    자식 탐색을 안하게 끊으면서 진행하니 세그먼트 트리로도 해결이 가능했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1457
    {

        static void Main1457(string[] args)
        {

            // 8145
            int S, E;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            List<int>[] edge;
            (int parent, int head, int dep, int num)[] chain;
            int[] child, seg;

            Input();

            SetSeg();

            SetChild();

            SetChain();

            GetRet();

            void GetRet()
            {

                int OP1 = 'A' - '0';
                int OP2 = 'W' - '0';

                int m = ReadInt();

                while (m > 0)
                {

                    int op = ReadInt();

                    if (op == OP1)
                    {

                        int f = ReadInt();
                        int t = ReadInt();

                        Query1(f, t);
                    }
                    else
                    {

                        int f = ReadInt();
                        m--;
                        Query2(f);
                    }
                }

                void Query1(int _f, int _t)
                {

                    if (chain[_f].dep > chain[_t].dep)
                    {

                        int temp = _f;
                        _f = _t;
                        _t = temp;
                    }

                    while (chain[_f].dep < chain[_t].dep)
                    {

                        Update(S, E, chain[chain[_t].head].num, chain[_t].num);
                        _t = chain[_t].parent;
                    }

                    while (chain[_f].head != chain[_t].head)
                    {

                        Update(S, E, chain[chain[_f].head].num, chain[_f].num);
                        Update(S, E, chain[chain[_t].head].num, chain[_t].num);

                        _f = chain[_f].parent;
                        _t = chain[_t].parent;
                    }

                    if (chain[_f].num > chain[_t].num)
                    {

                        int temp = _f;
                        _f = _t;
                        _t = temp;
                    }

                    Update(S, E, chain[_f].num + 1, chain[_t].num);
                }

                void Query2(int _idx)
                {

                    int ret = 0;

                    while (_idx > 1)
                    {

                        ret += GetVal(S, E, chain[chain[_idx].head].num, chain[_idx].num);
                        _idx = chain[_idx].parent;
                    }

                    sw.Write($"{ret}\n");
                }
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s || seg[_idx] == 0) return 0;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
            }

            void Update(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx] = 0;
                    return;
                }

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);

                seg[_idx] = seg[_idx * 2 + 1] + seg[_idx * 2 + 2];
            }

            void SetSeg()
            {

                int log = n == 1 ? 0 : (int)(Math.Log2(n - 1) + 1e-9) + 2;
                seg = new int[1 << log];

                S = 1;
                E = n;

                for (int i = 2; i <= n; i++)
                {

                    Init(S, E, i);
                }

                void Init(int _s, int _e, int _chk, int _idx = 0)
                {

                    if (_s == _e) 
                    { 
                        
                        seg[_idx] = 1;
                        return;
                    }

                    int mid = (_s + _e) >> 1;

                    if (_chk <= mid) Init(_s, mid, _chk, _idx * 2 + 1);
                    else Init(mid + 1, _e, _chk, _idx * 2 + 2);

                    seg[_idx] = seg[_idx * 2 + 1] + seg[_idx * 2 + 2];
                }
            }

            void SetChain()
            {

                chain = new (int parent, int head, int dep, int num)[n + 1];
                int cnt = 1;
                chain[1].head = 1;
                DFS();

                void DFS(int _cur = 1, int _prev = 0, int _depth = 1)
                {

                    chain[_cur].num = cnt++;
                    chain[_cur].dep = _depth;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (_prev == next) continue;

                        if (i == 0)
                        {

                            chain[next].head = chain[_cur].head;
                            chain[next].parent = chain[_cur].parent;

                            DFS(next, _cur, _depth);
                        }
                        else
                        {

                            chain[next].head = next;
                            chain[next].parent = _cur;

                            DFS(next, _cur, _depth + 1);
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

                        if (child[edge[_cur][0]] < child[next]
                            || edge[_cur][0] == _prev)
                        {

                            int temp = edge[_cur][0];
                            edge[_cur][0] = edge[_cur][i];
                            edge[_cur][i] = temp;
                        }
                    }

                    return ret;
                }
            }

            void Input()
            {

                n = ReadInt();
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
                    if (c == ' ' || c == '\n') return true;
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
// #include <iostream>
// #include <vector>
// #include <algorithm>

const int LEN = 250'001;

char cmd;
int N, M;
std::vector<int> g[LEN];
int s[LEN], e[LEN], t[LEN], l[LEN];
int order = 0;
void dfs(int u) {
	s[u] = ++order;
	for (const int& v : g[u]) {
		if (!s[v]) {
			l[v] = l[u] + 1;
			dfs(v);
		}
	}
	e[u] = order;
}
int sum(int i) {
	int ret = 0;
	while (i > 0) {
		ret += t[i];
		i -= i & -i;
	}
	return ret;
}
void update(int i, int d) {
	while (i <= N) {
		t[i] += d;
		i += i & -i;
	}
}

int main() {
	std::cin.tie(0)->sync_with_stdio(0);
	std::cin >> N;
	for (int i = 1, u, v; i < N; ++i) {
		std::cin >> u >> v;
		g[u].push_back(v);
	}
	dfs(1);
	std::cin >> M;
	for (int i = 1, u, v; i < N + M; ++i) {
		std::cin >> cmd;
		if (cmd == 'A') {
			std::cin >> u >> v;
			update(s[v], 1);
			update(e[v] + 1, -1);
		}
		if (cmd == 'W') {
			std::cin >> v;
			std::cout << l[v] - sum(s[v]) << '\n';
		}
	}
}
#endif
}
