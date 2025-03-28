using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 28
이름 : 배성훈
내용 : 모음의 개수
    문제번호 : 1264번

    문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1485
    {

        static void Main1485(string[] args)
        {

            string EXIT = "#";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string input;

            while ((input = sr.ReadLine()) != EXIT)
            {

                int ret = 0;
                for (int i = 0; i < input.Length; i++)
                {

                    switch (input[i])
                    {

                        case 'a':
                        case 'e':
                        case 'i':
                        case 'o':
                        case 'u':
                        case 'A':
                        case 'E':
                        case 'I':
                        case 'O':
                        case 'U':
                            ret++;
                            break;
                        default:
                            break;
                    }
                }

                sw.Write(ret);
                sw.Write('\n');
            }
        }
    }
}
