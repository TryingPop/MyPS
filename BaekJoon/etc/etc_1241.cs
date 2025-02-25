using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 3
이름 : 배성훈
내용 : 수열과 쿼리 3
    문제번호 : 13544번

    머지 소트 트리 문제다.
    세그먼트 트리와 비슷한데, 값이 배열이다.

    해당 범위의 값들을 모두 저장하고, 배열로 정렬한다.
    이후 해당 범위에 진입하면 이분 탐색으로 초과인 원소의 갯수를 찾는다.
*/

namespace BaekJoon.etc
{
    internal class etc_1241
    {

        static void Main1241(string[] args)
        {

            StreamReader sr;

            int S, E;
            int n;
            List<int>[] mer;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 0; i < mer.Length; i++) mer[i]?.Sort();

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int m = ReadInt();
                int ret = 0;

                for (int i = 0; i < m; i++)
                {

                    int a = ReadInt() ^ ret;
                    int b = ReadInt() ^ ret;
                    int c = ReadInt() ^ ret;

                    ret = GetVal(S, E, a, b, c);
                    sw.Write($"{ret}\n");
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9)) + 1;
                mer = new List<int>[1 << log];
                S = 1;
                E = n;

                for (int i = 1; i <= n; i++)
                {

                    Update(S, E, i, ReadInt());
                }
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _k, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return 0;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    int cnt = BinarySearch(_k);
                    return mer[_idx].Count - cnt;
                }

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chkS, _chkE, _k, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chkS, _chkE, _k, _idx * 2 + 2);

                int BinarySearch(int _chk)
                {

                    int l = 0;
                    int r = mer[_idx].Count - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (mer[_idx][mid] <= _chk) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    mer[_idx] ??= new(_e - _s + 1);
                    mer[_idx].Add(_val);
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chk) Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);
                else Update(_s, mid, _chk, _val, _idx * 2 + 1);

                mer[_idx] ??= new(_e - _s + 1);
                mer[_idx].Add(_val);
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

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

#if other
using System;
using System.IO;
using System.Linq;
using System.Numerics;

// #nullable disable

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

public class MergeSortTree
{
    private int _leafMask;
    private int[][] _tree;

    public MergeSortTree(int[] arr)
    {
        _leafMask = (int)BitOperations.RoundUpToPowerOf2((uint)arr.Length);
        _tree = new int[_leafMask << 1][];

        for (var idx = 0; idx < arr.Length; idx++)
            _tree[_leafMask | idx] = new[] { arr[idx] };

        for (var idx = _leafMask - 1; idx >= 1; idx--)
        {
            var l = _tree[2 * idx];
            var r = _tree[2 * idx + 1];

            if (l == null && r == null)
                continue;

            if (r == null)
                _tree[2 * idx] = l;
            else
                _tree[idx] = Merge(l, r);
        }
    }

    private int[] Merge(int[] l, int[] r)
    {
        var res = new int[l.Length + r.Length];
        var lidx = 0;
        var ridx = 0;

        while (lidx < l.Length && ridx < r.Length)
            if (l[lidx] < r[ridx])
            {
                res[lidx + ridx] = l[lidx];
                lidx++;
            }
            else
            {
                res[lidx + ridx] = r[ridx];
                ridx++;
            }

        while (lidx < l.Length)
        {
            res[lidx + ridx] = l[lidx];
            lidx++;
        }
        while (ridx < r.Length)
        {
            res[lidx + ridx] = r[ridx];
            ridx++;
        }

        return res;
    }

    public int Count(int stIncl, int edExcl, int minIncl)
    {
        var leftNode = _leafMask | stIncl;
        var rightNode = _leafMask | (edExcl - 1);

        var count = 0;
        while (leftNode <= rightNode)
        {
            if ((leftNode & 1) == 1)
            {
                count += _tree[leftNode].Length - LowerBound(_tree[leftNode], minIncl);
                leftNode++;
            }
            if ((rightNode & 1) == 0)
            {
                count += _tree[rightNode].Length - LowerBound(_tree[rightNode], minIncl);
                rightNode--;
            }

            leftNode /= 2;
            rightNode /= 2;
        }

        return count;
    }

    private static int LowerBound(int[] arr, int v)
    {
        var lo = 0;
        var hi = arr.Length;

        while (lo < hi)
        {
            var mid = (lo + hi) / 2;
            if (arr[mid] < v)
                lo = mid + 1;
            else
                hi = mid;
        }

        return lo;
    }
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var arr = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var mst = new MergeSortTree(arr);
        var lastans = 0;

        var q = Int32.Parse(sr.ReadLine());
        while (q-- > 0)
        {
            var (a, b, c) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            (a, b, c) = (a ^ lastans, b ^ lastans, c ^ lastans);

            lastans = mst.Count(a - 1, b, c + 1);
            sw.WriteLine(lastans);
        }
    }
}

#endif
}
