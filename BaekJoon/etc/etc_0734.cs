using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
날짜 : 2024. 5. 28
이름 : 배성훈
내용 : 나이트
    문제번호 : 3391번

    이분 매칭 문제다
    그냥 접근하면, 시간 초과난다;

    호프크로프트 카프 알고리즘을 썼다고 생각했는데, 잘못사용해왔다;
    여기도 디닉처럼 처리 해줘야한다;

    #if other분을 보고 알게되었다
    이전 껄 이어안가면 시간초과로 터진다;
    TimeOut 코드부분 보면 d[n]유무 차이와 그냥 이분매칭 부분이 있다
    그냥 이분 매칭역시 터진다;
*/

namespace BaekJoon.etc
{
    internal class etc_0734
    {

        static void Main734(string[] args)
        {

            StreamReader sr;

            int[][] board;
            int n;

            int len1, len2;
            List<int>[] line;

            int[] dirR, dirC;
            Queue<int> q;
            int[] A, B, lvl;
            bool[] visit;
            int[] d;
            int ret;
            Solve();

            void Solve()
            {

                Input();

                SetBoard();

                LinkLine();

                Match();

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                board = new int[n][];
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                }

                int m = ReadInt();
                ret = n * n - m;
                for (int i = 0; i < m; i++)
                {

                    int r = ReadInt() - 1;
                    int c = ReadInt() - 1;
                    board[r][c] = -1;
                }

                sr.Close();
            }

            void SetBoard()
            {

                len1 = 0;
                len2 = 0;

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] == -1) continue;
                        if ((r + c) % 2 == 0) board[r][c] = ++len1;
                        else board[r][c] = ++len2;
                    }
                }
            }

            void LinkLine()
            {

                line = new List<int>[len1];

                for (int i = 0; i < len1; i++)
                {

                    line[i] = new();
                }

                dirR = new int[8] { -2, -1, 1, 2, 2, 1, -1, -2 };
                dirC = new int[8] { 1, 2, 2, 1, -1, -2, -2, -1 };

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        if ((r + c) % 2 == 1 || board[r][c] == -1) continue;

                        int f = board[r][c] - 1;
                        for (int i = 0; i < 8; i++)
                        {

                            int nextR = r + dirR[i];
                            int nextC = c + dirC[i];

                            if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] == -1) continue;
                            line[f].Add(board[nextR][nextC] - 1);
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= n || _c >= n) return true;
                return false;
            }

            void Match()
            {

                A = new int[len1];
                d = new int[len1];
                B = new int[len2];
                Array.Fill(A, -1);
                Array.Fill(B, -1);

                q = new(len1);
                lvl = new int[len1];
                visit = new bool[len1];

                while (true)
                {

                    Array.Fill(d, 0);
                    BFS();

                    int match = 0;

                    for (int i = 0; i < len1; i++)
                    {

                        if (!visit[i] && DFS(i))
                            match++;
                    }

                    if (match == 0) break;
                    ret -= match;
                }
            }

            void BFS()
            {

                Array.Fill(lvl, -1);

                for (int i = 0; i < len1; i++)
                {

                    if (visit[i]) continue;
                    lvl[i] = 0;
                    q.Enqueue(i);
                }

                while (q.Count > 0)
                {

                    int a = q.Dequeue();

                    for (int i = 0; i < line[a].Count; i++)
                    {

                        int b = line[a][i];

                        if (B[b] != -1 && lvl[B[b]] == -1)
                        {

                            lvl[B[b]] = lvl[a] + 1;
                            q.Enqueue(B[b]);
                        }
                    }
                }
            }

            bool DFS(int _a)
            {

                for (; d[_a] < line[_a].Count; d[_a]++)
                {

                    int b = line[_a][d[_a]];

                    if (B[b] == -1 || (lvl[B[b]] == lvl[_a] + 1 && DFS(B[b])))
                    {

                        visit[_a] = true;
                        A[_a] = b;
                        B[b] = _a;
                        return true;
                    }
                }

                return false;
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

#if TimeOut

StreamReader sr;

int[][] board;
int n;

int len1, len2;
List<int>[] line;

int[] dirR, dirC;
bool[] visit;
int[] match;

int ret;
Solve();

void Solve()
{

    Input();

    SetBoard();

    LinkLine();

    Match();

    Console.WriteLine(ret);
}

void Input()
{

    sr = new(Console.OpenStandardInput(), bufferSize: 65536);
    n = ReadInt();

    board = new int[n][];
    for (int r = 0; r < n; r++)
    {

        board[r] = new int[n];
    }

    int m = ReadInt();
    ret = n * n - m;
    for (int i = 0; i < m; i++)
    {

        int r = ReadInt() - 1;
        int c = ReadInt() - 1;
        board[r][c] = -1;
    }

    sr.Close();
}

void SetBoard()
{

    len1 = 0;
    len2 = 0;

    for (int r = 0; r < n; r++)
    {

        for (int c = 0; c < n; c++)
        {

            if (board[r][c] == -1) continue;
            if ((r + c) % 2 == 0) board[r][c] = ++len1;
            else board[r][c] = ++len2;
        }
    }
}

void LinkLine()
{

    line = new List<int>[len1];

    for (int i = 0; i < len1; i++)
    {

        line[i] = new();
    }

    dirR = new int[8] { -2, -1, 1, 2, 2, 1, -1, -2 };
    dirC = new int[8] { 1, 2, 2, 1, -1, -2, -2, -1 };

    for (int r = 0; r < n; r++)
    {

        for (int c = 0; c < n; c++)
        {

            if ((r + c) % 2 == 1 || board[r][c] == -1) continue;

            int f = board[r][c] - 1;
            for (int i = 0; i < 8; i++)
            {

                int nextR = r + dirR[i];
                int nextC = c + dirC[i];

                if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] == -1) continue;
                line[f].Add(board[nextR][nextC] - 1);
            }
        }
    }
}

