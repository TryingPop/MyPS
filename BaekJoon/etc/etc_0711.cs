using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 20
이름 : 배성훈
내용 : 주차장
    문제번호 : 1348번

    BFS, 이분 탐색, 이분 매칭 문제다
    이분 탐색에서 상한을 너무 낮게 설정해 3번, 로직 오류로 1번 틀렸다;
        처음 2번에는 로직 오류로 추정하고 봤고
        -> 실제로 지문에 차가 0인 경우 0을 출력하라고 되어져 있었다
    
        해당 부분 수정하고도 틀리자 하나씩 다시 확인했다
        그래서 맵의 크기에 오류가 있음을 알게되었다
        50 * 50이라 100이 맥스겠지하고 쉽게 단정지었다;
        실제론 |_|-|_... 형식으로 이동하면 적어도 1000이상 잡아야한다!
        그래서 안일하게 1000잡다가 한 번 더 틀렸다;
        5000으로 확실하게 크게 잡아 제출하니 84ms에 이상없이 통과했다

    아이디어는 다음과 같다
    먼저 BFS로 자동차 위치에서 주차장까지의 거리를 모두 구한뒤 이동 가능한 간선을 저장한다(목적지와 거리)
    이후 이분 매칭을 하는데 일정 거리를 넘어가면 못지나가게 했다
    일정 거리는 이분 탐색으로 설정해 가능한 최대값을 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0711
    {

        static void Main711(string[] args)
        {

            StreamReader sr;
            int row, col;

            int[][] board;
            int[][] calc;

            List<(int r, int c)> car;
            List<(int r, int c)> park;

            Queue<(int r, int c)> q;
            List<(int dst, int dis)>[] line;

            int[] dirR;
            int[] dirC;

            bool[] visit;
            int[] match;

            Solve();

            void Solve()
            {

                Input();

                if (car.Count == 0) 
                { 
                    
                    Console.WriteLine(0);
                    return;
                }
                
                SetLine();

                Console.WriteLine(Find());
            }

            int Find()
            {

                visit = new bool[park.Count];
                match = new int[park.Count];

                int l = 0;
                int r = 5_000;

                int ret = -1;
                while(l <= r)
                {

                    int mid = (l + r) / 2;

                    if (Matching(mid)) 
                    { 
                        
                        r = mid - 1;
                        ret = mid;
                    }
                    else l = mid + 1;
                }

                return ret;
            }

            bool Matching(int _dis)
            {

                Array.Fill(match, -1);

                int ret = 0;
                for (int i = 0; i < car.Count; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i, _dis)) ret++;
                }

                return ret == car.Count;
            }

            bool DFS(int _n, int _dis)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    (int dst, int dis) next = line[_n][i];
                    if (_dis < next.dis || visit[next.dst]) continue;
                    visit[next.dst] = true;

                    if (match[next.dst] == -1 || DFS(match[next.dst], _dis))
                    {

                        match[next.dst] = _n;
                        return true;
                    }
                }

                return false;
            }

            void SetLine()
            {

                q = new(row * col);

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                line = new List<(int dst, int dis)>[car.Count];

                for (int i = 0; i < car.Count; i++)
                {

                    BFS(i);
                    LinkLine(i);
                }
            }

            void LinkLine(int _n)
            {

                line[_n] = new(park.Count);

                for (int i = 0; i < park.Count; i++)
                {

                    int dis = calc[park[i].r][park[i].c];
                    if (dis == -1) continue;

                    line[_n].Add((i, dis));
                }
            }

            void BFS(int _n)
            {

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        calc[r][c] = -1;
                    }
                }

                calc[car[_n].r][car[_n].c] = 0;
                q.Enqueue((car[_n].r, car[_n].c));

                while (q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();
                    int cur = calc[node.r][node.c];
                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];
                        if (ChkInvalidPos(nextR, nextC) || calc[nextR][nextC] != -1) continue;
                        calc[nextR][nextC] = cur + 1;
                        if (board[nextR][nextC] == 0) q.Enqueue((nextR, nextC));
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

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                car = new(row * col);
                park = new(row * col);

                board = new int[row][];
                calc = new int[row][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    calc[r] = new int[col];

                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == 'C') car.Add((r, c));
                        else if (cur == 'P') park.Add((r, c));
                        else if (cur == 'X') board[r][c] = -1;
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
// #pragma warning disable CS8604
// #pragma warning disable CS8602

