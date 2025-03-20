using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 7
이름 : 배성훈
내용 : 대역폭 관리
    문제번호 : 31234번

    HLD 문제다.
    세그먼트 트리의 리프 값을 남은 사용량으로 하고,
    부모로 가면 최솟값을 남기면서 해당 구간에 남은 최소량을 기록했다.
    에너지를 추가하면 허용량을 빼준다.

    이렇게 진행하면서 루트가 음의 값이 되는 경우
    리프의 어느 노드 하나가 허용량이 초과 되었다는 의미와 같다.
    이렇게 허용량을 관리했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1260
    {

        static void Main1260(string[] args)
        {

            int INF = 1_000_000_001;
            StreamReader sr;

            int S, E;
            int n, m;
            List<int>[] edge;
            int[] use;
            (int val, int lazy)[] seg;
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

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx].val = _val;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (_chk <= mid) Update(_s, mid, _chk, _val, _idx * 2 + 1);
                else Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);

                seg[_idx].val = Math.Min(seg[_idx * 2 + 1].val, seg[_idx * 2 + 2].val);
            }

            void UpdateLazy(int _s, int _e, int _chkS, int _chkE, int _val, int _idx = 0)
            {

                AdjLazy();

                if (_chkE < _s || _e < _chkS) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].lazy = _val;
                    AdjLazy();

                    return;
                }

                int mid = (_s + _e) >> 1;
                UpdateLazy(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1);
                UpdateLazy(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);

                seg[_idx].val = Math.Min(seg[_idx * 2 + 1].val, seg[_idx * 2 + 2].val);

                void AdjLazy()
                {

                    int lazy = seg[_idx].lazy;
                    if (lazy == 0) return;
                    seg[_idx].lazy = 0;

                    seg[_idx].val += lazy;
                    if (_s == _e) return;
                    seg[_idx * 2 + 1].lazy += lazy;
                    seg[_idx * 2 + 2].lazy += lazy;
                }
            }

            void GetRet()
            {

                int f, t, w;
                int ret = 0;
                while (m-- > 0)
                {

                    f = ReadInt();
                    t = ReadInt();
                    w = -ReadInt();

                    if (chain[f].dep > chain[t].dep) Swap();

                    while (chain[f].dep < chain[t].dep)
                    {

                        UpdateLazy(S, E, chain[chain[t].head].idx, chain[t].idx, w);
                        t = chain[t].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        UpdateLazy(S, E, chain[chain[f].head].idx, chain[f].idx, w);
                        UpdateLazy(S, E, chain[chain[t].head].idx, chain[t].idx, w);

                        f = chain[f].parent;
                        t = chain[t].parent;
                    }

                    if (chain[f].idx > chain[t].idx) Swap();

                    UpdateLazy(S, E, chain[f].idx, chain[t].idx, w);

                    if (seg[0].val < 0) break;
                    ret++;
                }

                Console.Write(ret);
                sr.Close();

                void Swap()
                {

                    int temp = f;
                    f = t;
                    t = temp;
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

                    Update(S, E, chain[_cur].idx, use[_cur]);
                }
            }

            void SetSeg()
            {

                int log = 1 + (int)Math.Ceiling(Math.Log2(n) + 1e-9);
                seg = new (int val, int lazy)[1 << log];
                Array.Fill(seg, (INF, 0));

                S = 1;
                E = n;
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

                        if (edge[_cur][0] == _prev || weight[edge[_cur][0]] < weight[next])
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

                use = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    use[i] = ReadInt();
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
// #include <bits/stdc++.h>

// #define endl "\n"
// #define NM 101010
// #define MAX 5555
// #define BIAS 1048576
// #define MOD 1000000007L
// #define X first
// #define Y second
// #define INF 0x3f3f3f3f
// #define FOR(i) for(int _=0;_<(i);_++)
// #define pii pair<int, int>
// #define pll pair<ll, ll>
// #define all(v) v.begin(), v.end()
// #define fastio ios_base::sync_with_stdio(false); cin.tie(NULL); cout.tie(NULL)
using namespace std;
typedef long long ll;
typedef unsigned long long ull;
using namespace std;

int n, m, depth[NM], parent[NM][19], visited[NM], limit[NM];
ll psum[NM] = {0, };
vector<int> tree[NM];
tuple<int, int, ll> query[NM];
int lca[NM];

void get_sum(int cur, int prev){
    for (int nxt: tree[cur]){
        if (nxt == prev) continue;
        get_sum(nxt, cur);
        psum[cur] += psum[nxt];
    }
}

bool ok(int x){ // 1 ~ x번까지 처리
    memset(psum, 0, sizeof psum);
    for (int i = 1; i <= x; i++){
        int flag = 0;
        auto&[a, b, c] = query[i];
        int LCA = lca[i];
        psum[a] += c;
        psum[b] += c;
        psum[LCA] -= c;
        psum[parent[LCA][0]] -= c;
    }
    get_sum(1, 0);
    for (int j = 1; j <= n; j++){
        if (psum[j] > limit[j]) return false;
    }
    return true;
}

void dfs(int cur){
    visited[cur] = 1;
    for (int nxt : tree[cur]){
        if (visited[nxt]) continue;
        depth[nxt] = depth[cur] + 1;
        parent[nxt][0] = cur;
        dfs(nxt);
    }
}

int get_lca(int a, int b){
    if (depth[a] > depth[b]) swap(a, b);
    int diff = depth[b] - depth[a];
    for (int j = 0;diff ; j++){
        if (diff&1) b = parent[b][j];
        diff >>= 1;
    }
    if (a == b) return a;
    for (int i = 18; i >= 0; i--){
        if (parent[a][i] ^ parent[b][i]){
            a = parent[a][i];
            b = parent[b][i];
        }
    }
    return parent[a][0];
}

void input() {
    cin >> n >> m;
    for (int i = 0 ; i < n - 1; i++){
        int a, b;
        cin >> a >> b;
        tree[a].push_back(b);
        tree[b].push_back(a);
    }
    for (int i = 1; i <= n; i++) cin >> limit[i];
    for (int i = 1; i <= m; i++){
        int a, b; ll c;
        cin >> a >> b >> c;
        query[i] = {a, b, c};
    }
}

void pro() {
    dfs(1);
    for (int j = 1; j < 19; j++){
        for (int i = 1;  i <= n;i++){
            parent[i][j] = parent[parent[i][j - 1]][j - 1];
        }
    }
    for (int i = 1; i <= m; i++){
        auto&[a, b, c] = query[i];
        lca[i] = get_lca(a, b);
    }
    int ans = 0;
    int left = 0;
    int right = m;
    while(left <= right){
        int mid = left + right >> 1;
        if (ok(mid)){
            ans = mid;
            left = mid + 1;
        }else{
            right = mid - 1;
        }
    }
    cout << ans << endl;
}

int main() {
    fastio;
    input();
    pro();
    return 0;
}
#endif
}