bool ChkInvalidPos(int _r, int _c)
{

    if (_r < 0 || _c < 0 || _r >= n || _c >= n) return true;
    return false;
}

void Match()
{

    visit = new bool[len2];
    match = new int[len2];
    Array.Fill(match, -1);
    for (int i = 0; i < len1; i++)
    {

        Array.Fill(visit, false);
        if (DFS(i)) ret--;
    }

}

bool DFS(int _n)
{

    for (int i = 0; i < line[_n].Count; i++)
    {

        int next = line[_n][i];
        if (visit[next]) continue;
        visit[next] = true;

        if (match[next] == -1 || DFS(match[next]))
        {

            match[next] = _n;
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
#elif TimeOut2
using System;
using System.IO;
using System.Collections.Generic;

int INF = 100_000;

StreamReader sr;

int[][] board;
int n;

int len1, len2;
List<int>[] line;

int[] dirR, dirC;

Queue<int> q;
int[] A, B, lvl;
bool[] visit;

int ret;
Solve();

void Solve()
{

    Input();

    SetBoard();

    LinkLine();

    Match();

    Console.WriteLine(ret);
}

void Input()
{

    sr = new(Console.OpenStandardInput(), bufferSize: 65536);
    n = ReadInt();

    board = new int[n][];
    for (int r = 0; r < n; r++)
    {

        board[r] = new int[n];
    }

    int m = ReadInt();
    ret = n * n - m;
    for (int i = 0; i < m; i++)
    {

        int r = ReadInt() - 1;
        int c = ReadInt() - 1;
        board[r][c] = -1;
    }

    sr.Close();
}

void SetBoard()
{

    len1 = 0;
    len2 = 0;

    for (int r = 0; r < n; r++)
    {

        for (int c = 0; c < n; c++)
        {

            if (board[r][c] == -1) continue;
            if ((r + c) % 2 == 0) board[r][c] = ++len1;
            else board[r][c] = ++len2;
        }
    }
}

void LinkLine()
{

    line = new List<int>[len1];

    for (int i = 0; i < len1; i++)
    {

        line[i] = new();
    }

    dirR = new int[8] { -2, -1, 1, 2, 2, 1, -1, -2 };
    dirC = new int[8] { 1, 2, 2, 1, -1, -2, -2, -1 };

    for (int r = 0; r < n; r++)
    {

        for (int c = 0; c < n; c++)
        {

            if ((r + c) % 2 == 1 || board[r][c] == -1) continue;

            int f = board[r][c] - 1;
            for (int i = 0; i < 8; i++)
            {

                int nextR = r + dirR[i];
                int nextC = c + dirC[i];

                if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] == -1) continue;
                line[f].Add(board[nextR][nextC] - 1);
            }
        }
    }
}

bool ChkInvalidPos(int _r, int _c)
{

    if (_r < 0 || _c < 0 || _r >= n || _c >= n) return true;
    return false;
}

void Match()
{

    A = new int[len1];
    B = new int[len2];
    Array.Fill(A, -1);
    Array.Fill(B, -1);

    q = new(len1);
    lvl = new int[len1];
    visit = new bool[len1];

    while (true)
    {

        BFS();

        int match = 0;

        for (int i = 0; i < len1; i++)
        {

            if (!visit[i] && DFS(i)) match++;
        }

        if (match == 0) break;
        ret -= match;
    }
}

void BFS()
{

    for (int i = 0; i < len1; i++)
    {

        if (visit[i]) lvl[i] = INF;
        else
        {

            lvl[i] = 0;
            q.Enqueue(i);
        }
    }

    while(q.Count > 0)
    {

        int a = q.Dequeue();

        for (int i = 0; i < line[a].Count; i++)
        {

            int b = line[a][i];

            if (B[b] != -1 && lvl[B[b]] == INF)
            {

                lvl[B[b]] = lvl[a] + 1;
                q.Enqueue(B[b]);
            }
        }
    }
}

bool DFS(int _a)
{

    for (int i = 0; i < line[_a].Count; i++)
    {

        int b = line[_a][i];

        if (B[b] == -1 || lvl[B[b]] == lvl[_a] + 1 && DFS(B[b]))
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
#endif
        }
    }

#if other
// #include <iostream>
// #include <vector>
// #include <cstring>
// #include <queue>
using namespace std;

const int N = 204;
const int M = N * N;
bool visit[N][N], check[N][N];
int dx[8] = { -2, -2, -1, -1, 1, 1, 2, 2 };
int dy[8] = { -1, 1, -2, 2, -2, 2, -1, 1 };
int A[M], B[M], level[M], dist[N][N], idx[M];
vector<int> adj[M];
bool dfs(int now){
	while (idx[now] < (int)adj[now].size()){
		int next = adj[now][idx[now]];
		if (!B[next]){
			A[now] = next, B[next] = now;
			return true;
		}
		else if (level[B[next]] == level[now] + 1 && dfs(B[next])){
			A[now] = next, B[next] = now;
			return true;
		}
		idx[now]++;
	}
	return false;
}
void f(int x, int y){
	visit[x][y] = true;
	queue<pair<int, int>> q;
	q.push(make_pair(x, y));

	while (!q.empty()){
		int cx = q.front().first;
		int cy = q.front().second;
		q.pop();
		for (int k = 0; k < 8; k++){
			int nx = cx + dx[k];
			int ny = cy + dy[k];
			if (!check[nx][ny] || visit[nx][ny]) continue;
			visit[nx][ny] = true;
			dist[nx][ny] = dist[cx][cy] + 1;
			q.push(make_pair(nx, ny));
		}
	}
}
int main(void){
	cin.tie(0);
	ios::sync_with_stdio(0);

	int n, m, x, y;
	cin >> n >> m;
	int answer = n * n;

	for (int i = 2; i <= n + 1; i++)
	for (int j = 2; j <= n + 1; j++)
		check[i][j] = true;

	while (m--){
		cin >> x >> y; x++, y++;
		if (check[x][y]) check[x][y] = false, answer--;
	}

	for (int i = 2; i <= n + 1; i++)
	for (int j = 2; j <= n + 1; j++)
	if (check[i][j] && !visit[i][j])
		f(i, j);

	for (int i = 2; i <= n + 1; i++)
	for (int j = 2; j <= n + 1; j++)
	if (visit[i][j] && dist[i][j] % 2 == 0)
	for (int k = 0; k < 8; k++){
		int nx = i + dx[k];
		int ny = j + dy[k];
		if (!visit[nx][ny]) continue;
		if (abs(dist[nx][ny] - dist[i][j]) == 1)
			adj[i * N + j].push_back(nx * N + ny);
	}



	while (1){
		memset(idx, 0, sizeof(idx));
		memset(level, -1, sizeof(level));
		queue<int> q;
		for (int i = 2; i <= n + 1; i++)
		for (int j = 2; j <= n + 1; j++)
		if (visit[i][j] && !A[i * N + j] && dist[i][j] % 2 == 0)
			level[i * N + j] = 0, q.push(i * N + j);

		while (!q.empty()){
			int now = q.front(); q.pop();
			for (int next : adj[now])
			if (B[next] && level[B[next]] == -1)
				level[B[next]] = level[now] + 1, q.push(B[next]);
		}

		int temp = 0;
		for (int i = 2; i <= n + 1; i++)
		for (int j = 2; j <= n + 1; j++)
		if (visit[i][j] && !A[i * N + j] && dist[i][j] % 2 == 0)
			temp += dfs(i * N + j);
		if (!temp) break;
		answer -= temp;
	}
	cout << answer;
	return 0;
}
#elif other2
from sys import stdin,setrecursionlimit
setrecursionlimit(100000)
input=stdin.readline

def DFS(_n):
    for next in line[_n]:
        if (visit[next]):
            continue
        visit[next] = True
        
        if (match[next] == -1 or DFS(match[next])):
            match[next] = _n
            return True
    return False

dirR = [1,1,-1,-1,2,2,-2,-2]
dirC = [2,-2,2,-2,1,-1,1,-1]

n, m = map(int, input().split())

blocked = set()

for i in range(m):
    a, b = map(int, input().split())
    blocked.add((a - 1, b - 1))
    
line = [[] for i in range(n * n)]

for r in range(n):
    for c in range(n):
        if (r + c) % 2 and (r, c) not in blocked:
            for i in range(8):
                dr = r + dirR[i]
                dc = c + dirC[i]
                if (-1 < dr < n) and (-1 < dc < n):
                    if (dr, dc) not in blocked:
                        line[r * n + c].append(dr * n + dc)
                        
match = [-1] * (n * n)
answer = n * n - m

for i in range(n * n):
    if line[i]:
        visit = [False] * (n * n)
        if (DFS(i)):
            answer -= 1
            
print(answer)
#elif other3
import java.io.*;
import java.util.*;

public class Main {
    static class Edge {
        int node, flow, capacity;
        Edge revEdge;

        public Edge(int node, int flow, int capacity) {
            this.node = node;
            this.flow = flow;
            this.capacity = capacity;
        }

        void addFlow(int n) {
            flow += n;
            revEdge.flow -= n;
        }

        int spare() {
            return capacity - flow;
        }
    }

    static void makeGraph(int start, int dest, int capacity) {
        Edge st = new Edge(start, 0, 0);
        Edge de = new Edge(dest, 0, capacity);
        st.revEdge = de;
        de.revEdge = st;
        graph[start].add(de);
        graph[dest].add(st);
    }

    static List<Edge>[] graph;
    static int[] level, work;

    public static void main(String[] args) throws IOException {
        Reader reader = new Reader();
        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));

        int n = reader.nextInt();
        int m = reader.nextInt();
        int size = n * n;
        int start = n * n + 1;
        int dest = n * n + 2;

        graph = new List[size + 3];
        for (int i = 0; i < graph.length; i++) graph[i] = new ArrayList<>();
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                if ((i + j) % 2 == 0) makeGraph(start, i * n + j, 1);
                else makeGraph(i * n + j, dest, 1);
            }
        }

        boolean[][] impossible = new boolean[n][n];
        for (int i = 0; i < m; i++) {
            int a = reader.nextInt() - 1;
            int b = reader.nextInt() - 1;
            impossible[a][b] = true;
        }

        int[] dx = {-2, -2, -1, -1, 1, 1, 2, 2};
        int[] dy = {-1, 1, -2, 2, -2, 2, -1, 1};
        for (int i = 0; i < n; i++) {
            for (int j = (i % 2 == 0 ? 0 : 1); j < n; j += 2) {
                if (impossible[i][j]) continue;
                for (int k = 0; k < 8; k++) {
                    int tx = i + dx[k];
                    int ty = j + dy[k];
                    if (tx >= 0 && ty >= 0 && tx < n && ty < n && !impossible[tx][ty])
                        makeGraph(i * n + j, tx * n + ty, 1);
                }
            }
        }

        int answer = 0;
        level = new int[size + 3];
        work = new int[size + 3];

        while (bfs(start, dest)) {
            Arrays.fill(work, 0);

            while (true) {
                int f = dfs(start, dest, Integer.MAX_VALUE);
                if (f == 0) break;
                answer += f;
            }
        }

        bw.write(String.valueOf(n * n - m - answer));
        bw.flush();
        bw.close();
    }

    static int dfs(int cur, int dest, int flow) {
        if (cur == dest) return flow;

        for (int i = work[cur]; i < graph[cur].size(); i++) {
            work[cur] = i;
            Edge next = graph[cur].get(i);

            if (level[next.node] == level[cur] + 1 && next.spare() > 0) {
                int f = dfs(next.node, dest, Math.min(flow, next.spare()));
                if (f > 0) {
                    next.addFlow(f);
                    return f;
                }
            }
        }

        return 0;
    }

    static boolean bfs(int start, int dest) {
        Arrays.fill(level, -1);

        Queue<Integer> q = new LinkedList<>();
        q.offer(start);
        level[start] = 0;

        while (!q.isEmpty()) {
            Integer cur = q.poll();

            for (Edge next : graph[cur]) {
                if (next.spare() > 0 && level[next.node] == -1) {
                    level[next.node] = level[cur] + 1;
                    q.offer(next.node);
                }
            }
        }

        return level[dest] != -1;
    }

    static class Reader {
        final private int BUFFER_SIZE = 1 << 16;
        private DataInputStream din;
        private byte[] buffer;
        private int bufferPointer, bytesRead;

        public Reader() {
            din = new DataInputStream(System.in);
            buffer = new byte[BUFFER_SIZE];
            bufferPointer = bytesRead = 0;
        }

        public String readLine() throws IOException {
            byte[] buf = new byte[64]; // line length
            int cnt = 0, c;
            while ((c = read()) != -1) {
                if (c == '\n') {
                    if (cnt != 0) {
                        break;
                    } else {
                        continue;
                    }
                }
                buf[cnt++] = (byte) c;
            }
            return new String(buf, 0, cnt);
        }

        public int nextInt() throws IOException {
            int ret = 0;
            byte c = read();
            while (c <= ' ') {
                c = read();
            }
            boolean neg = (c == '-');
            if (neg)
                c = read();
            do {
                ret = ret * 10 + c - '0';
            } while ((c = read()) >= '0' && c <= '9');

            if (neg)
                return -ret;
            return ret;
        }

        private void fillBuffer() throws IOException {
            bytesRead = din.read(buffer, bufferPointer = 0,
                    BUFFER_SIZE);
            if (bytesRead == -1)
                buffer[0] = -1;
        }

        private byte read() throws IOException {
            if (bufferPointer == bytesRead)
                fillBuffer();
            return buffer[bufferPointer++];
        }

        public void close() throws IOException {
            if (din == null)
                return;
            din.close();
        }
    }
}

