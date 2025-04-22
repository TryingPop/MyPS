using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 22
이름 : 배성훈
내용 : 판게아 1
    문제번호 : 10723번

    MST 문제다.
    처음에는 문제를 잘못봐서 간선과 노드가 10만개인줄 알았다.
    난이도가 골드라 접근할 수 있겠지? 하고 고민을 했고, 어떻게 효율적으로 구현이 안되었다.
    그러니 클릭해보니 10만개는 루비 문제였고, 링크/컷 트리를 이용한다고 한다.

    여기서는 노드가 2000개와 쿼리가 2000개고 시간이 20초로 넉넉해서
    MST로 접근했다. 추후에 시간나면 링크/컷 트리 부분을 확인해야 겠다.
    

    문제에서 요구하는 것은 트리에서 간선이 하나 추가되었을 때
    MST가 되는 총합의 길이를 구하는 것이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1564
    {

        static void Main1564(string[] args)
        {

            int N = 2_000;
            int M = 2_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;
            (int f, int t, int dis)[] edge = new (int f, int t, int dis)[M + 1];
            int empty;

            int[] group = new int[N], stk = new int[N];
            PriorityQueue<int, int> pq = new(N);
            int t = ReadInt();

            while (t-- > 0)
            {

                Input();

                GetRet();
            }
            
            void GetRet()
            {

                long ret = 0;
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int dis = ReadInt();

                    edge[empty] = (f, t, dis);

                    ret ^= Query();
                }

                sw.Write($"{ret}\n");

                long Query()
                {

                    pq.Clear();
                    for (int i = 0; i <= n; i++)
                    {

                        int dis = edge[i].dis;
                        pq.Enqueue(i, dis);
                    }

                    for (int i = 1; i < n; i++)
                    {

                        group[i] = i;
                    }

                    long ret = 0;
                    while (pq.Count > 0)
                    {

                        int cur = pq.Dequeue();

                        (int f, int t, int d) = edge[cur];

                        f = Find(f);
                        t = Find(t);


                        if (f == t) empty = cur;
                        else
                        {

                            if (t < f)
                            {

                                int temp = f;
                                f = t;
                                t = temp;
                            }

                            group[t] = f;
                            ret += d;
                        }
                    }

                    return ret;
                }

                int Find(int _chk)
                {

                    int len = 0;

                    while (group[_chk] != _chk)
                    {

                        stk[len++] = _chk;
                        _chk = group[_chk];
                    }

                    while (len-- > 0)
                    {

                        group[stk[len]] = _chk;
                    }

                    return _chk;
                }
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                empty = n;
                for (int i = 1; i < n; i++)
                {

                    edge[i] = (i, ReadInt(), ReadInt());
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
//reference: https://blog.naver.com/PostView.naver?blogId=jinhan814&logNo=222312875518&categoryNo=52&proxyReferer=

// #include <bits/stdc++.h>
// #define fastio ios::sync_with_stdio(0), cin.tie(0), cout.tie(0)
using namespace std;

using ll = long long;
using pii = pair<int, int>;

struct LinkCutTree {
    struct Node {
        //member variable
        Node* l, * r, * p;
        int v, idx;
        pii mx;
        bool flip;
        //constructor
        Node(int v = 0, int idx = 0, Node* p = nullptr) : v(v), idx(idx), p(p) {
            l = r = nullptr, flip = 0;
            mx = { v, idx };
        }
        //basic method
        bool IsRoot() const { return !p || this != p->l && this != p->r; }
        bool IsLeft() const { return this == p->l; }
        void Rotate() {
            if (IsLeft()) r && (r->p = p), p->l = r, r = p;
            else l && (l->p = p), p->r = l, l = p;
            if (!p->IsRoot()) (p->IsLeft() ? p->p->l : p->p->r) = this;
            auto t = p; p = t->p; t->p = this;
            t->Update(); Update();
        }
        void Update() {
            mx = { v, idx };
            if (l) mx = max(mx, l->mx);
            if (r) mx = max(mx, r->mx);
        }
        void Push() {
            if (!flip) return;
			swap(l, r);
			if (l) l->flip ^= 1;
			if (r) r->flip ^= 1;
			flip = 0;
        }
    };
    //member variable
    vector<Node> ptr;
    LinkCutTree(int n) : ptr(n) {}
    //basic method
    void Splay(Node* x) {
        for (; !x->IsRoot(); x->Rotate()) {
            if (!x->p->IsRoot()) x->p->p->Push(); x->p->Push(); x->Push();
            if (!x->p->IsRoot()) (x->IsLeft() ^ x->p->IsLeft() ? x : x->p)->Rotate();
        }
        x->Push();
    }
    void Access(Node* x) {
        Splay(x); x->r = nullptr;
        for (; x->p; Splay(x)) Splay(x->p), x->p->r = x;
    }
    void Link(Node* x, Node* p) {
        Access(x); Access(p);
        x->l = p; p->p = x;
        x->Update();
    }
    void Cut(Node* x) {
        Access(x); if (!x->l) return;
        x->l = x->l->p = nullptr;
        x->Update();
    }
    Node* GetPar(Node* x) {
        Access(x); if (!x->l) return nullptr;
        x = x->l; x->Push();
        for (; x->r; x = x->r, x->Push());
        Access(x); return x;
    }
    Node* GetLCA(Node* x, Node* y) {
        Access(x); Access(y); Splay(x);
        return x->p ? x->p : x;
    }
    void MakeRoot(Node* x) {
        Access(x); Splay(x);
        x->flip ^= 1;
    }
    //additional method
    void Access(int a)         { Access(&ptr[a]); }
    void Link(int a, int b)    { Link(&ptr[a], &ptr[b]); }
    void Cut(int a)            { Cut(&ptr[a]); }
    Node* GetPar(int a)        { return GetPar(&ptr[a]); }
    Node* GetLCA(int a, int b) { return GetLCA(&ptr[a], &ptr[b]); }
    void MakeRoot(int a)       { MakeRoot(&ptr[a]); }

    void Set(int i, int v, int idx) {
        ptr[i] = Node(v, idx);
    }
    pii GetMX(int a, int b) {
        auto l = GetLCA(a, b);
        pii ret{ l->v, l->idx };
        Access(a); Splay(l);
        if (l->r) ret = max(ret, l->r->mx);
        Access(b); Splay(l);
        if (l->r) ret = max(ret, l->r->mx);
        return ret;
    }
    void CutEdge(int a, int b) {
        if (GetPar(b) == &ptr[a]) swap(a, b);
        Cut(a);
    }
};

int main() {
    fastio;
    int N; cin >> N;
    while (N--) {
        //input
        int n, q, cnt = 0; cin >> n >> q;
        ll ans = 0, tot = 0;
        vector<vector<pii>> adj(n + 1);
        for (int cur = 2; cur <= n; cur++) {
            int nxt, cost; cin >> nxt >> cost; ++nxt;
            adj[cur].push_back({ nxt, cost });
            adj[nxt].push_back({ cur, cost });
            tot += cost;
        }

        //tree construction
        LinkCutTree tree(2 * n + q);
        vector<pii> edge(n + q);
        function<void(int, int)> dfs = [&](int cur, int prev) -> void {
            for (const auto& [nxt, cost] : adj[cur]) {
                if (nxt == prev) continue;
                dfs(nxt, cur); ++cnt;
                tree.Set(n + cnt, cost, cnt);
                tree.Link(nxt, n + cnt);
                tree.Link(n + cnt, cur);
                edge[cnt] = { cur, nxt };
            }
        };
        dfs(1, -1);

        //query
        auto query = [&](int a, int b, int cost) {
            auto [mx, idx] = tree.GetMX(a, b);
            if (mx <= cost || !idx) return;
            tot += cost - mx; ++cnt;
            auto [t1, t2] = edge[idx];
            tree.CutEdge(t1, n + idx);
            tree.CutEdge(t2, n + idx);
            tree.Set(n + cnt, cost, cnt);
            tree.MakeRoot(a);
            tree.Link(a, n + cnt);
            tree.Link(n + cnt, b);
            edge[cnt] = { a, b };
        };
        while (q--) {
            int a, b, cost; cin >> a >> b >> cost; ++a, ++b;
            query(a, b, cost);
            ans ^= tot;
        }
        cout << ans << '\n';
    }
}
#endif
}
