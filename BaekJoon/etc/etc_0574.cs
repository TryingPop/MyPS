using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024, 4. 19
이름 : 배성훈
내용 : 창문
    문제번호 : 13183번

    수학, 조합론, 확률론 문제다
    정답 식 도출이 주된 문제다
    정답 코드는 매우 간결하다;

    아이디어는 다음과 같다
    창문 크기를 설정하는 것은 가로의 서로 다른 꼭짓점 2개를 선택하고, 
    세로의 서로 다른 꼭짓점 2개를 선택해야한다
    가로, 세로는 서로 독립되어져 있다
    E(XY) = E(X) * E(Y)

    이에 세로를 임의의 두 점으로 고정했다고 생각하고
    가로의 경우를 살펴봤다

    그러면 전체 가로 길이가 n일 때, 가로를 길이별로 보면, 1짜리는 n개, 2짜리는 n - 1개, 3짜리는 n - 2개이고
    기대비용을 보면
        9 * n + 18 * (n - 1) + ... + 9 * w * 1 
            = (sig (9 * i * (n + 1 - i))) / (n * (n + 1) / 2), 1 <= i <= n
    까지로 할 수 있다
    여기서 sig는 덧셈 합의 기호 시그마를 나타내고 뒤의 1 <= i <= n 은 1부터 n까지라고 보면 된다

    이를 계산하면 가로의 길이가 n일 때 구매 기대값은 (n + 2) * 3을 얻는다
    세로도 똑같이 얻고 세로의 길이를 m이라하면 (m + 2) * 3

    즉, 가로 n, 세로 m일 때 기대값은 (n + 2) * (m + 2)가 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0574
    {

        static void Main574(string[] args)
        {

            const long MOD = 1_000_000_007;
            long[] input = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

            Solve();

            void Solve()
            {

                input[0] = (input[0] + 2) % MOD;
                input[1] = (input[1] + 2) % MOD;

                long ret = (input[0] * input[1]) % MOD;
                Console.WriteLine(ret);
            }

        }
    }
}
