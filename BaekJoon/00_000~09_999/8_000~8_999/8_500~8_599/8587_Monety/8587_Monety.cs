using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 29
이름 : 배성훈
내용 : Monety
    문제번호 : 8587번

    수학, 조합론 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1591
    {

        static void Main1591(string[] args)
        {

            long MOD = 1_000_000_007;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();

            // 정렬된 상태로 넣기
            int[] cnt = new int[n + 1];
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                cnt[cur]++;
            }

            long ret = 1;
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {

                if (cnt[i] == 0) continue;

                // 남은 빈자리에 넣는 수를 누적
                for (int j = 0; j < cnt[i]; j++)
                {

                    ret = ret * (i - sum++) % MOD;
                }
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
