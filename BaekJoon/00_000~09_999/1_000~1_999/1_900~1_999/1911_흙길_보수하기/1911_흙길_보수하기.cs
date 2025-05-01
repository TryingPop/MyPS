using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 1
이름 : 배성훈
내용 : 흙길 보수하기
    문제번호 : 1911번

    그리디, 정렬, 스위핑 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1601
    {

        static void Main1601(string[] args)
        {

            int n, l;
            (int s, int e)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr, (x, y) => x.s.CompareTo(y.s));

                int end = -1;
                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i].e <= end) continue;
                    int s = Math.Max(end, arr[i].s);
                    int len = arr[i].e - s;
                    int cnt = len / l;
                    if (len % l > 0) cnt++;

                    ret += cnt;
                    end = s + l * cnt;
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                l = ReadInt();
                arr = new (int s, int e)[n];

                for (int i = 0;i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
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
