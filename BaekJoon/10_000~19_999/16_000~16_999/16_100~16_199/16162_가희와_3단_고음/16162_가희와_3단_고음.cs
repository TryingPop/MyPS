using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 25
이름 : 배성훈
내용 : 가희와 3단 고음
    문제번호 : 16162번

    그리디 문제다.
    현재 고음을 올릴 수 있다면 올리는게 좋다.
*/

namespace BaekJoon.etc
{
    internal class etc_1786
    {

        static void Main1786(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int a = ReadInt();
            int d = ReadInt();
            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                // 현재 고음
                int cur = ReadInt();
                if (cur != a) continue;
                // 올릴 수 있으면 올린다.
                // ret단 고음
                ret++;
                // 다음 찾을 고음
                a += d;
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
