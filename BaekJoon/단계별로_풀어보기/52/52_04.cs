using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 1
이름 : 배성훈
내용 : 동전 교환
    문제번호 : 11493번

    MCMF 문제다
    처음에 정점을 분할해도 되지 않나 싶어 분할없이 해봤다;
    해당 경우 역방향 거리를 조절할 수 없지만 그래도 되지 않을까? 하고 냈다
    그런데 2%에서 틀려 안되겠네 하고 넘어갔다

    그리고 분할해서 로직을 바꿔도 2%를 넘지 못하고
    혹시 상한설정이 문제인가? 하고 10만에서 10억까지 늘려봤고 3번 더 틀렸다

    이후 먼저 상한을 구하기 위해 경우의 수 계산부터 다시 했다
    500개의 노드가 중복을 제외하고 최대 500번 거쳐서 지나갈 수 있으므로 25만이 최대이므로
    상한은 100만 잡았다 
    조금 더 생각해보면 250개 노드가 최대 이동할 수 있는 노드고 거리도 500을 넘지 않아 12.5만으로 줄여도 된다
    조건을 조금 더 따져보면 더 줄일 수 있다!
    
    그래서 문제를 새로이 다시 읽었다
    그러니 색상 문제였다;
    이를 수정하니 3.7? 2.8초대에 이상없이 통과했다;

    아이디어는 다음과 같다
    흰 칸 위에 검정돌이 있는 곳 -> 검정칸 위에 흰돌이 있는 곳으로 최대 유량을 흘리게 했다
    그러면 검정돌이 이동하는 경로가 된다
    여기서 거리를 1로해서 최단거리인 SPFA 알고리즘을 적용시키면 검은돌이 검은칸으로 이동하는데 최단 경로가 된다
*/

namespace BaekJoon._52
{
    internal class _52_04
    {

