using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 23
이름 : 배성훈
내용 : 다리 만들기
    문제번호 : 2146번

    BFS 문제다
    섬에 대해 각각 다른 섬들과의 거리를 모두 BFS로 찾으니 1.3초 걸렸다;
    맵의 최대 크기에 체스판처럼 섬이 배치된 경우가 있어 보인다

    그래서 dis 메모리를 늘려서 출발한 섬을 기록하고 동시에 BFS를 진행하며
    다른 섬에서 도달한 곳에 한해 최소 거리를 결과로 저장했다

    이렇게 하니 1344 -> 72ms로 줄었다
*/

namespace BaekJoon.etc
{
    internal class etc_0719
    {

        static void Main719(string[] args)
        {

            StreamReader sr;

            int[][] board;
            int size;
            int area;
            int[] dirR, dirC;

            (int n, int s)[][] dis;
            int ret = 0;
            Queue<(int r, int c)> q;
            Solve();

            void Solve()
            {

                Input();

                SetArea();

                ret = 100_000;
                SetDis();
                ret -= 2;
                Console.WriteLine(ret);
            }

            void SetDis()
            {

                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        if (board[r][c] <= 0) continue;
                        q.Enqueue((r, c));
                        dis[r][c] = (1, board[r][c]);
                    }
                }

                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    int s = dis[node.r][node.c].s;
                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC)) continue;
                        if (dis[nextR][nextC].n == 0)
                        {

                            dis[nextR][nextC] = (dis[node.r][node.c].n + 1, s);
                            q.Enqueue((nextR, nextC));
                        }
                        else if (dis[nextR][nextC].s != s)
                        {

                            int chk = dis[node.r][node.c].n + dis[nextR][nextC].n;
                            ret = Math.Min(chk, ret);
                        }
                    }
                }
            }

            void SetArea()
            {

                area = 0;
                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        if (board[r][c] != 0) continue;

                        q.Enqueue((r, c));
                        board[r][c] = ++area;

                        while (q.Count > 0)
                        {

                            var node = q.Dequeue();

                            for (int i = 0; i < 4; i++)
                            {

                                int nextR = node.r + dirR[i];
                                int nextC = node.c + dirC[i];

                                if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] != 0) continue;
                                board[nextR][nextC] = area;

                                q.Enqueue((nextR, nextC));
                            }
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= size || _c >= size) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = ReadInt();
                board = new int[size][];
                dis = new (int n, int s)[size][];

                for (int r = 0; r < size; r++)
                {

                    board[r] = new int[size];
                    dis[r] = new (int n, int s)[size];

                    for (int c = 0; c < size; c++)
                    {

                        board[r][c] = ReadInt() - 1;
                    }
                }

                q = new(size * size);
                dirR = new int[4] { 0, -1, 0, 1 };
                dirC = new int[4] { -1, 0, 1, 0 };
                sr.Close();
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
namespace BOJ
{
    class Program
    {
        static void Main()
        {
            using StreamReader input = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            using StreamWriter output = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // 입력받기
            // N 입력
            // N,N 배열 입력
            // 0 : 바다, 1 : 육지

            // 육지일때는 패스하고 바다일떄

            // bfs돌면서 1씩 저장시키면서 저장
            // 다른 섬을 만났을때 break

            // 다른섬인지 또 BFS로 체크해줘야겠네?

            int N = int.Parse(input.ReadLine());

            int[,] board = new int[N, N];

            bool[,] visited = new bool[N, N];
            int[,] dist = new int[N, N];

            int[] dx = new int[] { 1, 0, -1, 0 };
            int[] dy = new int[] { 0, 1, 0, -1 };

            
            Queue<(int, int)> q2 = new Queue<(int, int)>();

            for(int x = 0 ; x < N ; x++)
            {
                int[] inputs = Array.ConvertAll(input.ReadLine().Split(), int.Parse);
                for(int y = 0 ; y < N ; y++)
                {
                    board[x, y] = inputs[y];
                }
            }

            int island_count = 1;
            for(int x = 0 ; x < N ; x++)
            {
                for(int y = 0 ; y < N ; y++)
                {
                    if(visited[x, y] || board[x, y] == 0) continue;

                    Queue<(int, int)> q1 = new Queue<(int, int)>();
                    q1.Enqueue((x, y));
                    visited[x, y] = true;

                    while(q1.Count > 0)
                    {
                        var cur = q1.Dequeue();
                        board[cur.Item1, cur.Item2] = island_count;

                        for(int dir = 0 ; dir < 4 ; dir++)
                        {
                            int nx = cur.Item1 + dx[dir];
                            int ny = cur.Item2 + dy[dir];

                            if(nx < 0 || nx >= N || ny < 0 || ny >= N) continue;
                            if(visited[nx, ny] || board[nx, ny] == 0) continue;

                            q1.Enqueue((nx, ny));
                            visited[nx, ny] = true;
                        }
                    }

                    island_count++;
                }
            }

            for(int x = 0 ; x < N ; x++)
            {
                for(int y = 0 ; y < N ; y++)
                {
                    dist[x, y] = -1;
                }
            }

            for(int x = 0 ; x < N ; x++)
            {
                for(int y = 0 ; y < N ; y++)
                {
                    if(board[x, y] != 0)
                    {
                        dist[x, y] = 0;
                        q2.Enqueue((x, y));
                    }
                }
            }

            int myMin = int.MaxValue;
            while(q2.Count > 0)
            {
                var cur = q2.Dequeue();

                for(int dir = 0 ; dir < 4 ; dir++)
                {
                    int nx = cur.Item1 + dx[dir];
                    int ny = cur.Item2 + dy[dir];

                    if(nx < 0 || nx >= N || ny < 0 || ny >= N) continue;
                    if(board[nx, ny] == board[cur.Item1, cur.Item2]) continue;

                    if(board[nx, ny] != 0)
                    {
                        myMin = Math.Min(myMin, dist[nx, ny] + dist[cur.Item1, cur.Item2]);
                    }
                    else
                    {
                        board[nx, ny] = board[cur.Item1, cur.Item2];
                        dist[nx, ny] = dist[cur.Item1, cur.Item2] + 1;
                        q2.Enqueue((nx, ny));
                    }
                }
            }

            output.WriteLine(myMin);
        }
    }
}

