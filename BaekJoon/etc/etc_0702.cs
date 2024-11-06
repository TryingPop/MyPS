using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 17
이름 : 배성훈
내용 : 룩 어택
    문제번호 : 1574번

    이분 매칭 문제다
    빈자리는 놓을 순 없지만 공격 경로로는 인정하는 자리다
    빈자리를 제외하고 행과 열로 이어 이분매칭을 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0702
    {

        static void Main702(string[] args)
        {

            StreamReader sr;
            int row, col;
            int len;

            int[][] board;

            List<int>[] line;
            int[] match;
            bool[] visit;

            Solve();

            void Solve()
            {

                Input();

                LinkLine();

                int ret = 0;
                for (int i = 1; i <= row; i++)
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

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        if (board[r][c] == 1) continue;
                        line[r].Add(c);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                match = new int[col + 1];
                visit = new bool[col + 1];

                line = new List<int>[row + 1];

                board = new int[row + 1][];

                for (int i = 1; i <= row; i++)
                {

                    line[i] = new(col);
                    board[i] = new int[col + 1];
                }

                len = ReadInt();
                for (int i = 0; i < len; i++)
                {

                    int r = ReadInt();
                    int c = ReadInt();

                    board[r][c] = 1;
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
// #include <stdio.h>
// #include <queue>
using namespace std;
int v[330][330], n, m, x, y, z;
int a[330];
int b[330];
int d[330];
queue<int>q;
void bfs()
{
	for (int i = 0; i < n; i++)d[i] = -1;
	for (int i = 0; i < n; i++)if (a[i] < 0)q.push(i), d[i] = 0;
	while (q.size())
	{
		x = q.front();
		q.pop();
		for (int i = 0; i < m; i++)
		{
			if (v[x][i])continue;
			if (b[i] < 0)continue;
			if (d[b[i]] < 0)
			{
				d[b[i]] = d[x] + 1;
				q.push(b[i]);
			}
		}
	}
}
int dfs(int x)
{
	for (int i = 0; i < m; i++)
	{
		if (v[x][i])continue;
		if (b[i] < 0 || d[b[i]] == d[x] + 1 && dfs(b[i]))
		{
			a[x] = i;
			b[i] = x;
			return 1;
		}
	}
	return 0;
}
int main(void)
{
	scanf("%d %d %d", &n, &m, &z);
	while (z--)
	{
		scanf("%d %d", &x, &y);
		v[x - 1][y - 1] = 1;
	}
	for (int i = 0; i < n; i++)a[i] = -1;
	for (int i = 0; i < m; i++)b[i] = -1;
	y = 0;
	while (1)
	{
		bfs();
		z = 0;
		for (int i = 0; i < n; i++)if (a[i] < 0)z += dfs(i);
		if (!z)break;
		y += z;
	}
	printf("%d\n", y);
}
#endif
}
