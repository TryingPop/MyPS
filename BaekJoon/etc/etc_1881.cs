using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 11
이름 : 배성훈
내용 : 귀여운 라이언
    문제번호 : 15565번

    두 포인터, 슬라이딩 윈도우 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1881
    {

        static void Main1881(string[] args)
        {

#if first
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            int cnt = 0;
            int front = 0;

            int ret = n + 1;
            for (int back = 0; back < n; back++)
            {

                if (arr[back] == 1) cnt++;

                if (cnt < k) continue;

                while (cnt == k)
                {

                    if (arr[front] == 1) cnt--;
                    front++;
                }

                ret = Math.Min(ret, back - front + 2);
            }

            ret = ret == n + 1 ? -1 : ret;
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
#else
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();

            int[] arr = new int[n];

            int cnt = 0;
            int front = 0;

            int ret = n + 1;

            for (int back = 0; back < n; back++)
            {

                arr[back] = ReadInt();
                if (arr[back] == 1) cnt++;
                if (cnt < k) continue;

                while (cnt == k)
                {

                    if (arr[front] == 1) cnt--;
                    front++;
                }

                ret = Math.Min(ret, back - front + 2);
            }

            ret = ret == n + 1 ? -1 : ret;
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
#endif
        }
    }
}
