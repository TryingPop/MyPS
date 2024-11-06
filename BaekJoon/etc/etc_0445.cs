using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 3
이름 : 배성훈
내용 : 동전
    문제번호 : 2091번

    그리디, 브루트포스, dp 문제다
    처음에는 그리디로 접근해서 뒤에서부터 줄일 수 있는만큼 최대한 줄였다
    그런데, 31 6 0 3 1 반례를 보고 잘못되었음을 확인했다

    이후엔 브루트 포스로 해볼까 했고,
    사중 포문으로 확인하려고 했고 시간 초과떴다

    그래서 마지막으로 배낭 문제처럼 접근하니 통과했다
    5, 10, 25센트는 5배수이므로 5배수로 만들어 풀었다
    이후에는 이상없이 통과했다

    중간에 잔 실수(인덱스 잘못 기입)나 그리디(10짜리, 25짜리는 끝만 늘려가면 되지 않을까?) 쓰려고 해서 많이 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0445
    {

        static void Main445(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            if (input[0] % 5 > input[1])
            {

                Console.Write("0 0 0 0");
                return;
            }

            int add = input[0] % 5;
            int len = input[0] / 5;
            input[1] -= add;
            (int all, int a, int b, int c, int d)[] dp = new (int all, int a, int b, int c, int d)[len + 1];
            for (int i = 0; i <= len; i++)
            {

                dp[i].all = -1;
            }

            for (int i = 0; i <= input[1] / 5; i++)
            {

                if (i > len) break;
                dp[i] = (i * 5, i * 5, 0, 0, 0);
            }

            if (len < input[2]) input[2] = len;
            for (int i = 1; i <= input[2]; i++)
            {

                bool arriveEnd = false;
                for (int j = len; j >= 0; j--)
                {

                    if (dp[j].all == -1) continue;
                    if (j + 1 > len) continue;

                    dp[j + 1] = dp[j];
                    dp[j + 1].all += 1;
                    dp[j + 1].b += 1;
                    break;
                }

                if (dp[len].all != -1) break;
            }

            if (len < 2 * input[3]) input[3] = len / 2;
            for (int i = 1; i <= input[3]; i++)
            {

                if (dp[len].all != -1) break;

                for (int j = len; j >= 0; j--)
                {

                    if (dp[j].all == -1 || j + 2 > len) continue;
                    if (dp[j + 2].all > dp[j].all + 1) continue;
                    dp[j + 2] = dp[j];
                    dp[j + 2].all += 1;
                    dp[j + 2].c += 1;
                }
            }

            if (len < 5 * input[4]) input[4] = len / 5;
            for (int i = 1; i <= input[4]; i++)
            {

                for (int j = len; j >= 0; j--)
                {

                    if (dp[j].all == -1 || j + 5 > len) continue;
                    if (dp[j + 5].all > dp[j].all + 1) continue;

                    dp[j + 5] = dp[j];
                    dp[j + 5].all += 1;
                    dp[j + 5].d += 1;
                }
            }

            if (dp[len].all == -1) Console.WriteLine("0 0 0 0");
            else Console.WriteLine($"{dp[len].a + add} {dp[len].b} {dp[len].c} {dp[len].d}");
#if Greedy
            // 31 6 0 3 1이 반례
            // 6 + 1 = 7 vs 1 + 3

            int[] val = { 0, 1, 5, 10, 25 };
            // 1, 5, 10, 25
            // greedy
            int cur = 0;

            int digit = 0;
            for (int i = 1; i <= 4; i++)
            {

                cur += val[i] * input[i];

                if (cur < input[0]) continue;

                digit = i;
                break;
            }

            if (digit == 0)
            {

                Console.WriteLine("0 0 0 0");
                return;
            }

            cur -= input[0];
            for (int i = digit; i >= 1; i--)
            {

                int diff = cur / val[i];
                if (input[i] >= diff)
                {

                    input[i] -= diff;
                    cur -= diff * val[i];
                }
                else
                {

                    cur -= val[i] * input[i];
                    input[i] = 0;
                }
            }

            if (cur == 0)
            {

                for (int i = 1; i <= digit; i++)
                {

                    Console.Write($"{input[i]} ");
                }

                for (int i = digit + 1; i <= 4; i++)
                {

                    Console.Write("0 ");
                }
            }
            else
            {

                Console.Write("0 0 0 0");
            }
#elif TimeOut
int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
int[] ret = { 0, 0, 0, 0 };

if (input[0] % 5 > input[1])
{

    Console.Write("0 0 0 0");
    return;
}

int use = 0;
int maxUse = 0;
for (int a = input[0] % 5; a <= input[1]; a += 5)
{

    int remainA = input[0] - a;
    if (remainA < 0) break;

    for (int b = 0; b <= input[2]; b++)
    {

        int remainB = remainA - b * 5;
        if (remainB < 0) break;

        for (int c = 0; c <= input[3]; c++)
        {

            int remainC = remainB - c * 10;
            if (remainC < 0) break;

            for (int d = 0; d <= input[4]; d++)
            {

                int remainD = remainC - d * 25;
                if (remainD < 0) break;

                use = a + b + c + d;
                if (remainD == 0 && maxUse < use)
                {

                    maxUse = use;
                    ret[0] = a;
                    ret[1] = b;
                    ret[2] = c;
                    ret[3] = d;
                }
            }
        }
    }
}

Console.WriteLine($"{ret[0]} {ret[1]} {ret[2]} {ret[3]}");
#endif
        }
    }
#if other
using System;

public class Program
{
    static void Main()
    {
        int[] xabcd = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int x = xabcd[0];
        int[] coins = { 1, 5, 10, 25 }, count = new int[4];
        for (int i = 0; i < 4; i++)
        {
            count[i] = xabcd[i + 1];
        }
        int[,] dp = new int[x + 1, 5];
        for (int i = 1; i <= x; i++)
        {
            dp[i, 0] = -1;
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = coins[i]; j <= x; j++)
            {
                if (dp[j - coins[i], 0] != -1 && dp[j, 0] <= dp[j - coins[i], 0] && dp[j - coins[i], i + 1] < count[i])
                {
                    for (int k = 0; k < 5; k++)
                    {
                        dp[j, k] = dp[j - coins[i], k];
                    }
                    dp[j, 0]++; dp[j, i + 1]++;
                }
            }
        }
        Console.Write($"{dp[x, 1]} {dp[x, 2]} {dp[x, 3]} {dp[x, 4]}");
    }
}
#endif
}