#elif other4
// #include <bits/stdc++.h>
using namespace std;
using lint = long long;
using llf = long double;
using pi = pair<int, int>;
// #define sz(v) ((int)(v).size())
// #define all(v) (v).begin(), (v).end()
const int MAXN = 40005;
const int MAXM = 40005;
const int mod = 1e9 + 7;

struct bpm{
	vector<int> gph[MAXN];
	int dis[MAXN], l[MAXN], r[MAXM], vis[MAXN];
	void clear(){ for(int i=0; i<MAXN; i++) gph[i].clear();	}
	void add_edge(int l, int r){ gph[l].push_back(r); }
	bool bfs(int n){
		queue<int> que;
		bool ok = 0;
		memset(dis, 0, sizeof(dis));
		for(int i=0; i<n; i++){
			if(l[i] == -1 && !dis[i]){
				que.push(i);
				dis[i] = 1;
			}
		}
		while(!que.empty()){
			int x = que.front();
			que.pop();
			for(auto &i : gph[x]){
				if(r[i] == -1) ok = 1;
				else if(!dis[r[i]]){
					dis[r[i]] = dis[x] + 1;
					que.push(r[i]);
				}
			}
		}
		return ok;
	}
	bool dfs(int x){
		if(vis[x]) return 0;
		vis[x] = 1;
		for(auto &i : gph[x]){
			if(r[i] == -1 || (!vis[r[i]] && dis[r[i]] == dis[x] + 1 && dfs(r[i]))){
				l[x] = i; r[i] = x;
				return 1;
			}
		}
		return 0;
	}
	int match(int n){
		memset(l, -1, sizeof(l));
		memset(r, -1, sizeof(r));
		int ret = 0;
		while(bfs(n)){
			memset(vis, 0, sizeof(vis));
			for(int i=0; i<n; i++) if(l[i] == -1 && dfs(i)) ret++;
		}
		return ret;
	}
	bool chk[MAXN + MAXM];
	void rdfs(int x, int n){
		if(chk[x]) return;
		chk[x] = 1;
		for(auto &i : gph[x]){
			chk[i + n] = 1;
			rdfs(r[i], n);
		}
	}
	vector<int> getcover(int n, int m){ // solve min. vertex cover
		match(n);
		memset(chk, 0, sizeof(chk));
		for(int i=0; i<n; i++) if(l[i] == -1) rdfs(i, n);
		vector<int> v;
		for(int i=0; i<n; i++) if(!chk[i]) v.push_back(i);
		for(int i=n; i<n+m; i++) if(chk[i]) v.push_back(i);
		return v;
	}
}bpm;

