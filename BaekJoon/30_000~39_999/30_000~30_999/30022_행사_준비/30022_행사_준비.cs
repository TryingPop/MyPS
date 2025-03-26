using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 24
이름 : 배성훈
내용 : 행사 준비
    문제번호 : 30022번

    그리디, 정렬 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1458
    {

        static void Main1458(string[] args)
        {

            int n, a, b;
            (int a, int b)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = 0;

                for (int i = 0; i < a; i++)
                {

                    ret += arr[i].a;
                }

                for (int i = a; i < n; i++)
                {

                    ret += arr[i].b;
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                a = ReadInt();
                b = ReadInt();
                arr = new (int a, int b)[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
                }

                Array.Sort(arr, (x, y) => (x.a - x.b).CompareTo(y.a - y.b));

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;
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
