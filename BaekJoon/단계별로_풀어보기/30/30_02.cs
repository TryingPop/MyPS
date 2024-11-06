using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 17
이름 : 배성훈
내용 : 행렬 곱셈 순서
    문제 번호 : 11049번

    크기가 N X M 행렬 A와 크기가 M X L 인 행렬 B의 곱 AB에 필요한 연산은 N X M X L이다
    앞의 문제를 이용해 같은 방법으로 풀었다
*/

namespace BaekJoon._30
{
    internal class _30_02
    {

        static void Main2(string[] args)
        {

            const int MAX = int.MaxValue;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            // 곱할 행렬들 저장
            int[][] matrix = new int[len][];

            // i 부터 j 까지 곱했을 때 최소값 저장할 dp
            int[][] dp = new int[len][];

            for (int i = 0; i < len; i++)
            {

                matrix[i] = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                dp[i] = new int[len];
            }

            sr.Close();

            // dp에 사용할 i, j 의 차이
            for (int diff = 1; diff < len; diff++)
            {

                // 시작 인덱스
                for (int startIdx = 0; startIdx < len - diff; startIdx++)
                {

                    int endIdx = startIdx + diff;
                    // 조건이 최악의 경우도 2^31 - 1 보다 작거나 같다고 했으므로 2^31 -1 값을 최대값으로 넣어준다
                    dp[startIdx][endIdx] = MAX;

                    // 중간 인덱스
                    for (int mid = 0; mid < diff; mid++)
                    {

                        int midIdx = startIdx + mid;

                        int _1 = dp[startIdx][endIdx];
                        // 첫 번째 줄은 이전 연산량 중 가장 작은 값
                        // 두 번째 줄은 이번 행렬 곱에 필요한 연산량
                        int _2 = dp[startIdx][midIdx] + dp[midIdx + 1][endIdx] 
                            + matrix[startIdx][0] * matrix[midIdx][1] * matrix[endIdx][1];

                        // 둘 중에 작은값 저장
                        dp[startIdx][endIdx] = Math.Min(_1, _2);
                    }
                }
            }

            Console.WriteLine(dp[0][len -1]);
        }
    }
}
