using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 16
이름 : 배성훈
내용 : K번째 수
    문제번호 : 7469번

    머지 소트 트리 문제다.
    etc_1406인 백설공주와 난쟁이 문제를 풀기전
    머지 소트 트리 개념을 상기할 목적으로 푼 문제다.

    아이디어는 다음과 같다.
    K번째 수는 해당 구간에 해당 수 이하가 몇 개 있는지 확인해서 구한다.
    즉 이분 탐색(매개 변수 탐색)알고리즘을 이용해 구한다.
    그래서 k개 이상이 되는 가장 작은 수를 찾으면 해당 수가 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1410
    {

        static void Main1410(string[] args)
        {

            int S, E;
            int OFFSET = 1_000_000_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;
            int[] arr;
            List<int>[] mergeSeg;

            Input();

            SetSeg();

            GetRet();

            void GetRet()
            {
                
                while (m-- > 0)
                {

                    int i = ReadInt() - 1;
                    int j = ReadInt() - 1;
                    int k = ReadInt();

                    long l = 0, r = 2_000_000_000;
                    while (l <= r)
                    {

                        long mid = (l + r) >> 1;

                        if (GetCnt(S, E, i, j, mid) < k) l = mid + 1;
                        else r = mid - 1;
                    }

                    sw.Write($"{l - OFFSET}\n");
                }

                int GetCnt(int _s, int _e, int _chkS, int _chkE, long _val, int _idx = 0)
                {

                    if (_chkE < _s || _e < _chkS) return 0;
                    else if (_chkS <= _s && _e <= _chkE)
                        return LowCnt();

                    int mid = (_s + _e) >> 1;
                    return GetCnt(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1)
                        + GetCnt(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);

                    int LowCnt()
                    {

                        List<int> list = mergeSeg[_idx];

                        int l = 0;
                        int r = list.Count - 1;

                        while (l <= r)
                        {

                            int mid = (l + r) >> 1;
                            if (list[mid] <= _val) l = mid + 1;
                            else r = mid - 1;
                        }

                        return l;
                    }
                }
            }

            void SetSeg()
            {

                int log = n == 1 ? 1 : (int)(Math.Ceiling(Math.Log2(n - 1)) + 1e-9) + 2;
                mergeSeg = new List<int>[1 << log];

                S = 0;
                E = n - 1;
                for (int i = 0; i < n; i++)
                {

                    Init(S, E, i);
                }

                void Init(int _s, int _e, int _chk, int _idx = 0)
                {

                    mergeSeg[_idx] ??= new(_e - _s + 1);
                    if (_s == _e)
                    {

                        mergeSeg[_idx].Add(arr[_chk]);
                        return;
                    }

                    int mid = (_s + _e) >> 1;
                    if (_chk <= mid) Init(_s, mid, _chk, _idx * 2 + 1);
                    else Init(mid + 1, _e, _chk, _idx * 2 + 2);

                    mergeSeg[_idx].Add(arr[_chk]);
                }

                for (int i = 0; i < mergeSeg.Length; i++)
                {

                    if (mergeSeg[i] == null) continue;
                    mergeSeg[i].Sort();
                }
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt() + OFFSET;
                }
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
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }

#if other
using ProblemSolving.Templates.Utility;
using System;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.IO {}
namespace System.Linq {}

#nullable disable

public class Bucket
{
    public int[] Arr;
    public int[] Sorted;
    public int StIncl;
    public int EdExcl;

    public Bucket(int[] arr, int stIncl, int edExcl)
    {
        Arr = arr;
        Sorted = arr.OrderBy(v => v).ToArray();
        StIncl = stIncl;
        EdExcl = edExcl;
    }

    public int CountSmallerEq(int v, int l, int r)
    {
        if (l <= StIncl && EdExcl <= r)
        {
            if (Sorted[0] > v)
                return 0;
            if (Sorted[^1] <= v)
                return Sorted.Length;

            // flo->true, fhi->false
            var lo = 0;
            var hi = Arr.Length - 1;

            while (lo + 1 < hi)
            {
                var mid = (lo + hi) / 2;
                var fmid = Sorted[mid] <= v;

                if (fmid)
                    lo = mid;
                else
                    hi = mid;
            }

            return lo + 1;
        }
        else
        {
            var st = Math.Max(l, StIncl) - StIncl;
            var ed = Math.Min(r, EdExcl) - StIncl;
            var c = 0;

            for (var idx = st; idx < ed; idx++)
                if (Arr[idx] <= v)
                    c++;

            return c;
        }
    }
}

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
        var (n, m) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var a = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var compmap = a
            .Distinct()
            .OrderBy(v => v)
            .Select((v, i) => (v, i))
            .ToDictionary(p => p.v, p => p.i);

        var comprevmap = compmap
            .OrderBy(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToArray();

        for (var idx = 0; idx < a.Length; idx++)
            a[idx] = compmap[a[idx]];

        var bucketSize = 1 + (int)Math.Sqrt(n * Math.Log2(1 + n));
        var bucketCount = (n + bucketSize - 1) / bucketSize;
        var buckets = new Bucket[bucketCount];

        for (var idx = 0; idx < bucketCount; idx++)
        {
            var slice = a.Skip(idx * bucketSize).Take(bucketSize).ToArray();
            buckets[idx] = new Bucket(slice, idx * bucketSize, idx * bucketSize + slice.Length);
        }

        while (m-- > 0)
        {
            var (l, r, k) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            l--;

            // flo->true, fhi->false
            var lo = -1;
            var hi = comprevmap.Length;

            while (lo + 1 < hi)
            {
                var mid = (lo + hi) / 2;
                var count = 0;

                foreach (var b in buckets)
                    count += b.CountSmallerEq(mid, l, r);

                var fmid = count < k;
                if (fmid)
                    lo = mid;
                else
                    hi = mid;
            }

            sw.WriteLine(comprevmap[lo + 1]);
        }
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

// This is source code merged w/ template
// Timestamp: 2024-09-10 09:50:31 UTC+9

#endif
}
