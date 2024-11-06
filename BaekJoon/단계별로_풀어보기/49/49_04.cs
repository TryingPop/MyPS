using System;
using System.Collections.Generic;
using System.Drawing;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 11
이름 : 배성훈
내용 : 점 분리
    문제번호 : 3878번

    기하학, 볼록 껍질, 선분 교차 판정, 볼록 다각형 내부의 점 판정 문제다
    가지치기? 조건을 나눠서 풀었다

    아이디어는 다음과 같다
    우선 색깔별로 점들이 이루는 영역을 나눠봤다
    영역은 점, 선, 다각형이 되었다
    직선으로 나눌 구간을 나눠야 하므로 3개 이상의 점들은 볼록 껍질 영역(다각형)으로 확인하는 것과 같다
    또한 영역이 분리되어 있는지 확인하는 것과 같다

    1. 점과 점
        이 경우 점의 위치가 다르므로 분리되어져 있다

    2. 점과 선분
        이 경우 선분 안에 점이 있으면 못나누고 이외 경우는 항상 분리되어져 있다

    3. 선분과 선분
        이 경우 ccw를 이용한 선분 교차 판정으로 분리되어있는지 확인했다

    4. 점과 다각형
        컨벡스 헐 알고리즘을 보면 다각형은 스텍의 점들을 순차적으로 이어서 반직선을 만든 것이고
        내부에 위치한 점은 해당 반직선과 점을 ccw하면 모두 반시계 방향으로 존재함을 알 수 있다
        볼록껍질의 모든 반직선과 해당 점을 ccw돌려 시계방향이 하나라도 존재하면 외부, 아니면 내부로 판정할 수 있다
        이렇게 영역이 구분되어 있는지 확인했다

    5. 다각형과 다각형
        다각형 A, B라하면 A의 각 꼭짓점에 대해 B에 포함되는지 확인했다
        포함되면 영역이 겹친다 영역이 포함 안된다면
        이제 B의 각 꼭짓점에 대해 A에 포함되는지 확인했다
        포함되면 영역이 겹치고 이래도 영역이 포함안되면 이제 영역이 분리되어 있다고 말할 수 있다
        (A가 B를 포함하면 각 A의 꼭짓점은 B의 밖에 있어 양쪽 확인이 필수다!)

    6. 선분과 다각형
        이는 다각형의 각 선분에 대해 교차 판정을 했다
        (선분이 다각형 외부에 있는지만, 다각형의 구간을 나누는 경우가 있기에 4번과 같은 판정으로 확인할 수 없다!)

    이렇게 구간을 나눠 푸니, 300 줄 이상 코드가 나오고(코드길이 7667b), 시간은 80ms로 통과되었다
    코드가 길어지니 2 ~ 3 번 검토해도 긴장하게 된다;
*/

namespace BaekJoon._49
{
    internal class _49_04
    {

        static void Main4(string[] args)
        {

            string YES = "YES\n";
            string NO = "NO\n";

            StreamReader sr;
            StreamWriter sw;

            Point[] pos1;
            Point[] pos2;

            int len1;
            int len2;

            int[] arr1;
            int[] arr2;

            int idx1;
            int idx2;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    bool ret = GetRet();

                    sw.Write(ret ? YES : NO);
                }

                sr.Close();
                sw.Close();
            }

            bool GetRet()
            {

                if (len1 == 1)
                {

                    if (len2 == 1) return true;
                    else if (len2 == 2) return NotCrossLP(pos1[0], pos2[0], pos2[1]);
                    else
                    {

                        idx2 = ConvexHull(pos2, len2, arr2);

                        return NotCrossPP(pos2, arr2, idx2, pos1[0]);
                    }
                }
                else if (len1 == 2)
                {

                    idx2 = ConvexHull(pos2, len2, arr2);

                    bool chk = NotCrossLL(pos1[0], pos1[1], pos2[arr2[idx2 - 1]], pos2[arr2[0]]);
                    if (!chk) return false;

                    for (int i = 1; i < idx2; i++)
                    {

                        chk = NotCrossLL(pos1[0], pos1[1], pos2[arr2[i - 1]], pos2[arr2[i]]);
                        if (!chk) return false;
                    }

                    return true;
                }

                idx1 = ConvexHull(pos1, len1, arr1);
                idx2 = ConvexHull(pos2, len2, arr2);

                bool ret;
                for (int i = 0; i < idx1; i++)
                {

                    ret = NotCrossPP(pos2, arr2, idx2, pos1[arr1[i]]);
                    if (!ret) return false;
                }

                for (int i = 0; i < idx2; i++)
                {

                    ret = NotCrossPP(pos1, arr1, idx1, pos2[arr2[i]]);
                    if (!ret) return false;
                }

                return true;
            }

