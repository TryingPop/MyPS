using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 12
이름 : 배성훈
내용 : 배열의 힘
    문제번호 : 8462번

    mo's 알고리즘 문제다
    n * n + 2 n + 1 = (n + 1) * (n + 1)
    와 mo's 알고리즘을 이용하니 풀렸다
*/

namespace BaekJoon._53
{
    internal class _53_10
    {

        static void Main10(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m, sqrt;
            int[] arr, cnt;
            (int s, int e, int idx)[] queries;
            long[] ret;
            long temp;

            Solve();

            void Solve()
            {

                Input();

                Query();

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 0; i < m; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }
                sw.Close();
            }

            void Query()
            {

                Array.Sort(queries, (x, y) => QueryComp(ref x, ref y));

                ret = new long[m];
                cnt = new int[1_000_001];

                int l = queries[0].s;
                int r = queries[0].e;

                temp = 0;

                for (int i = l; i <= r; i++)
                {

                    Add(arr[i]);
                }

                ret[queries[0].idx] = temp;

                for (int i = 1; i < m; i++)
                {

                    while (queries[i].s < l) Add(arr[--l]);
                    while (queries[i].e > r) Add(arr[++r]);
                    while (queries[i].s > l) Sub(arr[l++]);
                    while (queries[i].e < r) Sub(arr[r--]);

                    ret[queries[i].idx] = temp;
                }
            }

            void Add(int _n)
            {

                cnt[_n]++;
                long k = 2 * cnt[_n] - 1;
                temp += k * _n;
            }

            void Sub(int _n)
            {

                long k = 2 * cnt[_n] - 1;
                temp -= k * _n;
                cnt[_n]--;
            }

            int QueryComp(ref (int s, int e, int idx) _q1, ref (int s, int e, int idx) _q2)
            {

                if (_q1.s / sqrt != _q2.s / sqrt) return _q1.s.CompareTo(_q2.s);
                return _q1.e.CompareTo(_q2.e);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                queries = new (int s, int e, int idx)[m];
                for (int i = 0; i < m; i++)
                {

                    queries[i] = (ReadInt() - 1, ReadInt() - 1, i);
                }

                sqrt = (int)Math.Sqrt(n);
                sr.Close();
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

#if other
using System;
using System.IO;
using System.Linq;

#nullable disable

public class ArrayPowerMosState : IMosState<int, long>
{
    private int[] _count;
    private long _power;

    public ArrayPowerMosState(int maxVal)
    {
        _count = new int[1 + maxVal];
    }

    public void Add(int value)
    {
        var c = _count[value]++;
        _power += (2L * c + 1) * value;
    }
    public void Remove(int value)
    {
        var c = _count[value]--;
        _power += (-2L * c + 1) * value;
    }

    public long Query()
    {
        return _power;
    }
}

public interface IMosState<TAddRemove, TQuery>
{
    TQuery Query();
    void Add(TAddRemove value);
    void Remove(TAddRemove value);
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nt = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (n, t) = (nt[0], nt[1]);

        var arr = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var queries = new (int idx, int l, int r, long power)[t];
        for (var idx = 0; idx < t; idx++)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            queries[idx] = (idx, l[0] - 1, l[1] - 1, 0);
        }

        var mos = new ArrayPowerMosState(arr.Max());
        var currl = 0;
        var currr = 0;

        var sqrtn = (int)(n / Math.Sqrt(t));

        mos.Add(arr[0]);

        foreach (var (idx, l, r, _) in queries.OrderBy(q => q.l / sqrtn).ThenBy(q => q.r))
        {
            while (l < currl) mos.Add(arr[--currl]);
            while (currr < r) mos.Add(arr[++currr]);
            while (currl < l) mos.Remove(arr[currl++]);
            while (r < currr) mos.Remove(arr[currr--]);

            queries[idx].power = mos.Query();
        }

        foreach (var q in queries)
            sw.WriteLine(q.power);
    }
}

#endif
}