#elif other2
var reader = new Reader();
int n = reader.NextInt();

var map = new int[n, n];
for (int i = 0; i < n; i++)
    for (int j = 0; j < n; j++)
        map[i, j] = reader.NextInt();

var dir = new (int x, int y)[4] { (1, 0), (-1, 0), (0, 1), (0, -1) };

int islandCount = 0;
var q = new Queue<(int x, int y)>();
var visited = new bool[n, n];

for (int i = 0; i < n; i++)
for (int j = 0; j < n; j++)
{
    if (map[i, j] == 0 || visited[i, j])
        continue;

    map[i, j] = islandCount + 1;
    visited[i, j] = true;
    q.Enqueue((i, j));

    while (q.Count > 0)
    {
        var c = q.Dequeue();
        foreach (var d in dir)
        {
            var (dx, dy) = (c.x + d.x, c.y + d.y);
            if (dx < 0 || dx >= n || dy < 0 || dy >= n)
                continue;
            
            if (map[dx, dy] != 1 || visited[dx, dy])
                continue;

            map[dx, dy] = islandCount + 1;
            visited[dx, dy] = true;
            q.Enqueue((dx, dy));
        }
    }

    islandCount++;
}

int shortest = (int)1e9;
var dist = new int[n, n];
var dq = new Queue<(int x, int y)>();

for (int i = 0; i < n; i++)
for (int j = 0; j < n; j++)
{
    if (map[i, j] == 0)
        continue;

    dist[i, j] = 1;
    q.Enqueue((i, j));
    dq.Enqueue((i, j));

    int s = (int)1e9;
    while (q.Count > 0)
    {
        var c = q.Dequeue();
        
        if (map[c.x, c.y] != 0 && map[c.x, c.y] != map[i, j])
        {
            s = dist[c.x, c.y] - 2;
            // Console.WriteLine($"{(i,j)}[{map[i,j]}] -> {c}[{map[c.x,c.y]}] [{s}]");
            break; 
        }

        foreach (var d in dir)
        {
            var (dx, dy) = (c.x + d.x, c.y + d.y);
            if (dx < 0 || dx >= n || dy < 0 || dy >= n)
                continue;
            
            if (map[dx, dy] == map[i, j] || dist[dx, dy] != 0)
                continue;

            dist[dx, dy] = dist[c.x, c.y] + 1;
            q.Enqueue((dx, dy));
            dq.Enqueue((dx, dy));
        }
    }

    shortest = Math.Min(shortest, s);
    while (dq.Count > 0)
    {
        var dc = dq.Dequeue();
        dist[dc.x, dc.y] = 0;
    }
    q.Clear();
}

Console.Write(shortest);
#endif
}
