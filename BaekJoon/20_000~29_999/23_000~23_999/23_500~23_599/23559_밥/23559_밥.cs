using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 6
이름 : 배성훈
내용 : 밥
    문제번호 : 23559번

    그리디, 정렬 문제다.
    5000원짜리 맛 - 1000원짜리 맛의 값이 큰 것을 바꾸는게 전체 총합이 늘어난다.
    그래서 5000원짜리를 살 수 있는 최대갯수를 x라 하면 5000원짜리 맛 - 1000원짜리 맛이 큰 것 x개를 사는게 좋다.
    5000원짜리 맛 - 1000원짜리 맛이 음수인 경우 5000원짜리를 안사는게 좋다.
    그래서 0으로 반례처리를 하거나 0인 경우 탈출을 하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1616
    {

        static void Main1616(string[] args)
        {

            int n, x;
            int[] arr;
            int ret;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr, (x, y) => y.CompareTo(x));
                for (int i = 0; i < x; i++)
                {

                    if (arr[i] <= 0) break;
                    ret += arr[i];
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                x = ((int)(ReadLong() / 1_000) - n) / 4;

                arr = new int[n];
                ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int b = ReadInt();
                    int a = ReadInt();

                    arr[i] = b - a;
                    ret += a;
                }

                long ReadLong()
                {

                    long ret = 0;

                    while (TryReadLong()) ;
                    return ret;

                    bool TryReadLong()
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
