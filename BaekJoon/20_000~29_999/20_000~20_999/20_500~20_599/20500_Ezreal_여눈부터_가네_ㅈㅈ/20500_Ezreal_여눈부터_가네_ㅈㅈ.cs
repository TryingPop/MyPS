using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 27
이름 : 배성훈
내용 : Ezreal 여눈부터 가네 ㅈㅈ
    문제번호 : 20500번

    dp, 수학 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1789
    {

        static void Main1789(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int MOD = 1_000_000_007;

            // ?[i][j] : i 는 끝이 1이냐 5의 수, j는 모든 자리수 총합 % 3일 때 개수
            int[][] cur = new int[2][];
            int[][] next = new int[2][];
            for (int i = 0; i < 2; i++)
            {

                cur[i] = new int[3];
                next[i] = new int[3];
            }

            // 1의 상태
            cur[0][1] = 1;
            cur[1][2] = 1;

            for (int i = 1; i < n; i++)
            {

                for (int j = 0; j < 2; j++)
                {

                    for (int k = 0; k < 3; k++)
                    {

                        // 앞에 1 추가
                        next[j][(k + 1) % 3] = (cur[j][k] + next[j][(k + 1) % 3]) % MOD;
                        next[j][(k + 2) % 3] = (cur[j][k] + next[j][(k + 2) % 3]) % MOD;
                    }
                }

                for (int j = 0; j < 2; j++)
                {

                    for (int k = 0; k < 3; k++)
                    {

                        cur[j][k] = next[j][k];
                        next[j][k] = 0;
                    }
                }
            }

            Console.Write(cur[1][0]);
        }
    }

#if other
using System;

public class Program
{
    const int Mod = 1000000007;
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[,,] dp = new int[n + 1, 3, 2];
        dp[0, 0, 0] = 1;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    dp[i + 1, (j + 1) % 3, 0] = (dp[i + 1, (j + 1) % 3, 0] + dp[i, j, k]) % Mod;
                    dp[i + 1, (j + 2) % 3, 1] = (dp[i + 1, (j + 2) % 3, 1] + dp[i, j, k]) % Mod;
                }
            }
        }
        Console.Write(dp[n, 0, 1]);
    }
}
#endif
}
