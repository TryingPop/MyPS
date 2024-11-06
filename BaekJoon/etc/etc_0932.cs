using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 1
이름 : 배성훈
내용 : 미네랄
    문제번호 : 2933번

    구현, 시뮬레이션 bfs 문제다
    시뮬레이션 돌리면서 찾았다
    내려갈 부분을 찾는데 BFS를 이용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0932
    {

        static void Main932(string[] args)
        {

            StreamReader sr;

            int row, col;
            int[][] board, group;
            bool[][] visit;
            int n, g;
            int[] shot;

            int[] dirR, dirC;
            int[] bot;

            Queue<(int r, int c)> q;

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
                q = new(row * col);

                for (int i = 0; i < n; i++)
                {

                    if (Shot(shot[i], i % 2 == 0)) continue;

                    SetGroup();

                    Gravity();
                }


                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int r = 0; r < row; r++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            if (board[r][c] == 0) sw.Write('.');
                            else sw.Write('x');
                        }

                        sw.Write('\n');
                    }
                }
            }

            void Gravity()
            {

                int b = 0;
                for (int i = 1; i <= g; i++)
                {

                    if (bot[i] == row - 1) continue;
                    b = i;
                    break;
                }

                if (b == 0) 
                { 
                
                    // 내려갈 곳이 없으면 초기화만 한다
                    for (int r = 0; r < row; r++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            group[r][c] = 0;
                            visit[r][c] = false;
                        }
                    }

                    return; 
                }

                int d = 10_000;
                for (int c = 0; c < col; c++)
                {

                    bool flag = true;
                    int down = 0;
                    for (int r = row - 1; r >= 0; r--)
                    {

                        if (group[r][c] == 0) down++;
                        else if (group[r][c] == b) 
                        {

                            flag = false;
                            break; 
                        }
                        else down = 0;
                    }

                    if (flag) continue;
                    d = Math.Min(d, down);
                }

                // 맨 밑에 있는게 row - 1 행이 아니고
                // 그룹이 붕 떠 있는데 밑에 받치는게 존재한다는 말과 같다
                // 이는 존재 불가능!한 경우다!
                // return 해도 상관없다
                if (d == 10_000) return;

                // 초기화하고 내려가는거 갱신
                for (int r = row - 1; r >= 0; r--)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = group[r][c];
                        
                        if (cur != b) board[r][c] = cur == 0 ? 0 : 1;
                        else
                        {

                            board[r][c] = 0;
                            board[r + d][c] = 1;
                        }

                        group[r][c] = 0;
                        visit[r][c] = false;
                    }
                }
            }

            void SetGroup()
            {

                g = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == 0 || visit[r][c]) continue;

                        visit[r][c] = true;
                        q.Enqueue((r, c));

                        GroupBFS(++g); 
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void GroupBFS(int _g)
            {

                bot[_g] = -1;
                while(q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();
                    group[node.r][node.c] = _g;
                    bot[_g] = Math.Max(bot[_g], node.r);

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC]) continue;
                        visit[nextR][nextC] = true;

                        if (board[nextR][nextC] == 0) continue;
                        q.Enqueue((nextR, nextC));
                    }
                }
            }

            bool Shot(int _r, bool _isL)
            {

                int r = row - _r;

                if (_isL)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == 0) continue;
                        board[r][c] = 0;
                        return false;
                    }
                }
                else
                {

                    for (int c = col - 1; c >= 0; c--)
                    {

                        if (board[r][c] == 0) continue;
                        board[r][c] = 0;
                        return false;
                    }
                }

                return true;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                group = new int[row][];
                visit = new bool[row][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    group[r] = new int[col];
                    visit[r] = new bool[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == 'x') board[r][c] = 1;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                n = ReadInt();
                shot = new int[n];
                for (int i = 0; i < n; i++)
                {

                    shot[i] = ReadInt();
                }

                bot = new int[101];
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
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static int[] dy = new int[] { 0, 1, 0, -1 };
        static int[] dx = new int[] { 1, 0, -1, 0 };
        static List<(int, int)> movem = new();
        static bool[,] notmove;
        static bool[,] move;
        static int[,] map;
        static int r;
        static int c;
        static int dis;
        static bool dir = true;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb = new StringBuilder();
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            r = arr[0]; c = arr[1];
            map = new int[r, c];
            for(int i = 0; i < r; i++)
            {
                string s = input.ReadLine();
                for(int j = 0; j < c; j++)
                {
                    if (s[j] == '.')
                        map[i, j] = 0;
                    else
                        map[i, j] = 1;
                }
            }
            int n = int.Parse(input.ReadLine());
            int[] stick = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);            
            for(int i = 0; i < n; i++)
            {                
                notmove = new bool[r, c];
                move = new bool[r, c];
                dis = r;
                throwstick(stick[i]);
                ground();
                checkmovemineral();
                if(movem.Count > 0)
                    movemineral();
            }
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (map[i, j] == 0)
                        sb.Append('.');
                    else
                        sb.Append('x');
                }
                sb.Append('\n');
            }

            output.Write(sb);

            input.Close();
            output.Close();
        }
        static void throwstick(int height)
        {
            int row = r - height;
            if (dir)
            {
                dir = !dir;
                for(int i = 0; i < c; i++)
                {
                    if (map[row,i] == 1)
                    {
                        map[row, i] = 0;
                        return;
                    }
                }
            }
            else
            {
                dir = !dir;
                for (int i = c - 1; i >= 0; i--)
                {
                    if (map[row, i] == 1)
                    {
                        map[row, i] = 0;
                        return;
                    }
                }
            }
        }
        static void ground()//바닥과 연결된 미네랄들
        {
            int end = r - 1;
            for(int i = 0; i < c; i++)
            {
                if (map[end, i] == 0 || notmove[end,i]) continue;
                Queue<(int, int)> q = new();
                q.Enqueue((end, i));
                notmove[end, i] = true;
                while(q.Count > 0)
                {
                    (int row, int col) = q.Dequeue();

                    for(int k = 0; k < 4; k++)
                    {
                        int nr = row + dy[k];
                        int nc = col + dx[k];
                        if (nr < 0 || nr == r || nc < 0 || nc == c || map[nr, nc] == 0 || notmove[nr, nc]) continue;
                        q.Enqueue((nr, nc));
                        notmove[nr, nc] = true;
                    }
                }
            }
        }
        static void checkmovemineral()
        {
            for (int i = r - 2; i >= 0; i--)
            {
                for (int j = 0; j < c; j++)
                {
                    if (map[i, j] == 0 || notmove[i, j] || move[i,j]) continue;
                    Queue<(int, int)> q = new();
                    q.Enqueue((i, j));
                    move[i, j] = true;
                    movem.Add((i, j));
                    while (q.Count > 0)
                    {
                        (int row, int col) = q.Dequeue();

                        dis = Math.Min(dist(row, col), dis);
                        for (int k = 0; k < 4; k++)
                        {
                            int nr = row + dy[k];
                            int nc = col + dx[k];
                            if (nr < 0 || nr == r || nc < 0 || nc == c || map[nr, nc] == 0 || move[nr,nc]) continue;
                            q.Enqueue((nr, nc));
                            move[nr, nc] = true;
                            movem.Add((nr, nc));
                        }
                    }
                }
            }
            movem.Sort((x,y) => y.Item1.CompareTo(x.Item1));
        }
        static void movemineral()
        {
            foreach((int row,int col) in movem)
            {
                int nr = row + dis;

                map[row, col] = 0;
                map[nr, col] = 1;
            }
            movem.Clear();
        }
        static int dist(int cr,int cc)
        {
            int count = 0;
            for(int i = cr + 1; i < r; i++)
            {
                if (notmove[i, cc] && map[i, cc] == 1) break;
                count++;
            }
            return count;
        }
    }
}
#elif other2
// #include <bits/stdc++.h>

