using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 3
이름 : 배성훈
내용 : 성적표
    문제번호 : 14721번

    수학, 브루트포스 문제다.
    RSS가 최소인 a, b의 범위가 1 ~ 100인 경우다.
    그래서 브루트포스로 찾아주면 된다.
    여기서 a, b의 범위가 없다면, 뉴턴의 방법을 써야할거 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1961
    {

        static void Main1961(string[] args)
        {

            int n;
            long x = 0, xx = 0, y = 0, yy = 0, xy = 0;

            Input();

            GetRet();

            void GetRet()
            {

                int ret1 = 1, ret2 = 1;
                long min = RSS(1, 1);
                for (int a = 1; a <= 100; a++)
                {

                    for (int b = 1; b <= 100; b++)
                    {

                        long cur = RSS(a, b);
                        if (min <= cur) continue;
                        ret1 = a;
                        ret2 = b;
                        min = cur;
                    }
                }

                Console.Write($"{ret1} {ret2}");

                long RSS(int a, int b)
                {

                    return a * a * xx
                        + yy
                        + b * b * n
                        + 2 * a * b * x
                        - 2 * a * xy
                        - 2 * b * y;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    int curX = ReadInt();
                    int curY = ReadInt();

                    x += curX;
                    xx += curX * curX;

                    y += curY;
                    yy += curY * curY;

                    xy += curX * curY;
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
}
