using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 19
이름 : 배성훈
내용 : 비숍 2
    문제번호 : 2570번

    이분 매칭 문제다
    대각선으로 영역을 나누어 매칭하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0707
    {

        static void Main707(string[] args)
        {

            StreamReader sr;

            int size;
            int[][][] board;

            List<int>[] line;
            int[] match;
            bool[] visit;

            int len1, len2;

            Solve();

            void Solve()
            {

                Input();

                SetArea();

                LinkLine();

                int ret = 0;

                for (int i = 1; i <= len1; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                Console.WriteLine(ret);
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                return false;
            }

            void LinkLine()
            {

                line = new List<int>[len1 + 1];
                for (int i = 1; i <= len1; i++)
                {

                    line[i] = new();
                }

                for (int r = 0; r < size; r++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        if (board[r][c][0] == 1) continue;
                        line[board[r][c][1]].Add(board[r][c][2]);
                    }
                }

                match = new int[len2 + 1];
                visit = new bool[len2 + 1];
            }

            void SetArea()
            {

                len1 = 0;
                for (int sum = 0; sum < size; sum++)
                {

                    for (int r = sum; r >= 0; r--)
                    {

                        int c = sum - r;
                        if (board[r][c][0] == 1) continue;
                        board[r][c][1] = ++len1;

                        for (int i = r - 1; i >= 0; i--, r--)
                        {

                            int j = sum - i;
                            if (board[i][j][0] == 1) break;
                            board[i][j][1] = len1;
                        }
                    }
                }

                for (int sum = size; sum < 2 * size - 1; sum++)
                {

                    int n = 2 * size - 1 - sum;
                    for (int c = size - n; c < size; c++)
                    {

                        int r = sum - c;
                        if (board[r][c][0] == 1) continue;
                        board[r][c][1] = ++len1;

                        for (int j = c + 1; j < size; j++, c++)
                        {

                            int i = sum - j;
                            if (board[i][j][0] == 1) break;
                            board[i][j][1] = len1;
                        }
                    }
                }

                len2 = 0;

                for (int sub = 0; sub < size; sub++)
                {

                    for (int r = 0; r < size; r++)
                    {

                        int c = r + sub;
                        if (size <= c) break;
                        if (board[r][c][0] == 1) continue;

                        board[r][c][2] = ++len2;

                        for (int i = r + 1; i < size; i++, r++)
                        {

                            int j = i + sub;
                            if (size <= j || board[i][j][0] == 1) break;
                            board[i][j][2] = len2;
                        }
                    }
                }

                for (int sub = 1; sub < size; sub++)
                {

                    for (int c = 0; c < size; c++)
                    {

                        int r = c + sub;
                        if (size <= r) break;
                        if (board[r][c][0] == 1) continue;

                        board[r][c][2] = ++len2;

                        for (int j = c + 1; j < size; j++, c++)
                        {

                            int i = j + sub;
                            if (size <= i || board[i][j][0] == 1) break;
                            board[i][j][2] = len2;
                        }
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = ReadInt();

                board = new int[size][][];
                for (int r = 0; r < size; r++)
                {

                    board[r] = new int[size][];
                    for (int c = 0; c < size; c++)
                    {

                        board[r][c] = new int[3];
                    }
                }

                int len = ReadInt();
                for (int i = 0; i < len; i++)
                {

                    int r = ReadInt() - 1;
                    int c = ReadInt() - 1;
                    board[r][c][0] = 1;
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
import java.util.ArrayList;
import java.util.Arrays;

public class Main {
	public static ArrayList<Integer>[] g;
	public static boolean[] visit;
	public static int[] match;
	public static void main(String[] args) throws Exception {
		int N = readInt();
		int M = readInt();
		int[][] map = new int[N][N];
		int[][] vmap = new int[N][N];
		for (int i = 0; i < M; i++) {
			map[readInt() - 1][readInt() - 1] = 1;
		}
		int x = 0;
		int y = 0;
		int cnt = 1;
		boolean flag = false;
		for (int z = 1; z < N+N; z++) {
			int dx = x;
			int dy = y;	
			while(0 <= dx && dx < N && 0 <= dy && dy < N) {
				if(map[dx][dy] == 0) {
					vmap[dx][dy] = cnt;
					flag = true;
				} else if(flag){
					cnt++;
					flag = false;
				}
				dx--;
				dy++;
			}

			if(x == N - 1) {
				y++;
			} else {
				x++;
			}
			if(flag) {
				cnt++;
				flag = false;
			}
		}
		g = new ArrayList[cnt];
		visit = new boolean[cnt];
		for (int i = 0; i < cnt; i++) {
			g[i] = new ArrayList<>();
		}
		x = N-1;
		y = 0;
		cnt = 1;
		for (int z = 1; z < N+N; z++) {
			int dx = x;
			int dy = y;
			
			while(0 <= dx && dx < N && 0 <= dy && dy < N) {
				if(map[dx][dy] == 0) {
					g[vmap[dx][dy]].add(cnt);
					flag = true;
				} else if(flag){
					cnt++;
					flag = false;
				}
				dx++;
				dy++;
			}

			if(x == 0) {
				y++;
			} else {
				x--;
			}
			if(flag) {
				cnt++;
				flag = false;
			}
		}
		match = new int[cnt];
		int result = 0;
		for (int i = 1; i < visit.length; i++) {
			Arrays.fill(visit, false);
			if(dfs(i))
				result++;
		}
		System.out.println(result);
	}
	public static boolean dfs(int v) {
		visit[v] = true;
		for (int i = 0; i < g[v].size(); i++) {
			int next = g[v].get(i);
			if(match[next] == 0 || (!visit[match[next]] && dfs(match[next]))) {
				match[next] = v;
				return true;
			}
		}
		return false;
	}

	public static int readInt() throws Exception {
		int val = 0;
		int c = System.in.read();
		while (c <= ' ') {
			c = System.in.read();
		}
		boolean flag = (c == '-');
		if (flag)
			c = System.in.read();
		do {
			val = 10 * val + c - 48;
		} while ((c = System.in.read()) >= 48 && c <= 57);

		if (flag)
			return -val;
		return val;
	}
}
#elif other2
import sys
input = sys.stdin.readline

def DFS(nx):
    for mx in G[nx]:
        if not check[mx]:
            check[mx] = 1
            if V[mx] == -1 or DFS(V[mx]):
                V[mx] = nx
                return True
    return False

N = int(input())
S = [[0] * N for _ in range(N)]
V = [[0] * N for _ in range(N)]
for _ in range(int(input())):
    a, b = map(int, input().split())
    S[a - 1][b - 1] = 'X'
x = 0
for i in range(N):
    for j in range(N):
        if V[i][j] == 0 and S[i][j] != 'X':
            nx, ny = i, j
            while True:
                V[nx][ny] = 1
                S[nx][ny] = x
                nx += 1
                ny += 1
                if nx == N or ny == N or S[nx][ny] == 'X':
                    break
            x += 1
G = [[] for _ in range(x)]
y = 0
for i in range(N):
    for j in range(N):
        if V[i][j] == 1 and S[i][j] != 'X':
            nx, ny = i, j
            while True:
                V[nx][ny] = 2
                G[S[nx][ny]].append(y)
                nx += 1
                ny -= 1
                if nx == N or ny == -1 or S[nx][ny] == 'X':
                    break
            y += 1
V = [-1] * y
ans = 0
for i in range(x):
    check = [0] * y
    if DFS(i):
        ans += 1
print(ans)
#elif other3
// #include <cstdio>
// #include <vector>
using namespace std;

// #define MAX 101
// #define MAXN 5010

int map[MAX][MAX];
int N, M;
vector<int> adj[MAXN];
vector<int> left(MAXN, -1);
vector<int> right(MAXN, -1);
vector<bool> visit(MAXN, false);
int inc, dec;

void make_adj() {
	int y, x, pre = -1;
	for (int i = 1; i < 2 * N; i++) {
		y = i; x = 1;
		if (y >= N) {
			x += (y - N);
			y = N;
		}
		while (0 < y && y <= N && 0 < x && x <= N) {
			if (map[y][x] != -1) {
				if (pre == -1) inc++;
				map[y][x] = inc;
			}
			pre = map[y][x];
			x++; y--;
		}
		pre = -1;
	}

	for (int i = 1; i < 2 * N; i++) {
		y = N; x = i;
		if (x >= N) {
			y -= (x - N);
			x = N;
		}
		while (0 < y && y <= N && 0 < x && x <= N) {
			if (map[y][x] != -1) {
				if (pre == -1) dec++;
				adj[map[y][x]].push_back(dec);
			}
			pre = map[y][x];
			x--; y--;
		}
		pre = -1;
	}
}
bool dfs(int now) {
	visit[now] = true;
	for (int next : adj[now]) {
		if (right[next] == -1 || (!visit[right[next]] && dfs(right[next]))) {
			left[now] = next;
			right[next] = now;
			return true;
		}
	}
	return false;
}
int main() {

//	freopen("input.txt", "r", stdin);

	scanf("%d %d", &N, &M);
	int t1, t2;
	for (int i = 0; i < M; i++) {
		scanf("%d %d", &t1, &t2);
		map[t1][t2] = -1;
	}

	make_adj();

	int match = 0;
	for (int i = 1; i <= inc; i++) {
		fill(visit.begin(), visit.end(), false);
		if (dfs(i)) match++;
	}

	printf("%d", match);
	return 0;
}
#endif
}
