using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. -
이름 : 배성훈
내용 : 타일 채우기
    문제번호 : 2133번

    동적 계획법 문제 풀기전에 미리 풀어볼 문제!
    왼쪽에 이어붙이는 형식으로 했다
    2배수마다 2개씩 새로운 모양이 나온다

        - -     |=|
        |=|     - -

    와 같은 형태들이 추가된다
*/

namespace BaekJoon.etc
{
    internal class etc_0098
    {

        static void Main98(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int[] dp = new int[n + 1];

            dp[0] = 1;

            if (n >= 2) dp[2] = 3;
            for (int i = 4; i <= n; i += 2)
            {

                // 왼쪽에 이어붙이기 형식으로 한다!
                for (int j = 0; j < i; j += 2)
                {

                    if (j == 0) dp[i] += 2;
                    else if (j == 2) dp[i] += 3 * dp[i - 2];
                    else dp[i] += 2 * dp[i - j];
                }
            }

            Console.WriteLine(dp[n]);
        }
    }
}
