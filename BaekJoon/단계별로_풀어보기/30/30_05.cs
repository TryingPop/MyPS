using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 18
이름 : 배성훈
내용 : 동전 1
    문제번호  : 2293번

    동전 만들기
    메모리 제한 4mb << 너무 많이 쓰면 안된다! 
    1, 2, 5만 놓고 보자
    10 만드는 방법
    1 * 10
    1 * 8, 2 * 1
    1 * 6, 2 * 2
    1 * 4, 2 * 3
    1 * 2, 2 * 4
    2 * 5

    5 + 5
    1 * 5, 5 * 1
    1 * 3, 2 * 1, 5 * 1
    1 * 1, 2 * 2, 5 * 1
    5 * 2

    답이 안보인다
    점화식을 세워보자..
    a_n != a_(n-1) + a_(n-2) + a_(n_5) // 중복가능성!이 있다
    예를들어 
    n = 10이라 하면
    a_9 에는 5 * 1, 2 * 1, 1 * 2
    a_5 에는 2 * 1, 1 * 3
    이라 하면 각자 a_9 에 1을 추가하는 것이므로
    a_10 에 포함되는 5 * 1, 2 * 1, 1 * 3 이 같은 경우로 취급된다

    
    -> 해당 아이디어는 X 

    a_1 : 1 * 1
    a_2 : 1 * 2 ++ 2 * 1
    a_3 : 1 * 3 ++ 1 * 1, 2 * 1
    a_4 : 1 * 4 ++ 1 * 2, 2 * 1 ++ 2 * 2
    a_5 : 1 * 5 ++ 1 * 3, 2 * 1 ++ 1 * 1, 2 * 2 ++ 5 * 1
    a_6 : 1 * 6 ++ 1 * 4, 2 * 1 ++ 1 * 2, 2 * 2 ++ 1 * 1, 5 * 1 ++ 2 * 3
    a_7 : 1 * 7 ++ 1 * 5, 2 * 1 ++ 1 * 3, 2 * 2 ++ 1 * 1, 2 * 3 ++ 1 * 2, 5 * 1 ++ 2 * 1, 5 * 1
    a_8 : 1 * 8 ++ 1 * 6, 2 * 1 ++ 1 * 4, 2 * 2 ++ 1 * 2, 2 * 3 ++ 2 * 4 ++ 1 * 3, 5 * 1 ++ 1 * 1, 2 * 1, 5 * 1
    a_9 = 1 * 9 ...

    2, 3, 5를 놓고 보자;

    2 * 5
    2 * 2, 3 * 2
    2 * 1, 3 * 1, 5 * 1
    5 * 2

    ... 여전히 안보인다
    
    다른 사람 풀이를 봐서 해결했다
    앞에서 a_n != a_(n-1) + a_(n-2) + a_(n_5)
    점화식을 세웠는데 근사했지만 여기서는 정답과 '틀린' 공식이다!

    각 1, 2, 5 에 대해서 따로따로 순차적으로 진행함으로 겹치는 구간을 없앴다

    1인 경우에
    a_n += a_(n-1)

    2인 경우에
    a_n += a_(n-2)

    5인 경우에
    a_n += a_(n-5)
*/

namespace BaekJoon._30
{
    internal class _30_05
    {

        static void Main5(string[] args)
        {


            const int MAX = 10_000;
            // 입력 - 동전 개수, 목표 동전
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[] coins = new int[info[0]];

            int idx = 0;
            for (int i = 0; i < info[0]; i++)
            {

                int input = int.Parse(sr.ReadLine());

                if (input > MAX) continue;
                coins[idx++] = input;
            }

            sr.Close();

            int[] dp = new int[info[1] + 1];

            dp[0] = 1;

            for (int i = 0; i < idx; i++)
            {

                for (int j = coins[i]; j < dp.Length; j++)
                {

                    dp[j] += dp[j - coins[i]];
                }
            }

            // 출력
            Console.WriteLine(dp[info[1]]);
        }
    }
}
