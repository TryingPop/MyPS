using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 3
이름 : 배성훈
내용 : 전시회
    문제번호 : 15554번

    그리디, 정렬, 누적 합 문제다.
    아이디어는 다음과 같다.
    a의 범위에 있는 미술품을 최대한 챙기는게 S값이 증가하기에 좋다.
    그래서 그리디로 해당 범위에 있는 모든 미술품을 챙긴다고 본다.

    그래서 a로 정렬한 뒤 b에 누적 합 알고리즘을 적용해주면 된다.
    그러면 i번을 포함한 최댓값은 j ≤ i인 j에 대해 Sum(i, j - 1) - (ai - aj)의 최댓값이 된다.

    여기서 Sum(i, j - 1)은 j, j + 1, ..., i번째까지의 b의 총합이고,
    ai는 i번째 a가 된다.

    다만 최솟값 갱신을 뒤에 해줘서 여러 번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1608
    {

        static void Main1608(string[] args)
        {

            int n;
            (long a, long b)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(arr, (x, y) => x.a.CompareTo(y.a));

                for (int i = 1; i <= n; i++)
                {

                    arr[i].b += arr[i - 1].b;
                }

                long ret = 0;
                long min = arr[0].b - arr[1].a;

                for (int i = 1; i <= n; i++)
                {

                    long cur = arr[i].b - arr[i].a;
                    min = Math.Min(min, arr[i - 1].b - arr[i].a);
                    ret = Math.Max(ret, cur - min);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new (long a, long b)[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = (ReadLong(), ReadInt());
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
                        if (c == ' ' || c == '\n') return true;
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
                        if (c == ' ' || c == '\n') return true;
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
