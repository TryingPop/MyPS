using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 24
이름 : 배성훈
내용 : 분단의 슬픔
    문제번호 : 13161번

    최대 유량 문제다
    에드먼드 카프 알고리즘은 시간복잡도가 O(V * E^2) 이다
    반면 디닉 알고리즘은 O(E * V^2) 이다

    다른 사람 풀이를 보고 작성했다
    https://m.blog.naver.com/kks227/220812858041

    잘 보면 dfs의 for문에서 d(work)의 주소를 받아와 실행하는데,
    이를 놓쳐 시간 초과가 났다

    이후 다시 보니, d가 왜 쓰이지 의문을 갖게 되었고 포인터 값을 주었음을 알게되었다
    이에 for문 끝날 때, d[_n]의 값을 최근에 사용한 i로 갱신해주니 496ms에 이상없이 통과했다

    아이디어는 다음과 같다
    슬픔의 합의 최소값은 A팀에서 시작해서 B팀에 유량을 흐르게 할 때의 최대 유량이 최소가 된다고 한다
    (최대 흐름 최소 절단 정리)
        https://m.blog.naver.com/jqkt15/222063980106

    아마 비슷한 문제를 풀어보면서 감각을 익혀야겠다;
*/

namespace BaekJoon._51
{
    internal class _51_05
    {

        static void Main5(string[] args)
        {

            int INF = 1_000_000_000;
            StreamReader sr;
            StreamWriter sw;

            int[,] c, f;
            List<int>[] line;

            int n;
            int source, sink;
            int[] lvl;
            int[] d;

            int ret;
            Queue<int> q;

            bool[] visit;

            Solve();

            void Solve()
            {

                Input();
                ret = 0;

                q = new(n + 2);
                lvl = new int[n + 2];
                d = new int[n + 2];
                while (BFS())
                {

                    Array.Fill(d, 0);

                    while (true)
                    {

                        int flow = DFS(source, INF);
                        if (flow == 0) break;
                        ret += flow;
                    }
                }

                SetTeam();
                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{ret}\n");

                for (int i = 1; i <= n; i++)
                {

                    if (visit[i]) sw.Write($"{i} ");
                }

                sw.Write('\n');

                for (int i = 1; i <= n; i++)
                {

                    if (visit[i]) continue;

                    sw.Write($"{i} ");
                }

                sw.Write('\n');
                sw.Close();
            }

            void SetTeam()
            {

                visit = new bool[n + 2];
                q.Enqueue(source);

                while (q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];
                        if (visit[next] || c[node, next] - f[node, next] == 0) continue;

                        visit[next] = true;
                        q.Enqueue(next);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                source = 0;
                sink = n + 1;

                c = new int[n + 2, n + 2];
                f = new int[n + 2, n + 2];

                line = new List<int>[n + 2];
                line[source] = new();
                line[sink] = new();
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new(n + 2);
                    int cur = ReadInt();

                    if (cur == 0) continue;
                    else if (cur == 1)
                    {

                        line[i].Add(source);
                        line[source].Add(i);
                        c[source, i] = INF;
                    }
                    else
                    {

                        line[i].Add(sink);
                        line[sink].Add(i);
                        c[i, sink] = INF;
                    }
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        int cur = ReadInt();
                        c[i, j] = cur;
                        if (cur == 0) continue;
                        line[i].Add(j);
                    }
                }

                sr.Close();
            }

            bool BFS()
            {

                ///
                /// source에서 sink로 
                /// 유량을 전달 할 수 있는지 확인한다
                /// 경로가 존재하지 않으면 탈출한다!
                /// 그리고 경로에 대해 단계를 부여한다
                ///
                Array.Fill(lvl, -1);
                lvl[source] = 0;

                q.Enqueue(source);
                while (q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];

                        if (lvl[next] == -1 && c[node, next] - f[node, next] > 0)
                        {

                            lvl[next] = lvl[node] + 1;
                            q.Enqueue(next);
                        }
                    }
                }

                return lvl[sink] != -1;
            }

            int DFS(int _n, int _flow)
            {

                ///
                /// BFS에서 경로의 존재성이 보장되고 찾은 경로에 최대한 유량을 흐르게 한다
                ///
                if (_n == sink) return _flow;

                for (int i = d[_n]; i < line[_n].Count; i++, d[_n] = i)
                {

                    int next = line[_n][i];
                    if (lvl[next] == lvl[_n] + 1 && c[_n, next] - f[_n, next] > 0)
                    {

                        int ret = DFS(next, Math.Min(c[_n, next] - f[_n, next], _flow));

                        if (ret > 0)
                        {

                            f[_n, next] += ret;
                            f[next, _n] -= ret;
                            return ret;
                        }
                    }
                }

                return 0;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }


