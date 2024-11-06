using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 6 
이름 : 배성훈
내용 : 연구소 2
    문제번호 : 17141번

    BFS, 브루트 포스 문제다
    시작지점 방문 처리를 안해 1번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0799
    {

        static void Main799(string[] args)
        {

            int MAX = 10_000;
            StreamReader sr;
            Queue<(int r, int c)> q;
            int n, m;
            int[][] board;
            (int r, int c)[] pos;
            bool[][] visit;
            int[] arr;
            int[] dirR, dirC;

            Solve();

            void Solve()
            {

                Input();

                int ret = DFS(0, 0);

                if (ret == MAX) Console.Write(-1);
                else Console.Write(ret);
            }

            int DFS(int _depth, int _s)
            {

                if (_depth == m) return BFS();

                int ret = MAX;

                for (int i = _s; i < pos.Length; i++)
                {

                    arr[_depth] = i;
                    int chk = DFS(_depth + 1, i + 1);
                    if (chk < ret) ret = chk;
                }

                return ret;
            }

            int BFS()
            {

                for (int i = 0; i < m; i++)
                {

                    int r = pos[arr[i]].r;
                    int c = pos[arr[i]].c;

                    q.Enqueue((r, c));
                    board[r][c] = 1;
                    visit[r][c] = true;
                }

                while(q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();

                    int cur = board[node.r][node.c];

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC]) continue;
                        visit[nextR][nextC] = true;

                        if (board[nextR][nextC] == -1) continue;
                        board[nextR][nextC] = cur + 1;
                        q.Enqueue((nextR, nextC));
                    }
                }

                int ret = 0;
                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] == -1) continue;
                        visit[r][c] = false;
                        int cur = board[r][c];
                        board[r][c] = 0;
                        if (cur == 0) cur = 10_001;
                        ret = Math.Max(cur, ret);
                        
                    }
                }

                ret--;
                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= n || _c >= n) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                arr = new int[m];

                q = new(n * n);
                board = new int[n][];
                visit = new bool[n][];

                int len = 0;
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    visit[r] = new bool[n];

                    for (int c = 0; c < n; c++)
                    {

                        int cur = ReadInt();
                        if (cur == 2) len++;
                        else if (cur == 1) cur = -1;
                        board[r][c] = cur;
                    }
                }

                pos = new (int r, int c)[len];
                len = 0;
                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] != 2) continue;
                        board[r][c] = 0;
                        pos[len++] = (r, c);
                    }
                }

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
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
using System.Numerics;
using System.Runtime.CompilerServices;

var sr = new StreamReader(Console.OpenStandardInput());
var sw = new StreamWriter(Console.OpenStandardOutput());

var inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
var N = inputs[0];
var M = inputs[1];
var board = new int[N, N];
var virusList = new List<(int y, int x)>();
var zeroCount = 0;
for (var y = 0; y < N; y++)
{
    inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    for (var x = 0; x < N; x++)
    {
        board[y, x] = inputs[x];
        if (inputs[x] == 0 || inputs[x] == 2) zeroCount++;
        if (inputs[x] == 2)
        {
            virusList.Add((y, x));
            board[y, x] = 0;
        }
    }
}

var dx = new int[4] { 0, 0, 1, -1 };
var dy = new int[4] { 1, -1, 0, 0 };
var totalVirusLength = virusList.Count;
var comb = MakeComb(totalVirusLength, M);
var result = int.MaxValue;

while (comb != null)
{
    BFS(comb);
    comb = Successor(comb, totalVirusLength, M);
}

void BFS(int[] virusArray)
{
    var q = new Queue<(int y, int x, int depth)>();
    var visited = new bool[N, N];

    foreach (var virusIndex in virusArray)
    {
        var virus = virusList[virusIndex];
        q.Enqueue((virus.y, virus.x, 0));
        visited[virus.y, virus.x] = true;
    }

    var count = 0;
    while (q.Count > 0)
    {
        var current = q.Dequeue();
        var y = current.y;
        var x = current.x;
        var depth = current.depth;
        count++;
        if (depth >= result) return;
        if (count == zeroCount)
        {
            result = Math.Min(result, depth);
            return;
        }

        for (var d = 0; d < 4; d++)
        {
            var nextY = y + dy[d];
            var nextX = x + dx[d];
            if (nextY < 0 || nextY >= N || nextX < 0 || nextX >= N) continue;
            if (visited[nextY, nextX]) continue;
            if (board[nextY, nextX] == 1) continue;
            visited[nextY, nextX] = true;
            q.Enqueue((nextY, nextX, depth + 1));
        }
    }
}

if (result == int.MaxValue) sw.WriteLine(-1);
else sw.WriteLine(result);

sw.Flush();
sw.Close();
sr.Close();

