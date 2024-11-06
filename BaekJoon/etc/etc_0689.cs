using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 13
이름 : 배성훈
내용 : 마법사 상어와 비바라기
    문제번호 : 21610번

    구현, 시뮬레이션 문제다
    큐를 이용해 비 구름을 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0689
    {

        static void Main689(string[] args)
        {

            StreamReader sr;
            int size, len;
            int[][] board;
            bool[][] visit;

            int[] dirR;
            int[] dirC;

            int dir;
            int move;

            Queue<(int r, int c)> q1;
            Queue<(int r, int c)> q2;

            Solve();

            void Solve()
            {

                Input();
                
                for (int i = 0; i < len; i++)
                {

                    dir = ReadInt() - 1;
                    move = ReadInt();

                    MoveCloud();
                    CopyWater();
                    MakeCloud();
                }

                sr.Close();

                int ret = GetRet();
                Console.WriteLine(ret);
            }

            int GetRet()
            {

                int ret = 0;
                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        ret += board[r][c];
                    }
                }

                return ret;
            }

            void MoveCloud()
            {

                while (q1.Count > 0)
                {

                    (int r, int c) node = q1.Dequeue();

                    int r = (node.r + move * dirR[dir]) % size;
                    int c = (node.c + move * dirC[dir]) % size;

                    if (r < 0) r += size;
                    if (c < 0) c += size;

                    visit[r][c] = true;
                    board[r][c] += 1;
                    q2.Enqueue((r, c));
                }
            }

            void CopyWater()
            {

                while (q2.Count > 0)
                {

                    (int r, int c) node = q2.Dequeue();
                    for (int i = 1; i < 8; i += 2)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC)) continue;
                        if (board[nextR][nextC] > 0) board[node.r][node.c] += 1;
                    }
                }
            }

            void MakeCloud()
            {

                if (q1.Count > 0) return;

                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        if (visit[r][c])
                        {

                            visit[r][c] = false;
                            continue;
                        }

                        if (board[r][c] < 2) continue;
                        board[r][c] -= 2;
                        q1.Enqueue((r, c));
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= size || _c >= size) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = ReadInt();
                len = ReadInt();

                board = new int[size][];
                visit = new bool[size][];
                for (int r = 0; r < size; r++)
                {

                    board[r] = new int[size];
                    visit[r] = new bool[size];
                    for (int c = 0; c < size; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                q1 = new(size * size);
                q2 = new(size * size);

                q1.Enqueue((size - 2, 0));
                q1.Enqueue((size - 2, 1));
                q1.Enqueue((size - 1, 0));
                q1.Enqueue((size - 1, 1));

                dirR = new int[8] { 0, -1, -1, -1, 0, 1, 1, 1 };
                dirC = new int[8] { -1, -1, 0, 1, 1, 1, 0, -1 };
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
namespace ConsoleApp1
{
    internal class Program
    {
        static int[] dy = new int[] { 0, -1, -1, -1, 0, 1, 1, 1 };
        static int[] dx = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 };
        static bool[,] visit;
        static int[,] map;
        static Queue<(int,int)> q = new();
        static int n, m;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            n = arr[0]; m = arr[1];
            map = new int[n, n];
            q.Enqueue((n - 1, 0)); q.Enqueue((n - 1, 1)); q.Enqueue((n - 2, 0)); q.Enqueue((n - 2, 1));
            for (int i = 0; i < n; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                for (int j = 0; j < n; j++)
                    map[i, j] = temp[j];
            }
            for(int i = 0; i < m; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                move(temp[0] - 1, temp[1]);
                copymake();
            }
            int answer = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    answer += map[i, j];
                }
            }

            output.Write(answer);

            input.Close();
            output.Close();
        }
        static void move(int dir,int dist)
        {
            visit = new bool[n, n];
            int len = q.Count;
            for(int i = 0; i < len; i++)
            {
                (int row, int col) = q.Dequeue();

                int nr = (row + dy[dir] * dist) % n;
                int nc = (col + dx[dir] * dist) % n;
                if (nr < 0)
                    nr += n;
                if(nc < 0)
                    nc += n;
                visit[nr, nc] = true;
                map[nr, nc]++;
            }
        }
        static void copymake()
        {
            int[] move = new int[] { 1, 3, 5, 7 };
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (visit[i, j])
                    {
                        int count = 0;
                        for(int k = 0; k < 4; k++)
                        {
                            int nr = i + dy[move[k]];
                            int nc = j + dx[move[k]];
                            if (nr < 0 || nr == n || nc < 0 || nc == n) continue;
                            if (map[nr, nc] > 0)
                                count++;
                        }
                        map[i, j] += count;
                    }
                }                    
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (visit[i, j]) continue;
                    if (map[i,j] >= 2)
                    {
                        q.Enqueue((i, j));
                        map[i, j] -= 2;
                    }                    
                }
            }
        }
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ALGO
{
    public struct Node
    {
        public int y { get; set; }
        public int x { get; set; }
        public Node(int _y, int _x)
        {
            y = _y; x = _x;
        }
    }

    class Program
    {
        static int[] xPos = { -1, -1, 0, 1, 1, 1, 0, -1 }; 
        static int[] yPos = { 0, -1, -1, -1, 0, 1, 1, 1 };

        static int N, M, score;
        static int[,] map = null;
        static List<int> cloudX = null;
        static List<int> cloudY = null;

        static StringBuilder sb = new StringBuilder();
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

        static void Main(string[] args)
        {
            sw.AutoFlush = true;
            string[] tmp = sr.ReadLine().Split(' ');
            N = Convert.ToInt32(tmp[0]);
            M = Convert.ToInt32(tmp[1]);
            map = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                tmp = sr.ReadLine().Split(' ');
                for (int j = 0; j < N; j++)
                {
                    map[i, j] = Convert.ToInt32(tmp[j]);
                }
            }

            cloudY = new List<int>(new int[] { N - 1, N - 1, N - 2, N - 2 });
            cloudX = new List<int>(new int[] { 0, 1, 0, 1 });

            int d, s, ny, nx;
            while(M > 0)
            {
                List<Node> lst = new List<Node>();
                int[,] water = new int[N,N];
                tmp = sr.ReadLine().Split(' ');
                d = Convert.ToInt32(tmp[0]) - 1;
                s = Convert.ToInt32(tmp[1]);

                // 구름이 d방향으로 s칸 이동하여 바구니의 물 1 증가
                for(int c = 0; c < cloudX.Count; c++)
                {
                    ny = (cloudY[c] + yPos[d] * s) % N;
                    nx = (cloudX[c] + xPos[d] * s) % N;
                    if (ny < 0) ny = ny + N;
                    if (nx < 0) nx = nx + N;

                    water[ny, nx]++;
                    map[ny, nx]++;
                    lst.Add(new Node(ny, nx));
                }

                //Print();

                // 구름이 사라진다.
                cloudX.Clear();
                cloudY.Clear();

                // 물복사버그 마법
                foreach (Node nde in lst)
                {
                    for (int p = 1; p < 8; p = p + 2)
                    {
                        ny = nde.y + yPos[p];
                        nx = nde.x + xPos[p];
                        if (ny >= N || nx >= N || ny < 0 || nx < 0) continue;

                        if (map[ny, nx] > 0)
                        {
                            map[nde.y, nde.x]++;
                        }
                    }
                }

                //Print();

                for (int i = 0; i < N; i++)
                {
                    for(int j = 0; j < N; j++)
                    {
                        // 구름이 사라진 칸이 아니어야 하므로
                        if (water[i, j] > 0) continue;

                        if(map[i, j] >= 2)
                        {
                            cloudY.Add(i);
                            cloudX.Add(j);
                            map[i, j] -= 2;
                        }
                    }
                }

                M--;
            }

            // 바구니에 들어있는 물의 양의 합
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    score += map[i, j];
                }
            }

            sw.WriteLine(score.ToString());
        }
        
        private static void Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    sb.Append(map[i, j] + " ");
                }
                sb.Append(Environment.NewLine);
            }

            sw.WriteLine(sb.ToString());
        }
    }
}

#endif
}
