using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 14
이름 : 배성훈
내용 : 약속
    문제번호 : 1183번

    수학 문제다.
    절댓값 함수들의 합인데,
    중앙값이 최소가 되게 한다.

    중앙값으로 찾아도 되지만 n이 충분히작아
    브루트포스로 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1274
    {

        static void Main1274(string[] args)
        {

            int n;
            int[] y;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long chkMin = long.MaxValue;

                long[] dp = new long[n];
                for (int i = 0; i < n; i++)
                {

                    dp[i] = Chk(-y[i]);
                    if (chkMin <= dp[i]) continue;
                    chkMin = dp[i];
                }

                int min = int.MaxValue, max = int.MinValue;

                for (int i = 0; i < n; i++)
                {

                    if (chkMin == dp[i])
                    {

                        max = Math.Max(max, -y[i]);
                        min = Math.Min(min, -y[i]);
                    }
                }

                Console.Write(max - min + 1);

                long Chk(long _val)
                {

                    long ret = 0;
                    for (int i = 0; i < n; i++)
                    {

                        ret += Math.Abs(_val + y[i]);
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                y = new int[n];

                for (int i = 0; i < n; i++)
                {

                    y[i] = ReadInt() - ReadInt();
                }

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
}