#if other
// #include <bits/stdc++.h>
// #define sz(v) ((int)(v).size())
// #define all(v) (v).begin(), (v).end()
using namespace std;
typedef long long lint;
typedef pair<int, int> pi;

template<class flow_t> struct HLPP {
	struct Edge {
		int to, inv;
		flow_t rem, cap;
	};

	vector<basic_string<Edge>> G;
	vector<flow_t> extra;
	vector<int> hei, arc, prv, nxt, act, bot;
	queue<int> Q;
	int n, high, cut, work;

	// Initialize for n vertices
	HLPP(int k) : G(k) {}
	
	int addEdge(int u, int v,
	            flow_t cap, flow_t rcap = 0) {
		G[u].push_back({ v, sz(G[v]), 0, cap });
		G[v].push_back({ u, sz(G[u])-1, 0, rcap });
		return sz(G[u])-1;
	}

	void raise(int v, int h) {
		prv[nxt[prv[v]] = nxt[v]] = prv[v];
		hei[v] = h;
		if (extra[v] > 0) {
			bot[v] = act[h]; act[h] = v;
			high = max(high, h);
		}
		if (h < n) cut = max(cut, h+1);
		nxt[v] = nxt[prv[v] = h += n];
		prv[nxt[nxt[h] = v]] = v;
	}

	void global(int s, int t) {
		hei.assign(n, n*2);
		act.assign(n*2, -1);
		iota(all(prv), 0);
		iota(all(nxt), 0);
		hei[t] = high = cut = work = 0;
		hei[s] = n;
		for (int x : {t, s})
			for (Q.push(x); !Q.empty(); Q.pop()) {
				int v = Q.front();
				for(auto &e : G[v]){
					if (hei[e.to] == n*2 &&
					    G[e.to][e.inv].rem)
						Q.push(e.to), raise(e.to,hei[v]+1);
				}
			}
	}

	void push(int v, Edge& e, bool z) {
		auto f = min(extra[v], e.rem);
		if (f > 0) {
			if (z && !extra[e.to]) {
				bot[e.to] = act[hei[e.to]];
				act[hei[e.to]] = e.to;
			}
			e.rem -= f; G[e.to][e.inv].rem += f;
			extra[v] -= f; extra[e.to] += f;
		}
	}

	void discharge(int v) {
		int h = n*2, k = hei[v];

		for(int j = 0; j < sz(G[v]); j++){
			auto& e = G[v][arc[v]];
			if (e.rem) {
				if (k == hei[e.to]+1) {
					push(v, e, 1);
					if (extra[v] <= 0) return;
				} else h = min(h, hei[e.to]+1);
			}
			if (++arc[v] >= sz(G[v])) arc[v] = 0;
		}

		if (k < n && nxt[k+n] == prv[k+n]) {
			for(int j = k; j < cut; j++){
			while (nxt[j+n] < n)
				raise(nxt[j+n], n);
			}
			cut = k;
		} else raise(v, h), work++;
	}

	// Compute maximum flow from src to dst
	flow_t flow(int src, int dst) {
		extra.assign(n = sz(G), 0);
		arc.assign(n, 0);
		prv.resize(n*3);
		nxt.resize(n*3);
		bot.resize(n);
		for(auto &v : G){
			for(auto &e : v) e.rem = e.cap;
		}

		for(auto &e : G[src]){
			extra[src] = e.cap, push(src, e, 0);
		}

		global(src, dst);

		for (; high; high--)
			while (act[high] != -1) {
				int v = act[high];
				act[high] = bot[v];
				if (v != src && hei[v] == high) {
					discharge(v);
					if (work > 4*n) global(src, dst);
				}
			}

		return extra[dst];
	}

	// Get flow through e-th edge of vertex v
	flow_t getFlow(int v, int e) {
		return G[v][e].cap - G[v][e].rem;
	}

	// Get if v belongs to cut component with src
	bool cutSide(int v) { return hei[v] >= n; }
};

