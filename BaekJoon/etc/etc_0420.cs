using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 팩토리얼 0의 개수
    문제번호 : 1676번

    수학 문제다
    뒤에 0의 개수는 인수분해 했을 때, 2와 5의 개수 중 적은것과 일치한다
    그런데 팩토리얼이므로 5의 개수는 항상 2의 개수보다 적다!
    그래서 5의 개수만 세어주면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0420
    {

        static void Main420(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int ret = 0;
            while (n > 0)
            {

                n /= 5;
                ret += n;
            }

            Console.WriteLine(ret);
        }
    }
}
