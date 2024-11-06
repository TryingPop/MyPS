using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 28
이름 : 배성훈
내용 : 2048 (Hard)
    문제번호 : 12094번

    구현, 시뮬레이션, 백트래킹 문제다
    Easy처럼 함수 구현했다가 시간초과 엄청 발생했다
    그래서 이동과 합치는 것을 한 번에 진행되게 바꿨다

    또한, C#으로 너무 느려 C++로 먼저 맞추고 코드를 C#에 맞춰 바꿨다
    시간은 4배가까이 차이난다;

    주된 아이디어는 탈출 조건이다
    모양이 같은 경우 탐색 정지한다 그리고 최대값은 잘해야 매번 2배씩 증가 가능하기에 
    남은 횟수가 n일 때 2^n * 현재 최대값 < 전체 최대값인 경우 해당 경우 탐색할 의미가 없어 탈출해줘야한다
*/

namespace BaekJoon.etc
{
    internal class etc_0735
    {

        static void Main735(string[] args)
        {

            StreamReader sr;
            int[][,] board;
            int n;
            int ret;

            int[] powTwo;

            Solve();

            void Solve()
            {

                Input();

                DFS();

                Console.WriteLine(ret);
            }

            void Operator(int _idx, int _op)
            {

                switch (_op)
                {

                    case 1:
                        U(_idx);
                        return;

                    case 2:
                        D(_idx);
                        return;

                    case 3:
                        L(_idx);
                        return;

                    case 4:
                        R(_idx);
                        return;

                    default:
                        return;
                }
            }

            void Copy(int _idx)
            {

                int[,] before = board[_idx - 1];
                int[,] cur = board[_idx];
                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        cur[r, c] = before[r, c];
                    }
                }
            }

            void U(int _idx)
            {

                int[,] cur = board[_idx];
                for (int c = 0; c < n; c++)
                {

                    int bVal = -1;
                    int rIdx = -1;
                    for (int r = 0; r < n; r++)
                    {

                        int val = cur[r, c];
                        if (val == 0) continue;

                        if (bVal == val)
                        {

                            cur[rIdx, c] = 2 * val;
                            bVal = -1;
                        }
                        else
                        {

                            cur[++rIdx, c] = val;
                            bVal = val;
                        }
                    }

                    for (int r = rIdx + 1; r < n; r++)
                    {

                        cur[r, c] = 0;
                    }
                }
            }

            void D(int _idx)
            {

                int[,] cur = board[_idx];
                for (int c = 0; c < n; c++)
                {

                    int bVal = -1;
                    int rIdx = n;
                    for (int r = n - 1; r >= 0; r--)
                    {

                        int val = cur[r, c];
                        if (val == 0) continue;
                        
                        if (bVal == val)
                        {

                            cur[rIdx, c] = 2 * val;
                            bVal = -1;
                        }
                        else
                        {

                            cur[--rIdx, c] = val;
                            bVal = val;
                        }
                    }

                    for (int r = rIdx - 1; r >= 0; r--)
                    {

                        cur[r, c] = 0;
                    }
                }
            }

            void L(int _idx)
            {

                int[,] cur = board[_idx];
                for (int r = 0; r < n; r++)
                {

                    int bVal = -1;
                    int cIdx = -1;
                    for (int c = 0; c < n; c++)
                    {

                        int val = cur[r, c];
                        if (val == 0) continue;

                        if (bVal == val)
                        {

                            cur[r, cIdx] = 2 * val;
                            bVal = -1;
                        }
                        else
                        {

                            cur[r, ++cIdx] = val;
                            bVal = val;
                        }
                    }

                    for (int c = cIdx + 1; c < n; c++)
                    {

                        cur[r, c] = 0;
                    }
                }
            }

            void R(int _idx)
            {

                int[,] cur = board[_idx];
                for (int r = 0; r < n; r++)
                {

                    int bVal = -1;
                    int cIdx = n;
                    for (int c = n - 1; c >= 0; c--)
                    {

                        int val = cur[r, c];
                        if (val == 0) continue;
                        if (bVal == val)
                        {

                            cur[r, cIdx] = 2 * val;
                            bVal = -1;
                        }
                        else
                        {

                            cur[r, --cIdx] = val;
                            bVal = val;
                        }
                    }
                    
                    for (int c = cIdx - 1; c >= 0; c--)
                    {

                        cur[r, c] = 0;
                    }
                }
            }

