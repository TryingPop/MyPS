using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 30
이름 : 배성훈
내용 : 마법사 상어와 파이어볼
    문제번호 : 20056번

    구현, 시뮬레이션 문제다
    큐를 이용해 저장한 파이어볼을 이동시켰다
    이동한 파이어볼은 배열에 기록했다

    그리고 배열을 읽으면서 합쳐진 경우 분할을 하고,
    한 개인 경우 그대로 채워넣었다

    이렇게 조건대로 반복해서 질량합을 구했다
*/

namespace BaekJoon.etc
{
    internal class etc_0745
    {

        static void Main745(string[] args)
        {

            StreamReader sr;
            int size, k;
            Queue<(int r, int c, int m, int s, int d)> fireball;
            (int cnt, int m, int s, int d)[][] board;

            int[] dirR;
            int[] dirC;

            int[] odd;
            int[] even;

            Solve();

            void Solve()
            {

                Input();

                for (int i = 0; i < k; i++)
                {

                    Move();
                    ChkBoard();
                }

                Console.Write(GetRet());
            }

            int GetRet()
            {

                int ret = 0;
                while(fireball.Count > 0)
                {

                    var node = fireball.Dequeue();
                    ret += node.m;
                }

                return ret;
            }

            void Move()
            {

                // 파이어볼 이동
                while(fireball.Count > 0)
                {

                    var node = fireball.Dequeue();

                    int nextR = (node.r + node.s * dirR[node.d]) % size;
                    int nextC = (node.c + node.s * dirC[node.d]) % size;

                    nextR = nextR < 0 ? nextR + size : nextR;
                    nextC = nextC < 0 ? nextC + size : nextC;

                    board[nextR][nextC].cnt++;
                    board[nextR][nextC].s += node.s;
                    if (board[nextR][nextC].cnt == 1) board[nextR][nextC].d = node.d;
                    else
                    {

                        int d = board[nextR][nextC].d;
                        if (d != 101 && d % 2 != node.d % 2) board[nextR][nextC].d = 101;
                    }
                    board[nextR][nextC].m += node.m;
                }
            }

            void ChkBoard()
            {

                // 파이어볼 채워넣기
                // 겹쳐진 경우 분할도 여기서 한다
                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        if (board[r][c].cnt == 0) continue;

                        // 1개짜리면 그냥 바로 채운다
                        if (board[r][c].cnt == 1) fireball.Enqueue((r, c, board[r][c].m, board[r][c].s, board[r][c].d));
                        else
                        {

                            // 2개 이상 겹친 경우 조건대로 분할 연산
                            int m = board[r][c].m / 5;
                            int s = board[r][c].s / board[r][c].cnt;

                            Fill(r, c, m, s, board[r][c].d == 101);
                        }

                        board[r][c] = (0, 0, 0, 0);
                    }
                }
            }

