using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 설탕 배달 2
    문제번호 : 26099번

    수학(정수론) - 합동식 문제다
    3, 5는 서로 소이므로 a *  5 + b * 3 = n 으로 모든 n을 만들 수 있다
    그래서 그리디로 i 이후의 수들에 대해 경우의 수가 존재하는 i의 최소값을 찾고
    i ~ i + 14까지의 경우를 찾은 뒤에
    그리디로 3씩 더해주면 이상없이 풀린다
    해당 논리를 아래 구현한 것 뿐이다
*/

namespace BaekJoon.etc
{
    internal class etc_0190
    {

        static void Main190(string[] args)
        {

            long n = long.Parse(Console.ReadLine());

            // 3 ~ 22
            //              0,  1,  2, 3   4  5  6   7  8  9 10 11 12 13 14 15 16 17 18 19 20 21 22
            long[] temp = { 0, -1, -1, 1, -1, 1, 2, -1, 2, 3, 2, 3, 4, 3, 4, 3, 4, 5, 4, 5, 4, 5, 6 };
            long ret = 0;

            if (n > 22)
            {

                long calc = n - 22;
                long mul = 1 + calc / 15;
                calc %= 15;
                if (calc == 0) 
                { 
                    
                    calc = 15;
                    mul--;
                }

                ret = 3 * (mul) + temp[7 + calc];
            }
            else
            {

                ret = temp[n];
            }

            Console.WriteLine(ret);
        }
    }
}
