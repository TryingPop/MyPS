using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. -
이름 : 배성훈
내용 : 4xn 타일링
    문제번호 : 11333번

    dp 문제다
    아이디어는 다음과 같다
    1 x 3, 3 x 1의 크기이고
    1 = gcd(3, 4)이므로, 
    모두 채우는 경우는 3의 배수일 때만 가능하다
    그래서 3배수에 한해서 경우의 수를 찾으면 된다

    간단하게 길이가 3 x n인 경우의 수f(n)는
    i는 1 이상인 자연수라하고 
    길이 k짜리를 쪼갤 수 없는 k길이를 채우는 경우의 수라 하면
    f(n) = f(0) * g(n) + f(1) * g(n - 1) + ... + f(n) * g(0)
    식이 성립한다

    g(n)을 풀어보면
    g(0) = 1, g(1) = 3, g(2) = 4, g(3) = 6, ..., g(k) = 2k
    가 된다
    k 가 2 이상인 경우를 보면
    양 끝에 세로로 놓고, 남은 하나를 세로를 배치하는 경우의 수는 왼쪽끝이거나 
    가로로 배치하는 것들의 사이 오른쪽 끝 이렇게 존재한다
    k개의 경우의 수가 나온다

    만약 세로로 배치하는게 4개 이상이면 
    이는 분할이 되어 g의 정의에 만족하지 않는다

    위 아래로 가로와 세로를 배치할 수 있으므로 2배해주면 2k가 된다

    그러면 다음 점화식이 나온다
        f(n) = 3 f(n - 1) + 4f(n - 2) + ... + 2n f(0)
        => f(n) - f(n - 1) = 2 f(n - 1) + 4 f(n - 2) + ... 2n f(0)
        => (f(n + 1) - f(n))- (f(n) - f(n - 1)) = 2 f(n) + 2 f(n - 1) + ... + 2 f(0)
        => f(n + 1) - 2 f(n) + f(n - 1) = 2 f(n) + f(n) - 2 f(n - 1) + f(n - 2)
        => f(n + 1) = 5f(n) - 3 f(n - 1) + f(n - 2)
    를 얻는다

    그래서 dp를 1만 채우고 다음 식을 채우니 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0955
    {

        static void Main955(string[] args)
        {

            long MOD = 1_000_000_007;
            StreamReader sr;
            StreamWriter sw;

            long[] dp;

            Solve();
            void Solve()
            {

                Init();

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int n = ReadInt();
                    sw.Write($"{dp[n]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                dp = new long[10_001];
                dp[0] = 1;
                dp[3] = 3;
                dp[6] = 13;
                dp[9] = 57;

                for (int i = 12; i <= 10_000; i += 3)
                {

                    dp[i] = (5 * dp[i - 3] - 3 * dp[i - 6] + dp[i - 9]) % MOD;
                    if (dp[i] < 0) dp[i] += MOD;
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
