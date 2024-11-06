using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 공룡 게임
    문제번호 : 20544번

    dp 문제다
    1차원 배열로 접근하다가 여러 번 틀렸다

    시간을 많이 먹기에 여기 사이트를 참고해서 풀었다
    https://9327144.tistory.com/entry/BOJ-20544-%EA%B3%B5%EB%A3%A1%EA%B2%8C%EC%9E%84Java?category=945534
    일차원 배열로 접근한게 틀렸다
    N + 1번째가 땅인 경우를 확인하는게 중요하다

    N번째 지점이 바닥인 경우는 
    N - 3 이 바닥 & 2단 점프한 경우
        높이 1, 1 을 점프 혹은 1, 2를 점프, 2, 1을 점프
        단 높이 2가 안나왔다면 1, 1 경우는 불가능

    N - 2 이 바닥 & 1단 점프한 경우
        높이 1을 점프 혹은 높이 2를 점프
        단 높이 2가 안나왔다면 1은 불가능

    N - 1 이 바닥인 경우
        유일한 경우

    이를 역순으로 찾아간다
    dp[i, 0] : i 번째 바닥, 높이 2인 장애물이 안나온경우
        // 이는 각각 앞에 빈땅 에서 그냥 오는 경우
        // 높이 1짜리를 1단 점프해서 오는 경우, 
        // 높이 1, 1짜리를 2단 점프해서 오는 경우
        -> dp[i, 0] = dp[i - 1, 0] + dp[i - 2, 0] + dp[i - 3, 0]

    dp[i, 1] : i번째가 바닥, 이제까지 높이 2인 장애물이 나온 경우
        // 해당 경우로 오는 방법은 정해져 있다
        // dp[i - 1, 1] 에서 오는경우
        // dp[i - 2, 0] 에서 2짜리를 점프해서 오는 경우
        // dp[i - 2, 1] 에서 1 혹은 2 짜리를 1단 점프해서 오는 경우
        // dp[i - 3, 0] 에서 1, 2나 2, 1을 2단 점프해서 오는 경우
        // dp[i - 3, 1] 에서 1, 1이나 1, 2나 2, 1을 2단 점프해서 오는 경우
        -> dp[i, 1] = dp[i - 1, 1] + dp[i - 2, 0] + dp[i - 2, 1] * 2 + dp[i - 3, 0] * 2 + dp[i - 3, 1] * 3
*/

namespace BaekJoon.etc
{
    internal class etc_0262
    {

        static void Main262(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            // 뒤에서부터 땅을 만들면서 진행한다
            // i, 0 : i가 바닥 높이가 이제까지 2인 장애물이 나오지 않은 경우
            // i, 1 : i가 바닥 높이가 이제까지 높이가 2인 장애물이 나온 경우
            long[,] dp;

            if (n < 4) dp = new long[5, 2];
            else dp = new long[n + 2, 2];


            // 초기 가능한 경우
            dp[0, 1] = 1;
            dp[1, 0] = 1;
            dp[2, 0] = 1;
            dp[3, 0] = 2;
            dp[3, 1] = 1;

            for (int i = 4; i <= n + 1; i++)
            {


                dp[i, 0] = dp[i - 1, 0] + dp[i - 2, 0] + dp[i - 3, 0];
                dp[i, 0] %= 1_000_000_007;
                dp[i, 1] = dp[i - 1, 1] + dp[i - 2, 0] + dp[i - 2, 1] * 2 + dp[i - 3, 0] * 2 + dp[i - 3, 1] * 3;
                dp[i, 1] %= 1_000_000_007;
            }

            Console.WriteLine(dp[n + 1, 1]);
        }
    }
}
