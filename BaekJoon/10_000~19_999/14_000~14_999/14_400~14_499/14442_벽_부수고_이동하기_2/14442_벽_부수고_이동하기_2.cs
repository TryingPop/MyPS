using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 4
이름 : 배성훈
내용 : 벽 부수고 이동하기 2
    문제번호 : 14442번

    BFS 문제다
    크기를 확장해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_1025
    {

        static void Main1025(string[] args)
        {

            int MAX = 10_000_000;
            StreamReader sr;
            int[][] board;
            int[][][] move;

            bool[][][] visit;
            int row, col, k;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void GetRet()
            {

                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                Queue<(int k, int r, int c)> q = new(k * row * col);
                q.Enqueue((0, 0, 0));
                visit[0][0][0] = true;
                move[0][0][0] = 1;

                while (q.Count > 0) 
                {

                    var node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC)) continue;
                        if (board[nR][nC] == 1)
                        {

                            if (node.k == k || visit[node.k + 1][nR][nC]) continue;
                            move[node.k + 1][nR][nC] = move[node.k][node.r][node.c] + 1;
                            visit[node.k + 1][nR][nC] = true;
                            q.Enqueue((node.k + 1, nR, nC));
                        }
                        else
                        {

                            if (visit[node.k][nR][nC]) continue;
                            move[node.k][nR][nC] = move[node.k][node.r][node.c] + 1;
                            visit[node.k][nR][nC] = true;
                            q.Enqueue((node.k, nR, nC));
                        }
                    }
                }

                int ret = MAX;
                for (int i = 0; i <= k; i++)
                {

                    if (visit[i][row - 1][col - 1])
                        ret = Math.Min(ret, move[i][row - 1][col - 1]);
                }

                if (ret == MAX) ret = -1;
                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();
                k = ReadInt();

                move = new int[k + 1][][];
                visit = new bool[k + 1][][];
                for (int i = 0; i <= k; i++)
                {

                    move[i] = new int[row][];
                    visit[i] = new bool[row][];
                    for (int r = 0; r < row; r++)
                    {

                        move[i][r] = new int[col];
                        visit[i][r] = new bool[col];
                    }
                }

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = sr.Read() - '0';
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
namespace Baekjoon;

public class Program
{
    private static void Main(string[] args)
    {
        var input = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
        int rowSize = input[0], colSize = input[1], breakChance = input[2];
        var map = new string[rowSize];
        for (int i = 0; i < rowSize; i++)
        {
            map[i] = Console.ReadLine()!;
        }

        var curPos = new Queue<ValueTuple<int, int>>();
        var nextPos = new Queue<ValueTuple<int, int>>();
        curPos.Enqueue((0, 0));
        var dirs = new ValueTuple<int, int>[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
        var lastChances = new int[rowSize, colSize];
        var visitCurDist = new bool[rowSize, colSize];
        for (int r = 0; r < rowSize; r++)
        {
            for (int c = 0; c < colSize; c++)
            {
                lastChances[r, c] = -1;
            }
        }
        lastChances[0, 0] = breakChance;
        var distance = 0;
        do
        {
            distance++;
            do
            {
                (var r, var c) = curPos.Dequeue();
                if ((r, c) == (rowSize - 1, colSize - 1))
                {
                    Console.Write(distance);
                    return;
                }
                visitCurDist[r, c] = false;

                foreach ((var ar, var ac) in dirs)
                {
                    (var nr, var nc) = (r + ar, c + ac);
                    if (!(0 <= nr && nr < rowSize && 0 <= nc && nc < colSize))
                        continue;
                    int nextChance = -1;
                    if (map[nr][nc] == '1' && lastChances[r, c] > 0)
                        nextChance = lastChances[r, c] - 1;
                    else if (map[nr][nc] == '0')
                        nextChance = lastChances[r, c];

                    if (lastChances[nr, nc] < nextChance)
                    {
                        if (!visitCurDist[nr, nc])
                        {
                            nextPos.Enqueue((nr, nc));
                            visitCurDist[nr, nc] = true;
                        }
                        lastChances[nr, nc] = nextChance;
                    }
                }
            }
            while (curPos.Count > 0);
            (curPos, nextPos) = (nextPos, curPos);
        } while (curPos.Count > 0);
        Console.Write("-1");
    }
}

#elif other2
// #pragma GCC optimize("Ofast")
// #include <iostream>
using namespace std;

class Point {
public:
	short y;
	short x;
	char breakWall;
};

char board__[1003 * 1003];
char *board_[1003];
char **board;
char vst__[1003 * 1003];
char *vst_[1003];
char **vst;

constexpr long long QSIZE = 1 << 19;
Point q[QSIZE + 1];
long long qLeft;
long long qRight;
long long qFRight;

int main() {

	ios_base::sync_with_stdio(false);
	cin.tie(0);
	long long i, j;

	board = board_ + 1;
	vst = vst_ + 1;
	for (i = 0; i < 1003; i++) {
		board_[i] = board__ + 1 + i * 1003;
		vst_[i] = vst__ + 1 + i * 1003;
	}

	long long n, m, k;
	cin >> n >> m >> k;

	for (long long ni = 0; ni < n; ni++) {
		cin >> board[ni];
	}

	for (i = 0; i < n; i++) {
		board[i][-1] = '1';
		vst[i][-1] = 99;
		board[i][m] = '1';
		vst[i][m] = 99;
	}
	for (j = 0; j < m; j++) {
		board[-1][j] = '1';
		vst[-1][j] = 99;
		board[n][j] = '1';
		vst[n][j] = 99;
	}

	q[0].y = 0;
	q[0].x = 0;
	q[0].breakWall = k + 1;

	qLeft = 0;
	qRight = 1;

	long long d;
	long long dy[4] = {0, 0, 1, -1};
	long long dx[4] = {1, -1, 0, 0};

	long long depth = 0;

	while (qLeft != qRight) {

		depth++;
		qFRight = qRight;

		while (qLeft != qFRight) {

			long long y = q[qLeft].y;
			long long x = q[qLeft].x;
			long long breakWall = q[qLeft].breakWall;

			if (y == n - 1 && x == m - 1) {
				cout << depth;
				return 0;
			}

			if (breakWall > 0) {
				for (d = 0; d < 4; d++) {

					long long newY = y + dy[d];
					long long newX = x + dx[d];

					if (board[newY][newX] == '0') {
						if (vst[newY][newX] < breakWall) {
							vst[newY][newX] = breakWall;
							q[qRight].y = newY;
							q[qRight].x = newX;
							q[qRight].breakWall = breakWall;
							qRight++;
							qRight &= ~QSIZE;
						}
					} else {
						if (vst[newY][newX] < breakWall - 1) {
							vst[newY][newX] = breakWall - 1;
							q[qRight].y = newY;
							q[qRight].x = newX;
							q[qRight].breakWall = breakWall - 1;
							qRight++;
							qRight &= ~QSIZE;
						}
					}

				}
			} else {
				for (d = 0; d < 4; d++) {

					long long newY = y + dy[d];
					long long newX = x + dx[d];

					if (board[newY][newX] == '0') {
						if (vst[newY][newX] < breakWall) {
							vst[newY][newX] = breakWall;
							q[qRight].y = newY;
							q[qRight].x = newX;
							q[qRight].breakWall = breakWall;
							qRight++;
							qRight &= ~QSIZE;
						}
					}

				}
			}
			
			qLeft++;
			qLeft &= ~QSIZE;

		}

	}

	cout << "-1";

}
#endif
}
