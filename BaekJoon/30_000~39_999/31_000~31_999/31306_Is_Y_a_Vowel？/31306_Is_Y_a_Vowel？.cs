using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 16
이름 : 배성훈
내용 : Is Y a Vowel?
    문제번호 : 31306번

    구현, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1548
    {

        static void Main1548(string[] args)
        {

            // 31306 번
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string input = sr.ReadLine();
            int v = 0;
            int y = 0;

            for (int i = 0; i < input.Length; i++)
            {

                if (input[i] == 'a' || input[i] == 'e' || input[i] == 'i' || input[i] == 'o' || input[i] == 'u') v++;
                else if (input[i] == 'y') y++;
            }

            Console.Write($"{v} {v + y}");
        }
    }
}
