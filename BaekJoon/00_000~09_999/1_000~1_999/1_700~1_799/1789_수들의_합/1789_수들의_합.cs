using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 수들의 합
    문제번호 : 1789번

    수학, 그리디 알고리즘 문제다
    99%에서 1번 틀려서 작은수가 문제인가 싶어 1을 넣었고
    0이 나와 for문의 i범위를 num + 1까지 확대했다
    기존에는 num 까지만 확인했다(끝값 포함)
*/

namespace BaekJoon.etc
{
    internal class etc_0326
    {

        static void Main326(string[] args)
        {

            long num = long.Parse(Console.ReadLine());

            long sum = 0;
            int ret = 0;
            for (int i = 1; i < num + 2; i++)
            {

                sum += i;
                if (sum > num) 
                {

                    ret = i - 1;
                    break; 
                }
            }

            if (num == 1) ret = 1;
            Console.WriteLine(ret);
        }
    }
}
