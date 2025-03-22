using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 23
이름 : 배성훈
내용 : 다이어트
    문제번호 : 1484번

    수학, 두 포인터 문제다.
    에라토스테네스의 체로 합과 차를 구하고,
    둘 다 자연수인지 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1452
    {

        static void Main1452(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int len = 0;
            int[] ret = new int[n];

            for (int sub = 1; sub * sub <= n; sub++)
            {

                if (n % sub != 0) continue;
                int add = n / sub;

                int a = (sub + add) / 2;
                int b = (add - sub) / 2;

                if (a + b != add || a - b != sub || a < b || a <= 0 || b <= 0) continue;

                ret[len++] = a;
            }

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            if (len == 0) sw.Write(-1);
            for (int i = len - 1; i >= 0; i--)
            {

                sw.Write($"{ret[i]}\n");
            }
        }
    }
}