            bool ChkSame(int _idx)
            {

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        if (board[_idx - 1][r, c] == board[_idx][r, c]) continue;
                        return false;
                    }
                }

                return true;
            }

            bool ChkStop(int _depth, int _val)
            {

                int r = 11 - _depth;
                int chk = _val * powTwo[r];
                return chk <= ret;
            }

            void DFS(int _depth = 1)
            {

                int[,] curBoard = board[_depth];
                int curMax = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        curBoard[i, j] = board[_depth - 1][i, j];
                        if (curMax < curBoard[i, j]) curMax = curBoard[i, j];
                    }
                }

                if (ret < curMax) ret = curMax;

                if (_depth > 10 || ChkStop(_depth, curMax)) return;

                for (int i = 1; i <= 4; i++)
                {

                    Operator(_depth, i);

                    if (ChkSame(_depth)) continue;
                    
                    DFS(_depth + 1);
                    Copy(_depth);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                board = new int[12][,];
                for (int i = 0; i < 12; i++)
                {

                    board[i] = new int[n, n];
                }

                ret = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        int cur = ReadInt();
                        board[0][i, j] = cur;
                        ret = Math.Max(ret, cur);
                    }
                }

                powTwo = new int[11];
                powTwo[0] = 1;
                for (int i = 1; i <= 10; i++)
                {

                    powTwo[i] = powTwo[i - 1] * 2;
                }
                sr.Close();
            }

            int ReadInt()
            {
                int c, ret = 0;
                while((c= sr.Read()) != -1 && c!= ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if first
// #include <iostream>
// #define MAX_SIZE 20
// #define TRIALS 10

using namespace std;

int N;
int board[MAX_SIZE][MAX_SIZE];
int ret = 0;
int pow2[11] = { 1, 2, 4, 8, 16, 32 ,64, 128, 256, 512, 1024 };

void Copy(int temp[][MAX_SIZE])
{

	for (int r = 0; r < N; r++) {

		for (int c = 0; c < N; c++)
		{

			board[r][c] = temp[r][c];
		}
	}
}

void U()
{

	for (int c = 0; c < N; c++)
	{

		int bVal = -1;
		int rIdx = -1;
		for (int r = 0; r < N; r++)
		{

			int val = board[r][c];
			if (!val) continue;

			if (bVal == val) 
			{

				board[rIdx][c] = 2 * val;
				bVal = -1;
			}
			else 
			{

				board[++rIdx][c] = val;
				bVal = val;
			}
		}

		for (int r = rIdx + 1; r < N; r++)
		{

			board[r][c] = 0;
		}
	}
}

void D()
{

	for (int c = 0; c < N; c++)
	{

		int bVal = -1;
		int rIdx = N;
		for (int r = N - 1; r >= 0; r--)
		{

			int val = board[r][c];
			if (!val) continue;
			if (bVal == val) 
			{

				board[rIdx][c] = 2 * val;
				bVal = -1;
			}
			else 
			{

				board[--rIdx][c] = val;
				bVal = val;
			}
		}

		for (int r = rIdx - 1; r >= 0; r--)
		{

			board[r][c] = 0;
		}
	}
}

void L()
{

	for (int r = 0; r < N; r++)
	{

		int bVal = -1;
		int cIdx = -1;
		for (int c = 0; c < N; c++)
		{

			int val = board[r][c];
			if (!val) continue;
			if (bVal == val) 
			{

				board[r][cIdx] = 2 * val;
				bVal = -1;
			}
			else 
			{

				board[r][++cIdx] = val;
				bVal = val;
			}
		}

		for (int c = cIdx + 1; c < N; c++) 
		{

			board[r][c] = 0;
		}
	}
}

void R()
{

	for (int r = 0; r < N; r++)
	{

		int bVal = -1;
		int cIdx = N;
		for (int c = N - 1; c >= 0; c--)
		{

			int val = board[r][c];
			if (!val) continue;
			if (bVal == val)
			{

				board[r][cIdx] = 2 * val;
				bVal = -1;
			}
			else 
			{

				board[r][--cIdx] = val;
				bVal = val;
			}
		}

		for (int c = cIdx - 1; c >= 0; c--)
		{

			board[r][c] = 0;
		}
	}
}

void Operator(int _op)
{

	switch (_op)
	{

	case 1:
		U();
		return;

	case 2:
		D();
		return;

	case 3:
		L();
		return;

	case 4:
		R();
		return;

	default:
		return;
	}
}

bool ChkSame(int temp[][MAX_SIZE])
{

	for (int r = 0; r < N; r++)
	{

		for (int c = 0; c < N; c++)
		{

			if (board[r][c] == temp[r][c]) continue;
			return false;
		}
	}

	return true;
}

bool ChkStop(int _depth, int _val)
{

	int r = 10 - _depth;
	int chk = _val * pow2[r];

	return chk <= ret;
}

void DFS(int _depth)
{

	int temp[MAX_SIZE][MAX_SIZE];

	int curMax = 0;
	for (int i = 0; i < N; i++)
	{

		for (int j = 0; j < N; j++)
		{

			temp[i][j] = board[i][j];
			if (curMax < temp[i][j]) curMax = temp[i][j];
		}
	}

	if (ret < curMax) ret = curMax;

	if (_depth >= TRIALS) return;

	if (ChkStop(_depth, curMax)) return;

	for (int i = 1; i <= 4; i++)
	{

		Operator(i);
		if (ChkSame(temp)) continue;

		DFS(_depth + 1);
		Copy(temp);
	}
}

int main() 
{

	ios::sync_with_stdio(false); 
	cin.tie(nullptr);
	cout.tie(NULL);

	cin >> N;
	for (int i = 0; i < N; i++)
	{

		for (int j = 0; j < N; j++)
		{

			cin >> board[i][j];
			if (board[i][j] > ret) ret = board[i][j];
		}
	}

	DFS(0);

	cout << ret;
	return 0;
}
#endif



#if other
// #include <stdio.h>
// #include <vector>
// #include <algorithm>
// #include <queue>
// #include <math.h>
// #include <string.h>
// #include <map>
// #include <set>
// #include <stack>
using namespace std;

int N;
int A[21][21];
int max_result, result;

//0: U, 1: R, 2: D, 3: L

int func(int arr[][21], int prev_turn, bool score_changed, int turn_left, int now_score)
{
    /*for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            printf("%d ", arr[i][j]);
        }
        printf("\n");
    }
    printf("----------------------\n");*/
    result = max(result, now_score);
    if (turn_left == 0)
    {
        return 0;
    }
    if ((1 << (turn_left)) * now_score <= result)
    {
        return 0;
    }
    if (max_result == result)
    {
        return 1;
    }
    int B[4][21][21] = {0};

    if (prev_turn % 2 == 1 || score_changed)
    {
        bool new_score_changed = false;
        bool something_changed = false;
        int new_now_score = now_score;
        for (int i = 0; i < N; i++)
        {
            int index = 0;
            int pre = 0;
            for (int j = 0; j < N; j++)
            {
                if (arr[j][i])
                {
                    if (pre == arr[j][i])
                    {
                        B[0][index - 1][i] *= 2;
                        new_now_score = max(new_now_score, B[0][index - 1][i]);
                        pre = 0;
                        new_score_changed = true;
                        something_changed = true;
                    }
                    else
                    {
                        if (index != j)
                        {
                            something_changed = true;
                        }
                        B[0][index++][i] = arr[j][i];
                        pre = arr[j][i];
                    }
                }
            }
        }
        if (something_changed && func(B[0], 0, new_score_changed, turn_left - 1, new_now_score))
        {
            return 1;
        }
    }
    if (prev_turn % 2 == 0 || score_changed)
    {
        bool new_score_changed = false;
        bool something_changed = false;
        int new_now_score = now_score;
        for (int i = 0; i < N; i++)
        {
            int index = N - 1;
            int pre = 0;
            for (int j = N - 1; j >= 0; j--)
            {
                if (arr[i][j])
                {
                    if (pre == arr[i][j])
                    {
                        B[1][i][index + 1] *= 2;
                        new_now_score = max(new_now_score, B[1][i][index + 1]);
                        pre = 0;
                        new_score_changed = true;
                        something_changed = true;
                    }
                    else
                    {
                        if (index != j)
                        {
                            something_changed = true;
                        }
                        B[1][i][index--] = arr[i][j];
                        pre = arr[i][j];
                    }
                }
            }
        }
        if (something_changed && func(B[1], 1, new_score_changed, turn_left - 1, new_now_score))
        {
            return 1;
        }
    }
    if (prev_turn % 2 == 1 || score_changed)
    {
        bool new_score_changed = false;
        bool something_changed = false;
        int new_now_score = now_score;
        for (int i = 0; i < N; i++)
        {
            int index = N - 1;
            int pre = 0;
            for (int j = N - 1; j >= 0; j--)
            {
                if (arr[j][i])
                {
                    if (pre == arr[j][i])
                    {
                        B[2][index + 1][i] *= 2;
                        new_now_score = max(new_now_score, B[2][index + 1][i]);
                        pre = 0;
                        new_score_changed = true;
                        something_changed = true;
                    }
                    else
                    {
                        if (index != j)
                        {
                            something_changed = true;
                        }
                        B[2][index--][i] = arr[j][i];
                        pre = arr[j][i];
                    }
                }
            }
        }
        if (something_changed && func(B[2], 2, new_score_changed, turn_left - 1, new_now_score))
        {
            return 1;
        }
    }
    if (prev_turn % 2 == 0 || score_changed)
    {
        bool new_score_changed = false;
        bool something_changed = false;
        int new_now_score = now_score;
        for (int i = 0; i < N; i++)
        {
            int index = 0;
            int pre = 0;
            for (int j = 0; j < N; j++)
            {
                if (arr[i][j])
                {
                    if (pre == arr[i][j])
                    {
                        B[3][i][index - 1] *= 2;
                        new_now_score = max(new_now_score, B[3][i][index - 1]);
                        pre = 0;
                        new_score_changed = true;
                        something_changed = true;
                    }
                    else
                    {
                        if (index != j)
                        {
                            something_changed = true;
                        }
                        B[3][i][index++] = arr[i][j];
                        pre = arr[i][j];
                    }
                }
            }
        }
        if (something_changed && func(B[3], 3, new_score_changed, turn_left - 1, new_now_score))
        {
            return 1;
        }
    }
    return 0;
}

int main(void)
{
    scanf("%d", &N);
    int sum = 0;
    int _max = 0;
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            scanf("%d", &A[i][j]);
            sum += A[i][j];
            _max = max(_max, A[i][j]);
        }
    }
    for (int i = 18; i >= 0; i--)
    {
        if (sum & (1 << i))
        {
            max_result = (1 << i);
            break;
        }
    }
    func(A, -1, true, 10, _max);
    printf("%d\n", result);
}
#elif other2
// #include<iostream>
// #include<algorithm>
// #include<cstring>

