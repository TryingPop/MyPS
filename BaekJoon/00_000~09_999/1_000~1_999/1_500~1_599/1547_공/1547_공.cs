using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 2
이름 : 배성훈
내용 : 공
    문제번호 : 1547번

    구현, 시뮬레이션
*/

namespace BaekJoon.etc
{
    internal class etc_1744
    {

        static void Main1744(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int ret = 1;
            int m = ReadInt();
            for (int i = 0; i < m; i++)
            {

                int f = ReadInt();
                int t = ReadInt();

                if (f == ret)
                    ret = t;
                else if (t == ret)
                    ret = f;
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
