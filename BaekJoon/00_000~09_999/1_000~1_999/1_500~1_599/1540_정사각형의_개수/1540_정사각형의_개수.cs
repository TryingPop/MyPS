using System;

/*
날짜 : 2025. 4. 30
이름 : 배성훈
내용 : 정사각형의 개수
    문제번호 : 1540번

    수학 문제다.
    점을 놓을 때 산술기하 평균으로 사각형이 정사각형에 가까울 수록 만들 수 있는 정사각형의 갯수가 많음을 알 수 있다.
    그래서 제곱근에 가깝게 길이를 만든다.

    그리고 정사각형의 갯수를 세면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1598
    {

        static void Main1598(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            if (n < 4)
            {

                Console.Write(0);
                return;
            }

            int w = MySqrt(n);
            long ret = GetCnt(w);

            Console.Write(ret);

            int MySqrt(int _val)
            {

                int l = 1;
                int r = 1_000;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (mid * mid <= _val) l = mid + 1;
                    else r = mid - 1;
                }

                return l - 1;
            }

            long GetCnt(int _w)
            {

                int h = n / _w;
                int r = n - _w * h;

                // 남은건 오른쪽 끝이 정해짐
                long ret = (r * (r - 1)) >> 1;
                // 길이 i인 정사각형의 갯수
                for (int i = 1; i < _w; i++)
                {

                    ret += (_w - i) * (h - i);
                }

                return ret;
            }
        }
    }
}