map<pi, int> mp;

int findOrCreate(pi p){
	if(mp.find(p) != mp.end()) return mp[p];
	int s = sz(mp);
	return mp[p] = s;
}

const int dx[8] = {1, 2, 2, 1, -1, -2, -2, -1};
const int dy[8] = {2, 1, -1, -2, -2, -1, 1, 2};

int arr[205][205];
int chk[205][205];

int main(){
	int m, n;
	scanf("%d %d",&m,&n);
	for(int i = 1; i <= n; i++){
		int x, y; scanf("%d %d",&x,&y);
		chk[x+2][y+2] = 1;
	}
	n = 0;
	for(int i = 3; i <= m + 2; i++){
		for(int j = 3; j <= m + 2; j++){
			if(!chk[i][j]) arr[i][j] = ++n;
		}
	}
	for(int x = 3; x <= m + 2; x++){
		for(int y = 3; y <= m + 2; y++){
			if(!arr[x][y]) continue;
			for(int j = 0; j < 8; j++){
				if(arr[x + dx[j]][y + dy[j]]){
					int k = arr[x + dx[j]][y + dy[j]];
					if((x + y) % 2) bpm.add_edge(arr[x][y], k);
					else bpm.add_edge(k, arr[x][y]);
				}
			}
		}
	}
	cout << n - bpm.match(n + 1) << endl;
}
#elif other5
// #include <iostream>
// #include <vector>
// #include <algorithm>
// #include <queue>
// #include <string>
// #include <ranges>
// #include <climits>