using namespace std;

int map[20][20];
int n;
int maxcnt;

void right(int cnt);
void left(int cnt);
void up(int cnt);
void down(int cnt);

void right(int cnt)
{
	int cmax = 0;
	bool shifted = false;
	for (int i = 0; i < n; i++)
	{
		int zidx = -1, cur = -1, curidx;
		for (int j = n - 1; j >= 0; j--)
		{
			if (map[i][j] != 0)
			{
				if (map[i][j] == cur)
				{
					map[i][curidx] *= 2;
					map[i][j] = 0;
					cur = -1;
					zidx = max(j, zidx);

					cmax = max(cmax, map[i][curidx]);
					shifted = true;
				}
				else if (zidx == -1)
				{
					cmax = max(cmax, map[i][j]);
					cur = map[i][j];
					curidx = j;
				}
				else
				{
					shifted = true;
					cmax = max(cmax, map[i][j]);

					map[i][zidx] = map[i][j];
					cur = map[i][j];
					curidx = zidx--;
					map[i][j] = 0;
				}
			}
			else if (j > zidx)
				zidx = j;
		}
	}

	if (cmax <= maxcnt >> (10 - cnt) || !shifted)
		return;

	if (cnt == 10)
	{
		maxcnt = cmax;
		return;
	}

	int pre[20][20];
	memcpy(pre, map, sizeof(pre));

	right(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	left(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	up(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	down(cnt + 1);
	memcpy(map, pre, sizeof(pre));
}

void left(int cnt)
{
	bool shifted = false;
	int cmax = 0;
	for (int i = 0; i < n; i++)
	{
		int zidx = 20, cur = -1, curidx;
		for (int j = 0; j < n; j++)
		{
			if (map[i][j] != 0)
			{
				if (map[i][j] == cur)
				{
					map[i][curidx] *= 2;
					map[i][j] = 0;
					cur = -1;
					zidx = min(j, zidx);

					cmax = max(cmax, map[i][curidx]);
					shifted = true;
				}
				else if (zidx == 20)
				{
					cmax = max(cmax, map[i][j]);
					cur = map[i][j];
					curidx = j;
				}
				else
				{
					shifted = true;
					cmax = max(cmax, map[i][j]);

					map[i][zidx] = map[i][j];
					cur = map[i][j];
					curidx = zidx++;
					map[i][j] = 0;
				}
			}
			else if (j < zidx)
				zidx = j;
		}
	}

	if (cmax <= maxcnt >> (10 - cnt) || !shifted)
		return;

	if (cnt == 10)
	{
		maxcnt = cmax;
		return;
	}

	int pre[20][20];
	memcpy(pre, map, sizeof(pre));

	right(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	left(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	up(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	down(cnt + 1);
	memcpy(map, pre, sizeof(pre));
}

void up(int cnt)
{
	bool shifted = false;
	int cmax = 0;
	for (int j = 0; j < n; j++)
	{
		int zidx = 20, cur = -1, curidx;
		for (int i = 0; i < n; i++)
		{
			if (map[i][j] != 0)
			{
				if (map[i][j] == cur)
				{
					map[curidx][j] *= 2;
					map[i][j] = 0;
					cur = -1;
					zidx = min(i, zidx);

					cmax = max(cmax, map[curidx][j]);
					shifted = true;
				}
				else if (zidx == 20)
				{
					cmax = max(cmax, map[i][j]);
					cur = map[i][j];
					curidx = i;
				}
				else
				{
					shifted = true;
					cmax = max(cmax, map[i][j]);

					map[zidx][j] = map[i][j];
					cur = map[i][j];
					curidx = zidx++;
					map[i][j] = 0;
				}
			}
			else if (i < zidx)
				zidx = i;
		}
	}

	if (cmax <= maxcnt >> (10 - cnt) || !shifted)
		return;

	if (cnt == 10)
	{
		maxcnt = cmax;
		return;
	}

	int pre[20][20];
	memcpy(pre, map, sizeof(pre));

	right(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	left(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	up(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	down(cnt + 1);
	memcpy(map, pre, sizeof(pre));
}

void down(int cnt)
{
	bool shifted = false;
	int cmax = 0;
	for (int j = 0; j < n; j++)
	{
		int zidx = -1, cur = -1, curidx;
		for (int i = n - 1; i >= 0; i--)
		{
			if (map[i][j] != 0)
			{
				if (map[i][j] == cur)
				{
					map[curidx][j] *= 2;
					map[i][j] = 0;
					cur = -1;
					zidx = max(i, zidx);

					cmax = max(cmax, map[curidx][j]);
					shifted = true;
				}
				else if (zidx == -1)
				{
					cmax = max(cmax, map[i][j]);
					cur = map[i][j];
					curidx = i;
				}
				else
				{
					shifted = true;
					cmax = max(cmax, map[i][j]);

					map[zidx][j] = map[i][j];
					cur = map[i][j];
					curidx = zidx--;
					map[i][j] = 0;
				}
			}
			else if (i > zidx)
				zidx = i;
		}
	}

	if (cmax <= maxcnt >> (10 - cnt) || !shifted)
		return;

	if (cnt == 10)
	{
		maxcnt = cmax;
		return;
	}

	int pre[20][20];
	memcpy(pre, map, sizeof(pre));

	right(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	left(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	up(cnt + 1);
	memcpy(map, pre, sizeof(pre));

	down(cnt + 1);
	memcpy(map, pre, sizeof(pre));
}

void solve()
{
	int pre[20][20];
	memcpy(pre, map, sizeof(pre));

	right(1);
	memcpy(map, pre, sizeof(pre));

	left(1);
	memcpy(map, pre, sizeof(pre));

	up(1);
	memcpy(map, pre, sizeof(pre));

	down(1);
}

int main()
{
	ios::sync_with_stdio(false);
	cin.tie(NULL);

	cin >> n;
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
		{
			cin >> map[i][j];
			maxcnt = max(maxcnt, map[i][j]);
		}
	}
	solve();
	cout << maxcnt;
}
#endif
}
