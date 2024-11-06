using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 9
이름 : 배성훈
내용 : 맹독 방벽
    문제번호 : 7420번

    기하학, 볼록 껍질 문제다
    아이디어는 다음과 같다
    컨벡스 헐 알고리즘으로 외각을 둘러싸는 다각형을 구한다
    그리고, 해당 다각형에서 거리 l인 외곽 좌표들을 모아놓은 도형이 정답이된다
    컨벡스 헐로 구한 볼록 다각형의 꼭짓점이(특이점) 아닌 선분에 해당하는 점들은
    따로 변수가 없다 해당 점들에 대해 거리 l인 좌표들을 모으면 볼록 다각형의 둘레의 길이와 같기 때문이다
    주의할 것은 볼록 다각형의 선분과 선분이 교차하는 꼭짓점인(특이점) 경우다
    해당 구간에서 거리 l인 점들을 모으면 호가 된다

    해당 호의 길이는 세 점을 이용해 코사인 법칙이나 사인 법칙등으로 각각의 호의 길이를 구할 수 있으나,
    도형이라는 성질을 이용하면 총합은 쉽게 구할 수 있다

    도형의 내부 각의 합은 삼각형으로 쪼개서 보면 (꼭짓점의 개수 - 2) * pi이고, 외부의 각은
    각 꼭짓점에 2pi의 각도를 갖지만 여기서 쓸 수 있는 각은 pi이다 (꼭짓점의 개수) * pi - 내부 각도의 총합
    이고 이를 계산하면 2 * pi만 남는다

    이렇게 길이들을 더해서 제출하니 이상없이 통과했다
*/

namespace BaekJoon._49
{
    internal class _49_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr;
            int n, l;

            Point[] pos;
            int[] arr;
            int idx;
            Solve();

            void Solve()
            {

                Input();

                ConvexHull();

                double ret = GetDis();

                Console.WriteLine($"{ret:0}");
            }

            double GetDis()
            {

                double ret = Math.Sqrt(Point.Dis(pos[arr[0]], pos[arr[idx - 1]])); 

                
                for (int i = 0; i < idx - 1; i++)
                {

                    ret += Math.Sqrt(Point.Dis(pos[arr[i]], pos[arr[i + 1]]));
                }

                ret += Math.PI * 2 * l;
                return ret;
            }

            void ConvexHull()
            {

                Array.Sort(pos, 1, n - 1);

                idx = 0;
                for (int i = 0; i < n; i++)
                {

                    while(idx >= 2 && Point.CCW(pos[arr[idx - 2]], pos[arr[idx - 1]], pos[i]) <= 0)
                    {

                        idx--;
                    }

                    arr[idx++] = i;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                n = ReadInt();
                l = ReadInt();

                pos = new Point[n];
                arr = new int[n];

                int minY = 10_000;
                int minX = 10_000;
                int minIdx = 0;
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

                SetMin(minIdx);

                sr.Close();
            }

            void SetMin(int _idx)
            {

                Point temp = pos[0];
                pos[0] = pos[_idx];
                pos[_idx] = temp;

                Point.SetFixed(pos[0]);
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == '\r') c = sr.Read();
                if (c == '\n' || c == ' ' || c == -1) return 0;
                bool plus = c != '-';

                int ret;
                if (plus) ret = c - '0';
                else ret = 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }

        struct Point : IComparable<Point>
        {

            private static Point fix;

            private int x;
            private int y;

            public static void SetFixed(Point _a)
            {

                fix = _a;
            }

            public void Set(int _x, int _y)
            {

                x = _x;
                y = _y;
            }

            public static int Dis(Point _a, Point _b)
            {

                int x = _a.x - _b.x;
                x *= x;

                int y = _a.y - _b.y;
                y *= y;

                return x + y;
            }

            public static int CCW(Point _a, Point _b, Point _c)
            {

                long ccw = 1L * _a.x * _b.y;
                ccw += 1L * _b.x * _c.y;
                ccw += 1L * _c.x * _a.y;

                ccw -= 1L * _b.x * _a.y;
                ccw -= 1L * _c.x * _b.y;
                ccw -= 1L * _a.x * _c.y;

                if (ccw > 0) return 1;
                if (ccw < 0) return -1;
                return 0;
            }

            public int CompareTo(Point _other)
            {

                int ccw = CCW(this, _other, fix);
                if (ccw == 0) return Dis(this, fix).CompareTo(Dis(_other, fix));
                return -ccw;
            }
        }
    }

