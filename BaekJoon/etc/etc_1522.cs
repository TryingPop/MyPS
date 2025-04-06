using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 5
이름 : 배성훈
내용 : 음식 평론가
    문제번호 : 1188번

    유클리드 호제법 문제다.
    아이디어는 다음과 같다.
    우선 n을 m으로 나눈다.
    x.y 형태로 나오는데 여기서 정수 부분 x는 그냥 주는게 가장 자르는 횟수가 적음을 그리디로 알 수 있다.
    그래서 n을 m으로 나눈 나머지에 대해 최솟값을 찾는 것과 같다.
    
    그래서 n / m = 0.y인 경우만 생각하자.
    n, m이 서로 소인 경우 소시지를 일렬로 세운 뒤 n / m의 길이 씩 잘라주는면,
    또한 n, m이 서로 소이고 유클리드 호제법으로 해당 과정을 진행해가면 
    남은 부분으로 n / m을 오로지 마지막에 1개만 만들 수 있음을 알 수 있다.
    그래서 m - 1회에 만들 수 있다.

    그리고 이제 m - 1회가 최소임을 보이자.
    우선 유클리드 호제법으로 n / m 씩 잘라갈 때 n, m이 서로 소이므로 마지막에 1번 나옴을 알 수 있다.
    그래서 n / m 정수 배수 길이 이외의 길이 a를 자르고 최소 횟수가 m - 1보다 적은 횟수로 전부 나눠줄 수 있다 가정하자.
    그러면 n - a 는 n / m의 정수배가 아니다.
    n - a = b라하면 a, b구간으로 나눌 수 있다.

    그러면 a구간이 (a / m) - 1회 미만으로 자를 수 있던, b구간이 (b / m) - 1회 미만으로 전부 자를 수 있다.
    해당 구간을 c라하자. 즉, c구간을 나눠주는데, c / m - 1 미만으로 해결된다.
    이후 c / m - 1회 미만으로 해결되므로 이는 명백히 n / m의 정수배로 잘라서는 c / m - 1 미만으로 만들 수 없는 경우다.
    즉 이번에도 n / m의 정수배가 아닌 다른 길이로 잘라야 한다.
    이렇게 구간을 계속해서 줄여나가면 페르마 강하법으로 어느 순간 구간에 내놓는 횟수는 1이되고,
    구간의 길이 d구간을 d / m - 1은 2로 나눠 떨어져야 하는데 우리는 n / m배수가 아니게 잘라왔고 이는 모순이 생김을 알 수 있다.
    귀류법으로 결국 m - 1이 최소임을 알 수 있다.

    이제 n과 m이 서로 소인 경우를 보면,
    앞과 똑같이 진행하는 경우 gcd를 n, m의 최대 공약수라 할 때
    gcd x ((m / gcd) - 1)회로 자름을 알 수 있다.

    이 값이 최소임을 보이면 된다.
    여기서는 n / gcd, m / gcd가 서로 소이므로 앞과 같은 논리를 이용하면 된다.
    이렇게 최소임을 확인하고 제출하니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1522
    {

        static void Main1522(string[] args)
        {

            int n, m;

            Input();

            GetRet();

            void GetRet()
            {

                n %= m;

                int ret;
                if (n == 0)
                {

                    // 나눠떨어지는 경우 gcd(0, m) = m이 자명하므로 
                    // 반례처리
                    ret = 0;
                }
                else
                {

                    int gcd = GetGCD(n, m);
                    m /= gcd;
                    ret = (m - 1) * gcd;
                }

                Console.Write(ret);

                int GetGCD(int _f, int _t)
                {

                    while (_t > 0)
                    {

                        int temp = _f % _t;
                        _f = _t;
                        _t = temp;
                    }

                    return _f;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
            }
        }
    }
}