static int[] MakeComb(int n, int k)
{
    int[] result = new int[k];
    for (int i = 0; i < k; i++)
        result[i] = i;
    return result;
}

static void ShowComb(int[] comb)
{
    int n = comb.Length;
    for (int i = 0; i < n; ++i)
        Console.Write(comb[i] + " ");
    Console.WriteLine("");
}

static bool IsLast(int[] comb, int n, int k)
{
    // is comb(8,3) like [5,6,7] ?
    if (comb[0] == n - k)
        return true;
    else
        return false;
}

static int[] Successor(int[] comb, int n, int k)
{
    if (IsLast(comb, n, k) == true)
        return null;

    //int i;
    int[] result = new int[k]; // make copy
    for (int i = 0; i < k; ++i)
        result[i] = comb[i];

    int idx = k - 1;
    while (idx > 0 && result[idx] == n - k + idx)
        --idx;

    ++result[idx];

    for (int j = idx; j < k - 1; ++j)
        result[j + 1] = result[j] + 1;

    return result;
}

static int[] Element(BigInteger m, int n, int k)
{
    // compute element [m] using the combinadic
    BigInteger maxM = Choose(n, k) - 1;

    if (m > maxM)
        throw new Exception("m value is too large in Element");

    int[] ans = new int[k];

    int a = n;
    int b = k;
    BigInteger x = maxM - m; // x is the "dual" of m

    for (int i = 0; i < k; ++i)
    {
        ans[i] = LargestV(a, b, x); // see helper below    
        x = x - Choose(ans[i], b);
        a = ans[i];
        b = b - 1;
    }

    for (int i = 0; i < k; ++i)
        ans[i] = (n - 1) - ans[i];

    return ans;
}

static int LargestV(int a, int b, BigInteger x)
{
    int v = a - 1;
    while (Choose(v, b) > x)
        --v;
    return v;
}

static BigInteger Choose(int n, int k)
{
    // number combinations
    if (n < 0 || k < 0)
        throw new Exception("Negative argument in Choose()");
    if (n < k) return 0; // special
    if (n == k) return 1; // short-circuit

    int delta, iMax;

    if (k < n - k) // ex: Choose(100,3)
    {
        delta = n - k;
        iMax = k;
    }
    else // ex: Choose(100,97)
    {
        delta = k;
        iMax = n - k;
    }

    BigInteger ans = delta + 1;
    for (int i = 2; i <= iMax; ++i)
        ans = (ans * (delta + i)) / i;

    return ans;
}
#elif other2
namespace ConsoleApp1
{
    internal class Program
    {
        static int[,] map;
        static bool check = false;
        static List<(int, int)> pos = new();
        static int n;
        static int m;
        static int count;
        static int answer = int.MaxValue;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            n = arr[0]; m = arr[1];
            map = new int[n, n];
            count = n * n;
            for (int i = 0; i < n; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                for (int j = 0; j < n; j++)
                {
                    map[i, j] = temp[j];
                    if (temp[j] == 2)
                        pos.Add((i, j));
                    if (temp[j] == 1)
                        count--;
                }
            }
            bool[] visit = new bool[pos.Count];
            dfs(0, 0, visit);

            output.Write(answer == int.MaxValue? -1:answer);
            input.Close();
            output.Close();
        }
        static void dfs(int start, int num, bool[] visit)
        {
            if(num == m)
            {
                (bool check, int time) = play(visit);
                if (check)
                    answer = Math.Min(answer, time);
                else
                    return;
            }

            for(int i = start; i < visit.Length; i++)
            {
                visit[i] = true;
                dfs(i + 1, num + 1, visit);
                visit[i] = false;
            }
        }
        static (bool,int) play(bool[] visit)
        {
            int c = count;
            bool[,] v = new bool[n, n];
            int[] dy = new int[] { 0, 1, 0, -1 };
            int[] dx = new int[] { 1, 0, -1, 0 };
            Queue<(int, int, int)> q = new();
            for(int i = 0; i < visit.Length; i++)
            {
                if (visit[i])
                {
                    q.Enqueue((pos[i].Item1, pos[i].Item2, 0));
                    v[pos[i].Item1, pos[i].Item2] = true;
                }
            }
            int max = 0;
            while(q.Count > 0)
            {
                (int row, int col, int time) = q.Dequeue();
                c--;
                if(time > max) max = time;

                for(int i = 0; i < 4; i++)
                {
                    int nr = row + dy[i];
                    int nc = col + dx[i];
                    if (nr < 0 || nr == n || nc < 0 || nc == n || map[nr, nc] == 1 || v[nr, nc]) continue;
                    q.Enqueue((nr, nc, time + 1));
                    v[nr, nc] = true;
                }
            }
            if (c == 0)
                return (true, max);
            else
                return (false, 0);
        }
    }
}
#endif
}
