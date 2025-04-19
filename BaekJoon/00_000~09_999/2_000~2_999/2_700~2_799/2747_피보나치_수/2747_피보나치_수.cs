using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 18
이름 : 배성훈
내용 : 피보나치 수
    문제번호 : 2747번

    수학 문제다.
    45항까지는 ulong 범위로 표현할 수 있다.
    입력되는 범위가 1항 이상이므로 따로 0항은 반례처리가 필요 없다.
*/

namespace BaekJoon.etc
{
    internal class etc_1554
    {

        static void Main1554(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            ulong prev = 0, cur = 1;
            // n > 0
            for (int i = 1; i < n; i++)
            {

                ulong temp = cur;
                cur = prev + cur;
                prev = temp;
            }

            ulong ret = cur;

            Console.Write(ret);
        }
    }
}