using namespace std;

using ii = pair<int, int>;

const int dx[4]{1, -1, 0, 0};
const int dy[4]{0, 0, 1, -1};

int R, C;
vector<string> A;
vector<ii> B;

bool dfs(int x, int y) {
	if (x == R - 1) {
		return false;
	}
	A[x][y] = '.';
	B.push_back({x, y});
	for (int i = 0; i < 4; ++i) {
		int nx = x + dx[i];
		int ny = y + dy[i];
		if (0 <= nx && nx < R && 0 <= ny && ny < C && A[nx][ny] == 'x') {
			if (!dfs(nx, ny)) {
				return false;
			}
		}
	}
	return true;
}

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	cin >> R >> C;
	cin.ignore();
	A.resize(R);
	for (int i = 0; i < R; ++i) {
		getline(cin, A[i]);
	}
	int Q{};
	cin >> Q;
	for (int i = 0; i < Q; ++i) {
		int x{};
		cin >> x;
		x = R - x;
		int y = i % 2 ? C - 1 : 0;
		while (0 <= y && y < C && A[x][y] == '.') {
			y += i % 2 ? -1 : 1;
		}
		if (y == -1 || y == C) {
			continue;
		}
		A[x][y] = '.';
		B.clear();
		for (int j = 0; j < 4; ++j) {
			int nx = x + dx[j];
			int ny = y + dy[j];
			if (0 <= nx && nx < R && 0 <= ny && ny < C && A[nx][ny] == 'x') {
				if (dfs(nx, ny)) {
					break;
				}
				for (auto &[x, y] : B) {
					A[x][y] = 'x';
				}
				B.clear();
			}
		}
		if (B.empty()) {
			continue;
		}
		int p{};
		while (true) {
			bool yes = true;
			for (auto &[x, y] : B) {
				if (x + p == R - 1 || A[x + p + 1][y] == 'x') {
					yes = false;
					break;
				}
			}
			if (!yes) {
				break;
			}
			++p;
		}
		for (auto &[x, y] : B) {
			A[x + p][y] = 'x';
		}
	}
	for (auto &s : A) {
		cout << s << "\n";
	}
	return 0;
}

