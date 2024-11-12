using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 12
이름 : 배성훈
내용 : 게임
    문제번호 : 1103

    다이나믹 프로그래밍, 깊이 우선 탐색 문제다
    사이클이 있는지 판별하는 문제다.
    먼저 끝나는지점을 확인하는 isEnd과 방문 여부를 확인하는 visit으로 구했다.
    dp는 던질 수 있는 최대 횟수를 기록하는데, 사이클이 발견되면 의미 없는 숫자로 변한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1106
    {

        static void Main(string[] args)
        {

            int INF = 10_000;
            int[][] board, dp;

            bool[][] isEnd, visit;  // isEnd : 끝나는 곳으로 가는지 확인, visit : 이미 방문 여부
            int row, col;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };

                // 끝나는 지점을 먼저 true로 한다.
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == -1)
                        {

                            isEnd[r][c] = true;
                            continue;
                        }

                        int jump = board[r][c];
                        bool flag = true;
                        for (int i = 0; i < 4; i++)
                        {

                            int nR = r + dirR[i] * jump;
                            int nC = c + dirC[i] * jump;

                            if (ChkInvalidPos(nR, nC)) continue;
                            flag = false;
                            break;
                        }

                        if (flag)
                        {

                            isEnd[r][c] = true;
                            dp[r][c] = 1;
                        }
                    }
                }

                // 이후 탐색
                int ret = DFS(0, 0);
                if (isEnd[0][0]) Console.Write(ret);
                else Console.Write(-1);

                int DFS(int _r, int _c)
                {

                    if (isEnd[_r][_c] || dp[_r][_c] != 0) return dp[_r][_c];
                    // 사이클 발견
                    else if (visit[_r][_c]) return INF;
                    visit[_r][_c] = true;

                    bool curEnd = true;
                    int jump = board[_r][_c];

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = _r + dirR[i] * jump;
                        int nC = _c + dirC[i] * jump;

                        if (ChkInvalidPos(nR, nC)) continue;
                        dp[_r][_c] = Math.Max(DFS(nR, nC), dp[_r][_c]);
                        curEnd &= isEnd[nR][nC];
                    }

                    isEnd[_r][_c] = curEnd;
                    dp[_r][_c]++;
                    return dp[_r][_c];
                }

                bool ChkInvalidPos(int _r, int _c) => _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                dp = new int[row][];
                isEnd = new bool[row][];
                visit = new bool[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    dp[r] = new int[col];
                    isEnd[r] = new bool[col];
                    visit[r] = new bool[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == 'H')
                        {

                            board[r][c] = -1;
                            isEnd[r][c] = true;
                        }
                        else board[r][c] = cur - '0';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();

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
    }
#if other
// #include <cstdio>

int R, C, ANS;
int dy[] = {0, 1, 0, -1};
int dx[] = {1, 0, -1, 0};
int LIMIT;
int MAX_CNT[50][50]; // BOARD와 크기가 같은 배열 : prunning 용
char BOARD[50][51];

void dfs(int cy, int cx, int cnt){
    if (ANS < cnt) ANS = cnt;
    if (ANS > LIMIT) return;
    if (!(0 <= cy && cy < R && 0 <= cx && cx < C)) return;
    if (BOARD[cy][cx] == -1) return;
    if (cnt <= MAX_CNT[cy][cx]) return;

    MAX_CNT[cy][cx] = cnt;
    int mul = BOARD[cy][cx];

    for (int i = 0; i < 4; i++){
        dfs(cy + dy[i] * mul, cx + dx[i] * mul, cnt + 1);
    }
}

int main(){

    scanf("%d%d", &R, &C);
    LIMIT = R*C;

    // init MAX_CNT
    for (int i = 0; i < R; i++){
        for (int j = 0; i < C; i++){
            MAX_CNT[i][j] = -1;
        }
    }

    // init BOARD
    for (int i = 0; i < R; i++){
        scanf("%s", BOARD[i]);
        for (int j = 0; j < C; j++){
            if (BOARD[i][j] == 'H') BOARD[i][j] = -1;
            else BOARD[i][j] = BOARD[i][j] - '0';
        }
    }

    dfs(0, 0 ,0);
    if (ANS > LIMIT) ANS = -1;
    printf("%d", ANS);
}
#endif
}
