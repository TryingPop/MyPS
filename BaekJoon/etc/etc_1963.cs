using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 5
이름 : 배성훈
내용 : 복불복으로 지구 멸망
    문제번호 : 17358번

    조합론 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1963
    {

        static void Main1963(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int MOD = 1_000_000_007;
            long ret = 1;
            while (n > 0)
            {

                ret = (ret * (n - 1)) % MOD;
                n -= 2;
            }

            Console.Write(ret);
        }
    }
}
