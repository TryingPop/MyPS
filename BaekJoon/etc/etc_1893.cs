using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 17
이름 : 배성훈
내용 : Statistics
    문제번호 : 20674번

    사칙연산 문제다.
    증가하지 않아야 하고, 진단 결과를 줄이기만 해야한다.
    그래서 이전 개수보다 많은 경우 넘지 않게 해주기만 하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1893
    {

        static void Main1893(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int ret = 0;

            int prev = ReadInt();
            for (int i = 1; i < n; i++)
            {

                int cur = ReadInt();
                if (prev < cur) ret += cur - prev;
                else prev = cur;
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
