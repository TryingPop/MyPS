using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 29
이름 : 배성훈
내용 : A/B - 2
    문제번호 : 15792번

    구현, 사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1300
    {

        static void Main1300(string[] args)
        {

            int num, div;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int digit = 0;
                sw.Write(num / div);
                num %= div;

                if (num == 0) return;
                sw.Write('.');

                while (num > 0 && digit < 1_100)
                {

                    num *= 10;
                    sw.Write(num / div);
                    num %= div;
                    digit++;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                num = int.Parse(temp[0]);
                div = int.Parse(temp[1]);
            }
        }
    }
}
