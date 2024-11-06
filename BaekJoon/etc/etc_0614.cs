using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 25
이름 : 배성훈
내용 : 로봇 청소기
    문제번호 : 14503번

    구현, 시뮬레이션 문제다
    BFS 형태로 시뮬레이션을 돌렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0614
    {

        static void Main614(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] board;
            int dir;
            Queue<(int r, int c)> q;

            int ret = 0;

            Solve();

            void Solve()
            {

                Input();

                BFS();
                Console.WriteLine(ret);
            }

            void BFS()
            {

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, 1, 0, -1 };

                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    if (board[node.r][node.c] == 0)
                    {

                        ret++;
                        board[node.r][node.c] = 1;
                    }

                    bool move = false;
                    for (int i = 0; i < 4; i++)
                    {

                        dir = dir == 0 ? 3 : dir - 1;
                        int nextR = node.r + dirR[dir];
                        int nextC = node.c + dirC[dir];

                        if (board[nextR][nextC] != 0) continue;

                        q.Enqueue((nextR, nextC));
                        move = true;
                        break;
                    }

                    if (move) continue;

                    node.r -= dirR[dir];
                    node.c -= dirC[dir];
                    if (board[node.r][node.c] != -1) q.Enqueue((node.r, node.c));
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                row = ReadInt();
                col = ReadInt();

                q = new(1);

                q.Enqueue((ReadInt(), ReadInt()));
                dir = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = ReadInt();
                        if (cur == 1) board[r][c] = -1;
                    }
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while(( c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System.IO;

int n, m, r, c, d;
bool[,] map;

using (var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
{
    n = ScanInt();
    m = ScanInt();
    r = ScanInt();
    c = ScanInt();
    d = sr.Read() - '0';
    if (sr.Read() == '\r') sr.Read();

    map = new bool[n, m];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
            map[i, j] = sr.Read() == '1';
            if (sr.Read() == '\r') sr.Read();
        }
    }

    int ScanInt()
    {
        int c, n = 0;
        while ((c = sr.Read()) != '\n' && c != ' ' && c != -1)
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
}

var cleaned = new bool[n, m];
var cleanNum = 0;


do
{
    if (!cleaned[r, c])
    {
        cleaned[r, c] = true;
        cleanNum++;
    }

    var foundToClean = false;
    for (int i = 0; i < 4; i++)
    {
        d = (d + 3) % 4;
        int nextR = r, nextC = c;
        switch (d)
        {
            case 0:
                if (nextR-- == 0)
                    continue;
                break;
            case 1:
                if (++nextC == m)
                    continue;
                break;
            case 2:
                if (++nextR == n)
                    continue;
                break;
            default:
                if (nextC-- == 0)
                    continue;
                break;
        }

        if (!map[nextR, nextC] && !cleaned[nextR, nextC])
        {
            r = nextR;
            c = nextC;
            foundToClean = true;
            break;
        }
    }
    if (foundToClean)
        continue;

    var needToStop = d switch
    {
        0 => ++r == n,
        1 => c-- == 0,
        2 => r-- == 0,
        _ => ++c == m,
    } || map[r, c];
    if (needToStop)
        break;
} while (true);

Console.Write(cleanNum);
#elif other2
StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));

int[] nm = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
int[] rcd = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

int[,] map = new int[nm[0], nm[1]];
int[] row;

for (int i = 0; i < nm[0]; i++)
{
    row = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

    for (int j = 0; j < nm[1]; j++)
    {
        map[i, j] = row[j];
    }
}

sr.Close();

Robot robotVacuum = new Robot(map, rcd[0], rcd[1], (Dir)rcd[2]);
robotVacuum.PowerOn();

Console.Write(robotVacuum.CleanCnt);


enum Dir
{
    North,
    East,
    South,
    West
}

class Robot
{
    private int[,] map;
    private int r, c;
    private Dir d;

    private int stopCnt;
    public int CleanCnt { get; private set; }

    public Robot(int[,] map, int r, int c, Dir d)
    {
        this.map = map;
        this.r = r;
        this.c = c;
        this.d = d;

        stopCnt = 0;
        CleanCnt = 0;
    }

    public void Clean()
    {
        map[r, c] = 2;
    }

    public bool CanClean()
    {
        switch (d)
        {
            case Dir.North:
                if (map[r, c - 1] != 0) return false;
                break;

            case Dir.East:
                if (map[r - 1, c] != 0) return false;
                break;

            case Dir.South:
                if (map[r, c + 1] != 0) return false;
                break;

            case Dir.West:
                if (map[r + 1, c] != 0) return false;
                break;
        }

        return true;
    }

    public void Turn()
    {
        d = (Dir)((3 + (int)d) % 4);
    }

    public void Move(bool back = false)
    {
        switch (d)
        {
            case Dir.North:
                if (back) r++; else r--;
                break;

            case Dir.East:
                if (back) c--; else c++;
                break;

            case Dir.South:
                if (back) r--; else r++;
                break;

            case Dir.West:
                if (back) c++; else c--;
                break;
        }
    }

    public bool CanStop()
    {
        switch (d)
        {
            case Dir.North:
                if (map[r + 1, c] == 1) return true;
                break;

            case Dir.East:
                if (map[r, c - 1] == 1) return true;
                break;

            case Dir.South:
                if (map[r - 1, c] == 1) return true;
                break;

            case Dir.West:
                if (map[r, c + 1] == 1) return true;
                break;
        }

        return false;
    }

    public void PowerOn()
    {
        while (true)
        {
            //Step 1
            if (map[r, c] == 0)
            {
                Clean();
                CleanCnt++;
            }

            //Step 2
            if (CanClean())
            {
                Turn();
                Move();
                stopCnt = 0;
            }
            else
            {
                Turn();
                stopCnt++;
            }

            //Step 3
            if (stopCnt == 4)
            {
                if (CanStop()) break;
                else Move(true);
                stopCnt = 0;
            }
        }
    }
}
#endif
}
