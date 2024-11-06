using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 22
이름 : 배성훈
내용 : 점프 점프
    문제번호 : 11060번

    dp 문제다
    5
    1 0 2 5 5
    와 같이 2번째 칸에서 이동을 못하는데, 0이 나와 한 번 틀렸다;
    이후에 수정하니 이상없이 통과했다

    N*M, N^2 이외의 방법은 모르겠다;
*/

namespace BaekJoon.etc
{
    internal class etc_0716
    {

        static void Main716(string[] args)
        {

            StreamReader sr;

            int[] arr;
            int n;

            int[] dp;

            Solve();

            void Solve()
            {

                Input();

                SetDp();

                Console.WriteLine(dp[n - 1]);
            }

            void SetDp()
            {

                dp[0] = 0;
#if first

                for (int i = 1; i < n; i++)
                {

                    for (int j = 0; j < i; j++)
                    {

                        if (j + arr[j] < i || dp[j] == -1) continue;
                        if (dp[i] == -1) dp[i] = dp[j] + 1;
                        else dp[i] = Math.Min(dp[i], dp[j] + 1);
                    }
                }
#else

                for (int i = 0; i < n; i++)
                {

                    if (dp[i] == -1) break;

                    for (int j = 1; j <= arr[i]; j++)
                    {

                        if (i + j >= n) break;
                        if (dp[i + j] == -1) dp[i + j] = dp[i] + 1;
                        else dp[i + j] = Math.Min(dp[i + j], dp[i] + 1);
                    }
                }
#endif
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                dp = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                    dp[i] = -1;
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
}