#if other
using System.IO;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;

class Programs
{
    static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), Encoding.Default);
    static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), Encoding.Default);
    class Vector : IComparer, IComparable
    {
        public long x, y;
        public static Vector operator -(Vector a, Vector b)
        {
            Vector r = new Vector(b.x - a.x, b.y - a.y);
            return r;
        }
        public Vector(long x = 0, long y = 0)
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
                if (Math.Abs(a.x + a.y) < Math.Abs(b.x + b.y))
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
        long r = AB.x * BC.y - AB.y * BC.x;
        return r;
    }
    static long CCW(Vector a, Vector b)
    {
        long r = a.x * b.y - a.y * b.x;
        return r;
    }
    static Vector[] arr = new Vector[1000];
    static Vector[] cv = new Vector[1000];
    static void Main(String[] args)
    {
        string[] str = sr.ReadLine().Split();
        int n = int.Parse(str[0]);
        int l = int.Parse(str[1]);
        long x, y;
        for (int i = 0; i < n; i++)
        {
            str = sr.ReadLine().Split();
             x = int.Parse(str[0]);
            y = int.Parse(str[1]);
            arr[i] = new Vector(x, y);
        }
        Array.Sort(arr,0,n);
        Array.Sort(arr, 1, n - 1, new Vector());
        Stack<int> st = new Stack<int>();
        st.Push(0);
        st.Push(1);
        for (int i = 2; i < n; i++)
        {
            while (st.Count >= 2)
            {
                int second = st.Pop();
                int first = st.Peek();
                if (CCW(arr[first], arr[second], arr[i]) > 0)
                {
                    //반시계인 경우만
                    st.Push(second);
                    break;
                }
            }
            st.Push(i);
        }
        double answer = 0;
        int size = st.Count;
        for (int i = st.Count - 1; i >= 0; i--)
        {
            cv[i] = arr[st.Pop()];
        }
        double  distance;
        for (int i = 1; i < size; i++)
        {
            x = cv[i].x - cv[i - 1].x;
            y = cv[i].y - cv[i - 1].y;
            distance =Math.Sqrt( x * x + y * y);
            answer += distance;
        }
        x = cv[0].x - cv[size - 1].x;
        y = cv[0].y - cv[size - 1].y;
        distance = Math.Sqrt(x * x + y * y);
        answer += distance;
        //컨벡스헐의 길이+L이 반지름인 원의 둘레
        //원의 둘레는 2*파이*반지름
        answer += (3.141592 * 2 * l);
        sw.Write($"{answer:0}");
        sw.Dispose();
    }

}

#elif other2
var reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));


var NL = reader.ReadLine().Split();
var N = int.Parse(NL[0]);
var L = int.Parse(NL[1]);

var points = new Point[N];
for (int i = 0; i < N; i++)
{
    var line = reader.ReadLine().Split();

    long x = long.Parse(line[0]);
    long y = long.Parse(line[1]);

    points[i] = new Point(x, y);
}

var convexHull = ConvexHull.Scan(points);

double dist = 0;
for (int i = 0; i < convexHull.Length; i++)
    dist += convexHull[i].Distance(convexHull[(i + 1) % convexHull.Length]);
dist += Math.PI * 2 * L;

writer.WriteLine(Math.Round(dist));

reader.Close();
writer.Close();

static class ConvexHull
{
    public static Point[] Scan(Point[] points)
    {
        var stack = new PointStack(points.Length);

        // Sort by coordinates first
        var sortedPoints = points.OrderBy(p => p, new PointComparer()).ToArray();

        // Assign basePoint.
        var basePoint = sortedPoints.First();
        stack.Push(basePoint);

        // Sort by angles from base point.
        sortedPoints = sortedPoints.Skip(1)
                                   .OrderBy(p => p, new PointCcwComparer(basePoint))
                                   .ToArray();

        foreach (var p in sortedPoints)
        {
            while (stack.Count > 1 && 
                   p.CounterClockWise(new Vector2(stack.Peek(1), stack.Peek())) <= 0)
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

    public double Distance(Point b) => Math.Sqrt(this.LongDistance(b));

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

    // Check if the vector is counter-clockwise from operand vecvtor.
    public long CounterClockWise(Vector2 operand)
    {
        var offset = operand.Initial - this.Initial;
        var moved = new Vector2(this.Initial + offset, this.Terminal + offset);

        return moved.Terminal.CounterClockWise(operand);
    }

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
#endif
}
