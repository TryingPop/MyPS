using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 4
이름 : 배성훈
내용 : 크냑과 3D 프린터
    문제번호 : 30923번

    수학 문제다.
    겉넓이를 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1612
    {

        static void Main1612(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();

            int cur = ReadInt();
            long ret = cur * 4 + 2;
            for (int i = 1; i < n; i++)
            {

                int prev = cur;
                cur = ReadInt();
                // 겹치는 부분은 작은 부분을 빼면 된다.
                int min = Math.Min(prev, cur);
                ret += cur * 4 + 2 - 2 * min;
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
                    if (c == '\n' || c == ' ') return false;
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
