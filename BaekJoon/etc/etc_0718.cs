using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 23
이름 : 배성훈
내용 : 열혈강호 3
    문제번호 : 11377번

    이분 매칭 문제다
    호프크로프트 카프 알고리즘으로 해당 문제를 어떻게 풀지 몰라
    기존 이분매칭 방법을 이용했다

    다만, visit 초기화와 최대값 탈출 에서 최대값을 m이 아닌 n으로 잘못 설정해 총 2번 틀렸다
    아이디어는 다음과 같다
    이분 매칭을 조건적으로 2번 돌리면 된다
    다만, k번 2번 매칭되면 끊으면 된다
    이분 매칭 로직상 앞에서 매칭 안된사람은 두 번째에서 매칭될 일이 없어서 k번 매칭되면 끊으면 된다
    그리고 이미 전부 매칭된 경우면 괜히 더 매칭할 필요 없다
*/

namespace BaekJoon.etc
{
    internal class etc_0718
    {

        static void Main718(string[] args)
        {

            int INF = 10_000;

            StreamReader sr;
            int n, m, k;
            int[][] line;

            int[] match;
            bool[] visit;
            int ret;

            Solve();

            void Solve()
            {

                Input();

                Match();
            }

            void Match()
            {

                ret = 0;

                match = new int[m + 1];
                visit = new bool[m + 1];

                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                int add = k;
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i))
                    {

                        ret++;
                        add--;

                        if (add == 0) break;
                    }

                    if (ret == m) break;
                }

