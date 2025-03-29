using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 29
이름 : 배성훈
내용 : 뒤집힌 덧셈
    문제번호 : 1357번

    문자열 문제다.
    345 5 반례를 캐치 못해 한 번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1491
    {

        static void Main1491(string[] args)
        {

            string[] input = Console.ReadLine().Split();

            int a = Rev(input[0]);
            int b = Rev(input[1]);
            string rev = (a + b).ToString();
            int ret = Rev(rev);

            Console.Write(ret);

            int Rev(string _str)
            {

                int ret = 0;
                for (int i = _str.Length - 1; i >= 0; i--)
                {

                    ret = ret * 10 + _str[i] - '0';
                }

                return ret;
            }
        }
    }
}
