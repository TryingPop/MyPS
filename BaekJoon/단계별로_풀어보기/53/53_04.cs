using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 6
이름 : 배성훈
내용 : XOR
    문제번호 : 12844번

    느리게 갱신되는 세그먼트 트리 문제다
    XOR 연산의 성질을 알아야한다

    먼저 XOR에 익숙치 않기에 연산순서를 바꿔도 되는지 확인했다
    각 자리수에 대해 진행하기에 1, 0에 한해서
    처럼 1, 0로 이루어진 3개의 변수에한해 연산순서를 바꿔도 되는지 확인했다
        1 ^ (0 ^ 1) = (1 ^ 0) ^ 1 = 0
    위 식은 하나의 예이다 총 8가지 경우를 일일히 확인했다
    결과는 성립한다! 그리고 0, 1결과를 내기에 잘 정의되었음만 확인했다
    
    다음으로 교환법칙이 성립하는지 확인했다
        8가지 경우를 일일히 확인하기가 싫어
        해당 방법은 그냥 밴다이어 그램을 그려서 색칠되는 영역으로 확인했다
    교환법칙도 성립한다

    이로써 (a ^ b) ^ (a ^ c) = a ^ a ^ (b ^ c) 로 해줄 수 있다
    즉, 세그먼트 트리에 길이만큼 XOR을 해주면 된다

    그런데 XOR연산 중 a ^ a = 0을 확인했고
    개수가 짝수면 누적 xor연산을 하지 않고
    홀수인 경우만 1번 해주면 된다

    이렇게 세그먼트 트리를 작성해 제출하니 900ms에 이상없이 통과했다
*/

namespace BaekJoon._53
{
    internal class _53_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            (int n, int lazy)[] seg;
            int n;

            Solve();

            void Solve()
            {

                Init();

                int len = ReadInt();
                int START = 0;
                int END = n - 1;

                for (int i = 0; i < len; i++)
                {

                    int op = ReadInt();

                    int f = ReadInt();
                    int b = ReadInt();

                    if (op == 1)
                    {

                        int val = ReadInt();
                        Update(START, END, f, b, val);
                    }
                    else
                    {

                        int ret = GetVal(START, END, f, b);
                        sw.Write($"{ret}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new (int n, int lazy)[1 << log];

                for (int i = 0; i < n; i++)
                {

                    int val = ReadInt();
                    Update(0, n - 1, i, i, val);
                }
            }

            bool ChkXOR(int _s, int _e)
            {

                return ((_e - _s) & 1) == 0;
            }

            void LazyUpdate(int _s, int _e, int _idx)
            {

                int lazy = seg[_idx].lazy;
                if (lazy == 0) return;

                seg[_idx].lazy = 0;
                if (ChkXOR(_s, _e)) seg[_idx].n ^= lazy;

                if (_s == _e) return;

                seg[_idx * 2 + 1].lazy ^= lazy;
                seg[_idx * 2 + 2].lazy ^= lazy;
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_e < _chkS || _chkE < _s) return 0;
                if (_chkS <= _s && _e <= _chkE) return seg[_idx].n;

                int mid = (_s + _e) >> 1;

                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    ^ GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
            }

            void Update(int _s, int _e, int _chkS, int _chkE, int _val, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);
                if (_e < _chkS || _chkE < _s) return;

                if (_chkS <= _s && _e <= _chkE)
                {

                    if (ChkXOR(_s, _e)) seg[_idx].n ^= _val;

                    if (_s != _e)
                    {

                        seg[_idx * 2 + 1].lazy ^= _val;
                        seg[_idx * 2 + 2].lazy ^= _val;
                    }

                    return;
                }

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);

                seg[_idx].n = seg[_idx * 2 + 1].n ^ seg[_idx * 2 + 2].n;
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
        }
    }

#if other1
var reader = new Reader();
var n = reader.NextInt();

var array = new int[n];
for (int i = 0; i < n; i++)
    array[i] = reader.NextInt();

var segmentTree = new LazySegmentTree(array);

