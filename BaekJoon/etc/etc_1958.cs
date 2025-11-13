using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 31
이름 : 배성훈
내용 : 피보나치 치킨
    문제번호 : 13270번

    수학 문제다.
    F를 F(0) = F(1) = 1, F(2) = 2인 피보나치 수열이라하자.
    먼저 n > 0일 때 F(n + 1) > 1에 대해 F(n) / F(n + 1) >= 0.5임을 확인하자.

    피보나치 점화식에 의해 F(n) + F(n + 1) = F(n + 2)이다.
    이로 F(0) = F(1) = 1, F(2) = 2라 하면 F(n) < F(n + 1)임을 알 수 있다.
    
    그래서 준식을 확인하면
    F(n) / F(n + 1) = F(n) / (F(n) + F(n - 1)) >= F(n) / (F(n) + F(n)) = 0.5
    그래서 2개씩 최대한 엮는게 최소임을 알 수 있다.

    최대는 3개씩 최대한 묶는게 좋다.
    이는 F(n) / F(n + 1) <= 2 / 3임을 보이면 된다.

    피보나치 점화식에 의해 F(n) + F(n + 1) = F(n + 2)이다.
    이로 F(0) = F(1) = 1, F(2) = 2라 하면 F(n) < F(n + 1)임을 알 수 있다.
    이 둘을 이용하면 F(n + 1) <= F(n) + F(n)임을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1958
    {

        static void Main1958(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int min = n / 2;
            if (n % 2 == 1) min++;
            int max = (n / 3) * 2;
            if (n % 3 == 2) max++;

            Console.Write($"{min} {max}");
        }
    }
}
