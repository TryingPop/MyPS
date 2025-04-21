using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 징검다리 건너기 (small)
    문제번호 : 22869번

    dp 문제다
    문제를 잘못읽어 힘을 쓰면 힘이 빠지는 줄 알아 1번 틀렸다
    그래서 힘을 기록해서 가는 방법으로 틀렸다

    이후에 힘이 빠지는게 아님을 알고, 지나갈 수 있으면 1, 못지나가면 0을 기록했다
    그리고 다음 지나갈 수 있는 길에 한해 1로 표기했다

    이중포문 N^2 탐색으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0303
    {

        static void Main303(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int k = ReadInt(sr);

            int[] pow = new int[n];
            for (int i = 0; i < n; i++)
            {

                pow[i] = ReadInt(sr);
            }

            sr.Close();

            int[] dp = new int[n];
            dp[0] = 1;
            for (int i = 0; i < n; i++)
            {

                if (dp[i] == 0) continue;

                for (int j = i + 1; j < n; j++)
                {

                    int powDiff = pow[j] - pow[i];
                    powDiff = powDiff < 0 ? -powDiff : powDiff;

                    int usePow = (j - i) * (1 + powDiff);
                    if (usePow > k) continue;
                    dp[j] = 1;
                }
            }

            if (dp[n - 1] == 1) Console.WriteLine("YES");
            else Console.WriteLine("NO");
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
