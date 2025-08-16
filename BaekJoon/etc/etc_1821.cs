using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 13
이름 : 배성훈
내용 : 귀찮아 (SIB)
    문제번호 : 14929번

    수학, 누적합 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1821
    {

        static void Main1821(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();

            int[] arr = new int[n];
            long sum = 0;
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
                sum += arr[i];
            }

            long ret = 0;
            for (int i = 0; i < n; i++)
            {

                ret += (sum - arr[i]) * arr[i];
            }

            Console.Write(ret >> 1);

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
