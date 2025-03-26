using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 25
이름 : 배성훈
내용 : 선물
    문제번호 : 1166번

    이분 탐색 문제다.
    실수형으로 하니 26% 쯤에서 시간초과 걸린다.
    그래서 이분탐색 시행횟수를 100번으로 제한해주니 이상없이 통과한다.

    100회는 10^9 = 2^30 정도이고,
    찾아야 하는게 10^-9오차 이내이다.
    10^9 < 2^30이고, 매번 오차가 반으로 줄어들기에
    100번 정도 시행한 탐색 범위는 10^9 x 2^-100 < 10^-10 으로 10^-10범위 안으로 들어오기 때문이다.
    그리고 double 는 15 ~ 16자리까지 정확한 연산을 하는데 오차 방지로 10^-10을 더해줘서 오차를 맞춰갔다.
*/

namespace BaekJoon.etc
{
    internal class etc_1466
    {

        static void Main1466(string[] args)
        {

            int n, l, w, h;

            Input();

            GetRet();

            void GetRet()
            {

                Console.Write(BinarySearch());

                double BinarySearch()
                {

                    int trial = 0;
                    double l = 1e-9;
                    double r = 1e9;

                    while (l < r)
                    {

                        double mid = (l + r) / 2;
                        if (Chk(mid)) l = mid + 1e-10;
                        else r = mid - 1e-10;

                        if (trial++ > 100) break;
                    }

                    return l;
                }

                bool Chk(double _val)
                {

                    return Div(l) * Div(w) * Div(h) + 1e-10 >= n;

                    double Div(int _up)
                        => Math.Floor(_up / _val + 1e-10);
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                l = int.Parse(temp[1]);
                w = int.Parse(temp[2]);
                h = int.Parse(temp[3]);
            }
        }
    }
}
