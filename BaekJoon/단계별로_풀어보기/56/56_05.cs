using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 23
이름 : 배성훈
내용 : Suffix Array
    문제번호 : 9248번

    문자열, 접미사 배열과 lcp 배열 문제다
    문자열 정렬상에 오류로 엄청나게 틀렸다;
    그냥 Array.Sort를 쓰니 해결되었다;
    원인은 down부분에 len이 길이로 봐서 생긴 오류였다
    해당 부분을 수정하니 통과했다

    우선 접미사 배열을 정렬하는 방법은 다음과 같다
    접미사 배열을 정렬한다
    
    접미사 정렬 기준을 맨 앞과, 맨앞 + d번째(2배수)의 순서 값을 비교한다
    여기서 d가 길이를 넘어가는 경우 배열의 길이로 비교한다
    
    순서 값은 초기에 문자열 값으로 잡고,
    이후는 매번 결과로 앞에서부터 1씩 매긴다

    정렬을 순서 기준이 바뀔 때마다 1번씩 하기에
    매 회당 N * log N 시간이 걸린다
    그리고 해당 과정은 접미사 배열을 벗어나면 길이로 비교하기에, 최대 log N번 반복이 된다

    이제 접미사를 구하면, z알고리즘처럼 접두사를 찾아간다 O(N)
    이러면 N * ((log N)^2) 의 시간이 걸린다
*/

namespace BaekJoon._56
{
    internal class _56_05
    {

        static void Main5(string[] args)
        {

#if first
            string str;
            int[] ord, sfx, lcp, calc;
            int len, d;

            Solve();

            void Solve()
            {

                Input();

                SetSFX();
                SetLCP();

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);

                sw.Write($"{sfx[0] + 1}");
                for (int i = 1; i < len; i++)
                {

                    sw.Write($" {sfx[i] + 1}");
                }
                sw.Write('\n');

                sw.Write('x');

                for (int i = 1; i < len; i++)
                {

                    sw.Write($" {lcp[i]}");
                }

                sw.Close();
            }

            void SetSFX()
            {

                for (int i = 0; i < len; i++)
                {

                    sfx[i] = i;
                    ord[i] = str[i] - 'a';
                }

                for (d = 1; ; d <<= 1)
                {

                    Array.Sort(sfx, (x, y) => MyComp(x, y));

                    calc[sfx[0]] = 0;
                    for (int i = 1; i < len; i++)
                    {

                        calc[sfx[i]] = calc[sfx[i - 1]] + (MyComp(sfx[i], sfx[i - 1]) != 0 ? 1 : 0);
                    }

                    int[] temp = ord;
                    ord = calc;
                    calc = temp;

                    if (ord[sfx[len - 1]] == len - 1) break;
                }
            }

            void SetLCP()
            {

                for (int i = 0; i < len; i++)
                {

                    calc[sfx[i]] = i;
                }

                for (int k = 0, i = 0; i < len; i++)
                {

                    if (calc[i] == 0) continue;
                    for (int j = sfx[calc[i] - 1]; Math.Max(i, j) + k < len && str[i + k] == str[j + k]; k++) { }
                    lcp[calc[i]] = k > 0 ? k-- : 0;
                }
            }

            int MyComp(int _idx1, int _idx2)
            {

                if (ord[_idx1] != ord[_idx2]) return ord[_idx1].CompareTo(ord[_idx2]);

                _idx1 += d;
                _idx2 += d;

                return (_idx1 < len && _idx2 < len) ? ord[_idx1].CompareTo(ord[_idx2]) : _idx2.CompareTo(_idx1);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                str = sr.ReadLine();
                len = str.Length;

                ord = new int[len];
                sfx = new int[len];
                lcp = new int[len];
                calc = new int[len];

                sr.Close();
            }
#else

            MySuffixArr data;
            string str;

            Solve();

            void Solve()
            {

                Input();


                data = new(str);
                data.SetSuffixArr();
                data.SetLCP();

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);

                sw.Write($"{data.sfx[0] + 1}");
                for (int i = 1; i < data.len; i++)
                {

                    sw.Write($" {data.sfx[i] + 1}");
                }
                sw.Write('\n');

                sw.Write('x');
                for (int i = 1; i < data.len; i++)
                {

                    sw.Write($" {data.lcp[i]}");
                }
                sw.Write('\n');
                sw.Close();
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                str = sr.ReadLine();
                sr.Close();
            }
#endif
        }

