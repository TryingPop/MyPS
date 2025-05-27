using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 26
이름 : 배성훈
내용 : 우유 도시
    문제번호 : 14722번

    dp 문제다.
    우유를 먹을 수 있으면 최대한 먹으면서 진행하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1644
    {

        static void Main1644(string[] args)
        {

            int n;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int[][] dp = new int[n + 1][];

                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new int[n + 1];
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        dp[i][j] = Math.Max(dp[i - 1][j], dp[i][j - 1]);
                        if (board[i - 1][j - 1] == dp[i][j] % 3) dp[i][j]++;
                    }
                }

                Console.Write(dp[n][n]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                board = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        board[i][j] = ReadInt();
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

#if other
// #include <iostream>

int dp[1000];

int main()
{
    std::ios::sync_with_stdio(false);
    std::cin.tie(__null);

    int n, i, j, temp, west, north, big;
    std::cin >> n;
    for (i = 0; i < n; ++i) {
        west = 0;
        for (j = 0; j < n; ++j) {
            north = dp[j];
            big = (north >= west) ? north : west;
            std::cin >> temp;
            dp[j] = west = (temp == big % 3) ? big + 1 : big;
        }
    }

    std::cout << dp[n-1] << std::endl;
    return 0;
}
#endif
}
