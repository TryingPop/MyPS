using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 30
이름 : 배성훈
내용 : 이면수와 임현수
    문제번호 : 1291번

    구현 문제다.
    지문에 쓸모없는 말이 너무 많다;
    요약하면, 이면수는 6이상에 각자릿수의 합이 홀수여야 한다.
    임현수는 2 or 4인 경우 또는 합성수이면서 동시에 서로 다른 소인수가 짝수개 여야 한다.
    성경수는 임현수와 이면수가 아닌 경우다.
*/

namespace BaekJoon.etc
{
    internal class etc_1495
    {

        static void Main1495(string[] args)
        {

            string str = Console.ReadLine();
            int n = int.Parse(str);

            int type = 3;
            if (Chk1()) type = 1;

            if (Chk2()) type = type == 1 ? 4 : 2;

            Console.Write(type);

            bool Chk2()
            {

                if (n == 2 || n == 4) return true;
                else if (n < 5) return false;
                int chk = n;
                int cnt = 0;

                for (int i = 2; i * i <= chk; i++)
                {

                    if (chk % i != 0) continue;

                    cnt++;
                    while (chk % i == 0)
                    {

                        chk /= i;
                    }
                }

                if (chk > 1) cnt++;
                return (cnt & 1) == 0;
            }

            bool Chk1()
            {

                if (n < 6) return false;

                int digit = 0;
                for (int i = 0; i < str.Length; i++)
                {

                    digit += str[i] - '0';
                }

                if ((digit & 1) == 0) return false;
                return true;
            }
        }
    }
}
