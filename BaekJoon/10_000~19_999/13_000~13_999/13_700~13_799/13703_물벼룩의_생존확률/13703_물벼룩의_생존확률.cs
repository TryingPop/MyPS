using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 28
이름 : 배성훈
내용 : 물벼룩의 생존확률
    문제번호 : 13703번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1790
    {

        static void Main1790(string[] args)
        {

            int n, k;

            Input();

            GetRet();

            void GetRet()
            {

                int MAX = 200;
                long[] dp = new long[MAX + 1];
                long[] next = new long[MAX + 1];

                dp[k] = 1;

                for (int i = 0; i < n; i++)
                {

                    for (int j = 1; j < MAX; j++)
                    {

                        next[j - 1] += dp[j];
                        next[j + 1] += dp[j];
                    }

                    Swap();
                }

                long ret = 0;
                for (int i = 1; i < MAX; i++)
                {

                    ret += dp[i];
                }

                Console.Write(ret);

                void Swap()
                {

                    for (int i = 0; i < dp.Length; i++)
                    {

                        dp[i] = next[i];
                        next[i] = 0;
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();

                k = int.Parse(temp[0]);
                n = int.Parse(temp[1]);
            }
        }
    }
}
