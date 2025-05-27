using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 17
이름 : 배성훈
내용 : 팰린드롬 개수 구하기 (Large)
    문제번호 : 14517번

    dp 문제다.
    부분 수열이 연속된 부분수열을 의미하는 것은 아니다.
    그래서 abb의 부분 수열에 ab가 2개이다.

    dp[i][j] = val를 시작이 i이고 끝이 j이하인 
    팰린드롬의 개수를 val로 설정하면 된다.

    str[i] != str[j] 인 경우
    dp[i + 1][j]와 dp[i][j - 1]의 경우의 수를 계승한다.
    그리고 dp[i + 1][j]와 dp[i][j - 1]의 교집합의 경우 dp[i + 1][j - 1]을 두 번 세므로 빼줘야한다.
    dp[i][j] = dp[i + 1][j] + dp[i][j - 1] - dp[i + 1][j - 1] 이다.

    str[i] == str[j] 인 경우
    반면 ==인 경우는 dp[i + 1][j - 1]인 경우를 빼줘야 하나,
    str[i]와 str[j]를 끝으로 하는 팰린드롬을 만들 수 있다.
    그래서 dp[i + 1][j - 1]을 더해주고 없어도 str[i]와 str[j]를 합친 것이 있으므로 + 1을 해준다.
    dp[i][j] = dp[i + 1][j] + d[i][j - 1] + 1이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1197
    {

        static void Main1197(string[] args)
        {

            int MOD = 10_007;
            int n;
            int[][] dp;

            string str;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 0; i < n; i++)
                {

                    dp[i][i] = 1;
                }

                for (int i = 1; i < n; i++)
                {

                    dp[i - 1][i] = 2;
                    if (str[i] == str[i - 1]) dp[i - 1][i]++;
                }

                for (int len = 2; len < n; len++)
                {

                    for (int e = len; e < n; e++)
                    {

                        if (str[e] == str[e - len]) dp[e - len][e] = (dp[e - len][e - 1] + dp[e - len + 1][e] + 1) % MOD;
                        else dp[e - len][e] = (MOD + dp[e - len][e - 1] + dp[e - len + 1][e] - dp[e - len + 1][e - 1]) % MOD;
                    }
                }

                Console.Write(dp[0][n - 1]);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                str = sr.ReadLine();

                n = str.Length;
                dp = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    dp[i] = new int[n];
                }

                sr.Close();
            }
        }
    }
}
