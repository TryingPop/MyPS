using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 17
이름 : 배성훈
내용 : 적록색약
    문제번호 : 10026번

    BFS, DFS 문제다
    BFS로 영역을 나누고 2번 탐색해서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0560
    {

        static void Main560(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            Queue<(int r, int c)> q;
            int n;
            int[,] board;
            bool[,] visit;

            int ret1 = 0;
            int ret2 = 0;

            Solve();
            sr.Close();

            void Solve()
            {

                Input();
                BFS();
                Console.Write($"{ret1} {ret2}");
            }

            void BFS()
            {

                int[] dirR = { -1, 1, 0, 0 };
                int[] dirC = { 0, 0, -1, 1 };

                
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        if (visit[i, j]) continue;
                        visit[i, j] = true;
                        q.Enqueue((i, j));
                        ret1++;
                        int curColor = board[i, j];
                        
                        while(q.Count > 0)
                        {

                            (int r, int c) node = q.Dequeue();

                            for (int k = 0; k < 4; k++)
                            {

                                int nextR = node.r + dirR[k];
                                int nextC = node.c + dirC[k];

                                if (ChkInvalidPos(nextR, nextC) || visit[nextR, nextC] || board[nextR, nextC] != curColor) continue;
                                visit[nextR, nextC] = true;
                                q.Enqueue((nextR, nextC));
                            }
                        }
                    }
                }

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        if (!visit[i, j]) continue;
                        visit[i, j] = false;
                        q.Enqueue((i, j));
                        ret2++;

                        int curColor = board[i, j] / 2;

                        while(q.Count > 0)
                        {

                            (int r, int c) node = q.Dequeue();

                            for (int k = 0; k < 4; k++)
                            {

                                int nextR = node.r + dirR[k];
                                int nextC = node.c + dirC[k];

                                if (ChkInvalidPos(nextR, nextC) || !visit[nextR, nextC] || board[nextR, nextC] / 2 != curColor) continue;
                                visit[nextR, nextC] = false;
                                q.Enqueue((nextR, nextC));
                            }
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _r >= n || _c < 0 || _c >= n) return true;
                return false;
            }

            void Input()
            {

                n = int.Parse(sr.ReadLine());
                board = new int[n, n];
                visit = new bool[n, n];
                q = new(n * n);

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        int cur = sr.Read();
                        if (cur == 'R') continue;
                        if (cur == 'G') board[i, j] = 1;
                        else board[i, j] = 2;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }
            }
        }
    }

#if other
using System;

class MainClass {
    static int[] dx = { 1, -1, 0, 0 }; // 상하좌우 이동을 위한 배열
    static int[] dy = { 0, 0, 1, -1 };
    static int N;
    static char[,] picture;
    static bool[,] visited;

    public static void Main (string[] args) {
        N = int.Parse(Console.ReadLine());
        picture = new char[N, N];
        visited = new bool[N, N];

        for (int i = 0; i < N; i++) {
            string line = Console.ReadLine();
            for (int j = 0; j < N; j++) {
                picture[i, j] = line[j];
            }
        }

        int normalCount = CountAreas(false);
        ResetVisited();
        int colorBlindCount = CountAreas(true);

        Console.WriteLine(normalCount + " " + colorBlindCount);
    }

    static int CountAreas(bool colorBlind) {
        int count = 0;
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                if (!visited[i, j]) {
                    DFS(i, j, picture[i, j], colorBlind);
                    count++;
                }
            }
        }
        return count;
    }

    static void DFS(int x, int y, char color, bool colorBlind) {
        visited[x, y] = true;
        for (int i = 0; i < 4; i++) {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (nx >= 0 && nx < N && ny >= 0 && ny < N && !visited[nx, ny]) {
                if (colorBlind) {
                    if ((color == 'R' && picture[nx, ny] == 'G') || (color == 'G' && picture[nx, ny] == 'R') || picture[nx, ny] == color) {
                        DFS(nx, ny, color, colorBlind);
                    }
                } else {
                    if (picture[nx, ny] == color) {
                        DFS(nx, ny, color, colorBlind);
                    }
                }
            }
        }
    }

    static void ResetVisited() {
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                visited[i, j] = false;
            }
        }
    }
}

#endif
}
