using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 10
이름 : 배성훈
내용 : 시로코와 은행털기
    문제번호 : 26607번
*/

namespace BaekJoon.etc
{
    internal class etc_1690
    {

        static void Main1690(string[] args)
        {

            int n, k, x;
            (int a, int b)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                bool[][] dp = new bool[k + 1][];
                for (int i = 0; i <= k; i++)
                {

                    dp[i] = new bool[x * i + 1];
                }

                dp[0][0] = true;

                for (int i = 0; i < k; i++)
                {

                    for (int j = i; j >= 0; j--)
                    {

                        for (int s = 0; s < dp[j].Length; s++)
                        {

                            if (dp[j][s])
                                dp[j + 1][s + arr[i].a] = true;
                        }
                    }
                }

                for (int i = k; i < n; i++)
                {

                    for (int j = k - 1; j >= 0; j--)
                    {

                        for (int s = 0; s < dp[j].Length; s++)
                        {

                            if (dp[j][s])
                                dp[j + 1][s + arr[i].a] = true;
                        }
                    }
                }

                int sum = k * x;
                int ret = 0;
                for (int i = 0; i < dp[k].Length; i++)
                {

                    if (dp[k][i])
                    {

                        int chk = (sum - i) * i;
                        ret = Math.Max(ret, chk);
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();
                x = ReadInt();

                arr = new (int a, int b)[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
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
