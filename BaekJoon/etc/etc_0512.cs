using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 알고스팟
    문제번호 : 1261번

    다익스트라, 최단경로, 0-1 너비 우선 탐색 문제다
    BFS 탐색을 약간 변형?했다 - 다익스트라?

    아이디어는 다음과 같다
    벽을 부순횟수를 가중치로 두고 가중치가 낮은 곳부터 먼저 탐색하게 했다
    가중치에 따라 먼저 탐색해야하므로 그냥 큐가아닌 우선순위 큐로 탐색을 했다

    다른 사람 풀이를 보고 나니, 우선순위 큐가 아닌 2개의 큐를 이용해서 풀 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0512
    {

        static void Main512(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int col = ReadInt();
            int row = ReadInt();

            int[,] board = new int[row, col];
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = -(sr.Read() - '0');
                }

                sr.ReadLine();
            }

            sr.Close();

            bool[,] visit = new bool[row, col];
            PriorityQueue<(int r, int c, int b), int> q = new(row * col);

            q.Enqueue((0, 0, 0), 0);

            visit[0, 0] = true;

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            while(q.Count > 0)
            {

                var node = q.Dequeue();

                for (int i = 0; i < 4; i++)
                {

                    int nextR = node.r + dirR[i];
                    int nextC = node.c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC) || visit[nextR, nextC]) continue;
                    visit[nextR, nextC] = true;

                    int nextB = node.b;
                    if (board[nextR, nextC] == -1) nextB++;
                    q.Enqueue((nextR, nextC, nextB), nextB);
                    board[nextR, nextC] = nextB;
                }
            }

            Console.WriteLine(board[row - 1, col - 1]);

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
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
#if ohter
using System;
using System.Collections.Generic;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

int m = ScanInt(), n = ScanInt();
var isEmpty = new bool[n, m];
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
        isEmpty[i, j] = sr.Read() == '0';
    if (sr.Read() == '\r')
        sr.Read();
}

var q = new Queue<ValueTuple<int, int>>();
var nextQ = new Queue<ValueTuple<int, int>>();
var visited = new bool[n, m];
var dirs = new ValueTuple<int, int>[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
q.Enqueue((0, 0));
var broken = 0;
do
{
    while (q.TryDequeue(out var coord))
    {
        if (coord == (n - 1, m - 1))
        {
            Console.Write(broken);
            return;
        }

        (var r, var c) = coord;
        foreach ((var ar, var ac) in dirs)
        {
            int nr = r + ar, nc = c + ac;
            if (!(0 <= nr && nr < n && 0 <= nc && nc < m) ||
                visited[nr, nc])
                continue;
            visited[nr, nc] = true;

            (isEmpty[nr, nc] ? q : nextQ).Enqueue((nr, nc));
        }
    }

    (q, nextQ) = (nextQ, q);
    broken++;
} while (true);

int ScanInt()
{
    int c, n = 0;
    while (!((c = sr.Read()) is ' ' or '\n'))
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        n = 10 * n + c - '0';
    }
    return n;
}
#elif other2
public class Program
{
    private static void Main(string[] args)
    {
        var size = Console.ReadLine().Split();
        var m = int.Parse(size[0]);
        var n = int.Parse(size[1]);

        var arr = new int[m, n];
        var result = new int[m, n];

        for (int i = 0; i < n; i++)
        {
            var column = Console.ReadLine().ToCharArray();
            for (int j = 0; j < column.Length; j++)
            {
                arr[j, i] = int.Parse(column[j].ToString());
                result[j, i] = -1;
            }
        }

        var start = (1, 1);
        var end = (m, n);

        if (start == end)
        {
            Console.WriteLine(0);
            return;
        }

        var dx = new int[] { -1, 1, 0, 0 };
        var dy = new int[] { 0, 0, -1, 1 };

        var list = new List<(int, int)>();
        list.Add((0, 0));
        result[0, 0] = 0;

        while (list.Count > 0)
        {
            var curr = list[0];
            list.RemoveAt(0);

            var x = curr.Item1;
            var y = curr.Item2;

            for (int i = 0; i < dx.Length; i++)
            {
                var nx = x + dx[i];
                var ny = y + dy[i];

                if (nx < 0 || nx >= m || ny < 0 || ny >= n) continue;
                if (result[nx, ny] == -1)
                {
                    if (arr[nx, ny] == 0)
                    {
                        result[nx, ny] = result[x, y];
                        list.Insert(0, (nx, ny));
                    }
                    else
                    {
                        result[nx, ny] = result[x, y] + 1;
                        list.Add((nx, ny));
                    }
                }
            }
        }

        Console.WriteLine(result[m - 1, n - 1]);
    }
}
#elif other3
using System;
using System.Collections.Generic;
class Program
{
    static int N, M;
    static int[,] map = new int[100, 100];
    static int[,] weight = new int[100, 100];
    static bool[,] check = new bool[100, 100];
    static Queue<Location> BFS = new Queue<Location>();
    static void Main()
    {
        var nm = Console.ReadLine().Split();
        N = int.Parse(nm[1]); M = int.Parse(nm[0]);
        for(int i = 0; i < N; i++)
        {
            string row = Console.ReadLine();
            for (int j = 0; j < M; j++)
            {
                map[i, j] = row[j] == '0' ? 0 : 1;
                weight[i, j] = 10000;
            }
        }
        check[0, 0] = true;
        weight[0, 0] = 0;
        BFS.Enqueue(new Location(0, 0));
        while (BFS.Count > 0)
        {
            int a = BFS.Peek().x;
            int b = BFS.Peek().y;
            Search(a, b);
            BFS.Dequeue();
        }
        Console.WriteLine(weight[N - 1, M - 1]);
    }
    static void DFS(int a, int b)
    {
        Search(a, b);
    }
    static void Search(int a, int b)
    {
        int[] dx = { -1, 0, 1, 0 };
        int[] dy = { 0, -1, 0, 1 };
        int x, y;
        for(int i = 0; i < 4; i++)
        {
            x = a + dx[i]; y = b + dy[i];
            if (x < 0 || y < 0 || x >= N || y >= M) continue;

            if (!check[x, y])
            {
                if(map[x,y] == 0)
                {
                    check[x, y] = true;
                    weight[x, y] = weight[a, b];
                    DFS(x, y);
                }
                else
                {
                    check[x, y] = true;
                    weight[x, y] = Math.Min(weight[a, b] + 1, weight[x, y]);
                    BFS.Enqueue(new Location(x, y));
                }
            }
        }
    }
}
public class Location
{
    public int x, y;
    public Location(int a, int b)
    {
        x = a;
        y = b;
    }
}
#endif
}
