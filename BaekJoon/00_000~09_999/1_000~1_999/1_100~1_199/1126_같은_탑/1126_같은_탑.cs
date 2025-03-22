using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 2
이름 : 배성훈
내용 : 같은탑
    문제번호 : 1126번

    dp 문제다.
    dp[i][j] = val를 i 번째 사각형을 택하고 
    j 높이차일 때 val의 최대 높이를 기록해가면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1238
    {

        static void Main1238(string[] args)
        {

            int MAX = 500_000;
            int n;
            int[] arr;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();

                SetDp();

                GetRet();
            }

            void SetDp()
            {

                dp = new int[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new int[MAX + 1];
                    Array.Fill(dp[i], -1);
                }
            }

            void GetRet()
            {

                dp[0][0] = 0;
                dp[0][arr[0]] = arr[0];
                for (int i = 1; i < n; i++)
                {

                    for (int j = 0; j <= MAX; j++)
                    {

                        // 이전에 방문 X 면 변화 X
                        if (dp[i - 1][j] == -1) continue;
                        // 이전 값으로 갱신
                        dp[i][j] = Math.Max(dp[i][j], dp[i - 1][j]);

                        // A에 쌓는다.
                        dp[i][j + arr[i]] = Math.Max(dp[i][j + arr[i]], dp[i - 1][j] + arr[i]);
                        // B에 쌓는다.
                        if (arr[i] < j) dp[i][j - arr[i]] = Math.Max(dp[i][j - arr[i]], dp[i - 1][j]);
                        else dp[i][arr[i] - j] = Math.Max(dp[i][arr[i] - j], dp[i - 1][j] + arr[i] - j);
                    }
                }

                Console.Write(dp[n - 1][0] > 0 ? dp[n - 1][0] : -1);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
using ProblemSolving.Templates.Utility;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.ComponentModel {}
namespace System.IO {}
namespace System.Linq {}
namespace System.Numerics {}

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
        var height = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var bias = 500_500;
        var diff = new int?[1_100_000];
        var backup = new int?[1_100_000];

        diff[bias] = 0;
        foreach (var h in height)
        {
            for (var idx = 0; idx < diff.Length; idx++)
                backup[idx] = diff[idx];

            for (var bef = 0; bef < diff.Length; bef++)
                if (diff[bef].HasValue)
                {
                    // add to hi
                    if (bef + h < diff.Length)
                        backup[bef + h] = Math.Max(backup[bef + h] ?? 0, diff[bef].Value + h);

                    // add to lo
                    if (bef - h >= 0)
                        backup[bef - h] = Math.Max(backup[bef - h] ?? 0, diff[bef].Value);
                }

            (diff, backup) = (backup, diff);
        }

        sw.WriteLine(diff[bias] == 0 ? -1 : diff[bias]);
    }
}
#endif
}
