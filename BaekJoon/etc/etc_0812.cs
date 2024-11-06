using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 14
이름 : 배성훈
내용 : 더 푸르게
    문제번호 : 9983번

    구현, 브루트포스, 백트래킹 문제다
    브루트포스로 풀었다

    효율성을 생각한다면, 네트워크 플로우로 접근해야할지 싶다
*/

namespace BaekJoon.etc
{
    internal class etc_0812
    {

        static void Main812(string[] args)
        {

            string RET = "Minimum Number of Pieces to be removed: ";
            int MAX = 10;

            int K = 'K' - '0';
            int Q = 'Q' - '0';
            int R = 'R' - '0';
            int B = 'B' - '0';
            int N = 'N' - '0';
            int E = 'E' - '0';

            StreamReader sr;
            StreamWriter sw;

            int row, col, len;
            int[][] board;
            int[] dir8R, dir8C;
            int[] dirNR, dirNC;

            (int r, int c)[] units;

            int ret;

            Solve();

            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                string chk = sr.ReadLine();

                while(chk != null && chk != string.Empty)
                {

                    Input();

                    ret = len;
                    DFS();
                    sw.Write(RET);
                    sw.Write($"{ret}\n");

                    sr.ReadLine();
                    chk = sr.ReadLine();
                }

                sr.Close();
                sw.Close();
            }

            bool ChkKill(int _idx)
            {
                
                int r = units[_idx].r;
                int c = units[_idx].c;
                int type = board[r][c];

                if (type == K) return ChkK(r, c);
                else if (type == Q) return ChkQ(r, c);
                else if (type == R) return ChkR(r, c);
                else if (type == B) return ChkB(r, c);
                else if (type == N) return ChkN(r, c);

                return false;
            }

            bool ChkK(int _r, int _c)
            {

                for (int i = 0; i < 8; i++)
                {

                    int nR = _r + dir8R[i];
                    int nC = _c + dir8C[i];

                    if (ChkInvalidPos(nR, nC) || ChkEmpty(nR, nC)) continue;
                    return true;
                }
                
                return false;
            }

            bool ChkQ(int _r, int _c)
            {

                return ChkR(_r, _c) || ChkB(_r, _c);
            }

            bool ChkR(int _r, int _c)
            {

                for (int i = 1; i < 8; i += 2)
                {

                    int nR = _r;
                    int nC = _c;

                    for (int j = 0; j < MAX; j++)
                    {

                        nR += dir8R[i];
                        nC += dir8C[i];

                        if (ChkInvalidPos(nR, nC)) break;
                        else if (ChkEmpty(nR, nC)) continue;

                        return true;
                    }
                }

                return false;
            }

            bool ChkB(int _r, int _c)
            {

                for (int i = 0; i < 8; i += 2)
                {

                    int nR = _r;
                    int nC = _c;

                    for (int j = 0; j < MAX; j++)
                    {

                        nR += dir8R[i];
                        nC += dir8C[i];

                        if (ChkInvalidPos(nR, nC)) break;
                        else if (ChkEmpty(nR, nC)) continue;

                        return true;
                    }
                }

                return false;
            }

