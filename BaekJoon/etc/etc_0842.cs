using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 27
이름 : 배성훈
내용 : 파이프 옮기기 1, 2
    문제번호 : 17070번, 17069번

    dp 문제다
    파이프 옮기기 2에서 자료형 범위를 잘못 설정해 1번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0842
    {

        static void Main842(string[] args)
        {

            StreamReader sr;
            int[][] board;
            long[][][] dp;
            int n;

            Solve();
            void Solve()
            {

                Input();

                dp[n - 1][n - 1][0] = 1;
                dp[n - 1][n - 1][1] = 1;
                dp[n - 1][n - 1][2] = 1;
                
                long ret = DFS();
                Console.Write(ret);
            }

            long DFS(int _r = 0, int _c = 1, int _type = 0)
            {

                if (_r >= n || _c >= n) return 0;
                else if (dp[_r][_c][_type] != -1) return dp[_r][_c][_type];
                long ret = 0;

                if (_type != 2 && _c + 1 < n 
                    && board[_r][_c + 1] == 0) ret += DFS(_r, _c + 1, 0);

                if (_type != 0 && _r + 1 < n 
                    && board[_r + 1][_c] == 0) ret += DFS(_r + 1, _c, 2);

                if (_r + 1 < n && _c + 1 < n
                    && board[_r + 1][_c] == 0
                    && board[_r][_c + 1] == 0
                    && board[_r + 1][_c + 1] == 0) ret += DFS(_r + 1, _c + 1, 1);

                return dp[_r][_c][_type] = ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                board = new int[n][];
                dp = new long[n][][];
                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];
                    dp[i] = new long[n][];
                    for (int j = 0; j < n; j++)
                    {

                        dp[i][j] = new long[3];
                        board[i][j] = ReadInt();
                        Array.Fill(dp[i][j], -1);
                    }
                }

                sr.Close();
            }

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

#if other
// #include <stdio.h>
int main() {
	int dp[17][17][3]{}, map[17][17]{}, N;
	scanf("%d", &N);
	for (int r = 1; r <= N; ++r) for (int c = 1; c <= N; ++c) scanf("%d", &map[r][c]);

	dp[1][2][0] = 1;
	for (int r = 1; r <= N; ++r) for (int c = 3; c <= N; ++c) {

		if (map[r][c]) continue;
		dp[r][c][0] = dp[r][c-1][0] + dp[r][c-1][1];
		dp[r][c][2] = dp[r-1][c][1] + dp[r-1][c][2];

		if (map[r-1][c] || map[r][c-1]) continue;
		dp[r][c][1] = dp[r-1][c-1][0] + dp[r-1][c-1][1] + dp[r-1][c-1][2];
	}

	printf("%d", dp[N][N][0] + dp[N][N][1] + dp[N][N][2]);
}
#endif
}
