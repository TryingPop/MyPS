using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 4
이름 : 배성훈
내용 : 팩토리얼 분해
    문제번호 : 2057번

    수학, 그리디, 브루트포스 문제다.
    n 팩토리얼인 n!을 fac[n]이라 하자
    그러면 다음이 성립한다.
    fac[0] + fac[1] + ... + fac[n - 1] ≤ fac[n] 이 성립한다.
    수학적 귀납법으로 증명 가능하다.(그리디)

    그래서 큰수로 최대한 채워가면서 해당 수를 만들 수 있는지 확인하면 된다.
    다만 0인 경우 반례를 잘 고려해야 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1315
    {

        static void Main1315(string[] args)
        {

            long n = Convert.ToInt64(Console.ReadLine());
            long[] fac = new long[20];
            fac[0] = 1;
            for (int i = 1; i < 20; i++)
            {

                fac[i] = i * fac[i - 1];
            }

            bool flag = false;
            for (int i = 19; i >= 0; i--)
            {

                if (n < fac[i]) continue;
                n -= fac[i];
                flag = true;
            }

            if (flag && n == 0) Console.Write("YES");
            else Console.Write("NO");
        }
    }
}