            bool ChkContains(int _l, int _r, int _x)
            {

                if (_l <= _x && _x <= _r) return true;
                if (_r <= _x && _x <= _l) return true;

                return false;
            }

            bool NotCrossLP(Point _p, Point _l, Point _r)
            {

                int ccw = Point.CCW(_p, _l, _r);
                if (ccw != 0) return true;

                if (_l.X != _r.X) return !ChkContains(_l.X, _r.X, _p.X);
                else return !ChkContains(_l.Y, _r.Y, _p.Y);
            }

            bool NotCrossLL(Point _l1, Point _r1, Point _l2, Point _r2)
            {

                int ret1 = Point.CCW(_l1, _r1, _l2) * Point.CCW(_l1, _r1, _r2);
                int ret2 = Point.CCW(_l2, _r2, _l1) * Point.CCW(_l2, _r2, _r1);

                if (ret1 == 0 && ret2 == 0)
                {

                    int min1, min2, max1, max2;
                    if (_l1.X != _r1.X)
                    {

                        min1 = _l1.X < _r1.X ? _l1.X : _r1.X;
                        max1 = _l1.X < _r1.X ? _r1.X : _l1.X;

                        min2 = _l2.X < _r2.X ? _l2.X : _r2.X;
                        max2 = _l2.X < _r2.X ? _r2.X : _l2.X;

                    }
                    else
                    {

                        min1 = _l1.Y < _r1.Y ? _l1.Y : _r1.Y;
                        max1 = _l1.Y < _r1.Y ? _r1.Y : _l1.Y;

                        min2 = _l2.Y < _r2.Y ? _l2.Y : _r2.Y;
                        max2 = _l2.Y < _r2.Y ? _r2.Y : _l2.Y;
                    }

                    return max2 < min1 || max1 < min2;
                }
                else return ret1 > 0 || ret2 > 0;
            }

            bool NotCrossPP(Point[] _polygon, int[] _arr, int _idx, Point _p)
            {

                int ccw = Point.CCW(_p, _polygon[_arr[_idx - 1]], _polygon[_arr[0]]);
                if (ccw < 0) return true;
                for (int i = 1; i < _idx; i++)
                {

                    ccw = Point.CCW(_p, _polygon[_arr[i - 1]], _polygon[_arr[i]]);

                    if (ccw < 0) return true;
                }

                return false;
            }

            void Input()
            {

                len1 = ReadInt();
                len2 = ReadInt();

                for (int i = 0; i < len1; i++)
                {

                    pos1[i].Set(ReadInt(), ReadInt());
                }

                for (int i = 0; i < len2; i++)
                {

                    pos2[i].Set(ReadInt(), ReadInt());
                }

                if (len2 < len1) Swap();
            }

            void SetMin(Point[] _pos, int _len)
            {

                int minY = 10_000;
                int minX = 10_000;
                int minIdx = 0;

                for (int i = 0; i < _len; i++)
                {

                    if (_pos[i].Y < minY)
                    {

                        minY = _pos[i].Y;
                        minX = _pos[i].X;
                        minIdx = i;
                    }
                    else if (_pos[i].Y == minY && _pos[i].X < minX)
                    {

                        minX = _pos[i].X;
                        minIdx = i;
                    }
                }

                Point temp = _pos[0];
                _pos[0] = _pos[minIdx];
                _pos[minIdx] = temp;

                Point.SetFix(_pos[0]);
            }

            void Swap()
            {

                int tempLen = len1;
                len1 = len2;
                len2 = tempLen;

                Point[] tempPos = pos1;
                pos1 = pos2;
                pos2 = tempPos;
            }