template <class T> struct Circulation {
    const T INF = numeric_limits<T>::max() / 2;
	T lowerBoundSum = 0;
	HLPP<T> mf;

	// Initialize for n vertices
	Circulation(int k) : mf(k + 2) {}
	void addEdge(int s, int e, T l, T r){
		lowerBoundSum += l;
		mf.addEdge(s + 2, e + 2, r - l);
		if(l > 0){
			mf.addEdge(0, e + 2, l);
			mf.addEdge(s + 2, 1, l);
		}
	}
	bool solve(int s, int e){
		mf.addEdge(e+2, s+2, INF); // to reduce as maxflow with lower bounds, in circulation problem skip this line
		return lowerBoundSum == mf.flow(0, 1);
		// to get maximum LR flow, run maxflow from s+2 to e+2 again
	}
};

int n, grp[505], adj[505][505];

int main(){
    ios::sync_with_stdio(0);
    cin.tie(0); cout.tie(0);
    cin >> n;
	for(int i=1; i<=n; i++){
		cin >> grp[i];
	}
	for(int i=1; i<=n; i++){
		for(int j=1; j<=n; j++){
            cin >> adj[i][j];
		}
	}
	HLPP<int> maxflow(n+2);
	for(int i=1; i<=n; i++){
		maxflow.addEdge(0, i, grp[i] == 2 ? 1e9 : 0);
		maxflow.addEdge(i, n+1, grp[i] == 1 ? 1e9 : 0);
		for(int j=1; j<i; j++){
			if(adj[i][j]) maxflow.addEdge(i, j, adj[i][j], adj[i][j]);
		}
	}
	cout << maxflow.flow(0, n+1) << "\n";
	for(int i = 1; i <= n; i++){
		if(!maxflow.cutSide(i)) cout << i << " ";
	}
    cout << "\n";
	for(int i = 1; i <= n; i++){
		if(maxflow.cutSide(i)) cout << i << " ";
	}
    cout << "\n";
}

#elif other2
// #include<bits/stdc++.h>
using namespace std;
const int INF=1e9;

template<typename T>
struct Flow{
    struct Edge{
        int nxt;
        T cap;
        int rev;
        Edge(int nxt,T cap,int rev):nxt(nxt),cap(cap),rev(rev){}
    };
    int n,src,snk,high,cnt;
    vector<int> h;
    vector<T> ex;
    vector<vector<Edge>> adj;
    vector<vector<int>> list;
    Flow(int _n):n(_n+2),src(n-1),snk(n),ex(n+1),adj(n+1),list(2*n+1){}
    void edge(int a,int b,T c){
        adj[a].emplace_back(b,c,adj[b].size());
        adj[b].emplace_back(a,0,adj[a].size()-1);
    }
    void global(){
        cnt=0;
        high=0;
        h.assign(n+1,n);
        list.clear();
        list.resize(2*n+1);
        queue<int> q;
        q.push(snk);
        h[snk]=0;
        while(!q.empty()){
            int cur=q.front();
            q.pop();
            for(auto&[nxt,cap,rev]:adj[cur]){
                if(adj[nxt][rev].cap && h[nxt]==n){
                    h[nxt]=h[cur]+1;
                    q.push(nxt);
                    if(ex[nxt] && nxt!=src && nxt!=snk){
                        list[h[nxt]].push_back(nxt);
                        high=max(high,h[nxt]);
                    }
                }
            }
        }
    }
    void discharge(int cur){
        int tmp=2*n;
        for(auto&[nxt,cap,rev]:adj[cur]){
            if(cap){
                if(h[nxt]==h[cur]-1){
                    T f=min(ex[cur],cap);
                    cap-=f;
                    adj[nxt][rev].cap+=f;
                    ex[cur]-=f;
                    ex[nxt]+=f;
                    if(ex[nxt]==f && nxt!=src && nxt!=snk){
                        list[h[nxt]].push_back(nxt);
                        high=max(high,h[nxt]);
                    }
                }
                else tmp=min(tmp,h[nxt]+1);
            }
            if(!ex[cur]) return;
        }
        ++cnt;
        if(tmp!=2*n) h[cur]=tmp;
        list[h[cur]].push_back(cur);
        high=max(high,h[cur]);
    }
    int sol(){
        for(auto&[nxt,cap,rev]:adj[src]){
            adj[nxt][rev].cap+=cap;
            ex[nxt]+=cap;
            cap=0;
        }
        global();
        while(high>=0){
            if(list[high].empty()){
                --high;
                continue;
            }
            int cur=list[high].back();
            list[h[cur]].pop_back();
            discharge(cur);
            if(cnt>=4*n) global();
        }
        return ex[snk];
    }
    int cut(){
        vector<bool> chk(2*n+1);
        for(int i=1; i<=n-2; ++i){
            chk[h[i]]=1;
        }
        for(int i=1;; ++i){
            if(!chk[i]) return i;
        }
    }
};

