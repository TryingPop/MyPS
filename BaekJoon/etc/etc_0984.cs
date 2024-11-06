using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 21
이름 : 배성훈
내용 : 진우의 달 여행 (Small, Large)
    문제번호 : 17484, 17485번

    dp 문제다
    중앙부분을 chk로 놓고 ret에 넣지 않아 2번 틀렸다;
    이후에 해당 부분 수정하니 이상없이 통과했다

    아이디어는 다음과 같다
    위에서 아래로 내려오는데 방향에 따라 최솟값을 기록하면서 진행하면 된다
    그리디 알고리즘으로 최소 경로를 이용해 이동하는 경우 최소가 보장됨을 알 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0984
    {

        static void Main984(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int row, col;
            int[][][] dp;
            int[][] board;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {


                // dp[i][j][k]
                // i : 현재(0) or 다음(1)
                // j : 방향 /(0), |(1), \(2)
                dp = new int[2][][];
                for (int i = 0; i < 2; i++)
                {

                    dp[i] = new int[3][];
                    for (int j = 0; j < 3; j++)
                    {

                        dp[i][j] = new int[col];
                    }
                }

                for (int c = 0; c < col; c++)
                {

                    for (int i = 0; i < 3; i++)
                    {

                        dp[0][i][c] = board[0][c];
                    }
                }

                for (int r = 1; r < row; r++)
                {

                    dp[1][0][0] = Math.Min(dp[0][1][1], dp[0][2][1]) + board[r][0];
                    dp[1][1][0] = dp[0][0][0] + board[r][0];

                    dp[1][1][col - 1] = dp[0][2][col - 1] + board[r][col - 1];
                    dp[1][2][col - 1] = Math.Min(dp[0][1][col - 2], dp[0][0][col - 2]) + board[r][col - 1];

                    for (int c = 1; c < col - 1; c++)
                    {

                        dp[1][0][c] = Math.Min(dp[0][1][c + 1], dp[0][2][c + 1]) + board[r][c];
                        dp[1][1][c] = Math.Min(dp[0][0][c], dp[0][2][c]) + board[r][c];
                        dp[1][2][c] = Math.Min(dp[0][0][c - 1], dp[0][1][c - 1]) + board[r][c];
                    }

                    var temp = dp[1];
                    dp[1] = dp[0];
                    dp[0] = temp;
                }

                int ret = Math.Min(Math.Min(dp[0][0][0], dp[0][1][0]),
                    Math.Min(dp[0][1][col - 1], dp[0][2][col - 1]));

                for (int i = 0; i < 3; i++)
                {


                    for (int c = 1; c < col - 1; c++)
                    {

                        ret = Math.Min(ret, dp[0][i][c]);
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                sr.Close();
            }

            bool TryReadInt(out int _ret)
            {

                int c = sr.Read();
                _ret = 0;
                if (c == -1 || c == ' ' || c == '\n') return true;
                _ret = c - '0';

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    _ret = _ret * 10 + c - '0';
                }

                return false;
            }

            int ReadInt()
            {

                int ret;

                while (TryReadInt(out ret)) { }

                return ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
// #include <sys/mman.h>
using namespace std;

// #define R(n) int n = 0; do n = 10 * n + *p - 48; while (*++p & 16); p++;
int main() {
	char *p = (char*)mmap(0, 1 << 22, 1, 1, 0, 0);
	R(n) R(m);

	vector dp(m, vector(3, 0)), ndp(m, vector(3, 0));
	for (int _ = 0; _ < n; _++) {
		for (int i = 0; i < m; i++) {
			R(x)
			for (int j = 0; j < 3; j++) {
				ndp[i][j] = 1 << 30;
				int p = i + j - 1;
				if (p < 0 || p >= m) continue;
				for (int k = 0; k < 3; k++) if (j != k) ndp[i][j] = min(ndp[i][j], dp[p][k] + x);
			}
		}
		dp.swap(ndp);
	}

	int res = 1 << 30;
	for (int i = 0; i < m; i++) for (int j = 0; j < 3; j++) res = min(res, dp[i][j]);
	cout << res << '\n';
}
#endif
}
