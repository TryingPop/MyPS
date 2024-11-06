using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 25
이름 : 배성훈
내용 : RGB 거리 2
    문제번호 : 17404번

    처음에 인접한 세 개의 색상이 모두 달라야하는 줄 알았다
    그래서 /// \\\ 형태로 연산을 했고, 틀렸다

    조건을 다시 보니, 그냥 인접한 색상이랑만 다르면 된다
    둥근 띠 형태라서 처음과 끝도 달라야한다!

    이후에는 그냥 시작지점과 n번째 위치별로 저장할까 했는데
    이 경우 3^1000승이라 엄청난 메모리를 사용하고,

    다음으로 도착지점에 최소비용을 dp로 잡을까 생각했다
    이는 3 * 1000으로 메모리에는 이상없으나 마지막에 인접 조건에 맞는지 찾기가 힘들다

    그래서, dp를 i에서 시작해서 j에 끝날 때, 최소 비용이 담기게 설정했다
    입력 받으면 매번 갱신된다

    dp의 크기는 3 * 3 = 9가 되고, 다음 dp[0][2]는 dp[0][1]에서 2로가는 값과, dp[0][0]에서 2로 가는 값 중 작은게 된다
    비교를 dp[i][j] j = 0, 1, 2에서 해줘야하기에 갱신은 마지막에 했다
*/

namespace BaekJoon._41
{
    internal class _41_05
    {

        static void Main5(string[] args)
        {

            const int INF = 2_000_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            int[][] dp = new int[3][];

            for (int i = 0; i < 3; i++)
            {

                dp[i] = new int[3];
            }

            int[] inits = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {

                    if (i == j) dp[i][j] = inits[i];
                    else dp[i][j] = INF;
                }
            }

            for (int i = 1; i < len; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                for (int j = 0; j < 3; j++)
                {

                    int min0 = Math.Min(dp[j][1] + temp[0], dp[j][2] + temp[0]);
                    int min1 = Math.Min(dp[j][2] + temp[1], dp[j][0] + temp[1]);
                    int min2 = Math.Min(dp[j][1] + temp[2], dp[j][0] + temp[2]);

                    dp[j][0] = min0;
                    dp[j][1] = min1;
                    dp[j][2] = min2;
                }
            }

            int min = dp[0][1];
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {

                    if (i == j) continue;
                    if (min > dp[i][j]) min = dp[i][j];
                }
            }

            Console.WriteLine(min);
        }
    }
}
