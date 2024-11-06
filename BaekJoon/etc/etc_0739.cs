using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 29
이름 : 배성훈
내용 : 도미노
    문제번호 : 1824번

    이분 매칭 문제다
    우선 정답은 항상 존재하므로 적합성 판정할 필요가 없다!
    호프크로프트 카프 알고리즘으로 풀었다

    맞춘 사람들 풀이를 살펴보니 디닉처럼 따로 확인한 인덱스 배열을 부여해야한다!
*/

namespace BaekJoon.etc
{
    internal class etc_0739
    {

        static void Main739(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int row, col;
            int[][] board;
            bool[] skip, visit;
            int[] match, d, lvl;
            List<int>[] line;
            Queue<int> q;

            int len;
            Solve();

            void Solve()
            {

                Input();

                Match();

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= len; i++)
                {

                    if (skip[i]) continue;
                    sw.Write($"{i} {match[i]}\n");
                }

                sw.Close();
            }

            void Match()
            {

                q = new(len);

                visit = new bool[len + 1];
                lvl = new int[len + 1];
                match = new int[len + 1];
                d = new int[len + 1];
                lvl[0] = -1;

                while (true)
                {

                    Array.Fill(d, 0);
                    BFS();

                    int match = 0;
                    lvl[0] = -1;
                    for (int i = 1; i <= len; i++)
                    {

                        if (!skip[i] && !visit[i] && DFS(i)) match++;
                    }

                    if (match == 0) break;
                }
            }

            void BFS()
            {


                for (int i = 1; i <= len; i++)
                {

                    if (skip[i]) continue;
                    if (visit[i]) lvl[i] = 0;
                    else
                    {

                        lvl[i] = 1;
                        q.Enqueue(i);
                    }
                }

                while (q.Count > 0)
                {

                    int a = q.Dequeue();

                    for (int i = 0; i < line[a].Count; i++)
                    {

                        int b = line[a][i];
                        if (lvl[match[b]] == 0)
                        {

                            lvl[match[b]] = lvl[a] + 1;
                            q.Enqueue(match[b]);
                        }
                    }
                }
            }

            bool DFS(int _a)
            {

                for (; d[_a] < line[_a].Count; d[_a]++)
                {

                    int b = line[_a][d[_a]];
                    if (match[b] == 0 || (lvl[match[b]] == lvl[_a] + 1 && DFS(match[b])))
                    {

                        visit[_a] = true;
                        match[_a] = b;
                        match[b] = _a;

                        return true;
                    }
                }

                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                int[][] board = new int[row][];
                len = 0;
                skip = new bool[row * col + 1];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ++len;
                        if (((r + c) & 1) == 1) skip[len] = true;
                    }
                }

                int m = ReadInt();
                HashSet<(int f, int b)> block = new(m);
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    if (skip[f])
                    {

                        int temp = f;
                        f = b;
                        b = temp;
                    }

                    block.Add((f, b));
                }

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                line = new List<int>[len + 1];

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = board[r][c];
                        if (skip[cur]) continue;
                        line[cur] = new();

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = r + dirR[i];
                            int nextC = c + dirC[i];

                            if (ChkInvalidPos(nextR, nextC)) continue;

                            int next = board[nextR][nextC];
                            if (block.Contains((cur, next))) continue;
                            line[cur].Add(next);
                        }
                    }
                }

                sr.Close();
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
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
// #include <cstring>
// #include <queue>
// #include <set>
using namespace std;

// #define N 10001
int board[102][102];
vector<int> adj[N];
int dx[4] = { -1, 1, 0, 0 }, dy[4] = { 0, 0, -1, 1 }, A[N], B[N], level[N], idx[N];
set<int> s[N];
bool dfs(int now){
	while (idx[now] < (int)adj[now].size()){
		int next = adj[now][idx[now]];
		if (!B[next]){
			A[now] = next, B[next] = now;
			return true;
		}
		else if (level[B[next]] == level[now] + 1 && dfs(B[next])){
			A[now] = next, B[next] = now;
			return true;
		}
		idx[now]++;
	}
	return false;
}
int main(void){
	cin.tie(0);
	ios::sync_with_stdio(0);

	int m, n, q, x = 1, y; cin >> m >> n;
	for (int i = 1; i <= m; i++)
	for (int j = 1; j <= n; j++)
		board[i][j] = x++;

	cin >> q;
	while (q--){
		cin >> x >> y;
		s[x].insert(y);
		s[y].insert(x);
	}

	for (int i = 1; i <= m; i++)
	for (int j = 1; j <= n; j++)
	if ((i + j) % 2)
	for (int k = 0; k < 4; k++){
		int x = board[i][j];
		int y = board[i + dx[k]][j + dy[k]];
		if (y && s[x].find(y) == s[x].end())
			adj[x].push_back(y);
	}

	while (1){
		memset(idx, 0, sizeof(idx));
		memset(level, -1, sizeof(level));
		queue<int> q;

		for (int i = 1; i <= m; i++)
		for (int j = 1; j <= n; j++)
		if ((i + j) % 2 && !A[board[i][j]])
			level[board[i][j]] = 0, q.push(board[i][j]);

		while (!q.empty()){
			int now = q.front(); q.pop();
			for (int next : adj[now])
			if (B[next] && level[B[next]] == -1)
				level[B[next]] = level[now] + 1, q.push(B[next]);
		}

		int temp = 0;
		for (int i = 1; i <= m; i++)
		for (int j = 1; j <= n; j++)
		if ((i + j) % 2 && !A[board[i][j]])
			temp += dfs(board[i][j]);
		if (!temp) break;
	}

	for (int i = 1; i <= m; i++)
	for (int j = 1; j <= n; j++)
	if ((i + j) % 2 && A[board[i][j]])
		cout << board[i][j] << ' ' << A[board[i][j]] << '\n';

	return 0;
}
#endif
}
