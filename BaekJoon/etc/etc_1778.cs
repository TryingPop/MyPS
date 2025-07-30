using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 19
이름 : 배성훈
내용 : 포화 이진 트리 도로 네트워크
    문제번호 : 12888번

    dp 문제다.
    이전 경로의 최소 경로에 추가하는식이 좋다고 추론했다.
    그래서 확장하는 식으로 최대한 많이 지나가게 하며 찾았다.
    
    dp[i] = dp[i - 1] * 2 + (i & 1 == 0 ? 1 : -1)
    의 규칙이 보였다.

    왜냐하면 홀수번째의 루트를 지나는 경로는 루트 -> 리프로 가는 형태거나
    리프 -> 루트로 가는 형태다.
    반면 짝수로 가게되면 이는 리프 -> 리프로 가는 경로가 생겨난다.

    그래서 이렇게 제출하니 이상없이 통과한다.
    0인 경우 노드 1개뿐이므로 1이다.
    이를 간과해 여러 번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1778
    {

        static void Main1778(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            long[] dp = new long[n + 1];
            dp[0] = 1;
            if (n > 0) dp[1] = 1;
            for (int i = 2; i <= n; i++)
            {

                if ((i & 1) == 0) dp[i] = dp[i - 1] * 2 + 1;
                else dp[i] = dp[i - 1] * 2 - 1;
            }

            Console.Write(dp[n]);
        }
    }
}
