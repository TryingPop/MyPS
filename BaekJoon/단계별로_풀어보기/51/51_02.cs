using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 16
이름 : 배성훈
내용 : 열혈강호 4
    문제번호 : 11378번

    네트워크 플로우 문제다

    이분 매칭으로 해결했다;
    모든 인원에 대해 이분 매칭을 한 번 시켰다
    그리고 벌점 k라는 가상인물을 만들고 변화가 없을 때까지 최대 k번 매칭을 돌렸다

    호프크로프트 카프 알고리즘으로 해결하고 싶었으나 작동 방식을 봤을 때
    A의 길이 늘리면 해결 가능하나, 이분매칭처럼 1개만 추가해 k번 돌리는 방법과 비슷하게
    만들 줄 몰라 시도 안했다

    시간 복잡도는 이분 매칭은 O(VE), 
    호프크로프트 카프 알고리즘은 O((root V) * E)라 일단 늘리는 쪽으로 다시 제출해봐야겠다!

    이후 에드먼드 카프 알고리즘(네트워크 플로우)으로 풀었다
    잘못짜서 그런지 메모리 40배, 시간도 20배 이상 걸린다
    
    아이디어는 다음과 같다
    시작지점을(source) 0으로 하고 모든 사원들을 상징하는 정점과 이었다
    그리고 일을 나타내는 모든 정점은 끝 점(sink)에 이었다

    그리고 k번 벌점은 이분 매칭과 같이 벌점 노드(add)를 만들어 갈수 있는 모든 경로로 이었다
    물론 시작지점에서(source) 벌점 노드(add)로 이어줬다

    여기서 시작지점에서 벌점 노드로 가는 간선만 최대 유량을 k로 하고 이외는 1로 한다!
    그리고 시작지점에서 끝점으로 네트워크 플로우 알고리즘으로 흐르는 양을 찾으면 최대 매칭된 값이 된다
*/

namespace BaekJoon._51
{
    internal class _51_02
    {

