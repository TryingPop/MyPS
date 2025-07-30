using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 29
이름 : 배성훈
내용 : 예쁜수
    문제번호 : 25958번

    dp, 배낭 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1795
    {

        static void Main1795(string[] args)
        {

            int m, mod;

            Input();

            GetRet();

            void GetRet()
            {

                bool[] isB = new bool[m + 1];

                for (int i = 1; i <= m; i++)
                {

                    isB[i] = IsCute(i);
                }

                int[] dp = new int[m + 1];

                dp[0] = 1;

                for (int i = 1; i <= m; i++)
                {

                    if (!isB[i]) continue;

                    for (int j = m; j >= 0; j--)
                    {

                        if (dp[j] == 0) continue;

                        for (int k = j + i; k <= m; k += i)
                        {

                            dp[k] = (dp[k] + dp[j]) % mod;
                        }
                    }
                }

                Console.Write(dp[m]);

                bool IsCute(int _val)
                {

                    int div = _val;
                    int s = 0;

                    while (div / 10 > 0)
                    {

                        s += div % 10;
                        div /= 10;
                    }

                    s += div;

                    return _val % s == 0;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                m = int.Parse(temp[0]);
                mod = int.Parse(temp[1]);
            }
        }
    }
}
