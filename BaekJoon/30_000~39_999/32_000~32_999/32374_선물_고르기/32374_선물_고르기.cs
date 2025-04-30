using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 29
이름 : 배성훈
내용 : 선물 고르기
    문제번호 : 32374번

    그리디 문제다.
    아이디어는 단순하다.
    무조건 선물을 포장할 수 있는 상태가 주어진다.
    그래서 남은 상자 중 담을 수 있는 가장 큰 것을 고르면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1587
    {

        static void Main1587(string[] args)
        {

            int n, k;
            int[] present, bag, pick;

            Input();

            GetRet();

            void GetRet()
            {

                Comparer<int> comp = Comparer<int>.Create((x, y) => y.CompareTo(x));

                Array.Sort(present, comp);
                Array.Sort(bag, comp);
                Array.Sort(pick, comp);

                int idx = 0;
                for (int i = 0; i < n; i++)
                {

                    if (pick[idx] == bag[i])
                    {

                        idx++;
                        continue;
                    }

                    int ret = BinarySearch(bag[i]);
                    Console.Write(present[ret]);

                    break;
                }

                int BinarySearch(int _val)
                {

                    int l = 0;
                    int r = n - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;

                        if (present[mid] <= _val) r = mid - 1;
                        else l = mid + 1;
                    }

                    return r + 1;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                present = new int[n];
                bag = new int[n];
                pick = new int[k + 1];

                for (int i = 0; i < n; i++)
                {

                    present[i] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    bag[i] = ReadInt();
                }

                for (int i = 0; i < k; i++)
                {

                    pick[i] = ReadInt();
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();

                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;

                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
using ProblemSolving.Templates.SegmentTree.Impl;
using ProblemSolving.Templates.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
namespace ProblemSolving.Templates.SegmentTree.Impl {}
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Collections.Generic {}
namespace System.IO {}
namespace System.Linq {}
namespace System.Numerics {}

#nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var (n, k) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var prices = sr.ReadLine().Split(' ').Select(Int32.Parse).ToList();
        var boxes = sr.ReadLine().Split(' ').Select(Int32.Parse).ToList();
        var picked = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var boxToPrices = new Dictionary<int, List<int>>();
        var remain = new Dictionary<int, int>();

        for (var idx = 0; idx < n; idx++)
            remain[boxes[idx]] = 1 + remain.GetValueOrDefault(boxes[idx]);

        for (var idx = 0; idx < k; idx++)
            remain[picked[idx]] = -1 + remain.GetValueOrDefault(picked[idx]);

        var myboxsize = remain
            .Where(kvp => kvp.Value > 0)
            .Max(kvp => kvp.Key);

        prices.Sort();
        boxes.Remove(myboxsize);
        boxes.Sort();

        var invcount = new int[prices.Count];
        var invpsum = new long[1 + boxes.Count];
        var tpidx = 0;
        for (var idx = 0; idx < n; idx++)
        {
            while (tpidx < boxes.Count && boxes[tpidx] < prices[idx])
                tpidx++;

            invcount[idx] = tpidx;
            invpsum[tpidx]++;
        }

        for (var idx = 1; idx < invpsum.Length; idx++)
            invpsum[idx] += invpsum[idx - 1];

        var seg = new MinSegTieLeft(invpsum.Length);
        seg.Init(invpsum);

        var max = 0;
        for (var idx = 0; idx < n; idx++)
        {
            // try remove idx-th price
            var ok = true;

            if (idx > 0 && seg.Range(0, idx).val <= 0)
                ok = false;

            if (idx + 1 < invpsum.Length && seg.Range(idx + 1, invpsum.Length).val - 1 <= 0)
                ok = false;

            if (ok && prices[idx] <= myboxsize)
                max = Math.Max(max, prices[idx]);
        }

        sw.WriteLine(max);
    }
}

namespace ProblemSolving.Templates.Utility
{
    public static class DeconstructHelper
    {
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
    }
}


namespace ProblemSolving.Templates.SegmentTree.Impl
{
    public class MinSegTieLeft : MonoidSegTree<(int idx, long val), long>
    {
        public MinSegTieLeft(int size) : base(size)
        {
        }

        public void Init(long init)
        {
            var id = Identity();
            for (var idx = 0; idx < Size; idx++)
                _tree[_leafMask | idx] = (idx, init);
            for (var idx = Size; idx < _leafMask; idx++)
                _tree[_leafMask | idx] = id;

            for (var idx = _leafMask - 1; idx > 0; idx--)
                _tree[idx] = Merge(_tree[2 * idx], _tree[2 * idx + 1]);
        }
        public void Init(IList<long> init)
        {
            var id = Identity();
            for (var idx = 0; idx < init.Count; idx++)
                _tree[_leafMask | idx] = (idx, init[idx]);
            for (var idx = init.Count; idx < _leafMask; idx++)
                _tree[_leafMask | idx] = id;

            for (var idx = _leafMask - 1; idx > 0; idx--)
                _tree[idx] = Merge(_tree[2 * idx], _tree[2 * idx + 1]);
        }

        protected override (int idx, long val) Identity() => (-1, Int64.MaxValue);
        protected override (int idx, long val) Merge((int idx, long val) l, (int idx, long val) r) => l.val <= r.val ? l : r;
        protected override (int idx, long val) UpdateElement((int idx, long val) elem, long update) => (elem.idx, update);
    }
}


namespace ProblemSolving.Templates.SegmentTree
{
    public abstract class MonoidSegTree<TElement, TUpdate>
        where TElement : struct
        where TUpdate : struct
    {
        protected TElement[] _tree;
        protected int _leafMask;

        public int Size { get; private set; }

        private List<int> _lefts;
        private List<int> _rights;

        public MonoidSegTree(int size)
        {
            _leafMask = (int)BitOperations.RoundUpToPowerOf2((uint)size);
            var treeSize = _leafMask << 1;

            _lefts = new List<int>();
            _rights = new List<int>();

            _tree = new TElement[treeSize];
            Size = size;
        }

        public TElement AllRange => _tree[1];
        public TElement ElementAt(int idx) => _tree[_leafMask | idx];

        public void Init(IList<TElement> init)
        {
            var id = Identity();
            for (var idx = 0; idx < init.Count; idx++)
                _tree[_leafMask | idx] = init[idx];
            for (var idx = init.Count; idx < _leafMask; idx++)
                _tree[_leafMask | idx] = id;

            for (var idx = _leafMask - 1; idx > 0; idx--)
                _tree[idx] = Merge(_tree[2 * idx], _tree[2 * idx + 1]);
        }

        public void Update(int index, TUpdate val)
        {
            var curr = _leafMask | index;
            _tree[curr] = UpdateElement(_tree[curr], val);
            curr >>= 1;

            while (curr != 0)
            {
                _tree[curr] = Merge(_tree[2 * curr], _tree[2 * curr + 1]);
                curr >>= 1;
            }
        }

        public TElement Range(int stIncl, int edExcl)
        {
            if (stIncl >= _leafMask || edExcl > _leafMask)
                throw new ArgumentOutOfRangeException();

            var leftNode = _leafMask | stIncl;
            var rightNode = _leafMask | (edExcl - 1);

            _lefts.Clear();
            _rights.Clear();

            while (leftNode <= rightNode)
            {
                if ((leftNode & 1) == 1)
                    _lefts.Add(leftNode++);
                if ((rightNode & 1) == 0)
                    _rights.Add(rightNode--);

                leftNode >>= 1;
                rightNode >>= 1;
            }

            foreach (var idx in _rights.AsEnumerable().Reverse())
                _lefts.Add(idx);

            var aggregated = _tree[_lefts[0]];
            foreach (var idx in _lefts.Skip(1))
                aggregated = Merge(aggregated, _tree[idx]);

            return aggregated;
        }

        protected abstract TElement Identity();
        protected abstract TElement UpdateElement(TElement elem, TUpdate update);
        protected abstract TElement Merge(TElement l, TElement r);
    }
}

#endif
}
