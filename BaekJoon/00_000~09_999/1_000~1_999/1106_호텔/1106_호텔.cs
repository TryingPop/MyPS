using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 25
이름 : 배성훈
내용 : 호텔
    문제번호 : 1106번

    dp를 쓰는 문제이다
    O(N * M)의 시간이 걸린다, N은 찾는 범위, M은 입력 수이다
    M이 많아야 20이고, N이 1000으로 적어서 썼다
    이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0091
    {

        static void Main91(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            int goal = ReadInt(sr);
            int len = ReadInt(sr);

            (int cost, int val)[] chk = new (int cost, int val)[len];

            for (int i = 0; i < len; i++)
            {

                chk[i] = (ReadInt(sr), ReadInt(sr));
            }
            sr.Close();

            int[] dp = new int[goal + 1];
            Array.Fill(dp, -1);

            dp[0] = 0;
            for (int i = 0; i < goal; i++)
            {

                if (dp[i] == -1) continue;

                for (int j = 0; j < len; j++)
                {

                    int idx = i + chk[j].val;

                    if (idx > goal) idx = goal;
                    int val = dp[i] + chk[j].cost;
                    if (dp[idx] == -1 || dp[idx] > val) dp[idx] = val;
                }
            }
            
            Console.WriteLine(dp[goal]);
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
