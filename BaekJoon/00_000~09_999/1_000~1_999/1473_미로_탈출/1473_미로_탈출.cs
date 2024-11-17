using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 17
이름 : 배성훈
내용 : 미로 탈출
    문제번호 : 1473번

    너비 우선 탐색, 비트마스킹 문제다.
    회전 상태와 좌표에 따라 최소 도달 시간을 구하고,
    목표 지점을 모두 탐색해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1120
    {

        static void Main1120(string[] args)
        {

            int row, col;
            int[][][][] move;
            int[][] board;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                Queue<(int r, int c, int rS, int cS)> q = new(row * col * (1 << row) * (1 << col));

                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                q.Enqueue((0, 0, 0, 0));
                move[0][0][0][0] = 1;

                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    int cur = move[node.rS][node.cS][node.r][node.c];

                    for (int i = 0; i < 4; i++)
                    {

                        if (ChkImpo(node.r, node.c, node.rS, node.cS, i)) continue;
                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || ChkImpo(nR, nC, node.rS, node.cS, i)
                            || move[node.rS][node.cS][nR][nC] != 0) continue;
                        q.Enqueue((nR, nC, node.rS, node.cS));
                        move[node.rS][node.cS][nR][nC] = cur + 1;
                    }

                    node.rS ^= 1 << node.r;
                    node.cS ^= 1 << node.c;
                    if (move[node.rS][node.cS][node.r][node.c] != 0) continue;
                    q.Enqueue((node.r, node.c, node.rS, node.cS));
                    move[node.rS][node.cS][node.r][node.c] = cur + 1;
                }

                // 맵 벗어났는지 확인
                bool ChkInvalidPos(int _r, int _c) => _r < 0 || _c < 0 || _r >= row || _c >= col;
                // 해당 좌표에서 방향이 가능한지 확인
                // 가능하면 false, 불가능하면 true
                bool ChkImpo(int _r, int _c, int _rS, int _cS, int _dir)
                {

                    int cur = board[_r][_c];
                    if (cur == 1) return true;
                    else if (cur == 0) return false;

                    int chk1 = (1 << _r & _rS) == 0 ? 0 : 1;
                    int chk2 = (1 << _c & _cS) == 0 ? 0 : 1;
                    
                    // 회전 둘 다 되면 180도 회전이고 기존과 기능이 같다.
                    // 그래서 XOR 연산인 ^로 표현
                    if ((chk1 ^ chk2) == 1)
                    {

                        if (cur == 2) cur = 3;
                        else cur = 2;
                    }

                    // 위 아래 이동 가능한 곳이고 방향이 위아래인 경우
                    if (cur == 2 && _dir % 2 == 0) return false;
                    // 좌우 이동 가능한 곳이고 방향이 좌우인 경우
                    else if (cur == 3 && _dir % 2 == 1) return false;
                    else return true;
                }

                // 골에 도착하는 경우 조사
                int ret = 10_000_000;
                for (int i = 0; i < 1 << row; i++)
                {

                    for (int j = 0; j < 1 << col; j++)
                    {

                        if (move[i][j][row - 1][col - 1] == 0) continue;
                        ret = Math.Min(ret, move[i][j][row - 1][col - 1]);
                    }
                }

                if (ret == 10_000_000) Console.Write(-1);
                else Console.Write(ret - 1);
            }

            void SetArr()
            {

                move = new int[1 << row][][][];
                for (int i = 0; i < 1 << row; i++)
                {

                    move[i] = new int[1 << col][][];
                    for (int j = 0; j < 1 << col; j++)
                    {

                        move[i][j] = new int[row][];
                        for (int r = 0; r < row; r++)
                        {

                            move[i][j][r] = new int[col];
                        }
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        // 4방향 모두 이동 가능 A -> 0
                        // 4방향 모두 접근 불가능 B -> 1
                        // 위 아래 이동 가능 C -> 2
                        // 좌 우 이동 가능 D -> 3
                        board[r][c] = sr.Read() - 'A';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();

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
    }

#if other
using ProblemSolving.Templates.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Collections.Generic {}
namespace System.IO {}
namespace System.Linq {}

#nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var (height, width) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var map = new string[height];
        for (var idx = 0; idx < height; idx++)
            map[idx] = sr.ReadLine();

        var inq = new bool[1 << width, 1 << height, width, height];
        var q = new Queue<(int move, int stx, int sty, int x, int y)>();
        q.Enqueue((0, 0, 0, 0, 0));
        inq[0, 0, 0, 0] = true;

        while (q.TryDequeue(out var state))
        {
            var (move, stx, sty, x, y) = state;

            if ((x, y) == (width - 1, height - 1))
            {
                sw.WriteLine(move);
                return;
            }

            if (IsHorizontalOpen(stx, sty, x, y, map))
            {
                if (x > 0 && !inq[stx, sty, x - 1, y] && IsHorizontalOpen(stx, sty, x - 1, y, map))
                {
                    inq[stx, sty, x - 1, y] = true;
                    q.Enqueue((1 + move, stx, sty, x - 1, y));
                }
                if (x + 1 < width && !inq[stx, sty, x + 1, y] && IsHorizontalOpen(stx, sty, x + 1, y, map))
                {
                    inq[stx, sty, x + 1, y] = true;
                    q.Enqueue((1 + move, stx, sty, x + 1, y));
                }
            }
            if (IsVerticalOpen(stx, sty, x, y, map))
            {
                if (y > 0 && !inq[stx, sty, x, y - 1] && IsVerticalOpen(stx, sty, x, y - 1, map))
                {
                    inq[stx, sty, x, y - 1] = true;
                    q.Enqueue((1 + move, stx, sty, x, y - 1));
                }
                if (y + 1 < height && !inq[stx, sty, x, y + 1] && IsVerticalOpen(stx, sty, x, y + 1, map))
                {
                    inq[stx, sty, x, y + 1] = true;
                    q.Enqueue((1 + move, stx, sty, x, y + 1));
                }
            }

            if (!inq[stx ^ (1 << x), sty ^ (1 << y), x, y])
            {
                inq[stx ^ (1 << x), sty ^ (1 << y), x, y] = true;
                q.Enqueue((1 + move, stx ^ (1 << x), sty ^ (1 << y), x, y));
            }
        }

        sw.WriteLine(-1);
    }

    public static bool IsHorizontalOpen(int stx, int sty, int x, int y, string[] map)
    {
        var ch = map[y][x];
        var fx = (stx & (1 << x)) != 0;
        var fy = (sty & (1 << y)) != 0;

        if (ch == 'A')
            return true;
        if (ch == 'B')
            return false;
        if (ch == 'C')
            return fx ^ fy;
        if (ch == 'D')
            return !(fx ^ fy);
        return false;
    }
    public static bool IsVerticalOpen(int stx, int sty, int x, int y, string[] map)
    {
        var ch = map[y][x];
        var fx = (stx & (1 << x)) != 0;
        var fy = (sty & (1 << y)) != 0;

        if (ch == 'A')
            return true;
        if (ch == 'B')
            return false;
        if (ch == 'C')
            return !(fx ^ fy);
        if (ch == 'D')
            return fx ^ fy;
        return false;
    }
}

