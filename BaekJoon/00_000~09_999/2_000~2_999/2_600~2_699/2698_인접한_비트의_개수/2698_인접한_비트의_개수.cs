using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 13
이름 : 배성훈
내용 : 인접한 비트의 개수
    문제번호 : 2698번

    dp 문제다.
    그러니 끝에 이어붙이는 1, 0 을 이어붙인 갯수로 dp를 설정하니
    dp[i][j][k] = val를
    길이 i이고, 인접한 갯수가 j이고 끝 값이 k인 갯수를 val로 dp를 잡았다.
    끝에 0을 붙이는 경우는 앞에 0, 1의 경우의 수가 누적된다.
    dp[i + 1][j][0] = dp[i][j][0] + dp[i][j][1]이다.
    
    끝에 1인 경우 같은 인접 갯수는 0일 때 마지막에 1을 붙인 경우다.
    dp[i + 1][j][1] += dp[i][j][0]

    반면 끝에 1인데 1을 이어붙이면 인접 갯수가 1개 증가한다.
    dp[i + 1][j + 1][1] = dp[i][j][1]

    이러한 점화식을 얻을 수 있다.
    문제에서 2^32 - 1보다 작거나 같다로 보장했으므로 int 자료형을 잡았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1541
    {

        static void Main1541(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);


            int[][][] dp;

            SetDp();

            int t = ReadInt();
            while (t-- > 0)
            {

                int len = ReadInt();
                int adj = ReadInt();

                sw.Write($"{dp[len][adj][0] + dp[len][adj][1]}\n");
            }

            void SetDp()
            {

                // dp[i][j][k]
                // i : 길이
                // j : 인접한 갯수
                // k : 맨 뒤에 이어붙이는 형식이므로 끝 값
                dp = new int[101][][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new int[i][];
                    for (int j = 0; j < i; j++)
                    {

                        dp[i][j] = new int[2];
                    }
                }

                dp[2][1][1] = 1;
                dp[2][0][1] = 1;
                dp[2][0][0] = 2;

                for (int len = 3; len < dp.Length; len++)
                {

                    int prev = len - 1;
                    for (int adj = 0; adj < dp[prev].Length; adj++)
                    {

                        dp[len][adj][0] += dp[prev][adj][0] + dp[prev][adj][1];
                        dp[len][adj][1] += dp[prev][adj][0];

                        dp[len][adj + 1][1] += dp[prev][adj][1];
                    }
                }
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

                    while((c =sr.Read()) != -1 && c != ' ' && c != '\n')
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
