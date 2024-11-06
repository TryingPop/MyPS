using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 5
이름 : 배성훈
내용 : 구간 합 구하기 2
    문제번호 : 10999번

    느리게 갱신되는 세그먼트 트리 문제다
    느리게 갱신되는 세그먼트 트리는 구간합을 넣었을 때 방문한 노드의 값만 갱신한다
    그리고 자식들은 lazy에 기록만하고 갱신은 하지 않는다
    이후 lazy값은 해당 자식 노드를 방문하면 lazy 처리를 해주고 해당 노드의 자식들한테 넘겨준다

    그래서 범위에 해당하는 노드로 가면 lazy값을 누적합에 적용시키고
    자식이 존재한다면 자식들에게 lazy값을 넘긴다
    이후 부모노드들은 임시로 lazy값을 초기화한 것일 뿐, 자식들이 초기화 되었다면
    자식들 값의 합으로 채운다
    이에 자식 탐색을 왼쪽길만 해당된다고 왼쪽길만 탐색하면 안된다!
    양쪽길 둘 다 탐색해야 합이 정확하게 일치한다! (오른쪽 값은 lazy 갱신만 하고 탈출하게 해야한다)

    이렇게 제출하니 588ms에 통과했다
    양쪽 자식 모두 lazy 업데이트를 안해 한 번 틀렸다;
*/

namespace BaekJoon._53
{
    internal class _53_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m, k;
            (long n, long lazy)[] seg;

            Solve();

            void Solve()
            {

                Init();
                int len = m + k;

                for (int i = 0; i < len; i++)
                {

                    int op = ReadInt();
                    int f = ReadInt();
                    int b = ReadInt();

                    if (op == 1)
                    {

                        long add = ReadLong();
                        Update(1, n, f, b, add);
                    }
                    else
                    {

                        long ret = GetVal(1, n, f, b);
                        sw.Write($"{ret}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                int log = (int)(Math.Ceiling(Math.Log2(n))) + 1;
                seg = new (long n, long lazy)[1 << log];

                for (int i = 1; i <= n; i++)
                {

                    long cur = ReadLong();
                    Update(1, n, i, i, cur);
                }
            }

            void LazyUpdate(int _s, int _e, int _idx)
            {

                if (seg[_idx].lazy == 0) return;
                long lazy = seg[_idx].lazy;
                seg[_idx].n += (_e - _s + 1) * lazy;
                seg[_idx].lazy = 0;

                if (_s == _e) return;
                seg[_idx * 2 + 1].lazy += lazy;
                seg[_idx * 2 + 2].lazy += lazy;
            }

            void Update(int _s, int _e, int _chkS, int _chkE, long _add, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);
                if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].n += (_e - _s + 1) * _add;
                    if (_s != _e)
                    {

                        seg[_idx * 2 + 1].lazy += _add;
                        seg[_idx * 2 + 2].lazy += _add;
                    }

                    return;
                }

                if (_chkE < _s || _e < _chkS) return;

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _add, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _add, _idx * 2 + 2);

