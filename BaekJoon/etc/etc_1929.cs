using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 6
이름 : 배성훈
내용 : 인터뷰
    문제번호 : 27914번

    누적 합 문제다.
    아이디어는 다음과 같다.
    i번째를 끝으로 하는 가장 긴 경우를 찾는다.
    그러면 i번째를 끝으로 하는 경우는 가장 긴 경우개수만큼 나온다.
    이를 누적해주면 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1929
    {

        static void Main1929(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();
            int q = ReadInt();

            int[] sum = new int[n + 1];
            long[] ret = new long[n + 1];

            for (int i = 1; i <= n; i++)
            {

                int cur = ReadInt();
                if (cur == k) sum[i] = 0;
                else sum[i] = sum[i - 1] + 1;
                ret[i] = ret[i - 1] + sum[i];
            }

            for (int i = 0; i < q; i++)
            {

                int val = ReadInt();

                sw.Write($"{ret[val]}\n");
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
