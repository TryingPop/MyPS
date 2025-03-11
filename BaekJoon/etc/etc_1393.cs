using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 11
이름 : 배성훈
내용 : 창영이와 퇴근
    문제번호 : 22116번

    다익스트라 문제다.
    높이차가 최소가 되게 목적지까지 이동할 때 값을 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1393
    {

        static void Main1393(string[] args)
        {

            
            int n;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                int INF = 1_000_000_001;
                int[] dis = new int[n * n];
                Array.Fill(dis, INF);
                dis[0] = 0;
                PriorityQueue<int, int> pq = new(n * n);

                for (int i = 2; i < 4; i++)
                {

                    int nR = dirR[i];
                    int nC = dirC[i];

                    if (ChkInvalidPos(nR, nC)) continue;

                    int next = GetIdx(nR, nC);
                    dis[next] = Math.Abs(board[nR][nC] - board[0][0]);
                    pq.Enqueue(next, dis[next]);
                }

                while (pq.Count > 0)
                {

                    int idx = pq.Dequeue();

                    int curR = GetR(idx);
                    int curC = GetC(idx);
                    int curHeight = board[curR][curC];
                    int curDis = dis[idx];

                    for (int i = 0; i < 4; i++)
                    {


                        int nR = dirR[i] + curR;
                        int nC = dirC[i] + curC;

                        if (ChkInvalidPos(nR, nC)) continue;
                        int next = GetIdx(nR, nC);
                        int chkDis = Math.Max(Math.Abs(curHeight - board[nR][nC]), curDis);
                        if (dis[next] <= chkDis) continue;
                        dis[next] = chkDis;
                        pq.Enqueue(next, chkDis);
                    }
                }

                Console.Write(dis[GetIdx(n - 1, n - 1)]);



                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _r >= n || _c < 0 || _c >= n;

                int GetIdx(int _r, int _c)
                    => _r * n + _c;

                int GetR(int _idx)
                    => _idx / n;

                int GetC(int _idx)
                    => _idx % n;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                board = new int[n][];

                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
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
// #include <bits/stdc++.h>
// #define ll long long

using namespace std;

class MoveNode {
    public:
        int cost, x, y;

        MoveNode(int _c, int _x, int _y) {
            cost = _c;
            x = _x;
            y = _y;
        }

        bool operator < (MoveNode other) const {
            return other.cost < cost;
        }
};

bool visited[1000][1000];
int board[1000][1000], n;
int dx[] = {0, 0, 1, -1};
int dy[] = {1, -1, 0, 0};
priority_queue<MoveNode> q;
MoveNode cur = MoveNode(0, 0, 0);

int main() {
    cin.tie(nullptr);
    cout.tie(nullptr);
    ios_base::sync_with_stdio(false);

    cin >> n;
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            cin >> board[i][j];
        }
    }

    q.push(cur);
    while (cur.x != n - 1 or cur.y != n - 1) {
        cur = q.top();
        q.pop();

        if (visited[cur.x][cur.y]) continue;
        visited[cur.x][cur.y] = true;

        for (int i = 0; i < 4; i++) {
            if (not (0 <= cur.x + dx[i] and cur.x + dx[i] < n and 0 <= cur.y + dy[i] and cur.y + dy[i] < n)) continue;
            if (visited[cur.x + dx[i]][cur.y + dy[i]]) continue;

            q.push(MoveNode(max(cur.cost, abs(board[cur.x][cur.y] - board[cur.x + dx[i]][cur.y + dy[i]])), cur.x + dx[i], cur.y + dy[i]));
        }
    }

    cout << cur.cost;

    return 0;
}
#endif
}
