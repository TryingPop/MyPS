using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 13
이름 : 배성훈
내용 : 문명
    문제번호 : 14868번

    분리집합, BFS 문제다
    현재 상태에서 인접한거 확인 -> 확장하면서 인접하는지 확인
    하는 방식으로 BFS를 2번씩 진행했다
    그리고 모두 이어지면 BFS 탐색을 종료했다
*/

namespace BaekJoon.etc
{
    internal class etc_0967
    {

        static void Main967(string[] args)
        {

            StreamReader sr;
            int n, k;
            int[][] map;
            bool[][] visit;

            int[] dirR, dirC;
            int[] group, s;
            int g;
            Queue<(int r, int c)>[] q;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
                int ret = 0;

                while (true)
                {

                    while (q[0].Count > 0)
                    {

                        (int r, int c) node = q[0].Dequeue();
                        q[1].Enqueue(node);
                        int cur = Find(map[node.r][node.c]);

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = node.r + dirR[i];
                            int nextC = node.c + dirC[i];

                            if (ChkInvalidPos(nextR, nextC)) continue;

                            if (visit[nextR][nextC])
                            {

                                int next = Find(map[nextR][nextC]);
                                cur = Find(cur);
                                if (next == cur) continue;
                                Union(next, cur);
                            }
                        }
                    }

                    if (g == 0) break;
                    ret++;

                    SwapQ();

                    while (q[0].Count > 0)
                    {

                        (int r, int c) node = q[0].Dequeue();
                        int cur = map[node.r][node.c];

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = node.r + dirR[i];
                            int nextC = node.c + dirC[i];

                            if (ChkInvalidPos(nextR, nextC)) continue;
                            
                            if (visit[nextR][nextC])
                            {

                                int next = Find(map[nextR][nextC]);
                                cur = Find(cur);
                                if (next == cur) continue;
                                Union(next, cur);
                            }
                            else
                            {

                                visit[nextR][nextC] = true;
                                map[nextR][nextC] = cur;
                                q[1].Enqueue((nextR, nextC));
                            }
                        }
                    }

                    SwapQ();
                }

                Console.Write(ret);

                void SwapQ()
                {

                    var temp = q[0];
                    q[0] = q[1];
                    q[1] = temp;
                }

                bool ChkInvalidPos(int _r, int _c)
                {

                    return _r < 0 || _c < 0 || _r >= n || _c >= n;
                }
            }

            void Union(int _f, int _b)
            {

                if (_f < _b)
                {

                    int temp = _f;
                    _f = _b;
                    _b = temp;
                }

                group[_f] = _b;
                g--;
            }

            int Find(int _chk)
            {

                int len = 0;
                while (group[_chk] != _chk)
                {

                    s[len++] = _chk;
                    _chk = group[_chk];
                }

                while(len > 0)
                {

                    group[s[--len]] = _chk;
                }

                return _chk;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                g = k - 1;
                map = new int[n][];
                visit = new bool[n][];
                for (int i = 0; i < n; i++)
                {

                    map[i] = new int[n];
                    visit[i] = new bool[n];
                }

                group = new int[k + 1];
                s = new int[k];

                q = new Queue<(int r, int c)>[2];
                q[0] = new(n * n);
                q[1] = new(n * n);

                for (int i = 1; i <= k; i++)
                {

                    int r = ReadInt() - 1;
                    int c = ReadInt() - 1;

                    map[r][c] = i;
                    visit[r][c] = true;
                    q[0].Enqueue((r, c));
                }

                for (int i = 1; i <= k; i++)
                {

                    group[i] = i;
                }

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
// #include <bits/stdc++.h>
using namespace std;
// #define fastio cin.tie(0)->ios::sync_with_stdio(0); cout.tie(0); setvbuf(stdout, nullptr, _IOFBF, BUFSIZ);
// #pragma warning(disable:4996)

// #include <unordered_set>

const int MAX_IN = 2001 * 2001;
char board[MAX_IN]; // 2250000
int n, m;

bool visitTB[MAX_IN];
queue<int> q_water, q_bfs;

void Print()
{
	std::cout << endl;
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
			cout << char(board[i*n+j] + '0');
		cout << endl;
	}
	cout << endl;
}

