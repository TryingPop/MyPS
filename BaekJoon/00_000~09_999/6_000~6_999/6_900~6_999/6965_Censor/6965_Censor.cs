using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 14
이름 : 배성훈
내용 : Censor
    문제번호 : 6965번

    문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1407
    {

        static void Main1407(string[] args)
        {

            string MOSAIC = "****";
            char CONN = ' ';
            char ENDL = '\n';

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            for (int i = 0; i < n; i++)
            {

                string[] input = sr.ReadLine().Split();
                for (int j = 0; j < input.Length; j++)
                {

                    if (input[j].Length == 4) sw.Write(MOSAIC);
                    else sw.Write(input[j]);

                    sw.Write(CONN);
                }

                sw.Write(ENDL);
                sw.Write(ENDL);
            }
        }
    }
}
