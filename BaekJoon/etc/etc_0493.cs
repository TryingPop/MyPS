using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 9
이름 : 배성훈
내용 : 탈출
    문제번호 : 3055번

    BFS 문제다
    처음에 순간 물이 차오르는걸 이동별로 물이 차오르는걸 리셋하면서 DFS 탐색할지 고민했다
    그러면서 이전에도 비슷한 유형 푼거 떠오르고 해당 유형 어떻게 풀었는지 고민했었고
    현재 문제의 로직을 다시 살펴봤다

    그러니 크게 신경안써도 되는 부분임을 확인했다
    해당 문제에서는 이미 방문했다면 물이 차오르는걸 끊던, 이후에 물을 채우던 어떤것을 해도 상관없다
    물이 차오르기 전에 이미 지나간 곳이므로 해당 다음 지역은 먼저 이동이 가능하다
    다른 방향에서 차오르는 물이 아닌 이상 최단 경로이기에 해당 경로를 막는건 불가능하다
    여기서는 물이 차오르는 것을 방문처리하며 끊었다

    이후에는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0493
    {

        static void Main493(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int row = ReadInt();
            int col = ReadInt();
            Queue<(int r, int c)> q = new(row * col);
            Queue<(int r, int c)> water = new(row * col);
            bool[,] visit = new bool[row, col];

            int[,] board = new int[row, col];
            (int r, int c) end = ( -1, -1);
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    int cur = sr.Read();
                    if(cur == 'S')
                    {

                        q.Enqueue((r, c));
                        visit[r, c] = true;
                    }
                    else if (cur == 'D')
                    {

                        // 골인지점
                        end = (r, c);
                        board[r, c] = -1;
                    }
                    else if (cur == 'X')
                    {

                        // 돌
                        board[r, c] = -3;
                        visit[r, c] = true;
                    }
                    else if (cur == '*')
                    {

                        // 물
                        board[r, c] = -1;
                        water.Enqueue((r, c));
                        visit[r, c] = true;
                    }
                }

                sr.ReadLine();
            }

            sr.Close();

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };
            Queue<(int r, int c)> calc = new(row * col);
            while(q.Count > 0)
            {

                while (water.Count > 0)
                {

                    var node = water.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInValidPos(nextR, nextC) || visit[nextR, nextC] || board[nextR, nextC] < 0) continue;

                        visit[nextR, nextC] = true;
                        board[nextR, nextC] = -1;
                        calc.Enqueue((nextR, nextC));
                    }
                }

                var temp = water;
                water = calc;
                calc = temp;

                while (q.Count > 0)
                {

                    var node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];
                        if (ChkInValidPos(nextR, nextC) || visit[nextR, nextC]) continue;
                        visit[nextR, nextC] = true;
                        board[nextR, nextC] = board[node.r, node.c] + 1;
                        calc.Enqueue((nextR, nextC));
                    }
                }

                temp = q;
                q = calc;
                calc = temp;
            }

            if (visit[end.r, end.c]) Console.WriteLine(board[end.r, end.c]);
            else Console.WriteLine("KAKTUS");

            bool ChkInValidPos(int _r, int _c)
            {

                if (_r < 0 || _r >= row || _c < 0 || _c >= col) return true;
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
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using Point = System.ValueTuple<int, int>;

var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int rL = ScanInt(), cL = ScanInt(),
    str = 0, stc = 0;
var map = new char[rL, cL];
var waterList = new Queue<Point>();
for (int i = 0; i < rL; i++)
{
    for (int j = 0; j < cL; j++)
    {
        var item = (char)sr.Read();
        map[i, j] = item;
        if (item == 'S')
        {
            (str, stc) = (i, j);
            map[i, j] = '.';
        }
        else if (item == '*')
            waterList.Enqueue((i, j));
    }
    if (sr.Read() == '\r')
        sr.Read();
}

var dirs = new (int r, int c)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
var q = new Queue<Point>();
var nextQ = new Queue<Point>();
var nextWaterList = new Queue<Point>();
q.Enqueue((str, stc));
var time = 0;
var visited = new bool[rL, cL];
while (q.Count > 0)
{
    time++;
    while (waterList.TryDequeue(out var p))
    {
        (var r, var c) = p;
        foreach (var d in dirs)
        {
            int nR = r + d.r, nC = c + d.c;
            if (0 <= nR && nR < rL &&
                0 <= nC && nC < cL &&
                map[nR, nC] is '.')
            {
                map[nR, nC] = '*';
                nextWaterList.Enqueue((nR, nC));
            }
        }
    }
    (waterList, nextWaterList) = (nextWaterList, waterList);

    while (q.TryDequeue(out var p))
    {
        (var r, var c) = p;
        foreach (var d in dirs)
        {
            int nR = r + d.r, nC = c + d.c, nTime = time + 1;
            if (!(0 <= nR && nR < rL &&
                0 <= nC && nC < cL))
                continue;

            if (map[nR, nC] == 'D')
            {
                Console.Write(time);
                return;
            }

            if (map[nR, nC] == '.' && !visited[nR, nC])
            {
                visited[nR, nC] = true;
                nextQ.Enqueue((nR, nC));
            }
        }
    }
    (q, nextQ) = (nextQ, q);
}
Console.Write("KAKTUS");

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baekjoon.Gold
{
    internal class _3055
    {
        static int[] n;
        static char[][] map;
        static int[][] water;
        static int[][] dochi;
        static (int, int) biber;

        static int[] ud = { -1, 1, 0, 0 };
        static int[] lr = { 0, 0, -1, 1 };

        static void Main(string[] args)
        {
            n = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            map = new char[n[0]][];
            water = new int[n[0]][];
            dochi = new int[n[0]][];

            (int, int) dochiStart = (0, 0);
            Queue<(int, int)> waterPos = new Queue<(int, int)>();
            for(int i = 0; i < n[0]; i++)
            {
                map[i] = Console.ReadLine().ToCharArray();
                water[i] = new int[n[1]];
                dochi[i] = new int[n[1]];
                for(int j = 0; j < n[1]; j++)
                {
                    if (map[i][j] == '*')
                    {
                        waterPos.Enqueue((i, j));
                        water[i][j] = 1;
                    }
                    else if (map[i][j] == 'D') biber = (i, j);
                    else if (map[i][j] == 'S') dochiStart = (i, j);
                }
            }

            WaterBfs(waterPos);
            Console.WriteLine(DochiBfs(dochiStart) ? dochi[biber.Item1][biber.Item2]-1:"KAKTUS");
        }

        static void WaterBfs(Queue<(int,int)> startPos)
        {
            while(startPos.Count>0)
            {
                var temp = startPos.Dequeue();
                for(int i = 0; i<4; i++)
                {
                    int x = temp.Item1 + ud[i];
                    int y = temp.Item2 + lr[i];

                    if (x < 0 || y < 0 || x >= n[0] || y >= n[1]) continue;
                    if (map[x][y] == 'X' || map[x][y] == 'D' || water[x][y] > 0) continue;

                    startPos.Enqueue((x,y));
                    water[x][y] = water[temp.Item1][temp.Item2] + 1;
                }
            }
        }

        static bool DochiBfs((int, int) startPos)
        {
            Queue<(int, int)> q = new Queue<(int, int)>();
            q.Enqueue(startPos);
            dochi[startPos.Item1][startPos.Item2] = 1;

            while(q.Count>0)
            {
                var temp = q.Dequeue();
                if (temp.Item1 == biber.Item1 && temp.Item2 == biber.Item2)
                    return true;

                for (int i = 0; i<4;i++)
                {
                    int x = temp.Item1 + ud[i];
                    int y = temp.Item2 + lr[i];

                    if (x < 0 || y < 0 || x >= n[0] || y >= n[1]) continue;
                    if (map[x][y] == 'X' || dochi[x][y] > 0) continue;
                    if (water[x][y] == 0 || dochi[temp.Item1][temp.Item2] + 1 < water[x][y])
                    {
                        q.Enqueue((x, y));
                        dochi[x][y] = dochi[temp.Item1][temp.Item2] + 1;
                    }
                }
            }

            return false;
        }
    }
}

#endif
}