#if !first

        class MySuffixArr
        {

            string str;
            int d;
            int[] calc;

            public int len;
            public int[] sfx;
            public int[] lcp;
            public int[] ord;

            public MySuffixArr(string _str)
            {

                str = _str;
                len = str.Length;
                ord = new int[len];
                sfx = new int[len];
                lcp = new int[len];

                calc = new int[len];
            }

            public void SetLCP()
            {

                for (int i = 0; i < len; i++)
                {

                    calc[sfx[i]] = i;
                }

                for (int k = 0, i = 0; i < len; i++)
                {

                    if (calc[i] == 0) continue;
                    for (int j = sfx[calc[i] - 1]; Math.Max(i, j) + k < len && str[i + k] == str[j + k]; k++) { }
                    lcp[calc[i]] = k > 0 ? k-- : 0;
                }
            }

            public void SetSuffixArr()
            {

                for (int i = 0; i < len; i++)
                {

                    sfx[i] = i;
                    ord[i] = str[i] - 'a';
                }

                for (d = 1; ; d <<= 1)
                {

                    Sort();

                    calc[sfx[0]] = 0;
                    for (int i = 1; i < len; i++)
                    {

                        calc[sfx[i]] = calc[sfx[i - 1]] + (MyComp(sfx[i], sfx[i - 1]) > 0 ? 1 : 0);
                    }

                    int[] temp = ord;
                    ord = calc;
                    calc = temp;

                    if (ord[sfx[len - 1]] == len - 1) break;
                }
            }

            private void Sort()
            {

                for (int i = 0; i < len; i++)
                {

                    SetUp(i);
                }

                Swap(0, len - 1);
                for (int i = len - 2; i >= 1; i--)
                {

                    SetDown(i);
                    Swap(0, i);
                }
            }

            private void SetUp(int _idx)
            {

                while (_idx > 0)
                {

                    int parent = (_idx - 1) / 2;
                    if (MyComp(sfx[_idx], sfx[parent]) > 0)
                    {

                        Swap(_idx, parent);
                        _idx = parent;
                    }
                    else return;
                }
            }

            private void SetDown(int _len)
            {

                int idx = 0;
                while (idx < _len)
                {

                    int l = idx * 2 + 1;
                    int r = idx * 2 + 2;

                    if (l <= _len)
                    {

                        if (r <= _len)
                        {

                            int child;
                            if (MyComp(sfx[l], sfx[r]) > 0) child = l;
                            else child = r;

                            if (MyComp(sfx[child], sfx[idx]) > 0)
                            {

                                Swap(idx, child);
                                idx = child;
                                continue;
                            }
                        }
                        else if (MyComp(sfx[l], sfx[idx]) > 0)
                        {

                            Swap(idx, l);
                            idx = l;
                            continue;
                        }
                    }

                    return;
                }
            }

            private void Swap(int _idx1, int _idx2)
            {

                int temp = sfx[_idx1];
                sfx[_idx1] = sfx[_idx2];
                sfx[_idx2] = temp;
            }

            int MyComp(int _idx1, int _idx2)
            {

                if (ord[_idx1] != ord[_idx2]) return ord[_idx1].CompareTo(ord[_idx2]);

                _idx1 += d;
                _idx2 += d;

                return (_idx1 < len && _idx2 < len) ? ord[_idx1].CompareTo(ord[_idx2]) : _idx2.CompareTo(_idx1);
            }
        }
#endif
    }


