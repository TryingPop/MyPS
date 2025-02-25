using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 7
이름 : 배성훈
내용 : Milk Visits
    문제번호 : 18263번

    HLD, 머지 소트 트리 문제다.
    단순 세그먼트 트리를 놓는게 아닌, 머지 소트트리를 놓는다.
    원래라면 List를 둬야 한다.
    하지만 여기서는 C# HashSet 성능이 값 찾는데
    매우 좋기에 HashSet으로 뒀다.
*/

namespace BaekJoon.etc
{
    internal class etc_1261
    {

        static void Main1261(string[] args)
        {

            StreamReader sr;
            int S, E;
            int n, m;
            List<int>[] edge;
            int[] cow;
            HashSet<int>[] seg;
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

                int f, t, c;
                while (m-- > 0)
                {

                    f = ReadInt();
                    t = ReadInt();

                    c = ReadInt();

                    bool ret = false;
                    if (chain[f].dep > chain[t].dep) Swap();

                    while (chain[f].dep < chain[t].dep)
                    {

                        ret |= ChkVal(S, E, chain[chain[t].head].idx, chain[t].idx, c);
                        t = chain[t].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        ret |= ChkVal(S, E, chain[chain[f].head].idx, chain[f].idx, c);
                        ret |= ChkVal(S, E, chain[chain[t].head].idx, chain[t].idx, c);

                        f = chain[f].parent;
                        t = chain[t].parent;
                    }

                    if (chain[f].idx > chain[t].idx) Swap();

                    ret |= ChkVal(S, E, chain[f].idx, chain[t].idx, c);
                    if (ret) sw.Write(1);
                    else sw.Write(0);
                }

                sr.Close();

                void Swap()
                {

                    int temp = f;
                    f = t;
                    t = temp;
                }
            }

            bool ChkVal(int _s, int _e, int _chkS, int _chkE, int _val, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return false;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx].Contains(_val);

                int mid = (_s + _e) >> 1;

                return ChkVal(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1)
                    || ChkVal(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                seg[_idx] ??= new(_e - _s + 1);
                seg[_idx].Add(_val);

                if (_s == _e) return;

                int mid = (_s + _e) >> 1;
                if (_chk <= mid) Update(_s, mid, _chk, _val, _idx * 2 + 1);
                else Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);
            }

            void SetChain()
            {

                chain = new (int idx, int head, int parent, int dep)[n + 1];
                chain[1].head = 1;
                int cnt = 1;
                DFS();

                for (int i = 1; i <= n; i++)
                {

                    Update(S, E, chain[i].idx, cow[i]);
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

                }
            }

            void SetSeg()
            {

                int log = (int)Math.Ceiling(Math.Log2(n) + 1e-9) + 1;
                seg = new HashSet<int>[1 << log];
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

                cow = new int[n + 1];
                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    cow[i] = ReadInt();
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
// #include<bits/stdc++.h>

using namespace std;

const int N = 1e5 + 1;

int type[N];
vector<int> edge[N];
pair<int, int> rn[N];

int v[N];
bool ans[N];
int lv;

bool isAnc(int a, int b){ // b is a's anc
	return (rn[a].first >= rn[b].first) & (rn[a].second <= rn[b].second);
}
 
void dfs(int s){

	v[s] = 1;
	rn[s].first = ++lv;

	for(auto i: edge[s])
		if(!v[i])
			dfs(i);

	rn[s].second = lv;

}


vector<pair<int, pair<int,int>>> endPoint[N]; // idx, another, cow
vector<int> cow[N];
int nxt[N];

void dfs2(int s){
	v[s] = 1;

	cow[type[s]].push_back(s);

	for(auto i: endPoint[s]){
		if(cow[i.second.second].empty())
			continue;

		int t = cow[i.second.second].back();

		if(t == s || t == i.second.first){
			ans[i.first] = true;
			continue;
		}
		if(!isAnc(i.second.first, t) || (isAnc(i.second.first, t) && !isAnc(i.second.first, nxt[t])))
			ans[i.first] = true;
	}

	for(auto i: edge[s])
		if(!v[i]){
			nxt[s] = i;
			dfs2(i);
			nxt[s] = 0;
		}

	cow[type[s]].pop_back();
	return;
}




int main(){
	cin.tie(NULL);
	ios::sync_with_stdio(false);

	int n, m;
	cin >> n >> m;

	for(int i=1; i<=n; i++)
		cin >> type[i];

	for(int i=1, a, b; i<n; i++){
		cin >> a >> b;
		edge[a].push_back(b);
		edge[b].push_back(a);
	}

	dfs(1);


	for(int i=1, a, b, c; i<=m; i++){
		cin >> a >> b >> c;
		endPoint[a].push_back({i, {b, c}});
		endPoint[b].push_back({i, {a, c}});
	}

	fill(v, v+N, 0);
	dfs2(1);

	for(int i=1; i<=m; i++)
		cout << ans[i];


	return 0;
}
#endif
}
