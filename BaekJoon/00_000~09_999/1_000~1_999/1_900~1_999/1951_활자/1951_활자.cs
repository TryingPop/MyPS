using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 23
이름 : 배성훈
내용 : 활자
    문제번호 : 1951번

    수학, 구현 문제다.
    오버플로우로 2번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1215
    {

        static void Main1215(string[] args)
        {

            int MOD = 1_234_567;
            long n = int.Parse(Console.ReadLine());
            long tenPow = 1;

            long ret = 0;
            for (int cnt = 1; 0 < n; cnt++)
            {

                long nextPow = tenPow * 10;
                long sub = Math.Min(n, nextPow - tenPow);
                n -= sub;
                tenPow = nextPow;
                ret = (ret + cnt * sub) % MOD;
            }

            Console.Write(ret);
        }
    }
}