#elif other3
// #include <iostream>
// #include <cstring>
// #include <vector>

// #define endl	"\n"

using namespace std;

int R, C, Try;
char MAP[101][101];
int Throw[100];
bool visit[101][101];

int drow[] = {-1, 0, 1, 0};
int dcol[] = {0, 1, 0, -1};

void Input(void)
{
	cin >> R >> C;
	for (int i = 1; i <= R; i++) {
		for (int j = 1; j <= C; j++) {
			cin >> MAP[i][j];
		}
	}

	cin >> Try;
	for (int i = 0; i < Try; i++) {
		cin >> Throw[i];
	}
}

void Print(void)
{
	for (int i = R; i >= 1; i--) {
		for (int j = 1; j <= C; j++) {
			cout << MAP[i][j];
		}
		cout << endl;
	}
}

bool find_ground;
vector <pair<int, int>> v;

void DFS(int row, int col)
{
	if (find_ground)
		return;

	v.push_back(make_pair(row, col));
	visit[row][col] = true;
	MAP[row][col] = '.';

	if (row == 1) {
		find_ground = true;
	}

	for (int dir = 0; dir < 4; dir++) {
		int next_row = row + drow[dir];
		int next_col = col + dcol[dir];

		if (next_row < 1 || next_col < 1 || next_row > R || next_col > C)
			continue;

		if (MAP[next_row][next_col] == '.' || visit[next_row][next_col])
			continue;

		DFS(next_row, next_col);
	}
}

void MoveMinerals(void)
{
	bool find = false;

	for (auto mi : v) {
		int row = mi.first - 1;
		int col = mi.second;

		if (row == 0 || MAP[row][col] == 'x') {
			find = true;
			break;
		}
	}

	if (find) {
		return;
	}
	else {
		for (int i = 0; i < v.size(); i++) {
			v[i].first--;
		}
		MoveMinerals();
	}
}

void Check(int row, int col, bool left) {
	find_ground = true;
	if (row < R && MAP[row + 1][col] == 'x') {
		memset(visit, false, sizeof(visit));
		v.clear();
		find_ground = false;
		DFS(row + 1, col);
		if (!find_ground) {
			MoveMinerals();
		}
		//redraw
		for (auto mi : v) {
			MAP[mi.first][mi.second] = 'x';
		}
	}
	
	if (find_ground && left && col < C && MAP[row][col + 1] == 'x') {
		memset(visit, false, sizeof(visit));
		v.clear();
		find_ground = false;
		DFS(row, col+1);
		/*cout << "case2" << endl;
		cout << "find_ground: " << find_ground << endl;*/
		if (!find_ground) {
			MoveMinerals();
		}
		//redraw
		for (auto mi : v) {
			MAP[mi.first][mi.second] = 'x';
		}
	}
	
	if (find_ground && !left && col > 1 && MAP[row][col - 1] == 'x') {
		memset(visit, false, sizeof(visit));
		v.clear();
		find_ground = false;
		DFS(row, col-1);
		/*cout << "case3" << endl;
		cout << "find_ground: " << find_ground << endl;*/

		if (!find_ground) {
			MoveMinerals();
		}
		//redraw
		for (auto mi : v) {
			MAP[mi.first][mi.second] = 'x';
		}
	}
	
	if (find_ground && row > 1 && MAP[row - 1][col] == 'x') {
		memset(visit, false, sizeof(visit));
		v.clear();
		find_ground = false;
		DFS(row -1, col);
		/*cout << "case4" << endl;
		cout << "find_ground: " << find_ground << endl;*/

		if (!find_ground) {
			MoveMinerals();
		}
		//redraw
		for (auto mi : v) {
			MAP[mi.first][mi.second] = 'x';
		}
	}
}

void ReverseMap(void)
{
	char temp[101][101];

	for (int i = 1; i <= R; i++) {
		for (int j = 1; j <= C; j++) {
			temp[i][j] = MAP[R-i+1][j];
		}
	}
	memcpy(MAP, temp, sizeof(temp));
}

void Solution(void)
{
	bool left;

	ReverseMap();
	for (int i = 0; i < Try; i++) {
		if (i % 2 == 0)
			left = true;
		else
			left = false;

		//Throw
		int h = Throw[i];
		if (left) {
			for (int j = 1; j <= C; j++) {
				if (MAP[h][j] == 'x') {
					MAP[h][j] = '.';
					Check(h, j, true);
					break;
				}
			}
		}
		else {
			for (int j = C; j > 0; j--) {
				if (MAP[h][j] == 'x') {
					MAP[h][j] = '.';
					Check(h, j, false);
					break;
				}
			}
		}
	}
}

int main(void)
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	Input();
	Solution();
	Print();

	return 0;
}
#endif
}