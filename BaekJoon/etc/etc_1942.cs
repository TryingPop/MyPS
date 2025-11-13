using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 16
이름 : 배성훈
내용 : 세준세비
    문제번호 : 1524번

    구현 문제다.
*/

namespace Study._02_백준
{
    internal class etc_1942
    {

        static void Main1942(string[] args)
        {
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();

            while (t-- > 0)
            {

                int a = ReadInt();
                int b = ReadInt();
                int maxA = 0;
                int maxB = 0;

                for (int i = 0; i < a; i++)
                {

                    maxA = Math.Max(maxA, ReadInt());
                }

                for (int i = 0; i < b; i++)
                {

                    maxB = Math.Max(maxB, ReadInt());
                }

                if (maxA >= maxB) sw.Write('S');
                else sw.Write('B');

                sw.Write('\n');
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }
                    return false;
                }
            }
        }
    }
}
