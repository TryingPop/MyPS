using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 16
이름 : 배성훈
내용 : 루트
    문제번호 : 4619번

    수학 문제다.
    A^n의 값이 B와 가장 가까운 A를 찾는 것이다.
    n = 1 인 경우 A = B임이 자명하다.

    이제 n > 1인 경우로 보면,
    B의 범위가 100만이므로 sqrt(100만) = 1000까지 n승을 하면서 확인해주면 된다.
    다만 200만을 넘어가는 경우 멈추면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1339
    {

        static void Main1339(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int b, n;
            int[][] dp;

            Init();

            while (Input())
            {

                GetRet();
            }

            void Init()
            {

                dp = new int[10][];
                for (int i = 0; i < 10; i++)
                {

                    dp[i] = new int[1_001];
                }

                Array.Fill(dp[0], 1);
                for (int j = 1; j <= 1_000; j++)
                {

                    for (int i = 1; i < 10; i++)
                    {

                        int next = dp[i - 1][j] * j;
                        if (next > 2_000_000) next = 2_000_000;
                        dp[i][j] = next;
                    }
                }
            }

            bool Input()
            {

                string[] temp = sr.ReadLine().Split();
                b = int.Parse(temp[0]);
                n = int.Parse(temp[1]);

                return n != 0 || b != 0;
            }

            void GetRet()
            {
                
                if (n == 1)
                {

                    sw.WriteLine(b);
                    return;
                }

                int ret = 0;
                int diff = 2_000_000;
                for (int j = 1; j <= 1_000; j++)
                {

                    int cur = Math.Abs(dp[n][j] - b);
                    if (cur >= diff) continue;
                    diff = cur;
                    ret = j;
                }

                sw.WriteLine(ret);
            }
        }
    }
}
