using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 매우 어려운 문제
    문제번호 : 31738번

    수학, 애드혹, 정수론 문제다
    앞이 뒤보다 크거나 같으면 0으로 바로 제출했다
    이외 경우는 브루트포스로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0544
    {

        static void Main544(string[] args)
        {

            long[] input = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

            if (input[0] >= input[1])
            {

                Console.WriteLine(0);
                return;
            }

            long ret = 1;
            for (int i = 1; i <= input[0]; i++)
            {

                ret = (ret * i) % input[1];
            }

            Console.WriteLine(ret);
        }
    }
}
