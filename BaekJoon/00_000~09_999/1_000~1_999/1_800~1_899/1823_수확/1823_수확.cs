using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 11
이름 : 배성훈
내용 : 수확
    문제번호 : 1823번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1693
    {

        static void Main1693(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                // dp[f][t] = val
                // 남은 구간이 [f, t]를 수확할 때 벼의 가치의 최댓값
                int[][] dp = new int[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new int[n + 1];
                    // -1 : 미방문
                    Array.Fill(dp[i], -1);
                }

                Console.Write(DFS(1, n));

                int DFS(int _f, int _t, int _cnt = 1)
                {

                    if (_t < 1 || _f > n) return 0;
                    ref int ret = ref dp[_f][_t];

                    if (ret != -1) return ret;
                    else if (_t < _f)
                    {

                        ret = 0;
                        return ret;
                    }

                    ret = Math.Max(DFS(_f + 1, _t, _cnt + 1) + _cnt * arr[_f], DFS(_f, _t - 1, _cnt + 1) + _cnt * arr[_t]);

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
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