                Console.WriteLine(ret);
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Length; i++)
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

                line = new int[n + 1][];

                for (int i = 1; i <= n; i++)
                {

                    int len = ReadInt();
                    line[i] = new int[len];

                    for (int j = 0; j < len; j++)
                    {

                        line[i][j] = ReadInt();
                    }
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
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public enum GraphNodeType
{
    Source,
    Sink,
    Inter,
    Person,
    Book,
}

public record struct GraphNode(GraphNodeType NodeType, int Value);
public class Flow
{
    public int CurrFlow;
    public int MaxFlow;

    public bool CanFlow => CurrFlow < MaxFlow;

    public Flow(int currFlow, int maxFlow)
    {
        CurrFlow = currFlow;
        MaxFlow = maxFlow;
    }

    public override string ToString()
    {
        return $"{CurrFlow}/{MaxFlow}";
    }
}

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nmk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (n, m, k) = (nmk[0], nmk[1], nmk[2]);

        var graph = new Dictionary<GraphNode, Dictionary<GraphNode, Flow>>();
        var source = new GraphNode(GraphNodeType.Source, 0);
        var inter = new GraphNode(GraphNodeType.Inter, 0);
        var sink = new GraphNode(GraphNodeType.Sink, 0);

        AddFlowEdge(graph, source, inter, k);

        for (var personId = 0; personId < n; personId++)
        {
            var person = new GraphNode(GraphNodeType.Person, personId);
            AddFlowEdge(graph, source, person, 1);
            AddFlowEdge(graph, inter, person, 1);
        }

        for (var bookId = 1; bookId <= m; bookId++)
        {
            var book = new GraphNode(GraphNodeType.Book, bookId);
            AddFlowEdge(graph, book, sink, 1);
        }

        for (var personId = 0; personId < n; personId++)
        {
            var arr = sr.ReadLine().Split(' ').Select(Int32.Parse).Skip(1).ToArray();

            var person = new GraphNode(GraphNodeType.Person, personId);
            foreach (var bookId in arr)
            {
                var book = new GraphNode(GraphNodeType.Book, bookId);
                AddFlowEdge(graph, person, book, 1);
            }
        }

        var maxflow = Dinic(graph, source, sink);
        sw.WriteLine(maxflow);
    }

    public static void AddFlowEdge(Dictionary<GraphNode, Dictionary<GraphNode, Flow>> graph, GraphNode src, GraphNode dst, int flow)
    {
        if (!graph.TryGetValue(src, out var d1))
        {
            d1 = new Dictionary<GraphNode, Flow>();
            graph[src] = d1;
        }

        if (d1.ContainsKey(dst))
            throw new ArgumentException();

        d1[dst] = new Flow(0, flow);

        if (!graph.TryGetValue(dst, out var d2))
        {
            d2 = new Dictionary<GraphNode, Flow>();
            graph[dst] = d2;
        }

        if (d2.ContainsKey(src))
            throw new ArgumentException();

        d2[src] = new Flow(0, 0);
    }

    private static int Dinic(Dictionary<GraphNode, Dictionary<GraphNode, Flow>> graph, GraphNode source, GraphNode sink)
    {
        var maxflow = 0;
        var levelGraph = graph.ToDictionary(v => v.Key, _ => default(int?));
        var isDeadEnd = graph.ToDictionary(v => v.Key, _ => false);

        while (true)
        {
            var isMaxFlowChanged = false;

            foreach (var k in graph.Keys)
            {
                levelGraph[k] = default;
                isDeadEnd[k] = false;
            }

            BuildLevelGraph(graph, levelGraph, source, sink);

            while (true)
            {
                var flow = FindFlow(graph, levelGraph, isDeadEnd, source, sink, Int32.MaxValue);
                if (flow == 0)
                    break;

                isMaxFlowChanged = true;
                maxflow += flow;
            }

            if (!isMaxFlowChanged)
                break;
        }

        return maxflow;
    }

    private static int FindFlow(
        Dictionary<GraphNode, Dictionary<GraphNode, Flow>> graph,
        Dictionary<GraphNode, int?> levelGraph,
        Dictionary<GraphNode, bool> isDeadEnd,
        GraphNode currentNode,
        GraphNode sink,
        int minflow)
    {
        if (currentNode == sink)
            return minflow;

        foreach (var (dst, flow) in graph[currentNode])
        {
            if (!flow.CanFlow)
                continue;

            if (isDeadEnd[dst])
                continue;

            if (!levelGraph[currentNode].HasValue || !levelGraph[dst].HasValue)
                continue;

            if (levelGraph[currentNode].Value >= levelGraph[dst].Value)
                continue;

            var remainflow = flow.MaxFlow - flow.CurrFlow;
            var f = FindFlow(graph, levelGraph, isDeadEnd, dst, sink, Math.Min(remainflow, minflow));

            if (f != 0)
            {
                var ghost = graph[dst][currentNode];

                flow.CurrFlow += f;
                ghost.CurrFlow -= f;

                return f;
            }
        }

        isDeadEnd[currentNode] = true;
        return 0;
    }

    private static void BuildLevelGraph(Dictionary<GraphNode, Dictionary<GraphNode, Flow>> graph, Dictionary<GraphNode, int?> levelGraph, GraphNode source, GraphNode sink)
    {
        var q = new Queue<(GraphNode node, int level)>();
        q.Enqueue((source, 0));

        while (q.TryDequeue(out var s))
        {
            var (node, level) = s;

            if (levelGraph[node].HasValue)
                continue;

            levelGraph[node] = level;

            if (node == sink)
                continue;

            foreach (var (dst, flow) in graph[node])
                if (flow.CanFlow && !levelGraph[dst].HasValue)
                    q.Enqueue((dst, 1 + level));
        }
    }
}

