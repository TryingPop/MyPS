using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 4
이름 : 배성훈
내용 : Wifi Setup
    문제번호 : 5864번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1675
    {

        static void Main1675(string[] args)
        {

            int n;
            long a, b;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                if (n == 0)
                {

                    Console.Write(0);
                    return;
                }

                long INF = 1_000_000_000_000_000_000;
                long[] dp = new long[n + 1];
                Array.Fill(dp, INF);
                dp[0] = 0;
                Array.Sort(arr);

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j < i; j++)
                    {

                        dp[i] = Math.Min(dp[i], dp[j - 1] + GetVal(arr[j], arr[i]));
                    }

                    dp[i] = Math.Min(dp[i], dp[i - 1] + a);
                }

                Console.Write(dp[n] / 2.0);

                long GetVal(int _f, int _t)
                    => a + b * ((_t - _f) / 2);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                a = ReadInt() << 1;
                b = ReadInt();
                arr = new int[n + 1];

                arr[0] = -1;
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt() << 1;
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
