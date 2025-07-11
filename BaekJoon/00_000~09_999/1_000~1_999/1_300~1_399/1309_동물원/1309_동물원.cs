using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 동물원
    문제번호 : 1309번

    dp 문제다
    점화식을 찾는데 고민 했다

    아이디어는 다음과 같다    
    사자를 2 * n에 n마리까지 배치할 수 있다
    여기서 맨 위에 줄이 추가된다고 생각했다
    그러면 맨 위에 줄만 보면 [ O, X ], [ X, O ], [ X, X ] 인 경우로 될 것이다
    여기서 O는 사자를 놓고, X는 안놓는다는 의미다

    이제 위에서 2번째 줄을 보면, 
    2번째 줄이 모두 비어있는 경우 [ O, X ], [ X, O ], [ X, X ] 3가지 모두 가능하다 
    반면 2번째 줄이 비어있지 않다면 [ O, X ] or [ X, O ] 중 하나만 가능하고
    [ X, X ]는 항상 가능하다 그래서 2가지 경우가 가능하다
    여기서 중복되는 경우는 없다!

    이제 2번째 줄이 비어있는 여부는 i - 2의 경우만 보면 될거라 생각했다
    2번째 줄이 차 있는 경우는 i - 1에서 i - 2 경우를 뺀 것이다

    그래서 dp[i] = (dp[i - 1] - dp[i - 2]) * 2 + 3 * dp[i - 2]
    로 해서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0419
    {

        static void Main419(string[] args)
        {

#if first
            int MOD = 9901;
            int n = int.Parse(Console.ReadLine());

            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = 3;

            for (int i = 2; i <= n; i++)
            {

                dp[i] = (dp[i - 1] - dp[i - 2]) * 2;
                dp[i] += dp[i - 2] * 3;
                dp[i] %= MOD;
            }

            Console.WriteLine(dp[n]);
#else

            int MOD = 9901;
            int n = int.Parse(Console.ReadLine());

            int[][][] dp = new int[n + 1][][];
            for(int i = 0; i <= n; i++)
            {

                dp[i] = new int[4][];
                for (int j = 0; j < 4; j++)
                {

                    dp[i][j] = new int[2];
                    if (i != n) Array.Fill(dp[i][j], -1);
                    else Array.Fill(dp[i][j], 1);
                }
            }

            Console.Write(DFS(0));

            int DFS(int _dep = 0, int _state = 0, int _pos = 0)
            {

                ref int ret = ref dp[_dep][_state][_pos];
                if (ret != -1) return ret;

                ret = 0;
                int nState = _state >> 1;

                if (_pos == 0)
                {


                    // 사자 미배치
                    ret += DFS(_dep, nState, 1);

                    // 위가 비었는지 확인해서 사자 배치
                    if ((_state & 1) == 0) ret += DFS(_dep, nState | 2, 1);
                }
                else
                {

                    // 사자 미배치
                    ret += DFS(_dep + 1, nState, 0);

                    // 위와 이전께 비었는지 확인해서 사자 배치
                    if ((_state & 1) == 0 && (_state & 2) == 0) ret += DFS(_dep + 1, nState | 2, 0);
                }

                ret %= MOD;
                return ret;
            }
#endif
        }
    }
}
