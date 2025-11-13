using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 6
이름 : 배성훈
내용 : 로프
    문제번호 : 2217번

    수학, 그리디, 정렬 문제다.
    누적합을 이용해 풀면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1964
    {

        static void Main1964(string[] args)
        {

            int MAX = 10_000;
            int[] cnt;
            int n;

            Input();

            GetRet();

            void GetRet()
            {

                for (int i = MAX - 1; i >= 0; i--)
                {

                    cnt[i] += cnt[i + 1];
                }

                int ret = 0;
                for (int i = 1; i <= MAX; i++)
                {

                    ret = Math.Max(i * cnt[i], ret);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                cnt = new int[MAX + 1];
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    cnt[cur]++;
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
}
