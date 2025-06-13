using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 27
이름 : 배성훈
내용 : 문자열 화폐
    문제번호 : 17828번

    그리디, 문자열 문제다.
    가장 큰 값을 최대한 할당하는게 좋다.
    그래야 A의 개수를 최대화할 수 있고 이는 사전식으로 앞서기 때문이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1134
    {

        static void Main1134(string[] args)
        {

            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            if (input[1] < input[0] || input[0] * 26 < input[1])
            {

                Console.Write('!');
                return;
            }

            input[1] -= input[0];
            int zIdx = input[1] / 25;
            int mVal = input[1] % 25;

            using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
            {

                int aIdx = input[0] - zIdx - 1;
                for (int i = 0; i < aIdx; i++)
                {

                    sw.Write('A');
                }

                if (zIdx != input[0]) sw.Write((char)('A' + mVal));

                for (int i = 0; i < zIdx; i++)
                {

                    sw.Write('Z');
                }
            }
        }
    }
}
