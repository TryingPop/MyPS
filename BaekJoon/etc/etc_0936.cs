using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 2
이름 : 배성훈
내용 : 바둑알 점프
    문제번호 : 17492번

    브루트포스, 백트래킹 문제다
    프로그래머스에서 N-Queen을 풀고 나니,
    
    돌의 수가 적어서 백트래킹하면 풀릴거 같아 시도했다
    시도하니 이상없이 통과했다

    다른 사람의 풀이를 보니, 그냥 BFS 시도해도 풀린다고 한다
*/

namespace BaekJoon.etc
{
    internal class etc_0936
    {

        static void Main936(string[] args)
        {

            StreamReader sr;
            int n, m;
            int[][] map;
            (int r, int c, bool d)[] dol;
            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                if (DFS()) Console.Write("Possible");
                else Console.Write("Impossible");
            }

            bool DFS(int _n = 1)
            {

                if (_n == m) return true;

                for (int i = 1; i <= m; i++)
                {

                    if (dol[i].d) continue;
                    for (int j = 0; j < 8; j++)
                    {

                        int dolR = dol[i].r + dirR[j];
                        int dolC = dol[i].c + dirC[j];

                        int emR = dolR + dirR[j];
                        int emC = dolC + dirC[j];

                        if (ChkInvalidPos(emR, emC) || map[emR][emC] != 0 || map[dolR][dolC] <= 0) continue;

                        map[dol[i].r][dol[i].c] = 0;
                        dol[i].r = emR;
                        dol[i].c = emC;
                        map[dol[i].r][dol[i].c] = i;

                        int dead = map[dolR][dolC];
                        dol[dead].d = true;
                        map[dolR][dolC] = 0;

                        if (DFS(_n + 1)) return true;

                        map[dolR][dolC] = dead;
                        dol[dead].d = false;

                        map[dol[i].r][dol[i].c] = 0;
                        dol[i].r -= 2 * dirR[j];
                        dol[i].c -= 2 * dirC[j];
                        map[dol[i].r][dol[i].c] = i;
                    }
                }

                return false;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= n || _c >= n;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                map = new int[n][];
                dol = new (int r, int c, bool d)[8];
                m = 0;
                for (int r = 0; r < n; r++)
                {

                    map[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        int cur = ReadInt();
                        if (cur == 1) map[r][c] = -1;
                        else if (cur == 2) 
                        { 
                            
                            dol[++m] = (r, c, false); 
                            map[r][c] = m;
                        }
                    }
                }

                dirR = new int[8] { -1, -1, 0, 1, 1, 1, 0, -1 };
                dirC = new int[8] { 0, 1, 1, 1, 0, -1, -1, -1 };

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
from collections import deque
from copy import deepcopy
from itertools import product

n = int(input())
field = [list(map(int, input().split())) for _ in range(n)]

q = deque()
q.append(deepcopy(field))

found = False
while q:
    f = q.popleft()

    count = 0
    for r in range(n):
        for c in range(n):
            if f[r][c] == 2:
                count += 1

    if count == 1:
        found = True
        break

    for r in range(n):
        for c in range(n):
            if f[r][c] != 2:
                continue
            count += 1

            for dr, dc in product([-1, 0, 1], [-1, 0, 1]):
                if dr == 0 and dc == 0:
                    continue

                nr = r + dr
                nc = c + dc
                if f[nr][nc] != 2:
                    continue

                nnr = nr + dr
                nnc = nc + dc
                if f[nnr][nnc] != 0:
                    continue

                nf = deepcopy(f)

                nf[r][c] = 0
                nf[nr][nc] = 0
                nf[nnr][nnc] = 2

                q.append(nf)

if found:
    print('Possible')
else:
    print('Impossible')

#elif other2
// #include<stdio.h>

int bcnt, result, n;
int map[10][10];
int dx[8] = { 1,1,1,-1,-1,-1,0,0 };
int dy[8] = { 1,-1,0,1,-1,0,1,-1 };
void chkA();
void Move();

int main()
{
	scanf("%d", &n);
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < n; j++) {
			scanf("%d", &map[i][j]);
			if (map[i][j] == 2) bcnt++;
		}
	}
	chkA();
	Move();
	if (result == 1) printf("Possible\n");
	else printf("Impossible\n");
}

void Move()
{
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < n; j++) {
			if (map[i][j] != 2) continue;
			for (int k = 0; k < 8; k++) {
				int x = i + dx[k], y = j + dy[k];
				if (map[x][y] != 2) continue;
				if (map[x + dx[k]][y + dy[k]] != 0) continue;
				map[i][j] = 0;
				map[x][y] = 0;
				map[x + dx[k]][y + dy[k]] = 2;
				bcnt--;
				Move();
				bcnt++;
				map[i][j] = 2;
				map[x][y] = 2;
				map[x + dx[k]][y + dy[k]] = 0;
			}
		}
	}
	chkA();
}

void chkA()
{
	if (bcnt == 1 && result == 0) {
		result = 1;
	}
}
#endif
}
