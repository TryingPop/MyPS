using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 7
이름 : 배성훈
내용 : 볼록 껍질
    문제번호 : 1708번

    기하학, 볼록 껍질 문제다
    CCW 부분에 곱 연산 합연산으로 잘못해서 몇 시간동안 고생했다

    아이디어는 다음과 같다
    먼저 기준이 될 점을 하나 선택한다
    찾아본 사이트에서는 y가 가장 작고, x를 가장 작은 점으로 기준을 삼아 정렬한다
    기준점만 찾아 0번으로 오면 된다

    그리고 기준 점과 직선을 그으면서, 반시계? 방향으로 정렬한다
    만약 같은 직선에 있는 세 점인 경우 가까운 점을 앞에 오게 한다

    반시계 방향 정렬에 CCW 알고리즘이 쓰인다
    반시계 방향은 ccw가 양수이다
    그런데, 정렬에서 앞에 오게 하려면, 음수여야한다
    그래서 반시계 정렬에서 -ccw가 되게 해야한다

    이제 정렬한 점들에 대해, 하나씩 스택에 넣는다
    3개가 되면 끝에 반시계인지 확인한다
    그리고 시계방향인 경우 중간점을 제외한다
    이렇게 빼가면서 3개가 되면 다음 점으로 넘어간다
    이렇게 나오는 마지막 결과가 정답이된다
*/

namespace BaekJoon._49
{
    internal class _49_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr;
            int n;
            int ret;
            Point[] pos;
            int[] arr;

            Solve();

            void Solve()
            {

                Input();

                Array.Sort(pos, 1, n - 1, Comparer<Point>.Create((x, y) => Point.Comp(x, y, pos[0])));

                ret = 0;
                for (int i = 0; i < n; i++)
                {

                    while (ret >= 2 && Point.CCW(pos[arr[ret - 2]], pos[arr[ret - 1]], pos[i]) <= 0) { ret--; }
                    Push(i);
                }

                Console.WriteLine(ret);
            }

            void Push(int _n)
            {

                arr[ret++] = _n;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                pos = new Point[n];
                arr = new int[n];

                int minY = 50_000;
                int minX = 50_000;
                int minIdx = -1;
                for (int i = 0; i < n; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    pos[i].Set(x, y);

                    if (y < minY)
                    {

                        minY = y;
                        minX = x;
                        minIdx = i;
                    }
                    else if (y == minY && x < minX)
                    {

                        minX = x;
                        minIdx = i;
                    }
                }

                Point temp = pos[0];
                pos[0] = pos[minIdx];
                pos[minIdx] = temp;

                sr.Close();
            }
            
            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }

        struct Point
        {

            private int x;
            private int y;

            public static int CCW(Point _a, Point _b, Point _c)
            {

                long ccw = _a.x * _b.y;
                ccw += _b.x * _c.y;
                ccw += _c.x * _a.y;

                ccw -= _a.y * _b.x;
                ccw -= _b.y * _c.x;
                ccw -= _c.y * _a.x;

                if (ccw > 0) return 1;
                else if (ccw < 0) return -1;
                else return 0;
            }

            public static long Distance(Point _a, Point _b)
            {

                long x = (_a.x - _b.x);
                x *= x;
                long y = (_a.y - _b.y);
                y *= y;
                return x + y;
            }

            public void Set(int _x, int _y)
            {

                x = _x;
                y = _y;
            }

            public static int Comp(Point _a, Point _b, Point _help)
            {

                int ccw = CCW(_a, _b, _help);
                if (ccw == 0) return Distance(_a, _help).CompareTo(Distance(_b, _help));
                return -ccw;
            }
        }
    }

#if other
using System;
using System.IO;
using System.Linq;

#nullable disable

