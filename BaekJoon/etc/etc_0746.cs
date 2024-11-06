using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 31
이름 : 배성훈
내용 : 로봇 청소기
    문제번호 : 4991번

    브루트 포스, BFS, 비트마스킹 문제다
    각 먼지와 청소기에 한해 BFS를 시행하며 먼지간 최단 거리를 재었다
    O(row * col)의 시간이 걸린다

    A -> F로 갈 때, 6 곳을 방문한 뒤 F로 간다고 가정하면
    A -> B -> C -> D -> E -> F, A -> E -> B -> C -> D -> F가 있고
    사이의 경로 B -> C -> D 처럼 반복되는 것이 보인다

    dp는 앞의 idx는 마지막에 도착지점, 뒤의 idx는 현재 도착한 곳의 상태를 나타내고
    값은 최단 경로가되게 dp를 설정했다

    여기서 노드들을 걸친 최단 거리는 A -> E와 B -> E의 거리는 일반적으로 다르기에
    A -> B -> C에서 B, C를 거쳐 A -> D로가는 최단 경로를 구할 때
    A -> C -> B -> D, A -> B -> C -> D를 비교해 최단 경로를 넣어줘야한다
    A -> B -> C인 최단 경로에서 B -> D인 경우가 짧아 둘이 합치면 안된다!
    이렇게 모든 경로를 탐색하며 합쳐갔다;
    노드들을 걸쳐 가는 최단 거리는 이 방법 이외에는 모르겠다;

    이렇게 제출하니 92ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0746
    {

        static void Main746(string[] args)
        {

            int MAX = 20;

            StreamReader sr;
            StreamWriter sw;

            int row, col;
            int[][] board;
            bool[][] visit;

            Queue<(int r, int c)> q;
            (int r, int c)[] arr;
            int[][] dis;
            int n;

            int[] dirR, dirC;
            int[][] dp;
            
            Solve();

            void Solve()
            {

                Init();

                while (SetSize())
                {

                    SetBoard();

                    if (GetDis())
                    {

                        sw.Write($"-1\n");
                        continue;
                    }

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            bool SetSize()
            {

                col = ReadInt();
                row = ReadInt();

                return row != 0 || col != 0;
            }

            void SetBoard()
            {

                n = 0;
                for (int r = 0; r < row; r++) 
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == '.') board[r][c] = 0;
                        else if (cur == 'x') board[r][c] = -1;
                        else if (cur == 'o')
                        {

                            board[r][c] = 0;
                            arr[0] = (r, c);
                        }
                        else
                        {

                            board[r][c] = 0;
                            arr[++n] = (r, c);
                        }
                    }

                    if (sr.Read() == '\r') sr.Read();
                }
            }

            bool GetDis()
            {

                for (int i = 0; i <= n; i++)
                {

                    
                    if (BFS(i)) continue;
                    return true;
                }

                return false;
            }

            bool BFS(int _idx)
            {

                q.Enqueue((arr[_idx].r, arr[_idx].c));

                while (q.Count > 0)
                {

                    var node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC]) continue;
                        visit[nextR][nextC] = true;
                        if (board[nextR][nextC] == -1) continue;

                        board[nextR][nextC] = board[node.r][node.c] + 1;
                        q.Enqueue((nextR, nextC));
                    }
                }

                bool ret = true;
                for (int i = 0; i <= n; i++)
                {

                    if (i == _idx) continue;
                    int cur = board[arr[i].r][arr[i].c];
                    dis[_idx][i] = cur;
                    if (cur == 0) ret = false;
                }

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (!visit[r][c]) continue;
                        visit[r][c] = false;
                        if (board[r][c] == -1) continue;
                        board[r][c] = 0;
                    }
                }

                return ret;
            }

            void GetRet()
            {

                int len = 1 << (n + 1);

                for (int i = 0; i <= n; i++) 
                { 
                    
                    Array.Fill(dp[i], -1, 0, len); 
                }

                int ret = DFS(0, 0, 1);

                sw.Write($"{ret}\n");
            }

            int DFS(int _depth, int _n, int _state)
            {

                if (_depth == n) return 0;
                if (dp[_n][_state] != -1) return dp[_n][_state];
                int ret = 100_000_000;
                dp[_n][_state] = 0;

                for (int i = 1; i <= n; i++)
                {

                    if ((_state & (1 << i)) != 0) continue;

                    int chk = DFS(_depth + 1, i, _state | 1 << i);
                    ret = Math.Min(ret, chk + dis[_n][i]);
                }

                dp[_n][_state] = ret;
                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void Init() 
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                board = new int[MAX][];
                visit = new bool[MAX][];

                for (int r = 0; r < MAX; r++)
                {

                    board[r] = new int[MAX];
                    visit[r] = new bool[MAX];
                }

                q = new(MAX * MAX);
                arr = new (int r, int c)[11];
                dis = new int[11][];

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                dp = new int[11][];
                for (int i = 0; i <= 10; i++)
                {

                    dp[i] = new int[1 << 12];
                    dis[i] = new int[11];
                }
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
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static int w;
        static int h;
        static int dcount;
        static int[,] map;
        static (int, int)[] dirty;
        static int[,] dist;
        static int answer;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb = new StringBuilder();
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            while (arr[0] > 0)
            {
                answer = 99999;
                w = arr[0]; h = arr[1];
                map = new int[h, w];
                dirty = new (int, int)[11];
                dist = new int[11, 11];
                dcount = 0;
                for(int i = 0; i < h; i++)
                {
                    string s = input.ReadLine();
                    for(int j = 0; j < w; j++)
                    {
                        if (s[j] == 'o')
                        {
                            dirty[0] = (i, j);
                        }
                        else if (s[j] == 'x')
                            map[i, j] = 1;
                        else if (s[j] == '*')
                        {
                            dirty[++dcount] = (i, j);
                            map[i, j] = 2;
                        }
                    }
                }
                for (int i = 0; i <= dcount; i++)
                    bfs(i);
                bool check = false;
                for(int i = 1; i <= dcount; i++)
                {
                    if (dist[0,i] == 0)
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    sb.Append("-1\n");
                }
                else
                {
                    bool[] visit = new bool[dcount + 1];
                    visit[0] = true;
                    dfs(0, 0, 0, visit);
                    sb.Append($"{answer}\n");
                }
                arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            }

            output.Write(sb);
            input.Close();
            output.Close();
        }
        static void bfs(int index)
        {
            bool[,] visit = new bool[h, w];
            int[,] d = new int[h, w];
            int[] dy = new int[] { 0, 1, 0, -1 };
            int[] dx = new int[] { 1, 0, -1, 0 };

            Queue<(int, int, int)> q = new();
            q.Enqueue((dirty[index].Item1, dirty[index].Item2, 0));
            visit[dirty[index].Item1, dirty[index].Item2] = true;
            while (q.Count > 0)
            {
                (int row, int col, int count) = q.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    int nr = row + dy[i];
                    int nc = col + dx[i];
                    if (nr < 0 || nr == h || nc < 0 || nc == w || visit[nr, nc] || map[nr, nc] == 1) continue;
                    q.Enqueue((nr, nc, count + 1));
                    visit[nr, nc] = true;
                    d[nr, nc] = count + 1;
                }
            }
            for(int i = index + 1; i <= dcount; i++)
            {
                dist[index, i] = d[dirty[i].Item1, dirty[i].Item2];
                dist[i,index] = d[dirty[i].Item1, dirty[i].Item2];
            }
        }
        static void dfs(int index,int sum,int count, bool[] visit)
        {
            if (sum > answer) return;
            if(count == dcount)
            {
                answer = Math.Min(answer, sum);
                return;
            }

            for(int i = 0; i <= dcount; i++)
            {
                if (visit[i]) continue;
                visit[i] = true;
                dfs(i, sum + dist[index, i], count + 1, visit);
                visit[i] = false;
            }
        }
    }
}
#elif other2
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace BOJ
{
    class BOJ4491
    {
        static int W, H, ans, dust_count;
        static char[,] room;
        static Coordinate Robot = new Coordinate();
        static List<Coordinate> Dusts;
        static int[] RobotToDusts;
        static int[,] DustsToDusts;
        static int[,] Delta = { { 0, -1 }, { -1, 0 }, { 0, 1 }, { 1, 0 } };

        static StringBuilder sb = new StringBuilder();
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

        public static void Start()
        {
            string[] inputs = sr.ReadLine().Split();
            W = int.Parse(inputs[0]);
            H = int.Parse(inputs[1]);

            while (!(W == 0 && H == 0))
            {
                Set();
                CalcRobotToDusts();

                bool flag = true;
                foreach (int i in RobotToDusts)
                {
                    if (i == -1)
                    {
                        sb.Append("-1\n");
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    DustsToDusts = new int[dust_count, dust_count];
                    for (int i = 0; i < dust_count; i++)
                    {
                        CalcDustsToDusts(i);
                    }
                    DFS(0, 0, 0, 0);
                    sb.AppendFormat("{0}\n", ans);
                }

                // 마지막
                inputs = sr.ReadLine().Split();
                W = int.Parse(inputs[0]);
                H = int.Parse(inputs[1]);
            }

            sw.WriteLine(sb.ToString());
            sb.Clear();
            sw.Flush();
        }

        static void DFS(int chosen, int subsum, int visit, int last)
        {
            if (chosen == dust_count)
            {
                ans = Math.Min(ans, subsum);
                return;
            }
            if (subsum >= ans) return;

            for (int i = 0; i < dust_count; i++)
            {
                if ((visit & (1 << i)) == 0)
                {
                    if (chosen == 0) DFS(chosen + 1, subsum + RobotToDusts[i], visit | (1 << i), i);
                    else DFS(chosen + 1, subsum + DustsToDusts[last, i], visit | (1 << i), i);
                }
            }
        }

        static void CalcDustsToDusts(int start)
        {
            Coordinate Start = Dusts[start];
            bool[,] visited = new bool[H, W];
            visited[Start.row, Start.col] = true;

            Queue<Coordinate> que = new Queue<Coordinate>();
            que.Enqueue(new Coordinate { row = Start.row, col = Start.col, distance = 0 });

            while (que.Count > 0)
            {
                Coordinate now = que.Dequeue();

                int nextRow, nextCol, nextDis;
                for (int i = 0; i < 4; i++)
                {
                    nextRow = now.row + Delta[i, 0];
                    nextCol = now.col + Delta[i, 1];
                    nextDis = now.distance + 1;

                    if (nextRow >= 0 && nextRow < H && nextCol >= 0 && nextCol < W && !visited[nextRow, nextCol] && (room[nextRow, nextCol] == '.' || room[nextRow, nextCol] == '*'))
                    {
                        visited[nextRow, nextCol] = true;
                        que.Enqueue(new Coordinate { row = nextRow, col = nextCol, distance = nextDis });

                        if (room[nextRow, nextCol] == '*')
                        {
                            for (int k = 0; k < dust_count; k++)
                            {
                                if (Dusts[k].row == nextRow && Dusts[k].col == nextCol)
                                {
                                    DustsToDusts[start, k] = nextDis;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        static void CalcRobotToDusts()
        {
            RobotToDusts = new int[dust_count];
            for (int i = 0; i < dust_count; i++)
                RobotToDusts[i] = -1;

            bool[,] visited = new bool[H, W];
            visited[Robot.row, Robot.col] = true;

            Queue<Coordinate> que = new Queue<Coordinate>();
            que.Enqueue(new Coordinate { row = Robot.row, col = Robot.col, distance = 0 });

            while (que.Count > 0)
            {
                Coordinate now = que.Dequeue();

                int nextRow, nextCol, nextDis;
                for (int i = 0; i < 4; i++)
                {
                    nextRow = now.row + Delta[i, 0];
                    nextCol = now.col + Delta[i, 1];
                    nextDis = now.distance + 1;

                    if (nextRow >= 0 && nextRow < H && nextCol >= 0 && nextCol < W && !visited[nextRow, nextCol] && (room[nextRow, nextCol] == '.' || room[nextRow, nextCol] == '*'))
                    {
                        visited[nextRow, nextCol] = true;
                        que.Enqueue(new Coordinate { row = nextRow, col = nextCol, distance = nextDis });

                        if (room[nextRow, nextCol] == '*')
                        {
                            for (int k = 0; k < dust_count; k++)
                            {
                                if (Dusts[k].row == nextRow && Dusts[k].col == nextCol)
                                {
                                    RobotToDusts[k] = nextDis;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Set()
        {
            room = new char[H, W];
            Dusts = new List<Coordinate> { };
            ans = int.MaxValue;
            dust_count = 0;

            for (int i = 0; i < H; i++)
            {
                string input = sr.ReadLine();

                for (int j = 0; j < W; j++)
                {
                    if (input[j] == 'o')
                    {
                        Robot.row = i; Robot.col = j;
                        room[i, j] = '.';
                        continue;
                    }

                    if (input[j] == '*')
                    {
                        Dusts.Add(new Coordinate { row = i, col = j });
                        dust_count++;
                    }
                    room[i, j] = input[j];
                }
            }
        }
    }

    public struct Coordinate
    {
        public int row { get; set; }
        public int col { get; set; }
        public int distance { get; set; }
    }

    class Program
    {
        static void Main()
        {
            BOJ4491.Start();
        }
    }
}

#elif other3
// #include <cstdio>
// #include <cstring>
// #include <queue>
// #include <functional>
using namespace std;

constexpr int INF = 1e9 + 7;
struct pii { int x, y; };

int main() {
	for (int n, m; scanf("%d%d", &m, &n) && n;) {
		//input
		char board[n][m + 1];
		for (int i = 0; i < n; i++) scanf("%s", board[i]);

		//numbering
        pii num[11]; int sz = 1;
		for (int i = 0; i < n; i++) for (int j = 0; j < m; j++) {
			if (board[i][j] == 'o') num[0] = { i, j };
			if (board[i][j] == '*') num[sz++] = { i, j };
		}

		//graph_construction
        int adj[11][11]; memset(adj, -1, sizeof adj);
		for (int i = 0; i < sz; i++) {
			int dist[n * m]; memset(dist, -1, sizeof dist); queue<pii> Q;
			dist[num[i].x * m + num[i].y] = 0; Q.push(num[i]);
			while (Q.size()) {
				auto [x, y] = Q.front(); Q.pop();
				for (int k = 0; k < 4; k++) {
					int nx = x + "1012"[k] - '1';
					int ny = y + "2101"[k] - '1';
					if (nx < 0 || nx >= n || ny < 0 || ny >= m) continue;
					if (board[nx][ny] == 'x' || ~dist[nx * m + ny]) continue;
					dist[nx * m + ny] = dist[x * m + y] + 1; Q.push({ nx, ny });
				}
			}
			for (int j = 0; j < sz; j++) {
				if (dist[num[j].x * m + num[j].y] == -1) continue;
				adj[i][j] = dist[num[j].x * m + num[j].y];
			}
		}

		//TSP
        int DP[11][1 << 11]; memset(DP, 0, sizeof DP);
		function<int(int, int)> DFS = [&](int cur, int state) -> int {
			if (state == (1 << sz) - 1) return 0;
			int& ret = DP[cur][state];
			if (ret) return ret; ret = INF;
			for (int i = 0; i < sz; i++) {
				if (state & 1 << i || adj[cur][i] == -1) continue;
				ret = min(ret, DFS(i, state | 1 << i) + adj[cur][i]);
			}
			return ret;
		};
		if (int ans = DFS(0, 1); ans == INF) puts("-1");
		else printf("%d\n", ans);
	}
}
#endif

}
