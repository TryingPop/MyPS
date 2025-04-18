using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 15
이름 : 배성훈
내용 : 최대 상승
    문제번호 : 25644번

    dp, 구현 문제다.
    해당 날짜에 판매할 때 가장 큰 이득을 얻는 것은
    이전 날짜 중 가장 싼 값에 샀을 때이다.

    그리고 0 ~ i번째의 최솟값을 k라 하면,
    0 ~ i + 1번째의 최솟값은 min(k, arr[i + 1])이 보장됨을 알 수 있다.
    해당 아이디어로 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1545
    {

        static void Main1545(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt(), min = 1_000_000_000;
            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                min = Math.Min(min, cur);
                ret = Math.Max(ret, cur - min);
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