            void Fill(int r, int c, int m, int s, bool _isOdd)
            {

                // 분할된 경우 파이어볼 채우기
                if (m == 0) return;

                for (int i = 0; i < 4; i++)
                {

                    // 조건에 따라 홀짝인지 판변한 후 번호대로 채워넣는다
                    fireball.Enqueue((r, c, m, s, _isOdd ? odd[i] : even[i]));
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = ReadInt();
                int n = ReadInt();
                k = ReadInt();

                board = new (int cnt, int m, int s, int d)[size][];
                for (int i = 0; i <size; i++)
                {

                    board[i] = new (int cnt, int m, int s, int d)[size];
                }

                fireball = new(size * size * 4);
                for (int i = 0; i < n; i++)
                {

                    int r = ReadInt() - 1;
                    int c = ReadInt() - 1;
                    int m = ReadInt();
                    int s = ReadInt();
                    int d = ReadInt();

                    fireball.Enqueue((r, c, m, s, d));
                }

                dirR = new int[8] { -1, -1, 0, 1, 1, 1, 0, -1 };
                dirC = new int[8] { 0, 1, 1, 1, 0, -1, -1, -1 };

                odd = new int[4] { 1, 3, 5, 7 };
                even = new int[4] { 0, 2, 4, 6 };
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
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static Queue<(int, int, int)>[,] map;
        static int n;
        static Queue<(int, int, int, int, int)> fire = new();
        static int[] dy = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 };
        static int[] dx = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 };
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            n = arr[0]; int m = arr[1]; int k = arr[2];
            map = new Queue<(int, int, int)>[n + 1, n + 1];
            for(int i = 0; i < m; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                fire.Enqueue((temp[0], temp[1], temp[2], temp[3], temp[4]));
            }
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    map[i, j] = new();
                }
            }
            for (int i = 0; i < k; i++)
            {
                move();
            }
            int answer = 0;
            while(fire.Count > 0)
            {
                (int r, int c, int w, int s, int d) = fire.Dequeue();

                answer += w;
            }

            output.Write(answer);

            input.Close();
            output.Close();
        }
        static void move()
        {
            while (fire.Count > 0)
            {
                (int r,int c,int m,int s,int d) = fire.Dequeue();

                int nr = r + dy[d] * (s % n);
                int nc = c + dx[d] * (s % n);
                if(nr > n || nc > n)
                {
                    nr %= n;
                    nc %= n;
                }
                if(nr < 1)
                    nr += n;
                if(nc < 1)
                    nc += n;

                map[nr,nc].Enqueue((m, s, d));                
            }
            div();
        }
        static void div()
        {
            for(int i = 1; i <= n; i++)
            {
                for(int j = 1; j <= n; j++)
                {
                    if (map[i,j].Count >= 2)
                    {
                        int m = 0;
                        int s = 0;
                        int count = map[i,j].Count;
                        int od = 0;
                        int ev = 0;
                        while (map[i,j].Count > 0)
                        {
                            (int w, int sp, int d) = map[i,j].Dequeue();
                            m += w;
                            s += sp;
                            if (d % 2 == 0)
                                ev++;
                            else
                                od++;
                        }
                        m /= 5;
                        if (m == 0) continue;
                        s /= count;
                        int k;
                        if (ev == 0 || od == 0)
                            k = 0;
                        else
                            k = 1;
                        for (; k < 8; k += 2)
                            fire.Enqueue((i, j, m, s, k));
                    }
                    else if (map[i,j].Count == 1)
                    {
                        (int m, int s, int d) = map[i, j].Dequeue();
                        fire.Enqueue((i, j, m, s, d));
                    }                        
                }
            }
        }
    }
}
#elif other2
var reader = new Reader();

var (n, m, k) = (reader.NextInt(), reader.NextInt(), reader.NextInt());
var fireballs = new Queue<Fireball>();

while (m-- > 0)
    fireballs.Enqueue(new(new(reader.NextInt() - 1, reader.NextInt() - 1), reader.NextInt(), reader.NextInt(), reader.NextInt()));

var fireballQueues = new Queue<Fireball>[n, n];
var coordQueue = new Queue<Point>();
var coordChecked = new bool[n, n];

while (k-- > 0)
{
    // Move fireballs and enqueue.
    while (fireballs.Count > 0)
    {
        var fireball = fireballs.Dequeue();
        fireball.Move(n);

        var (x, y) = fireball.Coordinate;
        if (coordChecked[x, y] == false)
            coordQueue.Enqueue(fireball.Coordinate);

        (fireballQueues[x, y] ??= new()).Enqueue(fireball);
        coordChecked[x, y] = true;
    }
    
    /*
    Console.WriteLine(new string('-', 50));
    foreach (var c in coordQueue)
    {
        Console.WriteLine($"Queue {(c.X, c.Y)}");
        foreach (var f in fireballQueues[c.X, c.Y])
            Console.WriteLine(f);
        Console.WriteLine();
    }
    Console.WriteLine(new string('-', 50));
    */

    // Cast a magic!
    while (coordQueue.Count > 0)
    {
        var (x, y) = coordQueue.Dequeue();
        var fireballQueue = fireballQueues[x, y];
        
        if (fireballQueue.Count == 1)
        {
            fireballs.Enqueue(fireballQueue.Dequeue());
        }
        else
        {
            int fireballCount = fireballQueue.Count;
            int totalMass = 0;
            int totalVelocity = 0;
            int oddEvenOffset = 0;
            int lastDir = -1;

            while(fireballQueue.Count > 0)
            {
                var fb = fireballQueue.Dequeue();
                totalMass += fb.Mass;
                totalVelocity += fb.Velocity;

                if (lastDir == -1)
                    lastDir = fb.Direction;
                else
                {
                    if (lastDir % 2 != fb.Direction % 2)
                        oddEvenOffset = 1;
                    
                    lastDir = fb.Direction;
                }
            }

            int mass = totalMass / 5;
            int velocity = totalVelocity / fireballCount;

            if (mass != 0)
                for (int dir = oddEvenOffset; dir < 8; dir += 2)
                    fireballs.Enqueue(new(new(x, y), mass, velocity, dir));
        }

        coordChecked[x, y] = false;
    }

    /*
    foreach (var f in fireballs)
        Console.WriteLine(f);
    Console.WriteLine();
    */
}

Console.Write(fireballs.Sum(x => x.Mass));

