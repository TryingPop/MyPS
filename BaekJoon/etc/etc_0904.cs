using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 23
이름 : 배성훈
내용 : 가장 큰 정사각형
    문제번호 : 1915번

    dp 문제다
    프로그래머스에서 낮에 푼 문제다
    다만, 최대값 찾는걸 바로 dp 연산과 동시에 진행하는데
    r = 0, c = 0 부분을 확인안해 3번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0904
    {

        static void Main904(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] dp;
            string[] board;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                for (int c = 0; c < col; c++)
                {

                    int cur = board[0][c] - '0';
                    dp[0][c] = cur;

                    if (ret < cur) ret = cur;
                }

                for (int r = 1; r < row; r++)
                {

                    int cur = board[r][0] - '0';
                    dp[r][0] = cur;

                    if (ret < cur) ret = cur;
                }

                for (int r = 1; r < row; r++)
                {

                    for (int c = 1; c < col; c++)
                    {

                        if (board[r][c] == '0') continue;
                        int max = Math.Min(dp[r - 1][c], dp[r][c - 1]);
                        max = Math.Min(max, dp[r - 1][c - 1]);

                        max++;

                        dp[r][c] = max;
                        if (ret < max) ret = max;
                    }
                }

                Console.Write(ret * ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                row = ReadInt();
                col = ReadInt();

                board = new string[row];
                dp = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = sr.ReadLine().Trim();
                    dp[r] = new int[col];
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
var arr = Console.ReadLine().Split();
int n = int.Parse(arr[0]), m = int.Parse(arr[1]);
int[,] mat = new int[n, m];
int[,] dp = new int[n, m];
int max = 0;
for (int i = 0; i < n; i++)
{
    var row = Console.ReadLine();
    for (int j = 0; j < m; j++)
    {
        mat[i, j] = row[j] - '0';
        if (mat[i, j] == 1)
        {
            if (i == 0 || j == 0)
                dp[i, j] = 1;
            else
                dp[i, j] = Math.Min(Math.Min(dp[i - 1, j], dp[i, j - 1]), dp[i - 1, j - 1]) + 1;
            if (dp[i, j] > max)
                max = dp[i, j];
        }
    }
}
Console.WriteLine(max * max);
#endif
}
