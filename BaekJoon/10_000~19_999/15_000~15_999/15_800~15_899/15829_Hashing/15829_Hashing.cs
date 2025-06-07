using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 7
이름 : 배성훈
내용 : Hashing
    문제번호 : 15829번

    해싱, 구현, 문자열 문제다
    조건대로 해싱하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1035
    {

        static void Main1035(string[] args)
        {

            int M = 1_234_567_891;
            int R = 31;

            StreamReader sr;
            string str;
            int n;

            Solve();
            void Solve()
            {

                n = int.Parse(Console.ReadLine());
                str = Console.ReadLine();

                Console.Write(GetMyHash());
            }

            long GetMyHash()
            {

                long ret = 0;
                for (int i = n - 1; i >= 0; i--)
                {

                    int cur = str[i] - 'a' + 1;
                    ret = (ret * R + cur) % M;
                }

                return ret;
            }
        }
    }
}
