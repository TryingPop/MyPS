using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 16
이름 : 배성훈
내용 : 두 동전
    문제번호 : 16197번

    BFS 문제다.
    4^10으로 브루트포스가 유효하다 생각해 DFS로 해결했다.
    DFS를 이용해 해결하니 시간이 더 걸리는거 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1631
    {

        static void Main1631(string[] args)
        {

            int row, col;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int INF = 123;
                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };

                int r1, c1, r2, c2;

                Find();

                int ret = DFS(r1, c1, r2, c2);
                if (ret == INF) ret = -1;
                Console.Write(ret);

                void Find()
                {

                    bool flag = false;

                    r1 = -1; 
                    c1 = -1; 

                    r2 = -1; 
                    c2 = -1;

                    for (int r = 1; r <= row; r++)
                    {

                        for (int c = 1; c <= col; c++)
                        {

                            if (board[r][c] != 'o') continue;

                            if (flag)
                            {

                                r2 = r;
                                c2 = c;
                                return;
                            }
                            else
                            {

                                flag = true;
                                r1 = r;
                                c1 = c;
                            }
                        }
                    }
                }

                int DFS(int _r1, int _c1, int _r2, int _c2, int _dep = 0)
                {

                    if (_dep == 11) return INF;
                    int cnt = 0;
                    if (ChkOut(_r1, _c1)) cnt++;
                    if (ChkOut(_r2, _c2)) cnt++;

                    if (cnt > 0) return cnt == 1 ? _dep : INF;

                    int ret = INF;
                    for (int i = 0; i < 4; i++)
                    {

                        int nR1 = _r1 + dirR[i];
                        int nC1 = _c1 + dirC[i];

                        if (board[nR1][nC1] == '#')
                        {

                            nR1 = _r1;
                            nC1 = _c1;
                        }

                        int nR2 = _r2 + dirR[i];
                        int nC2 = _c2 + dirC[i];

                        if (board[nR2][nC2] == '#')
                        {

                            nR2 = _r2;
                            nC2 = _c2;
                        }

                        ret = Math.Min(ret, DFS(nR1, nC1, nR2, nC2, _dep + 1));
                    }

                    return ret;
                }

                bool ChkOut(int _r, int _c)
                    => _r == 0 || _c == 0 || _r > row || _c > col;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row + 2][];
                for (int r = 0; r < board.Length; r++)
                {

                    board[r] = new int[col + 2];
                }

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        board[r][c] = sr.Read();
                    }

                    while (sr.Read() != '\n') ;
                }

                int ReadInt() 
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
using System.Numerics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
// var _ = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
// int n = int.Parse(sr.ReadLine());
class Program
{
    enum Type
    {
        Floor,
        Wall,
    }

    private static void Main(string[] args)
    {
        StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
        StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));
        StringBuilder sb = new();

        var _ = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
        int N = _[0], M = _[1];
        Type[,] map = new Type[N, M];
        bool[,,,] visited = new bool[N, M, N, M];
        var coins = new (int x, int y)[2];

        int[] dx = { -1, 0, 1, 0 };
        int[] dy = { 0, 1, 0, -1 };

        int k = 0;
        for (int i = 0; i < N; i++)
        {
            string input = sr.ReadLine();
            for (int j = 0; j < M; j++)
            {
                switch (input[j])
                {
                    case '.':
                        map[i, j] = Type.Floor;
                        break;
                    case '#':
                        map[i, j] = Type.Wall;
                        break;
                    case 'o':
                        map[i, j] = Type.Floor;
                        coins[k++] = (i, j);
                        break;
                }
            }
        }

        int Bfs()
        {
            Queue<(int x1, int y1, int x2, int y2, int count)> queue = new();

            queue.Enqueue((coins[0].x, coins[0].y, coins[1].x, coins[1].y, 0));

            while (queue.Count > 0)
            {
                (int x1, int y1, int x2, int y2, int count) = queue.Dequeue();
                if (count == 10)
                    return -1;
               
                for (int i = 0; i < 4; i++)
                {
                    (int nx1, int ny1) = (x1, y1);
                    (int nx2, int ny2) = (x2, y2);

                    nx1 += dx[i]; nx2 += dx[i];
                    ny1 += dy[i]; ny2 += dy[i];

                    if (nx1 >= 0 && nx1 < N && ny1 >= 0 && ny1 < M
                        && nx2 >= 0 && nx2 < N && ny2 >= 0 && ny2 < M) // 둘다 안
                    {
                        // 벽으로 간거 보정
                        if (map[nx1, ny1] == Type.Wall)
                        {
                            nx1 -= dx[i]; 
                            ny1 -= dy[i];
                        }
                        if (map[nx2, ny2] == Type.Wall)
                        {
                            nx2 -= dx[i]; 
                            ny2 -= dy[i];
                        }

                        if (!visited[nx1, ny1, nx2, ny2])
                        {
                            visited[nx1, ny1, nx2, ny2] = true;
                            queue.Enqueue((nx1, ny1, nx2, ny2, count + 1));
                        }
                    }
                    else if ((nx1 < 0 || nx1 >= N || ny1 < 0 || ny1 >= M)
                        && (nx2 < 0 || nx2 >= N || ny2 < 0 || ny2 >= M)) // 둘다 바깥
                    {
                        continue;
                    }
                    else // 한쪽만 안
                    {
                        return count + 1;
                    }
                }
            }

            return -1;
        }

        sw.WriteLine(Bfs());

        sr.Close();
        sw.Close();
    }
}
#endif
}
