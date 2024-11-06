using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 12
이름 : 배성훈
내용 : 소년 점프
    문제번호 : 16469번

    BFS 문제다
    그냥 3번의 BFS를 구하고 최소가 되는 지점을 찾았다

    처음에는 BFS 3번이아닌 줄일 수 없을까 생각을 했고
    이동 가능 경로들의 그룹을 나누고, 이후 같은 그룹에 속하면 모두 동시에 BFS 탐색을 진행할까 생각했다
    그런데, 해당 방법은 모두가 만나는 곳을 찾는 문제가 있다

    일반적인 BFS를 쓰면 동시에 돌리면 2명이 만나는 경우 확장될 수 없고, 만나는 지점이 + 1인 경우 등으로 못쓴다
    약간의 변형이 필요할텐데 당장에 안떠오른다 그래서 해당 방법을 포기하고 BFS 3번으로 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0514
    {

        static void Main514(string[] args)
        {

            int WALL = 10_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int row = ReadInt();
            int col = ReadInt();

            int[,,] board = new int[row, col, 4];
            bool[,] visit = new bool[row, col];
            for (int i = 0; i < row; i++)
            {

                for (int j = 0; j < col; j++)
                {

                    int cur = sr.Read() - '0';
                    cur *= WALL;
                    board[i, j, 0] = cur;
                }

                sr.ReadLine();
            }

            Queue<(int r, int c)> q = new(row * col);
            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            for (int i = 0; i < 3; i++)
            {

                bool visited = i % 2 == 0;
                BFS(ReadInt() - 1, ReadInt() - 1, visited, i + 1);
            }

            int ret1 = WALL;
            int ret2 = 0;

            GetRet();

            if (ret1 == WALL) Console.Write(-1);
            else Console.Write($"{ret1 - 1}\n{ret2}");

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void BFS(int _sR, int _sC, bool _visit, int _depth)
            {

                q.Enqueue((_sR, _sC));
                board[_sR, _sC, _depth] = 1;
                visit[_sR, _sC] = _visit;
                while(q.Count > 0)
                {

                    var node = q.Dequeue();
                    int cur = board[node.r, node.c, _depth];

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || visit[nextR, nextC] == _visit) continue;
                        visit[nextR, nextC] = _visit;
                        if (board[nextR, nextC, 0] == WALL) continue;

                        board[nextR, nextC, _depth] = cur + 1;
                        q.Enqueue((nextR, nextC));
                    }
                }
            }

            bool ChkInvalidMax(int _r, int _c, out int _max)
            {

                int chk1 = board[_r, _c, 1];
                int chk2 = board[_r, _c, 2];
                int chk3 = board[_r, _c, 3];

                _max = -1;
                if (chk1 == 0 || chk2 == 0 || chk3 == 0) return true;

                _max = Math.Max(chk1, Math.Max(chk2, chk3));
                return false;
            }

            void GetRet()
            {

                for (int i = 0; i < row; i++)
                {

                    for (int j = 0; j < col; j++)
                    {

                        if (board[i, j, 0] == WALL || ChkInvalidMax(i, j, out int chk)) continue;
                        
                        board[i, j, 0] = chk;
                        if (chk < ret1)
                        {

                            ret1 = chk;
                            ret2 = 1;
                        }
                        else if (chk == ret1) ret2++;
                    }
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
    const int inf = int.MaxValue;
    static void Main()
    {
        string[] rc = Console.ReadLine().Split(' ');
        int r = int.Parse(rc[0]), c = int.Parse(rc[1]);
        char[,] maze = new char[r, c];
        for (int i = 0; i < r; i++)
        {
            string row = Console.ReadLine();
            for (int j = 0; j < c; j++)
            {
                maze[i, j] = row[j];
            }
        }
        int[,,] visit = new int[r, c, 3];
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    visit[i, j, k] = inf;
                }
            }
        }
        Pair[] next = { new(-1, 0), new(1, 0), new(0, 1), new(0, -1) };
        Queue<Pair> queue = new();
        for (int i = 0; i < 3; i++)
        {
            string[] xy = Console.ReadLine().Split(' ');
            int x = int.Parse(xy[0]) - 1, y = int.Parse(xy[1]) - 1;
            visit[x, y, i] = 0;
            queue.Enqueue(new(x, y));
            while (queue.Count > 0)
            {
                Pair cur = queue.Dequeue();
                for (int j = 0; j < 4; j++)
                {
                    int nr = cur.r + next[j].r, nc = cur.c + next[j].c;
                    if (nr >= 0 && nr < r && nc >= 0 && nc < c && maze[nr, nc] == '0' && visit[nr, nc, i] == inf)
                    {
                        visit[nr, nc, i] = visit[cur.r, cur.c, i] + 1;
                        queue.Enqueue(new(nr, nc));
                    }
                }
            }
        }
        int answer = inf, count = 0;
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                if (maze[i, j] == '1')
                    continue;
                int max = 0;
                for (int k = 0; k < 3; k++)
                {
                    max = Math.Max(max, visit[i, j, k]);
                }
                if (max == inf)
                    continue;
                if (max < answer)
                {
                    answer = max;
                    count = 1;
                }
                else if (max == answer)
                    count++;
            }
        }
        Console.Write(answer == inf ? -1 : $"{answer}\n{count}");
    }
}
#endif
}
