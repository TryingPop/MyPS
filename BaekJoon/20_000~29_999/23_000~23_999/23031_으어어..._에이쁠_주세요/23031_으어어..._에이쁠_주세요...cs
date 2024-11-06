using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 1
이름 : 배성훈
내용 : 으어어… 에이쁠 주세요..
    문제번호 : 23031번

    구현, 시뮬레이션 문제다
    스위치가 하나인줄 알거나, 스위치를 켜면 1씩 타일이 증가하거나
    문제를 잘못 이해했다
    그리고 스위치를 켜면 스위치 부분이 불빛으로 바뀌게 
    잘못 구현해 3번 틀렸다

    해당 부분을 고치니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_1013
    {

        static void Main1013(string[] args)
        {

            string YES = "Phew...";
            string NO = "Aaaaaah!";
            StreamReader sr;

            int n;
            string op;
            int[][] board;
            int[] dirR, dirC;
            (int r, int c, int d)[] students;
            int sLen;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dirR = new int[8] { 1, 0, -1, 0, -1, -1, 1, 1 };
                dirC = new int[8] { 0, -1, 0, 1, -1, 1, -1, 1 };
                board[0][0] = 1;
                for (int i = 0; i < op.Length; i++)
                {

                    if (AraTurn(op[i]) || ZombieTurn())
                    { 
                        
                        Console.Write(NO);
                        return;
                    }
                }

                Console.Write(YES);
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= n || _c >= n;
            }

            bool AraTurn(char _op)
            {

                int dir = students[0].d;
                if (_op == 'R')
                    students[0].d = dir == 3 ? 0 : dir + 1;
                else if (_op == 'L')
                    students[0].d = dir == 0 ? 3 : dir - 1;
                else
                {

                    int r = students[0].r;
                    int c = students[0].c;

                    int nR = r + dirR[dir];
                    int nC = c + dirC[dir];

                    if (ChkInvalidPos(nR, nC)) return false;

                    if (board[nR][nC] == 2) return true;
                    else if (board[nR][nC] == 3)
                    {

                        board[nR][nC] = 4;
                        for (int i = 0; i < 8; i++)
                        {

                            int chkR = nR + dirR[i];
                            int chkC = nC + dirC[i];

                            if (ChkInvalidPos(chkR, chkC) || board[chkR][chkC] == 3) continue;
                            board[chkR][chkC] = 4;
                        }
                    }
                    else if (board[nR][nC] == 0) board[nR][nC] = 1;

                    if (board[r][c] == 1) board[r][c] = 0;
                    students[0] = (nR, nC, dir);
                }

                return false;
            }

            bool ZombieTurn()
            {

                for (int i = 1; i <= sLen; i++)
                {

                    if (board[students[i].r][students[i].c] == 2) 
                        board[students[i].r][students[i].c] = 0;
                }

                for (int i = 1; i <= sLen; i++)
                {

                    int r = students[i].r;
                    int c = students[i].c;
                    int dir = students[i].d;

                    int nR = r + dirR[dir];
                    int nC = c + dirC[dir];

                    if (ChkInvalidPos(nR, nC))
                    {

                        students[i].d = (dir + 2) % 4;
                        continue;
                    }

                    if (board[nR][nC] == 1) return true;
                    else if (board[nR][nC] == 0) board[nR][nC] = 2;
                    students[i] = (nR, nC, dir);
                }

                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                op = sr.ReadLine();

                board = new int[n][];
                students = new (int r, int c, int d)[n * 2 + 1];
                sLen = 0;
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        int cur = sr.Read();
                        if (cur == 'Z')
                        {

                            students[++sLen] = (r, c, 0);
                            board[r][c] = 2;
                        }
                        else if (cur == 'S') 
                            board[r][c] = 3;
                    }

                    if (sr.Read() == '\r') sr.Read();
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
// #include <cstdio>
// #define MAX 53

int main(){
	int n;
	char a[MAX];
	char map[MAX][MAX];
	scanf("%d %s", &n, a);
	for(int i = 0; i < n; ++i){
		scanf("%s", map[i]);
	}
	int dx[8] = {1, 1, 0, -1, -1, -1, 0, 1};
	int dy[8] = {0, 1, 1, 1, 0, -1, -1, -1};
	bool s[MAX][MAX] = {};
	int zombie[MAX][MAX] = {};
	for(int x = 0; x < n; ++x){
		for(int y = 0; y < n; ++y){
			if(map[x][y] == 'S'){
				s[x][y] = true;
			}else if(map[x][y] == 'Z'){
				zombie[x][y] |= 1;
			}
		}
	}
	bool safe[MAX][MAX] = {};
	int x = 0, y = 0;
	int d = 0;
	for(int idx = 0; a[idx] != '\0'; ++idx){
		if(a[idx] == 'F'){
			int nx = x + dx[d];
			int ny = y + dy[d];
			if(nx >= 0 && nx < n && ny >= 0 && ny < n){
				x = nx;
				y = ny;
			}
		}else if(a[idx] == 'L'){
			d = (d + 2) % 8;
		}else{
			d = (d + 6) % 8;
		}
		if(s[x][y]){
			safe[x][y] = true;
			for(int i = 0; i < 8; ++i){
				int nx = x + dx[i];
				int ny = y + dy[i];
				if(nx >= 0 && nx < n && ny >= 0 && ny < n){
					safe[nx][ny] = true;
				}
			}
		}
		if(zombie[x][y] && !safe[x][y]){
			printf("Aaaaaah!\n");
			return 0;
		}
		int new_zombie[MAX][MAX] = {};
		for(int x = 0; x < n; ++x){
			for(int y = 0; y < n; ++y){
				if(zombie[x][y] & 1){
					if(x + 1 < n){
						new_zombie[x + 1][y] |= 1;
					}else{
						new_zombie[x][y] |= 2;
					}
				}
				if(zombie[x][y] & 2){
					if(x - 1 >= 0){
						new_zombie[x - 1][y] |= 2;
					}else{
						new_zombie[x][y] |= 1;
					}
				}
			}
		}
		for(int x = 0; x < n; ++x){
			for(int y = 0; y < n; ++y){
				zombie[x][y] = new_zombie[x][y];
			}
		}
		if(zombie[x][y] && !safe[x][y]){
			printf("Aaaaaah!\n");
			return 0;
		}
	}
	printf("Phew...\n");
	return 0;
}
#endif
}