        static void Main2(string[] args)
        {

#if first

            // 이분 매칭
            StreamReader sr;
            int n, m, k;

            List<int>[] line;

            int[] match;
            bool[] visit;

            Solve();
            void Solve()
            {

                Input();

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                while (true)
                {

                    int match = 0;
                    Array.Fill(visit, false);
                    if (DFS(n + 1)) match++;
                    k--;
                    ret += match;
                    if (k <= 0 || match == 0 || ret == m) break;
                }

                Console.WriteLine(ret);
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                match = new int[m + 1];
                visit = new bool[m + 1];

                line = new List<int>[n + 2];

                for (int i = 1; i <= n; i++)
                {

                    int len = ReadInt();
                    line[i] = new(len);
                    for (int j = 0; j < len; j++)
                    {

                        int b = ReadInt();
                        line[i].Add(b);
                        visit[b] = true;
                    }
                }

                line[n + 1] = new(m);
                for (int i = 1; i <= m; i++)
                {

                    if (visit[i]) line[n + 1].Add(i);
                }

                sr.Close();
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
#else

            int INF = 10_000;
            StreamReader sr;

            int n, m, k;
            int[][] c, f;
            int[] d;

            List<int>[] line;
            int source, sink;   // 시작과 끝
            int len;
            int ret = 0;

            Solve();

            void Solve()
            {

                Input();

                MaxFlow(source, sink);

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                source = 0;
                sink = n + m + 2;

                len = sink + 1;

                c = new int[len][];
                f = new int[len][];
                d = new int[len];

                line = new List<int>[len];

                for (int i = 0; i < len; i++)
                {

                    line[i] = new();
                    c[i] = new int[len];
                    f[i] = new int[len];
                }

                for (int i = 1; i <= n; i++)
                {

                    int l = ReadInt();
                    for (int j = 0; j < l; j++)
                    {

                        int to = ReadInt() + n;
                        line[i].Add(to);
                        line[to].Add(i);

                        d[to] = 1;
                        c[i][to] = 1;
                    }
                }

                int add = n + m + 1;
                for (int i = n + 1; i <= n + m; i++)
                {

                    line[sink].Add(i);
                    line[i].Add(sink);
                    c[i][sink] = 1;

                    if (d[i] != 1) continue;

                    line[add].Add(i); 
                    line[i].Add(add);
                    c[add][i] = 1;
                    d[i] = 0;
                }

                for (int i = 1; i <= n; i++)
                {

                    line[source].Add(i);
                    line[i].Add(source);

                    c[source][i] = 1;
                }

                line[source].Add(add);
                line[add].Add(source);
                c[source][add] = k;

                sr.Close();
            }

            void MaxFlow(int _s, int _e)
            {

                Queue<int> q = new(len);
                while (true)
                {

                    Array.Fill(d, -1);

                    q.Enqueue(_s);

                    while(q.Count > 0)
                    {

                        int cur = q.Dequeue();

                        for (int i = 0; i < line[cur].Count; i++)
                        {

                            int next = line[cur][i];

                            if (c[cur][next] - f[cur][next] > 0 && d[next] == -1)
                            {

                                q.Enqueue(next);
                                d[next] = cur;
                                if (next == _e) break;
                            }
                        }
                    }

                    if (d[_e] == -1) break;
                    int flow = INF;

                    for (int i = _e; i != _s; i = d[i])
                    {

                        flow = Math.Min(flow, c[d[i]][i] - f[d[i]][i]);
                    }

                    for (int i = _e; i != _s; i = d[i])
                    {

                        f[d[i]][i] += flow;
                        f[i][d[i]] -= flow;
                    }

                    ret += flow;
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
#endif
        }
    }

#if other
// #include <unistd.h>
// #include <sys/stat.h>
// #include <sys/mman.h>

int buf[36];
char* s = (char*) mmap(0, buf[12], 3, 2, 0, fstat(0, (struct stat*)buf)), *q = s;
inline int rI() {
  int x=0; bool e;
  q += e = *q == '-';
  while(*q >= '0') x = x*10 + *q++ - '0'; q++;
  return e ? -x : x;
}

// #include <iostream>
// #include <stack>
// #include <algorithm>
using namespace std;

int n,m,k,t,ans,p;
bool w[1000][1000],chk;
int r[1000],c[1000],r0[1000];
stack<int> st;

void row(){
	while (!st.empty()){
		t=st.top(); st.pop();
		if (!r[t]) {continue;}
		for (p=0; !w[t][p]; p++);
		if (!k) {ans+=r0[t]?0:1; c[p]--;}
		else {
			ans++; r0[t]++; c[p]--;
			w[t][p]=false; r[t]--;
			if (r0[t]>1) {k--;}
			if (!c[p]) {continue;}
			c[p]=0;
		}	
		for (int i=0; i<n; i++){
			if (w[i][p]){
				r[i]--; w[i][p]=false;
				if (r[i]==1) {st.push(i);}
			}	
		}
	}
}

void col(){
	while (!st.empty()){
		t=st.top(); st.pop();
		if (!c[t]) {continue;}
		for (p=0; !w[p][t]; p++);
		if (!k) {ans+=r0[p]?0:1; r[p]=0;}
		else {
			ans++; r0[p]++; c[t]=0;
			w[p][t]=false; r[p]--;
			if (r0[p]>1) {k--;}
			if (k || !r[p]) {continue;}
			r[p]=0;
		}	
		for (int i=0; i<m; i++){
			if (w[p][i]){
			c[i]--; w[p][i]=false;
			if (c[i]==1) {st.push(i);}
			}	
		}
	}
}

int main(){
    n=rI(),m=rI(),k=rI();
    for (int i=0; i<n; i++){
    	r[i]=rI();
    	for (int j=0; j<r[i]; j++){
    		t=rI(); t--;
    		w[i][t]=true; c[t]++;
		}	
	}
	while (1){
		chk=false;	
		for (int i=0; i<n; i++)
		{if (r[i]==1) {st.push(i);} }
		if (!st.empty()) {chk=true; row();}
		for (int i=0; i<m; i++)
		{if (c[i]==1) {st.push(i);} }
		if (!st.empty()) {chk=true; col();}
		if (!chk) {break;} 
	}
	cout << ans+min(n-count(r,r+n,0)+k,m-count(c,c+m,0));
    return 0;
}
#elif other2
// #include <bits/stdc++.h>

using namespace std;

vector<vector<int>> g;
vector<bool> vis;

bool augment(int u, int dst) {
  vis[u] = true;

  if (u == dst) return true;

  for (int i = 0; i < g[u].size(); ++i) {
    int v = g[u][i];
    if (!vis[v]) {
      if (augment(v, dst)) {
        swap(g[u][i], g[u].back());
        g[u].pop_back();
        i--;
        g[v].push_back(u);
        return true;
      }
    }
  }
  return false;
}

int main() {
  ios::sync_with_stdio(false);
  cin.tie(nullptr);
  cout.tie(nullptr);

  int n, m, k;
  cin >> n >> m >> k;

  g.resize(n + m + 3);
  vis.resize(n + m + 3);

  for (int i = 0; i < n; ++i) {
    int num_elements;
    cin >> num_elements;

    for (int j = 0; j < num_elements; ++j) {
      int e;
      cin >> e;
      g[i + 1].push_back(n + e);
    }
  }

  for (int i = 1; i <= n; ++i) {
    g[0].push_back(i);
  }
  for (int i = n + 1; i <= n + m; ++i) {
    g[i].push_back(n + m + 1);
  }
  for (int i = 0; i < k; ++i) {
    g[0].push_back(n + m + 2);
  }
  for (int i = 1; i <= n; ++i) {
    for (int j = 0; j < min(k, (int)g[i].size()); ++j) {
      g[n + m + 2].push_back(i);
    }
  }

  int cnt = 0;
  while (augment(0, n + m + 1)) {
    fill(vis.begin(), vis.end(), false);
    cnt += 1;
  }

  cout << cnt;
}

#elif other3
import java.io.*;

class Main {

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
		public Reader(String File_name) throws IOException {
			di = new DataInputStream(new FileInputStream(File_name));
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
	static int n, m, k, l, ret, ans, B[], adj[][];
	static boolean v[];
	
	static boolean dfs(int a) {
		for(int b : adj[a]) {
			if(!v[b]) {
				v[b] = true;
				if(B[b] == 0 || dfs(B[b])) {
					B[b] = a;
					return true;
				}
			}
		}
		return false;
	}
	static int match(boolean first) {
		int ret = 0;
		for(int i=1; i<=n; i++) {
			if(dfs(i)) {
				ret++;
				if(!first && --k == 0) break;
				v = new boolean[m+1];
			}
		}
		return ret;
	}
	public static void main(String[] args) throws Exception {
		Reader rs = new Reader();
		n = rs.nextInt();
		m = rs.nextInt();
		k = rs.nextInt();
		B = new int[m+1];
		adj = new int[n+1][];
		v = new boolean[m+1];
		
		for(int i=1; i<=n; i++) {
			adj[i] = new int[l = rs.nextInt()];
			for(int j=0; j<l; j++)
				adj[i][j] = rs.nextInt();
		}
		ans = match(true);
		do ans += ret = match(false);
		while(ret > 0 && k > 0);
		System.out.println(ans);
	}
}
#elif other4
using static IO;
public class IO{
public static IO Cin=new();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static implicit operator string(IO _)=>reader.ReadLine();
public static implicit operator int(IO _)=>int.Parse(reader.ReadLine());
public static implicit operator string[](IO _)=>reader.ReadLine().Split();
public static implicit operator int[](IO _)=>Array.ConvertAll(reader.ReadLine().Split(),int.Parse);
public static implicit operator (int,int)(IO _){int[] a=Cin;return(a[0],a[1]);}
public static implicit operator (int,int,int)(IO _){int[] a=Cin;return(a[0],a[1],a[2]);}
public void Deconstruct(out int a,out int b){(int,int) r=Cin;(a,b)=r;}
public void Deconstruct(out int a,out int b,out int c){(int,int,int) r=Cin;(a,b,c)=r;}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    record LineInfo(int dot,int limit);
    public static void Coding() {
        (int worker,int barn,int pro) = Cin;
        int[][] cows = new int[worker][];
        for(int i=0;i<worker;i++) {
            int[] input = Cin;
            cows[i] = new int[input[0]];
            for(int x=0;x<input[0];x++) {
                cows[i][x] = input[x+1]-1;
            }
        }
        int?[] book = new int?[barn];
        bool[] visited;
        bool dfs(int me,int exclude) {
            if (visited[me]) return false;
            visited[me]=true;
            foreach(var place in cows[me]) {
                if (place == exclude) continue;
                if (book[place] is int other) {
                    if (dfs(other,place)) {
                        book[place] = me;
                        return true;
                    }
                    //양보 안되면 다른 장소 찾아보기.
                } else {
                    book[place] = me;
                    return true;
                }
            }
            return false;
        }

        for(int x=0;x<worker;x++) {
            visited = new bool[worker];
            dfs(x,-1);
        }
        for(int x=0;x<worker && pro>0;x++) {
            
            //일할수 잇다면 네... 하세요
            while (pro > 0) {
                visited = new bool[worker];
                if (!dfs(x,-1)) break;
                pro--;
            }
        }

        Cout = book.Count(x => x is not null);
    }
}
#elif other5
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Data;

namespace Algorithm
{
    class Program
    {
        static List<List<int>> graph;
        static int[] d;
        static bool[] vis;
        static bool DFS(int x)
        {
            for(int i = 0; i < graph[x].Count; i++)
            {
                int tmp = graph[x][i];
                if(vis[tmp])
                    continue;
                vis[tmp] = true;
                if(d[tmp] == 0 || DFS(d[tmp]))
                {
                    d[tmp] = x;
                    return true;
                }
            }
            return false;
        }
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            
            string[] nm = sr.ReadLine().Split();
            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);
            int k = int.Parse(nm[2]);
            graph = new List<List<int>>(){new List<int>()};
            d = new int[m+1];
            vis = new bool[m+1];
            int count = 0;
            for(int i = 0; i < n; i++)
            {
                List<int> input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse).ToList();
                input.RemoveAt(0);
                graph.Add(input.ToList());
            }
            for(int i = 1; i <= n; i++)
            {
                vis = new bool[m+1];
                if(DFS(i))
                    count++;
            }
            int prek = k;
            while(k!= 0)
            {
                for(int i = 1; i <= n; i++)
                {
                    vis = new bool[m+1];
                    if(DFS(i))
                    {
                        count++;
                        k--;
                        if(k == 0)
                            break;
                    }
                }
                if(k == prek)
                    break;
                prek = k;
            }
            sb.Append(count);
            sw.WriteLine(sb);
            sr.Close();
            sw.Close(); 
        }
        static int LIS(List<int> seq)
        {
            List<double> lis = new List<double>();
            for(int i = 0; i< seq.Count; i++)
            {
                int index = lis.BinarySearch(seq[i]);
                if(index < 0)
                    index = Math.Abs(index)-1;
                /*
                else
                {
                    lis[index]-=0.5f;
                    index++;
                }
                */
                if(index >= lis.Count)
                    lis.Add(seq[i]);
                else
                    lis[index] = seq[i];
            }
            return lis.Count;
        }
    }
}
#endif
}
