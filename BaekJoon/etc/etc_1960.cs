using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 2
이름 : 배성훈
내용 : 당근 키우기
    문제번호 : 20363번

    그리디 문제다.
    빠지는 것을 최소로 해야한다.
    먼저 두 수 중 큰 것을 채우고,
    이후에 작은 것을 채우는 식으로 하는 것이 최소임을 그리디로 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1960
    {

        static void Main1960(string[] args)
        {

            int a, b;

            Input();

            GetRet();

            void GetRet()
            {

                int max = a > b ? a : b;
                int min = a > b ? b : a;

                long ret = max + min;
                ret += min / 10;

                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                a = int.Parse(temp[0]);
                b = int.Parse(temp[1]);
            }
        }
    }
}
