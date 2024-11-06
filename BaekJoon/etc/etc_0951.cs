using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 8
이름 : 배성훈
내용 : 타일 채우기
    문제번호 : 2718번

    dp, 비트마스킹 문제다
    etc_0950의 방법으로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0951
    {

        static void Main951(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[][] dp;
            int n;

            Solve();
            void Solve()
            {

                Init();

                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    int m = ReadInt();

                    sw.Write($"{dp[0][m]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                SetDp();
            }

            void SetDp()
            {

                dp = new int[1 << 4][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new int[1_001];
                }

                dp[0][0] = 1;

                for (int i = 1; i <= 1_000; i++)
                {

                    dp[0][i] += dp[0][i - 1];
                    dp[3][i] += dp[0][i - 1];
                    dp[9][i] += dp[0][i - 1];
                    dp[12][i] += dp[0][i - 1];
                    dp[15][i] += dp[0][i - 1];

                    dp[2][i] += dp[1][i - 1];
                    dp[8][i] += dp[1][i - 1];
                    dp[14][i] += dp[1][i - 1];

                    dp[1][i] += dp[2][i - 1];
                    dp[13][i] += dp[2][i - 1];

                    dp[0][i] += dp[3][i - 1];
                    dp[12][i] += dp[3][i - 1];

                    dp[8][i] += dp[4][i - 1];
                    dp[11][i] += dp[4][i - 1];

                    dp[10][i] += dp[5][i - 1];

                    dp[9][i] += dp[6][i - 1];

                    dp[8][i] += dp[7][i - 1];

                    dp[1][i] += dp[8][i - 1];
                    dp[4][i] += dp[8][i - 1];
                    dp[7][i] += dp[8][i - 1];

                    dp[0][i] += dp[9][i - 1];
                    dp[6][i] += dp[9][i - 1];

                    dp[5][i] += dp[10][i - 1];

                    dp[4][i] += dp[11][i - 1];

                    dp[0][i] += dp[12][i - 1];
                    dp[3][i] += dp[12][i - 1];

                    dp[2][i] += dp[13][i - 1];

                    dp[1][i] += dp[14][i - 1];

                    dp[0][i] += dp[15][i - 1];
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
class MainApp
{
    static void Main(string[] args)
    {
        var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        int T = int.Parse(sr.ReadLine());
        int[] Cache = new int[22];
        Cache[0] = 1;
        Cache[1] = 1;
        Cache[2] = 5;
        for (int i = 3; i < 22; ++i)
        {
            Cache[i] += Cache[i - 1] + 4 * Cache[i - 2];
            for (int j = i - 3; j >= 0; --j)
            {
                Cache[i] += 2 * Cache[j];
            }
            for (int j = i - 4; j >= 0; j -= 2)
            {
                Cache[i] += Cache[j];
            }
        }
        int[] Result = new int[T];
        for (int i = 0; i < T; ++i)
        {
            int N = int.Parse(sr.ReadLine());
            Result[i] = Cache[N];
        }
        for (int i = 0; i < T; ++i)
        {
            Console.WriteLine(Result[i]);
        }
    }
}
#endif
}
