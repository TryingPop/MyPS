using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 25
이름 : 배성훈
내용 : 프리즌 브레이크
    문제번호 : 1886번

    이분 매칭 문제다
    아이디어는 다음과 같다
    우선 BFS 탐색으로 모든 사람이 적어도 하나의 출구로 갈 수 있는지 확인한다
    여기서 못가면 탈출 불가능이다 -> 이는 BFS 탐색을하고 간선을 이으므로 간선의 수로 확인했다

    모두 간선이 이어졌다면 이제 최소 탈출 시간을 찾아야한다
    탈출 시간을 이분 탐색으로 설정하고 이분 매칭으로 가능한지 판별했다

    여기서 1개의 탈출구에는 1초당 1명씩 밖에 못지나가므로 시간에 따라 탈출구를 구분해주거나
    시간에 따라 사람을 구분해줘야한다

    외부에 벽 또는 문만 있기 때문에 최대 시간은 (row - 2) * (col - 2)이다
    최대 크기에서 (row - 2) * (col - 2), 2 * (row - 1) + 2 * (col - 1)이 둘을 비교해보면
    전자가 100, 후자는 36밖에 안되므로 문에 시간을 곱해 연산을 했다
    그리고 같은 이유로 간선 연결의 BFS 탐색도 사람에 따라 하는게 아닌, 문에 따라 진행했다
    그래서 시간에 따라 문을 구분하고 이분매칭을 돌렸다

    이분매칭 알고리즘은 호프크로프트 카프 알고리즘으로 했다
    이렇게 제출하니 96ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0725
    {

        static void Main725(string[] args)
        {

            int INF = 1_000;

            StreamReader sr;
            int row, col;
            int[][] board;
            int[][] dis;
            List<(int dst, int dis)>[] line;
            int n, m;

            Queue<(int r, int c)> temp;
            Queue<(int r, int c)> qLine;

            int[] dirR, dirC;
            Queue<int> q;

            int[] A, B, lvl;
            bool[] visit;

            Solve();

            void Solve()
            {

                Input();

                SetLine();

                for (int i = 0; i < line.Length; i++)
                {

                    if (line[i].Count == 0)
                    {

                        Console.Write("impossible");
                        return;
                    }
                }

                Init();
                int l = 0;
                int r = row * col;
                while(l <= r)
                {

                    int mid = (l + r) / 2;

                    if (Match(mid)) r = mid - 1;
                    else l = mid + 1;
                }

                Console.WriteLine(r + 1);
            }

            bool Match(int _limit)
            {

                int ret = 0;

                Array.Fill(A, -1);
                Array.Fill(B, -1);
                Array.Fill(visit, false);
                while (true)
                {

                    BFS(_limit);
                    int match = 0;

                    for (int i = 0; i < n; i++)
                    {

                        if (!visit[i] && DFS(i, _limit)) match++;
                    }

                    if (match == 0) break;

                    ret += match;
                }

                return ret >= n;
            }

            void Init()
            {

                visit = new bool[n];
                A = new int[n];
                B = new int[m * (row * col)];
                lvl = new int[n];
                q = new(n);
            }

            void BFS(int _limit)
            {

                for (int i = 0; i < n; i++)
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

                    for (int i = 0; i < line[a].Count; i++)
                    {

                        if (line[a][i].dis > _limit) continue;
                        int b = NodeToIdx(line[a][i]);
                        if (B[b] != -1 && lvl[B[b]] == INF)
                        {

                            lvl[B[b]] = lvl[a] + 1;
                            q.Enqueue(B[b]);
                        }
                    }
                }
            }

            bool DFS(int _a, int _limit)
            {

                for (int i = 0; i < line[_a].Count; i++)
                {

                    if (line[_a][i].dis > _limit) continue;

                    int b = NodeToIdx(line[_a][i]);

                    if (B[b] == -1 || lvl[B[b]] == lvl[_a] + 1 && DFS(B[b], _limit))
                    {

                        visit[_a] = true;
                        A[_a] = b;
                        B[b] = _a;
                        return true;
                    }
                }

                return false;
            }

            void SetLine()
            {

                qLine = new(row * col);
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                line = new List<(int dst, int dis)>[n];
                for (int i = 0; i < n; i++)
                {

                    line[i] = new();
                }

                int idx = 0;
                while(temp.Count > 0)
                {

                    var node = temp.Dequeue();
                    qLine.Enqueue(node);
                    dis[node.r][node.c] = 0;

                    BFSLine();

                    LinkLine(idx);
                    idx++;
                }
            }

            int NodeToIdx((int dst, int dis) _node)
            {

                int MUL = row * col;
                return _node.dst * MUL + _node.dis;
            }

            void LinkLine(int _idx)
            {

                int M = (row - 2) * (col - 2);
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int d = dis[r][c];
                        if (d == -1) continue;
                        dis[r][c] = -1;
                        int f = board[r][c];
                        if (f < 0) continue;

                        for (int i = d; i <= M; i++)
                        {

                            line[f].Add((_idx, i));
                        }
                    }
                }
            }

            void BFSLine()
            {

                while(qLine.Count > 0)
                {

                    var node = qLine.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || dis[nextR][nextC] != -1) continue;
                        dis[nextR][nextC] = dis[node.r][node.c] + 1;

                        if (board[nextR][nextC] < 0) continue;
                        qLine.Enqueue((nextR, nextC));
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void Input()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()));

                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                dis = new int[row][];
                n = 0;
                m = 0;

                temp = new(row * 2 + col * 2 - 4);

                for (int r = 0; r< row; r++)
                {

                    board[r] = new int[col];
                    dis[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        dis[r][c] = -1;
                        int cur = sr.Read();
                        if (cur == '.') board[r][c] = n++;
                        else if (cur == 'X') board[r][c] = -1;
                        else
                        {

                            board[r][c] = -2;
                            m++;
                            temp.Enqueue((r, c));
                        }
                    }

                    if (sr.Read() == '\r') sr.Read();
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
// #include <bits/stdc++.h>

using namespace std;

typedef pair<int,int> pii;

int dx[4] = {1,0,-1,0};
int dy[4] = {0,-1,0,1};
int dist[13][13];
int j_idx[13][13],d_idx[13][13];
char arr[13][13];
int dcnt,jcnt;
int N,M;
vector<pii> edge[10001];
vector<int> indicator;
bool visited[10001];
vector<int> t_edge[10001];

bool Approx(int X,int Y) {
    if(X>=1 && X<=M && Y>=1 && Y<=N) return true;
    return false;
}

void BFS(int X,int Y) {
    memset(dist,5,sizeof(dist));
    dist[X][Y] = 0;
    int idx = j_idx[X][Y];
    queue<pii> task;
    task.push({X,Y});
    int cnt = 0;
    while(!task.empty()) {
        int nx = task.front().first;
        int ny = task.front().second;
        task.pop();

        if(arr[nx][ny] == 'D') {
            edge[idx].push_back({d_idx[nx][ny],dist[nx][ny]});
            cnt++;
            continue;
        }

        for(int i=0; i<4; i++) {
            int ox = nx + dx[i];
            int oy = ny + dy[i];
            if(Approx(ox,oy) && arr[ox][oy] != 'X') {
                if(dist[ox][oy] > dist[nx][ny]+1) {
                    dist[ox][oy] = dist[nx][ny]+1;
                    task.push({ox,oy});
                }
            }
        }
    }
    if(cnt == 0) cout << "impossible", exit(0);
}

bool DFS(int num) {
    for(int i=0; i<t_edge[num].size(); i++) {
        int next = t_edge[num][i];
        if(visited[next]) continue;
        visited[next] = true;
        if(indicator[next] == 0 || DFS(indicator[next])) {
            indicator[next] = num;
            return true;
        }
    }
    return false;
}

bool check(int num) {
    indicator = vector<int>(num*dcnt+1,0);
    for(int i=0; i<10001; i++) t_edge[i].clear();
    
    for(int i=1; i<=jcnt; i++) {
        for(auto tt : edge[i]) {
            int ent = tt.first;
            int w = tt.second;
            for(int j=0; j<=num-w; j++) {
                t_edge[i].push_back(ent+dcnt*j);
            }
        }
    }
    int cnt = 0;
    for(int i=1; i<=jcnt; i++) {
        memset(visited,false,sizeof(visited));
        if(DFS(i) == true) cnt++;
    }
    if(cnt == jcnt) return true;
    return false;
}
int main() {
    cin.tie(NULL);
    ios_base::sync_with_stdio(false);

    cin >> N >> M;
    for(int i=1; i<=N; i++) {
        for(int j=1; j<=M; j++) {
            char C;
            cin >> C;
            if(C == 'D') d_idx[j][i] = ++dcnt;
            else if(C == '.') j_idx[j][i] = ++jcnt;
            arr[j][i] = C;
        }
    }

    for(int i=1; i<=N; i++) {
        for(int j=1; j<=M; j++) {
            if(j_idx[j][i] != 0) BFS(j,i);
        }
    }

    int left = 0, right = 150, ans = 0;
    while(left+1 < right) {
        int mid = (left+right)/2;
        if(check(mid))
            ans = right = mid;
        else left = mid;
    }
    cout << ans;
    exit(0);
}
#elif other2
// #include <bits/stdc++.h>
// #define fastio                    \
	ios_base::sync_with_stdio(0); \
	cin.tie(0);
// #define vi vector<int>
// #define vl vector<long long>
// #define vc vector<char>
// #define vs vector<string>
// #define pi pair<int, int>
// #define pl pair<ll, ll>
// #define vp vector<pi>
// #define vpl vector<pl>
// #define ll long long
// #define MAX 2147000000
// #define MOD 1000000007
using namespace std;


vp D, A;
int n, m;
char board[12][12];
int board2[12][12];
int dist[101][101];
int xx[]{-1,0,1,0};
int yy[]{0,1,0,-1};
int d{1}, a{1};

const int sz = 10000 + 10;
vector<vi> g(sz);
vi ch(sz);
vi match(sz);

bool bipartiteMatching(int x)
{
    for(auto& nx : g[x]){
        if (ch[nx] == 0)
        {
            ch[nx] = 1;
            if (match[nx] == 0 || bipartiteMatching(match[nx]))
            {
                match[nx] = x;
                return true;
            }
        }
    }
    return false;
}

int f(int mid){
    fill(match.begin(), match.end(), 0);
    for(int i{1}; i < a; ++i){
        g[i].clear();
        for(int j{1}; j < d; ++j){
            if(dist[i][j] == -1) continue;
            if(dist[i][j] <= mid) {
                for(int k{dist[i][j]}; k <= mid; ++k) g[i].push_back(j * 100 + k);
            }
        }
    }
    int ret{0};
    for(int i{1}; i < a; ++i){
        fill(ch.begin(), ch.end(), 0);
        if(bipartiteMatching(i)) ret++;
    }
    return ret;
}

void solve(){    
    cin >> n >> m;
    for(int i{0}; i < n; ++i){
        for(int j{0}; j < m; ++j){
            cin >> board[i][j];
            if(board[i][j] == 'D'){
                board2[i][j] = d++;
                D.push_back({i, j});
            }
            else if(board[i][j] == '.'){
                board2[i][j] = a++;
                A.push_back({i, j});
            }
        }
    }
    memset(dist, -1, sizeof(dist));
    for(int i{0}; i < (int)D.size(); ++i){
        queue<pi> Q;
        Q.push({D[i].first, D[i].second});
        vector<vi> temp(n, vi(m, -1));
        temp[D[i].first][D[i].second] = 0;
        while(!Q.empty()){
            int x{Q.front().first};
            int y{Q.front().second};
            Q.pop();
            for(int dir{0}; dir < 4; ++dir){
                int nx{x + xx[dir]};
                int ny{y + yy[dir]};
                if(nx < 0 || nx > n - 1 || ny < 0 || ny > m - 1) continue;
                if(board[nx][ny] == '.' && temp[nx][ny] == -1){
                    temp[nx][ny] = temp[x][y] + 1;
                    dist[board2[nx][ny]][board2[D[i].first][D[i].second]] = temp[nx][ny];
                    Q.push({nx, ny});
                }
            }
        }
    }
    int l{1}, r{100};
    int ans{-1};
    while(l <= r){
        int mid{(l + r) >> 1};
        if(f(mid) == a - 1){
            ans = mid;
            r = mid - 1;
        }
        else l = mid + 1;
    }
    if(ans == -1) cout << "impossible";
    else cout << ans;
}

int main(){
	fastio;
	int T;
	T = 1;
	while(T--){
		solve();
	}
}

#elif other3
from collections import defaultdict, deque

INF = 0x3f3f3f3f
xshift = [0, 0, 1, -1]
yshift = [1, -1, 0, 0]
visited = [False] * 26000

def id(i, j): # 이차원 배열 값을 1차원의 n으로 변경
    return i * n + j

def bfs(x, y):
    q = deque([(x, y)])
    DIST = [[-1 for _ in range(m+2)] for _ in range(n)]
    DIST[x][y] = 0
    iid = id(x, y) # 현재 죄수의 n위치
    ret = False # 탈출 가능 여부

    while q:
        a, b = q.popleft()

        for i in range(4):
            A = a + xshift[i]
            B = b + yshift[i]

            if 0 <= A < n and 1 <= B <= m:
                if board[A][B] == '.' and DIST[A][B] == -1:
                    q.append((A, B))
                    DIST[A][B] = DIST[a][b] + 1
                elif board[A][B] == 'D' and DIST[A][B] == -1:
                    ret = True
                    DIST[A][B] = DIST[a][b] + 1
                    for j in range(DIST[A][B], 160):
                        adj[iid].append((j * sink + id(A, B), j))

    return ret

def dfs(here, limit):
    global visited
    if visited[here]:
        return False
    visited[here] = True

    for i in range(len(adj[here])):
        next, cost = adj[here][i]
        if cost > limit:
            continue
        if parent[next] == -1 or dfs(parent[next], limit):
            parent[next] = here
            return True
    return False

def nf(limit):
    global parent
    global visited
    parent = [-1] * (sink * 160 + 1)

    flow = 0
    for i in range(n):
        for j in range(1, m+1):
            visited = [False] * (sink * 160 + 1)
            if dfs(id(i, j), limit):
                flow += 1

    return flow >= cnt

n, m = map(int, input().split())
// #sink = id(n - 1, m) + 1
sink = 160
board = [['X'] * (m + 2) for _ in range(n)]
adj = defaultdict(list)

for i in range(n):
    board[i][1:m+1] = input().strip()

cnt = sum(row.count('.') for row in board) # 범죄자 수

if not all(bfs(i, j) for i in range(n) for j in range(1, m+1) if board[i][j] == '.'): #bfs에서 한번이라도 탈출 불가능 사인이 나오면 탈출 불가능
    print("impossible")
else:
    lo, hi = 0, 161
    while lo + 1 < hi:
        mid = (lo + hi) // 2
        if nf(mid):
            hi = mid
        else:
            lo = mid
    print(hi)

#elif other4
from collections import deque
import sys

// # 입력범위 n, m 이 3 ~ 12 사이이므로 빠르게 입력받을 필요 X
max_y, max_x = map(int, input().split())
board = []
for y in range(max_y):
    line = list(input())
    board.append(line[:])

// # 애초에 탈출 불가능인지 체크하기
queue = deque()
visited = [[False for _ in range(max_x)] for _ in range(max_y)]
move = [(0, 1), (0, -1), (1, 0), (-1, 0)]
is_impossible = False
for y in range(max_y):
    for x in range(max_x):
        if board[y][x] == 'D':
            queue.append((y, x))
            visited[y][x] = True
while queue:
    y, x = queue.popleft()
    for d in move:
        ny, nx = y + d[0], x + d[1]
        if 0 <= ny < max_y and 0 <= nx < max_x:
            if not visited[ny][nx]:
                if board[ny][nx] == '.':
                    queue.append((ny, nx))
                    visited[ny][nx] = True
for y in range(max_y):
    for x in range(max_x):
        if board[y][x] == '.':
            if not visited[y][x]:
                is_impossible = True
if is_impossible:
// # 탈출이 불가능 할 때 바로 종료
    print('impossible')
    sys.exit()

del visited

// # 이제는 탈출이 가능함이 보장되었을 때 최소 시간을 구하라는 문제로 변형됨
'''
 각 시간 단위마다 독립적으로 최대 매칭을 수행하고, 
그 결과를 이전 시간 단위의 매칭 결과와 별개로 처리하는 방식은 
최적의 해를 보장하지 않는다.
 따라서, 정해진 시간에 대해 간선들을 모두 연결한다음, 
최대 매칭을 수행하는 방식으로 진행해야한다.
 그래프는 미리 만들어 두고, 
매칭만 여러번 수행하는것도 괜찮을것같다.

<시간복잡도 계산>
최악의 시간 복잡도 계산
사람의 최대 수: 100
탈출구의 최대 분할 노드 수: 4000
이분 탐색의 스텝 수: log2(100) ≈ 7

따라서, 최악의 시간 복잡도는 이분 매칭 알고리즘의 시간 복잡도에 
이분 탐색 스텝 수를 곱한 것이다. 
이를 표현하면 O(VE log T)가 된다.
 -> 충분히 가능할 것으로 보인다.
'''

// # 여러번 사용될 그래프 모델링 하기
'''
 그래프를 만들때 시간에 따른 매칭을 편하게 하기 위해서
탈출구에번호를 매기고
예를들어 n개의 탈출구가 있다면
0 ~ n-1 번까지가 1초의 탈출구
n ~ 2n - 1 번까지가 2초의 탈출구 이렇게 만든다.
그러려면 탈출구들을 각각 bfs 돌리면서 
x번 탈출구 노드의 인덱스 정점분할은 
x , x + n, x + 2n, x + 3n ... x + tn 이렇게 가면 될 것이다.
'''

// # 탈출구 위치 기록하기, 사람 수 세기
exits_info = []
count_people = 0
for y in range(max_y):
    for x in range(max_x):
        if board[y][x] == 'D':
            exits_info.append((y, x))
        if board[y][x] == '.':
            count_people += 1

// # 그래프 연결하기 (탈출구 -> 사람)
count_exits = len(exits_info)
graph = [[[] for _ in range(201)] for _ in range(count_exits)]
for i in range(count_exits):
    exityx = exits_info[i]
// # 각각의 탈출구마다 bfs 돌리면서 시간에따른 연결정보 기록
    visited = [[False for _ in range(max_x)] for _ in range(max_y)]
    queue = deque()
    y, x = exityx
    time = 0
    queue.append((y, x, time))
    visited[y][x] = True
    while queue:
        y, x, t = queue.popleft()
        for d in move:
            ny, nx, nt = y + d[0], x + d[1], t+1
            if 0 <= ny < max_y and 0 <= nx < max_x:
                if not visited[ny][nx]:
                    if board[ny][nx] == '.':
                        queue.append((ny, nx, nt))
                        visited[ny][nx] = True
                        for tt in range(nt, 200):
                            graph[i][tt].append((ny, nx))

// # 이분매칭 함수


def dfs_bipartite_matching(e, t):
    if visited[e][t]:
        return False
    visited[e][t] = True
    for next_yx in graph[e][t]:
        ny, nx = next_yx
        if matched[ny][nx] == -1 or dfs_bipartite_matching(*matched[ny][nx]):
            matched[ny][nx] = (e, t)
            return True
    return False


// # 이분탐색하면서 이분매칭하기
min_time = 0
max_time = 150
result = max_time
while min_time <= max_time:
    mid_time = (min_time + max_time) // 2

// # mid time 에 대해서 이분매칭 해보기
    m = 0  # m 은 매칭 개수
    matched = [[-1 for _ in range(max_x)] for _ in range(max_y)]
    for t in range(mid_time):
        for e in range(count_exits):
            visited = [[False for _ in range(201)] for _ in range(count_exits)]
            if dfs_bipartite_matching(e, t):
                m += 1

// # 매칭 후 모든 사람이 매칭됐는지 체크하기
    if m == count_people:
        result = mid_time
        max_time = mid_time-1
    else:
        min_time = mid_time+1

print(result-1)
#elif other5
// #include <bits/stdc++.h>
using namespace std;
using ll = long long;
using pii = pair<int, int>;
using pll = pair<ll, ll>;
// #define all(v) v.begin(), v.end()
const ll V = 25005, INF = 0x3f3f3f3f;
ll N, M, S, E, P;
char arr[55][55];
pii d[] = {{1, 0}, {-1, 0}, {0, 1}, {0, -1}};

inline ll f(int y, int x, int t) {return t*N*M+y*M+x;}
inline bool out(int y, int x) {return (y < 0 || y > N-1 || x < 0 || x > M-1);}

struct Flow {

	map<ll, ll> cap[V], flow[V];
	ll lv[V], wk[V];
	vector<ll> G[V];

	void mk(ll s, ll e, ll w=INF, bool p=false) {
		G[s].push_back(e);
		G[e].push_back(s);
		cap[s][e] += w;
		if (p) cap[e][s] += w;
	}

	void init(ll mt) {
		S = f(0, 0, mt+1), E = S+1;
		for (int i = 0; i <= E; i++) {
			cap[i].clear();
			flow[i].clear();
			G[i].clear();
		}

		for (int i = 0; i < N; i++) {
			for (int j = 0; j < M; j++) {
				if (arr[i][j] == '.') {
					mk(S, f(i, j, 0), 1);
					for (int t = 0; t < mt; t++) {
						mk(f(i, j, t), f(i, j, t+1));
						for (auto &[dy, dx]: d) {
							int ny = dy+i, nx = dx+j;
							if (out(ny, nx)) continue;
							if (arr[ny][nx] != 'X') mk(f(i, j, t), f(ny, nx, t+1));
						}
					}
				}
				if (arr[i][j] == 'D') {
					for (int t = 0; t <= mt; t++) mk(f(i, j, t), E, 1);
				}
			}
		}

	}
	
	bool bfs(ll s, ll e) {
		memset(lv, -1, sizeof(lv));
		lv[s] = 0;
		queue<ll> que;
		que.push(s);
		while (que.size()) {
			ll cur = que.front();
			que.pop();
			for (ll nxt: G[cur]) {
				if (lv[nxt] == -1 && cap[cur][nxt] > flow[cur][nxt]) {
					lv[nxt] = lv[cur] + 1;
					que.push(nxt);
				}
			}
		}
		return lv[e] != -1;
	}

	ll dfs(ll cur, ll end, ll mf) {
		if (cur == end) return mf;
		for (ll &i = wk[cur]; i < G[cur].size(); i++) {
			ll nxt = G[cur][i];
			if (lv[nxt] == lv[cur]+1 && cap[cur][nxt] > flow[cur][nxt]) {
				ll df = dfs(nxt, end, min(mf, cap[cur][nxt]-flow[cur][nxt]));
				if (df) {
					flow[cur][nxt] += df;
					flow[nxt][cur] -= df;
					return df;
				}
			}
		}
		return 0;
	}

	ll run(ll s, ll e) {
		ll ret = 0;
		while (bfs(s, e)) {
			memset(wk, 0, sizeof(wk));
			while (true) {
				ll f = dfs(s, e, INF);
				if (!f) break;
				ret += f;
			}
		}
		return ret;
	}

}flow;

bool ok(ll t) {
	flow.init(t);
	return (flow.run(S, E) == P);
}

void out(int x) {
	if (x < 0) cout << "impossible";
	else cout << x;
	exit(0);
}

void solve() {

	cin >> N >> M;
	for (int i = 0; i < N; i++) {
		for (int j = 0; j < M; j++) {
			cin >> arr[i][j];
			if (arr[i][j] == '.') P++;
		}
	}
	
	if (!ok(100)) out(-1);
	int lo = -1, hi = 100;
	while (lo + 1 < hi) {
		int mid = (lo+hi)/2;
		if (ok(mid)) hi = mid;
		else lo = mid;
	}	
	out(hi);

}

int main(void) {
	
	ios::sync_with_stdio(0);
	cin.tie(0); cout.tie(0);
	int T = 1; //cin >> T;
	while (T--) solve();
	return 0;
}
#endif
}