namespace ProblemSolving.Templates.Utility
{
    public static class DeconstructHelper
    {
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
    }
}
#elif other2
// #include <bits/stdc++.h>
// #pragma GCC optimize("O3")
using namespace std;

const int dx[]={0, 0, 1, -1};
const int dy[]={1, -1, 0, 0};

int n, m;
char a[10][10];
bool v[7][7][1<<7][1<<7];
// #define valid(x, y) (x>=0 && x<n && y>=0 && y<m)
int solve()
{
	queue<pair<int, int> > Q;
	Q.push({0, 0});
	v[0][0][0][0]=1;
	int cnt=0;
	while(Q.size())
	{
		for(int size=Q.size(); size--;)
		{
			auto [u, w]=Q.front();
			int x=u>>4, y=u&0xf, r=w>>7, c=w&0x7f;
			Q.pop();
			
			if(x==n-1 && y==m-1) return cnt;
			int nr=r^(1<<x), nc=c^(1<<y);

			if(!v[x][y][nr][nc])
			{
				Q.push({(x<<4)|y, (nr<<7)|nc});
				v[x][y][nr][nc]=1;
			}
		
			for(int i=4; i--;)
			{
				int nx=x+dx[i], ny=y+dy[i];
				if(!valid(nx, ny) || a[nx][ny]=='B' || a[x][y]=='B') continue;
				if((a[nx][ny]=='A' || ((i>>1)+(a[nx][ny]-'C')+!!(r&(1<<nx))+!!(c&(1<<ny)))&1) && \
				(a[x][y]=='A' || ((i>>1)+(a[x][y]-'C')+!!(r&(1<<x))+!!(c&(1<<y)))&1))
				{
					if(!v[nx][ny][r][c])
					{
					Q.push({(nx<<4)|ny, (r<<7)|c});
						v[nx][ny][r][c]=1;
					}
				}
			}
		}

		cnt++;
	}
	return -1;
}
int main()
{
    scanf("%d %d", &n, &m);
	for(int i=0; i<n; i++)
		scanf("%s", a[i]);

	printf("%d", solve());
}
#endif
}
