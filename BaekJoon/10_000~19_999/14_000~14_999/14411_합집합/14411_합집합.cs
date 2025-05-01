using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 30
이름 : 배성훈
내용 : 합집합
    문제번호 : 14411번

    정렬, 스택 문제다.
    정렬을 이용해 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1599
    {

        static void Main1599(string[] args)
        {

            int n;
            (int x, int y)[] rects;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(rects, (x, y) => 
                {

                    int ret = y.x.CompareTo(x.x);
                    if (ret == 0) ret = y.y.CompareTo(x.y);
                    return ret;
                });

                for (int i = 0; i < n; i++)
                {

                    rects[i + 1].y = Math.Max(rects[i].y, rects[i + 1].y);
                }

                long ret = 0L;

                for (int i = 0; i < n; i++)
                {

                    long w = rects[i].x - rects[i + 1].x;
                    ret += w * rects[i].y;
                }

                Console.Write(ret * 4);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                rects = new (int x, int y)[n + 1];

                rects[0] = (0, 10_000_000);

                for (int i = 1; i <= n; i++)
                {

                    rects[i] = (ReadInt() >> 1, ReadInt() >> 1);
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
}
