using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 18
이름 : 배성훈
내용 : 전투 시뮬레이션
    문제번호 : 19537번

    시뮬레이션, 다익스트라 문제다.
    다익스트라로 시뮬레이션 돌리는 경우
    각 경우 800개의 노드를 탐색한다.
    그리고 각 케이스는 많아야 1만개이다.
    그래서 시뮬레이션 돌려도 통과할만하다 판단했다.

    처음엔 Array.Fill로 모든 노드를 INF로 초기화했는데
    1.936초로 정말 아슬아슬하게 통과했다.
    이후 방문한 노드만 초기화하니 1.5초로 넉넉하게 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1827
    {

        static void Main1827(string[] args)
        {

            // 19537 - 전투 시뮬레이션
            int INF = 123;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m, row, col;
            int[][] cost, board, dis;
            (int m, int t, int r, int c)[] units;

            PriorityQueue<(int r, int c), int> pq;
            Queue<(int r, int c)> q;
            int[] dirR, dirC;

            Input();

            int t = ReadInt();

            while (t-- > 0)
            {

                Query(ReadInt() - 1, ReadInt(), ReadInt());
            }

            for (int i = 0; i < m; i++)
            {

                sw.Write($"{units[i].r} {units[i].c}\n");
            }

            void Query(int _u, int _dstR, int _dstC)
            {

                (int max, int t, int r, int c) = units[_u];

                if (cost[_dstR][_dstC] == -1 || board[_dstR][_dstC] > 0 || Dijkstra()) return;

                // 이동 가능하니 이동 위치 옮기기
                board[r][c] = 0;
                board[_dstR][_dstC] = t;
                units[_u].r = _dstR;
                units[_u].c = _dstC;

                bool Dijkstra()
                {

                    dis[r][c] = 0;
                    pq.Enqueue((r, c), 0);

                    while (pq.Count > 0)
                    {

                        var node = pq.Dequeue();
                        if (dis[node.r][node.c] > max) break;
                        q.Enqueue((node.r, node.c));

                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];
                            if (ChkImpo(nR, nC) || dis[nR][nC] != INF) continue;

                            int chk = dis[node.r][node.c] + cost[nR][nC];
                            if (chk > max) continue;
                            dis[nR][nC] = chk;
                            pq.Enqueue((nR, nC), dis[nR][nC]);
                        }
                    }

                    pq.Clear();
                    int ret = dis[_dstR][_dstC];

                    while (q.Count > 0)
                    {

                        var node = q.Dequeue();
                        dis[node.r][node.c] = INF;
                    }

                    return ret > max;

                    bool ChkImpo(int _r, int _c)
                    {

                        if (_r == _dstR && _c == _dstC) return false;
                        else if (ChkInvalidPos(_r, _c)  // 장외
                            || cost[_r][_c] == -1       // 못지나가는 곳
                            || board[_r][_c] + t == 3)  // 적이 있는 곳
                            return true;

                        // 4방향에 적이 있는지 확인
                        for (int i = 0; i < 4; i++)
                        {

                            int nR = _r + dirR[i];
                            int nC = _c + dirC[i];
                            if (ChkInvalidPos(nR, nC)) continue;
                            // 교전으로 못지나가는 곳
                            else if (board[nR][nC] + t == 3) return true;
                        }

                        return false;
                    }
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _r >= row || _c < 0 || _c >= col;
            }

            void Input()
            {

                n = ReadInt();
                row = ReadInt();
                col = ReadInt();

                cost = new int[row][];
                board = new int[row][];
                dis = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    cost[r] = new int[col];
                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        cost[r][c] = ReadInt() - 1;
                    }

                    dis[r] = new int[col];
                    Array.Fill(dis[r], INF);
                }

                int[] stamina = new int[n];
                for (int i = 0; i < n; i++)
                {

                    stamina[i] = ReadInt();
                }

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        cost[r][c] = stamina[cost[r][c]];
                    }
                }

                m = ReadInt();
                units = new (int m, int t, int r, int c)[m];
                for (int i = 0; i < m; i++)
                {

                    units[i] = (ReadInt(), ReadInt() + 1, ReadInt(), ReadInt());
                    board[units[i].r][units[i].c] = units[i].t;
                }
                
                pq = new(4_000);
                q = new(1_600);
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
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
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <functional>
// #include <algorithm>
// #include <string>
// #include <vector>
// #include <queue>
// #include <stack>
// #include <cmath>
// #include <set>
// #include <map>
// #include <list>
typedef long long ll;
typedef std::pair<int, int> pii;
typedef std::pair<int, pii> piii;
typedef std::pair<int, char> pic;

