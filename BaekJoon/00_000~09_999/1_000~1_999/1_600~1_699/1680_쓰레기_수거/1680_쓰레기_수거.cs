using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 30
이름 : 배성훈
내용 : 쓰레기 수거
    문제번호 : 1680번

    구현, 시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1741
    {

        static void Main1741(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int w, n;
            int t = ReadInt();

            for (int i = 0; i < t; i++)
            {

                w = ReadInt();
                n = ReadInt();

                long ret = 0;
                int pos = 0;
                int cur = 0;

                for (int j = 0; j < n; j++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    // 다음 수하물 실으려고 한다.
                    ret += x - pos;
                    pos = x;
                    if (cur + y > w)
                    {

                        // 만약 초과하면 현재 수하물을 비운다.
                        ret += 2 * pos;
                        cur = 0;
                    }

                    cur += y;
                    if (cur == w)
                    {

                        // 가득찼다면 비우러 간다.
                        cur = 0;
                        ret += 2 * pos;
                    }
                }

                // 마지막에 비운경우 되돌아올 필요 없기에 뺀다.
                if (cur == 0) ret -= pos;
                // 남은 수하물을 비우러간다.
                else ret += pos;

                sw.Write($"{ret}\n");
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
