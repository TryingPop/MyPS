using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 16
이름 : 배성훈
내용 : 제곱수의 합
    문제번호 : 1699번

    dp로 최단 경로를 메모하면서 풀었다
    N * sqrt(N) 시간이 걸린다
*/

namespace BaekJoon.etc
{
    internal class etc_0040
    {

        static void Main40(string[] args)
        {

            int MAX = 100_000;
            int n = int.Parse(Console.ReadLine());
            int[] dp = new int[n + 1];

            int maxSqrt = 2;
            dp[1] = 1;
            for (int i = 2; i <= n; i++)
            {

                if (maxSqrt * maxSqrt == i)
                {

                    dp[i] = 1;
                    maxSqrt++;
                    continue;
                }

                int min = MAX;
                for (int j = 1; j < maxSqrt; j++)
                {

                    int chk = dp[i - j * j ] + dp[j * j];
                    if (chk < min) min = chk;
                }

                dp[i] = min;
            }

            Console.WriteLine(dp[n]);
        }
    }
}
