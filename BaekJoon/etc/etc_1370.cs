using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 2
이름 : 배성훈
내용 : 첫 글자를 대문자로
    문제번호 : 4458번

    구현, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1370
    {

        static void Main1370(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            for (int i =0; i < n; i++)
            {

                string input = sr.ReadLine();

                if ('a' <= input[0] && input[0] <= 'z')
                {

                    int change = input[0] - 'a' + 'A';
                    sw.Write((char)change);
                }
                else sw.Write(input[0]);

                for (int j = 1; j < input.Length; j++)
                {

                    sw.Write(input[j]);
                }

                sw.Write('\n');
            }
        }
    }
}
