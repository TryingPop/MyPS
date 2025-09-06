using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 5
이름 : 배성훈
내용 : Apsnigtas takelis
    문제번호 : 30058번

    누적 합, 차분 배열 트릭 문제다.
    누적 합 아이디어를 이용해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1867
    {

        static void Main1867(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();

            long[] arr = new long[n + 1];
            for (int i = 1; i <= n; i++)
            {

                arr[i] = ReadInt();

            }
            int[] add = new int[n + 3];
            long ret = 0;
            for (int i = 1; i <= n; i++)
            {

                add[i] += add[i - 1];
                long cur = arr[i] + add[i];
                // 쌓인 눈이 없다.
                if (cur == 0) continue;

                // 눈이 존재!
                ret += cur;
                int s = i + 1;
                long e = Math.Min(n + 2, i + cur + 1);
                add[s]++;
                add[e]--;
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