int main(){
    cin.tie(0)->sync_with_stdio(0);
    int n;
    cin>>n;
    Flow<int> flow(n);
    for(int s,i=1; i<=n; ++i){
        cin>>s;
        if(s==1) flow.edge(flow.src,i,INF);
        if(s==2) flow.edge(i,flow.snk,INF);
    }
    for(int w,i=1; i<=n; ++i){
        for(int j=1; j<=n; ++j){
            cin>>w;
            if(w && i<j){
                flow.edge(i,j,w);
                flow.adj[j].back().cap=w;
            }
        }
    }
    cout<<flow.sol()<<'\n';
    int cut=flow.cut();
    for(int i=1; i<=n; ++i){
        if(flow.h[i]>cut) cout<<i<<' ';
    }
    cout<<'\n';
    for(int i=1; i<=n; ++i){
        if(flow.h[i]<cut) cout<<i<<' ';
    }
    cout<<'\n';
}
#elif other3
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.*;
import java.math.*;
import java.text.DecimalFormat;

public class Main {
	
	static int[][] gr, flow;
	static int N, st = 1, en = 2;
	static boolean end = false;
	static ArrayList<Integer>[] leg;
	static final int inf = 1000000000;
	
	public static void main(String[] args) throws IOException {
		BufferedReader rr = new BufferedReader(new InputStreamReader(System.in));
		BufferedWriter ww = new BufferedWriter(new OutputStreamWriter(System.out));
		int n = Integer.parseInt(rr.readLine());
		N = n + 3;
		gr = new int[N][N]; flow = new int[N][N];
		String[] s = rr.readLine().split(" ");
		for (int i = 0; i < n; i++) {
			if (s[i].equals("1")) gr[1][i+3] = inf;
			else if (s[i].equals("2")) gr[i+3][2] = inf;
		}
		for (int i = 0; i < n; i++) {
			s = rr.readLine().split(" ");
			for (int j = 0; j < n; j++) {
				int r = Integer.parseInt(s[j]);
				gr[i+3][j+3] = r;
				gr[j+3][i+3] = r;
			}
		}
		while (true) {
			tam();
			if (end) break;
		}
		int mf = 0;
		for (int i = 1; i < N; i++) mf += flow[st][i];
		ww.write(mf + "\n");
		boolean[] reach = new boolean[N], in = new boolean[N];
		TreeSet<Integer> aa = new TreeSet<>();
		ArrayDeque<Integer> q = new ArrayDeque<>();
		int f = st;
		in[f] = true;
		while (true) {
			for (int i = 1; i < N; i++) {
				if (in[i] || gr[f][i] - flow[f][i] <= 0) continue;
				q.add(i);
				in[i] = true;
				aa.add(i);
			}
			if (q.isEmpty()) break;
			f = q.pollFirst();
		}
		for (int c : aa) {
			ww.write(c-2 + " ");
			reach[c] = true;
		}
		ww.write("\n");
		for (int i = 3; i < N; i++) {
			if (!reach[i]) ww.write(i-2 + " ");
		}
		ww.write("\n");
		ww.flush();
		ww.close();
	}
	
