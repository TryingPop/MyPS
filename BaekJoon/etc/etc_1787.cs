using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 26
이름 : 배성훈
내용 : 달려달려
    문제번호 : 1757번

    dp 문제다.
    휴식을 하면 탈진 수치가 0이될때까지 강제로 쉬어야 한다.
    dp[i][j][k]를 i번째 달리기를 진행, j는 현재 탈진 수치, k는 강제 휴식 유무로 설정했다.
    그리고 점화식은 강제 휴식 -> 이동 시도로 했다.
    이동 시도 -> 휴식을 해도 되나
    dp를 2차원으로 줄이면서 반대로 했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1787
    {
        static void Main1787(string[] args)
        {

            int n, m;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int[][] dp = new int[m + 1][];
                for (int i = 0; i <= m; i++)
                {

                    dp[i] = new int[2];
                    Array.Fill(dp[i], -1);
                }

                dp[0][0] = 0;

                for (int i = 0; i < n; i++)
                {

                    for (int j = 1; j <= m; j++)
                    {

                        // 휴식 시도.
                        dp[j - 1][1] = Math.Max(dp[j][1], dp[j][0]);
                    }

                    for (int j = m; j > 0; j--)
                    {

                        // 달리기 시도
                        if (dp[j - 1][0] == -1) continue;
                        dp[j][0] = dp[j - 1][0] + arr[i];
                    }

                    dp[0][0] = Math.Max(dp[0][0], dp[0][1]);
                }

                Console.Write(dp[0][0]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                arr = new int[n];
                for (int i = 0;i < n; i++)
                {

                    arr[i] = ReadInt();
                }

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
                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
