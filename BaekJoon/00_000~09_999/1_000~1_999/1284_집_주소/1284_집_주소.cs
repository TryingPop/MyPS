using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : 집 주소
    문제번호 : 1284번

    사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1432
    {

        static void Main1432(string[] args)
        {

            string ZERO = "0";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] len = new int[255];
            len['0'] = 4;
            len['1'] = 2;

            for (int i = '2'; i <= '9'; i++)
            {

                len[i] = 3;
            }

            string input;
            while ((input = sr.ReadLine()) !=  ZERO)
            {

                int ret = input.Length + 1;
                for (int j = 0; j < input.Length; j++)
                {

                    ret += len[input[j]];
                }

                sw.Write($"{ret}\n");
            }
        }
    }
}
