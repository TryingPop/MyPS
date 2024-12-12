using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 12
이름 : 배성훈
내용 : 그리스-로마 건축
    문제번호 : 2967번

    구현, 브루트포스 문제다.
    아이디어는 다음과 같다. 

    먼저 왼쪽 위 끝점을 하나 찾는다.
    그리고 오른쪽과 아래로 한칸씩 이동하며 해당 변의 길이를 찾는다.
    이중 짧은 것을 길이로 삼으면 된다.
    이제 해당 정사각형은 하나가 된다!

    다음으로 두 번째는 정사각형 찾는 것은 주의해야 한다.
    먼저 첫 번째 찾은 정사각 형 내부에 지었을 경우, 그냥 첫 번째 정사각형 크기를 반환한다.

    반면 첫 번째 정사각형 내부가 아닌 곳에 지었다면,
    발견되는 x좌표가 적어도 하나 존재한다.
    그러면 x좌표들의 r, c의 차 중 큰게 두 번째 정사각형의 길이가 된다.
    그리고 이제 시작지점을 찾아야 하는데, 시작지점의 r은 maxR - dis이거나 minR일 수 있다.
    해당 좌표가 지워진 곳인지 혹은 'x'이면 해당 좌표가 시작지점이 된다.
    처음에 위를 우선으로 탐색했기에 maxR - dis -> minR 순으로 확인하면 이상 없다!
    이후 maxC - dis, minC도 똑같이 확인하면 두번째 정사각형을 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1182
    {

        static void Main1182(string[] args)
        {

            int row, col;
            int[][] board;
            (int r, int c, int dis) ret;
            Solve();
            void Solve()
            {

                Input();

                GetFirst();

                GetSecond();
            }

            void GetFirst()
            {

                ret = (-1, -1, -1);
                bool flag = true;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == 0) continue;
                        int add = 1;
                        while(true)
                        {

                            if (ChkInvalidPos(r + add, c + add) 
                                || board[r][c + add] == 0 
                                || board[r + add][c] == 0) break;

                            add++;
                        }

                        ret = (r, c, add);
                        Console.Write($"{ret.r + 1} {ret.c + 1} {ret.dis}\n");
                        for (int i = 0; i < add; i++)
                        {
                            
                            for (int j = 0; j < add; j++)
                            {

                                board[r + i][c + j] = -1;
                            }
                        }
                        flag = false;
                        break;
                    }

                    if (flag) continue;
                    break;
                }
            }

            void GetSecond()
            {

                int minR = row + 1;
                int maxR = -1;
                int minC = col + 1;
                int maxC = -1;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] != 1) continue;
                        minR = Math.Min(r, minR);
                        maxR = Math.Max(r, maxR);

                        minC = Math.Min(c, minC);
                        maxC = Math.Max(c, maxC);
                    }
                }

                if (minR != row + 1)
                {

                    int dis = Math.Max(maxR - minR, maxC - minC);
                    if (ChkPos(maxR - dis, maxC - dis)) ret = (maxR - dis, maxC - dis, dis + 1);
                    else if (ChkPos(maxR - dis, minC + dis)) ret = (maxR - dis, minC, dis + 1);
                    else if (ChkPos(minR + dis, maxC - dis)) ret = (minR, maxC - dis, dis + 1);
                    else ret = (minR, minC, dis + 1);
                }

                Console.Write($"{ret.r + 1} {ret.c + 1} {ret.dis}");

                bool ChkPos(int _r, int _c)
                {

                    return !ChkInvalidPos(_r, _c) && board[_r][_c] != 0;
                }
            }

            bool ChkInvalidPos(int _r, int _c) => _r < 0 || _c < 0 || row <= _r || col <= _c;

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string[] temp = sr.ReadLine().Split();
                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    string line = sr.ReadLine();
                    board[r] = new int[col];

                    for (int c = 0; c < col; c++)
                    {

                        if (line[c] == '.') continue;
                        board[r][c] = 1;
                    }
                }

                sr.Close();
            }
        }
    }

#if other
// #include <cstdio>
// #include <cstring>
// #include <algorithm>

using namespace std;

int r, c;
char in[101][101];

bool check(int x, int y, int s, int n) {
	for (int i = x; i < x + s; i++) {
		for (int j = y; j < y + s; j++) {
			if (in[i][j] == '.')
				return false;
			if (in[i][j] == 'x')
				--n;
		}
	}
	return n == 0;
}

int main() {
	scanf("%d%d", &r, &c);

	int n = 0;
	for (int i = 0; i < r; i++) {
		scanf("%s", in[i]);
		for (int j = 0; j < c; j++)
			if (in[i][j] == 'x') n++;
	}
	
	int x, y, s;
	bool br = 0;
	for (x = 0; x < r; x++) {
		for (y = 0; y < c; y++) {
			if (in[x][y] == 'x') {
				br = 1;
				break;
			}
		}
		if (br)
			break;
	}
	
	for (s = 0; x + s < r && y + s < c; s++)
		if (x + s + 1 >= r || y + s + 1 >= c || in[x + s + 1][y] == '.' ||
				in[x][y + s + 1] == '.')
			break;
	++s;

	printf("%d %d %d\n", x + 1, y + 1, s);

	for (int i = x; i < x + s; i++) for (int j = y; j < y + s; j++) {
		in[i][j] = 'o';
		n--;
	}

	if (n == 0) {
		printf("%d %d %d\n", x + 1, y + 1, s);
		return 0;
	}

	int xs = r, xf = 0, ys = c, yf = 0;
	for (int i = 0; i < r; i++) {
		for (int j = 0; j < c; j++) {
			if (in[i][j] != 'x') continue;
			xs = min(xs, i);
			xf = max(xf, i);
			ys = min(ys, j);
			yf = max(yf, j);
		}
	}

	s = max(xf - xs, yf - ys);
	++s;

	if (xf - xs == yf - ys) {
		x = xs;
		y = ys;
	} else if (xf - xs > yf - ys) {
		x = xs;
		for (int j = max(yf - s + 1, 0); j <= min(ys + s - 1, c - 1); j++)
			if (check(x, j, s, n)) {
				y = j;
				break;
			}
	} else {
		y = ys;
		for (int i = max(xf - s + 1, 0); i <= min(xs + s - 1, r - 1); i++)
			if (check(i, y, s, n)) {
				x = i;
				break;
			}
	}

	printf("%d %d %d\n", x + 1, y + 1, s);
}
#endif
}