void MeltPush(int cur)
{
	if (board[cur] < 1)
	{
		board[cur] += 2;
		q_water.push(cur);
		if (visitTB[cur])
			q_bfs.push(cur);  // 핵심
	}
}

void Melt()
{
	int size = q_water.size();
	while (size--)
	{
		int cur = q_water.front(); q_water.pop();
		int x = cur % n, y = cur / n;
		if (x > 0)      MeltPush(cur - 1);
		if (x + 1 < n)  MeltPush(cur + 1);
		if (y > 0)      MeltPush(cur - n);
		if (y + 1 < n)  MeltPush(cur + n);
	}
}

inline void Push(int cur)
{
	if (visitTB[cur]) return;
	visitTB[cur] = true;

	if (board[cur] < 1) return;
	q_bfs.push(cur);
}

int cnt = 0;
bool BFS()
{
	while (!q_bfs.empty())
	{
		int cur = q_bfs.front(); q_bfs.pop();

		if (board[cur] % 2) { cnt++; board[cur]--; }
		if (cnt == m) 
			return true;

		int x = cur % n; int y = cur / n;
		if (x > 0)     Push(cur - 1);
		if (x + 1 < n) Push(cur + 1);
		if (y > 0)     Push(cur - n);
		if (y + 1 < n) Push(cur + n);
	}

	return false;
}


int main()
{
	fastio;

	cin >> n >> m;;

	for (int i = 0; i < m; i++)
	{
		int x, y;
		cin >> x >> y; x--; y--;
		q_water.push(y * n + x);
		if (q_bfs.empty()) q_bfs.push(y * n + x);
		board[y * n + x] = 1;
	}

	int ans = 0;
	while (!BFS())
	{
		Melt();
		ans++;
		//Print();
	}

	cout << ans;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;

const int LM = 2005;
int n, k, g[LM][LM], gcnt, fr, re, ans, now;
int group[LM * LM];
int dr[] = {-1, 1, 0, 0}, dc[] = {0, 0, -1, 1};

struct Civil {
    int r, c, lev;
} que[LM * LM];

void ffill(int r, int c) {
    if (g[r][c] >= 0) return;
    g[r][c] = gcnt;
    que[re++] = {r, c, 0};
    for (int k = 0; k < 4; k++) {
        ffill(r + dr[k], c + dc[k]);
    }
}

void input() {
    scanf("%d %d", &n, &k);
    int i, j, r, c;
    for (i = 0; i < k; i++) {
        scanf("%d %d", &r, &c);
        g[r][c] = -1;
    }
    for (i = 1; i <= n; i++) {
        for (j = 1; j <= n; j++) {
            if (g[i][j] < 0) {
                gcnt++;
                ffill(i, j);
            }
        }
    }
    for (i = 1; i <= gcnt; i++) group[i] = i;
}

int Find(int k) {
    if (k == group[k]) return k;
    return group[k] = Find(group[k]);
}

int push(int r, int c, int lev) {
    if (r < 1 || r > n || c < 1 || c > n || g[r][c]) return 0;
    g[r][c] = now;
    que[re++] = {r, c, lev};
    for (int k = 0; k < 4; k++) {
        int nr = r + dr[k], nc = c + dc[k];
        if (g[nr][nc] == 0) continue;
        int next = Find(g[nr][nc]);
        if (now == next) continue;
        group[next] = now;
        gcnt--;
    }
    if (gcnt == 1) ans = lev;
    return gcnt == 1;
}

int bfs(){
    if (gcnt == 1) return ans;
    while (fr < re) {
        Civil&t = que[fr++];
        now = Find(g[t.r][t.c]);
        for (int k = 0; k < 4; k++) {
            if (push(t.r + dr[k], t.c + dc[k], t.lev + 1)) {
                return ans;
            }
        }
    }
    return ans;
}

int main() {
    input();
    printf("%d\n", bfs());
    return 0;
}
#endif
}