public record struct Point(long Y, long X);

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var points = new Point[n];

        for (var idx = 0; idx < n; idx++)
        {
            var l = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
            points[idx] = new Point(l[1], l[0]);
        }

        var hull = MonotoneChain(points);
        sw.WriteLine(hull.Length);
    }

    public static Point[] MonotoneChain(Point[] points)
    {
        var sorted = points
            .OrderBy(v => v.X)
            .ThenBy(v => v.Y)
            .ToArray();

        var buffer = new int[1 + points.Length];
        var inset = new bool[points.Length];
        var k = 0;

        for (var idx = 0; idx < sorted.Length; idx++)
        {
            buffer[k++] = idx;
            inset[idx] = true;

            while (k >= 3)
            {
                var ccw = CCW(sorted[buffer[k - 3]], sorted[buffer[k - 2]], sorted[buffer[k - 1]]);

                if (ccw < 0)
                {
                    inset[buffer[k - 2]] = false;
                    buffer[k - 2] = idx;
                    k--;
                }
                else if (ccw == 0)
                {
                    buffer[k - 2] = idx;
                    k--;
                }
                else
                {
                    break;
                }
            }
        }
        for (var idx = sorted.Length - 1; idx >= 0; idx--)
        {
            if (idx != 0 && inset[idx])
                continue;

            buffer[k++] = idx;
            inset[idx] = true;

            while (k >= 3)
            {
                var ccw = CCW(sorted[buffer[k - 3]], sorted[buffer[k - 2]], sorted[buffer[k - 1]]);

                if (ccw < 0)
                {
                    inset[buffer[k - 2]] = false;
                    buffer[k - 2] = idx;
                    k--;
                }
                else if (ccw == 0)
                {
                    buffer[k - 2] = idx;
                    k--;
                }
                else
                {
                    break;
                }
            }
        }

        return buffer.Take(k - 1).Select(idx => sorted[idx]).ToArray();
    }
    public static long CCW(Point l, Point m, Point r)
    {
        var rdy = r.Y - m.Y;
        var rdx = r.X - m.X;
        var ldy = m.Y - l.Y;
        var ldx = m.X - l.X;

        return rdy * ldx - rdx * ldy;
    }
}

#elif other2
var reader = new Reader();
var N = reader.NextInt();

var points = new Point[N];
for (int i = 0; i < N; i++)
    points[i] = new Point(reader.NextLong(), reader.NextLong());

var convexHull = ConvexHull.Scan(points.ToArray());

Console.WriteLine(convexHull.Length);

static class ConvexHull
{
    public static Point[] Scan(Point[] points)
    {
        var stack = new PointStack(points.Length);

        // Sort by coordinates first
        var sortedPoints = points.OrderBy(p => p, new PointComparer());

        // Assign basePoint.
        var basePoint = sortedPoints.First();
        stack.Push(basePoint);

        // Sort by angles from base point.
        sortedPoints = sortedPoints.Skip(1)
                                   .OrderBy(p => p, new PointCcwComparer(basePoint));

        foreach (var p in sortedPoints)
        {
            while (stack.Count > 1 && p.CounterClockWise(new Vector2(stack.Peek(1), stack.Peek())) <= 0)
                stack.Pop();

            stack.Push(p);
        }

        return stack.ToArray();
    }
}

struct Point
{
    public long X;

    public long Y;

    public Point(long x, long y)
    {
        this.X = x;
        this.Y = y;
    }

    public static Point operator+(Point a, Point b) =>
        new Point(a.X + b.X, a.Y + b.Y);

    public static Point operator-(Point a, Point b) => 
        new Point(a.X - b.X, a.Y - b.Y);

    // Check if the point is heading counter-clockwise from the vector.
    // -: Clockwise, 0: Perpendicular, +: Counter-Clockwise
    public long CounterClockWise(Vector2 vector) =>
        new Vector2(vector.Initial - this, vector.Terminal - vector.Initial).CrossProduct();

    public long LongDistance(Point b) => (this.X - b.X) * (this.X - b.X) + (this.Y - b.Y) * (this.Y - b.Y);

    public override string ToString() => $"{X} {Y}";
}

struct Vector2
{
    public Point Initial;

    public Point Terminal;

    public Vector2(Point initial, Point terminal)
    {
        this.Initial = initial;
        this.Terminal = terminal;
    }

    // Calculate the magnitude of the cross product.
    public long CrossProduct() =>
        this.Initial.X * this.Terminal.Y - this.Initial.Y * this.Terminal.X;

    public long LongMagnitude() =>
        this.Initial.LongDistance(this.Terminal);

    public override string ToString() => $"{Initial} {Terminal}";
}

class PointStack
{
    private Point[] container;

    private int top;

    public int Capacity { get; private set; }

    public int Count { get => this.top + 1; }

    public PointStack(int capacity)
    {
        this.Capacity = capacity;

        this.container = new Point[capacity];
        this.top = -1;
    }

    public void Push(Point point)
    {
        if (this.top + 1 >= this.Capacity)
            return;

        this.container[++this.top] = point;
    }

    public Point Pop()
    {
        if (this.top < 0)
            throw new Exception();

        return this.container[this.top--];
    }

    public Point Peek(int pos = 0)
    {
        if (this.top - pos < 0)
            throw new Exception();

        return this.container[this.top - pos];
    }

    public Point[] ToArray()
    {
        var copy = new Point[this.Count];
        for (int i = 0; i < this.Count; i++)
            copy[i] = this.container[i];

        return copy;
    }
}

