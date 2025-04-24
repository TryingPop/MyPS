using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 24
이름 : 배성훈
내용 : 롤케이크
    문제번호 : 16206번

    그리디 문제다.
    10단위가 있으면 10단위 먼저 자르는게 좋다.
    같은 10단위인경우면 작은거부터 자르는게 좋다.

    반면 10단위가 아닌 경우 아무거나 자르면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1574
    {

        static void Main1574(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            Array.Sort(arr, (x, y) =>
            {

                if (x % 10 == 0 && y % 10 != 0) return -1;
                else if (y % 10 == 0 && x % 10 != 0) return 1;

                return x.CompareTo(y);
            });

            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                if (arr[i] % 10 == 0)
                {

                    // 모두 다 자를 수 있는 경우
                    if (arr[i] / 10 <= m + 1)
                    {

                        // 1회 킵한다.
                        m -= arr[i] / 10 - 1;
                        ret += arr[i] / 10;
                    }
                    else
                    {

                        // 모두 못 자르는 경우는 자를 수 있는 만큼만 자른다.
                        ret += m;
                        m = 0;
                    }
                }
                else
                {

                    // 어떻게 잘라도 10단위로만 잘라야 한다.
                    int add = m <= arr[i] / 10 ? m : arr[i] / 10;
                    ret += add;
                    m -= add;
                }

                if (m == 0) break;
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
