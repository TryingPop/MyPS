using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 24
이름 : 배성훈
내용 : 아!
    문제번호 : 4999번

    문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1643
    {

        static void Main1643(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            string input = sr.ReadLine();
            int prev = input.Length;

            input = sr.ReadLine();
            Console.Write(prev < input.Length ? "no" : "go");
        }
    }
}
