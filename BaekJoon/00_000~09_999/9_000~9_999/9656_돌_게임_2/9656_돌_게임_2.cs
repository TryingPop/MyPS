using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 14
이름 : 배성훈
내용 : 돌 게임 2
    문제번호 : 9656번

    dp를 이용한 풀이!

    다시 보니 홀짝으로.. 승패가 정해지는거 같다
    어차피 가져갈 수 잇는 것은 홀수개이다;
    1번 왕복 할 때마다 짝수개씩 줄어든다
*/

namespace BaekJoon.etc
{
    internal class etc_0029
    {

        static void Main29(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            bool[] dp = new bool[1_000 + 1];

            // 해당 턴에 이기는 사람 true
            dp[2] = true;
            dp[4] = true;

            for (int i = 5; i <= n; i++)
            {

                // 상대가 지는 경우로 갈 수 있다면, 해당 턴을 잡은 사람이 이긴다
                if (!dp[i - 1] || !dp[i - 3]) dp[i] = true;
            }

            Console.WriteLine(dp[n] ? "SK" : "CY");
        }
    }
}
