using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 4
이름 : 배성훈
내용 : Pegman (Small, Large)
    문제번호 : 12139번, 12140번

    구현, 그리디, 시뮬레이션 문제다
    모든 화살표에 대해 다른 화살표로 향하게 할 수 있으면 된다
    그러면 화살표는 화살표를 가리키므로 사이클이 보장된다
    반면 어느 하나라도 안되면 불가능으로 판별하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0943
    {

        static void Main943(string[] args)
        {

            int MAX_SIZE = 100;

            string IMPO = "IMPOSSIBLE\n";

            StreamReader sr;
            StreamWriter sw;

            int[][] board;
            int[][] visit;
            int[] dirR, dirC;
            int row, col;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();
                for (int t = 1; t <= test; t++)
                {

                    sw.Write($"Case #{t}: ");
                    Input();

                    GetRet();
                }

                sw.Close();
                sr.Close();
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void GetRet()
            {

                int cnt = 0;
                bool impo = false;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int dir = board[r][c];
                        if (dir == 0) continue;

                        int add = -1;
                        for (int i = 1; i <= 4; i++)
                        {

                            int curR = r;
                            int curC = c;

                            while (true)
                            {

                                int nextR = curR + dirR[i];
                                int nextC = curC + dirC[i];

                                if (ChkInvalidPos(nextR, nextC)) break;

                                if (board[nextR][nextC] != 0)
                                {

                                    if (add == -1) add = dir == i ? 0 : 1;
                                    else add = Math.Min(add, dir == i ? 0 : 1);
                                    break;
                                }

                                curR = nextR;
                                curC = nextC;
                            }

                        }

                        if (add == -1)
                        {

                            impo = true;
                            break;
                        }
                        cnt += add;
                    }

                    if (impo) break;
                }

                if (impo) sw.Write(IMPO);
                else sw.Write($"{cnt}\n");
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == '^') board[r][c] = 1;
                        else if (cur == '>') board[r][c] = 2;
                        else if (cur == 'v') board[r][c] = 3;
                        else if (cur == '<') board[r][c] = 4;
                        else board[r][c] = 0;

                        visit[r][c] = -1;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                board = new int[MAX_SIZE][];
                visit = new int[MAX_SIZE][];
                for (int r = 0; r < board.Length; r++)
                {

                    board[r] = new int[MAX_SIZE];
                    visit[r] = new int[MAX_SIZE];
                }

                dirR = new int[5] { 0, -1, 0, 1, 0 };
                dirC = new int[5] { 0, 0, 1, 0, -1 };
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
// #define endl '\n'
// #define PRECISION 9
using namespace std;

int n, m;
char mp[5][5];
const int dy[4] = {-1, 0, 1, 0}, dx[4] = {0, 1, 0, -1};
const string dir = "^>v<";

void Main(){
	int t; cin >> t;
	for (int tt = 1; tt <= t; tt++){
		cout << "Case #" << tt << ": ";
		cin >> n >> m;
		for (int i = 1; i <= n; i++){
			for (int j = 1; j <= m; j++){ cin >> mp[i][j]; }
		}
		int ans = 0;
		for (int i = 1; i <= n; i++){
			for (int j = 1; j <= m; j++){
				if (mp[i][j] == '.'){ continue; }
				bool flg0 = 0, flg1 = 0;
				for (int k = 0; k < 4; k++){
					int y = i+dy[k], x = j+dx[k]; bool flg = 0;
					while (1){
						if (1 > y || y > n){ break; }
						if (1 > x || x > m){ break; }
						if (mp[y][x] != '.'){ flg = 1; break; }
						y += dy[k]; x += dx[k];
					}
					flg0 |= flg; flg1 |= (flg && mp[i][j]==dir[k]);
				}
				if (!flg0){ cout << "IMPOSSIBLE"; goto done; }
				ans += !flg1;
			}
		}
		cout << ans;
		done: cout << endl;
	}
}

int main(){
	ios_base::sync_with_stdio(0); cin.tie(0); cout.tie(0);
	cout.setf(ios::fixed); cout.precision(PRECISION);
	Main();
}

#endif
}
