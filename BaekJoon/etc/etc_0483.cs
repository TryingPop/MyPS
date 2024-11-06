using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : 이친수
    문제번호 : 2193번

    dp 문제다
    디버그를 돌려보니 피보나치 수열과 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0483
    {

        static void Main483(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            long[,] dp = new long[n + 1, 2];

            dp[1, 1] = 1;

            for (int i = 2; i <= n; i++)
            {

                // 맨 앞이 1인 이친수
                // 뒤에 + 1은 뒤에 모두 0만 오는 새로운 경우
                dp[i, 1] = dp[i - 1, 0] + 1;
                // 맨 앞이 0인 이친수 -> 연산용이다
                dp[i, 0] = dp[i - 1, 1] + dp[i - 1, 0];
            }

            Console.WriteLine(dp[n, 1]);
        }
    }
#if other
class Program
{
    private static void Main()
    {
        BOJ02193.Solution();
    }
}

class BOJ02193
{
    public static void Solution()
    {
        int num = int.Parse(Console.ReadLine() ?? "");

        Console.WriteLine(GetFibonacci(num));
    }


    private static long GetFibonacci(int count)
    {
        long[] numArr = new long[2] { 0, 1 };

        for (int i = 0; i < count; i++)
        {
            (numArr[0], numArr[1]) = (numArr[1], numArr[0] + numArr[1]);
        }

        return numArr[0];
    }
}
#endif
}
