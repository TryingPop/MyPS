using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 7
이름 : 배성훈
내용 : Tree
    문제번호 : 13038번

    HLD 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1262
    {

        static void Main1262(string[] args)
        {

            StreamReader sr;

            int n, bias;
            List<int>[] edge;
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

            int GetVal(int _l, int _r)
            {

                // 노드 1개인 경우에
                // 2, 1이 들어오고 bias = 2이다.
                // _l |= bias로 _l = 2가 되고, _r |= bias로 _r = 3이 된다.
                // 그래서 1이 출력되는 이상한 경우가 발생한다.
                if (_r < _l) return 0;
                _l |= bias;
                _r |= bias;

                int ret = 0;

                while (_l <= _r)
                {

                    if ((_l & 1) == 1) ret += seg[_l++];
                    if (((~_r) & 1) == 1) ret += seg[_r--];

                    _l >>= 1;
                    _r >>= 1;
                }

                return ret;
            }

            void Update(int _chk, int _val = -1)
            {

                int idx = chain[_chk].idx | bias;
                seg[idx] += _val;

                for (; idx > 1; idx >>= 1)
                {

                    seg[idx >> 1] = seg[idx] + seg[idx ^ 1];
                }
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int m = ReadInt();

                while (m-- > 0) 
                {

                    int op = ReadInt();
                    if (op == 1)
                    {

                        int f = ReadInt();
                        int t = ReadInt();

                        int ret = Query(f, t);
                        sw.Write($"{ret}\n");
                    }
                    else
                    {

                        int v = ReadInt();
                        Update(v);
                    }
                }

                int Query(int _f, int _t)
                {

                    int f = _f;
                    int t = _t;

                    int ret = 0;

                    if (chain[f].dep > chain[t].dep) Swap();

                    while (chain[f].dep < chain[t].dep)
                    {

                        ret += GetVal(chain[chain[t].head].idx, chain[t].idx);
                        t = chain[t].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        ret += GetVal(chain[chain[f].head].idx, chain[f].idx);
                        ret += GetVal(chain[chain[t].head].idx, chain[t].idx);

                        f = chain[f].parent;
                        t = chain[t].parent;
                    }

                    if (chain[f].idx > chain[t].idx) Swap();

                    ret += GetVal(chain[f].idx + 1, chain[t].idx);
                    return ret;

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
                for (int i = bias - 1; i > 0; i--)
                {

                    seg[i] = seg[i << 1] + seg[(i << 1) | 1];
                }

                void DFS(int _cur = 1, int _dep = 1)
                {

                    chain[_cur].dep = _dep;
                    chain[_cur].idx = cnt++;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];

                        if (i == 0)
                        {

                            chain[next].head = chain[_cur].head;
                            chain[next].parent = chain[_cur].parent;

                            DFS(next, _dep);
                        }
                        else
                        {

                            chain[next].head = next;
                            chain[next].parent = _cur;

                            DFS(next, _dep + 1);
                        }
                    }

                    seg[chain[_cur].idx | bias] = 1;
                }
            }

            void SetSeg()
            {

                int log = (int)Math.Ceiling(Math.Log2(n) + 1e-9);
                bias = 1 << log++;
                seg = new int[1 << log];
            }

            void SetWeight()
            {

                int[] weight = new int[n + 1];
                DFS();

                int DFS(int _cur = 1)
                {

                    ref int ret = ref weight[_cur];
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];

                        ret += DFS(next);

                        if (weight[edge[_cur][0]] < weight[next])
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
using namespace std;
constexpr int SZ = 1 << 17;

int N, Q, D[SZ], P[SZ], Top[SZ], S[SZ], In[SZ], T[SZ<<1];
vector<int> G[SZ];

void Update(int x, int v){ for(T[x|=SZ]=v; x>>=1; ) T[x] = T[x<<1] + T[x<<1|1]; }
int Query(int l, int r){
    int res = 0;
    for(l|=SZ, r|=SZ; l<=r; l>>=1, r>>=1){
        if(l & 1) res += T[l++];
        if(~r & 1) res += T[r--];
    }
    return res;
}

void DFS1(int v){
    S[v] = 1;
    for(auto &i : G[v]){
        D[i] = D[v] + 1; DFS1(i); S[v] += S[i];
        if(S[i] > S[G[v][0]]) swap(i, G[v][0]);
    }
}

void DFS2(int v){
    static int pv = 0; In[v] = ++pv;
    for(auto i : G[v]) Top[i] = i == G[v][0] ? Top[v] : i, DFS2(i);
}

int PathQuery(int u, int v){
    int res = 0;
    for(; Top[u] != Top[v]; u=P[Top[u]]){
        if(D[Top[u]] < D[Top[v]]) swap(u, v);
        res += Query(In[Top[u]], In[u]);
    }
    if(In[u] > In[v]) swap(u, v);
    res += Query(In[u]+1, In[v]);
    return res;
}

int main(){
    ios_base::sync_with_stdio(false); cin.tie(nullptr);
    cin >> N;
    for(int i=2; i<=N; i++) cin >> P[i], G[P[i]].push_back(i);
    DFS1(1); DFS2(Top[1]=1);
    for(int i=1; i<=N; i++) Update(In[i], 1);
    cin >> Q;
    for(int q=1; q<=Q; q++){
        int op; cin >> op;
        if(op == 1){ int l, r; cin >> l >> r; cout << PathQuery(l, r) << "\n"; }
        else{ int v; cin >> v; Update(In[v], 0); }
    }
}
#endif
}
