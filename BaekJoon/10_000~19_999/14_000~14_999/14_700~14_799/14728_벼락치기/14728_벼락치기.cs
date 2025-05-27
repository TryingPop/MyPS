using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 벼락치기
    문제번호 : 14728번

    dp는 인덱스를 해당 시간으로 하고, 값을 최대 점수로 잡아 완전 탐색으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0126
    {

        static void Main126(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int total = ReadInt(sr);

            (int time, int score)[] test = new (int time, int score)[len];
            for (int i = 0; i < len; i++)
            {

                test[i] = (ReadInt(sr), ReadInt(sr));
            }

            sr.Close();

            // 해당 시간에 가장 높은 점수를 보관
            int[] dp = new int[total + 1];
            Array.Fill(dp, -1);
            dp[0] = 0;
            // 이제 점수 계산
            for (int i = 0; i < len; i++)
            {

                for (int j = total; j >= 0; j--)
                {

                    if (dp[j] == -1) continue;
                    int chkScore = test[i].score + dp[j];
                    int chkTime = j + test[i].time;
                    if (chkTime <= total && dp[chkTime] < chkScore) 
                    { 
                        
                        dp[chkTime] = chkScore;
                    }
                }
            }

            int ret = 0;
            for (int i = 1; i <= total; i++)
            {

                if (ret < dp[i]) ret = dp[i];
            }

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
