using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 28
이름 : 배성훈
내용 : 소가 정보섬에 올라온 이유
    문제번호 : 17128번

    누적 합 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1955
    {

        static void Main1955(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int q = ReadInt();

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            long ret = 0;
            for (int i = 0; i < n; i++)
            {

                ret += GetVal(i);
            }

            while (q-- > 0)
            {

                int idx = ReadInt() - 1;
                arr[idx] = -arr[idx];
                int s = n + idx - 3;
                int e = n + idx;

                for (int i = s; i <= e; i++)
                {

                    ret += 2 * GetVal(i);
                }

                sw.Write(ret);
                sw.Write('\n');
            }

            int GetVal(int idx)
                => arr[idx % n] * arr[(idx + 1) % n] * arr[(idx + 2) % n] * arr[(idx + 3) % n];

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
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }
}