// Comparing Points by it's coordinate. 
class PointComparer : IComparer<Point>
{
    public int Compare(Point a, Point b)
    {
        if (a.Y < b.Y)
            return -1;

        if (a.Y > b.Y)
            return 1;

        if (a.X < b.X)
            return -1;

        if (a.X > b.X)
            return 1;

        return 0;
    }
}

// Comparing Points by cross products from basePoint.
class PointCcwComparer : IComparer<Point>
{
    private Point basePoint;

    public PointCcwComparer(Point basePoint)
    {
        this.basePoint = basePoint;
    }

    public int Compare(Point a, Point b)
    {
        var ccw = b.CounterClockWise(new Vector2(basePoint, a));
        if (ccw > 0)
            return -1;

        if (ccw < 0)
            return 1;

        var distanceA = basePoint.LongDistance(a);
        var distanceB = basePoint.LongDistance(b);
        if (distanceA < distanceB)
            return -1;

        if (distanceA > distanceB)
            return 1;

        return 0;
    }
}

class Reader
{
    StreamReader reader;

    public Reader()
    {
        BufferedStream stream = new(Console.OpenStandardInput());
        reader = new(stream);
    }

    public int NextInt()
    {
        bool negative = false;
        bool reading = false;

        int value = 0;
        while (true)
        {
            int c = reader.Read();

            if (reading == false && c == '-')
            {
                negative = true;
                reading = true;
                continue;
            }

            if ('0' <= c && c <= '9')
            {
                value = value * 10 + (c - '0');
                reading = true;
                continue;
            }

            if (reading == true)
                break;
        }

        return negative ? -value : value;
    }

    public long NextLong()
    {
        bool negative = false;
        bool reading = false;

        long value = 0;
        while (true)
        {
            int c = reader.Read();

            if (reading == false && c == '-')
            {
                negative = true;
                reading = true;
                continue;
            }

            if ('0' <= c && c <= '9')
            {
                value = value * 10 + (c - '0');
                reading = true;
                continue;
            }

            if (reading == true)
                break;
        }

        return negative ? -value : value;
    }
}
#elif other3
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
using System.Collections;

class Programs
{
    static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), Encoding.Default);
    static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), Encoding.Default);
    class Vector: IComparer,IComparable
    {
        public long x, y;
       public  static Vector operator-(Vector a, Vector b)
        {
            Vector r=new Vector(b.x-a.x,b.y-a.y);
            return r;
        }
        public Vector (long x=0,long y=0)
        {
            this.x = x;
            this.y = y;
        }
    
        public int Compare(object x, object y)
        {
            Vector a = (Vector)x;
            Vector b = (Vector)y;

            long r = CCW(arr[0], a, b);
            if (r == 0)
            {
                if (Math.Abs(a.x+ a.y)  <Math.Abs(b.x + b.y) )
                {
                    return -1;
                }
            }
            else if (r > 0)
            {
                return -1;
            }
            return 1;
        }

        public int CompareTo(object obj)
        {
            Vector a = (Vector)this;
            Vector b = (Vector)obj;

            if (a.y == b.y)
            {
                return a.x.CompareTo(b.x);
            }
            return a.y.CompareTo(b.y);
        }
    }
    static long CCW(Vector a, Vector b, Vector c)
    {
        Vector AB = b - a;
        Vector BC = c - b;
        long r = a.x * b.y + b.x * c.y + c.x * a.y - (a.y * b.x + b.y * c.x + c.y * a.x);
        //long r = AB.x * BC.y - AB.y * BC.x;
        return r;
    }
    static Vector[] arr;
    static void Main(String[] args)
    {
        int n = int.Parse(sr.ReadLine());
        arr = new Vector[n];
        for (int i = 0; i < n; i++)
        {
        string[] str = sr.ReadLine().Split();
            int x = int.Parse(str[0]);
            int y = int.Parse(str[1]);
            arr[i] = new Vector(x, y);
        }
        Array.Sort(arr);
        Array.Sort(arr,1,n-1,new Vector());
        Stack<int> st = new Stack<int>();
        st.Push(0);
        st.Push(1);
        int next = 2;
        int answer = 0;
        while (next < n)
        {
            while (st.Count >= 2)
            {
                int second = st.Pop();
                int first   = st.Peek();
                if (CCW(arr[first], arr[second], arr[next])>0)//반시계 방향
                {
                    //가능하다 
                    //fist제거후 second-next형태로
                    st.Push(second);
                    break;
                }
            }
            st.Push(next);
            next++;
         }
        answer = st.Count;
        sw.Write(answer);
        sw.Dispose();
    }
    
}

#endif
}