int n, h, w, m, k;
int land[501][501];
int r[10];
int mat[501][501];
int dist[501][501];

int dx[] = {-1, 1, 0, 0};
int dy[] = {0, 0, -1, 1};

struct Unit {
	int m, t, a, b;
};
Unit units[100000];

bool inMap(int i, int j) {
	return 0 <= i && i < h && 0 <= j && j < w;
}

bool onFight(int t, int a, int b) {
	for (int k = 0; k < 4; ++k) {
		int ni = a + dx[k];
		int nj = b + dy[k];
		if (!inMap(ni, nj)) continue;
		if (mat[ni][nj] && mat[ni][nj] != t) return true;
	}
	return false;
}

bool canMove(int u, int a, int b) {
	if (std::abs(a - units[u].a) + std::abs(b - units[u].b) > units[u].m) return false;
	std::priority_queue<piii> pq;
	for (int i = std::max(0, units[u].a - units[u].m); i <= std::min(h - 1, units[u].a + units[u].m); ++i) {
		for (int j = std::max(0, units[u].b - units[u].m); j <= std::min(w - 1, units[u].b + units[u].m); ++j) {
			dist[i][j] = 987654321;
		}
	}
	pq.push({0, {units[u].a, units[u].b}});
	for (; !pq.empty();) {
		auto t = pq.top();
		pq.pop();
		if (dist[t.second.first][t.second.second] < t.first) continue;

		for (int i = 0; i < 4; ++i) {
			int ni = t.second.first + dx[i];
			int nj = t.second.second + dy[i];
			if (!inMap(ni, nj)) continue;
			if (mat[ni][nj] && mat[ni][nj] != units[u].t) continue;
			if (r[land[ni][nj]] == -1) continue;
			if (!(ni == a && nj == b) && onFight(units[u].t, ni, nj)) {
				continue;
			}
			int nd = t.first + r[land[ni][nj]];
			if (nd > units[u].m) continue;
			if (std::abs(ni - a) + std::abs(nj - b) > units[u].m - nd) continue;
			if (dist[ni][nj] > nd) {
				dist[ni][nj] = nd;
				pq.push({nd, {ni, nj}});
			}
		}
	}
	return dist[a][b] <= units[u].m;
}

void move(int u, int a, int b) {
	mat[units[u].a][units[u].b] = 0;
	mat[a][b] = units[u].t;
	units[u].a = a;
	units[u].b = b;
}

int main() {
	std::ios_base::sync_with_stdio(0);
	std::cin.tie(0);
	std::cout.tie(0);

	std::cin >> n >> h >> w;
	for (int i = 0; i < h; ++i) {
		for (int j = 0; j < w; ++j) {
			std::cin >> land[i][j];
		}
	}
	for (int i = 0; i < n; ++i) {
		std::cin >> r[i + 1];
	}

	std::cin >> m;
	for (int i = 0; i < m; ++i) {
		Unit u;
		std::cin >> u.m >> u.t >> u.a >> u.b;
		++u.t;
		mat[u.a][u.b] = u.t;
		units[i + 1] = u;
	}

	std::cin >> k;
	for (int i = 0; i < k; ++i) {
		int u, a, b;
		std::cin >> u >> a >> b;
		if (mat[a][b]) continue;
		if (r[land[a][b]] == -1) continue;
		if (!canMove(u, a, b)) continue;
		move(u, a, b);
	}

	for (int i = 0; i < m; ++i) {
		std::cout << units[i + 1].a << " " << units[i + 1].b << "\n";
	}
    return 0;
}
#endif
}
