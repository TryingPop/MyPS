using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 동전 2
    문제번호 : 2294번

    dp 문제다
    배낭문제 아이디어를 이용해서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0318
    {

        static void Main318(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int k = ReadInt(sr);

            int pop = 0;
            int[] coin = new int[n];
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt(sr);
                coin[i] = cur;
                if (cur > k) pop++;
                else if (cur == k)
                {

                    Console.WriteLine(1);
                    return;
                }
            }

            sr.Close();

            Array.Sort(coin);
            
            n -= pop;

            int[] dp = new int[k + 1];
            Array.Fill(dp, -1);
            dp[0] = 0;
            for (int i = 0; i <= k; i++)
            {

                if (dp[i] == -1) continue;

                int cur = dp[i];

                for (int j = 0; j < n; j++)
                {

                    int next = i + coin[j];
                    if (k < next) break;

                    if (dp[next] > 0 && cur + 1 >= dp[next]) continue;
                    dp[next] = cur + 1;
                }
            }

            Console.WriteLine(dp[k]);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
