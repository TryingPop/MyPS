using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 19
이름 : 배성훈
내용 : 직사각형과 쿼리
    문제번호 : 14846번

    dp, 누적합 문제다.
    맵의 크기가 300 x 300이고 주어지는 숫자가 10 이하인 자연수이므로
    각 숫자별로 누적합 배열을 만들어 관리했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1557
    {

        static void Main1557(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();

            int[][][] dp = new int[10][][];
            for (int i = 0; i < 10; i++)
            {

                dp[i] = new int[n + 1][];
                for (int r = 0; r <= n; r++)
                {

                    dp[i][r] = new int[n + 1];
                }
            }

            for (int i = 1; i <= n; i++)
            {

                for (int j = 1; j <= n; j++)
                {

                    int cur = ReadInt() - 1;
                    dp[cur][i][j]++;
                }
            }

            for (int i = 0; i < 10; i++)
            {

                for (int r = 1; r <= n; r++)
                {

                    for (int c = 1; c <= n; c++)
                    {

                        dp[i][r][c] += dp[i][r][c - 1];
                    }
                }

                for (int c = 1; c <= n; c++)
                {

                    for (int r = 1; r <= n; r++)
                    {

                        dp[i][r][c] += dp[i][r - 1][c];
                    }
                }
            }

            int t = ReadInt();

            while (t-- > 0)
            {

                int r1 = ReadInt();
                int c1 = ReadInt();

                int r2 = ReadInt();
                int c2 = ReadInt();

                int ret = Query(r1, c1, r2, c2);
                sw.Write($"{ret}\n");
            }

            int Query(int _r1, int _c1, int _r2, int _c2)
            {

                int ret = 0;

                for (int i = 0; i < 10; i++)
                {

                    ret += GetVal(i);
                }

                return ret;

                int GetVal(int _i)
                {

                    int cur = dp[_i][_r2][_c2] - dp[_i][_r2][_c1 - 1]
                        - dp[_i][_r1 - 1][_c2] + dp[_i][_r1 - 1][_c1 - 1];

                    return cur > 0 ? 1 : 0;
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

#if other
// #include <iostream>
using namespace std;

int N, Q;
int dp[301][301][10];

int main() {
	ios::sync_with_stdio(0);
	cin.tie(0);

	cin >> N; N++;
	for (int r = 1; r < N; r++) {
		for (int c = 1; c < N; c++) {
			for (int i = 0; i < 10; i++) dp[r][c][i] = dp[r - 1][c][i] + dp[r][c - 1][i] - dp[r - 1][c - 1][i];
			int x; cin >> x;
			dp[r][c][x - 1]++;
		}
	}

	cin >> Q;
	while (Q--) {
		int ret = 0;
		int xL, xR, yL, yR; cin >> xL >> yL >> xR >> yR;
		xL--, yL--;
		for (int i = 0; i < 10; i++) {
			if (dp[xR][yR][i] - dp[xL][yR][i] - dp[xR][yL][i] + dp[xL][yL][i]) ret++;
		}

		cout << ret << '\n';
	}
}
#endif
}
