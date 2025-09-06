using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 1
이름 : 배성훈
내용 : 꼬리를 무는 숫자 나열
    문제번호 : 1598번

    수학 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1855
    {

        static void Main1855(string[] args)
        {

            // 1598
            // 통과 완료
            int n, m;

            Input();

            GetRet();

            void GetRet()
            {

                n--;
                m--;
                int r1 = n % 4;
                int r2 = m % 4;
                int c1 = n / 4;
                int c2 = m / 4;

                Console.Write(Math.Abs(r1 - r2) + Math.Abs(c1 - c2));
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
