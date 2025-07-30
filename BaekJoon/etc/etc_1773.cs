using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 17
이름 : 배성훈
내용 : 욱제는 사과팬이야!!
    문제번호 : 15924번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1773
    {

        static void Main1773(string[] args)
        {

            int row, col;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int MOD = 1_000_000_009;
                int[] dp = new int[col];
                dp[col - 1] = 1;
                int ret = 0;

                for (int r = row - 1; r >= 1; r--)
                {

                    for (int c = col - 1; c >= 1; c--)
                    {

                        ret = (ret + dp[c]) % MOD;
                        if (board[r][c - 1] != 2)
                            dp[c - 1] = (dp[c - 1] + dp[c]) % MOD;

                        if (board[r - 1][c] == 1)
                            dp[c] = 0;
                    }

                    ret = (ret + dp[0]) % MOD;
                    if (board[r - 1][0] == 1)
                        dp[0] = 0;
                }

                for (int c = col - 1; c >= 1; c--)
                {

                    ret = (ret + dp[c]) % MOD;
                    if (board[0][c - 1] != 2)
                        dp[c - 1] = (dp[c - 1] + dp[c]) % MOD;
                }

                ret = (ret + dp[0]) % MOD;
                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == 'E') board[r][c] = 1;
                        else if (cur == 'S') board[r][c] = 2;
                        else board[r][c] = 0;
                    }

                    while (sr.Read() != '\n') ;
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
// #include <stdio.h>
// #define MOD 1000000009

char u[3000 * 3001];

int main() {
	int n, m, i, j, dp[3001] = {0, }, s;	
	scanf("%d%d\n", &n, &m);
	fread(u, 1, 3000 * 3001, stdin);
	s = 1;
	dp[m - 1] = 1;
	i = n - 1;
	for (j = m - 2 ; j >= 0 ; j--) {
		if (u[i * (m + 1)  + j] == 'E' || u[i * (m + 1) + j] == 'B') dp[j] = dp[j + 1], s++;
	}
	for (i = n - 2 ; i >= 0 ; i--) {
		for (j = m - 1 ; j >= 0 ; j--) {
			if (u[i * (m + 1) + j] == 'E') dp[j] = dp[j + 1];
			else if (u[i * (m + 1) + j] == 'B') dp[j] += dp[j + 1];
			if (dp[j] >= MOD) dp[j] -= MOD;
			if ((s += dp[j]) >= MOD) s -= MOD;
		}
	}
	printf("%d", s);
}
#endif
}
