using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 9
이름 : 배성훈
내용 : 연속합과 쿼리
    문제번호 : 16993번

    세그먼트 트리 문제다

    처음에 연속합을 왼쪽 끝의 최대값, 
    오른쪽 끝의 최대값으로 하면 되겠지
    하고 접근했다

    그러니 전체 합 변수도 필요했다
    세그먼트 트리 update 부분은 해결되었으나
    이걸로 어떻게 구간 최대값 찾지? 에서 막혔다;
    이전꺼에 구간 최대값이 있으면 아래로 내려가야 할텐데... 하고 생각했고
    이 경우 세그먼트 트리가 의미 있나 생각이 들었다

    결국 검색을 했고, 해당 구간의 최대값을 저장하면 해결되는 문제였다;
    그리고 전체 최대값은 왼쪽, 오른쪽 구간의 최대값이나
    새롭게 만들어지는 왼쪽에서 오른쪽 끝과 오른쪽에서 왼쪽 끝을 이어붙이는 구간 중에서 나온다!
*/

namespace BaekJoon._57
{
    internal class _57_04
    {

        static void Main4(string[] args)
        {

            int MIN = -100_000_001;
            (int lMax, int rMax, int tMax, int tSum) ZERO = (MIN, MIN, MIN, 0);
            StreamReader sr;
            StreamWriter sw;
            (int lMax, int rMax, int tMax, int tSum)[] seg;

            int n;

            Solve();

            void Solve()
            {

                Input();

                GetRet();

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    var ret = GetVal(1, n, f, t);
                    sw.Write($"{ret.tMax}\n");
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                n = ReadInt();

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;

                // lMax: 해당 idx가 왼쪽끝, 
                // rMax: 해당 idx가 오른쪽 끝,
                // tMax: 해당 idx가 포함된 구간 
                seg = new (int lMax, int rMax, int tMax, int tSum)[1 << log];
                Array.Fill(seg, ZERO);
                for (int i = 1; i <= n; i++)
                {

                    int val = ReadInt();
                    Update(1, n, i, val);
                }
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] = (_val, _val, _val, _val);
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chk) Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);
                else Update(_s, mid, _chk, _val, _idx * 2 + 1);

                seg[_idx].lMax = Math.Max(seg[_idx * 2 + 1].lMax, 
                    seg[_idx * 2 + 1].tSum + seg[_idx * 2 + 2].lMax);

                seg[_idx].rMax = Math.Max(seg[_idx * 2 + 2].rMax,
                    seg[_idx * 2 + 1].rMax + seg[_idx * 2 + 2].tSum);

                seg[_idx].tMax = Math.Max(seg[_idx * 2 + 1].tMax, seg[_idx * 2 + 2].tMax);
                seg[_idx].tMax = Math.Max(seg[_idx].tMax, seg[_idx * 2 + 1].rMax + seg[_idx * 2 + 2].lMax);
                seg[_idx].tSum = seg[_idx * 2 + 1].tSum + seg[_idx * 2 + 2].tSum;
            }

            (int lMax, int rMax, int tMax, int tSum) GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return ZERO;
                if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                int mid = (_s + _e) >> 1;

                var ret = ZERO;
                var left = GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1);
                var right = GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);

                ret.lMax = Math.Max(left.lMax, left.tSum + right.lMax);
                ret.rMax = Math.Max(right.rMax, left.rMax + right.tSum);
                // 전체 최대값 찾기
                ret.tMax = Math.Max(left.tMax, right.tMax);
                ret.tMax = Math.Max(ret.tMax, left.rMax + right.lMax);

                ret.tSum = left.tSum + right.tSum;

                return ret;
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using System;
using System.Text;

