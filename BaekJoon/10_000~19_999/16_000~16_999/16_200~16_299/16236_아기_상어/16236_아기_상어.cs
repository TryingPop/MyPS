using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 1
이름 : 배성훈
내용 : 아기 상어
    문제번호 : 16236번

    구현 문제다

    상어 위치에서 먹이 탐색을 BFS로 먼저하고
    먹이를 발견하면 해당 먹이로 상어를 이동 및 먹이를 빈땅으로 표시하고 다시 먹이 탐색
    먹이를 발견 못하면 탐색 종료하는 식으로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0113
    {

        static void Main113(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[][] board = new int[n][];
            int[][] visit = new int[n][];
            int[] shark = new int[2];
            Queue<(int x, int y)> q = new(n * n);

            for (int i = 0; i < n; i++)
            {

                board[i] = new int[n];
                visit[i] = new int[n];
                for (int j = 0; j < n; j++)
                {

                    int val = ReadInt(sr);
                    if (val == 9) 
                    {

                        shark[0] = i;
                        shark[1] = j;
                        val = 0;
                    }

                    board[i][j] = val;
                }
            }
            sr.Close();

            int curLvl = 2;
            int curExp = 0;

            int ret = 0;

            int[] dirX = { -1, 0, 1, 0 };
            int[] dirY = { 0, -1, 0, 1 };

            while (true)
            {

                bool finish = true;

                // 먹이 탐색
                q.Enqueue((shark[0], shark[1]));
                visit[shark[0]][shark[1]] = 1;
                int add = 0;
                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    if (add != 0 && visit[node.x][node.y] > add) continue;

                    for (int i = 0; i < 4; i++)
                    {

                        int nextX = node.x + dirX[i];
                        int nextY = node.y + dirY[i];

                        if (ChkInvalidPos(nextX, nextY, n) || visit[nextX][nextY] != 0) continue;

                        if (board[nextX][nextY] > curLvl) continue;
                        // 빈땅이거나 크기가 같으면 못먹지만 지나갈 수 있다
                        else if (board[nextX][nextY] == curLvl || board[nextX][nextY] == 0)
                        { 
                            
                            visit[nextX][nextY] = visit[node.x][node.y] + 1;
                            if (finish) q.Enqueue((nextX, nextY));
                            continue;
                        }
                        
                        // 먹이 발견 -> 우선 좌표 확인
                        if (add == 0
                            || shark[0] > nextX 
                            || (shark[0] == nextX && shark[1] > nextY))
                        {

                            shark[0] = nextX;
                            shark[1] = nextY;
                            add = visit[node.x][node.y];
                        }
                        finish = false;
                    }
                }

                if (finish) break;

                // 먹이를 발견한 경우만 여기로 온다
                // 경험치 증가
                curExp++;
                if (curExp == curLvl)
                {

                    curExp = 0;
                    curLvl++;
                }

                // 상어 이동
                ret += add;
                board[shark[0]][shark[1]] = 0;
                // 초기화
                for (int i = 0; i < n; i++)
                {

                    Array.Fill(visit[i], 0);
                }
            }

            Console.WriteLine(ret);
        }

        static bool ChkInvalidPos(int _x, int _y, int _n)
        {

            if (_x < 0 || _y < 0 || _x >= _n || _y >= _n) return true;
            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
#if other
    class _16236
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[][] map = new int[n][];
            (int row, int column) shark = (-1, -1);
            for (int i = 0; i < n; i++)
            {
                map[i] = new int[n];
                string[] row = Console.ReadLine().Split(' ');
                for(int j = 0; j < n; j++)
                {
                    map[i][j] = int.Parse(row[j]);
                    if (map[i][j] == 9)
                    {
                        shark = (i, j);
                        map[i][j] = 0;
                    }
                        
                }
            }

            int sharkSize = 2;
            int ate = 0;
            int answer = 0;
            while (true)
            {
                (int row, int column, int distance) feed = BFS(map, shark, sharkSize);
                if (feed.distance == int.MaxValue) break;
                
                answer += feed.distance;
                ate++;
                if(ate == sharkSize)
                {
                    sharkSize++;
                    ate = 0;
                }
                shark = (feed.row, feed.column);
                map[feed.row][feed.column] = 0;
            }

            Console.WriteLine(answer);
        }

        static (int row, int column, int distance) BFS(int[][] map, (int row, int column) shark, int sharkSize)
        {
            (int x, int y)[] udlr = { (-1, 0), (0, -1), (0, 1), (1, 0) };
            bool[,] visited = new bool[map.Length, map[0].Length];
            Queue<(int row, int column, int distance)> queue = new Queue<(int row, int column, int distance)>();
            (int row, int column, int distance) current = (shark.row, shark.column, 0);
            queue.Enqueue(current);

            (int row, int column, int distance) feed = (int.MaxValue, int.MaxValue, int.MaxValue);
            while(queue.Count > 0)
            {
                current = queue.Dequeue();
                
                if (feed.distance < current.distance) break;

                if (visited[current.row, current.column]) continue;
                visited[current.row, current.column] = true;

                if (map[current.row][current.column] > sharkSize) continue;

                if (map[current.row][current.column] < sharkSize && map[current.row][current.column] > 0)
                {
                    if (feed.distance > current.distance
                        || feed.row > current.row
                        || (feed.column > current.column && feed.row == current.row))
                        feed = current;
                }

                for (int i = 0; i < udlr.Length; i++)
                {
                    int r = current.row + udlr[i].x;
                    int c = current.column + udlr[i].y;
                    if (r < 0 || c < 0 || r >= map.Length || c >= map[0].Length) continue;
                    if (map[r][c] > sharkSize) continue;

                    queue.Enqueue((r, c, current.distance + 1));
                }
            }

            return feed;
        }
    }
#endif
}
