using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 22
이름 : 배성훈
내용 : 한번 열면 멈출 수 없어
    문제번호 : 1636번

    그리디, 애드 혹 문제다
    범위를 교집합으로 줄여가며 풀었다

    만약 일치하지 않는 경우 거리가 최소가 되게 해서 점으로 만들었다
    그래서 n번째에서 점이 아닌 구간이면 이전은 점이 아닌 구간일 수 밖에 없다

    그리고 m번째에 점이면 m + 1번째부터는 모두 점이된다
*/

namespace BaekJoon.etc
{
    internal class etc_0770
    {

        static void Main770(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] dp;
            (int s, int e)[] arr;
            int ret, n;

            Solve();

            void Solve()
            {

                Input();

                Find();

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);

                sw.Write($"{ret}\n");

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{dp[i]}\n");
                }

                sw.Close();
            }

            void Find()
            {

                ret = 0;
                int s = arr[n - 1].s;
                int e = arr[n - 1].e;
                if (s != e)
                {

                    for (int i = 0; i < n; i++)
                    {

                        dp[i] = e;
                    }

                    return;
                }

                int start = -1;

                dp[n - 1] = arr[n - 1].s;

                for (int i = n - 2; i >= 0; i--)
                {

                    s = arr[i].s;
                    e = arr[i].e;
                    if (s == e)
                    {

                        dp[i] = s;
                        ret += Math.Abs(dp[i + 1] - dp[i]);
                        continue;
                    }

                    start = i;
                    break;
                }

                if (start == -1) return;

                s = arr[start].s;
                e = arr[start].e;

                int fill;
                if (s <= dp[start + 1] && dp[start + 1] <= e) fill = dp[start + 1];
                if (dp[start + 1] < s)
                {

                    ret += s - dp[start + 1];
                    fill = s;
                }
                else 
                {

                    ret += dp[start + 1] - e;
                    fill = e; 
                }

                for (int i = 0; i <= start; i++)
                {

                    dp[i] = fill;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);

                n = ReadInt();
                arr = new (int s, int e)[n];
                dp = new int[n];
                arr[0] = (ReadInt(), ReadInt());
                for (int i = 1; i < n; i++)
                {

                    int s = ReadInt();
                    int e = ReadInt();

                    int bs = arr[i - 1].s;
                    int be = arr[i - 1].e;

                    if (be <= s) arr[i] = (s, s);
                    else if (e <= bs) arr[i] = (e, e);
                    else
                    {

                        arr[i].s = Math.Max(s, bs);
                        arr[i].e = Math.Min(e, be);
                    }
                }

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
using ProblemSolving.Templates.Merger;
using ProblemSolving.Templates.Utility;
using System;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates.Merger {}
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.IO {}
namespace System.Linq {}

// #nullable disable

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
        var n = Int32.Parse(sr.ReadLine());
        var pringles = new (int s, int e)[n];

        for (var idx = 0; idx < n; idx++)
        {
            var (s, e) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            pringles[idx] = (s, e);
        }

        var minsum = Int64.MaxValue;
        var minarr = new int[n];
        var buf = new int[n];

        for (var st = pringles[0].s; st <= pringles[0].e; st++)
        {
            var sum = Simulate(st, pringles, buf);
            if (sum < minsum)
            {
                minsum = sum;
                (buf, minarr) = (minarr, buf);
            }
        }

        sw.WriteLine(minsum);
        foreach (var v in minarr)
            sw.WriteLine(v);
    }

    private static long Simulate(int v, (int s, int e)[] pringles, int[] arr)
    {
        var sum = 0L;

        arr[0] = v;
        for (var idx = 1; idx < pringles.Length; idx++)
        {
            var (s, e) = pringles[idx];

            if (v < s)
            {
                sum += Math.Abs(v - s);
                v = s;
            }
            else if (e < v)
            {
                sum += Math.Abs(v - e);
                v = e;
            }

            arr[idx] = v;
        }

        return sum;
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
// Timestamp: 2024-06-18 13:39:35 UTC+9

#endif
}
