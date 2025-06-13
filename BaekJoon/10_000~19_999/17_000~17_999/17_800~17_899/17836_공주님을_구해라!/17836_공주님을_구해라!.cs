using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 16
이름 : 배성훈
내용 : 공주님을 구해라!
    문제번호 : 17836번

    두 개의 맵을 만들어 풀었다
    다 풀고 나서 보니 해당 방법보다 더 좋은게 보인다

    공주님 위치를 처음부터 알고 있기에
    검을 얻을 경우 바로 계산하는 방법이 있다
    여기서는 가로 끝, 세로 끝에 위치해 있으므로 가로차, 세로차의 합으로 계산하면 된다;
    해당 방법은 공주님 위치가 바뀔 때 몇 턴이 걸리는지 알 수 있는 방법인거 같다

    방법을 바꿔서 검을 찾으면 바로 골로 가는 경우를 했고,
    맵을 1개로 하니 경우의 수를 따진다고 메모리는 적게 쓰나 속도가 느려졌다

    그리고 두 번째 방법으로 제출하면서 잦은 실수로 많이 틀렸다
    1. ret < maxTime에서 한번 틀렸고
    2. MAX를 1만으로해서 못가는 데도 불구하고, ret <= maxTime했음에도 maxTime <= 10_000이므로 1만을 출력해
    3. board[nextY][nextX] < cur + 1로 하다가 무한루프
*/

namespace BaekJoon.etc
{
    internal class etc_0041
    {

        static void Main41(string[] args)
        {

            int MAX = 10_001;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int height = ReadInt(sr);
            int width = ReadInt(sr);
            int maxTime = ReadInt(sr);

            // 2개의 보드를?
            int[][] map = new int[height][];
#if first
            int[][] board = new int[height][];
            int[][] cheatBoard = new int[height][];

            for (int i = 0; i < height; i++)
            {

                map[i] = new int[width];
                board[i] = new int[width];
                cheatBoard[i] = new int[width];

                for (int j = 0; j < width; j++)
                {

                    map[i][j] = ReadInt(sr);
                    board[i][j] = MAX;
                    cheatBoard[i][j] = MAX;
                }
            }

            Queue<(int x, int y, bool sword)> q = new Queue<(int x, int y, bool sword)>(width * height);
            q.Enqueue((0, 0, map[0][0] == 2));
            board[0][0] = 0;
            cheatBoard[0][0] = 0;

            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };
            
            while(q.Count > 0)
            {

                var node = q.Dequeue();
                int cur = node.sword ? cheatBoard[node.y][node.x] : board[node.y][node.x];
                if (cur > maxTime) 
                {

                    q.Clear();
                    break; 
                }

                if (cur > maxTime) continue;
                for (int i = 0; i < 4; i++)
                {

                    int nextX = node.x + dirX[i];
                    int nextY = node.y + dirY[i];

                    if (ChkInvalidPos(nextX, nextY, width, height)) continue;

                    // 검이 있는 경우 없는 경우 다름~!
                    if (node.sword)
                    {

                        // 검 있으면!
                        if (cheatBoard[nextY][nextX] <= cur + 1) continue;

                        cheatBoard[nextY][nextX] = cur + 1;
                        q.Enqueue((nextX, nextY, true));
                    }
                    else
                    {

                        int mapState = map[nextY][nextX];
                        if (mapState == 1) continue;

                        // 지나온 곳
                        if (board[nextY][nextX] <= cur + 1) continue;

                        board[nextY][nextX] = cur + 1;
                        if (mapState == 2) cheatBoard[nextY][nextX] = cur + 1;
                        q.Enqueue((nextX, nextY, mapState == 2));
                    }

                    if (nextX == width - 1 && nextY == height - 1)
                    {

                        q.Clear();
                        break;
                    }
                }
            }

            int bGoal = board[height - 1][width - 1];
            int sGoal = cheatBoard[height - 1][width - 1];

            int ret = bGoal < sGoal ? bGoal : sGoal;

            if (ret == MAX) Console.WriteLine("Fail");
            else Console.WriteLine(ret);
#else
            int[][] board = new int[height][];

            for (int i = 0; i < height; i++)
            {

                map[i] = new int[width];
                board[i] = new int[width];

                for (int j = 0; j < width; j++)
                {

                    map[i][j] = ReadInt(sr);
                    board[i][j] = MAX;
                }
            }

            Queue<(int x, int y)> q = new Queue<(int x, int y)>(height * width);
            q.Enqueue((0, 0));
            board[0][0] = 0;

            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };


            while(q.Count > 0)
            {

                var node = q.Dequeue();
                int cur = board[node.y][node.x];

                if (cur > maxTime || (node.y == height -1 && node.x == width - 1)) break;

                for (int i = 0; i < 4; i++)
                {

                    int nextX = node.x + dirX[i];
                    int nextY = node.y + dirY[i];

                    if (ChkInvalidPos(nextX, nextY, width, height)) continue;

                    int chkMap = map[nextY][nextX];
                    if (chkMap == 1) continue;
                    else if (chkMap == 2)
                    {

                        map[nextY][nextX] = 0;
                        board[nextY][nextX] = cur + 1;
                        int chk = (cur + 1) + (height - 1 - nextY) + (width - 1 - nextX);
                        q.Enqueue((nextX, nextY));
                        if (board[height - 1][width - 1] > chk) board[height - 1][width - 1] = chk;
                        continue;
                    }

                    if (board[nextY][nextX] <= cur + 1) continue;

                    board[nextY][nextX] = cur + 1;

                    q.Enqueue((nextX, nextY));
                }
            }

            int ret = board[height - 1][width - 1];
            if (ret <= maxTime) Console.WriteLine(ret);
            else Console.WriteLine("Fail");
#endif
        }

        static bool ChkInvalidPos(int _x, int _y, int _width, int _height)
        {

            if (_x < 0 || _width <= _x) return true;
            else if (_y < 0 || _height <= _y) return true;

            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
#if other
var reader = new Reader();
var (n, m, t) = (reader.NextInt(), reader.NextInt(), reader.NextInt());

var map = new int[n, m];
for (int i = 0; i < n; i++)
    for (int j = 0; j < m; j++)
        map[i, j] = reader.NextInt();

var dir = new (int x, int y)[4] { (-1, 0), (1, 0), (0, -1), (0, 1) };
var queue = new Queue<(int x, int y, int s)>();
var visited = new bool[n, m];

queue.Enqueue((0, 0, 0));
visited[0, 0] = true;

int withGramDist = 10001;
while (queue.Count > 0)
{
    var (cx, cy, cs) = queue.Dequeue();

    if (cx == n - 1 && cy == m - 1)
    {
        Console.Write(Math.Min(withGramDist, cs));
        return;
    }

    if (map[cx, cy] == 2)
    {
        var dist = cs + (n - 1 - cx) + (m - 1 - cy);
        if (dist <= t)
            withGramDist = dist;
    }

    if (cs == t)
        continue;

    foreach (var d in dir)
    {
        var (dx, dy) = (cx + d.x, cy + d.y);
        if (dx < 0 || dx >= n || dy < 0 || dy >= m)
            continue;

        if (visited[dx, dy] == true)
            continue;

        if (map[dx, dy] == 1)
            continue;

        queue.Enqueue((dx, dy, cs + 1));
        visited[dx, dy] = true;
    }
}

Console.Write(withGramDist != 10001 ? withGramDist : "Fail");

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#endif
}
