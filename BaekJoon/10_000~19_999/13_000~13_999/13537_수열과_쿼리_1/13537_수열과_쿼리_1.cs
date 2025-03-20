using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 2
이름 : 배성훈
내용 : 수열과 쿼리 1
    문제번호 : 13537번

    세그먼트 트리, 머지 소트 트리 문제다.
    머지 소트 트리는 찾아보니 세그먼트 트리인데, 
    리스트를 원소로 갖는다.
    오프라인 쿼리로 해결가능하기에 오프라인 쿼리로 해결하고
    비슷한 다른 문제에서 머지소트 트리로 풀어볼 생각이다.

    아이디어는 다음과 같다.
    해당 구간에서 k 초과인 원소를 찾는 것은
    k 초과인 원소들을 기록하고 현재 입력된 갯수를 찾는 것과 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1240
    {

        static void Main1240(string[] args)
        {

            int n, m;
            int[] seg;
            List<(int s, int e, int k, int idx)> queries;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return 0;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                int mid = (_s + _e) >> 1;

                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
            }

            void Update(int _s, int _e, int _chk, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] = 1;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chk) Update(mid + 1, _e, _chk, _idx * 2 + 2);
                else Update(_s, mid, _chk, _idx * 2 + 1);

                seg[_idx] = seg[_idx * 2 + 1] + seg[_idx * 2 + 2];
            }

            void SetArr()
            {

                int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9)) + 1;
                seg = new int[1 << log];

                // k 보다 큰 원소를 범위에서 찾기에
                // k를 기준으로 정렬한다.
                queries.Sort((x, y) =>
                {

                    int ret = y.k.CompareTo(x.k);
                    if (ret == 0) ret = x.s.CompareTo(y.s);
                    return ret;
                });
            }

            void GetRet()
            {

                int S = 1;
                int E = n;

                int[] ret = new int[m]; 
                
                for (int i = 0; i < queries.Count; i++)
                {

                    // k 이상인 원소를 기록한다.
                    if (queries[i].s == -1)
                        Update(S, E, queries[i].idx);
                    // k 초과인 원소의 개수를 범위에서 찾는다.
                    else
                    {

                        int cnt = GetVal(S, E, queries[i].s, queries[i].e);
                        ret[queries[i].idx] = cnt;
                    }
                }
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 0; i < m; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                queries = new (200_000);

                for (int i = 1; i <= n; i++)
                {

                    // 입력 쿼리
                    // 입력 쿼리는 시작이 -1로 구분
                    queries.Add((-1, -1, ReadInt(), i));
                }

                m = ReadInt();
                for (int i = 0; i < m; i++)
                {

                    // 찾는 쿼리
                    queries.Add((ReadInt(), ReadInt(), ReadInt() + 1, i));
                }

                sr.Close();

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
    }

#if other
using System;
using System.IO;
using System.Linq;

public class MergeTree
{
    private int _elementCount;
    private int _leafPrefix;
    public int[][] _tree;

    public MergeTree(int[] values)
    {
        _elementCount = values.Length;

        _leafPrefix = 1 << (int)(Math.Ceiling(Math.Log2(_elementCount)));
        _tree = new int[2 * _leafPrefix][];

        Init(values);
    }

    private void Init(int[] values)
    {
        for (var idx = 0; idx < values.Length; idx++)
        {
            _tree[_leafPrefix | idx] = new int[1];
            _tree[_leafPrefix | idx][0] = values[idx];
        }

        for (var idx = _leafPrefix - 1; idx > 0; idx--)
        {
            var left = _tree[2 * idx];
            var right = _tree[2 * idx + 1];

            if (left == null || right == null)
            {
                _tree[idx] = left ?? right;
            }
            else
            {
                _tree[idx] = new int[left.Length + right.Length];
                Merge(left, right, _tree[idx]);
            }
        }
    }

    public void UpdateElem(int node, int newValue)
    {
        var idx = _leafPrefix | node;
        _tree[idx][0] = newValue;

        for (idx /= 2; idx > 0; idx /= 2)
        {
            var left = _tree[2 * idx];
            var right = _tree[2 * idx + 1];

            if (left == null || right == null)
            {
                _tree[idx] = left ?? right;
            }
            else
            {
                Merge(left, right, _tree[idx]);
            }
        }
    }

    public int Query(int startInclusive, int endInclusive, int k)
    {
        var l = startInclusive | _leafPrefix;
        var r = endInclusive | _leafPrefix;

        var sum = 0;
        while (l <= r)
        {
            if (l % 2 == 1)
                sum += CountLarger(_tree[l++], k);
            if (r % 2 == 0)
                sum += CountLarger(_tree[r--], k);

            l >>= 1;
            r >>= 1;
        }

        return sum;
    }

    private int CountLarger(int[] arr, int threshold)
    {
        var l = 0;
        var r = arr.Length;

        while (l < r)
        {
            var mid = (l + r) / 2;
            if (threshold >= arr[mid])
                l = mid + 1;
            else
                r = mid;
        }

        return arr.Length - l;
    }

    private void Merge(int[] l, int[] r, int[] arr)
    {
        var x = 0;
        var y = 0;
        var idx = 0;

        while (x < l.Length && y < r.Length)
            if (l[x] < r[y])
                arr[idx++] = l[x++];
            else
                arr[idx++] = r[y++];

        while (x < l.Length)
            arr[idx++] = l[x++];

        while (y < r.Length)
            arr[idx++] = r[y++];
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

        var tree = new MergeTree(a);

        var m = Int32.Parse(sr.ReadLine());
        for (var idx = 0; idx < m; idx++)
        {
            var ijk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var i = ijk[0];
            var j = ijk[1];
            var k = ijk[2];

            sw.WriteLine(tree.Query(i - 1, j - 1, k));
        }
    }
}
#endif
}
