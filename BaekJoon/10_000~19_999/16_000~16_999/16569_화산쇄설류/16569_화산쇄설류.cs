using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 7
이름 : 배성훈
내용 : 화산쇄설류
    문제번호 : 16569번

    구현, 그래프 이론, 그래프 탐색, 너비 우선 탐색 문제다.
    화산도 못지나간다는 부분을 캐치 못해 2번 틑렸다.

    아이디어는 사람이 이동한 시간이 짧으면 사람이 이동할 수 있는 구간이고,
    화산재가 빨리 덮이면 사람은 이동할 수 없는 구간이다. 화산 지점은 못지나가므로 못지나가게 반례처리 한다.

    사람이 화산 지점을 제외하고 모든 지점을 이동하고 턴을 기록한다.

    그리고 이후 화산재로 덮이는 시간을 구했다.
    만약 해당 10턴에 화산이 화산재로 덮이면 10턴 이후에 화산이 터지는 것은 탐색할 필요가 없다.
    그래서 화산에도 화산재가 덮이게 설정했고 BFS로 가장 빠르게 덮이는 시간을 구했다.

    이후 사람이 지나간 곳에 한해 가장 높은 곳과 최단 턴을 찾으니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1098
    {

        static void Main1098(string[] args)
        {

            int INF = 10_000;

            int row, col, v;
            int[][] height, moveTurn, volTurn;
            (int r, int c, int turn)[] vol;
            (int r, int c) init;

            bool[][] volVisit, moveVisit;

            Queue<(int r, int c, int turn)> q;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                q = new(row * col);
                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                
                MoveBFS();
                VolBFS();

                int retMax = 0;
                int retTurn = INF;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (!moveVisit[r][c] || volTurn[r][c] <= moveTurn[r][c] || height[r][c] < retMax) continue;
                        if (retMax < height[r][c])
                        {

                            retMax = height[r][c];
                            retTurn = moveTurn[r][c];
                        }
                        else retTurn = Math.Min(moveTurn[r][c], retTurn);
                    }
                }

                Console.Write($"{retMax} {retTurn}");

                void VolBFS()
                {

                    Array.Sort(vol, (x, y) => x.turn.CompareTo(y.turn));

                    int idx = 0;
                    int turn = 0;
                    while (idx < v || q.Count != 0)
                    {

                        while (idx < v && vol[idx].turn == turn)
                        {

                            (int r, int c, int turn) node = vol[idx++];
                            moveVisit[node.r][node.c] = false;
                            if (volVisit[node.r][node.c]) continue;
                            volVisit[node.r][node.c] = true;
                            q.Enqueue(node);
                        }

                        while (q.Count > 0 && q.Peek().turn == turn)
                        {

                            (int r, int c, int turn) node = q.Dequeue();

                            for (int i = 0; i < 4; i++)
                            {

                                int nR = node.r + dirR[i];
                                int nC = node.c + dirC[i];

                                if (ChkInvalidPos(nR, nC) || volVisit[nR][nC]) continue;
                                volVisit[nR][nC] = true;
                                volTurn[nR][nC] = turn + 1;
                                q.Enqueue((nR, nC, turn + 1));
                            }
                        }

                        turn++;
                    }
                }

                void MoveBFS()
                {

                    q.Enqueue((init.r, init.c, 0));
                    moveVisit[init.r][init.c] = true;
                    while (q.Count > 0)
                    {

                        (int r, int c, int turn) node = q.Dequeue();
                        
                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];

                            if (ChkInvalidPos(nR, nC) || moveVisit[nR][nC]) continue;
                            moveVisit[nR][nC] = true;
                            moveTurn[nR][nC] = node.turn + 1;
                            q.Enqueue((nR, nC, node.turn + 1));
                        }
                    }
                }

                bool ChkInvalidPos(int _r, int _c) => _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();
                v = ReadInt();

                init = (ReadInt() - 1, ReadInt() - 1);

                height = new int[row][];
                moveTurn = new int[row][];
                volTurn = new int[row][];

                moveVisit = new bool[row][];
                volVisit = new bool[row][];

                for (int r = 0; r < row; r++)
                {

                    height[r] = new int[col];

                    moveTurn[r] = new int[col];
                    moveVisit[r] = new bool[col];

                    volTurn[r] = new int[col];
                    volVisit[r] = new bool[col];

                    for (int c = 0; c < col; c++)
                    {

                        height[r][c] = ReadInt();
                    }
                }

                vol = new (int r, int c, int turn)[v];
                for (int i = 0; i < v; i++)
                {

                    vol[i] = (ReadInt() - 1, ReadInt() - 1, ReadInt());
                    moveTurn[vol[i].r][vol[i].c] = INF;
                    moveVisit[vol[i].r][vol[i].c] = true;
                }

                sr.Close();

                int ReadInt()
                {

                    int ret = 0;
                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <vector>
// #include <algorithm>
// #include <queue>
// #include <functional>
// #include <stack>
// #include <deque>
// #include <string>
// #include <cmath>
// #include <map>
// #include <set>
// #include <climits>
using namespace std;
typedef long long ll;

struct state {
	int x, y, t;
};

int M, N, V, X, Y;
int vis[100][100];
int arr[100][100];
state vol[5000];

bool check(int x, int y, int t) {
	if (x < 0 || x >= M || y < 0 || y >= N) return false;
	if (vis[x][y] == 0) {
		for (int i = 0; i < V; i++) {
			if (t <= vol[i].t) continue;
			int d = t - vol[i].t;
			int dist = abs(x - vol[i].x) + abs(y - vol[i].y);
			if (d >= dist) {
				vis[x][y] = -2;
				return false;
			}
		}
		return true;
	}
	return false;
}

int main() {
	ios_base::sync_with_stdio(0); cin.tie(0);

	cin >> M >> N >> V;
	cin >> X >> Y; 
	X--; Y--;
	for (int i = 0; i < M; i++) {
		for (int j = 0; j < N; j++) {
			cin >> arr[i][j];
		}
	}
	int x, y, t;
	for (int i = 0; i < V; i++) {
		cin >> x >> y >> t;
		vol[i] = { x-1,y-1,t+1 }; // 첫 시각 1로 잡아서
		vis[x-1][y-1] = -1;
	}

	int dx[4] = { 1,-1,0,0 };
	int dy[4] = { 0,0,1,-1 };

	queue<state> q;
	q.push({ X,Y,1 });
	vis[X][Y] = 1;
	pair<int, int> ans = { arr[X][Y], 0};
	while (!q.empty()) {
		state cur = q.front(); q.pop();
		for (int i = 0; i < 4; i++) {
			int nx = cur.x + dx[i];
			int ny = cur.y + dy[i];
			if (check(nx, ny, cur.t+1)) {
				vis[nx][ny] = cur.t + 1;
				q.push({ nx, ny, cur.t + 1 });
				if (arr[nx][ny] > ans.first) {
					ans = { arr[nx][ny], cur.t };
				}
			}
		}
	}
	
	/*cout << "vis:\n";
	for (int i = 0; i < M; i++) {
		for (int j = 0; j < N; j++) {
			cout << vis[i][j] << " ";
		}
		cout << endl;
	}*/

	cout << ans.first << " " << ans.second;
}
#elif other2
using System;
using System.Collections.Generic;

public class Program
{
    struct Pair
    {
        public int r, c;
        public Pair(int r, int c)
        {
            this.r = r; this.c = c;
        }
    }
    static void Main()
    {
        int[] nmv = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int n = nmv[0], m = nmv[1], v = nmv[2];
        string[] xy = Console.ReadLine().Split(' ');
        int x = int.Parse(xy[0]) - 1, y = int.Parse(xy[1]) - 1;
        int[,] height = new int[m, n];
        for (int i = 0; i < m; i++)
        {
            int[] row = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            for (int j = 0; j < n; j++)
            {
                height[i, j] = row[j];
            }
        }
        (int r, int c, int t)[] volcano = new (int r, int c, int t)[v];
        for (int i = 0; i < v; i++)
        {
            int[] rct = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int r = rct[0] - 1, c = rct[1] - 1, t = rct[2];
            volcano[i] = (r, c, t);
        }
        int[] dx = { -1, 1, 0, 0 },
              dy = { 0, 0, -1, 1 };
        int[,] visited = new int[m, n];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                visited[i, j] = -1;
            }
        }
        Queue<Pair> queue = new();
        visited[x, y] = 0;
        queue.Enqueue(new(x, y));
        int ans = height[x, y], wer = 0;
        while (queue.Count > 0)
        {
            Pair cur = queue.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                int nr = cur.r + dx[i], nc = cur.c + dy[i];
                if (nr < 0 || nr >= m || nc < 0 || nc >= n || visited[nr, nc] != -1)
                    continue;
                int t = visited[cur.r, cur.c] + 1;
                for (int j = 0; j < v; j++)
                {
                    if (Math.Abs(nr - volcano[j].r) + Math.Abs(nc - volcano[j].c) <= Math.Max(0, t - volcano[j].t))
                        goto Continue;
                }
                if (height[nr, nc] > ans)
                {
                    ans = height[nr, nc];
                    wer = t;
                }
                visited[nr, nc] = t;
                queue.Enqueue(new(nr, nc));
            Continue:;
            }
        }
        Console.Write($"{ans} {wer}");
    }
}
#endif
}
