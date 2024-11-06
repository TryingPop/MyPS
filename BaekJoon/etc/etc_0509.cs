using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 양팔저울
    문제번호 : 25943번

    구현, 그리디 알고리즘 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0509
    {

        static void Main509(string[] args)
        {

            int[] weights = { 100, 50, 20, 10, 5, 2, 1 };
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int l = 0;
            int r = 0;

            for (int i = 0; i < n; i++)
            {

                if (l <= r) l += ReadInt();
                else r += ReadInt();
            }

            sr.Close();

            int diff = l - r;
            diff = diff < 0 ? -diff : diff;

            int ret = 0;

            for (int i = 0; i < weights.Length; i++)
            {

                ret += diff / weights[i];
                diff %= weights[i];
            }

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
