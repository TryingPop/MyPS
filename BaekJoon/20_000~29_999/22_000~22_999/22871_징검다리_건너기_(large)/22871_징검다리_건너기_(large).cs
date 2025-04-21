using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 징검다리 건너기 (large)
    문제번호 : 22871번

    dp, 이분탐색 문제다
    그냥 dp와 완전탐색으로 풀어냈다

    다만, 오버플로우로 한 번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0304
    {

        static void Main304(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[] pow = new int[n];

            for (int i = 0; i < n; i++)
            {

                pow[i] = ReadInt(sr);
            }

            sr.Close();

            long[] ret = new long[n];
            for (int i = 0; i < n - 1; i++)
            {

                long totalMinPow = ret[i];

                for (int j = i + 1; j < n; j++)
                {

                    long powDiff = pow[i] - pow[j];
                    powDiff = powDiff < 0 ? -powDiff : powDiff;

                    long usePow = (j - i) * (1 + powDiff);
                    long retPow = usePow < totalMinPow ? totalMinPow : usePow;
                    if (ret[j] != 0 && ret[j] <= retPow) continue;

                    ret[j] = retPow;
                }
            }

            Console.WriteLine(ret[n - 1]);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
