using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 27
이름 : 배성훈
내용 : 요다
    문제번호 : 5363번

    구현, 문자열 문제다.
    Split을 이용하면 쉽게 풀 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1363
    {

        static void Main1363(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            for (int i = 0; i < n; i++)
            {

                string[] input = sr.ReadLine().Split();
                for (int j = 2; j < input.Length; j++)
                {

                    sw.Write($"{input[j]} ");
                }

                sw.Write($"{input[0]} {input[1]}\n");
            }
        }
    }
}
