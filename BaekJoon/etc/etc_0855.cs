using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 31
이름 : 배성훈
내용 : 거울
    문제번호 : 2344번

    구현, 시뮬레이션 문제다
    빛이 나아가는 방향을 일일히 시뮬레이션 돌렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0855
    {

        static void Main855(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Queue<(int r, int c)> q;

            int[][] board;
            int row, col;

            int[] dirR, dirC;
            int[] ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 1; i < ret.Length; i++)
                {

                    sw.Write($"{ret[i]} ");
                }

                sw.Close();
            }

            void GetRet()
            {

                q = new(row + col);

                for (int i = 1; i <= row; i++)
                {

                    int f = board[i][0];
                    int t = BFS(i, 0, 0);
                    ret[f] = t;
                    ret[t] = f;
                }

                for (int i = 1; i <= col; i++)
                {

                    int f = board[row + 1][i];
                    int t = BFS(row + 1, i, 1);
                    ret[f] = t;
                    ret[t] = f;
                }
            }

            int BFS(int _r, int _c, int _dir)
            {

                q.Clear();
                q.Enqueue((_r + dirR[_dir], _c + dirC[_dir]));

                while(q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();

                    int cur = board[node.r][node.c];
                    if (cur > 0) return cur;
                    else if (cur == -1) _dir = _dir == 1 ? 0 : 1;

                    int nextR = node.r + dirR[_dir];
                    int nextC = node.c + dirC[_dir];

                    q.Enqueue((nextR, nextC));
                }

                return 0;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row + 2][];
                for (int r = 0; r < board.Length; r++)
                {

                    board[r] = new int[col + 2];
                }

                int cur = 1;
                for (int i = 1; i <= row; i++)
                {

                    board[i][0] = cur++;
                }

                for (int i = 1; i <= col; i++)
                {

                    board[row + 1][i] = cur++;
                }

                for (int i = row; i >= 1; i--)
                {

                    board[i][col + 1] = cur++;
                }

                for (int i = col; i >= 1; i--)
                {

                    board[0][i] = cur++;
                }

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        board[r][c] = -ReadInt();
                    }
                }

                ret = new int[(row + col) * 2 + 1];

                dirR = new int[2] { 0, -1 };
                dirC = new int[2] { 1, 0 };
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
// #include <iostream>
// #include <vector>
// #include <stack>
// #include <queue>
// #include <cmath>
// #include <algorithm>
// #include <string>
// #include <utility>
using namespace std;

int r[1001][1001];
int ans[4001];
int main() {
    ios::sync_with_stdio(false); cin.tie(NULL);
    int n, m; cin >> n >> m;
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < m; j++) {
            cin >> r[i][j];
        }
    }
    for (int i = 0; i < n; i++) {
        bool dirx = true;
        int curx=0, cury = i;
        while (curx != m && cury != -1) {
            if (r[cury][curx]) dirx = !dirx;
            if (dirx) curx++;
            else cury--;
        }
        if (dirx) {
            ans[i + 1] = 2 * n + m - cury;
            ans[2 * n + m - cury] = i + 1;
        }
        else {
            ans[i + 1] = 2 * n + 2 * m - curx;
            ans[2 * n + 2 * m - curx] = i + 1;
        }
    }
    for (int i = 0; i < m; i++) {
        bool dirx = false;
        int curx = i, cury = n - 1;
        while (curx != m && cury != -1) {
            if (r[cury][curx]) dirx = !dirx;
            if (dirx) curx++;
            else cury--;
        }
        if (dirx) {
            ans[i + n + 1] = 2 * n + m - cury;
            ans[2 * n + m - cury] = i + n + 1;
        }
        else {
            ans[i + n + 1] = 2 * n + 2 * m - curx;
            ans[2 * n + 2 * m - curx] = i + n + 1;
        }
    }
    for (int i = 1; i <= 2 * n + 2 * m; i++) {
        cout << ans[i] << " ";
    }
    cout << "\n";

    return 0;
}

#endif
}
