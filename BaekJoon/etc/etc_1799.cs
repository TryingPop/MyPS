using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 30
이름 : 배성훈
내용 : 3대 512
    문제번호 : 33990번

    구현, 사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1799
    {

        static void Main1799(string[] args)
        {

            int DST = 512;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int ret = -1;
            int n = ReadInt();

            for (int i = 0; i < n; i++)
            {

                int sum = ReadInt() + ReadInt() + ReadInt();
                if (sum < DST) continue;

                if (ret == -1 || ret > sum)
                    ret = sum;
            }

            Console.Write(ret);

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