        static void Main4(string[] args)
        {

            int INF = 125_000;
            int MAX = 500;
            StreamReader sr;
            StreamWriter sw;
            int n, m;
            List<int>[] line;

            int[,] c, f, d;
            int[] dis, before;
            int source, sink;
            bool[] inQ;
            Queue<int> q;
            int ret;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();
                while(test-- > 0)
                {

                    Input();

                    MCMF();
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int len = 2 * MAX + 2;
                line = new List<int>[len];

                for (int i = 0; i < len; i++)
                {

                    line[i] = new();
                }

                c = new int[len, len];
                f = new int[len, len];
                d = new int[len, len];

                dis = new int[len];
                before = new int[len];
                inQ = new bool[len];
                q = new(len);
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                source = 0;
                sink = n + n + 1;

                for (int i = source; i <= sink; i++)
                {

                    line[i].Clear();
                    for (int j = source; j <= sink; j++)
                    {

                        c[i, j] = 0;
                        c[j, i] = 0;

                        f[i, j] = 0;
                        f[j, i] = 0;

                        d[i, j] = 0;
                        d[j, i] = 0;
                    }
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[f + n].Add(b);
                    line[b].Add(f + n);

                    c[f + n, b] = INF;

                    d[f + n, b] = 1;
                    d[b, f + n] = -1;


                    line[b + n].Add(f);
                    line[f].Add(b + n);

                    c[b + n, f] = INF;

                    d[b + n, f] = 1;
                    d[f, b + n] = -1;
                }

                for (int i = 1; i < sink; i++)
                {

                    dis[i] = ReadInt();
                }

                for (int i = 1; i <= n; i++)
                {

                    line[i].Add(i + n);
                    line[i + n].Add(i);
                    c[i, i + n] = INF;

                    if (dis[i] == 0 && dis[i + n] == 1)
                    {

                        c[source, i] = 1;

                        line[source].Add(i);
                        line[i].Add(source);
                        continue;
                    }
                }

                for (int i = n + 1; i < sink; i++)
                {

                    if (dis[i] == 0 && dis[i - n] == 1)
                    {

                        c[i, sink] = 1;

                        line[sink].Add(i);
                        line[i].Add(sink);
                        continue;
                    }
                }
            }

            void MCMF()
            {

                ret = 0;

                while (true)
                {

                    Array.Fill(before, -1, source, sink + 1);
                    Array.Fill(inQ, false, source, sink + 1);
                    Array.Fill(dis, INF, source, sink + 1);
                    dis[source] = 0;

                    q.Enqueue(source);
                    inQ[source] = true;
                    before[source] = source;

                    while(q.Count > 0)
                    {

                        int node = q.Dequeue();
                        inQ[node] = false;

                        for (int i = 0; i < line[node].Count; i++)
                        {

                            int next = line[node][i];
                            int nDis = d[node, next];

                            if (c[node, next] - f[node, next] > 0 && dis[next] > dis[node] + nDis)
                            {

                                dis[next] = dis[node] + nDis;
                                before[next] = node;

                                if (!inQ[next])
                                {

                                    inQ[next] = true;
                                    q.Enqueue(next);
                                }
                            }
                        }
                    }

                    if (before[sink] == -1) break;

                    int flow = INF;
                    for (int i = sink; i != source; i = before[i])
                    {

                        flow = Math.Min(flow, c[before[i], i] - f[before[i], i]);
                    }

                    for (int i = sink; i != source; i = before[i])
                    {

                        ret += flow * d[before[i], i];
                        f[before[i], i] += flow;
                        f[i, before[i]] -= flow;
                    }
                }

                sw.Write($"{ret}\n");
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
import java.io.*;
import java.util.*;

class Main {

	static int T, n, m, s, t, ans, black[], dist[][], INF = (int)1e9;
	static List<Edge> adj[];
	
	static class Edge {
		
		int to, c, f, w;
		Edge reverse;
		
		Edge(int to, int c, int w){
			this.to = to;
			this.c = c;
			this.w = w;
		}
		int spare() {
			return c-f;
		}
		void addFlow() {
			f++;
			reverse.f--;
		}
	}
	static void addEdge(int u, int v, int w) {
		Edge e1 = new Edge(v, 1, w);
		Edge e2 = new Edge(u, 0, -w);
		e1.reverse = e2;
		e2.reverse = e1;
		adj[u].add(e1);
		adj[v].add(e2);
	}
	public static void main(String[] args) throws Exception {
		Reader rs = new Reader();
		BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
		
		for(T=rs.nextInt(); T>0; T--) {
			n = rs.nextInt();
			s = 2*n;
			t = s+1;
			m = rs.nextInt();
			ans = 0;
			black = new int[n];
			dist = new int[n][n];
			adj = new ArrayList[t+1];
			
			for(int i=0; i<n; i++) {
				Arrays.fill(dist[i], INF);
				dist[i][i] = 0;
			}
			for(int i=0; i<=t; i++)
				adj[i] = new ArrayList<>();
			
			while(m-- > 0) {
				int u = rs.nextInt()-1;
				int v = rs.nextInt()-1;
				dist[u][v] = 1;
				dist[v][u] = 1;
			}
			for(int k=0; k<n; k++)
				for(int i=0; i<n; i++)
					for(int j=0; j<n; j++)
						dist[i][j] = Math.min(dist[i][j], dist[i][k] + dist[k][j]);
			
			for(int i=0; i<n; i++)
				if((black[i] = rs.nextInt()) == 0)
					addEdge(i+n, t, 0);

			for(int i=0; i<n; i++) {
				if(rs.nextInt() == 0) {
					addEdge(s, i, 0);
					for(int j=0; j<n; j++) {
						if(black[j] == 0) {
							addEdge(i, j+n, dist[i][j]);
						}
					}
				}
			}
			while(true) {
				Edge path[] = new Edge[t+1];
				int dp[] = new int[t+1];
				boolean inQ[] = new boolean[t+1];
				Queue<Integer> q = new LinkedList<>();
				
				Arrays.fill(dp, INF);
				dp[s] = 0;
				inQ[s] = true;
				q.add(s);
				while(!q.isEmpty()) {
					int cur = q.poll();
					inQ[cur] = false;
					for(Edge e : adj[cur]) {
						if(e.spare() > 0 && dp[e.to] > dp[cur] + e.w) {
							dp[e.to] = dp[cur] + e.w;
							path[e.to] = e;
							if(!inQ[e.to]) {
								inQ[e.to] = true;
								q.add(e.to);
							}
						}
					}
				}
				if(path[t] == null) break;
				for(int i=t; i!=s; i=path[i].reverse.to) {
					ans += path[i].w;
					path[i].addFlow();
				}
			}
			bw.write(ans + "\n");
		}
		bw.close();
	}
	static class Reader {
		
		final private int BUFFER_SIZE = 1<<16;
		private DataInputStream di;
		private byte[] buffer;
		private int bufferPointer, bytesRead;
		
		public Reader() {
			di = new DataInputStream(System.in);
			buffer = new byte[BUFFER_SIZE];
			bufferPointer = bytesRead = 0;
		}
		public int nextInt() throws IOException{
			int ret = 0;
			byte c = read();
			while(c <= ' ') c = read();
			boolean neg = (c == '-');
			if(neg) c = read();
			do ret = ret*10+c-'0';
			while((c=read())>='0' && c<='9');
			return neg ? -ret : ret;
		}
		private void fillBuffer() throws IOException {
			bytesRead = di.read(buffer, bufferPointer = 0, BUFFER_SIZE);
			if(bytesRead == -1) buffer[0] = -1;
		}
		private byte read() throws IOException {
			if(bufferPointer == bytesRead) fillBuffer();
			return buffer[bufferPointer++];
		}
		public void close() throws IOException {
			if(di == null) return;
			di.close();
		}
	}
}
#elif other2
import sys
from collections import deque
input=sys.stdin.readline
INF=float('INF')
F=lambda:[*map(int,input().split())]
class mcmf:
  def __init__(self, V, st, ed) :
    self.len = V
    self.G = [[] for _ in range(V)]
    self.C = [[0] * V for _ in range(V)]
    self.COST = [[0] * V for _ in range(V)]
    self.chk = [False] * V
    self.vis = [False] * V
    self.lvl = [0] * V
    self.st = st
    self.ed = ed
    self.mc = 0

  def add_edge(self, u, v, flow, cost) :
    self.G[u].append(v)
    self.G[v].append(u)
    self.C[u][v] = flow
    self.COST[u][v] = cost
    self.COST[v][u] = -cost

  def bfs(self) :
    Q = deque([self.st])
    inQ = [False] * self.len 
    lvl = [INF] * self.len
    lvl[self.st] = 0

    while Q :
      u = Q.popleft()
      inQ[u] = False

      for v in self.G[u] :
        if self.C[u][v] > 0 and lvl[v] > lvl[u] + self.COST[u][v] :
          lvl[v] = lvl[u] + self.COST[u][v]
          if not inQ[v] :
            Q.append(v)
            inQ[v] = True
    
    self.lvl = lvl
    return self.lvl[self.ed] != INF

  def dfs(self, u, w) :
    if u == self.ed : return w
    flow = w
    self.vis[u] = True

    for v in self.G[u] :
      if not flow : break
      if not self.vis[v] and not self.chk[v] and self.C[u][v] > 0 and self.lvl[v] == self.lvl[u] + self.COST[u][v] :
        res = self.dfs(v, min(flow, self.C[u][v]))
        if res == 0 : 
          self.chk[v] = True
          continue
        self.C[u][v] -= res
        self.C[v][u] += res
        self.mc += res * self.COST[u][v]
        flow -= res
    
    self.vis[u] = False
    return w - flow

  def calc(self) :
    mf = 0
    while self.bfs() :
      self.chk = [False] * self.len
      self.vis = [False] * self.len
      mf += self.dfs(self.st, INF)
    return (self.mc, mf)

for _ in range(int(input())):
    N,M=F()
    ST=0;ED=2*N+1
    MCMF=mcmf(2*N+2,ST,ED) #1~N : in / N+1~2N: out
    for _ in range(M):
        a,b=F()
        MCMF.add_edge(a+N,b,INF,1)
        MCMF.add_edge(b+N,a,INF,1)
    COLOR1=F() #정점색
    COLOR2=F() #동전색
    for i in range(1,N+1):
        if COLOR2[i-1]:MCMF.add_edge(ST,i,1,0)
        if COLOR1[i-1]:MCMF.add_edge(i+N,ED,1,0)
        MCMF.add_edge(i,i+N,INF,0)
    print(MCMF.calc()[0])
    
#elif other3
// #include <iostream>
// #include <vector>
// #include "cstring"
// #include <set>
// #include <array>
// #include <algorithm>
// #include <random>
// #include "queue"
// #include <bitset>
// #include "array"
// #include <chrono>
//#include <ext/pb_ds/tree_policy.hpp>
//#include <ext/pb_ds/assoc_container.hpp>
using namespace std;
//using namespace __gnu_pbds;
//typedef tree<int, null_type, less<int>, rb_tree_tag,
//        tree_order_statistics_node_update> pds;
// #define all(x)x.begin(),x.end()
// #define pack(x)sort(all(x));x.erase(unique(all(x)),x.end())
// #define gi(x, v)lower_bound(all(x),v)-x.begin()
using ll = long long;
using ld = long double;
using tu = array<int, 3>;

template<int N>
struct McmfDin{
    struct stat{
        int nxt,cap,cos,rev;
    };
    int sv[N]{};
    vector<stat> v[N];
    int nv[N];////start_i
    bool vis[N];
    bool spfa(int s,int e){
        bool isIn[N]{};
        queue<int> q;
        q.push(s);
        memset(sv,-1,sizeof sv);
        sv[s]=0;
        while(!q.empty()){
            int p=q.front();
            q.pop();
            isIn[p]=false;
            for(auto i:v[p]){
                if(i.cap<=0)continue;
                if(sv[i.nxt]==-1||sv[i.nxt]>sv[p]+i.cos){
                    sv[i.nxt]=sv[p]+i.cos;
                    if(!isIn[i.nxt]){
                        q.push(i.nxt);
                        isIn[i.nxt]=true;
                    }
                }
            }
        }
        return sv[e] > 0;
    }
    int dfs(int s,int e,int f){
        vis[s]=true;
        if(s==e)return f;
        for(int &i=nv[s];i<v[s].size();i++){
            stat &j=v[s][i];
            if(vis[j.nxt]||sv[s]+j.cos!=sv[j.nxt]||j.cap<=0)continue;
            int w=dfs(j.nxt,e,min(f,j.cap));
            if(w<=0)continue;
            j.cap-=w;
            v[j.nxt][j.rev].cap+=w;
//        flow[s][j]+=w;
//        flow[j][s]-=w;
            return w;
        }return 0;
    }bool upd(int s,int n){
        int mn=-1e9;
        for(int i=s;i<=n;i++){
            if(!vis[i])continue;
            for(auto j:v[i]){
                if(j.cap>0&&!vis[j.nxt])mn=max(mn,-sv[i]-j.cos+sv[j.nxt]);
            }
        }if(mn==1e9)return 0;
        for(int i=s;i<=n;i++)if(!vis[i])sv[i]-=mn;
        return true;
    }
    static const int inf=1e9;
    void con(int a,int b,int c,int d,int e=0,int f=-inf){////a->b cap cos b-> cap cos
        if(f==-inf)f=-d;
        v[a].push_back({b,c,d,(int)v[b].size()});
        v[b].push_back({a,e,f,(int)v[a].size()-1});
    }
    pair<int,int> flowing(int s,int e,int idL,int idR){
        spfa(s,e);
        int ans=0;
        int flow=0;
        do{
            if(sv[e]<0)break;
            int now;
            memset(vis,0,sizeof vis);
            memset(nv,0,sizeof nv);
            while(now = dfs(s,e,1e9)){
                ans+=sv[e]*now;
                flow+=now;
                memset(vis,0,sizeof vis);
            }
        }while(upd(idL,idR));
        return {flow,ans};
    }
};
McmfDin<510> din;
int col[520];
void solve(){
    int n,m,a,b;
    cin>>n>>m;
    int s=n+1;
    int e=n+2;
    for(int i=1;i<=e;i++)din.v[i].clear();
    for(int i=1;i<=m;i++){
        cin>>a>>b;
        din.con(a,b,520,1);
        din.con(b,a,520,1);
    }for(int i=1;i<=n;i++){
        cin>>col[i];
    }for(int i=1;i<=n;i++){
        cin>>a;
        if(col[i]==0){
            din.con(s,i,a!=0,0);
        }else{
            din.con(i,e,a!=1,0);
        }
    }cout<<din.flowing(s,e,1,e).second<<'\n';
}
int main(){
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int tc;
    cin>>tc;
    while(tc--){
        solve();
    }
}
#endif
}
