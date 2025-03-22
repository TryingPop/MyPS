using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 피시방 알바
    문제번호 : 1453번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1447
    {

        static void Main1447(string[] args)
        {

            int MAX = 100;
            bool[] use = new bool[MAX];
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                int idx = ReadInt() - 1;
                if (use[idx]) ret++;
                use[idx] = true;
            }

            Console.Write(ret);

            int ReadInt()
            {
                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ' || c == -1) return true;

                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
