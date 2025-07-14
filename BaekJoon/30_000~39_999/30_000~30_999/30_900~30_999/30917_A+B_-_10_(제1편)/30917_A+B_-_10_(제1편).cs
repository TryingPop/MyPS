using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 14
이름 : 배성훈
내용 : A + B - 10 (제 1편)
    문제번호 : 30917번

    구현 문제다.
    채점기와 상호작용하는 문제다.
    채점기에게 값을 물어보고 원하는 결과를 출력해야 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1765
    {

        static void Main1765(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int a = GetNum('A');
            int b = GetNum('B');

            sw.Write($"! {a + b}");

            int GetNum(char _chk)
            {

                for (int i = 1; i <= 9; i++)
                {

                    sw.Write($"? {_chk} {i}\n");
                    sw.Flush();

                    int ans = ReadInt();

                    if (ans == 1)
                        return i;
                }

                return -1;
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
