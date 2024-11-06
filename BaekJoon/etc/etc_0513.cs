using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. -
이름 : 배성훈
내용 : 영어와 프랑스어 (Small, Large)
    문제번호 : 12143번, 12144번

    /////
    당장은 아이디어가 안떠올라 패스다
    영어가 포함되는 경우가 있으면 해당 말의 모든 단어를 영어로 봐야한다
    마찬가지로 프랑스어로 포함되는 경우가 있으면 해당 말의 모든 단어를 프랑스어로 봐야한다

    그리고 이전의 말도 다시 확인해야한다
    그렇게 해서 중복이 최소가 되게 세팅해야한다

    그리고 처음 단어는 1000개가된다
    해시셋으로 설정해 단어 확인을... 할까싶다
    일단 시간 나면 나중에 풀어봐야겠다;
    Small, Large 두 문제를 해결하려면 먼저 말들을 그룹화해서, 엮고, dp를 써야하지 않을까 싶다

    찾아보니 최대 유량 알고리즘을 써야한다
    /////

    문자열, 해시, 최대 유량 최소 컷 문제다
    아이디어는 다음과 같다
    모르는 문자들을 프랑스어와 영어로 나눠야 한다
    두 그룹을 나눠서 영어, 프랑스어로 구분 짓고
    단어들을 노드로 영어 -> 프랑스로 둘이 겹치는 언어간 간선을 놓는다
    그리고 최대유량을 흐르게 하면 최대 유량 최소 컷 정리에 의해
    공통인 최소 단어의 개수가 된다

    입력이 문자열 형태로 주어지므로 
    딕셔너리 자료구조를 이용해 문자열을 숫자로 바꿨다

    문자열 입력을 줄간격으로 많이 입력받기에 
    매번 줄로 읽고 Split 하기에는 메모리를 많이 먹을거 같아 직접 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0513
    {

        public struct Edge
        {

            public int dst;
            public int c;
            public int f;
            public int rev;

            public void Set(int _dst, int _c, int _rev)
            {

                dst = _dst;
                c = _c;
                f = 0;
                rev = _rev;
            }

            public bool CanFlow => c - f > 0;
            public int Remain => c - f;


            public void Flow(int _flow)
            {

                f += _flow;
            }
        }

        static void Main513(string[] args)
        {

            int INF = 1_000_000;

            StreamReader sr;
            StreamWriter sw;

            Dictionary<string, int> sTi;
            StringBuilder sb;

            List<List<int>> input, edges;
            Edge[] edge;
            int eLen;

            int idx;
            int n;
            int source, sink;

            Queue<int> q;
            bool[] visit;
            int[] prev, prevEdge;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                for (int i = 1; i <= t; i++)
                {

                    Input();

                    int ret = MCMF();

                    sw.Write($"Case #{i}: {ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            int MCMF()
            {

                int ret = 0;
                while (true)
                {

                    q.Enqueue(source);
                    visit[source] = true;

                    while(q.Count > 0)
                    {

                        int cur = q.Dequeue();

                        if (cur == sink) break;

                        for (int i = 0; i < edges[cur].Count; i++)
                        {

                            int idx = edges[cur][i];
                            int next = edge[idx].dst;

                            if (visit[next] || !edge[idx].CanFlow) continue;
                            visit[next] = true;
                            prev[next] = cur;
                            prevEdge[next] = idx;

                            q.Enqueue(next);
                        }
                    }

                    q.Clear();
                    if (!visit[sink]) break;

                    int flow = INF;

                    for (int i = sink; i != source; i = prev[i])
                    {

                        flow = Math.Min(flow, edge[prevEdge[i]].Remain);
                    }

                    for (int i = sink; i != source; i = prev[i])
                    {

                        int idx1 = prevEdge[i];
                        int idx2 = edge[prevEdge[i]].rev;
                        edge[idx1].Flow(flow);
                        edge[idx2].Flow(-flow);
                    }

                    ret += flow;

                    for (int i = 0; i < idx * 2 + n; i++)
                    {

                        visit[i] = false;
                        prev[i] = -1;
                        prevEdge[i] = -1;
                    }
                }

                return ret;
            }

            void Input()
            {

                idx = 0;
                eLen = 0;
                sTi.Clear();

                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    MyReadLine(i);
                }

                for (int i = 0; i < idx * 2 + n; i++)
                {

                    if (edges.Count <= i) edges.Add(new());
                    else edges[i].Clear();

                    visit[i] = false;
                    prev[i] = -1;
                    prevEdge[i] = -1;
                }

                for (int i = 0; i < input[0].Count; i++)
                {

                    AddEdge(source, input[0][i] * 2 + n, INF);
                }

                for (int i = 0; i < input[1].Count; i++)
                {

                    AddEdge(input[1][i] * 2 + 1 + n, sink, INF);
                }

                for (int i = 2; i < n; i++)
                {

                    for (int j = 0; j < input[i].Count; j++)
                    {

                        AddEdge(input[i][j] * 2 + 1 + n, i, INF);
                        AddEdge(i, input[i][j] * 2 + n, INF);
                    }
                }

                for (int i = 0; i < idx; i++)
                {

                    AddEdge(i * 2 + n, i * 2 + 1 + n, 1);
                }
            }

            void AddEdge(int _f, int _t, int _fcap, int _tcap = 0)
            {

                int idx1 = eLen++;
                int idx2 = eLen++;

                edge[idx1].Set(_t, _fcap, idx2);
                edge[idx2].Set(_f, _tcap, idx1);

                edges[_f].Add(idx1);
                edges[_t].Add(idx2);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sb = new(20);
                sTi = new(1000);

                source = 0;
                sink = 1;

                input = new();
                edges = new();
                edge = new Edge[25_000];

                q = new();
                visit = new bool[25_000];
                prev = new int[25_000];
                prevEdge = new int[25_000];
            }

            void MyReadLine(int _idx)
            {

                if (input.Count <= _idx) input.Add(new());
                else input[_idx].Clear();

                int c;
                while((c = sr.Read()) != -1 && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == ' ')
                    {

                        if (sb.Length == 0) continue;
                        string temp = sb.ToString();
                        sb.Clear();

                        if (!sTi.ContainsKey(temp)) sTi[temp] = idx++;
                        input[_idx].Add(sTi[temp]);
                        continue;
                    }

                    sb.Append((char)c);
                }

                if (sb.Length > 0)
                {

                    string temp = sb.ToString();
                    sb.Clear();

                    if (!sTi.ContainsKey(temp)) sTi[temp] = idx++;
                    input[_idx].Add(sTi[temp]);
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != '\n')
                {

                    if (c == '\r' || c == ' ') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// # Time  : O((N * L)^2)
// # Space : O(N * L)

def dfs(node, sink, used, E):
    if used[node]:
        return False
    used[node] = True

    for i in xrange(len(E[node])):
        if E[node][i] == sink or dfs(E[node][i], sink, used, E):
            E[E[node][i]].append(node)
            E[node][-1], E[node][i] = E[node][i], E[node][-1]
            E[node].pop()
            return True
    return False

def bilingual():
    N = input()
    
    def word_id(word_ids, word):
        if word in word_ids:
            return word_ids[word]
        word_ids[word] = len(word_ids)
        return word_ids[word]

// # Parse lines.
    word_ids = {}
    lines = [list(set([word_id(word_ids, word) \
             for word in raw_input().strip().split()])) \
             for _ in xrange(N)]

// # Init edges.
// # i (0 ~ N) node represents the ith line.
// # 2 * i + N node represents the word i is in English.
// # 2 * i + N + 1 node represents the word i is in French.
    source, sink = 0, 1
    E = [[] for _ in xrange(2 * len(word_ids) + N)]
    for i in xrange(len(word_ids)):
        E[2 * i + N].append(2 * i + N + 1)
    for x in lines[0]:
        E[source].append(2 * x + N)
    for y in lines[1]:
        E[2 * y + N + 1].append(sink)
    for i in xrange(2, N):
        for x in lines[i]:
            E[2 * x + N + 1].append(i)
            E[i].append(2 * x + N)
// # Run max flow.
    flow = 0
    used = [False for _ in xrange(len(E))]
    while dfs(source, sink, used, E):
        flow += 1
        used = [False for _ in xrange(len(E))]
    return flow


for case in xrange(input()):
    print "Case #%d: %d" % (case + 1, bilingual())
#elif other2
// #include<bits/stdc++.h>
using namespace std;
typedef long long int ll;
typedef double dl;
typedef pair<dl,dl> pdi;
typedef pair<ll,ll> pii;
typedef pair<ll,pii> piii;

// #define ff first
// #define ss second
// #define eb emplace_back
// #define ep emplace
// #define pb push_back
// #define mp make_pair
// #define all(x) (x).begin(), (x).end()
// #define compress(v) sort(all(v)), v.erase(unique(all(v)), v.end())
// #define IDX(v, x) lower_bound(all(v), x) - v.begin()
//cout<<fixed;
//cout.precision(12);

struct poi{
    ll val,xx,yy;
};

template<typename ft, size_t sz, ft inf=1000000007>
struct dinic{
	struct edg{
		int v,dual;
		ft c;
	};
	int lv[sz],wrk[sz];
	vector<edg> v[sz];
	void clear(){
		for(int i=0;i<sz;i++)
			v[i].clear();
	}
	void addedg(int s,int e,ft x){
		v[s].pb({e,(int)v[e].size(),x});
		v[e].pb({s,(int)v[s].size()-1,0});
	}
	void addedg2(int s,int e,ft x){
		v[s].pb({e,(int)v[e].size(),x});
		v[e].pb({s,(int)v[s].size()-1,x});
	}
	bool bfs(int s,int t){
		for(int i=0;i<sz;i++)
			lv[i]=0;
		queue<int> q;
		q.push(s);
		lv[s]=1;

		while(q.size()){
			int x=q.front();
			q.pop();
			for(auto &k:v[x]){
				if(k.c&&!lv[k.v]){
					q.push(k.v);
					lv[k.v]=lv[x]+1;
				}
			}
		}

		return lv[t];
	}

	ft dfs(int x,int t, ft tot){
		if(x==t) return tot;
		for(int &i=wrk[x];i<v[x].size();i++){
			edg &e=v[x][i];
			if(lv[e.v]!=lv[x]+1||!e.c) continue;
			ft val=dfs(e.v,t,min(tot,e.c));
			if(!val) continue;
			e.c-=val;
			v[e.v][e.dual].c+=val;
			return val;
		}
		return 0;
	}
	ft getflow(int s,int t){
		ft ret=0,tmp;
		while(bfs(s,t)){
			//for(int i=0;i<sz;i++){
			//	cout<<i<<' '<<lv[i]<<'\n';
			//}
			memset(wrk,0,sizeof(wrk));
			while(tmp=dfs(s,t,inf)) ret+=tmp;
		}
		return ret;
	}
};

vector<string> x;
ll n,m;
ll mod=998244353;
string s[202];
string t;
ll arr[1010];
string ss;
string nul;
ll inf= 1000000007;

ll dx[4]={0,1,0,-1};
ll dy[4]={1,0,-1,0};
ll st=4501,en=4502;
dinic<int, 4503> flow;
vector<string> v[202];
int main(){
    ios_base::sync_with_stdio(0);
    cin.tie(0);
    ll t,T;
    cin>>T;
    for(t=1;t<=T;t++){
    	ll i,j,k,l,a,b,c;

    	flow.clear();
    	x.clear();

    	cin>>n;
    	getline(cin,ss);
    	for(i=1;i<=n;i++){
    		getline(cin,s[i]);
    		v[i].clear();
    	}
    	ss=nul;
    	for(i=1;i<=n;i++){
    		for(j=0;j<s[i].size();j++){
    			if(s[i][j]==' '){
    				//cout<<t<<' '<<i<<' '<<j<<' '<<ss<<'\n';
    				v[i].pb(ss);
    				x.pb(ss);
    				ss=nul;
    			}
    			else{
    				ss+=s[i][j];
    			}
    		}
    		//cout<<t<<' '<<i<<' '<<j<<' '<<ss<<'\n';
    		v[i].pb(ss);
    		x.pb(ss);
    		ss=nul;
    	}
    	compress(x);
    	ll sz=x.size();

    	for(i=n+1;i<=n+sz;i++){
    		flow.addedg(i,i+sz,1);
    	}
    	flow.addedg(st,1,inf);
    	flow.addedg(2,en,inf);

    	for(i=1;i<=n;i++){
    		for(auto k:v[i]){
    			ll idx=IDX(x,k)+1+n;
    			//cout<<i<<' '<<idx<<'\n';
    			flow.addedg(i,idx,inf);
    			flow.addedg(idx+sz,i,inf);

    		}
    	}

    	auto ans=flow.getflow(st,en);
    	cout<<"Case #"<<t<<": "<<ans<<'\n';
    }
}

#elif other3
// #include <algorithm>
// #include <array>
// #include <bitset>
// #include <cassert>
// #include <chrono>
// #include <cstring>
// #include <iomanip>
// #include <iostream>
// #include <map>
// #include <numeric>
// #include <queue>
// #include <random>
// #include <set>
// #include <stack>
// #include <vector>

using namespace std;

// BEGIN NO SAD
// #define rep(i, a, b) for(int i = a; i < (b); ++i)
// #define trav(a, x) for(auto& a : x)
// #define all(x) x.begin(), x.end()
// #define sz(x) (int)(x).size()
// #define mp make_pair
// #define pb push_back
// #define eb emplace_back
// #define lb lower_bound
// #define ub upper_bound
// typedef vector<int> vi;
// #define f first
// #define s second
// #define derr if(0) cerr
// END NO SAD

typedef long long ll;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;
typedef vector<vector<ll>> matrix;

typedef int LL;

struct Edge {
  int u, v;
  LL cap, flow;
  Edge() {}
  Edge(int u, int v, LL cap): u(u), v(v), cap(cap), flow(0) {}
};

struct Dinic {
  int N;
  vector<Edge> E;
  vector<vector<int>> g;
  vector<int> d, pt;

  Dinic(int N): N(N), E(0), g(N), d(N), pt(N) {}

  void AddEdge(int u, int v, LL cap) {
    if (u != v) {
      E.emplace_back(u, v, cap);
      g[u].emplace_back(E.size() - 1);
      E.emplace_back(v, u, 0);
      g[v].emplace_back(E.size() - 1);
    }
  }

  bool BFS(int S, int T) {
    queue<int> q({S});
    fill(d.begin(), d.end(), N + 1);
    d[S] = 0;
    while(!q.empty()) {
      int u = q.front(); q.pop();
      if (u == T) break;
      for (int k: g[u]) {
        Edge &e = E[k];
        if (e.flow < e.cap && d[e.v] > d[e.u] + 1) {
          d[e.v] = d[e.u] + 1;
          q.emplace(e.v);
        }
      }
    }
    return d[T] != N + 1;
  }

  LL DFS(int u, int T, LL flow = -1) {
    if (u == T || flow == 0) return flow;
    for (int &i = pt[u]; i < g[u].size(); ++i) {
      Edge &e = E[g[u][i]];
      Edge &oe = E[g[u][i]^1];
      if (d[e.v] == d[e.u] + 1) {
        LL amt = e.cap - e.flow;
        if (flow != -1 && amt > flow) amt = flow;
        if (LL pushed = DFS(e.v, T, amt)) {
          e.flow += pushed;
          oe.flow -= pushed;
          return pushed;
        }
      }
    }
    return 0;
  }

  LL MaxFlow(int S, int T) {
    LL total = 0;
    while (BFS(S, T)) {
      fill(pt.begin(), pt.end(), 0);
      while (LL flow = DFS(S, T))
        total += flow;
    }
    return total;
  }
};

Dinic* dinic;
vector<string> words[205];
void rsolve() {
  delete dinic;
  int n;
  cin >> n;
  cin.ignore();
  for(int i = 0; i < n; i++) {
    string s;
    getline(cin, s);
    string t = "";
    words[i].clear();
    while(sz(s)) {
      if(s.back() == ' ') {
        words[i].pb(t);
        t = "";
        s.pop_back();
      }
      else {
        t += s.back();
        s.pop_back();
      }
    }
    words[i].pb(t);
  }
  vector<string> allv;
  for(int i = 0; i < n; i++) {
    for(auto out: words[i]) allv.pb(out);
  }
  sort(all(allv));
  allv.resize(unique(all(allv)) - allv.begin());
  dinic = new Dinic(n + 2*sz(allv));
  // 0 to n-1 are sentences, n onward are words
  for(int i = 0; i < n; i++) {
    sort(all(words[i]));
    words[i].resize(unique(all(words[i])) - words[i].begin());
    for(auto out: words[i]) {
      int idx = lb(all(allv), out) - allv.begin();
      idx += n;
      dinic->AddEdge(i, idx, 1e9);
      dinic->AddEdge(idx+sz(allv), i, 1e9);
    }
  }
  for(int i = 0; i < sz(allv); i++) {
    dinic->AddEdge(n+i, n+i+sz(allv), 1);
  }
  cout << dinic->MaxFlow(0, 1) << "\n";
}

void solve() {
  int t;
  cin >> t;
  for(int cc = 1; cc <= t; cc++) {
    cout << "Case #" << cc << ": ";
    rsolve();
  }
}

int main() {
  ios_base::sync_with_stdio(false);
  cin.tie(NULL); cout.tie(NULL);
  solve();
}

#endif
}
