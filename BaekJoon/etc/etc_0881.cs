using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 15
이름 : 배성훈
내용 : Wiring
    문제번호 : 10915번

    정수론 문제다
    문제를 잘못 이해해 한참을 고민했다;

    아이디어는 다음과 같다
    최대 N^2번 연산하는데, 

    1, 2, 3, ..., n, 1, 2, ..., n, ..., 순서로 이동하므로
    도착지점을 구하면 n 번째는 (n x (n + 1)) / 2이다
    
    N이 홀수의 경우 N이 홀수이므로 2로 나눠떨어지지 않고
        N x (N + 1) / 2에서 (N + 1)가 2로 나눠떨어진다
        즉 N의 배수다 그래서 초기위치에 있고 많아야 N번 연산하면 되는데
        N 번째는 재자리 이동이므로 무시하고 N - 1 번 연산하면 된다

        그리고 N이 홀수일 때 N - 1, 1번째 연산을 비교해보면

        N - 1번 연산은 0에서 역방향으로 N - 1만큼 떨어진 곳 1이다
        1번 연산은 0 에서 1만큼 떨어진 곳이므로 둘이 겹친다

        이렇게 귀납적으로 진행하면 
        N - i번 연산과 i번 연산은 겹침을 알 수 있다
        (N이 홀수다!)
    
        그래서 많아야 (N - 1) / 2 개의 철사를 두르면 된다
        도착지점으로 p x ((p + 1) / 2) == q x ((q + 1) / 2) (MOD N)인
        0 ~ N - 1 사이의 p, q를 보면 (p - q) x ((p + q + 1) / 2) == 0 (MOD N)
        으로 변경가능하고 p == q 이거나 p + q == N - 1일 때 밖에 없다
        그래서 결국 0 ~ (N - 1) / 2에서는 겹치는게 없다!
        그래서 홀수일 때는 총 (N - 1) / 2이다
    
    N이 짝수일때도 비슷하게 구하면
    짝수일 때는 N - 1개임을 알 수 있다

    /// 문제를 잘못 이해해서
    Pi - 1 ~ Pi를 잇는게 아닌 Pi가 체크되었는지 확인했다;
    그러니 값을 F(n x m) 이라 하고 gcd(n, m) = 1일 때
    F(n x m) = F(n) x F(m) 이고, p이 홀수인 소수면 F(p) = (p + 1) / 2
    까지 얻었다 그런데, F(p^k), k > 1인경우 규칙을 못찾아 엄청나게 고민했고
    다른 사람 제출한 코드 길이를 보니 잘못 이해한건가 싶어 문제를 다시 읽어 풀었다;
*/

namespace BaekJoon.etc
{
    internal class etc_0881
    {

        static void Main881(string[] args)
        {

            long n = long.Parse(Console.ReadLine());

            if (n % 2 == 1) Console.Write((n - 1) / 2);
            else Console.Write(n - 1);
        }
    }
}
