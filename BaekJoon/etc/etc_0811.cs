using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 13
이름 : 배성훈
내용 : 트리와 쿼리 5
    문제번호 : 13514번

    센트로이드 분할 문제다
    센트로이드 분할의 의미를 약간 이해한거 같다
    
    아이디어는 다음과 같다
    센트로이드 트리로 만든다

    여기서 센트로이드 트리란
    정점부터 중심이 되는 노드들로 새롭게 부모 자식 관계로 만든 트리다
    기존 트리와 부모 자식이 일치하지 않는다
    
    현재 노드에 색이 바뀌면 해당 노드로부터 
    센트로이드 트리의 부모로 가면서
    해당 정점과의 거리를 저장한다
    그러면 해당 중심을 지나가는 거리를 찾을 수 있다

    이제 탐색에서는 센트로이드 정점에 저장된 거리가 있는지 확인한다
    그리고 있다면 해당 정점과 현재 정점의 거리와
    저장된 거리의 합의 최소값을 찾으면 된다
    그리고 더짧은 거리를 찾아보기 위해 센트로이드 트리의 부모 노드로 간다

    최소값을 찾는데, 정렬된 딕셔너리 자료구조를 이용했다
    그리고 기본 오름차순이기에 First 메서드로 맨 앞에 있는 노드를 가져올 수 있기 때문이다

    이렇게 제출하니 2초대에 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_0811
    {

        static void Main811(string[] args)
        {

            int INF = 1_000_001;
            int LOG = 17;

            StreamReader sr;
            StreamWriter sw;

            int n;

            List<int>[] line;
            int[][] parent;
            int[] depth;

            int[] size;
            bool[] visit;
            int[] tree;

            bool[] color;
            SortedDictionary<int, int>[] nTc;


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

                int len = ReadInt();
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < len; i++)
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

                color[_idx] = !color[_idx];
                int cur = _idx;
                while (cur > 0)
                {

                    int dis = GetDis(cur, _idx);
                    if (color[_idx])
                    {

                        if (nTc[cur].ContainsKey(dis)) nTc[cur][dis] += 1;
                        else nTc[cur][dis] = 1;
                    }
                    else
                    {

                        if (nTc[cur][dis] == 1) nTc[cur].Remove(dis);
                        else nTc[cur][dis]--;
                    }

                    if (cur == tree[cur]) break;
                    cur = tree[cur];
                }
            }

            void Query(int _idx)
            {

                int ret = INF;
                int cur = _idx;

                while (cur > 0)
                {

                    int dis = GetDis(cur, _idx);
                    if (nTc[cur].Count > 0) ret = Math.Min(ret, dis + nTc[cur].First().Key);

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

                nTc = new SortedDictionary<int, int>[n + 1];
                color = new bool[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                    nTc[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    line[f].Add(b);
                    line[b].Add(f);
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
	private static int[][] dp;
	private static boolean[] white;
	private static boolean[] visited;
	private static Node[] adj;
	private static ArrayList<TreeMap<Integer, Integer>> multiset;
	
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
		multiset.add(new TreeMap<>());
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
		
		white[v] ^= true;
		for (i = v; i != 0; i = ct[i]) {
			dist = getDist(i, v);
			if (white[v]) {
			    multiset.get(i).put(dist, multiset.get(i).getOrDefault(dist, 0) + 1);
			} else {
				distCnt = multiset.get(i).getOrDefault(dist, 0);
			    if (distCnt == 1) {
			        multiset.get(i).remove(dist);
			    } else {
			        multiset.get(i).put(dist, distCnt - 1);
			    }
			}
		}
	}
	
	private static final int query(int v) {
		int i;
		int ret;
		
		ret = INF;
		for (i = v; i != 0; i = ct[i]) {
			if (!multiset.get(i).isEmpty()) {
				ret = Math.min(ret, getDist(i, v) + multiset.get(i).firstKey());
			}
		}
		return ret == INF ? -1 : ret;
	}
	
	public static void main(String[] args) throws IOException {
		int n;
		int m;
		int u;
		int v;
		int i;
		int j;
		StringBuilder sb;
		BufferedReader br;
		StringTokenizer st;
		
		br = new BufferedReader(new InputStreamReader(System.in));
		n = Integer.parseInt(br.readLine());
		adj = new Node[n + 1];
		for (i = 1; i < n; i++) {
			st = new StringTokenizer(br.readLine());
			u = Integer.parseInt(st.nextToken());
			v = Integer.parseInt(st.nextToken());
			adj[u] = new Node(v, adj[u]);
			adj[v] = new Node(u, adj[v]);
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
		multiset = new ArrayList<>(n + 1);
		multiset.add(null);
		buildCentroidTree(1, 0);
		white = new boolean[n + 1];
		sb = new StringBuilder();
		m = Integer.parseInt(br.readLine());
		for (i = 0; i < m; i++) {
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
from heapq import heappop, heappush

input = sys.stdin.readline

N = int(input()) #노드의 수

adj = [ [] for _ in range(N+1)]

for _ in range(N-1):
    a,b = map(int,input().split())
    adj[a].append(b)
    adj[b].append(a)


// #재귀로 돌릴 때 visited 없는 dfs가 가능한데
// #반복문으로도 해보자.

sons = [0]*(N+1)
nidx = [0]*(N+1)
stk= [0,1]
parent = [0] *(N+1)
depth = [0]*(N+1)
depth[1] = 1
cent_p = [0]*(N+1) #센트로이드부모
while stk:
    cur = stk[-1]

    if nidx[cur] == len(adj[cur]):
        sons[cur] += 1 #자신
        stk.pop()

    else:
        nxt = adj[cur][nidx[cur]]
        if nxt == stk[-2]:
            nidx[cur] += 1
        elif sons[nxt]:
            sons[cur] += sons[nxt]
            nidx[cur] += 1
        else:
            stk.append(nxt)
            parent[nxt] = cur
            depth[nxt] = depth[cur] + 1

parent = [parent]
flag = 1
while flag:
    flag = 0
    pcnt = []
    for i in range(N+1):
        pp = parent[-1][parent[-1][i]]
        pcnt.append(pp)
        flag = max(flag,pp)
    if flag:
        parent.append(pcnt)
LP = len(parent)
stk = [1]

while stk:
    cur = stk.pop()
    snum = sons[cur] #이번 서브트리에서 처리할 전체 노드 수
    pcp = cent_p[cur]
    while True:

        for nxt in adj[cur]:
            if sons[nxt]>snum:
                continue
            if sons[nxt]>snum//2:
                sons[cur] -= sons[nxt]
                sons[nxt] = snum
                cent_p[nxt] = pcp
                cur = nxt
                break
        else: #cur이 이번 루트로 당첨 
            break

    
    for nxt in adj[cur]:
        if sons[nxt]>snum:
            continue
        cent_p[nxt] = cur
        stk.append(nxt)

Max = 200000 #최대 거리
is_white = {0}
white = [ [(Max,0)] for _ in range(N+1)] #거리,노드
for _ in range(int(input())):
    a,b = map(int,input().split())
    if a== 1:
        if b in is_white:
            is_white.remove(b)
        else:
            is_white.add(b)
            cur = b
            while cur: #b가 더 깊다.
                dcur, db = cur, b #거리구하기 위한 임시 값
                if depth[cur]>depth[b]:
                    dcur, db = db, dcur
                diff = depth[db] - depth[dcur]
                for i in range(LP):
                    if diff&(1<<i):
                        db = parent[i][db]
                for i in range(LP-1,-1,-1):
                    if parent[i][db] != parent[i][dcur]:
                        db, dcur = parent[i][db], parent[i][dcur]
                if db!= dcur:
                    db = parent[0][db]
                D = depth[b] + depth[cur] - 2*depth[db]

                heappush(white[cur],(D,b))
                cur = cent_p[cur]
    else:
        rst = Max
        cur = b
        while cur:
            dcur, db = cur, b
            if depth[cur]>depth[b]:
                dcur, db = db, dcur
            diff = depth[db] - depth[dcur]
            for i in range(LP):
                if diff&(1<<i):
                    db = parent[i][db]
            for i in range(LP-1,-1,-1):
                if parent[i][db] != parent[i][dcur]:
                    db, dcur = parent[i][db], parent[i][dcur]
            if db != dcur:
                db = parent[0][db]
            D = depth[b] + depth[cur] - 2*depth[db]

            while white[cur][0][1] not in is_white:
                heappop(white[cur])

            rst = min(rst, D+white[cur][0][0])
            cur = cent_p[cur]
        if rst == Max:
            print(-1)
        else:
            print(rst)
            


#elif other3
// #include<bits/stdc++.h>
using namespace std;
using pii = pair<int, int>;
constexpr int MAXN = 100000;
constexpr int INF = INT_MAX;

vector<int> adj[MAXN+1];
int par[MAXN+1], sz[MAXN+1], dep[MAXN+1],
    chNo[MAXN+1], chDep[MAXN+1], chIdx[MAXN+1],
    ct_par[MAXN+1];
bool sel[MAXN+1]={false}, white[MAXN+1]={false};
priority_queue<pii, vector<pii>, greater<pii>> pqs[MAXN+1];

int dfs_hld(int pu, int u) {
    par[u] = pu;
    sz[u] = 1;
    dep[u] = dep[pu] + 1;
    for (int& v : adj[u]) if (v != pu)
        sz[u] += dfs_hld(u, v);
    return sz[u];
}
void hld(int u, int cn, int cd, int ci) {
    chNo[u] = cn;
    chDep[u] = cd;
    chIdx[u] = ci;

    int hv = 0;
    for (int& v : adj[u])
        if (v != par[u] && (!hv || sz[v] > sz[hv]))
            hv = v;
    if (hv)
        hld(hv, cn, cd, ci+1);
    for (int& v : adj[u])
        if (v != par[u] && v != hv)
            hld(v, v, cd+1, 0);
}
int dist(int u, int v) {
    int d = dep[u] + dep[v];
    while (chNo[u] != chNo[v]) {
        if (chDep[u] < chDep[v])
            swap(u, v);
        u = par[chNo[u]];
    }
    int lca = chIdx[u] < chIdx[v] ? u : v;
    return d - (dep[lca]<<1);
}

int dfs_ctd(int pu, int u) {
    sz[u] = 1;
    for (int& v : adj[u])
        if (v != pu && !sel[v])
            sz[u] += dfs_ctd(u, v);
    return sz[u];
}
int ct(int pu, int u, int m) {
    for (int& v : adj[u])
        if (v != pu && !sel[v] && sz[v] > m)
            return ct(u, v, m);
    return u;
}
void ctd(int pc, int u) {
    int n = dfs_ctd(pc, u);
    int c = ct(pc, u, n>>1);
    ct_par[c] = pc;
    sel[c] = true;
    for (int& v : adj[c]) if (!sel[v])
        ctd(c, v);
}

int main() {
    ios::sync_with_stdio(0);
    cin.tie(0); cout.tie(0);
    int N, M, u, v, q, r;

    cin >> N;
    for (int n=1; n<N; n++) {
        cin >> u >> v;
        adj[u].emplace_back(v);
        adj[v].emplace_back(u);
    }
    dfs_hld(0, 1);
    hld(1, 1, 0, 0);
    ctd(0, 1);

    cin >> M;
    while (M--) {
        cin >> q >> u;
        if (q&1) {
            white[u] = !white[u];
            if (white[u]) {
                v = u;
                while (v) {
                    pqs[v].emplace(dist(u, v), u);
                    v = ct_par[v];
                }
            }
        }
        else {
            r = INF;
            v = u;
            while (v) {
                auto& pq = pqs[v];
                while (!pq.empty() && !white[pq.top().second])
                    pq.pop();
                if (!pq.empty())
                    r = min(r, dist(u, v) + pq.top().first);
                v = ct_par[v];
            }
            cout << (r != INF ? r : -1) << '\n';
        }
    }

    return 0;
}

#endif
}
