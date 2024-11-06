using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 2
이름 : 배성훈
내용 : Hippity Hopscotch
    문제번호 : 4421번

    dp 문제다
    길이가 k이하인 가로 세로로만 이동가능하고
    이전 값보다 큰 값인 곳에만 이동가능하기에

    시작지점보다 큰 값을 가진 장소만 조사했다
    그리고 하나씩 이동하면서 최대값을 찾았다

    4중 포문이라 불안했지만 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_1017
    {

        static void Main1017(string[] args)
        {

            StreamReader sr;
            int n, k;
            int[][] board;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dp[0][0] = board[0][0];
                for (int i = dp[0][0]; i <= 100; i++)
                {

                    for (int r = 0; r < n; r++)
                    {

                        for (int c = 0; c < n; c++)
                        {

                            if (board[r][c] == i)
                            {

                                for (int j = -k; j <= k; j++)
                                {

                                    if (r + j < 0 || r + j >= n
                                        || dp[r][c] == -1
                                        || board[r + j][c] <= board[r][c]) continue;

                                    dp[r + j][c] = Math.Max(dp[r + j][c], dp[r][c] + board[r + j][c]);
                                }

                                for (int j = -k; j <= k; j++)
                                {

                                    if (c + j < 0 || c + j >= n
                                        || dp[r][c] == -1
                                        || board[r][c + j] <= board[r][c]) continue;

                                    dp[r][c + j] = Math.Max(dp[r][c + j], dp[r][c] + board[r][c + j]);
                                }
                            }
                        }
                    }
                }

                int ret = dp[0][0];
                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        ret = Math.Max(dp[r][c], ret);
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                board = new int[n][];
                dp = new int[n][];

                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    dp[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        board[r][c] = ReadInt();
                        dp[r][c] = -1;
                    }
                }
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
// #include <cstring>
// #include <iostream>
// #include <algorithm>
// #define MAX 101

using namespace std;

int n, k;
int grid[MAX][MAX];
int cache[MAX][MAX];
int dx[4] = {1, 0, -1, 0};
int dy[4] = {0, 1, 0, -1};

int dp(int x, int y){
    int &ret = cache[x][y];
    if(ret != -1){
        return ret;
    }
    ret = grid[x][y];
    for(int i = 0; i < 4; ++i){
        for(int j = 1; j <= k; ++j){
            int nx = x + dx[i] * j, ny = y + dy[i] * j;
            if(!(nx >= 0 && nx < n && ny >= 0 && ny < n)){
                break;
            }
            if(grid[nx][ny] > grid[x][y]){
                ret = max(ret, grid[x][y] + dp(nx, ny));
            }
        }
    }
    return ret;
}

int main(){
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cin >> n >> k;
    for(int i = 0; i < n; ++i){
        for(int j = 0; j < n; ++j){
            cin >> grid[i][j];
        }
    }
    memset(cache, -1, sizeof(cache));
    cout << dp(0, 0) << "\n";
    return 0;
}

#elif other2
#endif
}
