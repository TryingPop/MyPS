using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 21
이름 : 배성훈
내용 : 말이 되고픈 원숭이
    문제번호 : 1600번

    BFS 문제다
    k번 점프갯수 만큼 맵의 크기의 메모리를 할당해 최소횟수를 저장해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0894
    {

        static void Main894(string[] args)
        {

            StreamReader sr;
            int k, row, col;
            int[][] map;
            int[][][] move;

            Solve();
            void Solve()
            {

                Input();

                BFS();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i <= k; i++)
                {

                    int chk = move[i][row - 1][col - 1];
                    if (chk == 0) continue;
                    if (ret == 0 || chk < ret) ret = chk;
                }

                ret--;
                Console.Write(ret);
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void BFS()
            {

                Queue<(int k, int r, int c)> q = new(row * col);

                q.Enqueue((0, 0, 0));
                move[0][0][0] = 1;

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                int[] jumpR = { -2, -1, 1, 2, 2, 1, -1, -2 };
                int[] jumpC = { 1, 2, 2, 1, -1, -2, -2, -1 };

                while (q.Count > 0)
                {

                    (int k, int r, int c) node = q.Dequeue();

                    int cur = move[node.k][node.r][node.c];

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || map[nextR][nextC] == 1) continue;
                        if (move[node.k][nextR][nextC] != 0) continue;
                        move[node.k][nextR][nextC] = cur + 1;
                        q.Enqueue((node.k, nextR, nextC));
                    }

                    if (node.k < k)
                    {

                        node.k++;
                        for (int i = 0; i < 8; i++)
                        {

                            int nextR = node.r + jumpR[i];
                            int nextC = node.c + jumpC[i];

                            if (ChkInvalidPos(nextR, nextC) 
                                || map[nextR][nextC] == 1) continue;

                            if (move[node.k][nextR][nextC] != 0) continue;
                            move[node.k][nextR][nextC] = cur + 1;
                            q.Enqueue((node.k, nextR, nextC));
                        }
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                k = ReadInt();
                col = ReadInt();
                row = ReadInt();

                move = new int[k + 1][][];
                for (int i = 0; i <= k; i++)
                {

                    move[i] = new int[row][];
                    for (int j = 0; j < row; j++)
                    {

                        move[i][j] = new int[col];
                    }
                }

                map = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    map[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        map[r][c] = ReadInt();
                    }
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
int k = int.Parse(Console.ReadLine()!);
string[] inputs = Console.ReadLine()!.Split();
int w = int.Parse(inputs[0]), h = int.Parse(inputs[1]);

int[,] map = new int[h, w];
for (int i = 0; i < h; i++)
{
    inputs = Console.ReadLine()!.Split();
    for (int j = 0; j < w; j++)
    {
        map[i, j] = int.Parse(inputs[j]);
    }
}

Queue<(int, int, int)> queue = new();
queue.Enqueue((0, 0, k));

bool[,,] visited = new bool[h, w, k + 1];
visited[0, 0, 0] = true;

(int, int)[]
    dir = { (0, 1), (0, -1), (1, 0), (-1, 0) },
    horse = { (1, 2), (2, 1), (1, -2), (2, -1), (-1, 2), (-2, 1), (-1, -2), (-2, -1) };
int cnt = 0;
while (queue.Count > 0)
{
    int qcnt = queue.Count;
    while (qcnt-- > 0)
    {
        var (row, col, hor) = queue.Dequeue();

        if (row == h - 1 && col == w - 1)
        {
            Console.WriteLine(cnt);
            return;
        }

        foreach (var (r, c) in dir)
        {
            int tr = row + r;
            int tc = col + c;
            if (0 <= tr && tr < h && 0 <= tc && tc < w && map[tr, tc] != 1 && !visited[tr, tc, hor])
            {
                visited[tr, tc, hor] = true;
                queue.Enqueue((tr, tc, hor));
            }
        }

        if (hor > 0)
        {
            hor--;
            foreach (var (r, c) in horse)
            {
                int tr = row + r;
                int tc = col + c;
                if (0 <= tr && tr < h && 0 <= tc && tc < w && map[tr, tc] != 1 && !visited[tr, tc, hor])
                {
                    visited[tr, tc, hor] = true;
                    queue.Enqueue((tr, tc, hor));
                }
            }
        }
    }
    cnt++;
}

Console.WriteLine(-1);
#endif
}
