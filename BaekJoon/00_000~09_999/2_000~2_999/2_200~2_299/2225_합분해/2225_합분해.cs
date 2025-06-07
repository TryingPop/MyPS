using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : 합분해
    문제번호 : 2225번

    수학, dp 문제다
    dp를 써서 해결했다

    아이디어는 다음과 같다
    i번째 연산에서 k가 되는 경우의 수를 dp에 저장했다
    이는 삼중 포문으로 작성했다 N^3

    
    처음에 그냥 DFS 돌리다가 시간초과로 한 번 틀렸다
    200^200이니 당연한 결과다!
*/

namespace BaekJoon.etc
{
    internal class etc_0394
    {

        static void Main394(string[] args)
        {

            int MOD = 1_000_000_000;
            int[] info = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int[,] dp = new int[info[1] + 1, info[0] + 1];

            for (int i = 0; i <= info[0]; i++)
            {

                // 처음은 1번
                dp[1, i] = 1;
            }

            for (int i = 2; i <= info[1]; i++)
            {

                for (int j = 0; j <= info[0]; j++)
                {

                    for (int k = 0; k <= info[0]; k++)
                    {

                        if (j + k > info[0]) break;

                        dp[i, j + k] += dp[i - 1, j];
                        dp[i, j + k] %= MOD;
                    }
                }
            }

            Console.WriteLine(dp[info[1], info[0]]);
        }
    }

#if other
int[] NK = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
int N = NK[0]; int K = NK[1];
int[,] dp = new int[K + 1, N + 1];
for (int k = 1; k <= K; k++)
{
    for (int n = 0; n <= N; n++)
    {
        if (k == 1 || n == 0) dp[k, n] = 1;
        else dp[k, n] = (dp[k - 1, n] + dp[k, n - 1]) % 1000000000;
    }
}
Console.WriteLine(dp[K, N]);
#endif
}
