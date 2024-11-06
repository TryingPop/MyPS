using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/*
날짜 : 2024. 4. 18
이름 : 배성훈
내용 : 비숍
    문제번호 : 1799번

    백트래킹 문제다
    처음에는 바깥부터 접근하면 가장 큰 경우의 수가 나오지 않을까 하고 그리디하게 생각했다
    그런데 다음과 같은 경우 반례가 있었다
        0 0 0 0 0
        0 1 0 1 0
        1 0 0 0 1
        0 1 0 1 0
        0 0 0 0 0
    그리고 위에서부터 하면 될까 했지만
        0 1 0
        1 0 1
        0 0 0
    이렇게 바로 반례가 나온다

    그리고 비숍의 이동 경로로 나눠서 풀었다
    그러니 서로 독립인 홀짝으로 나눠지고,

    중간에서 만나기 문제로 바뀌었다
    그리고 하나씩 넣어보면서 시작 방향을 기준으로 올라가는 형식으로 변수명을 부여했는데
    left는 /, right는 \ 방향의 대각선으로 해석해서 풀었다
    그리고 놓을 수 있는 위치면 해당 위치에 놓아가면서 깊이 n만큼 탐색하면서 최대값을 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0562
    {

        static void Main562(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n;
            (int l, int r)[] white;
            (int l, int r)[] black;

            bool[] left;
            bool[] right;
            Solve();

            sr.Close();

            void Solve()
            {

                Input();
                int ret = Find();
                Console.WriteLine(ret);
            }

            int Find()
            {

                int ret = 0;
                left = new bool[n * 2];
                right = new bool[n * 2];

                ret = DFS(white, 0);
                ret += DFS(black, 0);

                return ret;
            }

            int DFS((int l, int r)[] _arr, int _s)
            {


                int ret = 0;

                for (int i = _s; i < _arr.Length; i++)
                {

                    if (left[_arr[i].l] || right[_arr[i].r]) continue;
                    left[_arr[i].l] = true;
                    right[_arr[i].r] = true;

                    int calc = DFS(_arr, i + 1) + 1;
                    ret = calc < ret ? ret : calc;

                    left[_arr[i].l] = false;
                    right[_arr[i].r] = false;
                }

                return ret;
            }

            void Input()
            {

                n = ReadInt();
                int[,] board = new int[n, n];
                int w = 0;
                int b = 0;

                Queue<(int r, int c)> q = new(n * n);
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        int cur = ReadInt();
                        if (cur == 0) continue;

                        if ((i + j) % 2 == 0) w++;
                        else b++;

                        q.Enqueue((i, j));
                    }
                }

                white = new (int l, int r)[w];
                black = new (int l, int r)[b];

                int wIdx = 0;
                int bIdx = 0;

                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    int l = (node.r + node.c);
                    int r = n + (node.r - node.c);
                    if ((node.r + node.c) % 2 == 0) white[wIdx++] = (l, r);
                    else black[bIdx++] = (l, r);
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
using System;
using System.IO;
using System.Text;

#nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());

        var map = new int[n, n];
        for (var y = 0; y < n; y++)
        {
            var l = sr.ReadLine().Split(' ');
            for (var x = 0; x < n; x++)
                map[y, x] = l[x] == "1" ? 1 : 0;
        }

        var (odd, even) = Split(n, map);

        var isBlocked = new bool[n];

        var c1 = Count(odd, n, 0, isBlocked);
        var c2 = Count(even, n, 0, isBlocked);

        sw.WriteLine(c1 + c2);
    }

    private static int Count(int[,] map, int n, int y, bool[] isBlocked)
    {
        if (y == n)
            return 0;

        var max = Count(map, n, 1 + y, isBlocked);

        for (var x = 0; x < n; x++)
            if (map[y, x] == 1 && !isBlocked[x])
            {
                isBlocked[x] = true;
                max = Math.Max(max, 1 + Count(map, n, y + 1, isBlocked));
                isBlocked[x] = false;
            }

        return max;
    }

    private static string MapToString(int[,] map)
    {
        var sb = new StringBuilder();
        var n = map.GetLength(0);

        for (var y = 0; y < n; y++)
        {
            for (var x = 0; x < n; x++)
                sb.Append(map[y, x]);

            sb.AppendLine();
        }

        return sb.ToString();
    }
    private static (int[,] odd, int[,] even) Split(int n, int[,] map)
    {
        var odd = new int[n, n];
        var even = new int[n, n];

        var offset = 1 + n / 2;

        for (var y = 0; y < n; y++)
            for (var x = 0; x < n; x++)
            {
                if ((x + y) % 2 == 0)
                    even[(y - x + n) / 2, (x + y) / 2] = map[y, x];
                else
                    odd[(y - x + n) / 2, (x + y) / 2] = map[y, x];
            }

        return (odd, even);
    }
}
#elif other2
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int N = int.Parse(sr.ReadLine());
int[,] existBishop = new int[2, 2 * N];
int[,] board = new int[N,N];
HashSet<(int, int)> dpCheck = new();

for(int i=0; i<N; i++)
{
    int[] inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    for(int j=0; j<N; j++)
        board[i,j] = inputs[j];
}

int ans = solve(0, 0, 0, 0, 0);
dpCheck.Clear();
ans += solve(1, 0, 0, 0, 1);
sw.WriteLine(ans);

sr.Close();
sw.Close();

int solve(int num, int depth, int bit1, int bit2, int odd)
{
    int ret = depth;

    if (dpCheck.Contains((bit1, bit2)))
    {
        return ret;
    }
    dpCheck.Add((bit1, bit2));

    for(int j=num; j<N*N; j++)
    {
        int row = j / N, col = j % N;
        if ((row + col) % 2 != odd)
            continue;
        if (board[row, col] == 0)
            continue;
        if (existBishop[0, row + col] > 0 || existBishop[1, N - row + col] > 0)
            continue;

        existBishop[0, row + col]++;
        existBishop[1, N - row + col]++;
        ret = Math.Max(ret, solve(j + 1, depth + 1, bit1 | (1<<(row+col)), bit2 | (1<<(N-row+col)), odd));
        existBishop[0, row + col]--;
        existBishop[1, N - row + col]--;
    }
    return ret;

}
#endif
}
