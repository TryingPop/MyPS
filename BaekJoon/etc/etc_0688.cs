using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 12
이름 : 배성훈
내용 : 동전
    문제번호 : 9084번

    dp, 배낭 문제다
    아직 미숙해서 그런지 고민을 많이 했다

    먼저 배낭문제처럼 dp를 구했다(주석 부분)
    해당 방법의 최대 시간이 N^2 * C * T 이므로 시간 초과 날거 같았고
    여기서 n은 최대 무게, c는 코인의 수, t는 케이스 수이다

    주석 부분을 보면 원래는 역순으로 탐색해야하나, 배열이 2개이므로 정방향으로 탐색해도 된다
    그리고 쌓이는 형식을 보면 누적합임을 확인할 수 있다 
    그래서 누적합 처럼 식을 수정하니 N * C * T로 줄일 수 있었다
    이렇게 제출하니 68ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0688
    {

        static void Main688(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            // int[] dp1;
            // int[] dp2;

            int[] dp;
            int[] arr;

            int n, m;

            Solve();

            void Solve()
            {

                Init();
                int test = ReadInt();

                while (test-- > 0)
                {

                    Input();

                    SetDp();

                    sw.Write($"{dp[m]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput());
                sw = new(Console.OpenStandardOutput());

                dp = new int[10_001];

                // dp1 = new int[10_001];
                // dp2 = new int[10_001];

                arr = new int[20];
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                m = ReadInt();
            }

            void SetDp()
            {

                /*
                // 배낭 방법?
                // 이를 중복되는 부분 처리 해야한다!
                Array.Fill(dp1, 0);

                dp1[0] = 1;
                for (int i = 0; i < n; i++)
                {

                    for (int j = m; j >= 0; j--)
                    // for (int j = 0; j <= m; j++)
                    {

                        if (dp1[j] == 0) continue;

                        for (int k = 0; k <= m; k += arr[i])
                        {

                            int next = j + k;
                            if (next > m) break;
                            dp2[next] += dp1[j];
                        }

                        dp1[j] = 0;
                    }

                    int[] temp = dp1;
                    dp1 = dp2;
                    dp2 = temp;
                }
                */

                Array.Fill(dp, 0);
                dp[0] = 1;
                for (int i = 0; i < n; i++)
                {

                    for (int j = arr[i]; j <= m; j++)
                    {

                        dp[j] += dp[j - arr[i]];
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.Runtime.Intrinsics.Arm;

namespace Baekjoon
{
    class Progrma
    {
        static void Main(string[] args)
        {
            // 테스트 케이스 개수 입력 받기 (첫번째)
            int T = int.Parse(Console.ReadLine());

            for (int t = 0; t < T; t++)
            {
                int N = int.Parse(Console.ReadLine()); // 동전의 종류 수 입력 (첫 줄)
                // 동전의 가치를 배열로 저장하기
                string[] CoinsInput = Console.ReadLine().Split(); // 동전의 N가지 금액 (두 줄)
                int[] coins = new int[N]; // 코인을 받을 동전 배열
                for (int i = 0; i < N; i++)
                {
                    // 동전들 저장하기
                    coins[i] = int.Parse(CoinsInput[i]);
                }

                int M = int.Parse(Console.ReadLine()); // 목표 금액
                int[] dp = new int[M +1]; //M(1 ≤ M ≤ 10000)
                // 금액이 0인 경우 동전 하나도 사용하지 않은 가지 수
                // 찾아본 내용
                dp[0] = 1;

                // 동전 구하기 문제
                for (int i = 0; i < N; i++)
                {
                    for (int j = coins[i]; j <= M; j++)
                    {
                        dp[j] += dp[j - coins[i]];
                    }
                }
                Console.WriteLine(dp[M]); // 결과 출력
            }
        }
    }
}
#endif
}
