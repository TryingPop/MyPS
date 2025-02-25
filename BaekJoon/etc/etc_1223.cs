using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 29
이름 : 배성훈
내용 : 1, 2, 3 더하기 9
    문제번호 : 16195번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1223
    {

        static void Main1223(string[] args)
        {

            int MAX = 1_000;
            int MOD = 1_000_000_009;

            StreamReader sr;
            StreamWriter sw;

            int[][] dp;

            Solve();
            void Solve()
            {

                SetDp();
                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int t = ReadInt();

                while(t-- > 0)
                {

                    int n = ReadInt();
                    int m = ReadInt();

                    sw.Write($"{dp[n][m]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void SetDp()
            {

                dp = new int[MAX + 1][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new int[i + 1];
                }

                dp[0][0] = 1;

                for (int i = 0; i <= MAX; i++)
                {

                    for (int j = 0; j < dp[i].Length; j++)
                    {

                        for (int k = 1; k <= 3; k++)
                        {

                            int nVal = i + k;
                            int nCnt = j + 1;
                            if (nVal > MAX) break;
                            dp[nVal][nCnt] = (dp[nVal][nCnt] + dp[i][j]) % MOD;
                        }
                    }
                }

                for (int i = 0; i <= MAX; i++)
                {

                    for (int j = 1; j < dp[i].Length; j++)
                    {

                        dp[i][j] = (dp[i][j] + dp[i][j - 1]) % MOD;
                    }
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
}
