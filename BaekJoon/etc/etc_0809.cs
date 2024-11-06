using BaekJoon.etc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 12
이름 : 배성훈
내용 : Äventyr 1, 2
    문제번호 : 15563번, 15564번

    센트로이드 (Centroid Decomposition) 알고리즘을 써야한다
    https://tjdgus4384.tistory.com/7

	etc_0811(트리와 쿼리 5)을 개조해서 풀었다
	etc_0809 -> etc_0811 순으로 했지만
	etc_811을 풀면서 센트로이드 분할이 뭔지 깨달았고

	여기서는 한번 색상이 정해지면 고정되기에
	비싼 SortedDictionary를 쓸 필요가 없다
	그냥 배열로 하니 양쪽 다 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0809
    {

        static void Main809(string[] args)
        {

            int INF = 1_000_001;
            int LOG = 17;

            StreamReader sr;
            StreamWriter sw;

            int n, k;

            List<int>[] line;
            int[][] parent;
            int[] depth;

            int[] size;
            bool[] visit;
            int[] tree;

            bool[] color;
            int[] nTc;


            Solve();

            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                SetDepth(1);
                SetParent();

                BuildCentTree(1);

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < k; i++)
                {

                    int op = ReadInt();
                    int idx = ReadInt();

                    if (op == 1) Update(idx);
                    else Query(idx);
                }

                sr.Close();
                sw.Close();
            }

            void Update(int _idx)
            {

                color[_idx] = true;
                int cur = _idx;
                while(cur > 0)
                {

                    int dis = GetDis(cur, _idx);
					if (dis < nTc[cur]) nTc[cur] = dis;
                    if (cur == tree[cur]) break;
                    cur = tree[cur];
                }
            }

            void Query(int _idx)
            {

                int ret = INF;
                int cur = _idx;

                while(cur > 0)
                {

                    int dis = GetDis(cur, _idx);
                    ret = Math.Min(ret, dis + nTc[cur]);

                    if (cur == tree[cur]) break;
                    cur = tree[cur];
                }

                if (ret == INF) ret = -1;

                sw.Write($"{ret}\n");
            }

            int LCA(int _a, int _b)
            {

                if (depth[_a] < depth[_b])
                {

                    int temp = _a;
                    _a = _b;
                    _b = temp;
                }

                int diff = depth[_a] - depth[_b];

                for (int i = 0; 0 < diff; i++)
                {

                    if ((diff & 1) == 1) _a = parent[i][_a];
                    diff >>= 1;
                }

                if (_a == _b) return _a;

                for (int i = LOG - 1; i >= 0; i--)
                {

                    if ((parent[i][_a] ^ parent[i][_b]) == 0) continue;
                    _a = parent[i][_a];
                    _b = parent[i][_b];
                }

                return parent[0][_a];
            }

            int GetDis(int _a, int _b)
            {

                return depth[_a] + depth[_b] - 2 * depth[LCA(_a, _b)];
            }

            void SetDepth(int _cur, int _before = 0)
            {

                parent[0][_cur] = _before;

                for (int i = 0; i < line[_cur].Count; i++)
                {

                    int next = line[_cur][i];
                    if (next == _before) continue;
                    depth[next] = depth[_cur] + 1;
                    SetDepth(next, _cur);
                }
            }

            void SetParent()
            {

                for (int i = 1; i < 17; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        parent[i][j] = parent[i - 1][parent[i - 1][j]];
                    }
                }
            }

            int GetSize(int _cur, int _before = 0)
            {

                size[_cur] = 1;

                for (int i = 0; i < line[_cur].Count; i++)
                {

                    int next = line[_cur][i];
                    if (_before == next || visit[next]) continue;

                    size[_cur] += GetSize(next, _cur);
                }

                return size[_cur];
            }

            int GetCent(int _cur, int _cap, int _before = 0)
            {

                for (int i = 0; i < line[_cur].Count; i++)
                {

                    int next = line[_cur][i];

                    if (next == _before || size[next] * 2 <= _cap || visit[next]) continue;

                    return GetCent(next, _cap, _cur);
                }

                return _cur;
            }

            void BuildCentTree(int _cur, int _before = 0)
            {

                int size = GetSize(_cur);
                int cent = GetCent(_cur, size);

                visit[cent] = true;
                tree[cent] = _before;

                for (int i = 0; i < line[cent].Count; i++)
                {

                    int next = line[cent][i];
                    if (visit[next]) continue;

                    BuildCentTree(next, cent);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();


                size = new int[n + 1];
                tree = new int[n + 1];

                depth = new int[n + 1];
                visit = new bool[n + 1];

                parent = new int[LOG][];
                for (int i = 0; i < LOG; i++)
                {

                    parent[i] = new int[n + 1];
                }

                line = new List<int>[n + 1];

                nTc = new int[n + 1];
				Array.Fill(nTc, INF);
                color = new bool[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                for (int i = 2; i <= n; i++)
                {

                    int f = ReadInt();
                    line[i].Add(f);
                    line[f].Add(i);
                }
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
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.StringTokenizer;
import java.util.TreeMap;

public class Main {
	private static final int INF = Integer.MAX_VALUE;
	private static final char FIRST = '1';
	
	private static final class Node {
		int idx;
		Node next;
		
		Node(int idx, Node next) {
			this.idx = idx;
			this.next = next;
		}
	}
	
	private static int logN;
	private static int[] ct;
	private static int[] depth;
	private static int[] subSize;
	private static int[] minDist;
	private static int[][] dp;
	private static boolean[] visited;
	private static Node[] adj;
	
	private static final void dfs(int curr, int parent) {
		Node child;
		
		dp[0][curr] = parent;
		depth[curr] = depth[parent] + 1;
		for (child = adj[curr]; child != null; child = child.next) {
			if (child.idx == parent) {
				continue;
			}
			dfs(child.idx, curr);
		}
	}
	
	private static int getSize(int curr, int parent) {
		Node child;
		
		subSize[curr] = 1;
		for (child = adj[curr]; child != null; child = child.next) {
			if (visited[child.idx] || child.idx == parent) {
				continue;
			}
			subSize[curr] += getSize(child.idx, curr);
		}
		return subSize[curr];
	}
	
	private static final int getCentroid(int curr, int parent, int threshold) {
		Node child;
		
		for (child = adj[curr]; child != null; child = child.next) {
			if (visited[child.idx] || child.idx == parent) {
				continue;
			}
			if (subSize[child.idx] > threshold) {
				return getCentroid(child.idx, curr, threshold);
			}
		}
		return curr;
	}
	
	private static final void buildCentroidTree(int curr, int parent) {
		int centroid;
		Node child;
		
		centroid = getCentroid(curr, -1, getSize(curr, -1) >> 1);
		ct[centroid] = parent;
		visited[centroid] = true;
		minDist[centroid] = INF;
		for (child = adj[centroid]; child != null; child = child.next) {
			if (visited[child.idx]) {
				continue;
			}
			buildCentroidTree(child.idx, centroid);
		}
	}
	
	private static final int lca(int u, int v) {
		int i;
		int temp;
		int diff;
		
		if (depth[u] < depth[v]) {
			temp = u;
			u = v;
			v = temp;
		}
		for (diff = depth[u] - depth[v], i = 0; diff != 0; diff >>= 1, i++) {
			if ((diff & 1) == 1) {
				u = dp[i][u];
			}
		}
		if (u == v) {
			return u;
		}
		for (i = logN; i >= 0; i--) {
			if (dp[i][u] != dp[i][v]) {
				u = dp[i][u];
				v = dp[i][v];
			}
		}
		return dp[0][u];
	}
	
	private static final int getDist(int u, int v) {
		return depth[u] + depth[v] - (depth[lca(u, v)] << 1);
	}
	
	private static final void update(int v) {
		int i;
		int dist;
		int distCnt;
		
		for (i = v; i != 0; i = ct[i]) {
			minDist[i] = Math.min(minDist[i], getDist(i, v));
		}
	}
	
	private static final int query(int v) {
		int i;
		int ret;
		
		ret = INF;
		for (i = v; i != 0; i = ct[i]) {
			if (minDist[i] != INF) {
				ret = Math.min(ret, getDist(i, v) + minDist[i]);
			}
		}
		return ret == INF ? -1 : ret;
	}
	
	public static void main(String[] args) throws IOException {
		int n;
		int q;
		int a;
		int i;
		int j;
		StringBuilder sb;
		BufferedReader br;
		StringTokenizer st;
		
		br = new BufferedReader(new InputStreamReader(System.in));
        st = new StringTokenizer(br.readLine());
		n = Integer.parseInt(st.nextToken());
        q = Integer.parseInt(st.nextToken());
		adj = new Node[n + 1];
        st = new StringTokenizer(br.readLine());
		for (i = 2; i <= n; i++) {
			a = Integer.parseInt(st.nextToken());
			adj[a] = new Node(i, adj[a]);
			adj[i] = new Node(a, adj[i]);
		}
		logN = (int) Math.ceil(Math.log(n) / Math.log(2));
		dp = new int[logN + 1][n + 1];
		depth = new int[n + 1];
		dfs(1, 0);
		for (i = 1; i <= logN; i++) {
			for (j = 1; j <= n; j++) {
				dp[i][j] = dp[i - 1][dp[i - 1][j]];
			}
		}
		ct = new int[n + 1];
		subSize = new int[n + 1];
		visited = new boolean[n + 1];
		minDist = new int[n + 1];
		buildCentroidTree(1, 0);
		sb = new StringBuilder();
		for (i = 0; i < q; i++) {
			st = new StringTokenizer(br.readLine());
			if (st.nextToken().charAt(0) == FIRST) {
				update(Integer.parseInt(st.nextToken()));
			} else {
				sb.append(query(Integer.parseInt(st.nextToken()))).append('\n');
			}
		}
		System.out.print(sb);
	}
}

#elif other2
import sys
input = sys.stdin.readline
sys.setrecursionlimit(200000)


N,M = map(int,input().split())
parent = [0,0]+ list(map(int,input().split()))
sonnum = [0]*(N+1)
depth = [0]*(N+1)

sons = [ set() for _ in range(N+1)]
for i in range(1,N+1):
    sons[ parent[i] ] .add(i)

def upson(node = 1, d=1):
    sonnum[node] = 1
    depth[node] = d

    for i in sons[node]:
        sonnum[node] += upson(i,d+1)

    return sonnum[node]

centp = [0]*(N+1)
upson()

def centing(node,p=0, ): #p는 이전센트

    snum = sonnum[node] #이번 서브트리 노드 수

    cent = node
    while True:

        for i in sons[cent]:
            if sonnum[i] > snum//2:
                

                sons[cent].remove(i)
                sons[i].add(cent)
                
                sonnum[cent] -= sonnum[i]
                sonnum[i] = snum
                cent = i
                break
        else:
            break

    centp[cent] = p
    for i in sons[cent]:
        centing(i,cent)

centing(1)

parent = [ parent]
Max = 1
while Max:
    Max = 0
    cnt = []
    for i in range(N+1):
        pp = parent[-1][parent[-1][i]]
        cnt.append(pp)
        Max = max(Max,pp)
    if Max:
        parent.append(cnt)
LP = len(parent)
def LCA(a,b):
    if depth[a]>depth[b]:
        a,b = b,a #b가 더 깊음
    diff = depth[b]-depth[a]
    for i in range(LP):
        if diff & (1<<i):
            b = parent[i][b]

    for i in range(LP-1,-1,-1):
        if parent[i][a] != parent[i][b]:
            a,b = parent[i][a], parent[i][b]
    if a!=b:
        return parent[0][a]
    return a

def dist(a,b):
    lca = LCA(a,b)
    return depth[a]+depth[b]- 2*depth[lca]

Max = 200000
Violin = [Max]*(N+1)
for _ in range(M):
    a,b = map(int,input().split())
    if a== 1:
        cur = b
        while cur:
            Violin[cur] = min(Violin[cur],dist(cur,b))
            cur = centp[cur]
        
    else:
        rst = Max
        cur = b
        while cur:
            rst = min(rst, Violin[cur]+dist(cur,b))
            cur = centp[cur]

        if rst == Max:
            print(-1)
        else:
            print(rst)
        
        
    

#elif other3
// #include <bits/stdc++.h>
using namespace std;

int dep[100100];
int dfn[100100];
int cnter = 1;
int sz[100100];
bool chk[100100];
int n, q;
int ch[100100];
int parent[100100];
int centpar[100100];
int mini[100100];
vector<int> adj[100001];

int dfs(int now, int prv) {
	sz[now] = 1;
	for (auto& nxt : adj[now]) {
		if (nxt == prv)continue;
		sz[now] += dfs(nxt, now);
		if (sz[nxt] > sz[adj[now][0]])swap(nxt, adj[now][0]);
	}
	return sz[now];
}

void hld(int now, int prv, int depth) {
	dep[now] = depth;
	dfn[now] = cnter++;
	parent[now] = prv;
	for (auto& nxt : adj[now]) {
		if (nxt == prv)continue;
		ch[nxt] = (nxt == adj[now][0] ? ch[now] : nxt);
		hld(nxt, now, depth + 1);
	}
}

int lca(int a, int b) {
	while (1) {
		if (dfn[a] < dfn[b])swap(a, b);
		if (ch[a] == ch[b])break;
		a = parent[ch[a]];
	}
	return b;
}

int distance(int a, int b) {
	int k = lca(a, b);
	return dep[a] + dep[b] - 2 * dep[k];
}

int dfsSz(int now, int prv) {
	sz[now] = 1;
	for (auto nxt : adj[now]) {
		if (nxt == prv || chk[nxt])continue;
		sz[now] += dfsSz(nxt, now);
	}
	return sz[now];
}

int dfsCentroid(int now, int prv, int size) {
	for (auto& nxt : adj[now]) {
		if (nxt == prv || chk[nxt])continue;
		if (sz[nxt] > size)return dfsCentroid(nxt, now, size);
	}
	return now;
}

void dnc(int cur, int prv) {
	int size = dfsSz(cur, -1);
	cur = dfsCentroid(cur, -1, size/2);
	chk[cur] = true;
	centpar[cur] = prv;
	for (auto& nxt : adj[cur]) {
		if (chk[nxt])continue;
		dnc(nxt, cur);
	}
}


void game() {
	int c, v;
	while (q--) {
		cin >> c >> v;
		if (c == 1) {//업데이트
			mini[v] = 0;
			int nxt = centpar[v];
			while (nxt) {
				mini[nxt] = min(mini[nxt], distance(nxt, v));
				nxt = centpar[nxt];
			}
		}
		else {//쿼리
			int ans = mini[v];
			int nxt = centpar[v];
			while (nxt) {
				ans = min(ans, distance(nxt, v) + mini[nxt]);
				nxt = centpar[nxt];
				//cout << nxt << endl;
			}
			cout << (ans == 987654 ? -1 : ans) << '\n';
		}
	}
}

void scan() {
	ios::sync_with_stdio(0); cin.tie(0); cout.tie(0);
	cin >> n >> q;
	for (int i = 2; i <= n; ++i) {
		int p; cin >> p;
		adj[p].push_back(i);
		adj[i].push_back(p);
	}
}

void hyosung() {
	scan();
	ch[1] = 1;
	dfs(1, 0);
	hld(1, 0, 1);
	for (int i = 1; i <= n; ++i)mini[i] = 987654;
	dnc(1, 0);
	game();
}

int main() {
	hyosung();
}
#elif other4
// #include <cstdio>
// #include <algorithm>
// #include <cstring>
// #include <cmath>
// #include <vector>
// #include <queue>
// #include <stack>
// #include <map>
// #include <set>
// #include <climits>
// #include <unordered_set>
// #include <unordered_map>
// #include <cassert>
// #include <iostream>
// #include <string>
// #define ll long long
// #define pll pair<ll,ll>
// #define pii pair<int,int>
// #define pci pair<char,int>
// #define pli pair<ll,int>
// #define pil pair<int,ll>
// #define pdi pair<double,int>
// #define mod 998244353
// #define mod1 1000000009
// #define mod2 1000000021
// #define INF 1987654321
// #define MAX 1000000000
long double PI = 3.141592653589793238462643383279502884197;
using namespace std;

/* 🐣🐥 */
vector<int> vec[100001];
int dist[100001][20], ch[100001], pa[100001], de[100001];
int mn[100001];
bool use[100001];
int getSize(int cur, int p) {
	ch[cur] = 1;
	for (int v : vec[cur]) {
		if (v != p && !use[v]) ch[cur] += getSize(v, cur);
	}
	return ch[cur];
}
int getNode(int cur,int p,int sz) {
	for (int v : vec[cur]) {
		if (v != p && !use[v] && ch[v] > sz)
			return getNode(v, cur, sz);
	}
	return cur;
}
void dfs(int cur, int dep, int dd, int p) {
	dist[cur][dep] = dd;
	for (int v : vec[cur]) {
		if (!use[v] && p != v)
			dfs(v, dep, dd + 1, cur);
	}
}
int centroid(int cur, int dep) {
	int nx = getNode(cur, cur, getSize(cur, cur) / 2);
	use[nx] = true;
	de[nx] = dep;
	for (int v : vec[nx]) {
		if (use[v]) continue;
		dfs(v, dep, 1, v);
		pa[centroid(v, dep + 1)] = nx;
	}
	return nx;
}
void update(int cur) {
	int c = cur;
	for (int i = cur, dd = de[cur];dd >= 0; dd--, c = pa[c]) {
		mn[c] = min(mn[c], dist[cur][dd]);
	}
}
int getAns(int cur) {
	int c = cur, ret = INF;
	for (int i = cur, dd = de[cur]; dd >= 0; dd--, c = pa[c]) {
		ret = min(ret, mn[c] + dist[cur][dd]);
	}
	return ret == INF ? -1 : ret;
}
int main() {
	int n, q, x, cm;
	scanf("%d %d", &n, &q);
	fill(mn, mn + n + 1, INF);
	for (int i = 2; i <= n; i++) {
		scanf("%d", &x);
		vec[i].push_back(x);
		vec[x].push_back(i);
	}
	centroid(1, 0);
	while (q--) {
		scanf("%d %d", &cm, &x);
		if (cm == 1) {
			update(x);
		}
		else if (cm == 2) {
			printf("%d\n", getAns(x));
		}
	}

	return 0;
}
#endif

#if other
// # include <bits/stdc++.h>
using namespace std;
// #define rep(i, a, b) for(int i = a; i < (b); ++i)
// #define all(x) begin(x), end(x)
// #define sz(x) (int)(x).size()
typedef long long ll;
typedef pair<int, int> pii;
typedef vector<int> vi;
template<typename C,typename T=typename enable_if<!is_same<C,string>::value,typename C::value_type>::type>
ostream& operator<<(ostream&os,const C&v){os<<"[";bool f=1;for(const T&x:v){if(!f)os<<", ";os<<x;f=0;}return os<<"]";}
template<typename T1,typename T2>
ostream& operator<<(ostream&os,const pair<T1,T2>&p){return os<<"("<<p.first<<", "<<p.second<<")";}
// #define nl "\n"
// # ifndef DEBUG_393939
ostream cnull(nullptr);
// #define cerr cnull
static const int __39 = [](){ios::sync_with_stdio(false);cin.tie(nullptr);cout.tie(nullptr);return 39;}();
// #endif
    template<class Fun>
class y_combinator_result
    {
        Fun fun_;
        public:
    template<class T>
    explicit y_combinator_result(T&& fun): fun_(std::forward<T>(fun)) { }

        template<class ...Args>
    decltype(auto) operator() (Args&& ...args) {
        return fun_(std::ref (*this), std::forward<Args>(args)...);
    }
};
template <class Fun>
decltype(auto) y_combinator(Fun && fun) {
    return y_combinator_result<std::decay_t<Fun>>(std::forward<Fun>(fun));
}

const int inf = 0x3f3f3f3f;

int main()
{
    int n, q;
    cin >> n >> q;
    set<int> dp;
    // it's always a line wtf
    rep(i, 1, n) {
        int u;
        cin >> u;
        assert(u == i);
    }
    rep(qi, 0, q) {
        int op, u;
        cin >> op >> u;
        if (op == 1)
        {
            dp.insert(u);
        }
        else if (op == 2)
        {
            int res = inf;
            auto it = dp.lower_bound(u);
            if (it != dp.end())
            {
                res = min(res, abs(*it - u));
            }
            if (it != dp.begin())
            {
                --it;
                res = min(res, abs(*it - u));
            }
            cout << (res < inf ? res : -1) << nl;
        }
        else assert(false);
    }
    return 0;
}

#endif
}
