using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 21
이름 : 배성훈
내용 : Counting portal
    문제번호 : 32938번

    dp 문제다.
    누적합을 이용해 해결했다.

    아이디어는 다음과 같다.
    누적합을 이용하면 해당 사각형이 가능한지 여부는 O(1)에 확인 가능하다.
    r1, r2를 설정하고 col을 두 포인터로 확인하면 O(N^2 * M)의 시간에 해결가능하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1210
    {

        static void Main1210(string[] args)
        {

            int NOT = 500;
            int sizeR = 4, sizeC = 3;
            int row, col;
            int[][] board, sum;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void SetArr()
            {

                sum = new int[row + 1][];
                sum[0] = new int[col + 1];
                for (int r = 1; r <= row; r++)
                {

                    sum[r] = new int[col + 1];
                    for (int c = 1; c <= col; c++)
                    {

                        sum[r][c] = sum[r][c - 1] + board[r][c];
                    }
                }

                for (int c = 1; c <= col; c++)
                {

                    for (int r = 1; r <= row; r++)
                    {

                        sum[r][c] += sum[r - 1][c];
                    }
                }
            }

            void GetRet()
            {

                long ret = 0;

                for (int top = 1; top <= row; top++)
                {

                    for (int bot = top + sizeR; bot <= row; bot++)
                    {

                        ret += Cnt(top, bot);
                    }
                }

                Console.Write(ret);

                int Cnt(int _topR, int _botR)
                {

                    int ret = 0;

                    int leftC = 1;
                    int rightC = sizeC + leftC;

                    while (rightC <= col)
                    {

                        if (Chk()) rightC++;
                        else
                        {

                            ret += rightC - leftC - sizeC;
                            leftC++;
                            if (rightC - leftC < sizeC) rightC = leftC + sizeC;
                        }
                    }

                    while (rightC - leftC >= sizeC)
                    {

                        ret += rightC - leftC - sizeC;
                        leftC++;
                    }
                    return ret;

                    bool Chk()
                    {

                        return ChkInner(_topR, leftC, _botR, rightC) && ChkOutLine(_topR, leftC, _botR, rightC);
                    }
                }

                bool ChkInner(int _topR, int _leftC, int _botR, int _rightC)
                {

                    return SumArea(_topR + 1, _leftC + 1, _botR - 1, _rightC - 1) == 0;
                }

                bool ChkOutLine(int _topR, int _leftC, int _botR, int _rightC)
                {

                    // Top
                    int chk = SumArea(_topR, _leftC + 1, _topR, _rightC - 1);
                    if (chk >= NOT) return false;

                    // Left
                    chk = SumArea(_topR + 1, _leftC, _botR - 1, _leftC);
                    if (chk >= NOT) return false;

                    // Bot
                    chk = SumArea(_botR, _leftC + 1, _botR, _rightC - 1);
                    if (chk >= NOT) return false;

                    // Right
                    chk = SumArea(_topR + 1, _rightC, _botR - 1, _rightC);
                    if (chk >= NOT) return false;

                    return true;
                }

                int SumArea(int _topR, int _leftC, int _botR, int _rightC)
                    => sum[_botR][_rightC] 
                    - sum[_topR - 1][_rightC] 
                    - sum[_botR][_leftC - 1] 
                    + sum[_topR - 1][_leftC - 1];
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row + 1][];
                board[0] = new int[col + 1];
                for (int r = 1; r <= row; r++)
                {

                    board[r] = new int[col + 1];
                    for (int c = 1; c <= col; c++)
                    {
                        
                        int cur = ReadInt();
                        board[r][c] = cur == 2 ? NOT : cur;
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();

                        if (c == '\r') c = sr.Read();
                        if (c == ' ' && c != '\n') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }
                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <iostream>
using namespace std;
typedef long long ll;

int R, C; ll ans;
int O[301][301], T[301][301];

int main() {
	ios::sync_with_stdio(0);
	cin.tie(0);

	cin >> R >> C;
	for (int r = 1; r <= R; r++) {
		for (int c = 1; c <= C; c++) {
			int x; cin >> x;
			if (x == 1) O[r][c]++;
			else if (x == 2) T[r][c]++;
			O[r][c] += O[r - 1][c];
			T[r][c] += T[r - 1][c];
		}
	}
	for (int r1 = 1; r1 + 4 <= R; r1++) {
		for (int r2 = r1 + 4; r2 <= R; r2++) {
			int val = 0;
			for (int c = 1; c <= C; c++) {
				if (T[r2 - 1][c] - T[r1][c]) val = 0;
				else {
					val++;
					ans += max(val - 3, 0);
					if (T[r2][c] - T[r2 - 1][c] || T[r1][c] - T[r1 - 1][c] || O[r2 - 1][c] - O[r1][c]) val = 1;
				}
			}
		}
	}
	cout << ans;
}
#endif
}
