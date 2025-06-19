using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 13
이름 : 배성훈
내용 : 턴 게임
    문제번호 : 12934번

    수학, 그리디 알고리즘 문제다
    문제 아이디어는 다음과 같다

    유효한 경기인지 확인한다
    두 명의 스코어 합이 1 + 2 + ... + n 의 값이므로
    누적합이 되는지 먼저 판별했다
    비정상적인 경기라면 합이 누적합과 값이 다르다

    합으로 판별했기에 개별 점수가 가질 수 있는지 의문을 가질 수 있다
    이는, 1 ~ n까지 원하는 숫자를 쓸 수 있어 합만 성립하면 해당 스코어는 보장된다
    실제로 합이 55인 경우 20, 35의 경우를 예를들어 보자
    그러면 1 + 2 + ... + 10 = 55이므로 1 ~ 10의 숫자를 마음대로 이용할 수 있다

    그리디하게 20점을 만들어보자
    20 - 10 = 10,
    10 - 9 = 1,
    그러면 1, 9, 10이면 20점을 만든다 물론 다른 경우도 존재한다

    35점도 35 - 10 = 25,
    25 - 9 = 16, 16 - 8 = 8, 8 - 7 = 1
    1, 7, 8, 9, 10 점을 이용하면 35점을 만든다
    그리고 5개의 숫자 미만으로는 35점을 못만듦을 알 수 있다 
    이는 최대값을 갖고 찾아갔기에 자명하다
    이러한 아이디어로 문제를 풀었다
    
    그래서 처음에는 dp로 누적합의 가능한 경우를 다찾고, 
    info[0] + info[1]의 값이 있는지 이분탐색으로 찾았다

    그런데, 이후 이분탐색은 불필요한 연산임을 느꼈고,
    sum으로 더하면서 바로 찾아가는 방법을 이용했다

    확실히 이분탐색을 안하고, 
    200만 경우를 저장안하니 메모리도 엄청 적게 먹는다
    76 -> 64ms
*/

namespace BaekJoon.etc
{
    internal class etc_0215
    {

        static void Main215(string[] args)
        {

            long[] info = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            long chk = info[0] + info[1];

            int idx = 0;
            long sum = 0;
            // long[] dp = new long[2_000_001];
            for (int i = 1; i < 2_000_001; i++)
            {

                // dp[i] = dp[i - 1] + i;
                sum += i;
                // if (dp[i] < chk) continue;
                if (sum < chk) continue;
                idx = i;
                break;
            }

            if (sum != chk) Console.WriteLine(-1);
            else
            {

                int ret = 0;
                chk = info[0];
                while(chk > 0)
                {

                    chk -= idx--;
                    ret++;
                }

                Console.WriteLine(ret);
            }
#if other
            int left = 0;
            int right = dp.Length - 1;

            while(left <= right)
            {

                int mid = (left + right) / 2;

                if (chk < dp[mid]) right = mid - 1;
                else left = mid + 1;
            }

            int find = left - 1;

            if (dp[find] != chk) Console.WriteLine(-1);
            else
            {

                int ret = 0;
                long calc = info[0];
                for (int i = find; i >= 0; i--)
                {

                    if (calc <= 0) break;
                    calc -= i;
                    ret++;
                }

                Console.WriteLine(ret);
            }
#endif
        }
    }
}
