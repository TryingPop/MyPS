using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 22
이름 : 배성훈
내용 : 점수따먹기
    문제번호 : 1749번

    dp, 브루트포스, 누적합 문제다
    누적합에 누적합을 이용하면 row * row * col * col이지만
    누적합에 그리디를 이용하여 row * row * col나 row * col * col 시간으로 줄일 수 있다

    처음에는 누적합에 누적합을 적용해 푸니 1.1초가 걸렸고
    다른 사람의 풀이를 참조해 그리디를 적용하니 76ms로 10배 이상 줄었다
*/

namespace BaekJoon.etc
{
    internal class etc_0832
    {

        static void Main832(string[] args)
        {

            StreamReader sr;
            int row, col;

            int[][] csum;

            int ret;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int r = 1; r <= col; r++)
                {

                    for (int l = 1; l < r; l++)
                    {

                        int sum = 0;
                        for (int k = 1; k <= row; k++)
                        {

                            int cur = csum[k][r] - csum[k][l];
                            sum = Math.Max(cur, sum + cur);
                            ret = Math.Max(ret, sum);
                        }
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                ret = -10_001;
                csum = new int[row + 1][];

                csum[0] = new int[col + 1];
                for (int r = 1; r <= row; r++)
                {

                    csum[r] = new int[col + 1];
                    for (int c = 1; c <= col; c++)
                    {

                        int cur = ReadInt();
                        csum[r][c] = csum[r][c - 1] + cur;

                        if (ret < cur) ret = cur;
                    }
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
// #include <iostream>
// #define INF 987654321
using namespace std;

int R, C, ans = -INF;
int board[200][200];

int main() {
    cin.tie(NULL);  ios_base::sync_with_stdio(false);
    cin >> R >> C;
    for (int i = 0; i < R; i++) {
        for (int j = 0; j < C; j++) {
            cin >> board[i][j];
            if (i) {
                board[i][j] += board[i - 1][j];
            }
        }
    }

    for (int i = 0; i < R; i++) {
        for (int j = i; j < R; j++) {
            int sum = 0;
            for (int k = 0; k < C; k++) {
                int val = board[j][k] - (i ? board[i - 1][k] : 0);
                sum = max(val, sum + val);
                ans = max(ans, sum);
            }
        }
    }
    cout << ans << '\n';
}
#endif
}