#if other
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

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var s = sr.ReadLine();

        var (sa, lcp) = SuffixArray(s);

        foreach (var v in sa)
            sw.Write($"{1 + v} ");

        sw.WriteLine();

        sw.Write("x ");
        foreach (var v in lcp.Skip(1))
            sw.Write($"{v} ");
    }

    private static (int[] sa, int[] lcp) SuffixArray(string s)
    {
        var n = s.Length;
        var sa = new int[n];
        var group = new int[n];
        var buf = new int[n];

        for (var idx = 0; idx < n; idx++)
        {
            sa[idx] = idx;
            group[idx] = s[idx];
        }

        var t = 1;
        var comp = Comparer<int>.Create((l, r) =>
        {
            if (group[l] != group[r])
                return group[l].CompareTo(group[r]);

            var lv = l + t < n ? group[l + t] : -1;
            var rv = r + t < n ? group[r + t] : -1;

            return lv.CompareTo(rv);
        });

        for (t = 1; t <= n; t <<= 1)
        {
            Array.Sort(sa, comp);

            buf[sa[0]] = 0;
            for (var idx = 1; idx < n; idx++)
                if (comp.Compare(sa[idx], sa[idx - 1]) == 0)
                    buf[sa[idx]] = buf[sa[idx - 1]];
                else
                    buf[sa[idx]] = 1 + buf[sa[idx - 1]];

            (group, buf) = (buf, group);
        }

        var revsa = new int[n];
        for (var idx = 0; idx < n; idx++)
            revsa[sa[idx]] = idx;

        var lcp = new int[n];
        var lastlcp = 0;

        for (var idx = 0; idx < n; idx++)
        {
            var saidx = revsa[idx];
            if (saidx == 0)
            {
                lastlcp = 0;
                continue;
            }

            var v = Math.Max(0, lastlcp - 1);
            var maxoffset = Math.Max(sa[saidx], sa[saidx - 1]);

            while (maxoffset + v < n && s[sa[saidx] + v] == s[sa[saidx - 1] + v])
                v++;

            lcp[saidx] = v;
            lastlcp = v;
        }

        return (sa, lcp);
    }
}
#elif other2
using System;
using System.IO;
using System.Text;

public class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        int[] sa = GetSuffixArray(s), lcp = GetLCPArray(s, sa);
        StringBuilder sb = new();
        BufferedStream bs = new(Console.OpenStandardOutput());
        for (int i = 0; i < sa.Length; i++)
        {
            sb.Append(sa[i] + 1);
            if (i < sa.Length - 1)
                sb.Append(' ');
        }
        sb.Append("\nx ");
        for (int i = 1; i < lcp.Length; i++)
        {
            sb.Append(lcp[i]);
            if (i < lcp.Length - 1)
                sb.Append(' ');
        }
        string answer = sb.ToString();
        bs.Write(Encoding.Default.GetBytes(answer), 0, Encoding.Default.GetByteCount(answer));
        bs.Close();
    }
    static int[] GetSuffixArray(string s)
    {
        int n = s.Length;
        int[] sa = new int[n], rank = new int[n + 1];
        for (int i = 0; i < n; i++)
        {
            sa[i] = i;
            rank[i] = Math.Max(0, s[i] - 'a' + 1);
        }
        int t = 1;
        int Compare(int a, int b)
        {
            if (rank[a] == rank[b])
            {
                int x = a + t >= n ? -1 : rank[a + t], y = b + t >= n ? -1 : rank[b + t];
                return x - y;
            }
            return rank[a] - rank[b];
        }
        int[] count = new int[Math.Max(26, n) + 1], array = new int[n];
        while (t < n)
        {
            Array.Fill(count, 0);
            for (int i = 0; i < n; i++)
            {
                count[rank[Math.Min(n, i + t)]]++;
            }
            for (int i = 1; i <= 26 || i <= n; i++)
            {
                count[i] += count[i - 1];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                array[--count[rank[Math.Min(n, i + t)]]] = i;
            }
            Array.Fill(count, 0);
            for (int i = 0; i < n; i++)
            {
                count[rank[i]]++;
            }
            for (int i = 1; i <= 26 || i <= n; i++)
            {
                count[i] += count[i - 1];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                sa[--count[rank[array[i]]]] = array[i];
            }
            array[sa[0]] = 1;
            for (int i = 1; i < n; i++)
            {
                if (Compare(sa[i - 1], sa[i]) < 0)
                    array[sa[i]] = array[sa[i - 1]] + 1;
                else
                    array[sa[i]] = array[sa[i - 1]];
            }
            for (int i = 0; i < n; i++)
            {
                rank[i] = array[i];
            }
            t *= 2;
        }
        return sa;
    }
    static int[] GetLCPArray(string s, int[] sa)
    {
        int n = s.Length;
        int[] lcp = new int[n], rank = new int[n];
        for (int i = 0; i < n; i++)
        {
            rank[sa[i]] = i;
        }
        int len = 0;
        for (int i = 0; i < n; i++)
        {
            if (rank[i] == 0)
                lcp[rank[i]] = 0;
            else
            {
                int j = sa[rank[i] - 1];
                while (i + len < n && j + len < n && s[i + len] == s[j + len])
                {
                    len++;
                }
                lcp[rank[i]] = len;
                if (len > 0)
                    len--;
            }
        }
        return lcp;
    }
}
#endif
}
