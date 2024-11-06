using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 8
이름 : 배성훈
내용 : Two Dots
    문제번호 : 16929번

    DFS 문제다
    BFS로도 가능하다

    아이디어는 다음과 같다
    4방향으로 1칸씩 이동하며 방문 기록을 한다
    이전 노드의 경우 재방문을 막는다
    그리고 이동을 하는데 이동한 곳이 재방문한 노드면 
    사이클 존재로 간주한다
*/

namespace BaekJoon.etc
{
    internal class etc_0871
    {

        static void Main871(string[] args)
        {

            string YES = "Yes";
            string NO = "No";
            StreamReader sr;

            int row, col;
            int[][] map;
            bool[][] visit;
            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (visit[r][c]) continue;
                        if (DFS(r, c, -1, -1))
                        {

                            Console.Write(YES);
                            return;
                        }
                    }
                }

                Console.Write(NO);
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            bool ChkBefore(int _r, int _c, int _br, int _bc)
            {

                return _r == _br && _c == _bc;
            }

            bool DFS(int _r, int _c, int _br, int _bc)
            {

                for (int i = 0; i < 4; i++)
                {

                    int nextR = _r + dirR[i];
                    int nextC = _c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC) 
                        || ChkBefore(nextR, nextC, _br, _bc)
                        || map[_r][_c] != map[nextR][nextC]) continue;

                    if (visit[nextR][nextC]) return true;
                    visit[nextR][nextC] = true;

                    if (DFS(nextR, nextC, _r, _c)) return true;
                }

                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                map = new int[row][];
                visit = new bool[row][];
                for (int i = 0; i < row; i++)
                {

                    map[i] = new int[col];
                    visit[i] = new bool[col];
                }

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        map[r][c] = sr.Read();
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

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
// #include <stdio.h>
// #include <stdlib.h>
// #include <vector>
// #include <queue>
using namespace std;

int dx[4] = { 1,0,-1,0 }, dy[4] = { 0,1,0,-1 }; //down rigth up left
bool visited[50][50] = { false, };
int N, M;
char** dots;

bool DFS(int prev_row, int prev_col, int row,int col) {
	visited[row][col] = true;

	int nrow, ncol;
	for (int i = 0; i < 4; i++) {
		nrow = row + dx[i];
		ncol = col + dy[i];

		if (nrow == prev_row && ncol == prev_col) continue;

		if (0 <= nrow && nrow < N && 0 <= ncol && ncol < M && dots[row][col] == dots[nrow][ncol]) {
			if (visited[nrow][ncol] == true) return true;
			if (DFS(row, col, nrow, ncol)) return true;
		}
	}
	return false;
}

int main() {
	
	scanf("%d %d", &N, &M);

	dots = (char**)malloc(sizeof(char*) * N);
	for (int i = 0; i < N; i++)
		dots[i] = (char*)malloc(sizeof(char) * M);

	for (int i = 0; i < N; i++)
		scanf("%s", dots[i]);

	bool flag = false;
	for (int i = 0; i < N && flag == false; i++) {
		for (int j = 0; j < M && flag == false; j++) {
			if (!visited[i][j])
				flag = DFS(-1, -1, i, j);
		}
	}

	if (flag) printf("Yes");
	else printf("No");

	return 0;
}
#elif other2
namespace ConsoleApp1
{
    internal class Program
    {
        static int[] dy = new int[] { 0, 1, 0, -1 };
        static int[] dx = new int[] { 1, 0, -1, 0 };
        static int n, m;
        static char[,] map;
        static bool[,] visit;
        static bool answer = false;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            n = arr[0]; m = arr[1];
            map = new char[n, m];
            visit = new bool[n, m];

            for (int i = 0; i < n; i++)
            {
                string s = input.ReadLine();
                for (int j = 0; j < m; j++)
                    map[i, j] = s[j];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (visit[i, j]) continue;
                    dfs(1, 0, 0, i, j);
                }
            }

            output.Write(answer ? "Yes" : "No");

            input.Close();
            output.Close();
        }
        static void dfs(int len, int br, int bc, int row, int col)
        {
            if (answer) return;

            for (int i = 0; i < 4; i++)
            {
                int nr = row + dy[i];
                int nc = col + dx[i];
                if (nr < 0 || nr == n || nc < 0 || nc == m || map[row, col] != map[nr, nc]) continue;
                if (len >= 3 && (nr != br || nc != bc) && visit[nr, nc])//길이가 3이상이고 바로 전 위치랑 겹치지 않으면서 이미 방문했던곳을 만나면 사이클이다.
                {
                    answer = true;
                    return;
                }
                else if (!visit[nr, nc])
                {
                    visit[nr, nc] = true;
                    dfs(len + 1, row, col, nr, nc);
                }
            }
        }
    }
}
#endif
}
