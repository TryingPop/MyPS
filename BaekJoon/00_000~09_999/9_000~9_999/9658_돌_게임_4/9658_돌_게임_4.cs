using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 9
이름 : 배성훈
내용 : 돌 게임 4
    문제번호 : 9658번

    둘이 완벽하게 게임을 한다기에, 베스킨 라빈스 게임처럼 풀었다

    한번에 돌을 1개, 3개, 4개씩 가져오므로 
    1개 가져왔을 때 상대가 지는 경우, 3개 가져왔을 때 상대가 지는경우, 4개 가져왔을 때 상대가 지는 경우인지 확인하면서
    상대가 지는 경우로 돌을 가져오면 해당 턴의 사람이 이긴다
    
    그래서 dp의 인덱스를 현재 돌의 개수라하고, 값은 현재 돌을 가져가는 사람이 이기는 경우면 true,
    지는 경우면 false를 담았다

    그리고 1 ~ 5까지 채우고 찾아가는 형식을 취했다
*/

namespace BaekJoon.etc
{
    internal class etc_0008
    {

        static void Main8(string[] args)
        {

            bool[] dp = new bool[1001];

            int n = int.Parse(Console.ReadLine());

            // 돌의 개수가 2, 4, 5일 때는 현재 턴 잡은 사람이 이긴다!
            // 1, 3은 현재 턴 잡은 사람이 진다
            dp[2] = true;
            dp[4] = true;
            dp[5] = true;

            for (int cur = 6; cur <= n; cur++)
            {

                // 내가 1개를 집으면 상대로 턴이 넘어가고, 이때 dp[cur -1] 이 false면 
                // 해당 경우는 내가 이긴다
                // 비슷하게 3, 4도 조사한다
                if (!dp[cur - 1] || !dp[cur - 3] || !dp[cur - 4]) dp[cur] = true;
            }

            // 선턴이 SK이므로 선턴이 이기면 SK출력, 지면 CY 출력
            if (dp[n]) Console.WriteLine("SK");
            else Console.WriteLine("CY");
        }
    }
}
