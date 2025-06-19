using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : FizzBuzz
    문제번호 : 28702번

    수학, 문자열 문제다
    3의 배수는 3개 연달아 나오는 숫자중 많아야 1번만 나온다
    5의 배수 역시 많아야 1개 나온다
    그래서 적어도 하나는 숫자다!
*/

namespace BaekJoon.etc
{
    internal class etc_1052
    {

        static void Main1052(string[] args)
        {

            string F = "Fizz";
            string B = "Buzz";
            string FB = "FizzBuzz";

            StreamReader sr;
            int cur;
            int find;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int num = cur + find;
                if (num % 3 == 0 && num % 5 == 0) Console.Write(FB);
                else if (num % 3 == 0) Console.Write(F);
                else if (num % 5 == 0) Console.Write(B);
                else Console.Write(num);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string f = sr.ReadLine();
                string s = sr.ReadLine();
                string t = sr.ReadLine();

                if (ChkNum(f))
                {

                    cur = int.Parse(f);
                    find = 3;
                    return;
                }

                if (ChkNum(s))
                {

                    cur = int.Parse(s);
                    find = 2;
                    return;
                }

                cur = int.Parse(t);
                find = 1;
                return;
            }

            bool ChkNum(string _str)
            {

                return _str != F && _str != B && _str != FB;
            }
        }
    }
}
