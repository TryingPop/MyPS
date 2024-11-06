using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 11
이름 : 배성훈
내용 : 수열과 쿼리 6
    문제번호 : 13548번

    mo's 문제다
    인덱스 에러로 많이 틀렸다
    아직 mo's를 사용하는게 미숙하다;
    n을 대상으로 root를 해야하는데, 
    m을 대상으로 하니 배 가까이 시간이 걸리기도 한다

    아이디어는 다음과 같다
    mo's로 정렬한 뒤 조사한다
    처음에는 완전히 조사하고 이후에는 증감값 만큼 변형시켜 조사한다

    앞에서는 서로 다른 개수를 세면 되었으나 여기서는
    중복 갯수 최댓값으로 찾으면 된다
    그래서 수에 따른 센 갯수, 그리고 중복에 따른 최대값을 세어 결과를 갱신했다
    여기서 Add 먼저하고 Sub를 뒤에 해야한다
    그래야 인덱스 에러가 발생하지 않는다!
*/

namespace BaekJoon._53
{
    internal class _53_09
    {

        static void Main9(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int sqrt;
            int n, m;
            int[] arr, ret, nCnt, dCnt;
            int temp;
            (int s, int e, int idx)[] queries;

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
                ret = new int[m];

                nCnt = new int[100_001];
                dCnt = new int[n + 1];

                int l = queries[0].s, r = queries[0].e;
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

            int QueryComp(ref (int s, int e, int idx) _query1, ref (int s, int e, int idx) _query2)
            {

                if (_query1.s / sqrt != _query2.s / sqrt) return _query1.s.CompareTo(_query2.s);
                return _query1.e.CompareTo(_query2.e);
            }

            void Add(int _n)
            {

                if (nCnt[_n] > 0) dCnt[nCnt[_n]]--;
                nCnt[_n]++;
                dCnt[nCnt[_n]]++;
                temp = Math.Max(temp, nCnt[_n]);
            }

            void Sub(int _n)
            {

                dCnt[nCnt[_n]]--;
                if (nCnt[_n] == temp && dCnt[nCnt[_n]] == 0) temp--;
                nCnt[_n]--;
                dCnt[nCnt[_n]]++;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n + 1];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                m = ReadInt();
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
using System.Runtime.CompilerServices;

#nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var sqrtn = (int)Math.Sqrt(n);
        var a = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var q = Int32.Parse(sr.ReadLine());
        var queries = new (int idx, int l, int r)[q];
        for (var idx = 0; idx < q; idx++)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            queries[idx] = (idx, l[0] - 1, l[1] - 1);
        }

        var currl = 0;
        var currr = 0;

        var numberToOccurance = new int[a.Max() + 1];
        var occuranceToCount = new int[1 + n];
        var maxOccur = 0;
        var queryValues = new int[q];

        occuranceToCount[0] = n;
        Add(numberToOccurance, occuranceToCount, ref maxOccur, a[0]);

        foreach (var (idx, l, r) in queries.OrderBy(q => q.l / sqrtn).ThenBy(q => q.r))
        {
            while (l < currl) Add(numberToOccurance, occuranceToCount, ref maxOccur, a[--currl]);
            while (currr < r) Add(numberToOccurance, occuranceToCount, ref maxOccur, a[++currr]);
            while (currl < l) Remove(numberToOccurance, occuranceToCount, ref maxOccur, a[currl++]);
            while (r < currr) Remove(numberToOccurance, occuranceToCount, ref maxOccur, a[currr--]);

            queryValues[idx] = maxOccur;
        }

        foreach (var v in queryValues)
            sw.WriteLine(v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Add(int[] numberToOccurance, int[] occuranceToNumber, ref int maxOccur, int v)
    {
        var occ = numberToOccurance[v];
        numberToOccurance[v]++;

        occuranceToNumber[occ]--;
        occuranceToNumber[occ + 1]++;

        if (maxOccur == occ)
            maxOccur = occ + 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Remove(int[] numberToOccurance, int[] occuranceToNumber, ref int maxOccur, int v)
    {
        var occ = numberToOccurance[v];
        numberToOccurance[v]--;

        occuranceToNumber[occ]--;
        occuranceToNumber[occ - 1]++;

        if (occuranceToNumber[maxOccur] == 0)
            maxOccur--;
    }
}

#elif other2
#endif
}