                seg[_idx].n = seg[_idx * 2 + 1].n + seg[_idx * 2 + 2].n;
            }

            long GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);
                if (_chkS <= _s && _e <= _chkE) return seg[_idx].n;
                if (_chkE < _s || _e < _chkS) return 0L;

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }

            long ReadLong()
            {

                long c = sr.Read();
                if (c == -1) return 0;

                bool plus = c != '-';
                long ret = plus ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

#nullable disable

public record struct LongSumLazyInfo(long Value, long Count);
public class LongSumLazySeg : LazySeg<long, long, LongSumLazyInfo>
{
    public LongSumLazySeg(int size)
        : base(size)
    {
    }

    public override void ApplyLazy(LongSumLazyInfo lazy, ref long target)
    {
        target += lazy.Value * lazy.Count;
    }

    public override LongSumLazyInfo CreateLazy(long update, int nodeCount)
    {
        return new LongSumLazyInfo(update, nodeCount);
    }

    public override void Merge(long left, long right, ref long target)
    {
        target = left + right;
    }

    public override LongSumLazyInfo MergeLazy(LongSumLazyInfo l, LongSumLazyInfo r)
    {
        return new LongSumLazyInfo(l.Value + r.Value, l.Count);
    }

    public override LongSumLazyInfo SplitLazy(LongSumLazyInfo lazy)
    {
        return new LongSumLazyInfo(lazy.Value, lazy.Count / 2);
    }
}
public abstract class LazySeg<TNode, TUpdate, TLazy>
    where TNode : struct
    where TUpdate : struct
    where TLazy : struct
{
    private TNode[] _tree;
    private TLazy?[] _lazy;

    private int _leafMask;

    private Queue<(int pos, int stIncl, int edExcl)> _q;

    public LazySeg(int size)
    {
        var initSizeLog = 1 + BitOperations.Log2((uint)size - 1);

        _leafMask = 1 << initSizeLog;
        var treeSize = 2 * _leafMask;

        _tree = new TNode[treeSize];
        _lazy = new TLazy?[treeSize];

        _q = new Queue<(int pos, int stIncl, int edExcl)>(2 * initSizeLog);
    }
    public void Init(TNode[] init)
    {
        if (init.Length > _leafMask)
            throw new ArgumentException();

        for (var idx = 0; idx < init.Length; idx++)
            _tree[_leafMask | idx] = init[idx];

        for (var idx = _leafMask - 1; idx >= 1; idx--)
        {
            var l = _tree[2 * idx];
            var r = _tree[2 * idx + 1];

            Merge(l, r, ref _tree[idx]);
        }
    }

    public void Update(int stIncl, int edExcl, TUpdate update)
    {
        _q.Enqueue((1, 0, _leafMask));

        while (_q.TryDequeue(out var state))
        {
            var (nodepos, nodeStIncl, nodeEdExcl) = state;
            EnsureLazyApplied(nodepos);

            // out of range
            if (nodeEdExcl <= stIncl || edExcl <= nodeStIncl)
            {
                continue;
            }
            // in range
            else if (stIncl <= nodeStIncl && nodeEdExcl <= edExcl)
            {
                var l = CreateLazy(update, nodeEdExcl - nodeStIncl);
                _lazy[nodepos] = l;
            }
            else
            {
                var overwrapCount = Math.Min(nodeEdExcl, edExcl) - Math.Max(nodeStIncl, stIncl);
                var l = CreateLazy(update, overwrapCount);
                ApplyLazy(l, ref _tree[nodepos]);

                var mid = (nodeStIncl + nodeEdExcl) / 2;
                _q.Enqueue((2 * nodepos, nodeStIncl, mid));
                _q.Enqueue((2 * nodepos + 1, mid, nodeEdExcl));
            }
        }
    }
    public TNode Range(int stIncl, int edExcl)
    {
        _q.Enqueue((1, 0, _leafMask));

        var aggregateList = new List<(int pos, int stIncl)>();

        while (_q.TryDequeue(out var state))
        {
            var (nodepos, nodeStIncl, nodeEdExcl) = state;
            EnsureLazyApplied(nodepos);

            // out of range
            if (nodeEdExcl <= stIncl || edExcl <= nodeStIncl)
            {
                continue;
            }
            // in range
            else if (stIncl <= nodeStIncl && nodeEdExcl <= edExcl)
            {
                aggregateList.Add((nodepos, nodeStIncl));
            }
            else
            {
                var mid = (nodeStIncl + nodeEdExcl) / 2;
                _q.Enqueue((2 * nodepos, nodeStIncl, mid));
                _q.Enqueue((2 * nodepos + 1, mid, nodeEdExcl));
            }
        }

        var aggregated = aggregateList
            .OrderBy(v => v.stIncl)
            .Select(v => _tree[v.pos])
            .Aggregate((l, r) => { Merge(l, r, ref l); return l; });

        return aggregated;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnsureLazyApplied(int pos)
    {
        if (_lazy[pos].HasValue)
        {
            ApplyLazy(_lazy[pos].Value, ref _tree[pos]);

            if ((pos & _leafMask) == 0)
            {
                var l = SplitLazy(_lazy[pos].Value);

                SafeMergeLazy(2 * pos, l);
                SafeMergeLazy(2 * pos + 1, l);
            }
        }

        _lazy[pos] = null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SafeMergeLazy(int pos, TLazy l)
    {
        if (_lazy[pos].HasValue)
            _lazy[pos] = MergeLazy(l, _lazy[pos].Value);
        else
            _lazy[pos] = l;
    }

    public abstract void Merge(TNode left, TNode right, ref TNode target);

    public abstract TLazy CreateLazy(TUpdate update, int nodeCount);
    public abstract void ApplyLazy(TLazy lazy, ref TNode target);
    public abstract TLazy MergeLazy(TLazy l, TLazy r);
    public abstract TLazy SplitLazy(TLazy lazy);
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nmk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (n, m, k) = (nmk[0], nmk[1], nmk[2]);

        var arr = new long[n];
        for (var idx = 0; idx < n; idx++)
            arr[idx] = Int64.Parse(sr.ReadLine());

        var seg = new LongSumLazySeg(n);
        seg.Init(arr);

        var q = m + k;
        while (q-- > 0)
        {
            var query = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();

            if (query[0] == 1)
            {
                var stIncl = (int)query[1] - 1;
                var edExcl = (int)query[2];
                var amount = query[3];

                seg.Update(stIncl, edExcl, amount);
            }
            else
            {
                var stIncl = (int)query[1] - 1;
                var edExcl = (int)query[2];
                var sum = seg.Range(stIncl, edExcl);

                sw.WriteLine(sum);
            }
        }
    }
}

#elif other2
var reader = new Reader();
var (n, m) = (reader.NextInt(), reader.NextInt() + reader.NextInt());

var array = new long[n];
for (int i = 0; i < n; i++)
    array[i] = reader.NextLong();

var segmentTree = new SegmentTree<long, long>(
    array, 0,
    (long oldValue, long value, int repeat) => oldValue += value * repeat,
    (long oldValue, long value, int repeat) => oldValue += value * repeat,
    (long left, long right) => left + right
);

using (var w = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
while (m -- > 0)
{
    var q = reader.NextInt();
    if (q == 1)
        segmentTree.UpdateRange(reader.NextInt() - 1, reader.NextInt() - 1, reader.NextLong());
    else
        w.WriteLine(segmentTree.Query(reader.NextInt() - 1, reader.NextInt() - 1));
}

class SegmentTree<T, QResult>
{
    public delegate QResult UpdateOperation(QResult oldValue, T value, int repeat = 1);
    public delegate QResult InternalUpdateOperation(QResult oldValue, QResult value, int repeat = 1);
    public delegate QResult QueryOperation(QResult left, QResult right);

    int _arraySize;
    int _treeSize;
    QResult[] _tree;
    QResult[] _lazy;
    UpdateOperation _updateOperation;
    InternalUpdateOperation _internalUpdateOperation;
    QueryOperation _queryOperation;
    QResult _queryResultNull;

    public SegmentTree(T[] array,
                       QResult queryResultNull,
                       Func<QResult, T, int, QResult> updateOperation,
                       Func<QResult, QResult, int, QResult> internalUpdateOperation,
                       Func<QResult, QResult, QResult> queryOperation)
    {
        int height = (int)Math.Ceiling(Math.Log2(array.Length)) + 1;

        _arraySize = array.Length;
        _treeSize = 1 << height;
        _tree = new QResult[_treeSize];
        _lazy = new QResult[_treeSize];
        _queryResultNull = queryResultNull;
        _updateOperation = new(updateOperation);
        _internalUpdateOperation = new(internalUpdateOperation);
        _queryOperation = new(queryOperation);

        DoBuild(array, 1, 0, _arraySize - 1);
    }

    private void DoBuild(T[] array, int node, int start, int end)
    {
        if (start == end)
        {
            _tree[node] = _updateOperation.Invoke(_tree[node], array[start]);
            return;
        }

        int left_child = LeftChildNode(node);
        int right_child = RightChildNode(node); 
        int mid = (start + end) / 2;
        DoBuild(array, left_child, start, mid);
        DoBuild(array, right_child, mid + 1, end);
        _tree[node] = _queryOperation.Invoke(_tree[left_child], _tree[right_child]);
    }

    private void DoLazyUpdate(int node, int start, int end)
    {
        if (_lazy[node].Equals(_queryResultNull))
            return;

        _tree[node] = _internalUpdateOperation(_tree[node], _lazy[node], end - start + 1);
        if (start != end) // Not a leaf node
        {
            int left_child = LeftChildNode(node);
            int right_child = RightChildNode(node);

            _lazy[left_child] = _internalUpdateOperation(_lazy[left_child], _lazy[node]);
            _lazy[right_child] = _internalUpdateOperation(_lazy[right_child], _lazy[node]);
        }

        _lazy[node] = _queryResultNull;
    }

    public QResult Query(int left, int right) => DoQuery(1, 0, _arraySize - 1, left, right);

    private QResult DoQuery(int node, int start, int end, int left, int right)
    {
        DoLazyUpdate(node, start, end);

        // Invalid Range
        if (right < start || end < left)
            return _queryResultNull;

        // In Range
        if (left <= start && end <= right)
            return _tree[node];

        // Partially In Range
        int left_child = LeftChildNode(node);
        int right_child = RightChildNode(node); 
        int mid = (start + end) / 2;
        QResult left_child_result = DoQuery(left_child, start, mid, left, right);
        QResult right_child_result = DoQuery(right_child, mid + 1, end, left, right);

        return _queryOperation.Invoke(left_child_result, right_child_result);
    }

    public void UpdateRange(int left, int right, T value) => DoUpdateRange(1, 0, _arraySize - 1, left, right, value);

    private void DoUpdateRange(int node, int start, int end, int left, int right, T value)
    {
        DoLazyUpdate(node, start, end);
    
        // Invalid Range
        if (right < start || end < left)
            return;

        int left_child = LeftChildNode(node);
        int right_child = RightChildNode(node);

        // In Range
        if (left <= start && end <= right)
        {
            _tree[node] = _updateOperation(_tree[node], value, end - start + 1);

            if (start != end) // Not a leaf node
            {
                _lazy[left_child] = _updateOperation(_lazy[left_child], value);
                _lazy[right_child] = _updateOperation(_lazy[right_child], value);
            }

            return;
        }

        // Partially In Range
        int mid = (start + end) / 2;
        DoUpdateRange(left_child, start, mid, left, right, value);
        DoUpdateRange(right_child, mid + 1, end, left, right, value);
        _tree[node] = _queryOperation(_tree[left_child], _tree[right_child]);
    }

    private static int LeftChildNode(int node) => node * 2;
    private static int RightChildNode(int node) => node * 2 + 1;
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
public long NextLong(){var(v,n,r)=(0L,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#endif
}
