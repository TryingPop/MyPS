using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 6
이름 : 배성훈
내용 : 개미
    문제번호 : 10158번

    수학 문제다.
    반사되는 조건을 보면 x축 대칭, y축 대칭 규칙을 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1808
    {

        static void Main1808(string[] args)
        {

            int w, h, p, q, t;

            Input();

            GetRet();

            void GetRet()
            {

                int x = p + t;
                int y = q + t;
                int rx = x % (w << 1);
                int ry = y % (h << 1);

                if (rx > w) rx = (w << 1) - rx;
                if (ry > h) ry = (h << 1) - ry;
                Console.Write($"{rx} {ry}");
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                w = int.Parse(temp[0]);
                h = int.Parse(temp[1]);

                temp = Console.ReadLine().Split();
                p = int.Parse(temp[0]);
                q = int.Parse(temp[1]);

                t = int.Parse(Console.ReadLine());
            }
        }
    }
}