// #define USE_MACRO
// #ifdef USE_MACRO
// #define FAST_IO cin.tie(NULL)->sync_with_stdio(false)
// #define endl '\n'
// #define xy2idx(real, y, w) (real) * (w) + (y)
// #define idx2xy(idx, w) pair((idx) / (w), (idx) % (w))
// #define isin(a, real, b) ((a) <= (real) && (real) < (b))
// #define fors(var, start, end, step) for (auto var=start; var end; var step)
// #define foreach(iterable, ...) for (auto __VA_ARGS__ : (iterable))
// #define vector2d(var, T, N, ...) vector<vector<T>> var(N, vector<T>(__VA_ARGS__))
// #define vector3d(var, T, N, M, ...) vector<vector<vector<T>>> var(N, vector<vector<T>>(M, vector<T>(__VA_ARGS__)))
// #define set_max(var, value) var = var < (value) ? (value) : var
// #define set_min(var, value) var = var > (value) ? (value) : var
// #define range(arr) arr.begin(), arr.end()
// #define reversed(arr) arr.rbegin(), arr.rend()
// #define debug(arr) cout << "[ "; for (auto debug_element : arr) cout << debug_element << ' '; cout << " ]" << endl << endl;
// #define debug2d(arr) for (auto debug_row : arr) { cout << ">>\t"; for (auto debug_element : debug_row) cout << debug_element << ' '; cout << endl; } cout << endl;
// #endif