#elif other2
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
		if (!k) {ans+=1-r0[t]; c[p]--;}
		else {
			ans++; r0[t]++; c[p]--;
			w[t][p]=false; r[t]--;
			if (r0[t]==2) {k--;}
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
		if (!k) {ans+=1-r0[p]; r[p]=0;}
		else {
			ans++; r0[p]++; c[t]=0;
			w[p][t]=false; r[p]--;
			if (r0[p]==1) {continue;}
			k--;
			if (!r[p]) {continue;}
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
#elif other3
// #include <bits/stdc++.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
using namespace std;

/////////////////////////////////////////////////////////////////////////////////////////////
/*
 * Author : jinhan814
 * Date : 2021-03-22
 * Description : FastIO implementation for cin, cout. (mmap ver.)
 */
const int INPUT_SZ = 8000000;
const int OUTPUT_SZ = 1 << 20;

class INPUT {
private:
	char* p;
	bool __END_FLAG__, __GETLINE_FLAG__;
public:
	explicit operator bool() { return !__END_FLAG__; }
    INPUT() { p = (char*)mmap(0, INPUT_SZ, PROT_READ, MAP_SHARED, 0, 0); }
	bool is_blank(char c) { return c == ' ' || c == '\n'; }
	bool is_end(char c) { return c == '\0'; }
	char _readChar() { return *p++; }
	char readChar() {
		char ret = _readChar();
		while (is_blank(ret)) ret = _readChar();
		return ret;
	}
	template<typename T>
	T _readInt() {
		T ret = 0;
		char cur = _readChar();
		bool flag = 0;
		while (is_blank(cur)) cur = _readChar();
		if (cur == '-') flag = 1, cur = _readChar();
		while (!is_blank(cur) && !is_end(cur)) ret = 10 * ret + cur - '0', cur = _readChar();
		if (is_end(cur)) __END_FLAG__ = 1;
		return flag ? -ret : ret;
	}
	int readInt() { return _readInt<int>(); }
	long long readLL() { return _readInt<long long>(); }
	string readString() {
		string ret;
		char cur = _readChar();
		while (is_blank(cur)) cur = _readChar();
		while (!is_blank(cur) && !is_end(cur)) ret.push_back(cur), cur = _readChar();
		if (is_end(cur)) __END_FLAG__ = 1;
		return ret;
	}
	double readDouble() {
		string ret = readString();
		return stod(ret);
	}
	string getline() {
		string ret;
		char cur = _readChar();
		while (cur != '\n' && !is_end(cur)) ret.push_back(cur), cur = _readChar();
        if (__GETLINE_FLAG__) __END_FLAG__ = 1;
		if (is_end(cur)) __GETLINE_FLAG__ = 1;
		return ret;
	}
	friend INPUT& getline(INPUT& in, string& s) { s = in.getline(); return in; }
} _in;

class OUTPUT {
private:
	char write_buf[OUTPUT_SZ];
	int write_idx;
public:
	~OUTPUT() { bflush(); }
	template<typename T>
	int getSize(T n) {
		int ret = 1;
		if (n < 0) n = -n;
		while (n >= 10) ret++, n /= 10;
		return ret;
	}
	void bflush() {
		fwrite(write_buf, sizeof(char), write_idx, stdout);
		write_idx = 0;
	}
	void writeChar(char c) {
		if (write_idx == OUTPUT_SZ) bflush();
		write_buf[write_idx++] = c;
	}
	void newLine() { writeChar('\n'); }
	template<typename T>
	void _writeInt(T n) {
		int sz = getSize(n);
		if (write_idx + sz >= OUTPUT_SZ) bflush();
		if (n < 0) write_buf[write_idx++] = '-', n = -n;
		for (int i = sz - 1; i >= 0; i--) write_buf[write_idx + i] = n % 10 + '0', n /= 10;
		write_idx += sz;
	}
	void writeInt(int n) { _writeInt<int>(n); }
	void writeLL(long long n) { _writeInt<long long>(n); }
	void writeString(string s) { for (auto& c : s) writeChar(c); }
	void writeDouble(double d) { writeString(to_string(d)); }
} _out;

/* operators */
INPUT& operator>> (INPUT& in, char& i) { i = in.readChar(); return in; }
INPUT& operator>> (INPUT& in, int& i) { i = in.readInt(); return in; }
INPUT& operator>> (INPUT& in, long long& i) { i = in.readLL(); return in; }
INPUT& operator>> (INPUT& in, string& i) { i = in.readString(); return in; }
INPUT& operator>> (INPUT& in, double& i) { i = in.readDouble(); return in; }

OUTPUT& operator<< (OUTPUT& out, char i) { out.writeChar(i); return out; }
OUTPUT& operator<< (OUTPUT& out, int i) { out.writeInt(i); return out; }
OUTPUT& operator<< (OUTPUT& out, long long i) { out.writeLL(i); return out; }
OUTPUT& operator<< (OUTPUT& out, size_t i) { out.writeInt(i); return out; }
OUTPUT& operator<< (OUTPUT& out, string i) { out.writeString(i); return out; }
OUTPUT& operator<< (OUTPUT& out, double i) { out.writeDouble(i); return out; }

/* macros */
// #define fastio 1
// #define cin _in
// #define cout _out
// #define istream INPUT
// #define ostream OUTPUT
/////////////////////////////////////////////////////////////////////////////////////////////

const int INF = int(1e9);

struct Dinic { //Dinic algorithm, O(V^2E). O(Esqrt(V)) for Bipartite-Graph.
	struct Edge {
		int nxt;
		int inv; //inverse edge index
		int res; //residual
	};
	vector<vector<Edge>> adj;
	vector<int> lv, work, check;
	int s, e; //source, sink
	Dinic(int sz) :
		adj(sz), lv(sz), work(sz), check(sz),
		s(-1), e(-1) {
	}
	void set_source(int t) { s = t; }
	void set_sink(int t) { e = t; }
	void add_edge(int a, int b, int cap, bool directed = 1) {
		Edge forward{ b, adj[b].size(), cap };
		Edge reverse{ a, adj[a].size(), directed ? 0 : cap };
		adj[a].push_back(forward);
		adj[b].push_back(reverse);
	}
	bool bfs() {
		memset(&lv[0], -1, sizeof(lv[0]) * lv.size());
		queue<int> Q;
		lv[s] = 0;
		Q.push(s);
		while (!Q.empty()) {
			auto cur = Q.front(); Q.pop();
			for (const auto& [nxt, inv, res] : adj[cur]) {
				if (lv[nxt] == -1 && res > 0) {
					lv[nxt] = lv[cur] + 1;
					Q.push(nxt);
				}
			}
		}
		return lv[e] != -1;
	}
	int dfs(int cur, int tot) {
		if (cur == e) return tot;
		for (int& i = work[cur]; i < adj[cur].size(); i++) {
			auto& [nxt, inv, res] = adj[cur][i];
			if (lv[nxt] == lv[cur] + 1 && res > 0) {
				int fl = dfs(nxt, min(tot, res));
				if (fl > 0) {
					res -= fl;
					adj[nxt][inv].res += fl;
					return fl;
				}
			}
		}
		return 0;
	}
	int max_flow() {
		if (s == -1 || e == -1) return -1;
		int ret = 0, fl;
		while (bfs()) {
			memset(&work[0], 0, sizeof(work[0]) * work.size());
			while (fl = dfs(s, INF)) ret += fl;
		}
		return ret;
	}
};

int main() {
	fastio;
	//input
	int n, m, k; cin >> n >> m >> k;
	int s = 0, e = n + m + 1, b = n + m + 2;
	//init
	Dinic flow(n + m + 3);
	flow.set_source(s);
	flow.set_sink(e);
	flow.add_edge(s, b, k);
	//source -> node
	for (int i = 1; i <= n; i++) {
		flow.add_edge(s, i, 1);
		flow.add_edge(b, i, 1);
	}
	//node -> sink
	for (int i = 1; i <= m; i++) {
		flow.add_edge(n + i, e, 1);
	}
	//edges
	for (int i = 1; i <= n; i++) {
		int cnt; cin >> cnt;
		while (cnt--) {
			int t; cin >> t;
			flow.add_edge(i, n + t, 1);
		}
	}
	//max_flow
	cout << flow.max_flow() << '\n';
}
#endif
}