namespace ConsoleApp1 {
    internal static class Program {
        private static string[]? _map;
        private static int[][,]? _dist; // n번째 차가 (r, c) 좌표로 이동하는데 필요한 거리
        private static int[]? _occupied; // 각 주차장에 어떤 차가 주차되어 있는지 저장용
        private static bool[]? _done; // 각 차가 주차되어있는지 저장용
        private static readonly List<(int, int)> Cars = new();
        private static readonly List<(int, int)> Parks = new();

        private static void Main(string[] args) {
            var rc = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var (r, c) = (rc[0], rc[1]);
            _map = new string[r];

            for (var i = 0; i < r; i++) {
                _map[i] = Console.ReadLine();
                for (var j = 0; j < c; j++) {
                    if (_map[i][j] == 'C') {
                        Cars.Add((i, j));
                    }

                    if (_map[i][j] == 'P') {
                        Parks.Add((i, j));
                    }
                }
            }

            if (Cars.Count == 0) {
                Console.WriteLine(0);
                return;
            }

            if (Parks.Count < Cars.Count) {
                Console.WriteLine(-1);
                return;
            }
            
            _dist = new int[Cars.Count][,];
            _occupied = new int[Parks.Count];
            _done = new bool[Parks.Count];
            
            for (var i = 0; i < _dist.Length; i++) {
                _dist[i] = new int[r, c];
                for (var j = 0; j < _dist[i].GetLength(0); j++) {
                    for (var k = 0; k < _dist[i].GetLength(1); k++) {
                        _dist[i][j, k] = int.MaxValue;
                    }
                }
            }
            
            InitDistances();

            int hi = int.MaxValue, lo = 0;
            var pivot = hi / 2;
            var isPossible = false;
            while (lo < hi) {
                Array.Fill(_occupied, -1);
                if (BipartiteMatch(pivot)) {
                    isPossible = true;
                    hi = pivot;
                }
                else {
                    lo = pivot + 1;
                }

                pivot = lo + (hi - lo) / 2;
                if (lo >= hi) break;
            }
            
            if (!isPossible) {
                Console.WriteLine(-1);
                return;
            }
            
            Console.WriteLine(pivot);
        }
        
        private static bool BipartiteMatch(int pivot) {
            var matchCount = 0;
            for (var i = 0; i < Cars.Count; i++) {
                Array.Fill(_done, false);
                if (Dfs(i, pivot)) {
                    matchCount++;
                }
            }

            return matchCount == Cars.Count;
        }
        
        private static bool Dfs(int car, in int pivot) {
            for (var i = 0; i < Parks.Count; i++) {
                if (_done[i]) continue;
                var (r, c) = Parks[i];
                if (_dist[car][r, c] > pivot) continue; // _done을 true로 바꾸지 않고 컨티뉴해야함
                _done[i] = true;

                // 현재 빈자리거나 || 기존 차를 다른 자리로 이동 가능하다면
                if ( _occupied[i] == -1 || Dfs(_occupied[i], pivot)) {
                    _occupied[i] = car;
                    return true;
                }
            }

            return false;
        }

        // 각각의 차에 대해 모든 위치에 대한 거리 구하기
        private static void InitDistances() {
            for (var i = 0; i < Cars.Count; i++) {
                var car = Cars[i];
                var q = new Queue<(int, int)>();
                var visited = new bool[_map.Length, _map[0].Length];
                _dist[i][car.Item1, car.Item2] = 0;
                q.Enqueue(car);

                while (q.Count > 0) {
                    var (r, c) = q.Dequeue();
                    if (visited[r, c]) continue;
                    visited[r, c] = true;

                    foreach (var (ar, ac) in AdjacentCoords((r, c))) {
                        if (_dist[i][ar, ac] > _dist[i][r, c] + 1) {
                            _dist[i][ar, ac] = _dist[i][r, c] + 1;
                            if (!visited[ar, ac]) q.Enqueue((ar, ac));
                        }
                    }
                }
            }
        }

        private static IEnumerable<(int, int)> AdjacentCoords((int, int) coord) {
            var (r, c) = coord;
            if (r > 0 && _map[r - 1][c] != 'X') yield return (r - 1, c);
            if (r < _map.Length - 1 && _map[r + 1][c] != 'X') yield return (r + 1, c);
            if (c > 0 && _map[r][c - 1] != 'X') yield return (r, c - 1);
            if (c < _map[0].Length - 1 && _map[r][c + 1] != 'X') yield return (r, c + 1);
        }
    }
}

/*
3 13
.C.....P.X...
XX.......X..P
XX.....C.....
*/
#elif other2
// #include<iostream>
// #include<vector>
// #include<queue>
// #include<algorithm>
// #include<cstring>

