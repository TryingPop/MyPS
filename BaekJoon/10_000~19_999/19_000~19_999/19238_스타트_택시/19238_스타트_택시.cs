using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 10
이름 : 배성훈
내용 : 스타트 택시
    문제번호 : 19238번

    구현, 시뮬레이션, BFS 문제다
    시뮬레이션 돌리면서 해결했다

    다만 사람을 태우고 목적지에 못간 경우를 
    체크 안해 한 번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1042
    {

        static void Main1042(string[] args)
        {

            StreamReader sr;
            int n, m;
            (int r, int c) car;

            int[][] board;
            int[][] move;

            (int r, int c)[] dst;
            int oil;
            int goal = -1;
            Queue<(int r, int c)> q;
            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                q = new(n * n);
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                for (int i = 0; i < m; i++)
                {

                    if (FindPerson() || MovePerson())
                    {

                        oil = -1;
                        break;
                    }
                }

                Console.Write(oil);
            }

            bool MovePerson()
            {

                InitMove();
                q.Enqueue((car.r, car.c));
                move[car.r][car.c] = 0;

                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    int curMove = move[node.r][node.c];

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC] == -1 || move[nR][nC] != -1) continue;
                        move[nR][nC] = curMove + 1;
                        q.Enqueue((nR, nC));
                    }
                }

                if (move[dst[goal].r][dst[goal].c] == -1) return true;
                car = (dst[goal].r, dst[goal].c);
                if (oil < move[dst[goal].r][dst[goal].c]) return true;

                oil += move[dst[goal].r][dst[goal].c];
                return false;
            }

            bool FindPerson()
            {

                InitMove();
                q.Enqueue((car.r, car.c));
                move[car.r][car.c] = 0;

                goal = -1;
                int dstR = -1, dstC = -1;
                int dstMove = n * n;
                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    int curMove = move[node.r][node.c];
                    if (board[node.r][node.c] > 0) ChkPerson(curMove, node.r, node.c);

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC] == -1 || move[nR][nC] > -1) continue;
                       
                        move[nR][nC] = curMove + 1;
                        q.Enqueue((nR, nC));
                    }
                }

                if (goal != -1) board[dstR][dstC] = 0;
                car = (dstR, dstC);
                oil -= dstMove;
                return oil < 0 || goal == -1;

                void ChkPerson(int _move, int _r, int _c)
                {

                    if (dstMove < _move) return;
                    else if (dstMove == _move)
                    {

                        if (dstR < _r) return;
                        else if (dstR == _r)
                        {

                            if (dstC < _c) return;
                        }

                        dstR = _r;
                        dstC = _c;
                        goal = board[_r][_c];
                    }
                    else
                    {

                        dstMove = _move;
                        dstR = _r;
                        dstC = _c;
                        goal = board[_r][_c];
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= n || _c >= n;
            }

            void InitMove()
            {

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        move[r][c] = -1;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                oil = ReadInt();

                board = new int[n][];
                move = new int[n][];
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    move[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        int cur = ReadInt();
                        if (cur == 1) board[r][c] = -1;
                    }
                }

                car = (ReadInt() - 1, ReadInt() - 1);

                dst = new (int r, int c)[m + 1];
                for (int i = 1; i <= m; i++)
                {

                    int r = ReadInt() - 1;
                    int c = ReadInt() - 1;
                    board[r][c] = i;
                    dst[i] = (ReadInt() - 1, ReadInt() - 1);
                }

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
using var reader = new StreamReader(Console.OpenStandardInput());
int[] Read() => Array.ConvertAll(reader.ReadLine().Split(), int.Parse);

var read = Read();
int N = read[0], M = read[1], fuel = read[2];

var board = new int[N, N];
var pass = new int[N, N];
var dest = new (int x, int y)[M];
var visited = new bool[N, N];
for (int i = 0; i < N; i++)
{
    read = Read();
    for (int j = 0; j < N; j++)
    {
        board[i, j] = read[j];
        pass[i, j] = -1;
    }
}

read = Read();
(int x, int y) taxi = (read[0] - 1, read[1] - 1);

for (int i = 0; i < M; i++)
{
    read = Read();
    pass[read[0] - 1, read[1] - 1] = i;
    dest[i] = (read[2] - 1, read[3] - 1);
}

(int, int)[] dirs = { (0, 1), (0, -1), (1, 0), (-1, 0) };
var queue = new Queue<(int x, int y, int d)>();
(int x, int y, int i, int d) t = (taxi.x, taxi.y, -1, 0);
for (int m = 0; m < M; m++)
{
    Array.Clear(visited);
    visited[t.x, t.y] = true;
    queue.Clear();
    queue.Enqueue((t.x, t.y, 0));

    t.d = int.MaxValue;
    while (queue.TryDequeue(out var q) && q.d <= t.d)
    {
        if (pass[q.x, q.y] >= 0)
        {
            if (t.i < 0 || q.d <= t.d && q.x * N + q.y < t.x * N + t.y)
                t = (q.x, q.y, pass[q.x, q.y], q.d);
            continue;
        }

        foreach ((int dx, int dy) in dirs)
        {
            int nx = q.x + dx;
            int ny = q.y + dy;
            if (0 <= nx && nx < N && 0 <= ny && ny < N && board[nx, ny] == 0 && !visited[nx, ny])
            {
                visited[nx, ny] = true;

                if (q.d < fuel)
                    queue.Enqueue((nx, ny, q.d + 1));
            }
        }
    }

    if (t.i == -1)
    {
        Console.WriteLine(-1);
        return;
    }

    fuel -= t.d;
    pass[t.x, t.y] = -1;

    Array.Clear(visited);
    visited[t.x, t.y] = true;
    queue.Clear();
    queue.Enqueue((t.x, t.y, 0));

    t.d = -1;
    while (queue.TryDequeue(out var q) && t.d < 0)
    {
        if (q.x == dest[t.i].x && q.y == dest[t.i].y)
        {
            t = (q.x, q.y, -1, q.d);
            break;
        }

        foreach ((int dx, int dy) in dirs)
        {
            int nx = q.x + dx;
            int ny = q.y + dy;
            if (0 <= nx && nx < N && 0 <= ny && ny < N && board[nx, ny] == 0 && !visited[nx, ny])
            {
                visited[nx, ny] = true;

                if (q.d < fuel)
                    queue.Enqueue((nx, ny, q.d + 1));
            }
        }
    }

    if (t.d == -1)
    {
        Console.WriteLine(-1);
        return;
    }

    fuel += t.d;
}

Console.WriteLine(fuel);
#endif
}
