using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 25
이름 : 배성훈
내용 : 색상환
    문제번호 : 2482번

    인접하지 않은 색상 찾기

        20개 중에 10개는 >>> 2, 4, 6, ...., 20으로 택해야한다
        혹은 1, 3, 5, 7, ... , 19 총 2가지 존재한다

        3개 중에 2개면 0개

        5개 중에 2개면
        1, 3
        1, 4

        2, 4,
        2, 5
    
        3, 5

        총 5개

    풀이는 간단하게 나왔다
    dp를 N개의 색상에서 K개를 고른다고 하면,
    LCS처럼 해당 경우에서 색칠할 수 잇는 맨 뒤에 색칠 or 색칠 X로 나뉜다
    그러면 dp[N][K] = dp[N - 1][K] + dp[N - 2][K - 1];
    점화식을 얻고,

    N >= 4이고 연산 중에 N - 2가 나오므로 5에서 이상한 곳에 안나오게 하기위해 초기값 채우는 N은 3, 4를 택했다
    그리고, K == 1, K == 0일때는 각각 N개, 0개이므로 이부분도 초기값으로 설정했다
    마지막으로 K * 2 > N인 경우 띄엄띄엄 선택할 수 없으므로 바로 0으로 탈출 시켰다

    탐색 방법은 DFS로 했다!
*/
namespace BaekJoon._41
{
    internal class _41_06
    {
        static void Main6(string[] args)
        {

            int N = int.Parse(Console.ReadLine());
            int K = int.Parse(Console.ReadLine());

            int[][] dp = new int[N + 1][];
            for (int i = 0; i < N + 1; i++)
            {

                dp[i] = new int[N + 1];
                Array.Fill(dp[i], -1);
            }

            dp[4][1] = 4;
            dp[4][2] = 2;
            dp[4][0] = 0;
            dp[3][0] = 0;
            dp[3][1] = 3;
            DFS(dp, N, K);

            Console.WriteLine(dp[N][K]);
        }

        static void DFS(int[][] _dp, int _n, int _k)
        {

            const int DIVIDE = 1_000_000_003;

            if (_dp[_n][_k] != -1) return;

            if (_k == 1)
            {

                _dp[_n][_k] = _n;
                return;
            }
            else if (_k == 0)
            {

                _dp[_n][_k] = 0;
                return;
            }
            else if (_k * 2 > _n)
            {

                _dp[_n][_k] = 0;
                return;
            }
           
            DFS(_dp, _n - 1, _k);
            DFS(_dp, _n - 2, _k - 1);
            int chk = _dp[_n - 1][_k] + _dp[_n - 2][_k - 1];


            chk %= DIVIDE;

            _dp[_n][_k] = chk;
        }
    }
}
