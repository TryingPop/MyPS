using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 3
이름 : 배성훈
내용 : 두 섬간의 이동
    문제번호 : 10888번

    분리집합, 수학 문제다.
    그룹별 조합을 찾아야함을 계산안해 여러 번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1245
    {

        static void Main1245(string[] args)
        {

            StreamReader sr;

            int n;
            int[] group;
            int[] cnt;
            int[] stk;


            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                long ret1 = 0, ret2 = 0;
                for (int i = 1; i < n; i++)
                {

                    int num = ReadInt();

                    int f = Find(num);
                    int t = Find(num + 1);

                    Union(f, t);
                }

                sr.Close();

                void Union(int _f, int _t)
                {

                    group[_t] = group[_f];
                    ret1 -= GetComb(cnt[_f]) + GetComb(cnt[_t]);
                    ret2 -= CntBridge(cnt[_f]) + CntBridge(cnt[_t]);
                    cnt[_f] += cnt[_t];
                    ret1 += GetComb(cnt[_f]);
                    ret2 += CntBridge(cnt[_f]);

                    sw.Write($"{ret1} {ret2}\n");
                }

                long GetComb(long _n)
                {

                    return (_n * (_n - 1)) / 2;
                }

                long CntBridge(long _n)
                {

                    return ((_n - 1) * _n * (_n + 1)) / 6;
                }

                int Find(int _chk)
                {

                    int len = 0;
                    while (group[_chk] != _chk)
                    {

                        stk[len++] = _chk;
                        _chk = group[_chk];
                    }

                    while (len-- > 0)
                    {

                        group[stk[len]] = _chk;
                    }

                    return _chk;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                

                n = ReadInt();
                group = new int[n + 1];
                cnt = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                    cnt[i] = 1;
                }

                stk = new int[n];
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

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        if (_set[v] == _set[_set[v]])
            return _set[v];

        var root = v;

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

    public int Rank(int v) => _rank[Find(v)];
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var uf = new UnionFind(n);

        var count = 0L;
        var distsum = 0L;

        for (var idx = 0; idx < n - 1; idx++)
        {
            var v = Int32.Parse(sr.ReadLine()) - 1;

            var lrank = uf.Rank(v);
            var rrank = uf.Rank(v + 1);
            uf.Union(v, v + 1);

            var urank = uf.Rank(v);

            count += Count(urank) - Count(lrank) - Count(rrank);
            distsum += DistSum(urank) - DistSum(lrank) - DistSum(rrank);

            sw.WriteLine($"{count} {distsum}");
        }
    }

    private static long DistSum(long n)
    {
        // 1*(n-1)+2*(n-2)+3*(n-3)+..+n*(n-n)
        // = (1+2+...+n-1+n)*n - (1*1+2*2+...+(n-1)*(n-1))

        return n * (n + 1) / 2 * n - n * (n + 1) * (2 * n + 1) / 6;
    }

    private static long Count(long n)
    {
        return n * (n - 1) / 2;
    }
}
#endif
}
