using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 12
이름 : 배성훈
내용 : 논리학 교수
    문제번호 : 1813번

    애드 혹 문제다.
    논리적으로 성립하는 경우를 보면
    i개가 맞다는 말은 i개 맞다는 사람이 정확히 i명 있어야 한다.
    그래서 i명이 있는지 확인하면서 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1331
    {

        static void Main1331(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            int[] cnt = new int[51];
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                cnt[cur]++;
            }

            int ret = -1;
            for (int i = 0; i <= n; i++)
            {

                if (i == cnt[i]) ret = i;
            }

            Console.Write(ret);

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
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
