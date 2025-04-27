using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 27
이름 : 배성훈
내용 : 즐거운 회의
    문제번호 : 31997번

    누적 합 문제다.
    친한 사람이 주어질 때 만나는 시간에 누적합을 진행하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1584
    {

        static void Main1584(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();
            int t = ReadInt();

            (int s, int e)[] arr = new (int s, int e)[n + 1];
            for (int i = 1; i <= n; i++)
            {

                arr[i] = (ReadInt(), ReadInt());
            }

            long[] ret = new long[t + 1];
            for (int i = 0; i < m; i++)
            {

                int h1 = ReadInt();
                int h2 = ReadInt();

                int start = Math.Max(arr[h1].s, arr[h2].s);
                int end = Math.Min(arr[h1].e, arr[h2].e);

                if (end < start) continue;
                ret[start]++;
                ret[end]--;
            }

            for (int i = 0; i < t; i++)
            {

                sw.Write($"{ret[i]}\n");
                ret[i + 1] += ret[i];
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