var m = reader.NextInt();
using (var w = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
while (m -- > 0)
{
    var q = reader.NextInt();
    if (q == 1)
        segmentTree.UpdateRange(reader.NextInt(), reader.NextInt(), reader.NextInt());
    else
        w.WriteLine(segmentTree.Query(reader.NextInt(), reader.NextInt()));
}

class LazySegmentTree
{
    int _arraySize;
    int _treeSize;
    int[] _tree;
    int[] _lazy;

    public LazySegmentTree(int[] array)
    {
        int height = (int)Math.Ceiling(Math.Log2(array.Length)) + 1;

        _arraySize = array.Length;
        _treeSize = 1 << height;
        _tree = new int[_treeSize];
        _lazy = new int[_treeSize];

        DoBuild(array, 1, 0, _arraySize - 1);
    }

    private void DoBuild(int[] array, int node, int start, int end)
    {
        if (start == end)
        {
            _tree[node] = array[start];
            return;
        }

        int left_child = LeftChildNode(node);
        int right_child = RightChildNode(node); 
        int mid = (start + end) / 2;
        DoBuild(array, left_child, start, mid);
        DoBuild(array, right_child, mid + 1, end);
        _tree[node] = _tree[left_child] ^ _tree[right_child];
    }

    private void DoLazyUpdate(int node, int start, int end)
    {
        if (_lazy[node] == 0)
            return;

        _tree[node] = (end - start + 1) % 2 == 0 ? _tree[node] : (_tree[node] ^ _lazy[node]);
        if (start != end) // Not a leaf node
        {
            int left_child = LeftChildNode(node);
            int right_child = RightChildNode(node);

            _lazy[left_child] = _lazy[left_child] ^ _lazy[node];
            _lazy[right_child] = _lazy[right_child] ^ _lazy[node];
        }

        _lazy[node] = 0;
    }

    public int Query(int left, int right) => DoQuery(1, 0, _arraySize - 1, left, right);

    private int DoQuery(int node, int start, int end, int left, int right)
    {
        DoLazyUpdate(node, start, end);

        // Invalid Range
        if (right < start || end < left)
            return 0;

        // In Range
        if (left <= start && end <= right)
            return _tree[node];

        // Partially In Range
        int left_child = LeftChildNode(node);
        int right_child = RightChildNode(node); 
        int mid = (start + end) / 2;
        int left_child_result = DoQuery(left_child, start, mid, left, right);
        int right_child_result = DoQuery(right_child, mid + 1, end, left, right);

        return left_child_result ^ right_child_result;
    }

    public void UpdateRange(int left, int right, int value) => DoUpdateRange(1, 0, _arraySize - 1, left, right, value);

    private void DoUpdateRange(int node, int start, int end, int left, int right, int value)
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
            _tree[node] = (end - start + 1) % 2 == 0 ? _tree[node] : (_tree[node] ^ value);

            if (start != end) // Not a leaf node
            {
                _lazy[left_child] = _lazy[left_child] ^ value;
                _lazy[right_child] = _lazy[right_child] ^ value;
            }

            return;
        }

        // Partially In Range
        int mid = (start + end) / 2;
        DoUpdateRange(left_child, start, mid, left, right, value);
        DoUpdateRange(right_child, mid + 1, end, left, right, value);
        _tree[node] = _tree[left_child] ^ _tree[right_child];
    }

    private static int LeftChildNode(int node) => node * 2;
    private static int RightChildNode(int node) => node * 2 + 1;
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#elif other2
//10999
StreamReader sr = new StreamReader(Console.OpenStandardInput());
StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
int len = int.Parse(sr.ReadLine()!)-1, questCount;
int[] order;
int[] nums = Array.ConvertAll(sr.ReadLine()!.Split(), int.Parse), tree = new int[nums.Length * 4], lazy = new int[tree.Length];
MakeTree(0, len);
questCount = int.Parse(sr.ReadLine()!);
for (int i = questCount; i > 0; i--)
{
    order = Array.ConvertAll(sr.ReadLine()!.Split(), int.Parse);
    switch (order[0])
    {
        default:
            ChangeValue(0, len, order[1], order[2], order[3]);
            break;
        case 2:
            sw.WriteLine(FindValue(0, len, order[1], order[2]));
            break;
    }
}
//sw.WriteLine("tree : " + string.Join(" ", tree));
//sw.WriteLine("lazy : " + string.Join(" ", lazy));
sr.Close();
sw.Close();
void ChangeValue(int left, int right, int rangeLeft, int rangeRight, int changeNum, int node = 1)
{

    if (node >= lazy.Length || right < rangeLeft || rangeRight < left)//포함 x
        return;
    if (left >= rangeLeft && right <= rangeRight)//현재 범위가 전부 포함
        lazy[node] ^= changeNum;
    else//일부 포함
    {
        int mid = left + (right - left) / 2;

        if (left <= rangeLeft)
        {
            if (right >= rangeRight)//더하는 범위가 현재 범위 안에 있음
            {
                tree[node] ^= (rangeRight - rangeLeft + 1) % 2 == 1 ? changeNum : 0;
            }
            else//더하는 범위의 왼쪽만 현재 범위 안에 있음
            {
                tree[node] ^= (right - rangeLeft + 1) % 2 == 1 ? changeNum : 0;
            }
        }
        else if (right >= rangeRight)//더하는 범위의 오른쪽만 현재 범위 안에 있음
        {
            tree[node] ^= (rangeRight - left + 1) % 2 == 1 ? changeNum : 0;
        }
        ChangeValue(left, mid, rangeLeft, rangeRight, changeNum, node * 2);
        ChangeValue(mid + 1, right, rangeLeft, rangeRight, changeNum, node * 2 + 1);
    }
}
void Propa(int left, int right, int node)
{
    if ((right - left + 1) % 2 == 1) tree[node] ^= lazy[node];
    if(left != right)
    {
        lazy[node * 2] ^= lazy[node];
        lazy[node * 2 + 1] ^= lazy[node];
    }
    lazy[node] = 0;
}
int FindValue(int left, int right, int rangeLeft, int rangeRight, int node = 1)
{
    if (right < rangeLeft || left > rangeRight) return 0;
    //sw.WriteLine($"L {left} - R {right} RL {rangeLeft} - RR {rangeRight}, N {node}");
    Propa(left,right, node);
    if (left >= rangeLeft && right <= rangeRight) return tree[node];
    int mid = left + (right - left) / 2;
    return FindValue(left, mid, rangeLeft, rangeRight, node * 2)
        ^ FindValue(mid + 1, right, rangeLeft, rangeRight, node * 2 + 1);
}
int MakeTree(int left, int right, int node = 1)
{
    if (left == right) tree[node] = nums[left];
    else tree[node] = MakeTree(left, left + (right - left) / 2, node * 2) ^ MakeTree(left + (right - left) / 2 + 1, right, node * 2 + 1);
    return tree[node];
}
#elif other3
using System.Text;

