using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 5
이름 : 배성훈
내용 : 트리
    문제번호 : 13309번

    HLD 문제다.
    해당 세그먼트 트리 끊어진걸 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1250
    {

        static void Main1250(string[] args)
        {

            StreamReader sr;

            int n, m, bias;

            bool[] seg;
            List<int>[] edge;
            (int head, int parent, int dep, int num)[] chain;


            Solve();
            void Solve()
            {

                Input();

                SetChild();

                SetSeg();

                SetChain();

                GetRet();
            }

            void Update(int _chk)
            {

                int idx = bias | _chk;
                seg[idx] = true;
                for (; idx > 1; idx >>= 1)
                {

                    seg[idx >> 1] = seg[idx] | seg[idx ^ 1];
                }
            }

            bool GetVal(int _l, int _r)
            {

                _l |= bias;
                _r |= bias;

                bool ret = false;

                while (_l <= _r)
                {

                    if ((_l & 1) == 1)
                    {

                        ret |= seg[_l];
                        _l++;
                    }

                    if (((~_r) & 1) == 1)
                    {

                        ret |= seg[_r];
                        _r--;
                    }

                    _l >>= 1;
                    _r >>= 1;
                }

                return ret;
            }

            void GetRet()
            {

                string YES = "YES\n";
                string NO = "NO\n";
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                while (m-- > 0)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    bool ret = Query(f, t);
                    int d = ReadInt();

                    if (d == 1)
                    {

                        int pop;
                        if (ret) pop = chain[f].num;
                        else pop = chain[t].num;

                        Update(pop);
                    }

                    sw.Write(ret ? YES : NO);
                }

                sr.Close();

                bool Query(int _f, int _t)
                {

                    int f = _f;
                    int t = _t;

                    if (chain[f].dep > chain[t].dep)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    bool ret = false;

                    while (chain[f].dep < chain[t].dep)
                    {

                        ret |= GetVal(chain[chain[t].head].num, chain[t].num);
                        t = chain[t].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        ret |= GetVal(chain[chain[f].head].num, chain[f].num);
                        ret |= GetVal(chain[chain[t].head].num, chain[t].num);

                        f = chain[f].parent;
                        t = chain[t].parent;
                    }

                    if (chain[f].num > chain[t].num)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    ret |= GetVal(chain[f].num + 1, chain[t].num);
                    return !ret;
                }
            }

            void SetSeg()
            {

                int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9));
                seg = new bool[1 << (log + 1)];
                bias = 1 << log;
            }

            void SetChain()
            {

                chain = new (int head, int parent, int dep, int num)[n + 1];
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

                int[] child = new int[n + 1];
                DFS();

                int DFS(int _cur = 1, int _prev = 0)
                {

                    ref int ret = ref child[_cur];
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (_prev == next) continue;

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

                for (int f = 2; f <= n; f++)
                {

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
 
using namespace std;
 
typedef long long ll;
 
ll n,q,cnt=1,qy=1;
ll ind[200004],pw[200004],chd[200004],par[200004];
vector<ll> g[200004];  //원래 번호 
 
ll val(ll a)
{
	ll ret=0;
	while(a)
	{
		ret+=pw[a];
		a-=(a&-a);	
	}	
	return ret;
}
 
void upd(ll a,ll x)
{
	while(a<=n)
	{
		pw[a]+=x;
		a+=(a&-a);
	}	
}
 
void updrange(ll a,ll sz)
{
	upd(a,qy);
	upd(a+sz,-qy);
	qy+=(200004-a);
}
 
ll dfs(ll a,ll p)
{
	ll ret=1;
	ind[a]=cnt++,par[ind[a]]=ind[p];
	for(int i=0;i<g[a].size();i++)
		ret+=dfs(g[a][i],a);
	chd[ind[a]]=ret;
	return ret;
}
 
int main()
{
    ios::sync_with_stdio(false);
    cin.tie(NULL);
	cin>>n>>q;
	for(int i=2;i<=n;i++)
	{
		ll a;cin>>a;
		g[a].push_back(i);
	}    
	dfs(1,0);
	for(int i=0;i<q;i++)
	{
		ll b,c,d;cin>>b>>c>>d;
		if(d==0)
		{
			if(val(ind[b])==val(ind[c]))
				cout<<"YES"<<"\n";
			else
				cout<<"NO"<<"\n";
		}
		else
		{
			if(val(ind[b])==val(ind[c]))
			{
				cout<<"YES"<<"\n";
				ll f=ind[b];
				if(val(f)==val(par[f]))
					updrange(f,chd[f]);
			}
			else
			{
				cout<<"NO"<<"\n";
				ll f=ind[c];
				if(val(f)==val(par[f]))
					updrange(f,chd[f]);	
			}
		}
	}
    return 0;
}
#endif
}