            int ConvexHull(Point[] _pos, int _len, int[] _arr)
            {

                SetMin(_pos, _len);
                Array.Sort(_pos, 1, _len - 1);
                int idx = 0;
                for (int i = 0; i < _len; i++)
                {

                    while (idx >= 2 && Point.CCW(_pos[_arr[idx - 2]], _pos[_arr[idx - 1]], _pos[i]) <= 0)
                    {

                        idx--;
                    }

                    _arr[idx++] = i;
                }

                return idx;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                pos1 = new Point[100];
                pos2 = new Point[100];

                arr1 = new int[100];
                arr2 = new int[100];
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

        struct Point : IComparable<Point>
        {

            private int x;
            private int y;

            private static Point fix;

            public int X => x;
            public int Y => y;

            public static void SetFix(Point _fix)
            {

                fix = _fix;
            }

            public static int CCW(Point _a, Point _b, Point _c)
            {

                int ccw = _a.x * _b.y;
                ccw += _b.x * _c.y;
                ccw += _c.x * _a.y;

                ccw -= _b.x * _a.y;
                ccw -= _c.x * _b.y;
                ccw -= _a.x * _c.y;

                if (ccw > 0) return 1;
                if (ccw < 0) return -1;
                return 0;
            }

            public static int Dis(Point _a, Point _b)
            {

                int x = _a.x - _b.x;
                x *= x;

                int y = _a.y - _b.y;
                y *= y;

                return x + y;
            }

            public int CompareTo(Point _other)
            {

                int ccw = CCW(this, _other, fix);
                if (ccw == 0) return Dis(this, fix).CompareTo(Dis(_other, fix));
                return -ccw;
            }

            public void Set(int _x, int _y)
            {

                x = _x;
                y = _y;
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
    static int n, m;
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

            if (a.x == b.x)
            {
                return a.y.CompareTo(b.y);
            }
            return a.x.CompareTo(b.x);
        }
    }
    static long CCW(Vector a, Vector b, Vector c)
    {
        Vector AB = b - a;
        Vector BC = c - b;
        long r = AB.x * BC.y - AB.y * BC.x;
        if (r < 0)
        {
            return -1;
        }
        else if (r > 0)
        {
            return 1;
        }
        return 0;
    }
    static Vector[] arr = new Vector[100];
    static List<Vector> black = new List<Vector>(), white = new List<Vector>();

    static void Convex(ref Vector[] vec, ref List<Vector> convex, int size)
    {
        convex.Clear();
        if (size == 1)
        {
            convex.Add(vec[0]);
            return;
        }
        Array.Sort(vec, 0, size);
        Array.Sort(vec, 1, size - 1, new Vector());
        convex.Add(vec[0]);
        convex.Add(vec[1]);
        for (int i = 2; i < size; i++)
        {
            while (convex.Count >= 2)
            {
                Vector second = convex[convex.Count - 1];
                convex.RemoveAt(convex.Count - 1);
                Vector first = convex[convex.Count - 1];
                if (CCW(first, second, vec[i]) > 0)
                {
                    convex.Add(second);
                    break;
                }
            }
            convex.Add(vec[i]);
        }
    }
    static int CalPos(Vector a, Vector b)
    {
        if (a.x == b.x && a.y == b.y)
        {
            return 0;
        }
        if (a.x < b.x)
        {
            return -1;
        }

        if (a.x.Equals(b.x) && a.y < b.y)
        {
            return -1;
        }
        return 1;
    }
    static void Swap(ref Vector a, ref Vector b)
    {
        Vector p = a;
        a = b;
        b = p;
    }
    private static void SwapPos(ref Vector a, ref Vector b)
    {
        if (a.x.Equals(b.x))
        {
            if (a.y > b.y)
            {
                Swap(ref a, ref b);
            }
        }
        else if (a.x > b.x)
        {
            Swap(ref a, ref b);
        }
    }
    static bool IsCross()
    {
        //각 색점 하나와 컨벡스헐의 모든 선분을 ccw로 구성했을 때 같은 방향인지 확인
        if (IsInside(ref white, black[0]) ||  IsInside(ref black, white[0]))
        {
            return true;//하나라도 교차했다면 불가능
        }
        for (int i = 0; i < black.Count; i++)
        {
            for (int j = 0; j < white.Count; j++)
            {
                if (Cross(black[i], black[(i + 1) % black.Count], white[j], white[(j + 1) % white.Count]))
                {
                    return true;
                }
            }
        }
        return false;
    }
    static bool IsInside(ref List<Vector> convex, Vector target)
    {
        if (convex.Count <= 2)
        {
            return false;
        }
        long dir = CCW(convex[0], convex[1], target);
        for (int i = 0; i < convex.Count; i++)
        {
            long result = CCW(convex[i], convex[(i + 1) % convex.Count], target);
            if (dir != result)
            {
                // 방향이 단 하나라도 다르다면 점이 내부에 포함된 것이아니다.
                return false;
            }
        }
        return true;
    }
    static bool Cross(Vector A, Vector B, Vector C, Vector D)
    {
        long aa = CCW(A, B, C) * CCW(A, B, D);
        long bb = CCW(C, D, A) * CCW(C, D, B);
        //교차하거나 같은 선분에 있음
        if (aa == 0 && bb == 0)
        {
            SwapPos(ref A, ref B);
            SwapPos(ref C, ref D);
            if (CalPos(C, B) <= 0 && CalPos(A, D) <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return aa <= 0 && bb <= 0;
    }
    static void Main(String[] args)
    {
        int t, x, y;
        t = int.Parse(sr.ReadLine());
        for (int z = 0; z < t; z++)
        {
            string[] str = sr.ReadLine().Split();
            n = int.Parse(str[0]);
            m = int.Parse(str[1]);
            Array.Clear(arr, 0, 100);
            //x값이 가장 작은 컨벡스헐을 따로 찾아주어야함
            for (int i = 0; i < n; i++)
            {
                str = sr.ReadLine().Split();
                x = int.Parse(str[0]);
                y = int.Parse(str[1]);
                arr[i]  = new Vector(x, y);
            }
            Convex(ref arr, ref black, n);
            Array.Clear(arr, 0, 100);
            for (int i = 0; i < m; i++)
            {
                str = sr.ReadLine().Split();
                x = int.Parse(str[0]);
                y = int.Parse(str[1]);
                arr[i]  = new Vector(x, y);
            }
            Convex(ref arr, ref white, m);

            if (IsCross())
            {
                sw.WriteLine("NO");
            }
            else
            {
                sw.WriteLine("YES");
            }
        }
        sw.Dispose();
    }

}

#elif other2
var reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

var T = int.Parse(reader.ReadLine());
while (T-- > 0)
{
    var nm = reader.ReadLine().Split();
    var n = int.Parse(nm[0]);
    var m = int.Parse(nm[1]);

    var blackPoints = new Point[n];
    for (int i = 0; i < n; i++)
    {
        var line = reader.ReadLine().Split();

        long x = long.Parse(line[0]);
        long y = long.Parse(line[1]);

        blackPoints[i] = new Point(x, y);
    }

    var whitePoints = new Point[m];
    for (int i = 0; i < m; i++)
    {
        var line = reader.ReadLine().Split();

        long x = long.Parse(line[0]);
        long y = long.Parse(line[1]);

        whitePoints[i] = new Point(x, y);
    }

    var chBlack = ConvexHull.Scan(blackPoints);
    var chWhite = ConvexHull.Scan(whitePoints);

    bool canBeSeparated = true;
    foreach (var wP in whitePoints)
    {
        if (wP.IsInConvexHull(chBlack))
        {
            canBeSeparated = false;
            break;
        }
    }

    if (canBeSeparated)
    {
        foreach (var bP in blackPoints)
        {
            if (bP.IsInConvexHull(chWhite))
            {
                canBeSeparated = false;
                break;
            }
        }
    }

    for (int i = 0; i < chBlack.Length; i++)
    {
        if (!canBeSeparated)
            break;

        var vB = new Vector2(chBlack[i], chBlack[(i + 1) % chBlack.Length]);
        for (int j = 0; j < chWhite.Length; j++)
        {
            var vW = new Vector2(chWhite[j], chWhite[(j + 1) % chWhite.Length]);
            
            if (vB.IsIntersect(vW))
            {
                canBeSeparated = false;
                break;
            }
        }
    }

    writer.WriteLine(canBeSeparated ? "YES" : "NO");
}

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

    public static bool operator ==(Point a, Point b) =>
        a.X == b.X && a.Y == b.Y;

    public static bool operator !=(Point a, Point b) =>
        a.X != b.X || a.Y != b.Y;

    public static bool operator >(Point a, Point b) =>
        a.X > b.X || (!(a.X > b.X) && a.Y > b.Y);
    
    public static bool operator <(Point a, Point b) =>
        a.X < b.X || (!(a.X < b.X) && a.Y < b.Y);

    public static bool operator >=(Point a, Point b) =>
        a > b || a == b;
    
    public static bool operator <=(Point a, Point b) =>
        a < b || a == b;
    
    // point x this
    public long CrossProduct(Point point) => 
        point.X * this.Y - point.Y * this.X;

    // Check if the point is heading counter-clockwise from the vector.
    // -: Clockwise, 0: Perpendicular, +: Counter-Clockwise
    public long CounterClockWise(Vector2 vector) =>
        new Vector2(vector.Initial - this, vector.Terminal - vector.Initial).CrossProduct();

    public int CounterClockWiseSigned(Vector2 vector)
    {
        var ccw = this.CounterClockWise(vector);

        return ccw == 0 ? 0 : ccw > 0 ? 1 : -1;
    }

    public long LongDistance(Point b) => (this.X - b.X) * (this.X - b.X) + (this.Y - b.Y) * (this.Y - b.Y);

    public bool IsInConvexHull(Point[] convexHull)
    {
        if (convexHull.Length < 3)
            return false;

        var basePoint = convexHull.First();

        var leftPoint = convexHull.Last() - basePoint;
        var rightPoint = convexHull[1] - basePoint;
        var targetPoint = this - basePoint;

        // targetPoint must be located in right side of leftPoint also in left side of the rightPoint.
        if (targetPoint.CrossProduct(leftPoint) > 0)
            return false;

        if (targetPoint.CrossProduct(rightPoint) < 0)
            return false;

        // Find the area of the point.
        int left = 1, right = convexHull.Length - 1;
        while (left + 1 < right)
        {
            int mid = (left + right) / 2;

            var midPoint = convexHull[mid] - basePoint;
            if (targetPoint.CrossProduct(midPoint) > 0)
                left = mid;
            else
                right = mid;
        }

        var p1 = this - convexHull[left];
        var p2 = convexHull[left + 1] - this;

        return p2.CrossProduct(p1) < 0;
    }
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

    public bool IsIntersect(Vector2 target)
    {
        int ccwOfOrigin = this.Initial.CounterClockWiseSigned(target) * this.Terminal.CounterClockWiseSigned(target);
        int ccwOfTarget = target.Initial.CounterClockWiseSigned(this) * target.Terminal.CounterClockWiseSigned(this);

        if (ccwOfOrigin == 0 && ccwOfTarget == 0)
        {
            Point oI = this.Initial, oT = this.Terminal;
            Point tI = target.Initial, tT = target.Terminal;

            if (oI > oT)
                (oI, oT) = (oT, oI);
            
            if (tI > tT)
                (tI, tT) = (tT, tI);

            return tI <= oT && oI <= tT;
        }

        return ccwOfOrigin <= 0 && ccwOfTarget <= 0;
    }
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
#elif other3
// #include <iostream>
// #include <vector>
// #include <algorithm>
// #define FAST_IO do{std::ios::sync_with_stdio(false);std::cin.tie(0);std::cout.tie(0);}while(false)
using namespace std;
using pii = pair<int, int>;

const char* yes = "YES\n";
const char* no = "NO\n";
vector<pii> GetConvexHull(vector<pii>&points);
bool IsMeeting(const vector<pii>&convexA, const vector<pii>&convexB);
long long ccw(const pii & a, const pii & b, const pii & c);
long long Distance(const pii & a, const pii & b);
bool InsideOf(const pii & p, const vector<pii>&convex);

int main()
{
	FAST_IO;
	int T;
	cin >> T;
	while (T--)
	{
		int n, m;
		cin >> n >> m;
		vector<pii> w(n), b(m);
		for (int i = 0; i < n; ++i)
			cin >> w[i].first >> w[i].second;
		for (int i = 0; i < m; ++i)
			cin >> b[i].first >> b[i].second;

		const vector<pii>& ch1 = GetConvexHull(w);
		const vector<pii>& ch2 = GetConvexHull(b);

		cout << (IsMeeting(ch1, ch2) ? no : yes);
	}

}

bool IsMeeting(const vector<pii>& convexA, const vector<pii>& convexB)
{
	if (convexA.empty() || convexB.empty()) return false;
	const int aSize = convexA.size(), bSize = convexB.size();
	if (aSize == 1)
		return InsideOf(convexA[0], convexB);
	if (bSize == 1)
		return InsideOf(convexB[0], convexA);

	if (InsideOf(convexA[0], convexB) || InsideOf(convexB[0], convexA))
		return true;

	for (int i = 0; i < aSize; ++i)
	{
		for (int j = 0; j < bSize; ++j)
		{
			pii a = convexA[i];
			pii b = convexA[(i + 1) % aSize];
			pii c = convexB[j];
			pii d = convexB[(j + 1) % bSize];
			long long val1 = ccw(a, b, c) * ccw(a, b, d);
			long long val2 = ccw(c, d, a) * ccw(c, d, b);

			if (val1 == 0 && val2 == 0)
			{
				// ccw판정법의 유일한 반례 : 네 점이 일직선 위에 있을 때.
				if (a > b)
					swap(a, b);
				if (c > d)
					swap(c, d);

				bool intersect = b >= c || d <= a;
				if (intersect)
					return true;
			}
			else if (val1 <= 0ll && val2 <= 0ll)
				return true;
		}
	}

	return false;
}

vector<pii> GetConvexHull(vector<pii>& points)
{
	if (points.empty()) return vector<pii>();
	vector<pii> ret;
	pii start = points[0];
	for (int i = 1; i < points.size(); ++i)
		if (points[i].second < start.second || (points[i].second == start.second && start.first > points[i].first))
			start = points[i];

	sort(points.begin(), points.end(), [&](const pii& p, const pii& q)->bool
		{
			return ccw(start, p, q) > 0 || (ccw(start, p, q) == 0 && Distance(start, p) < Distance(start, q));
		});

	for (int i = 0; i < points.size(); ++i)
	{
		while (ret.size() >= 2 && ccw(ret[ret.size() - 2], ret[ret.size() - 1], points[i]) <= 0)
			ret.pop_back();
		ret.push_back(points[i]);
	}

	return ret;
}

long long ccw(const pii& a, const pii& b, const pii& c)
{
	long long val = (long long)(b.first - a.first) * (c.second - a.second) - (long long)(b.second - a.second) * (c.first - a.first);
	return val;
}

long long Distance(const pii& a, const pii& b)
{
	return (long long)(a.first - b.first) * (a.first - b.first) + (long long)(a.second - b.second) * (a.second - b.second);
}

bool InsideOf(const pii& p, const vector<pii>& convex)
{
	const int vertexSize = convex.size();
	if (vertexSize == 0) return true;
	if (vertexSize == 1) return p == convex[0];

	bool ret = false;
	for (int i = 0; i < vertexSize; ++i)
	{
		int j = (i + 1) % vertexSize;
		if (j == 0 && i == 1) break;
		if (((p.second < convex[i].second) != (p.second < convex[j].second)))
		{
			double dist = (double)(p.second - convex[i].second) * (convex[j].first - convex[i].first) / (convex[j].second - convex[i].second) + convex[i].first - p.first;
			if (dist > 0.0)
				ret = !ret;
			else if (dist == 0.0)
				return true;
		}
	}
	return ret;
}
#elif other4
import java.io.*;
import java.util.*;

public class Main {
    static Reader r = new Reader();
    static StringBuilder sb = new StringBuilder();

    static class Point{
        int x, y;
        public Point(int a, int b){
            x = a; y = b;
        }
        @Override
        public String toString(){
            return this.x + " " + this.y;
        }
    }
    static int ccw(Point p1, Point p2, Point p3){
        int det = (p2.x-p1.x)*(p3.y-p1.y) - (p2.y-p1.y)*(p3.x-p1.x);
        return Integer.compare(det, 0);
    }
    static int dist(Point p1, Point p2){
        return (p1.x-p2.x)*(p1.x-p2.x)+(p1.y-p2.y)*(p1.y-p2.y);
    }
    static boolean edge_intersect(Point u1, Point u2, Point v1, Point v2){
        // detect intersection of edge u1-u2 and v1-v2
        int[] ccw = {ccw(u1,u2,v1),ccw(u1,u2,v2),ccw(v1,v2,u1),ccw(v1,v2,u2)};
        int sep_by_u = ccw[0]*ccw[1], sep_by_v = ccw[2]*ccw[3];
        if(sep_by_u==0 && sep_by_v==0){
            if(Math.max(u1.x,u2.x)<Math.min(v1.x,v2.x)) return false;
            if(Math.max(v1.x,v2.x)<Math.min(u1.x,u2.x)) return false;
            if(Math.max(u1.y,u2.y)<Math.min(v1.y,v2.y)) return false;
            return Math.max(v1.y, v2.y) >= Math.min(u1.y, u2.y);
        }
        return sep_by_u<=0 && sep_by_v<=0;
    }

    static Point[] convexHull(Point[] pt){
        // find the convex hull of pt (in ccw orientation)
        int n = pt.length;
        Point origin = pt[0];
        for(Point a: pt)
            if(a.x<origin.x || a.x==origin.x && a.y<origin.y)
                origin = a;

        Point p1 = origin, p2, p3;
        ArrayList<Point> li = new ArrayList<>();
        do{
            li.add(p1);
            p2 = pt[0];
            for(int i=1;i<n;i++){
                p3 = pt[i];
                int ccw = ccw(p1,p2,p3);
                if(ccw==0 && dist(p1,p2)<dist(p1,p3)) p2 = p3;
                else if(ccw<0) p2 = p3;
            }
            p1 = p2;
        }while(!origin.equals(p1));

        return li.toArray(Point[]::new);
    }

    static boolean convexHull_intersect(Point[] cvx1, Point[] cvx2){
        // detect intersection of two convex hulls
        int n = cvx1.length, m = cvx2.length;
        for(int i=0;i<n;i++){
            for(int j=0;j<m;j++){
                if(edge_intersect(cvx1[i],cvx1[(i+1)%n],cvx2[j],cvx2[(j+1)%m]))
                    return true;
            }
        }
        return false;
    }

    static boolean inside(Point p, Point[] cvx){
        // return true if p is inside the convex hull cvx
        // assume that p is not 'on' cvx
        if(cvx.length<=2) return false;
        for(int i=0;i<cvx.length;i++){
            if(ccw(p,cvx[i],cvx[(i+1)%cvx.length])<=0)
                return false;
        }
        return true;
    }
    static void solve3878() throws Exception{
        int b = r.readInt(), w = r.readInt();
        Point[] black = new Point[b], white = new Point[w];
        for(int i=0;i<b;i++)
            black[i] = new Point(r.readInt(), r.readInt());
        for(int j=0;j<w;j++)
            white[j] = new Point(r.readInt(), r.readInt());
        Point[] cv_black = convexHull(black);
        Point[] cv_white = convexHull(white);

        if(convexHull_intersect(cv_black,cv_white))
            sb.append("NO\n");
        else if(inside(cv_black[0],cv_white) || inside(cv_white[0],cv_black))
            sb.append("NO\n");
        else sb.append("YES\n");
    }

    public static void main(String[] args) throws Exception{
        int t = r.readInt();
        while(t-->0)
            solve3878();
        System.out.println(sb);
    }
}

class Reader {
    final private int BUFFER_SIZE = 1 << 16;
    private DataInputStream din;
    private byte[] buffer;
    private int bufferPointer, bytesRead;

    public Reader() {
        din = new DataInputStream(System.in);
        buffer = new byte[BUFFER_SIZE];
        bufferPointer = bytesRead = 0;
    }
    public int readInt() throws IOException {
        int ret = 0;
        byte c = read();
        while(c <= ' '){ c = read();}
        boolean neg = (c == '-');
        if(neg) c = read();
        do{
            ret = (ret<<3) + (ret<<1) + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        return neg ? -ret : ret;
    }

    public String readLine() throws IOException {
        byte[] buf = new byte[1000]; // line length
        int cnt = 0, c;
        while((c=read())!=-1){
            if(c=='\n'){
                if(cnt!=0) break;
                else continue;
            }
            buf[cnt++] = (byte)c;
        }
        return new String(buf, 0, cnt);
    }

    public long readLong() throws IOException {
        long ret = 0;
        byte c = read();
        while(c <= ' '){ c = read();}
        boolean neg = (c == '-');
        if(neg) c = read();
        do{
            ret = (ret<<3) + (ret<<1) + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        return neg ? -ret : ret;
    }

    private void fillBuffer() throws IOException {
        bytesRead = din.read(buffer, bufferPointer = 0, BUFFER_SIZE);
        if(bytesRead == -1) buffer[0] = -1;
    }

    private byte read() throws IOException {
        if(bufferPointer == bytesRead) fillBuffer();
        return buffer[bufferPointer++];
    }

    public void close() throws IOException {
        if(din==null) return;
        din.close();
    }
}
#endif
}
