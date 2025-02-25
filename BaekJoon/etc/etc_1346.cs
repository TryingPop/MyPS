using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 19
이름 : 배성훈
내용 : 돌 게임 3
    문제번호 : 9657번

    게임 이론, dp 문제다.
    dp[i] = val를 i 번째 돌을 잡을 경우 승리 유무 val를 담았다.
    그래서 dp[i - 1], dp[i - 3], dp[i - 4] 모두 true인 경우
    어떻게 넘겨도 상대가 이기므로 false가 되고 이외는 지는 경로로 넘길 수 있으니 true가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1346
    {

        static void Main1346(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            // 승패 유무를 담는다.
            // arr[i] = true : i번 돌을 잡으면 이긴다.
            bool[] arr = new bool[Math.Max(n + 1, 5)];
            arr[1] = true;
            arr[3] = true;
            arr[4] = true;

            for (int i = 5; i <= n; i++)
            {

                // 어떻게 선택해도 다음턴 상대가 이기는 경로만 있으면 진다.
                if (arr[i - 1] && arr[i - 3] && arr[i - 4]) continue;
                arr[i] = true;
            }

            Console.Write(arr[n] ? "SK" : "CY");
        }
    }
}
