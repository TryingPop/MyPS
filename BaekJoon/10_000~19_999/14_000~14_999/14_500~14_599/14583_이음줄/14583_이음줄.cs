using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 8
이름 : 배성훈
내용 : 이음줄
    문제번호 : 14583번

    수학 문제다.
    사각형이 주어질 때, 대각선에 밑변이 겹치게 해야 한다.
    접는 선은 밑변과 대각선의 각도를 반이 된다.

    그러면 코사인 반각 공식으로 접는 선의 길이를 구할 수 있다.
    접는 선과 밑변이 이루는 각을 x라 하면
    cos 2x = 2 (cos x)^2 - 1을 얻고, cos 2x = 밑변 / 대각선 을 알 수 있다.
    그래서 접는선 v를 구할 수 있다.
    그러면 접는 선의 절반이 우리가 찾는 밑변이 된다.

    이제 높이는 넓이를 이용해 구했다.
    접는 선과 밑변이 이루는 삼각형에서 높이는 sin x이다.
    sin x역시 코사인 반각 공식으로
    cos 2x = 1 - 2 (sin x)^2을 얻고, sin x = 찾는 높이 / v 임을 알 수 있다.
    앞의 v로 찾는 높이를 찾고, 해당 삼각형의 넓이를 양쪽으로 뺀 평행사변형의 넓이를 찾는다.

    그러면 v x h = 밑변 x 찾는 높이 임을 알 수 있다.
    이렇게 찾아 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1386
    {

        static void Main1386(string[] args)
        {

            double[] len = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);

            double dia = Math.Sqrt(len[0] * len[0] + len[1] * len[1]);
            double cos2x = len[0] / dia;

            double cosx = Math.Sqrt((cos2x + 1.0) / 2.0);
            double sinx = Math.Sqrt((1.0 - cos2x) / 2.0);

            double v = len[0] / cosx;
            double h = (len[1] - v * sinx) * len[0] / v;

            v /= 2.0;
            Console.Write($"{v:0.00} {h:0.00}");
        }
    }
}
