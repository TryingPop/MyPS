using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 거스름 돈
    문제번호 : 14916번

    수학, dp, 그리디 알고리즘 문제다
    정수론 아이디어를 썼다(합동식 해 찾기)
    2 와 5는 서로소이므로 음의 정수를 2와 5에 적절히 곱하면 모든 정수를 만들 수 있다
    그래서 자연수로 연속한 10개의 수가 나오는 경우를 찾고 연속한 10개의 끝값보다 큰 값에 대해서는
    5원짜리 동적을 2개 주면서 10씩 빼면서 연속한 10개의 범위가 나올때까지 진행한다
    그리고 해당 인덱스 값을 주면 최소값이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0311
    {

        static void Main311(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int[] dp = new int[14] { 0, -1, 1, -1, 2, 1, 3, 2, 4, 3, 2, 4, 3, 5 };

            int ret = 0;
            if (n >= 14)
            {

                n -= 4;
                int r = n % 10;
                n /= 10;
                ret = 2 * n + dp[r + 4];
            }
            else ret = dp[n];

            Console.WriteLine(ret);
        }
    }
}