var n = int.Parse(Console.ReadLine()!);

var arr = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

var tree = new int[n * 4];
var lazy = new int[n * 4];

Init(0, n-1, 1);
var sb = new StringBuilder();

var m = int.Parse(Console.ReadLine()!);
for (int i = 0; i < m; i++) {
    var q = Console.ReadLine()!.Split().Select(int.Parse).ToArray();
    if (q[0] == 1) {
        if (q[1] > q[2]) (q[1], q[2]) = (q[2], q[1]);
        Update(0, n-1, 1, q[1], q[2], q[3]);
    } else if (q[0] == 2) {
        if (q[1] > q[2]) (q[1], q[2]) = (q[2], q[1]);
        sb.Append(Query(0, n-1, 1, q[1], q[2]));
        sb.Append('\n');
    } else if (q[0] == 3) {
        Console.WriteLine(string.Join(" ", Enumerable.Range(0, n).Select(i => Query(0, n-1, 1, i, i)).Select(i => $"{i:b8}")));
    }
}

Console.Write(sb.ToString());


int Init(int s, int e, int node) {
    if (s == e) return tree[node] = arr[s];
    return tree[node] = Init(s, (s + e) / 2, node * 2) ^ Init((s + e) / 2 + 1, e, node * 2 + 1);
}

int Query(int s, int e, int node, int from, int to) {
    Lazy(s, e, node);
    if (e < from || s > to) return 0;
    if (from <= s && e <= to) return tree[node];

    return Query(s, (s + e) / 2, node * 2, from, to) ^ Query((s + e) / 2 + 1, e, node * 2 + 1, from, to);
}

void Update(int s, int e, int node, int from, int to, int delta) {
    Lazy(s, e, node);
    if (e < from || s > to) return;
    if (from <= s && e <= to) {
        if ((e - s + 1) % 2 == 1) tree[node] ^= delta;
        
        if (s != e) {
            lazy[node * 2] ^= delta;
            lazy[node * 2 + 1] ^= delta;
        }
        return;
    }

    Update(s, (s + e) / 2, node * 2, from, to, delta);
    Update((s + e) / 2 + 1, e, node * 2 + 1, from, to, delta);
    tree[node] = tree[node * 2] ^ tree[node * 2 + 1];
}

