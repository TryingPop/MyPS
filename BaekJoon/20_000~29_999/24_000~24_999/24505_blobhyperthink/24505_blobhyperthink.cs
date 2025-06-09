using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 9
이름 : 배성훈
내용 : blobhyperthink
    문제번호 : 24505번

    세그먼트 트리, dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1415
    {

        static void Main1415(string[] args)
        {

            int MOD = 1_000_000_007;
            int n;
            int[] arr;
            int[][] seg;

            Input();

            SetSeg();

            GetRet();

            void GetRet()
            {

                if (n < 11)
                {

                    Console.Write(0);
                    return;
                }

                int S = 0;
                int E = n;
                for (int i = 0; i < n; i++)
                {

                    Update(0, S, E, arr[i], 1);
                    for (int j = 1; j < 11; j++)
                    {

                        int val = GetVal(j - 1, S, E, arr[i] - 1);
                        Update(j, S, E, arr[i], val);
                    }
                }

                Console.Write(GetVal(10, S, E, n));

                int GetVal(int _f, int _s, int _e, int _chk, int _idx = 0)
                {

                    if (_e <= _chk) return seg[_f][_idx];
                    else if (_chk < _s) return 0;

                    int mid = (_s + _e) >> 1;

                    return (GetVal(_f, _s, mid, _chk, _idx * 2 + 1) + GetVal(_f, mid + 1, _e, _chk, _idx * 2 + 2)) % MOD;
                }

                void Update(int _f, int _s, int _e, int _chk, int _val, int _idx = 0)
                {

                    if (_s == _e)
                    {

                        seg[_f][_idx] = (seg[_f][_idx] + _val) % MOD;
                        return;
                    }

                    int mid = (_s + _e) >> 1;
                    if (_chk <= mid) Update(_f, _s, mid, _chk, _val, _idx * 2 + 1);
                    else Update(_f, mid + 1, _e, _chk, _val, _idx * 2 + 2);

                    seg[_f][_idx] = (seg[_f][_idx * 2 + 1] + seg[_f][_idx * 2 + 2]) % MOD;
                }
            }

            void SetSeg()
            {

                int log = n == 1 ? 1 : (int)Math.Log2(n) + 2;

                seg = new int[12][];
                for (int i = 0; i < 12; i++)
                {

                    seg[i] = new int[1 << log];
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
                        if (c == ' ' || c == '\n') return true;
                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

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


public sealed class SumSeg
{
    private long[] _tree;
    private int _leafMask;

    public SumSeg(int size)
    {
        var initSizeLog = 1 + BitOperations.Log2((uint)size - 1);

        _leafMask = 1 << initSizeLog;
        var treeSize = 2 * _leafMask;

        _tree = new long[treeSize];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Increase(int index, long diff)
    {
        var mod = 1_000_000_007;
        var curr = _leafMask | index;

        while (curr != 0)
        {
            _tree[curr] = (_tree[curr] + diff + mod) % mod;
            curr >>= 1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Range(int stIncl, int edExcl)
    {
        var mod = 1_000_000_007;
        var leftNode = _leafMask | stIncl;
        var rightNode = _leafMask | (edExcl - 1);

        var aggregated = 0L;
        while (leftNode <= rightNode)
        {
            if ((leftNode & 1) == 1)
                aggregated = (aggregated + _tree[leftNode++]) % mod;
            if ((rightNode & 1) == 0)
                aggregated = (aggregated + _tree[rightNode--]) % mod;

            leftNode >>= 1;
            rightNode >>= 1;
        }

        return aggregated;
    }
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var a = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var segs = new SumSeg[11];
        var maxa = 1 + a.Max();

        for (var idx = 0; idx < segs.Length; idx++)
            segs[idx] = new SumSeg(1 + maxa);

        foreach (var v in a.Reverse())
        {
            for (var idx = 0; idx < segs.Length - 1; idx++)
            {
                var count = segs[idx + 1].Range(v + 1, maxa);
                segs[idx].Increase(v, count);
            }

            segs[^1].Increase(v, 1);
        }

        sw.WriteLine(segs[0].Range(0, maxa));
    }
}

#endif
}
