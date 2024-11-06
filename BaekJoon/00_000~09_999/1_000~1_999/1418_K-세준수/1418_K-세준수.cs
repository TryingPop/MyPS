using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 14
이름 : 배성훈
내용 : K - 세준수
    문제번호 : 1418번

    그냥 매번 소수 판정하고 소수이면 소수들을 모아둔다
    그리고, 합성수인 경우 모아둔 소수들로 나누면서
    만약 k보다 큰 소수로 안나누어 떨어지면 + 1 연산을 하는 식으로 코드를 짜면 시간초과 나온다!
    수의 범위가 10만이고, 10만 이하 소수들은 1만개 가까이 있다

    그래서 dp아이디어를 이용했다
    만약 제곱근 보다 작은 수로 나누어 떨어질 때, 큰 쪽을 본다
    해당 큰 쪽의 k세준수 여부를 계승한다!
    안나누어떨어지면 소수이므로 k보다 큰지만 판별하면 된다

    k 가 n보다 클 수 있어서 인덱스 에러가 떴다

    실버 5라서 금방 풀릴 줄 알았는데,
    푸는데 시간이 많이 걸렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0031
    {

        static void Main31(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            bool[] accept = new bool[n + 1];

            for (int i = 1; i <= k; i++)
            {

                // i > n 보다 큰 경우가 있다;
                if (i > n) break;
                // k 이하인 수들은 모두 k-세준수가 된다
                accept[i] = true;
            }

            for (int i = k + 1; i <= n; i++)
            {

                // 에라토스테네스의 체 판정
                // 원리는 i가 합성수라면, sqrt값 보다 작거나 같은 값인 원소를 적어도 1개 포함한다이다!
                // 현대 대수학의 관점으로 보면 당연한 결과이다
                // A(>= 2)라는 자연수를 두 자연수의 합으로 표현할 때, 
                // 적어도 하나는 A의 반보다 작거나 같다
                int sqrt = (int)Math.Sqrt(i);
                int calc = i;
                
                for (int p = 2; p <= sqrt; p++)
                {

                    // 소수 판정
                    if (calc % p == 0)
                    {

                        // 합성수 일 때, 큰 쪽의 k-세준수 여부를 계승
                        calc /= p;
                        accept[i] = accept[calc];
                        break;
                    }
                }

                // 소수가 아니므로 false
            }

            // 결과를 센다
            int ret = 0;
            for (int i = 1; i <= n; i++)
            {

                if (accept[i]) ret++;
            }

            Console.WriteLine(ret);
        }
    }
}
