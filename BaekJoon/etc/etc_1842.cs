using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 27
이름 : 배성훈
내용 : K512에서 피자 먹기
    문제번호 : 28072번

    누적 합 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1842
    {

        static void Main1842(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();

            int[] cnt = new int[k];
            int sum = 0;

            for (int i = 0; i < n; i++)
            {

                sum = (sum + ReadInt()) % k;
                cnt[sum]++;
            }

            int ret = 0;
            for (int i = 0; i < k; i++)
            {

                ret = Math.Max(cnt[i], ret);
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