class Fireball
{
    private static readonly Point[] directions = new Point[8] {
        new(-1, 0),
        new(-1, 1),
        new(0, 1),
        new(1, 1),
        new(1, 0),
        new(1, -1),
        new(0, -1),
        new(-1, -1)
    };

    public Point Coordinate { get; private set; }

    public int Mass { get; private set; }

    public int Direction { get; private set; }

    public int Velocity { get; private set; }

    public Fireball(Point coordinate, int mass, int velocity, int direction)
    {
        Coordinate = coordinate;
        Mass = mass;
        Direction = direction;
        Velocity = velocity;
    }

    public void Move(int bound) => Coordinate = (Coordinate + directions[Direction] * Velocity) % bound;

    public override string ToString()
    {
        return $"Fireball at ({Coordinate.X}, {Coordinate.Y}), (M: {Mass}, V: {Velocity}, D: {Direction})";
    }
}

struct Point
{
    public int X { get; set; }

    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point p, int scaler) => new(p.X * scaler, p.Y * scaler);
    public static Point operator %(Point p, int mod) => new((p.X % mod + mod) % mod, (p.Y % mod + mod) % mod);

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#elif other3
public static class PS
{
    private static (int r, int c)[] dir =
        { (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1) };

    private class Fireball
    {
        public int Mass { get; }
        public int Speed { get; }
        public int Dir { get; }
        public bool CanMove { get; set; }

        public Fireball(int mass, int speed, int dir)
        {
            Mass = mass;
            Speed = speed;
            Dir = dir;
            CanMove = true;
        }
    }

    private static int n, k;
    private static List<Fireball>[,] map;

    static PS()
    {
        string[] input;

        input = Console.ReadLine().Split();
        n = int.Parse(input[0]);      
        k = int.Parse(input[2]);
        int m = int.Parse(input[1]);
        map = new List<Fireball>[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                map[i, j] = new();
            }
        }

        while (m-- > 0)
        {
            input = Console.ReadLine().Split();
            map[int.Parse(input[0]) - 1, int.Parse(input[1]) - 1].Add(
                new Fireball(int.Parse(input[2]), int.Parse(input[3]), int.Parse(input[4])));
        }
    }

    public static void Main()
    {
        while (k-- > 0)
        {
            Move();
            Split();
        }

        Console.Write(CheckMass());
    }

    private static void Move()
    {
        int moveR;
        int moveC;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (map[i, j].Count != 0)
                {
                    for (int k = map[i, j].Count - 1; k >= 0; k--)
                    {
                        if (map[i, j][k].CanMove)
                        {
                            map[i, j][k].CanMove = false;
                            moveR = i;
                            moveC = j;

                            switch (dir[map[i, j][k].Dir].r)
                            {
                                case < 0: moveR = (n + i - (map[i, j][k].Speed % n)) % n; break;
                                case > 0: moveR = (i + (map[i, j][k].Speed % n)) % n; break;
                            }
                                
                            switch (dir[map[i, j][k].Dir].c)
                            {
                                case < 0: moveC = (n + j - (map[i, j][k].Speed % n)) % n; break;
                                case > 0: moveC = (j + (map[i, j][k].Speed % n)) % n; break;
                            }           

                            if (moveR != i || moveC != j)
                            {
                                map[moveR, moveC].Add(map[i, j][k]);
                                map[i, j].RemoveAt(k);
                            }
                        }
                    }
                }
            }
        }
    }

    private static void Split()
    {
        int mSum;
        int sSum;
        bool dFlag;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (map[i, j].Count >= 2)
                {
                    mSum = map[i, j][0].Mass;
                    sSum = map[i, j][0].Speed;
                    dFlag = true;

                    for (int k = 1; k < map[i, j].Count; k++)
                    {
                        mSum += map[i, j][k].Mass;
                        sSum += map[i, j][k].Speed;
                        
                        if (dFlag && map[i, j][k].Dir % 2 != map[i, j][k - 1].Dir % 2)
                            dFlag = false;
                    }

                    mSum /= 5;
                    sSum /= map[i, j].Count;
                    map[i, j].Clear();

                    if (mSum == 0)
                        continue;

                    for (int k = 0; k < 4; k++)
                    {
                        map[i, j].Add(
                            new Fireball(mSum, sSum, dFlag ? k * 2 : k * 2 + 1));
                    }
                }
                else if (map[i, j].Count != 0)
                {
                    foreach (var fireball in map[i, j])
                    {
                        fireball.CanMove = true;
                    }
                }
            }
        }
    }

    private static int CheckMass()
    {
        int sum = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (map[i, j].Count != 0)
                {
                    foreach (var fireball in map[i, j])
                    {
                        sum += fireball.Mass;
                    }
                }
            }
        }

        return sum;
    }
}
#endif
}
