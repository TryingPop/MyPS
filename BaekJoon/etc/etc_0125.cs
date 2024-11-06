/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 2xn 타일링
    문제번호 : 11726번

    나머지 부분 신경안써서 한 번 틀렸다;
    일반적인 dp문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0125
    {

        static void Main125(string[] args)
        {

            int len = int.Parse(Console.ReadLine());
            if (len == 1)
            {

                Console.WriteLine(1);
                return;
            }
            
            int[] dp = new int[len + 1];

            dp[1] = 1;
            dp[2] = 2;

            for (int i = 3; i <= len; i++)
            {

                dp[i] = dp[i - 1] + dp[i - 2];
                dp[i] %= 10_007;
            }

            Console.WriteLine(dp[len]);
        }
    }
}
