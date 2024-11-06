using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 27
이름 : 배성훈
내용 : 박테리아 (Small, Large)
    문제번호 : 12427번, 12428번

    BFS, 이분 매칭 문제다
    아이디어는 다음과 같다
    우선 층마다 놓을 수 있는 공간을 모두 찾는다
    그리고 위 아래 층이므로 홀수층과 짝수층으로 분리한 뒤
    위 아래로 인접한 층에 한해 간선을 이었다

    이후 해당 간선으로 홀수층과 짝수층을 최대한 이분매칭 시키면
    두 층이 양립할 수 없는 경우가 최대한 나온다

    그래서 해당 개수를 제외하면 최대 경우의 수가 된다

    그리고 최대 층은 50층이고, 사이즈는 400이다
    한 층에서 최대 200개의 독립된 공간이 나올 수 있다 (체스판 처럼 .x를 배치)
    그래서 홀수층의 최대 공간은 5000개 이고, 짝수층 역시 마찬가지로 5000개이다

    v가 크므로 이분 매칭의 시간 복잡도 O(V * E)를 고려해 복잡도가 O(rootV * E)인
    호프크로프트 카프 알고리즘으로 풀었다

    이렇게 제출하니 80ms(Small), 124ms(Large)에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0730
    {

        static void Main730(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[][][] board;
            int row, col, floor;
            Queue<(int r, int c)> pos;
            int[] dirR, dirC;

            int idx1;
            int idx2;

            HashSet<int>[] line;

            int[] A, B, lvl;
            bool[] visit;
            Queue<int> q;

            int ret;

            Solve();

            void Solve()
            {

                Init();
                int test = ReadInt();

                for (int i = 1; i <= test; i++)
                {

                    Input();

                    SetBoard();

                    LinkLine();

                    Match();

                    sw.Write($"Case #{i}: {idx1 + idx2 - ret}\n");
                }

                sw.Close();
                sr.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                board = new int[50][][];

                for (int k = 0; k < 50; k++)
                {

                    board[k] = new int[50][];
                    for (int r = 0; r < 50; r++)
                    {

                        board[k][r] = new int[50];
                    }
                }

                pos = new(2500);

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                line = new HashSet<int>[5_001];
                for (int i = 1; i <= 5_000; i++)
                {

                    line[i] = new();
                }

                A = new int[5_001];
                B = new int[5_001];
                lvl = new int[5_001];
                visit = new bool[5_001];

                q = new(5_000);
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();
                floor = ReadInt();

                for (int k = 0; k < floor; k++)
                {

                    for (int n = 0; n < row; n++)
                    {

                        for (int m = 0; m < col; m++)
                        {

                            board[k][n][m] = sr.Read() == '#' ? -1 : 0;
                        }

                        if (sr.Read() == '\r') sr.Read();
                    }
                }
            }

            void SetBoard()
            {

                idx1 = 0;
                for (int k = 0; k < floor; k += 2)
                {

                    for (int n = 0; n < row; n++)
                    {

                        for (int m = 0; m < col; m++)
                        {

                            if (board[k][n][m] != 0) continue;
                            board[k][n][m] = ++idx1;

                            pos.Enqueue((n, m));

                            while(pos.Count > 0)
                            {

                                (int r, int c) node = pos.Dequeue();

                                for (int i = 0; i < 4; i++)
                                {

                                    int nextR = node.r + dirR[i];
                                    int nextC = node.c + dirC[i];

                                    if (ChkInvalidPos(nextR, nextC) || board[k][nextR][nextC] != 0) continue;
                                    board[k][nextR][nextC] = idx1;
                                    pos.Enqueue((nextR, nextC));
                                }
                            }
                        }
                    }
                }

                idx2 = 0;
                for (int k = 1; k < floor; k += 2)
                {

                    for (int n = 0; n < row; n++)
                    {

                        for (int m = 0; m < col; m++)
                        {

                            if (board[k][n][m] != 0) continue;
                            board[k][n][m] = ++idx2;

                            pos.Enqueue((n, m));

                            while (pos.Count > 0)
                            {

                                (int r, int c) node = pos.Dequeue();

                                for (int i = 0; i < 4; i++)
                                {

                                    int nextR = node.r + dirR[i];
                                    int nextC = node.c + dirC[i];

                                    if (ChkInvalidPos(nextR, nextC) || board[k][nextR][nextC] != 0) continue;
                                    board[k][nextR][nextC] = idx2;
                                    pos.Enqueue((nextR, nextC));
                                }
                            }
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void LinkLine()
            {

                for (int i = 1; i <= idx1; i++)
                {

                    line[i].Clear();
                }

                for (int k = 1; k < floor; k += 2)
                {

                    for (int n = 0; n < row; n++)
                    {

                        for (int m = 0; m < col; m++)
                        {

                            int t = board[k][n][m];
                            if (t == -1) continue;

                            int f1 = board[k - 1][n][m];
                            int f2;
                            if (k == floor - 1) f2 = -1;
                            else f2 = board[k + 1][n][m];

                            if (f1 != -1) line[f1].Add(t);
                            if (f2 != -1) line[f2].Add(t);
                        }
                    }
                }
            }

            void Match()
            {

                Array.Fill(A, 0, 1, idx1);
                Array.Fill(B, 0, 1, idx2);
                Array.Fill(visit, false, 1, idx1);

                ret = 0;
                while (true)
                {

                    BFS();

                    int match = 0;
                    for (int a = 1; a <= idx1; a++)
                    {

                        if (!visit[a] && DFS(a)) match++;
                    }

                    if (match == 0) break;
                    ret += match;
                }
            }

            void BFS()
            {

                int INF = 1_000_000;
                for (int i = 1; i <= idx1; i++)
                {

                    if (!visit[i])
                    {

                        lvl[i] = 0;
                        q.Enqueue(i);
                    }
                    else lvl[i] = INF;
                }

                while(q.Count > 0)
                {

                    int a = q.Dequeue();

                    foreach(int b in line[a])
                    {

                        if (B[b] != 0 && lvl[B[b]] == INF)
                        {

                            lvl[B[b]] = lvl[a] + 1;
                            q.Enqueue(B[b]);
                        }
                    }
                }
            }

            bool DFS(int _a)
            {

                foreach(int b in line[_a])
                {

                    if (B[b] == 0 || lvl[B[b]] == lvl[_a] + 1 && DFS(B[b]))
                    {

                        visit[_a] = true;
                        B[b] = _a;
                        A[_a] = b;
                        return true;
                    }
                }

                return false;
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
// #include <bits/stdc++.h>

using namespace std;
const int INF = 1e9;

struct HopcroftKarp {
    int n;
    vector<int> a, b, lv, work;
    vector<vector<int>> g;

    explicit HopcroftKarp(int n) : n(n) {
        a.assign(n + 1, -1);
        b.assign(n + 1, -1);
        lv.assign(n + 1, 0);
        g.resize(n + 1);
    }

    void addEdge(int x, int y) {
        g[x].push_back(y);
    }

    void bfs() {
        queue<int> q;
        for (int i = 1; i <= n; ++i) {
            if (a[i] == -1) {
                lv[i] = 0;
                q.push(i);
            } else {
                lv[i] = INF;
            }
        }
        while (!q.empty()) {
            int here = q.front();
            q.pop();
            for (auto there: g[here]) {
                if (b[there] != -1 && lv[b[there]] == INF) {
                    lv[b[there]] = lv[here] + 1;
                    q.push(b[there]);
                }
            }
        }
    }

    bool dfs(int here) {
        for (int &i = work[here]; i < g[here].size(); i++) {
            int there = g[here][i];
            if (b[there] == -1 || lv[b[there]] == lv[here] + 1 && dfs(b[there])) {
                a[here] = there;
                b[there] = here;
                return true;
            }
        }
        return false;
    }

    int run() {
        int ret = 0;
        while (1) {
            work.assign(n + 1, 0);
            bfs();
            int flow = 0;
            for (int i = 1; i <= n; ++i) {
                if (a[i] == -1 && dfs(i))flow++;
            }
            if (!flow) break;
            ret += flow;
        }
        return ret;
    }

};

const int dx[4] = {0, 1, 0, -1};
const int dy[4] = {1, 0, -1, 0};
int N, M, K;
string s;
int grid[51][21][21];
set<int> st[100000];

void dfs(int k, int x, int y, int color) {
    grid[k][x][y] = color;
    for (int i = 0; i < 4; ++i) {
        int nx = x + dx[i], ny = y + dy[i];
        if (nx < 0 || nx >= N || ny < 0 || ny >= M || grid[k][nx][ny] != 0) continue;
        dfs(k, nx, ny, color);
    }
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int T;
    cin >> T;
    for (int tc = 1; tc <= T; ++tc) {

        cin >> N >> M >> K;
        for (int k = 0; k < K; ++k) {
            for (int i = 0; i < N; ++i) {
                cin >> s;
                for (int j = 0; j < M; ++j) {
                    grid[k][i][j] = s[j] == '.' ? 0 : -1;
                }
            }
        }
        int color = 1;
        for (int k = 0; k < K; ++k) {
            for (int i = 0; i < N; ++i) {
                for (int j = 0; j < M; ++j) {
                    if (grid[k][i][j] == 0) dfs(k, i, j, color++);
                }
            }
        }

        for (int k = 0; k < K; k += 2) {
            for (int i = 0; i < N; ++i) {
                for (int j = 0; j < M; ++j) {
                    if (grid[k][i][j] != -1) {
                        if (k && grid[k - 1][i][j] != -1) st[grid[k][i][j]].insert(grid[k - 1][i][j]);
                        if (k < K - 1 && grid[k + 1][i][j] != -1) st[grid[k][i][j]].insert(grid[k + 1][i][j]);
                    }
                }
            }
        }
        HopcroftKarp matching(color);
        for (int i = 1; i < color; ++i) {
            for (auto x: st[i]) matching.addEdge(i, x);
        }
        cout << "Case #" << tc << ": " << color - matching.run() - 1 << '\n';

        for (int i = 1; i < color; ++i) {
            st[i].clear();
        }
    }
}

#elif other2
// #include <iostream>
// #include <set>
// #include <cstring>
// #include <queue>
using namespace std;

char board[52][22][22];
bool visit[52][22][22], check[20001];
set<int> adj[20001];
int A[20001], B[20001], component[52][22][22];
int dx[4] = { -1, 1, 0, 0 }, dy[4] = { 0, 0, -1, 1 };

bool dfs(int now){
	check[now] = true;
	for (int next : adj[now]){
		if (!B[next]){
			A[now] = next, B[next] = now;
			return true;
		}
		else if (!check[B[next]] && dfs(B[next])){
			A[now] = next, B[next] = now;
			return true;
		}
	}
	return false;
}
int main(void){
	cin.tie(0);
	ios::sync_with_stdio(0);

	int testcase; cin >> testcase;
	for (int t = 1; t <= testcase; t++){
		memset(board, 0, sizeof(board));
		memset(visit, 0, sizeof(visit));
		memset(A, 0, sizeof(A));
		memset(B, 0, sizeof(B));

		int m, n, num, idx = 0; cin >> m >> n >> num;
		for (int k = 1; k <= num; k++)
		for (int i = 1; i <= m; i++)
		for (int j = 1; j <= n; j++)
			cin >> board[k][i][j];

		for (int k = 1; k <= num; k++)
		for (int i = 1; i <= m; i++)
		for (int j = 1; j <= n; j++)
		if (!visit[k][i][j] && board[k][i][j] == '.'){
			idx++;
			queue<pair<int, int>> q;
			visit[k][i][j] = true;
			component[k][i][j] = idx;
			q.push(make_pair(i, j));

			while (!q.empty()){
				int cx = q.front().first;
				int cy = q.front().second;
				q.pop();

				for (int c = 0; c < 4; c++){
					int nx = cx + dx[c], ny = cy + dy[c];
					if (!visit[k][nx][ny] && board[k][nx][ny] == '.'){
						visit[k][nx][ny] = true;
						component[k][nx][ny] = idx;
						q.push(make_pair(nx, ny));
					}
				}
			}
		}
		
		for (int k = 2; k <= num; k += 2)
		for (int i = 1; i <= m; i++)
		for (int j = 1; j <= n; j++)
		if (board[k][i][j] == '.'){
			int A = component[k][i][j];
			if (board[k - 1][i][j] == '.'){
				int B = component[k - 1][i][j];
				adj[A].insert(B);
			}
			if (board[k + 1][i][j] == '.'){
				int C = component[k + 1][i][j];
				adj[A].insert(C);
			}
		}

		int answer = idx;
		for (int i = 1; i <= idx; i++)
		if (!A[i]){
			memset(check, 0, sizeof(check));
			answer -= dfs(i);
		}

		cout << "Case #" << t << ": " << answer << '\n';
		for (int i = 1; i <= idx; i++) adj[i].clear();
	}

	return 0;
}
#elif other3
// #include <bits/stdc++.h>
// #define sz(v) ((int)(v).size())
// #define all(v) (v).begin(), (v).end()
using namespace std;
typedef long long lint;
typedef pair<int, int> pi;
const int mod = 1e9 + 7;

template<class flow_t> struct HLPP {
    struct Edge {
        int to, inv;
        flow_t rem, cap;
    };

    vector<basic_string<Edge>> G;
    vector<flow_t> excess;
    vector<int> hei, arc, prv, nxt, act, bot;
    queue<int> Q;
    int n, high, cut, work;

    // Initialize for n vertices
    HLPP(int k) : G(k) {}

    int addEdge(int u, int v,
                flow_t cap, flow_t rcap = 0) {
        G[u].push_back({ v, sz(G[v]), cap, cap });
        G[v].push_back({ u, sz(G[u])-1, rcap, rcap });
        return sz(G[u])-1;
    }

    void raise(int v, int h) {
        prv[nxt[prv[v]] = nxt[v]] = prv[v];
        hei[v] = h;
        if (excess[v] > 0) {
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
        auto f = min(excess[v], e.rem);
        if (f > 0) {
            if (z && !excess[e.to]) {
                bot[e.to] = act[hei[e.to]];
                act[hei[e.to]] = e.to;
            }
            e.rem -= f; G[e.to][e.inv].rem += f;
            excess[v] -= f; excess[e.to] += f;
        }
    }

    void discharge(int v) {
        int h = n*2, k = hei[v];

        for(int j = 0; j < sz(G[v]); j++){
            auto& e = G[v][arc[v]];
            if (e.rem) {
                if (k == hei[e.to]+1) {
                    push(v, e, 1);
                    if (excess[v] <= 0) return;
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
        excess.assign(n = sz(G), 0);
        arc.assign(n, 0);
        prv.assign(n*3, 0);
        nxt.assign(n*3, 0);
        bot.assign(n, 0);
        for(auto &e : G[src]){
            excess[src] = e.rem, push(src, e, 0);
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

        return excess[dst];
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

string s[55][25];
const int dx[4] = {1, 0, -1, 0};
const int dy[4] = {0, 1, 0, -1};
int vis[55][25][25];

void dfs(int x, int y, int f, int col){
	if(vis[f][x][y]) return;
	vis[f][x][y] = col;
	for(int i = 0; i < 4; i++){
		int nx = x + dx[i];
		int ny = y + dy[i];
		if(nx < 0 || ny >= sz(s[f][nx]) || ny < 0) continue;
		if(s[f][nx][ny] == '#') continue;
		dfs(nx, ny, f, col);
	}
}

int solve(){
	memset(vis, 0, sizeof(vis));
	int n, m, k;
	cin >> n >> m >> k;
	for(int i = 0; i < k; i++){
		for(int j = 0; j < n; j++){
			cin >> s[i][j];
		}
	}
	int col = 0;
	vector<int> vect[2];
	for(int i = 0; i < k; i++){
		for(int j = 0; j < n; j++){
			for(int x = 0; x < m; x++){
				if(s[i][j][x] == '#') continue;
				if(vis[i][j][x]) continue;
				dfs(j, x, i, ++col);
				vect[i % 2].push_back(col);
			}
		}
	}
	HLPP<int> mf(col + 2);
	for(auto &i : vect[0]) mf.addEdge(0, i, 1);
	for(auto &i : vect[1]) mf.addEdge(i, col+1, 1);
	for(int i = 0; i+1 < k; i++){
		for(int j = 0; j < n; j++){
			for(int x = 0; x < m; x++){
				if(vis[i][j][x] && vis[i+1][j][x]){
					if(i % 2 == 0) mf.addEdge(vis[i][j][x], vis[i+1][j][x], 1);
					if(i % 2 == 1) mf.addEdge(vis[i+1][j][x], vis[i][j][x], 1);
				}
			}
		}
	}
	for(int i = 0; i < k; i++){
		for(int j = 0; j < n; j++){
			s[i][j].clear();
		}
	}
	return col - mf.flow(0, col + 1);
}

int main(){
	ios_base::sync_with_stdio(0);
	cin.tie(0);
	cout.tie(0);
	int tc; cin >> tc;
	for(int i = 1; i <= tc; i++){
		cout << "Case #" << i << ": " << solve() << "\n";
	}
}

#endif
}
