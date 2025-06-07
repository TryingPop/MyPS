using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 7
이름 : 배성훈
내용 : 선물 전달
    문제번호 : 1947번

    dp 문제다.
    출력을 if문 안에 해서 1, 2 케이스에 출력을 안해 1번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1686
    {

        static void Main1686(string[] args)
        {

            long MOD = 1_000_000_000;
            int n = Convert.ToInt32(Console.ReadLine());

            long ret;
            if (n == 1) ret = 0;
            else if (n == 2) ret = 1;
            else
            {

                long p = 0;
                ret = 1;
                for (long i = 3; i <= n; i++)
                {

                    long pp = p;
                    p = ret;
                    ret = ((i - 1) * (pp + p)) % MOD;
                }
            }

            Console.Write(ret);
        }
    }
}
