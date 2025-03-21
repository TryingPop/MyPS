using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 7
이름 : 배성훈
내용 : 슥삭슥삭 나무자르기
    문제번호 : 30092번

    HLD 문제다.
    LCA로도 풀 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1259
    {

        static void Main1259(string[] args)
        {

            StreamReader sr;
            int n, m;
            List<int>[] edge;
            (bool val, int lazy)[] seg;
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

            bool GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate();

                if (_e < _chkS || _chkE < _s) return false;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx].val;

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    || GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);

                void LazyUpdate()
                {

                    int lazy = seg[_idx].lazy;
                    if (lazy == 0) return;
                    seg[_idx].lazy = 0;

                    seg[_idx].val = lazy > 0;

                    if (_s == _e) return;
                    seg[_idx * 2 + 1].lazy = lazy;
                    seg[_idx * 2 + 2].lazy = lazy;
                }
            }

            void Update(int _s, int _e, int _chkS, int _chkE, bool _val, int _idx = 0)
            {

                LazyUpdate();

                if (_e < _chkS || _chkE < _s) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].lazy = _val ? 1 : -1;
                    LazyUpdate();
                    return;
                }

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);

                seg[_idx].val = seg[_idx * 2 + 1].val || seg[_idx * 2 + 2].val;

                void LazyUpdate()
                {

                    int lazy = seg[_idx].lazy;
                    if (lazy == 0) return;
                    seg[_idx].lazy = 0;

                    seg[_idx].val = lazy > 0;

                    if (_s == _e) return;
                    seg[_idx * 2 + 1].lazy = lazy;
                    seg[_idx * 2 + 2].lazy = lazy;
                }
            }

            void GetRet()
            {

                int S = 1;
                int E = n;
                string YES = "YES\n";
                string NO = "NO\n";

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                while (m-- > 0)
                {

                    int a = ReadInt();
                    int b = ReadInt();
                    int c = ReadInt();
                    int d = ReadInt();

                    UpdateQuery(a, b, true);

                    if (GetValQuery(c, d)) sw.Write(YES);
                    else sw.Write(NO);

                    UpdateQuery(a, b, false);
                }

                sr.Close();

                void UpdateQuery(int _f, int _t, bool _val)
                {

                    int f = _f;
                    int t = _t;

                    if (chain[f].dep > chain[t].dep) Swap();

                    while (chain[f].dep < chain[t].dep)
                    {

                        Update(S, E, chain[chain[t].head].idx, chain[t].idx, _val);
                        t = chain[t].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        Update(S, E, chain[chain[f].head].idx, chain[f].idx, _val);
                        Update(S, E, chain[chain[t].head].idx, chain[t].idx, _val);

                        f = chain[f].parent;
                        t = chain[t].parent;
                    }

                    if (chain[f].idx > chain[t].idx) Swap();

                    Update(S, E, chain[f].idx + 1, chain[t].idx, _val);

                    void Swap()
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }
                }

                bool GetValQuery(int _f, int _t)
                {

                    bool disconnect = false;

                    int f = _f;
                    int t = _t;

                    if (chain[f].dep > chain[t].dep) Swap();

                    while (chain[f].dep < chain[t].dep)
                    {

                        disconnect |= GetVal(S, E, chain[chain[t].head].idx, chain[t].idx);
                        t = chain[t].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        disconnect |= GetVal(S, E, chain[chain[f].head].idx, chain[f].idx);
                        disconnect |= GetVal(S, E, chain[chain[t].head].idx, chain[t].idx);

                        f = chain[f].parent;
                        t = chain[t].parent;
                    }

                    if (chain[f].idx > chain[t].idx) Swap();

                    disconnect |= GetVal(S, E, chain[f].idx + 1, chain[t].idx);
                    return !disconnect;

                    void Swap()
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }
                }
            }

            void SetChain()
            {

                chain = new (int idx, int head, int parent, int dep)[n + 1];
                chain[1].head = 1;
                int cnt = 1;
                DFS();

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
                }
            }

            void SetSeg()
            {

                int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9)) + 1;
                seg = new (bool val, int lazy)[1 << log];
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
// #include <algorithm>
// #include <vector>
using namespace std;
typedef long long ll;
typedef pair<int,int> pii;
vector<int> g[101010];

int in[101010];
int num = 0;
pii tbl[202020][18];
int dep[101010];

void dfs(int x, int par, int depth) {
    dep[x] = depth;
    tbl[in[x] = num++][0] = {depth, x};
    for(auto e : g[x]) {
        if (e == par) continue;
        dfs(e, x, depth+1);
        tbl[num++][0] = {depth, x};
    }
}

int lca(int a, int b) {
    int ai = in[a];
    int bi = in[b];
    if (ai > bi)swap(ai, bi);
    int sz = 31-__builtin_clz(bi-ai+1);
    return min(tbl[ai][sz], tbl[bi-(1<<sz)+1][sz]).second;
}

int main(){
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n, Q;
    cin >> n >> Q;
    for(int i=0;i<n-1;i++) {
        int a, b;
        cin >> a >> b;
        g[a].push_back(b);
        g[b].push_back(a);
    }
    dfs(1, 1, 0);

    for(int i=1;i<18;i++) {
        for(int j=0;j<num;j++) {
            if (j + (1<<i) > num)break;
            tbl[j][i] = min(tbl[j][i-1], tbl[j+(1<<(i-1))][i-1]);
        }
    }

    while(Q--) {
        int a, b, c, d;
        cin >> a >> b >> c >> d;
        int ab = lca(a, b);
        int ac = lca(a, c);
        int bc = lca(b, c);
        int ad = lca(a, d);
        int bd = lca(b, d);

        int cc = dep[ac] < dep[bc] ? bc : ac;
        int dd = dep[ad] < dep[bd] ? bd : ad;
        cc = dep[cc] > dep[ab] ? cc : ab;
        dd = dep[dd] > dep[ab] ? dd : ab;
        cout << (cc != dd ? "NO" : "YES") << '\n';
    }

}

#endif
}
