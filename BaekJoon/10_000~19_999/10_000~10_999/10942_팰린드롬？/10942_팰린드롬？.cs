using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 13
이름 : 배성훈
내용 : 팰린드롬?
    문제번호 : 10942번

    문자열이 주어졌을 때 부분 문자열이 팰린드롬인지 확인하는 문제이다!
*/

namespace BaekJoon.etc
{
    internal class etc_0025
    {

        static void Main25(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int arrLen = ReadInt(sr);

            int[] nums = new int[arrLen];
            for (int i = 0; i < arrLen; i++)
            {

                nums[i] = ReadInt(sr);
            }

            // i to j 까지 팰린드롬인지 확인!
            bool[,] dp = new bool[arrLen, arrLen];

            // 이제 조사!
            // 길이를 늘려나가는 방식이다
            for (int i = 0; i < arrLen; i++)
            {

                dp[i, i] = true;
            }

            for (int i = 0; i < arrLen - 1; i++)
            {

                if (nums[i] == nums[i + 1]) dp[i, i + 1] = true;
            }

            for (int addLen = 2; addLen < arrLen; addLen++)
            {

                for (int i = 0; i < arrLen - addLen; i++)
                {

                    if (nums[i] == nums[i + addLen] && dp[i + 1, i + addLen - 1]) dp[i, i + addLen] = true;
                }
            }

            // 결과 확인!
            int test = ReadInt(sr);
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                while (test-- > 0)
                {

                    int s = ReadInt(sr);
                    int e = ReadInt(sr);

                    sw.WriteLine(dp[s - 1, e - 1] ? 1 : 0);
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }
    }
}
