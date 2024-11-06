using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 22
이름 : 배성훈
내용 : 로하의 농사
    문제번호 : 26153번

    DFS 백트래킹 문제다
    파이프 설치 가능한 노드를 보면 3^20으로 35억 조금 안된다
    그리고 방향을 꺾는경우 비용이 2배로 발생하므로 
    절반 정도는 지워질거라 예상하고
    백트래킹으로 구현하니 112ms 에 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_0901
    {

        static void Main901(string[] args)
        {

            StreamReader sr;
            (int r, int c) s;
            int row, col;
            int[][] water;
            bool[][] visit;
            int[] dirR, dirC;
            int p;
            Solve();
            void Solve()
            {

                Input();
                visit[s.r][s.c] = true;

                int ret = DFS(p + 1, s.r, s.c, -1);
                Console.Write(ret);
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            int DFS(int _depth, int _r, int _c, int _prev)
            {

                if (_depth < 0) return 0;
                int ret = 0;

                for (int i = 0; i < 4; i++)
                {

                    int nextR = _r + dirR[i];
                    int nextC = _c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC) 
                        || visit[nextR][nextC]) continue;
                    visit[nextR][nextC] = true;
                    int nextDep = _depth - 1;
                    if (_prev != i) nextDep--;

                    ret = Math.Max(ret, DFS(nextDep, nextR, nextC, i));
                    visit[nextR][nextC] = false;
                }

                ret += water[_r][_c];
                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                water = new int[row][];
                visit = new bool[row][];
                for (int r = 0; r < row; r++)
                {

                    water[r] = new int[col];
                    visit[r] = new bool[col];

                    for (int c = 0; c < col; c++)
                    {

                        water[r][c] = ReadInt();
                    }
                }

                s = (ReadInt(), ReadInt());
                p = ReadInt();

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
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
// #define _CRT_SECURE_NO_WARNINGS
// #include <bits/stdc++.h>
using namespace std;

const int MAX_N = 50 + 5;
int water[MAX_N][MAX_N];
int visited[MAX_N][MAX_N];
int dy[] = { 1,0,-1,0 };
int dx[] = { 0,1,0,-1 };

int N, M;
int x, y, p;
int ans;

bool is_ok(int y, int x) {
	if (y < 0 || y >= N || x < 0 || x >= M || visited[y][x]) return false;
	return true;
}

void backtracking(pair<int,int> now, int pipe, int total_water, int dir) {
	if (pipe >= p) {
		if(pipe == p) ans = max(ans, total_water);
		return;
	}
	bool chk = false;
	for (int i = 0; i < 4; i++) {
		pair<int, int> next = { now.first + dy[i], now.second + dx[i] };
		if (is_ok(next.first, next.second)) {
			if (dir == i) {
				visited[next.first][next.second] = true;;
				backtracking(next, pipe + 1, total_water + water[next.first][next.second], i);
				visited[next.first][next.second] = false;
			}
			else {
				visited[next.first][next.second] = true;;
				backtracking(next, pipe + 2, total_water + water[next.first][next.second], i);
				visited[next.first][next.second] = false;
			}
		}
	}
	ans = max(ans, total_water);
}
int main(void) {
	ios_base::sync_with_stdio(false);
	cin.tie(NULL);
	cin >> N >> M;
	for (int i = 0; i < N; i++)
		for (int j = 0; j < M; j++)
			cin >> water[i][j];

	cin >> y >> x >> p;
	visited[y][x] = true;
	ans = water[y][x];
	for (int i = 0; i < 4; i++) {
		pair<int, int> next = { y + dy[i], x + dx[i] };
		if (!is_ok(next.first, next.second)) continue;
		visited[next.first][next.second] = true;
		backtracking(next, 1, water[y][x] + water[next.first][next.second], i);
		visited[next.first][next.second] = false;
	}

	cout << ans << "\n";
	return 0;
}
#endif
}
