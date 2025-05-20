using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 20
이름 : 배성훈
내용 : 입대
    문제번호 : 31413번

    dp 문제다.
    dp[i][j] = val를 i날에 j 번 헌혈 할 때
    최대 점수 val를 담게 설정했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1635
    {

        static void Main1635(string[] args)
        {

            int n, m, a, d;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int INF = n + 1;
                int NOT_VISIT = -1;

                // dp[i][j] = val
                // i : 날짜
                // j : 헌혈 횟수
                // val : 최고 봉사 점수
                int[][] dp = new int[n + d + 1][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new int[n + 1];
                    Array.Fill(dp[i], NOT_VISIT);
                }

                dp[0][0] = 0;

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        if (dp[i][j] == NOT_VISIT) continue;

                        // 봉사활동
                        dp[i + 1][j] = Math.Max(dp[i + 1][j], dp[i][j] + arr[i]);

                        // 헌혈
                        dp[i + d][j + 1] = Math.Max(dp[i + d][j + 1], dp[i][j] + a);
                    }
                }

                int ret = INF;
                for (int i = n; i < dp.Length; i++)
                {

                    for (int j = 0; j <= n; j++)
                    {

                        if (dp[i][j] == NOT_VISIT) continue;
                        if (dp[i][j] >= m) ret = Math.Min(ret, j);
                    }
                }

                if (ret == INF) ret = -1;
                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                a = ReadInt();
                d = ReadInt();

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
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
}
