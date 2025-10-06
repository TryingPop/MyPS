using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 5
이름 : 배성훈
내용 : 전구 주기 맞추기
    문제번호 : 32403번

    브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1928
    {

        static void Main1928(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            int t = ReadInt();

            int[] arr = new int[t + 2];
            int len = 0;

            for (int i = 1; i * i <= t; i++)
            {

                if (t % i != 0) continue;

                arr[len++] = i;
                arr[len++] = t / i;
            }

            Array.Sort(arr, 0, len);

            int ret = 0;
            // 성능 향상하려면 이분 탐색으로 찾는게 좋다.
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                int min = 123_456;
                for (int j = 0; j < len; j++)
                {

                    min = Math.Min(min, Math.Abs(cur - arr[j]));
                }

                ret += min;
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
