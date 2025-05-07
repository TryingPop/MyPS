using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 7
이름 : 배성훈
내용 : 구슬게임
    문제번호 : 2600번

    dp, 게임이론 문제다.
    공을 가져갈 때 턴을 잡는 경우 지는 경우가 있으면 해당 공을 가져가는게 좋다.
    그래서 dp로 채워가면서 진행했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1618
    {

        static void Main1618(string[] args)
        {

            string T = "A\n";
            string F = "B\n";

            int MAX = 500;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int[] b = new int[3];
            b[0] = ReadInt();
            b[1] = ReadInt();
            b[2] = ReadInt();

            bool[][] dp = new bool[MAX + 1][];
            for (int i = 0; i <= MAX; i++)
            {

                dp[i] = new bool[i + 1];
            }

            for (int i = 0; i <= MAX; i++)
            {

                for (int j = 0; j <= i; j++)
                {

                    if (dp[i][j]) continue;

                    for (int k = 0; k < 3; k++)
                    {

                        int chk1 = i + b[k];
                        int chk2 = j;

                        
                        if (chk1 <= MAX) dp[chk1][chk2] = true;

                        chk1 = i;
                        chk2 = j + b[k];

                        if (chk1 < chk2)
                        {

                            int temp = chk1;
                            chk1 = chk2;
                            chk2 = temp;
                        }

                        if (chk1 <= MAX) dp[chk1][chk2] = true;
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {

                int k1 = ReadInt();
                int k2 = ReadInt();
                if(k1 < k2)
                {

                    int temp = k1;
                    k1 = k2;
                    k2 = temp;
                }

                Console.Write(dp[k1][k2] ? T : F);
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n') 
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