public class Program
{
    struct Node
    {
        public Node(int v)
        {
            sum = max = left = right = v;
        }
        public int sum, max, left, right;
    }
    static int[] array;
    static Node[] tree;
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        array = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        tree = new Node[n * 4];
        Init(0, n - 1, 1);
        int m = int.Parse(Console.ReadLine());
        StringBuilder sb = new();
        for (int i = 0; i < m; i++)
        {
            string[] ij = Console.ReadLine().Split(' ');
            sb.AppendLine(Query(0, n - 1, int.Parse(ij[0]) - 1, int.Parse(ij[1]) - 1, 1).max.ToString());
        }
        Console.Write(sb.ToString());
    }
    static void Init(int start, int end, int node)
    {
        if (start == end)
        {
            tree[node] = new(array[start]);
            return;
        }
        int mid = (start + end) / 2;
        Init(start, mid, node * 2);
        Init(mid + 1, end, node * 2 + 1);
        tree[node] = Merge(tree[node * 2], tree[node * 2 + 1]);
    }
    static Node Query(int start, int end, int left, int right, int node)
    {
        if (left > end || right < start)
            return new(-100000000) { sum = 0 };
        if (start >= left && end <= right)
            return tree[node];
        int mid = (start + end) / 2;
        return Merge(Query(start, mid, left, right, node * 2), Query(mid + 1, end, left, right, node * 2 + 1));
    }
    static Node Merge(Node left, Node right)
    {
        return new() { sum = left.sum + right.sum, max = Math.Max(left.right + right.left, Math.Max(left.max, right.max)), left = Math.Max(left.left, left.sum + right.left), right = Math.Max(right.right, left.right + right.sum) };
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

#nullable disable

public record struct GoldMineSegNode(long LeftStartMax, long RightStartMax, long Max, long Sum);
public sealed class GoldMineSeg : GenericSeg<GoldMineSegNode>
{
    public GoldMineSeg(GoldMineSegNode[] init)
        : base(init)
    {
    }

    protected override GoldMineSegNode Merge(GoldMineSegNode lhs, GoldMineSegNode rhs)
    {
        return new GoldMineSegNode(
            Math.Max(lhs.LeftStartMax, lhs.Sum + rhs.LeftStartMax),
            Math.Max(rhs.RightStartMax, rhs.Sum + lhs.RightStartMax),
            Math.Max(Math.Max(lhs.Max, rhs.Max), lhs.RightStartMax + rhs.LeftStartMax),
            lhs.Sum + rhs.Sum);
    }
}

public abstract class GenericSeg<T>
    where T : struct
{
    private int _leafMask;
    private T?[] _tree;

    public GenericSeg(T[] init)
    {
        var n = init.Length;
        _leafMask = (int)BitOperations.RoundUpToPowerOf2((uint)n);
        _tree = new T?[2 * _leafMask];

        for (var idx = 0; idx < n; idx++)
            _tree[_leafMask | idx] = init[idx];

        for (var idx = _leafMask - 1; idx > 0; idx--)
        {
            if (!_tree[2 * idx + 1].HasValue)
                _tree[idx] = _tree[2 * idx];
            else
                _tree[idx] = Merge(_tree[2 * idx].Value, _tree[2 * idx + 1].Value);
        }
    }

    public T Range(int stIncl, int edExcl)
    {
        var q = new Queue<(int pos, int stIncl, int edExcl)>();
        q.Enqueue((1, 0, _leafMask));

        var targets = new List<(int stIncl, int pos)>();
        while (q.TryDequeue(out var s))
        {
            if (s.edExcl <= stIncl || edExcl <= s.stIncl)
                continue;

            if (stIncl <= s.stIncl && s.edExcl <= edExcl)
            {
                targets.Add((s.stIncl, s.pos));
                continue;
            }

            var mid = (s.stIncl + s.edExcl) / 2;
            q.Enqueue((2 * s.pos, s.stIncl, mid));
            q.Enqueue((2 * s.pos + 1, mid, s.edExcl));
        }

        return targets
            .OrderBy(p => p.stIncl)
            .Select(p => _tree[p.pos].Value)
            .Aggregate(Merge);
    }

    protected abstract T Merge(T lhs, T rhs);
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var values = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var seg = new GoldMineSeg(values.Select(v => new GoldMineSegNode(v, v, v, v)).ToArray());

        var m = Int32.Parse(sr.ReadLine());
        while (m-- > 0)
        {
            var q = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var v = seg.Range(q[0] - 1, q[1]);

            sw.WriteLine(v.Max);
        }
    }
}
#elif other3
// #pragma GCC optimize("O3")
// #pragma GCC optimize("unroll-loops")
// #pragma GCC target("avx,avx2")
// #include <bits/stdc++.h>
// #ifdef SHARAELONG
// #include "../../cpp-header/debug.hpp"
// #endif
using namespace std;
typedef long long ll;
typedef pair<int, int> pii;

