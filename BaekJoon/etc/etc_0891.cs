using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 20
이름 : 배성훈
내용 : 거스름돈
    문제번호 : 5585번

    그리디 문제다
    동전이 큰 값이 작은값에 항상 나눠떨어지기 때문에
    큰 동전부터 주는게 좋다
*/

namespace BaekJoon.etc
{
    internal class etc_0891
    {

        static void Main891(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int[] coin = { 500, 100, 50, 10, 5, 1 };

            n = 1000 - n;
            int ret = 0;
            for (int i = 0; i < coin.Length; i++)
            {

                ret += n / coin[i];
                n %= coin[i];
            }

            Console.Write(ret);
        }
    }
}
