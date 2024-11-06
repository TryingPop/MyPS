using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 행성 터널
    문제번호 : 2887번

    정렬, 최소 스패닝 트리 문제다
    아이디어는 다음과 같다
    도로 가격이 x차, y차, z차 중 가장 작은 것이기에
    각각 x, y, z 순으로 정렬하고 인접항끼리 거리를 계산하면 된다
    그러면 각 점에 대해 최소 거리가 3 ~ 6개가 나온다
    이러한 거리를 가지고, 유니온 파인드와 위상정렬으로 MST를 구하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0657
    {

        static void Main657(string[] args)
        {

            StreamReader sr;

            int n;
            (int x, int y, int z, int idx)[] arr;
            PriorityQueue<(int f, int b, int dis), int> q;
            Stack<int> s;
            int[] group;
            Solve();

            void Solve()
            {

                Input();
                GetDis();
                MST();
            }

            void MST()
            {

                long ret = 0;
                s = new();

                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    int f = Find(node.f);
                    int b = Find(node.b);

                    if (f == b) continue;
                    ret += node.dis;
                    group[f] = b;
                }

                Console.WriteLine(ret);
            }

            void GetDis()
            {

                q = new(3 * n);

                Array.Sort(arr, (x, y) => x.x.CompareTo(y.x));
                for (int i = 1; i < n; i++)
                {

                    int f = arr[i - 1].idx;
                    int b = arr[i].idx;
                    int dis = arr[i].x - arr[i - 1].x;

                    q.Enqueue((f, b, dis), dis);
                }

                Array.Sort(arr, (x, y) => x.y.CompareTo(y.y));
                for (int i = 1; i < n; i++)
                {

                    int f = arr[i - 1].idx;
                    int b = arr[i].idx;
                    int dis = arr[i].y - arr[i - 1].y;

                    q.Enqueue((f, b, dis), dis);
                }

                Array.Sort(arr, (x, y) => x.z.CompareTo(y.z));
                for (int i = 1; i < n; i++)
                {

                    int f = arr[i - 1].idx;
                    int b = arr[i].idx;
                    int dis = arr[i].z - arr[i - 1].z;

                    q.Enqueue((f, b, dis), dis);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                group = new int[n];
                arr = new (int x, int y, int z, int idx)[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt(), ReadInt(), i);
                    group[i] = i;
                }

                sr.Close();
            }

            int Find(int _chk)
            {

                while(_chk != group[_chk])
                {

                    s.Push(_chk);
                    _chk = group[_chk];
                }

                while(s.Count > 0)
                {

                    group[s.Pop()] = _chk;
                }

                return _chk;
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
    }

#if other
namespace BOJ_2887
{
    internal class Program
    {
        static int n;
        static int[] parent;
        static void Main(string[] args)
        {
            n = int.Parse(Console.ReadLine()!);
            parent = Enumerable.Repeat(-1, n + 1).ToArray();
            var sortedX = new List<(int,int)>();
            var sortedY = new List<(int,int)>();
            var sortedZ = new List<(int,int)>();
            for(int i = 1; i <= n; i++)
            {
                var xyz = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
                sortedX.Add((xyz[0], i));
                sortedY.Add((xyz[1], i));
                sortedZ.Add((xyz[2], i));
            }
            sortedX.Sort();
            sortedY.Sort();
            sortedZ.Sort();
            var dist = new PriorityQueue<(int, int, int), int>();
            var (answer, cnt) = (0, 0);
            for(int i = n - 1; i >= 1; i--)
            {
                dist.Enqueue((sortedX[i].Item1 - sortedX[i - 1].Item1, sortedX[i].Item2, sortedX[i - 1].Item2), sortedX[i].Item1 - sortedX[i - 1].Item1);
                dist.Enqueue((sortedY[i].Item1 - sortedY[i - 1].Item1, sortedY[i].Item2, sortedY[i - 1].Item2), sortedY[i].Item1 - sortedY[i - 1].Item1);
                dist.Enqueue((sortedZ[i].Item1 - sortedZ[i - 1].Item1, sortedZ[i].Item2, sortedZ[i - 1].Item2), sortedZ[i].Item1 - sortedZ[i - 1].Item1);
            }
            while (dist.Count > 0)
            {
                var (cost, a, b) = dist.Dequeue();
                if (!CheckGroup(a, b)) 
                    continue;
                answer += cost;
                cnt++;
                if (cnt == n - 1)
                    break;
            }
            Console.WriteLine(answer);
        }
        static int FindParent(int x)
        {
            if (parent[x] < 0) return x;
            return parent[x] = FindParent(parent[x]);
        }
        static bool CheckGroup(int a, int b)
        {
            if (a < b) (a, b) = (b, a);
            a = FindParent(a); b = FindParent(b);
            if (a == b) return false;
            parent[b] = a;
            return true;
        }
    }
}

#elif other2
using System;
using System.IO;

var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int n = GetSignedInt();

var coords = new Coord[n];
for (int i = 0; i < n; i++)
    coords[i] = new(GetSignedInt(), GetSignedInt(), GetSignedInt(), i);
sr.Close();

var edges = new Edge[(n - 1) * 3];
Array.Sort(coords, (a, b) => a.X - b.X);
for (int i = 0; i < n - 1; i++)
{
    var cur = coords[i];
    var nex = coords[i + 1];
    edges[i] = new(cur.I, nex.I, Math.Abs(cur.X - nex.X));
}

Array.Sort(coords, (a, b) => a.Y - b.Y);
for (int i = 0; i < n - 1; i++)
{
    var cur = coords[i];
    var nex = coords[i + 1];
    edges[i + n - 1] = new(cur.I, nex.I, Math.Abs(cur.Y - nex.Y));
}

Array.Sort(coords, (a, b) => a.Z - b.Z);
for (int i = 0; i < n - 1; i++)
{
    var cur = coords[i];
    var nex = coords[i + 1];
    edges[i + 2 * (n - 1)] = new(cur.I, nex.I, Math.Abs(cur.Z - nex.Z));
}
Array.Sort(edges, (a, b) => a.W - b.W);

var link = new int[n];
for (int i = 1; i < link.Length; i++)
    link[i] = i;

int ptr = 0;
long ret = 0;
for (int i = 0; i < n - 1; i++)
{
    Edge e;
    int ra, rb;
    do
        e = edges[ptr++];
    while ((ra = Root(e.A)) == (rb = Root(e.B)));
    link[rb] = ra;
    ret += e.W;
}
Console.Write(ret);

int Root(int item)
{
    int ret = item;
    while ((ret = link[ret]) != link[ret]) ;
    return link[item] = ret;
}

int GetSignedInt()
{
    int c, ret = 0;
    if ((c = sr.Read()) == '-')
        while ((c = sr.Read()) != ' ' &&
            (c != '\r' || (c = sr.Read()) < 0) &&
            c != '\n')
        {
            ret *= 10;
            ret -= c - '0';
        }
    else
    {
        ret += c - '0';
        while ((c = sr.Read()) != ' ' &&
            (c != '\r' || (c = sr.Read()) < 0) &&
            c != '\n')
        {
            ret *= 10;
            ret += c - '0';
        }
    }
    return ret;
}

struct Coord
{
    public int X;
    public int Y;
    public int Z;
    public int I;

    public Coord(int x, int y, int z, int i)
    {
        X = x;
        Y = y;
        Z = z;
        I = i;
    }
}

struct Edge
{
    public int A;
    public int B;
    public int W;

    public Edge(int i1, int i2, int v) : this()
    {
        A = i1;
        B = i2;
        W = v;
    }
}
#elif other3
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public record struct Point(int idx, long X, long Y, long Z);

public class UnionFind
{
    private int[] _set;
    private int[] _rank;
    private List<int> _buffer;

    public UnionFind(int n)
    {
        _set = new int[n];
        _rank = new int[n];
        _buffer = new List<int>(n);

        for (var idx = 0; idx < n; idx++)
        {
            _set[idx] = idx;
            _rank[idx] = 1;
        }
    }

    public int Find(int v)
    {
        var root = _set[v];
        if (root == _set[root])
            return root;

        _buffer.Clear();
        do
        {
            _buffer.Add(root);
            root = _set[root];
        }
        while (root != _set[root]);

        foreach (var val in _buffer)
            _set[val] = root;

        return root;
    }

    public void Union(int l, int r)
    {
        var leftRoot = Find(l);
        var rightRoot = Find(r);

        if (leftRoot == rightRoot)
            return;

        if (_rank[leftRoot] < _rank[rightRoot])
        {
            _set[leftRoot] = rightRoot;
            _rank[rightRoot] += _rank[leftRoot];
        }
        else
        {
            _set[rightRoot] = leftRoot;
            _rank[leftRoot] += _rank[rightRoot];
        }
    }
}

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
            points[idx] = new Point(idx, l[0], l[1], l[2]);
        }

        var edges = new List<(int src, int dst, long cost)>();

        Array.Sort(points, (l, r) => l.X.CompareTo(r.X));
        for (var idx = 0; idx < n - 1; idx++)
        {
            var p1 = points[idx];
            var p2 = points[idx + 1];

            edges.Add((p1.idx, p2.idx, Math.Abs(p1.X - p2.X)));
        }

        Array.Sort(points, (l, r) => l.Y.CompareTo(r.Y));
        for (var idx = 0; idx < n - 1; idx++)
        {
            var p1 = points[idx];
            var p2 = points[idx + 1];

            edges.Add((p1.idx, p2.idx, Math.Abs(p1.Y - p2.Y)));
        }

        Array.Sort(points, (l, r) => l.Z.CompareTo(r.Z));
        for (var idx = 0; idx < n - 1; idx++)
        {
            var p1 = points[idx];
            var p2 = points[idx + 1];

            edges.Add((p1.idx, p2.idx, Math.Abs(p1.Z - p2.Z)));
        }

        var uf = new UnionFind(n);
        var costSum = 0L;

        foreach (var (src, dst, cost) in edges.OrderBy(v => v.cost))
        {
            if (uf.Find(src) == uf.Find(dst))
                continue;

            uf.Union(src, dst);
            costSum += cost;
        }

        sw.WriteLine(costSum);
    }
}

#endif
}
