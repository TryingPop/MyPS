using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 5
이름 : 배성훈
내용 : 엘 도라도
    문제번호 : 6506번

    dp 문제다
    잘못 접근해 엄청나게 틀린 문제다
    처음에는 오름차순이 보여 자기보다 낮은 것의 개수를 찾는데,
    끝만 고정시키고 나머진 자기보다 작은 것 중에 k - 1개를 택하면 되지 않을까 생각했는데
        5 5
        3 1 2 4 5
    이 반례를 찾는데 한참 틀렸다;

    그리고 힌트를 보고
    그냥 이차원 배열로 앞에 몇 개 있는지 조사해 찾았다
    참고로 오름차순으로 100짜리 주면 long범위 아득히 벗어난다
*/

namespace BaekJoon.etc
{
    internal class etc_0794
    {

        static void Main794(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            long[][] dp;
            int[] arr;
            int n, k;

            Solve();
            void Solve()
            {

                Init();

                while(Input())
                {

                    for (int i = 0; i < n; i++)
                    {

                        dp[i][1] = 1;
                        for (int j = 2; j <= k; j++)
                        {

                            dp[i][j] = -1;
                        }
                    }

                    long ret = 0;

                    for (int i = 0; i < n; i++)
                    {

                        ret += DFS(i, k);
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            bool Input()
            {

                n = ReadInt();
                k = ReadInt();

                if (n == 0 && k == 0) return false;

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                arr = new int[100];
                dp = new long[100][];
                for (int i = 0; i < 100; i++)
                {

                    dp[i] = new long[101];
                }
            }

            long DFS(int _idx, int _len)
            {

                if (dp[_idx][_len] != -1) return dp[_idx][_len];
                long ret = 0;

                for (int i = _idx + 1; i < n; i++)
                {

                    if (arr[i] < arr[_idx]) continue;
                    ret += DFS(i, _len - 1);
                }

                dp[_idx][_len] = ret;

                return ret;
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';

                int ret = plus ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
#nullable disable

using System;
using System.IO;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        while (true)
        {
            var nk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nk[0];
            var k = nk[1];

            if (n == 0 && k == 0)
                return;

            // compress 
            var numbers = sr.ReadLine().Split(' ').Select(Int32.Parse)
                .Select((v, idx) => (v, idx))
                .OrderBy(v => v.v)
                .ToArray();

            var a = new int[1 + n];
            for (var compressed = 0; compressed < n; compressed++)
                a[1 + numbers[compressed].idx] = 1 + compressed;

            var amax = a.Max();

            // dp[k, l, e] = count of increasing sequence of A_1..A_e, ends with k, length l
            var dp = new long[1 + amax, 1 + k, 1 + n];

            for (var idx = 1; idx <= n; idx++)
                dp[a[idx], 1, idx] = 1;

            for (var e = 2; e <= n; e++)
            {
                for (var seqlen = 0; seqlen <= k; seqlen++)
                    for (var largest = 0; largest <= amax; largest++)
                    {
                        if (largest < a[e] && seqlen < k)
                            dp[a[e], 1 + seqlen, e] += dp[largest, seqlen, e - 1];

                        dp[largest, seqlen, e] += dp[largest, seqlen, e - 1];
                    }
            }

            var sum = 0L;
            for (var u = 0; u <= a.Max(); u++)
                sum += dp[u, k, n];

            sw.WriteLine(sum);
        }
    }
}

#endif
}