const int INF = 0x3f3f3f3f;

struct Node
{
    int mx, lmx, rmx, s;
};

Node operator *(const Node& a, const Node& b)
{
    return { max({ a.mx, b.mx, a.rmx + b.lmx }), max(a.lmx, a.s + b.lmx), max(b.rmx, a.rmx + b.s), a.s + b.s };
}

struct SegmentTree
{
    int n, h;
    vector<Node> tree;

    SegmentTree(const vector<int>& arr) {
        h = Log2(arr.size());
        n = 1 << h;
        tree.resize(2*n);
        for (int i=0; i<arr.size(); ++i) tree[n + i] = { arr[i], arr[i], arr[i], arr[i] };

    for (int i=n-1; i>0; --i) tree[i] = tree[2 * i] * tree[2 * i + 1];
    }

    int query(int l, int r)
    {
        l += n, r += n;
        Node lret = { -INF, -INF, -INF, 0 };
        Node rret = { -INF, -INF, -INF, 0 };
        for (; l <= r; l /= 2, r /= 2)
        {
            if (l & 1) lret = lret * tree[l++];
            if (~r & 1) rret = tree[r--] * rret;
        }

        Node ret = lret * rret;
        return ret.mx;
    }

    static int Log2(int x)
    {
        int ret = 0;
        while (x > (1 << ret)) ret++;
        return ret;
    }
};

namespace fio
{
    const int BSIZE = 1 << 21;
    char buffer[BSIZE];
    char wbuffer[BSIZE];
    char ss[30];
    int pos = BSIZE;
    int wpos = 0;
    int cnt = 0;

    inline char readChar()
    {
        if (pos == BSIZE)
        {
            fread(buffer, 1, BSIZE, stdin);
            pos = 0;
        }
        return buffer[pos++];
    }
    inline int readInt()
    {
        char c = readChar();

        while ((c < '0' || c > '9') && (c ^ '-'))
        {
            c = readChar();
        }
        int res = 0;

        bool neg = (c == '-');

        if (neg) c = readChar();

        while (c > 47 && c < 58)
        {
            res = res * 10 + c - '0';
            c = readChar();
        }

        if (neg) return -res;
        else return res;
    }
    inline void writeChar(char x)
    {
        if (wpos == BSIZE)
        {
            fwrite(wbuffer, 1, BSIZE, stdout);
            wpos = 0;
        }
        wbuffer[wpos++] = x;
    }
    inline void writeInt(int x)
    {
        if (x < 0)
        {
            writeChar('-');
            x = -x;
        }
        if (!x)
        {
            writeChar('0');
        }
        else
        {
            cnt = 0;
            while (x)
            {
                ss[cnt] = (x % 10) + '0';
                cnt++;
                x /= 10;
            }
            for (int j = cnt - 1; j >= 0; j--)
            {
                writeChar(ss[j]);
            }
        }

    }
    inline void my_flush()
    {
        if (wpos)
        {
            fwrite(wbuffer, 1, wpos, stdout);
            wpos = 0;
        }
    }
}

void solve()
{
    int n = fio::readInt();
    vector<int> arr(n);
    for (int & x: arr) x = fio::readInt();

SegmentTree seg(arr);
int m = fio::readInt();
for (int i = 0; i < m; ++i)
{
    int l = fio::readInt();
    int r = fio::readInt();
    cout << seg.query(l - 1, r - 1) << '\n';
}
}

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);

    solve();
}
#endif
}