using namespace std;

    void bfs();
    bool dfs(int);

    tuple<int, int> ADJ[] = { { -2, 1 }, { -2, -1 }, { -1, 2 }, { -1, -2 }, { 1, 2 }, { 1, -2 }, { 2, 1 }, { 2, -1 } };

    vector<vector<bool>> grid;
    vector<vector<int>> graph;
    vector<int> dist, matched_A, matched_B;
    vector<bool> visited;

    int N, M;
    int main()
    {
        cin >> N >> M;

        grid.assign(N, vector<bool>(N));

        fors(i, 0, < M, ++)
    
    {
            int x, y;
            cin >> x >> y;
            grid[x - 1][y - 1] = true;
        }

        graph.assign(N * N, vector<int>());
        fors(i, 0, < N, ++)
    
    {
            fors(j, 0, < N, ++)

        {
                if (grid[i][j] || (i + j) % 2 == 1)
                    continue;

                int now = xy2idx(i, j, N);
                foreach (ADJ, [nx, ny])
			{
                    nx += i, ny += j;
                    if (!(isin(0, nx, N) && isin(0, ny, N) && !grid[nx][ny]))
                        continue;

                    graph[now].push_back(xy2idx(nx, ny, N));
                }
            }
        }


        dist.assign(N * N, -1);
        matched_A.assign(N * N, -1);
        matched_B.assign(N * N, -1);
        int match = 0;

        while (true)
        {
            bfs();
            int flow = 0;
            visited.assign(N * N, false);
            fors(i, 0, < N * N, ++)
    
        {
                auto[x, y] = idx2xy(i, N);
                if (grid[x][y] || (x + y) % 2 == 1)
                    continue;

                if (matched_A[i] == -1 && dfs(i))
                    flow++;
            }

            if (!flow)
                break;

            match += flow;
        }

        cout << N * N - M - match << endl;

        return 0;
    }


    void bfs()
    {
        ranges::fill(dist, -1);

        queue<int> q;
        fors(i, 0, < N * N, ++)
    
    {
            auto[x, y] = idx2xy(i, N);
            if (grid[x][y] || (x + y) % 2 == 1)
                continue;
            if (matched_A[i] == -1)
            {
                dist[i] = 1;
                q.push(i);
            }
        }
        while (!q.empty())
        {
            int now = q.front(); q.pop();

            foreach (graph[now], next)
			if (matched_B[next] != -1 && dist[matched_B[next]] == -1)
            {
                dist[matched_B[next]] = dist[now] + 1;
                q.push(matched_B[next]);
            }
        }
    }

    bool dfs(int now)
    {
        if (visited[now])
            return false;
        visited[now] = true;

        foreach (graph[now], next)
		if (matched_B[next] == -1 || (dist[matched_B[next]] == dist[now] + 1 && dfs(matched_B[next])))
        {
            matched_A[now] = next;
            matched_B[next] = now;
            return true;
        }

        return false;
    }
#endif
}
