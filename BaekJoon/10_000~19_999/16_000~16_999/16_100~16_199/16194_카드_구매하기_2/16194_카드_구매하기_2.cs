using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 카드 구매하기 2
    문제번호 : 16194번

    dp 문제다
    배낭문제처럼 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0413
    {

        static void Main413(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();
            int[] arr = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {

                arr[i] = ReadInt();
            }
            sr.Close();

            int[] dp = new int[n + 1];

            for (int i = 0; i < n; i++)
            {

                for (int j = 1; j <= n; j++)
                {

                    if (i + j > n) break;
                    int chk = dp[i] + arr[j];
                    if (dp[i + j] == 0 || dp[i + j] > chk) dp[i + j] = chk;
                }
            }

            Console.WriteLine(dp[n]);

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
