using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 마트료시카 합치기
    문제번호 : 25631번

    자료구조, 그리디, 정렬 문제이다
    그리디로 숫자의 개수를 해시에 저장한 뒤 최대값으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0235
    {

        static void Main235(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = ReadInt(sr);

            int ret = 0;
            Dictionary<int, int> dp = new(len);

            for (int i = 0; i < len; i++)
            {

                int n = ReadInt(sr);
                if (dp.ContainsKey(n)) dp[n]++;
                else dp[n] = 1;

                if (ret < dp[n]) ret = dp[n];
            }

            sr.Close();

            Console.WriteLine(ret);
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