	static void tam() {
		leg = new ArrayList[N];
		for (int i = 0; i < N; i++) leg[i] = new ArrayList<>();
		ArrayDeque<Integer> q = new ArrayDeque<>();
		int[] lev = new int[N];
		int f = st, level = 1;
		lev[f] = level;
		boolean isP = false;
		while (true) {
			level++;
			for (int j = 1; j < N; j++) {
				if (lev[j] != 0 && lev[j] != level || gr[f][j] - flow[f][j] <= 0) continue;
				leg[f].add(j);				
				if (lev[j] != 0) continue;
				lev[j] = level;
				q.add(j);
				if (j == en) isP = true;
			}
			if (q.isEmpty()) break;
			f = q.pollFirst();
			level = lev[f];
		}
		if (!isP) {
			end = true;
			return;
		}
		dfs(st, inf);
	}
	
	static int dfs(int i, int j) {
		if (i == en) {
			return j;
		}
		int sum = 0;
		for (int c : leg[i]) {
			int r = dfs(c, Math.min(j, gr[i][c] - flow[i][c]));
			if (r > 0) {
				flow[i][c] += r;
				flow[c][i] -= r;
				j -= r;
				sum += r;
				if (j <= 0) break;
			}
		}
		return sum;
	}
}

#elif other4
import sys
from collections import deque
input = sys.stdin.readline

N = int(input())

graph = [[] for _ in range(N+2)]
capacity = [[0 for _ in range(N+2)] for _ in range(N+2)]
flow = [[0 for _ in range(N+2)] for _ in range(N+2)]
level = [-1] * (N+2)
work = [0] * (N+2)
start = N
sink = N+1

def bfs(start):
    global level
    level = [-1] * (N+2)
    level[start] = 0
    q = deque()
    q.append(start)

    while q:
        cur = q.popleft()

        for next in graph[cur]:
            if (capacity[cur][next] - flow[cur][next] > 0) and (level[next] == -1):
                q.append(next)
                level[next] = level[cur] + 1
    return level[sink] > 0

def dfs(cur, flow_num):
    if cur == sink:
        return flow_num
        
    while work[cur] < len(graph[cur]):
        next = graph[cur][work[cur]]
        if level[next] == level[cur] + 1 and (capacity[cur][next] - flow[cur][next] > 0):
            ret = dfs(next, min(flow_num, capacity[cur][next] - flow[cur][next]))
            if ret > 0:
                flow[cur][next] += ret
                flow[next][cur] -= ret
                return ret
        work[cur] += 1
    return 0

def get_path( start, visited):
    q = deque()
    q.append(start)

    while q:
        cur = q.popleft()
        visited[cur] = 1

        for next in graph[cur]:
            if (capacity[cur][next] - flow[cur][next]) > 0 and visited[next] == 0:
                q.append(next)

    return visited

if __name__ == "__main__":
    T = list(map(int, input().split()))
    w = [list(map(int, input().split())) for _ in range(N)]
    start = N
    sink = N+1

    for i in range(N):
        if T[i] == 1:
            graph[start].append(i)
            graph[i].append(start)
            capacity[start][i] = float('inf')
        elif T[i] == 2:
            graph[i].append(sink)
            graph[sink].append(i)
            capacity[i][sink] = float('inf')

    for i in range(N):
        for j in range(N):
            if w[i][j] > 0:
                graph[i].append(j)
                capacity[i][j] = w[i][j]
    ret = 0
    while bfs(start):
        work = [0] * (N+2)
        while True:
            fl = dfs(start, float('inf'))
            if fl == 0:
                break
            ret += fl
    visited = [0] * (N+2)
    q =deque([start])
    while q:
        cur=q.popleft()
        for next in graph[cur]:
            if capacity[cur][next] - flow[cur][next]>0 and visited[next]==0:
                visited[next] = 1
                q.append(next)
    a = []
    b = []
    for i in range(N):
        if visited[i] == 1:
            a.append(i+1)
        else:
            b.append(i+1)
        
    print(ret)
    print(' '.join(map(str,a)))
    print(' '.join(map(str,b)))
#endif
}