using namespace std;
typedef long long ll;

int r, c;
int map[50][50] = { {0} };
int dist[100][100] = { {0} };
int carloc[50][50] = { {0} };//차 번호
int lotloc[50][50] = { {0} };//주차장 번호
int carcnt = 0;
int lotcnt = 0;
vector<int> cartolot[100];//해당 번호의 차가 갈 수 있는 주차장 번호
bool checked[100];//주차확인 여부
int parkedcar[100];//주차된 차 번호

struct node {
	int row;
	int column;
	int d;
};

void bfs(int sr, int sc, int carnum)
{
	bool visit[50][50] = { {false} };
	queue<node> q;
	q.push(node{ sr, sc, 0 });
	visit[sr][sc] = true;
	while (!empty(q))
	{
		node now = q.front(); q.pop();
		if (map[now.row][now.column] == 2)
		{//주차장이면 거리계산, 해당 차가 갈 수 있는 주차장 번호로 기록
			dist[carnum][lotloc[now.row][now.column]] = now.d;
			cartolot[carnum].push_back(lotloc[now.row][now.column]);
		}
		if (now.row > 0 && map[now.row - 1][now.column] && !visit[now.row - 1][now.column])
		{//범위 내, 벽이 아님, 방문안함 -> 방문처리, 이동
			visit[now.row - 1][now.column] = true;
			q.push(node{ now.row - 1, now.column, now.d + 1 });
		}
		if (now.row < r - 1 && map[now.row + 1][now.column] && !visit[now.row + 1][now.column])
		{
			visit[now.row + 1][now.column] = true;
			q.push(node{ now.row + 1, now.column, now.d + 1 });
		}
		if (now.column > 0 && map[now.row][now.column - 1] && !visit[now.row][now.column - 1])
		{
			visit[now.row][now.column - 1] = true;
			q.push(node{ now.row, now.column - 1, now.d + 1 });
		}
		if (now.column < c - 1 && map[now.row][now.column + 1] && !visit[now.row][now.column + 1])
		{
			visit[now.row][now.column + 1] = true;
			q.push(node{ now.row, now.column + 1, now.d + 1 });
		}
	}
}

bool bipartite_matching(int cur, int time)
{
	for (auto i : cartolot[cur])//갈 수 있는 모든 주차장에 대해
	{
		if (checked[i] || dist[cur][i] > time)//밀어내기가 순환되거나 제한시간 이상 걸리면
			continue;//패스
		checked[i] = true;//확인처리
		if (parkedcar[i] == -1 || bipartite_matching(parkedcar[i], time)) {
			parkedcar[i] = cur;//주차된 차가 없거나 밀어내고 다른 경우를 만들 수 있다면
			return true;//가능
		}
	}
	return false;//불가능
}

int main(void)
{
	ios::sync_with_stdio(false);
	cin.tie(0);

	memset(carloc, -1, sizeof(carloc));
	memset(lotloc, -1, sizeof(lotloc));
	cin >> r >> c;
	for (int i = 0; i < r; i++)
		for (int j = 0; j < c; j++)
		{
			char input;
			cin >> input;
			switch (input)
			{
			case 'C':
				map[i][j] = 1;
				carloc[i][j] = carcnt++;//위치에 차랑번호 기록
				break;
			case 'P':
				map[i][j] = 2;
				lotloc[i][j] = lotcnt++;//위치에 주자창번호 기록
				break;
			case '.':
				map[i][j] = 3;
				break;
			default:
				break;//벽이면 아무처리도 안함 -> 맵에서 0
			}
		}
	for (int i = 0; i < r; i++)
		for (int j = 0; j < c; j++)//각 차량에서 주차구역까지의 거리 계산
			if (carloc[i][j] != -1)
				bfs(i, j, carloc[i][j]);
	int low = 0, high = 2500;
	int ans;
	while (low <= high)//이분탐색
	{
		memset(parkedcar, -1, sizeof(parkedcar));//주차된 차 번호
		int mid = (low + high) / 2;
		int ret = 0;
		for (int i = 0; i < carcnt; i++)
		{//모든 차 주차
			memset(checked, 0, sizeof(checked));//밀어내기 초기화
			if (bipartite_matching(i, mid))
				ret++;
		}
		if (ret == carcnt) {//모든차가 주차되면 상한 낮춤
			ans = mid;
			high = mid - 1;
		}
		else//모든차를 주차할 수 없다면 그보다 더 높게 하한 높임
			low = mid + 1;
	}
	if (high == 2500)//처음부터 모든 차를 주차할 수 없다면 -1
		cout << -1;
	else
		cout << ans;
	return 0;
}
#elif other3
// #include <stdio.h>
// #include <string.h>
// #include <algorithm>
// #include <vector>
// #include <queue>