void Lazy(int s, int e, int node) {
    if (lazy[node] == 0) return;
    if ((e - s + 1) % 2 == 1) tree[node] ^= lazy[node];
    if (s != e) {
        lazy[node * 2] ^= lazy[node];
        lazy[node * 2 + 1] ^= lazy[node];
    }

    lazy[node] = 0;
}
#elif other4
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

#nullable disable

public class XORSeg : GenericLazySeg<int>
{
    public XORSeg(int[] init)
        : base(init)
    {
    }

    protected override int Multiply(int diff, int count)
    {
        if (count % 2 == 0)
            return 0;
        else
            return diff;
    }
    protected override int Merge(int lhs, int rhs)
    {
        return lhs ^ rhs;
    }
}
public abstract class GenericLazySeg<T>
    where T : struct
{
    private int _leafMask;
    private T?[] _tree;
    private T?[] _lazy;

    private Queue<(int pos, int stIncl, int edExcl)> _q;

    public GenericLazySeg(T[] init)
    {
        var n = init.Length;
        _leafMask = (int)BitOperations.RoundUpToPowerOf2((uint)n);
        _tree = new T?[2 * _leafMask];
        _lazy = new T?[2 * _leafMask];

        for (var idx = 0; idx < n; idx++)
            _tree[_leafMask | idx] = init[idx];

        for (var idx = _leafMask - 1; idx > 0; idx--)
            _tree[idx] = SafeMerge(_tree[2 * idx], _tree[2 * idx + 1]);

        _q = new Queue<(int pos, int stIncl, int edExcl)>();
    }

    public void RangeUpdate(int stIncl, int edExcl, T diff)
    {
        _q.Clear();
        _q.Enqueue((1, 0, _leafMask));

        while (_q.TryDequeue(out var state))
        {
            var (pos, s, e) = state;

            if (e <= stIncl || edExcl <= s)
                continue;

            if (stIncl <= s && e <= edExcl)
            {
                _lazy[pos] = SafeMerge(_lazy[pos], diff);
            }
            else
            {
                var overlap = Math.Min(e, edExcl) - Math.Max(s, stIncl);
                _tree[pos] = SafeMerge(_tree[pos], Multiply(diff, overlap));

                var mid = (s + e) / 2;
                _q.Enqueue((2 * pos, s, mid));
                _q.Enqueue((2 * pos + 1, mid, e));
            }
        }
    }

    public T Range(int stIncl, int edExcl)
    {
        _q.Clear();
        _q.Enqueue((1, 0, _leafMask));

        var rv = default(T?);

        while (_q.TryDequeue(out var state))
        {
            var (pos, s, e) = state;

            if (_lazy[pos].HasValue)
            {
                // prop lazy
                if ((pos & _leafMask) == 0)
                {
                    var l = 2 * pos;
                    var r = 2 * pos + 1;

                    _lazy[l] = SafeMerge(_lazy[l], _lazy[pos]);
                    _lazy[r] = SafeMerge(_lazy[r], _lazy[pos]);
                }

                var add = Multiply(_lazy[pos].Value, e - s);
                _tree[pos] = SafeMerge(_tree[pos], add);
                _lazy[pos] = default(T?);
            }

            if (e <= stIncl || edExcl <= s)
                continue;

            if (stIncl <= s && e <= edExcl)
            {
                rv = SafeMerge(rv, _tree[pos]);
            }
            else
            {
                var mid = (s + e) / 2;
                _q.Enqueue((2 * pos, s, mid));
                _q.Enqueue((2 * pos + 1, mid, e));
            }
        }

        return rv.Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T? SafeMerge(T? v1, T? v2)
    {
        if (!v1.HasValue)
            return v2;

        if (!v2.HasValue)
            return v1;

        return Merge(v1.Value, v2.Value);
    }

    protected abstract T Multiply(T diff, int count);
    protected abstract T Merge(T lhs, T rhs);
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var arr = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var seg = new XORSeg(arr);

        var m = Int32.Parse(sr.ReadLine());
        while (m-- > 0)
        {
            var q = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            if (q[0] == 1)
            {
                seg.RangeUpdate(q[1], q[2] + 1, q[3]);
            }
            else
            {
                sw.WriteLine(seg.Range(q[1], q[2] + 1));
            }
        }
    }
}

#endif
}
