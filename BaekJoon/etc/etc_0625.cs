using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 재밌는 나머지 연산
    문제번호 : 28138번

    수학, 정수론, 소수판정 문제다
    처음에 모든 약수를 찾으려다가 크기 세팅을 잘못해서 인덱스 에러로 3번 틀렸다
    그리고 구지 약수들이 필요한가? 의문이들고 찾고나서 다시 이분탐색을 해야하므로
    약수들을 모으지 않고 바로 연산했다
    대소관계를 > 로 해야하는걸 >= 해서 1번 틀리고 통과했다

    아이디어는 다음과 같다
    input[0]을 나눴을 때 나머지가 input[1]이 되어야 하므로,
    찾을 숫자는 input[0] - input[1]을 나눴을 때 나머지가 0이어야한다
    그리고, input[0]을 나눌 때, 나머지가 input[1]이므로 찾을 숫자는 input[1]보다는 크다!
    input[0] - input[1]을 나룰 때, 나머지가 0이므로 input[0] - input[1]의 약수 중 하나이다
    그래서 약수를 찾는데, input[1]보다 큰 것만 찾았다

    여담으로 10^12 범위 안에서는 input[0] - input[1]의 약수의 개수는, 
    에라토스테네스의 체이론으로 접근하면 200만을 넘지 못한다
    그리디 알고리즘이랑, 컴퓨터 연산을 통해 1초안에 상한을 찾을 수 있지만, 
    해당 문제가 나오면 그때 할 예정이다
*/

namespace BaekJoon.etc
{
    internal class etc_0625
    {

        static void Main625(string[] args)
        {

            Solve();

            void Solve()
            {

                long[] input = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

                long chk = input[0] - input[1];
                long ret = 0;

                for (long i = 1; i <= chk; i++)
                {

                    if (i * i > chk) break;
                    if (chk % i != 0) continue;

                    if (i > input[1]) ret += i;
                    long other = chk / i;
                    if (other != i && other > input[1]) ret += other;
                }

                Console.WriteLine(ret);
            }
        }
    }

#if other
using System;
using System.Linq;

namespace IntegralTypes {
    class MainApp {
        static void Main(string[] args) {
            string sN1 = Console.ReadLine();
            string[] num1 = sN1.Split(' ');
            long N = Convert.ToInt64(num1[0]);
            long R = Convert.ToInt64(num1[1]);

            long m = 0; // 정답이 될 변수

            long N12 = Convert.ToInt64(Math.Sqrt(N)); // N의 제곱근

            long N1 = N - R; //N = mX + R, N - R = mX, 우리는 X를 구해보자

            for (long i = 1; i < N12 + 1; i++) { //0으론 나눌수 없다. N12까지 시행해야한다.
                if (0 == N1 % i) { // X값이 나왔다. m == i이다
                    if (i > R) { //나머지보다 작은값으로 나누면 나머지가 더 작아지겠지?
                        m += i;
                    }
                    if (i * i != N1 && N1 / i > R) { // 100 = 10 * 10, 15 / 2 = 7 ... 1, (15 - 1) / 2 = 7
                        m += N1 / i;
                    }
                }
            }

        Console.WriteLine(m);

        }
    }
}
#endif
}
