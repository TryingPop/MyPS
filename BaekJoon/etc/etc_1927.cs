using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 4
이름 : 배성훈
내용 : 대한민국을 지키는 가장 긴 힘
    문제번호 : 31263번

    dp, 그리디 문제다.
    dp[i]를  i번 자리까지 확인했을 때 최소 인원을 담기게 한다.
    그러면 dp[S]는 그리디로 최소가 보장된다.
    해가 존재하므로 따로 존재하지 않는 경우는 확인하지 않았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1927
    {

        static void Main1927(string[] args)
        {

            int n;
            string str;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dp = new int[n + 1];
                Array.Fill(dp, 12_345);
                dp[0] = 0;
                for (int s = 0; s < n; s++)
                {

                    for (int len = 1; len <= 3; len++)
                    {

                        int cur = GetNum(s, len);
                        if (cur > 641) break;

                        dp[s + len] = Math.Min(dp[s + len], dp[s] + 1);
                    }
                }

                Console.Write(dp[n]);

                int GetNum(int s, int len)
                {

                    if (s + len > n || str[s] == '0') return 12_345;
                    int ret = 0;

                    for (int i = 0; i < len; i++)
                    {

                        ret = ret * 10 + str[i + s] - '0';
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                str = sr.ReadLine();
            }
        }
    }
}
