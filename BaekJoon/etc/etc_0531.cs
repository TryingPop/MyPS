using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 14
이름 : 배성훈
내용 : 오르막 수
    문제번호 : 11057번

    dp 문제다
    자리수가 증가하면 이전 자리수 앞의 숫자가 현재 수보다 작거나 같은 경우 경우의 수가 늘어난다
    이렇게 점화식을 찾아 dp로 푸니 쉽게 풀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0531
    {

        static void Main531(string[] args)
        {

            int MOD = 10_007;
            int n = int.Parse(Console.ReadLine());

            int[,] dp = new int[n, 10];
            for (int i = 0; i < 10; i++)
            {

                dp[0, i] = 1;
            }

            for (int i = 1; i < n; i++)
            {

                int calc = 0;
                for (int j = 0; j < 10; j++)
                {

                    calc += dp[i - 1, j];
                    calc %= MOD;
                    dp[i, j] = calc;
                }
            }

            int ret = 0;
            for (int i = 0; i < 10; i++)
            {

                ret += dp[n - 1, i];
            }

            ret %= MOD;
            Console.WriteLine(ret);
        }
    }
#if other
using System;

namespace 동적계획법
{
    class 오르막수
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            
            long[,] data = new long[10, 1001];
            long sum = 0;
            
            for (int i = 0; i < 10; i++)
            {
                data[i, 1] = 1;
            }
            
            for (int j = 2; j <= n; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i == 0)
                    {
                        data[i, j] = 1;
                    }
                    else
                    {
                        data[i, j] = (data[i - 1, j] + data[i, j - 1]) % 10007;
                    }
                }
            }
            
            for (int i = 0; i < 10; i++)
            {
                sum += data[i, n];
            }
            
            Console.WriteLine(sum % 10007);
        }
    }
}
#endif
}
