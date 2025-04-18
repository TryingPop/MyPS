using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 16
이름 : 배성훈
내용 : 팰린드롬 파티션
    문제번호 : 2705번

    7인 경우
            7                                   dp[0]

                                                +
            1   5   1                           dp[1]

                                                +
            2   3   2                           dp[2]
            1   1   3   1   1

                                                +
            3   1   3                           dp[3]
            1   1   1   1   1   1   1

    문제 조건을 잘보면... 을 얻을 수 있다;
*/

namespace BaekJoon.etc
{
    internal class etc_0047
    {

        static void Main47(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = ReadInt(sr);
            int[] dp = new int[1_001];
            dp[0] = 1;
            dp[1] = 1;
            dp[2] = 2;
            dp[3] = 2;
            int cur = 3;


            while(test-- > 0)
            {

                int find = ReadInt(sr);

                for (int i = cur + 1; i <= find; i++)
                {

                    // 자기 자신!
                    for (int j = i; j >= 0; j -= 2)
                    {

                        dp[i] += dp[(i - j) / 2];
                    }
                }

                if (cur < find) cur = find;
                sw.WriteLine(dp[find]);
            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
