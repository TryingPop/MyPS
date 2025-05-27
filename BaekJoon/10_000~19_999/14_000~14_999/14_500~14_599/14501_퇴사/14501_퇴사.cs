using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 퇴사
    문제번호 : 14501번

    배낭 문제처럼 풀었다
    다만 시점은 고정되어져 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0188
    {

        static void Main188(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt(sr);

            int[] times = new int[n];
            int[] values = new int[n];

            for (int i = 0; i < n; i++)
            {

                times[i] = ReadInt(sr);
                values[i] = ReadInt(sr);
            }

            sr.Close();

            // 일 끝나고 idx 일에 얻는 최대 페이
            int[] dp = new int[n + 1];
            for (int i = 0; i < n; i++)
            {

                // 현재 시점 + 걸리는 시간 = 종료 날짜
                // 당일에 일이 끝나는 경우 다음날이 되게 한다
                int time = i + times[i];

                // n을 포함 안하는 이유는 다음날이 되게 세팅했기 때문이다
                // n일에 일을 안하기에 결과에는 영향 안미친다!
                if (time > n) continue;

                // 이전에 최대 페이
                int beforeMaxVal = 0;
                for (int j = 0; j <= i; j++)
                {

                    if (dp[j] > beforeMaxVal) beforeMaxVal = dp[j];
                }
                // 해당 일을 할 때 최대 페이
                int curVal = beforeMaxVal + values[i];

                // 해당 날짜의 최대 페이
                if (dp[time] < curVal) dp[time] = curVal;
            }

            int ret = 0;

            for (int i = 0; i <= n; i++)
            {

                if (dp[i] > ret) ret = dp[i];
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
