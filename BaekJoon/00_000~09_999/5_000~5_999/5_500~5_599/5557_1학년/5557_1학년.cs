using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 1학년
    문제번호 : 5557번

    dp 문제다
    해가 가능한 경우를 세는게 주된 아이디어다
    비슷한 유형 몇개 푼 기억은 있으나, 바로 안떠오르는거 보면, 해당 부분 더 연습해야 겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0325
    {

        static void Main325(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            long[,] dp = new long[n, 21];
            int[] arr = new int[n];
            
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            sr.Close();

            dp[0, arr[0]] = 1;
            for (int i = 1; i < n - 1; i++)
            {

                for (int j = 0; j <= 20; j++)
                {

                    if (dp[i - 1, j] == 0) continue;

                    if (j + arr[i] <= 20) dp[i, j + arr[i]] += dp[i - 1, j];
                    if (j - arr[i] >= 0) dp[i, j - arr[i]] += dp[i - 1, j];
                }
            }

            long ret = dp[n - 2, arr[n - 1]];

            Console.WriteLine(ret);

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
