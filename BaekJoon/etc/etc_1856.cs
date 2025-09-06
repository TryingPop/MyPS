using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 2
이름 : 배성훈
내용 : 고양이 목에 리본 달기
    문제번호 : 26093번

    dp 문제다.
    변수 범위로 n x k 의 방법이 유효하다.
    dp[i][j] = val를 i번 고양이까지 선택했고,
    j번 리본을 선택할 때 최대값 val가 담기게 한다.

    그러면 dp[i][j] = Max(dp[i - 1][k]) + ribon[j], k ≠ j이다.
    여기서 i번들 모두 찾고 i + 1을 찾기에, 1차원으로 축소할 수 있다.
    이제 Max(dp[i - 1][k])을 찾는다 naive하게 찾으면 k^2 x n으로 시간초과 난다.
    필요한 것은 가장 큰 것이다.

    이도 배열에 저장한다면 쉽게 찾을 수 있다.
    매 경우 최댓값을 즉 O(k log k)의 시간에 찾을 수 있다.
    그런데 필요한 것은 2개이므로 2개의 최댓값만 저장하는 배열을 만든다.
    그러면 매번 많아야 4번 연산을 한다. 그래서 O(k)의 시간에 찾을 수 있다.
    이후 해당 점화식을 진행해가면서 최댓값을 출력하니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1856
    {

        static void Main1856(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();

            int[] ribon = new int[k];
            // Max 2개를 알아야 한다!
            int[] dp = new int[k];
            int[] max = new int[2];

            for (int i = 0; i < n; i++)
            {

                FillRibon();

                if (i != 0) FindMax();

                AddDp();
            }

            FindMax();

            Console.Write(max[0]);

            void AddDp()
            {

                for (int i = 0; i < k; i++)
                {

                    int add = dp[i] == max[0] ? max[1] : max[0];
                    dp[i] = add + ribon[i];
                }
            }

            void FindMax()
            {

                max[0] = 0;
                max[1] = 0;

                for (int i = 0; i < k; i++)
                {

                    if (max[0] < dp[i])
                    {

                        max[1] = max[0];
                        max[0] = dp[i];
                    }
                    else if (max[1] < dp[i]) max[1] = dp[i];

                }
            }

            void FillRibon()
            {

                for (int i = 0; i < k; i++)
                {

                    ribon[i] = ReadInt();
                }
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
