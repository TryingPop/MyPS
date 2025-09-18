using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 16
이름 : 배성훈
내용 : 저울
    문제번호 : 3923번

    브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1886
    {

        static void Main1886(string[] args)
        {

            int INF = 123_456;
            int S = 50_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            int a = 0, b = 0, d = 0;

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                int x = INF;
                int y = INF;
                for (int i = -S; i <= S; i++)
                {

                    int chk = d - a * i;
                    if (chk % b != 0) continue;
                    int chkY = Math.Abs(chk / b);
                    int chkX = Math.Abs(i);

                    if (chkX + chkY < x + y
                        || (chkX + chkY == x + y && a * chkX + b * chkY < a * x + b * y))
                    {

                        x = chkX;
                        y = chkY;
                    }
                }

                sw.Write($"{x} {y}\n");
            }

            bool Input()
            {

                a = ReadInt();
                b = ReadInt();
                d = ReadInt();

                return a != 0 || b != 0 || d != 0;
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