            bool ChkN(int _r, int _c)
            {

                for (int i = 0; i < 8; i++)
                {

                    int nR = _r + dirNR[i];
                    int nC = _c + dirNC[i];

                    if (ChkInvalidPos(nR, nC) || ChkEmpty(nR, nC)) continue;
                    return true;
                }

                return false;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            bool ChkEmpty(int _r, int _c)
            {

                if (board[_r][_c] == E) return true;
                return false;
            }

            void DFS(int _depth = 0, int _broken = 0)
            {

                if (_depth == len)
                {

                    for (int i = 0; i < len; i++)
                    {

                        if (ChkKill(i)) return;
                    }

                    ret = Math.Min(ret, _broken);
                    return;
                }

                int r = units[_depth].r;
                int c = units[_depth].c;

                int type = board[r][c];
                board[r][c] = E;
                DFS(_depth + 1, _broken + 1);
                board[r][c] = type;

                DFS(_depth + 1, _broken);
                return;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                board = new int[MAX][];
                for (int i = 0; i < MAX; i++)
                {

                    board[i] = new int[MAX];
                }

                dir8R = new int[8] { -1, -1, -1, 0, 1, 1, 1, 0 };
                dir8C = new int[8] { -1, 0, 1, 1, 1, 0, -1, -1 };

                dirNR = new int[8] { -2, -1, 1, 2, 2, 1, -1, -2 };
                dirNC = new int[8] { 1, 2, 2, 1, -1, -2, -2, -1 };

                units = new (int r, int c)[15];
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();
                len = 0;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = ReadInt();
                        board[r][c] = cur;

                        if (cur == E) continue;
                        units[len++] = (r, c);
                    }
                }
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
int w, h;
char start[7], end[5], map[11][11], s;
int piece[101][2], idx, min, check[101], chk[11][11];
int dx[8] = { -1,1,0,0,-1,-1,1,1 };
int dy[8] = { 0,0,-1,1,-1,1,-1,1 };
int play() {
	for (int i = 1; i < idx; i++) {
		if (!check[i]) {
			int x = piece[i][0];
			int y = piece[i][1];
			if (map[x][y] == 'K') {
				for (int d = 0; d < 8; d++) {
					int nx = x + dx[d];
					int ny = y + dy[d];
					if (nx >= 0 && nx < h && ny >= 0 && ny < w) {
						int k = chk[nx][ny];
						if (k && !check[k]) return 0;
					}
				}
			}
			else if (map[x][y] == 'Q') {
				for (int d = 0; d < 8; d++) {
					int nx = x + dx[d];
					int ny = y + dy[d];
					while (1) {
						if (nx < 0 || nx >= h || ny < 0 || ny >= w) break;
						int k = chk[nx][ny];
						if (k && !check[k]) return 0;
						nx += dx[d]; ny += dy[d];
					}
				}
			}
			else if (map[x][y] == 'R') {
				for (int d = 0; d < 4; d++) {
					int nx = x + dx[d];
					int ny = y + dy[d];
					while (1) {
						if (nx < 0 || nx >= h || ny < 0 || ny >= w) break;
						int k = chk[nx][ny];
						if (k && !check[k]) return 0;
						nx += dx[d]; ny += dy[d];
					}
				}
			}
			else if (map[x][y] == 'B') {
				for (int d = 4; d < 8; d++) {
					int nx = x + dx[d];
					int ny = y + dy[d];
					while (1) {
						if (nx < 0 || nx >= h || ny < 0 || ny >= w) break;
						int k = chk[nx][ny];
						if (k && !check[k]) return 0;
						nx += dx[d]; ny += dy[d];
					}
				}

			}
			else if (map[x][y] == 'N') {
				for (int d = 0; d < 2; d++) {
					for (int dd = 2; dd < 4; dd++) {
						int nx = x + 2 * dx[d] + dx[dd];
						int ny = y + 2 * dy[d] + dy[dd];
						if (nx >= 0 && nx < h && ny >= 0 && ny < w) {
							int k = chk[nx][ny];
							if (k && !check[k]) return 0;
						}
					}
				}
				for (int d = 2; d < 4; d++) {
					for (int dd = 0; dd < 2; dd++) {
						int nx = x + 2 * dx[d] + dx[dd];
						int ny = y + 2 * dy[d] + dy[dd];
						if (nx >= 0 && nx < h && ny >= 0 && ny < w) {
							int k = chk[nx][ny];
							if (k && !check[k]) return 0;
						}
					}
				}
			}
		}
	}
	return 1;
}
void dfs(int count, int k) {
	if (play()) {
		if (min > count) {
			min = count;
		}
		return;
	}
	for (int i = k; i < idx; i++) {
		if (!check[i]) {
			check[i] = 1;
			dfs(count + 1, i);
			check[i] = 0;
		}
	}
}
int main(void) {
	while (1) {
		idx = 1;
		min = 1000;
		if (scanf("%s", start) == EOF) break;
		scanf("%d%d", &w, &h);
		for (int i = 0; i < h; i++) {
			for (int j = 0; j < w; j++) {
				scanf(" %c", &map[i][j]);
				if (map[i][j] != 'E') {
					chk[i][j] = idx;
					piece[idx][0] = i;
					piece[idx++][1] = j;
				}
			}
		}
		scanf("%s", end);
		dfs(0, 1);
		for (int i = 0; i < h; i++) {
			for (int j = 0; j < w; j++) {
				chk[i][j] = 0;
			}
		}
		printf("Minimum Number of Pieces to be removed: %d\n", min);
	}
}
#endif
}