// #define INF (int)2e9

using namespace std;

struct Hopcroft {
	int n, m, limit;
	vector<vector<pair<int, int>>> G;
	vector<int> A, B, level;

	Hopcroft(int n, int m) : n(n), m(m) {
		G.resize(n + 1);
		A.resize(n + 1, -1);
		B.resize(m + 1, -1);
		level.resize(n + 1);
	}

	void add_edge(int u, int v, int weight) {
		G[u].push_back(make_pair(v, weight));
	}

	void bfs(void) {
		queue<int> Q;

		for (int i = 1; i <= n; i++) {
			if (A[i] == -1) {
				level[i] = 0;
				Q.push(i);
			}
			else
				level[i] = -1;
		}

		while (!Q.empty()) {
			int cur = Q.front();
			Q.pop();

			for (auto child : G[cur]) {
				if (child.second <= limit && B[child.first] != -1 && level[B[child.first]] == -1) {
					level[B[child.first]] = level[cur] + 1;
					Q.push(B[child.first]);
				}
			}
		}

		return;
	}

	bool dfs(int cur) {
		for (auto child : G[cur]) {
			if (child.second <= limit && (B[child.first] == -1 || level[B[child.first]] == level[cur] + 1 && dfs(B[child.first]))) {
				A[cur] = child.first;
				B[child.first] = cur;

				return true;
			}
		}

		return false;
	}

	int flow(int l) {
		int total_flow = 0;
		limit = l;

		fill(A.begin(), A.end(), -1);
		fill(B.begin(), B.end(), -1);

		while (1) {
			int flow = 0;

			bfs();

			for (int i = 1; i <= n; i++)
				if (A[i] == -1 && dfs(i))
					flow++;

			if (flow == 0)
				break;
			else
				total_flow += flow;
		}

		return total_flow;
	}
};

void bfs(int);

int N, M, R, C;
char input[52][52];
pair<int, int> car[101], park[101];
int visited[52][52];

int main(void)
{
	scanf("%d %d", &R, &C);

	for (int i = 1; i <= R; i++) {
		for (int j = 1; j <= C; j++) {
			scanf(" %c", &input[i][j]);

			if (input[i][j] == 'C') {
				N++;
				car[N] = make_pair(i, j);
			}

			if (input[i][j] == 'P') {
				M++;
				park[M] = make_pair(i, j);
			}
		}
	}

	if (N > M) {
		printf("%d\n", -1);
		return 0;
	}

	if (N == 0) {
		printf("%d\n", 0);
		return 0;
	}

	Hopcroft BM(N, M);

	for (int i = 1; i <= N; i++) {
		bfs(i);

		for (int j = 1; j <= M; j++)
			if (visited[park[j].first][park[j].second] != INF)
				BM.add_edge(i, j, visited[park[j].first][park[j].second]);

	}

	int low = 0, mid, high = 10000;
	int MIN = INF;

	while (low <= high) {
		mid = (low + high) >> 1;

		if (BM.flow(mid) == N) {
			MIN = mid;
			high = mid - 1;
		}
		else
			low = mid + 1;
	}

	if (MIN == INF)
		printf("%d\n", -1);
	else
		printf("%d\n", MIN);

	return 0;
}

void bfs(int n)
{
	queue<pair<int, int>> Q;

	int dx[4] = { 0, 1, 0, -1 };
	int dy[4] = { 1, 0, -1, 0 };

	for (int i = 1; i <= R; i++) {
		for (int j = 1; j <= C; j++) {
			if (input[i][j] == 'X')
				visited[i][j] = 0;
			else
				visited[i][j] = INF;
		}
	}

	visited[car[n].first][car[n].second] = 0;
	Q.push(car[n]);

	while (!Q.empty()) {
		int curx = Q.front().first;
		int cury = Q.front().second;
		Q.pop();

		for (int i = 0; i < 4; i++) {
			int nx = curx + dx[i];
			int ny = cury + dy[i];

			if (input[nx][ny] && visited[curx][cury] + 1 < visited[nx][ny]) {
				visited[nx][ny] = visited[curx][cury] + 1;
				Q.push(make_pair(nx, ny));
			}
		}
	}

	return;
}

#endif
}
