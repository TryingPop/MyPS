using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 4
이름 : 배성훈
내용 : Corona Virus Testing
    문제번호 : 25828번

    사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1672
    {

        static void Main1672(string[] args)
        {

            int g, p, t;

            Input();

            GetRet();

            void GetRet()
            {

                int chk1 = g * p;
                int chk2 = g + t * p;

                int ret = chk1 == chk2 ? 0 : (chk1 < chk2 ? 1 : 2);
                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                g = int.Parse(temp[0]);
                p = int.Parse(temp[1]);
                t = int.Parse(temp[2]);
            }
        }
    }
}
