using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 군인
    문제번호 : 1321번

    세그먼트 트리, 이분탐색 문제다
    세그먼트 트리로 해결했다
    확인 부분에서 인덱스 처리를 잘못해서 1번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0300
    {

        static void Main300(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m;

            int[] seg;

            Solve();
            void Solve()
            {

                Input();
                m = ReadInt();

                for (int i = 0; i < m; i++)
                {

                    int type = ReadInt();
                    if (type == 1)
                    {

                        int f = ReadInt();
                        int b = ReadInt();
                        Update(1, n, f, b);
                    }
                    else
                    {

                        int f = ReadInt();
                        int ret = GetVal(1, n, f);
                        sw.Write($"{ret}\n");
                    }
                }

                sw.Close();
                sr.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                n = ReadInt();
                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new int[1 << log];

                for (int i = 1; i <= n; i++)
                {

                    int cur = ReadInt();
                    Update(1, n, i, cur);
                }
            }

            int GetVal(int _start, int _end, int _n, int _idx = 0)
            {

                if (_start == _end) return _start;

                int mid = (_start + _end) / 2;
                int ret;
                if (seg[_idx * 2 + 1] >= _n) ret = GetVal(_start, mid, _n, _idx * 2 + 1);
                else ret = GetVal(mid + 1, _end, _n - seg[_idx * 2 + 1], _idx * 2 + 2);

                return ret;
            }

            void Update(int _start, int _end, int _find, int _val, int _idx = 0)
            {

                if(_start == _end)
                {

                    seg[_idx] += _val;
                    return;
                }

                int mid = (_start + _end) / 2;
                if (mid < _find) Update(mid + 1, _end, _find, _val, _idx * 2 + 2);
                else Update(_start, mid, _find, _val, _idx * 2 + 1);

                seg[_idx] = seg[_idx * 2 + 1] + seg[_idx * 2 + 2];
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if ( c== '-')
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
// #nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SegTree<T>
{
    private record struct SegTreeRange(int StartInclusive, int EndInclusive)
    {
        public bool CanSplit => (StartInclusive != EndInclusive);

        public void Split(out SegTreeRange lhs, out SegTreeRange rhs)
        {
            var mid = (StartInclusive + EndInclusive) / 2;

            lhs = new SegTreeRange(StartInclusive, mid);
            rhs = new SegTreeRange(mid + 1, EndInclusive);
        }

        public bool IsOverlapped(SegTreeRange other)
        {
            return !(StartInclusive > other.EndInclusive || other.StartInclusive > EndInclusive);
        }
        public bool IsSubRangeOf(SegTreeRange other)
        {
            return other.StartInclusive <= StartInclusive && EndInclusive <= other.EndInclusive;
        }
    }

    public delegate T Aggregate(T lhs, T rhs);
    public delegate T UpdateAggregate(T elemBefore, T elemAfter, T aggregateBefore);

    private int _elementCount;
    private T[] _tree;

    private Aggregate _aggregate;

    // this is callvirt, change to concrete class if slow
    public SegTree(IList<T> values, Aggregate aggregate)
    {
        _aggregate = aggregate;
        _elementCount = values.Count;

        var treeSize = 1 << (int)(1 + Math.Ceiling(Math.Log2(_elementCount)));
        _tree = new T[treeSize];

        Init(values, 1, new SegTreeRange(0, _elementCount - 1));
    }

    private T Init(IList<T> values, int node, SegTreeRange range)
    {
        if (!range.CanSplit)
        {
            _tree[node] = values[range.StartInclusive];
        }
        else
        {
            range.Split(out var leftRange, out var rightRange);

            var lhs = Init(values, 2 * node, leftRange);
            var rhs = Init(values, 2 * node + 1, rightRange);

            _tree[node] = _aggregate(lhs, rhs);
        }

        return _tree[node];
    }

    public T Query(int startInclusive, int endInclusive)
    {
        var queryRange = new SegTreeRange(startInclusive, endInclusive);
        var treeRange = new SegTreeRange(0, _elementCount - 1);

        Query(1, queryRange, treeRange, out var v);
        return v;
    }
    private bool Query(int node, SegTreeRange queryRange, SegTreeRange treeRange, out T val)
    {
        // do something idempotent
        if (!queryRange.IsOverlapped(treeRange))
        {
            val = default;
            return false;
        }

        if (treeRange.IsSubRangeOf(queryRange))
        {
            val = _tree[node];
            return true;
        }

        treeRange.Split(out var leftRange, out var rightRange);
        var findLeft = Query(node * 2, queryRange, leftRange, out var leftVal);
        var findRight = Query(node * 2 + 1, queryRange, rightRange, out var rightVal);

        if (findLeft && findRight)
        {
            val = _aggregate(leftVal, rightVal);
        }
        else if (findLeft || findRight)
        {
            if (findLeft)
                val = leftVal;
            else
                val = rightVal;
        }
        else
        {
            val = default;
        }

        return findLeft || findRight;
    }

    public void UpdateElem(int node, T newValue)
    {
        UpdateElem(1, new SegTreeRange(node, node), new SegTreeRange(0, _elementCount - 1), newValue);
    }
    private void UpdateElem(int node, SegTreeRange queryRange, SegTreeRange treeRange, T newValue)
    {
        if (!queryRange.IsOverlapped(treeRange))
            return;

        if (treeRange.CanSplit)
        {
            treeRange.Split(out var leftRange, out var rightRange);

            UpdateElem(2 * node, queryRange, leftRange, newValue);
            UpdateElem(2 * node + 1, queryRange, rightRange, newValue);

            _tree[node] = _aggregate(_tree[2 * node], _tree[2 * node + 1]);
        }
        else
        {
            _tree[node] = newValue;
        }
    }
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var a = sr.ReadLine()
            .Split(' ')
            .Select(Int32.Parse)
            .ToArray();

        var m = Int32.Parse(sr.ReadLine());

        var segtree = new SegTree<int>(a, (l, r) => l + r);

        for (var idx = 0; idx < m; idx++)
        {
            var q = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            if (q[0] == 1)
            {
                var index = q[1] - 1;
                a[index] += q[2];

                segtree.UpdateElem(index, a[index]);
            }
            else if (q[0] == 2)
            {
                var left = 0;
                var right = n - 1;

                var leftSum = segtree.Query(0, left);
                var rightSum = segtree.Query(0, right);
                var target = q[1] - 1;

                while (true)
                {
                    if (left >= right)
                    {
                        sw.WriteLine(1 + right);
                        break;
                    }

                    var mid = (left + right) / 2;
                    var midSum = segtree.Query(0, mid);

                    if (target >= midSum)
                        left = mid + 1;
                    else
                        right = mid;
                }
            }
        }
    }
}

#elif other2
// #include <bits/stdc++.h>
// #define fastio cin.tie(0)->sync_with_stdio(0)
using namespace std;

int n, q, FT[500'001];

void Update(int i, int x) {
	for (; i <= n; i += i & -i) FT[i] += x;
}

int Kth(int k) {
	int ret = 0;
	for (int i = 18; ~i; i--) {
		int j = ret | 1 << i;
		if (j <= n && FT[j] < k) k -= FT[ret = j];
	}
	return ret + 1;
}

int main() {
	fastio;
	cin >> n;
	for (int i = 1; i <= n; i++) {
		cin >> FT[i];
		for (int s = i & -i; s >>= 1;) FT[i] += FT[i - s];
	}
	cin >> q;
	while (q--) {
		int t, a, b; cin >> t >> a;
		if (t == 1) cin >> b, Update(a, b);
		else cout << Kth(a) << '\n';
	}
}
#elif other3
// #include <bits/stdc++.h>
// #define Mid (start+end)/2

using namespace std;

int n, m, arr[1000003], seg[1000003];

int init(int start, int end, int idx) {

    if (start == end) return seg[idx] = arr[start];
    return seg[idx] = init(start, Mid, idx*2)+init(Mid+1, end, idx*2+1);

}

int update(int start, int end, int idx, int target, int value) {

    if (start > target || end < target) return seg[idx];
    if (start == end) return seg[idx] += value;
    return seg[idx] = update(start, Mid, idx*2, target, value)+update(Mid+1, end, idx*2+1, target, value);

}

int query(int start, int end, int idx, int target, int sum) {

    if (start == end) return start;
    if (target <= seg[idx*2]+sum) return query(start, Mid, idx*2, target, sum);
    return query(Mid+1, end, idx*2+1, target, sum+seg[idx*2]);

}

signed main() {

    ios_base::sync_with_stdio(0);
    cin.tie(0); cout.tie(0);

    cin >> n;
    for (int k = 1; k <= n; k++) cin >> arr[k];
    init(1, n, 1);
    cin >> m;
    int a, p, q;
    for (int k = 0; k < m; k++) {

        cin >> a;
        if (a == 1) {

            cin >> p >> q;
            update(1, n, 1, p, q);

        }
        else {

            cin >> p;
            cout << query(1, n, 1, p, 0) << '\n';

        }

    }

}
#endif
}
