using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 30
이름 : 배성훈
내용 : 벽 부수고 이동하기 3
    문제번호 : 16933번

    2개를 swap 하는 형식으로 하니 1%에서 틀린다
    찾아보니 먼저 방문하는게 최소임이 보장되지 않아 틀린거였다
    그래서 그냥 k크기를 할당해 푸니 이상없이 통과한다

    BFS 문제다
    밤에 벽에 부딪힌 경우 하루 더 대기하는 식으로 구현하니 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_1008
    {

        static void Main1008(string[] args)
        {

            int INF = 123_456_789;
            StreamReader sr;

            int row, col, k;

            int[][] map;

            int[][][] move;
            bool[][][] visit;

            Queue<(int r, int c, int d, int k)> q;
            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                q = new(k * row * col);

                dirR = new int[4] { 0, 1, 0, -1 };
                dirC = new int[4] { 1, 0, -1, 0 };
                int ret = INF;

                move[0][0][0] = 1;
                visit[0][0][0] = true;
                q.Enqueue((0, 0, 1, 0));

                BFS();

                for (int i = 0; i <= k; i++)
                {

                    int chk = move[i][row - 1][col - 1];
                    chk = chk == 0 ? INF : chk;
                    ret = Math.Min(ret, chk);
                }

                if (ret == INF) ret = -1;

                Console.Write(ret);
            }

            void BFS()
            {

                while (q.Count > 0)
                {

                    (int r, int c, int d, int k) node = q.Dequeue();
                    bool isNight = (node.d & 1) == 0;
                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || visit[node.k][nR][nC]) continue;

                        if (map[nR][nC] == 0)
                        {

                            visit[node.k][nR][nC] = true;
                            move[node.k][nR][nC] = node.d + 1;
                            q.Enqueue((nR, nC, node.d + 1, node.k));
                        }
                        else if (isNight)
                            q.Enqueue((node.r, node.c, node.d + 1, node.k));
                        else if (node.k < k)
                        {

                            visit[node.k][nR][nC] = true;
                            visit[node.k + 1][nR][nC] = true;
                            move[node.k + 1][nR][nC] = node.d + 1;
                            q.Enqueue((nR, nC, node.d + 1, node.k + 1));
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();
                k = ReadInt();

                map = new int[row][];
                move = new int[k + 1][][];
                visit = new bool[k + 1][][];

                for (int i = 0; i <= k; i++)
                {

                    move[i] = new int[row][];
                    visit[i] = new bool[row][];

                    for (int r = 0; r < row; r++)
                    {

                        move[i][r] = new int[col];
                        visit[i][r] = new bool[col];
                    }
                }

                for (int r = 0; r < row; r++)
                {

                    map[r] = new int[col];

                    for (int c = 0; c < col; c++)
                    {

                        map[r][c] = sr.Read() - '0';
                    }

                    if (sr.Read() == '\r') sr.Read();
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
var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
int n = input[0], m = input[1], k = input[2];
var map = Enumerable.Range(0, n).Select(i => Console.ReadLine()).ToArray();
var v = new bool[k+1, n, m];
var d = new (int x, int y)[] { (0, 1), (1, 0), (-1, 0), (0, -1) };
var q = new Queue<(int x, int y, int k, int c, bool s)>();

q.Enqueue((0, 0, 0, 0, true));
v[0, 0, 0] = true;

while (q.TryDequeue(out var p))
{
    if (p.x == n - 1 && p.y == m - 1)
    {
        Console.Write(p.c+1);
        return;
    }

    for (int i = 0; i < 4; i++)
    {
        int dx = p.x + d[i].x;
        int dy = p.y + d[i].y;

        if (dx < 0 || dy < 0 || dx >= n || dy >= m)
            continue;

        if (map[dx][dy] == '1' && p.k < k && !v[p.k + 1, dx, dy])
        {
            if (p.s)
            {
                v[p.k + 1, dx, dy] = true;
                q.Enqueue((dx, dy, p.k + 1, p.c + 1, false));
            }
            else q.Enqueue((p.x, p.y, p.k, p.c + 1, true));
        }
        else if (map[dx][dy] == '0' && !v[p.k, dx, dy])
        {
            q.Enqueue((dx, dy, p.k, p.c + 1, !p.s));
            v[p.k, dx, dy] = true;
        }
    }
}

Console.Write(-1);
#endif
}
