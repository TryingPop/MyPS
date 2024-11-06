using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 12
이름 : 배성훈
내용 : 흩날리는 시험지 속에서 내 평점이 느껴진거야
    문제번호 : 17951번

    이분탐색, 매개변수 탐색 문제다
    최소 시험 점수 값을 정하고 해당 점수로 구간을 나눴다
    그리고 나뉘어진 구간이 k개 이상인지 확인하며 점수값으로 이분탐색했다
*/

namespace BaekJoon.etc
{
    internal class etc_0875
    {

        static void Main875(string[] args)
        {

            StreamReader sr;
            int n, k;
            int[] sum;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int l = 0;
                int r = 2_000_000;

                int ret;

                while(l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (Find(mid)) l = mid + 1;
                    else r = mid - 1;
                }

                ret = l - 1;
                Console.Write(ret);
            }

            bool Find(int _min)
            {

                int cnt = 0;
                int prev = 0;
                for (int i = 1; i <= n; i++)
                {

                    int chk = sum[i] - sum[prev];
                    if (chk < _min) continue;
                    prev = i;
                    cnt++;
                }

                return k <= cnt;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                sum = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    sum[i] = sum[i - 1] + ReadInt();
                }
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
using ProblemSolving.Templates.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Collections.Generic {}
namespace System.Globalization {}
namespace System.IO {}
namespace System.Linq {}

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
        var scores = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var lo = 0;
        var hi = 30 * 100000;

        while (lo + 1 < hi)
        {
            var mid = (lo + hi) / 2;
            var fmid = F(scores, mid, k);

            if (fmid)
                lo = mid;
            else
                hi = mid;
        }

        sw.WriteLine(lo);
    }

    private static bool F(int[] scores, int mid, int k)
    {
        var curr = 0;
        foreach (var v in scores)
        {
            curr += v;

            if (curr >= mid)
            {
                k--;
                curr = 0;
            }
        }

        return k <= 0;
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
#endif
}
