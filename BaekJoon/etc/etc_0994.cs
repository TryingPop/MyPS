using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 27
이름 : 배성훈
내용 : 마법사 상어와 블리자드
    문제번호 : 21611번

    구현, 시뮬레이션 문제다
    처음에는 2차원으로 접근했는데, 여러 가지 고려 못해
    계속해서 틀렸다

    이후 1차원으로 접근해서 풀었다
    여기서 입력 받을 때 0을 무시해 board에 idx가 저장안되어 문제가 생겼다
    이후 해당 부분 수정하니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_0994
    {

        static void Main994(string[] args)
        {

            StreamReader sr;

            int n, k;

            int[] arr, next;
            int ret1, ret2, ret3;
            int[][] board;
            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                Simul();

                Console.Write(ret1 + ret2 * 2 + ret3 * 3);
            }

            void Simul()
            {

                next = new int[n * n + 1];
                dirR = new int[5] { 0, -1, 1, 0, 0 };
                dirC = new int[5] { 0, 0, 0, -1, 1 };

                ret1 = 0;
                ret2 = 0;
                ret3 = 0;

                int mid = n >> 1;
                int len = n * n;

                int d, s;

                for (int i = 0; i < k; i++)
                {

                    d = ReadInt();
                    s = ReadInt();

                    Shot();
                    Move();

                    while (ChkBoom())
                    {

                        Move();
                    }

                    Cnt();
                }

                void Shot()
                {

                    int r = mid;
                    int c = mid;

                    for (int i = 0; i < s; i++)
                    {

                        r += dirR[d];
                        c += dirC[d];

                        int idx = board[r][c];
                        arr[idx] = 0;
                    }
                }

                void Swap()
                {

                    var temp = next;
                    next = arr;
                    arr = temp;
                }

                void Move()
                {

                    int idx = 1;
                    for (int i = 1; i < len; i++)
                    {

                        if (arr[i] == 0) continue;
                        next[idx++] = arr[i];
                    }

                    for (int i = idx; i < len; i++)
                    {

                        next[i] = 0;
                    }

                    Swap();
                }

                bool ChkBoom()
                {

                    int cnt = 0;
                    int cur = 0;
                    int idx = 1;

                    bool ret = false;
                    for (int i = 1; i < arr.Length; i++)
                    {

                        if (cur != arr[i])
                        {

                            if (cnt > 3)
                            {

                                ret = true;
                                if (cur == 1) ret1 += cnt;
                                else if (cur == 2) ret2 += cnt;
                                else if (cur == 3) ret3 += cnt;
                                

                                for (int j = 0; j < cnt; j++)
                                {

                                    arr[j + idx] = 0; 
                                }
                            }

                            cur = arr[i];
                            idx = i;
                            cnt = 0;
                        }

                        cnt++;
                    }

                    return ret;
                }

                void Cnt()
                {

                    int cnt = 0;
                    int cur = 0;
                    int idx = 1;

                    for (int i = 1; i < arr.Length; i++)
                    {

                        if (len <= idx) break;
                        if (arr[i] != cur)
                        {

                            if (cnt > 0)
                            {

                                next[idx++] = cnt;
                                next[idx++] = cur;
                            }

                            if (arr[i] == 0) break;
                            cur = arr[i];
                            cnt = 0;
                        }

                        cnt++;
                    }

                    for (int i = idx; i < len; i++)
                    {

                        next[i] = 0;
                    }

                    Swap();
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                arr = new int[n * n + 1];
                board = new int[n][];

                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        int idx = CalcIdx(r, c);
                        board[r][c] = idx;

                        int cur = ReadInt();
                        if (cur == 0) continue;
                        arr[idx] = cur;
                    }
                }
            }

            int CalcIdx(int _r, int _c)
            {

                int mid = n >> 1;
                int ret;

                if (_r <= mid)
                {

                    if (_c < _r) 
                    {

                        ret = 2 * (mid - _c) - 1;
                        ret *= ret;
                        ret += _r - _c - 1;
                    }
                    else if (_c < n - _r)
                    {

                        ret = 2 * (mid - _r) + 1;
                        ret *= ret;
                        ret += _r - 1 - _c;
                    }
                    else
                    {

                        ret = (_c - mid) << 1;
                        ret = ret * (ret + 1);
                        ret -= _r - n + 1 + _c;
                    }
                }
                else
                {

                    if (_r < _c)
                    {

                        ret = (_c - mid) << 1;
                        ret *= ret;
                        ret -= _r - _c;
                    }
                    else if (_c < n - _r)
                    {

                        ret = (mid - _c) << 1;
                        ret = ret * (ret - 1);
                        ret += _r - n + 1 + _c;
                    }
                    else
                    {

                        ret = (_r - mid) << 1;
                        ret *= ret;
                        ret -= _r - _c;
                    }
                }

                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
int[] line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
int N = line[0];
int M = line[1];

int[,] idxtable = new int[N, N];
CreateIdxTable(idxtable);

List<int> queue = new List<int>();
for (int i = 0; i < N * N - 1; i++) queue.Add(0);
for(int i = 0; i < N; i++)
{
    line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
    for (int j = 0; j < N; j++)
        if (line[j] != 0)
            queue[idxtable[i, j]] = line[j];
}

// 빈 구슬 제거
for(int i = queue.Count-1; i >= 0; i--)
{
    if (queue[i] == 0) queue.RemoveAt(i);
    else break;
}

//Print(queue, "Init");

long score = 0;
for (int t = 0; t < M; t++)
{
    line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
    Blizzard(line[0], line[1]);
    //Print(queue, "Blizzard " + (t + 1).ToString());

    while (PopLinks())
    {
        //Print(queue, "POP");
        while (PopLinks()) ;// Print(queue, "POP");
        RemoveZero();
        //Print(queue, "RemoveZero");
    }

    Transition();
    //Print(queue, "Transition " + (t + 1).ToString());
}

Console.WriteLine(score);


return;

void DoNothing() { }

void Transition()
{
    if (queue.Count == 0)
        return;

    List<int> transition = new List<int>();
    int ball = queue[0];
    int count = 0;
    int idx;
    for(idx = 0; idx < queue.Count; idx++)
    {
        if (ball == queue[idx])
            count++;
        else
        {
            transition.Add(count);
            transition.Add(ball);
            ball = queue[idx];
            count = 1;
        }

        if (transition.Count > N * N)
            break;
    }
    transition.Add(count);
    transition.Add(ball);
    ball = queue[idx - 1];
    count = 1;

    while (transition.Count > N * N - 1)
        transition.RemoveAt(transition.Count - 1);

    queue = transition;
}

void RemoveZero()
{
    for(int idx = queue.Count-1; idx >= 0; idx--)
        if (queue[idx] == 0)
            queue.RemoveAt(idx);
}

bool PopLinks()
{
    if (queue.Count == 0)
        return false;

    int idx = 0;
    int ball = queue[0];
    int count = 0;
    while(idx < queue.Count)
    {
        if (queue[idx] == 0)
        {
            ball = queue[idx];
            idx++;
            count = 0;
            continue;
        }
        if (queue[idx] == ball)
        {
            count++;
            idx++;
            continue;
        }
        if (count>= 4)
        {
            for (int i = 0; i < count; i++)
            {
                idx--;
                score += queue[idx];
                //queue.RemoveAt(idx);
                queue[idx] = 0;
            }
            idx++;
            return true;
        }
        ball = queue[idx];
        count = 0;
    }
    if (count >= 4)
    {
        for (int i = 0; i < count; i++)
        {
            idx--;
            score += queue[idx];    
            //queue.RemoveAt(idx);
            queue[idx] = 0;
        }
        idx++;
        return true;
    }

    return false;
}

void Blizzard(int d, int s)
{
    switch (d)
    {
        case 1: // 북
            for (int i = s; i > 0; i--)
            {
                if (queue.Count > idxtable[N / 2 - i, N / 2])
                    queue.RemoveAt(idxtable[N / 2 - i, N / 2]);
            }
            break;
        case 2: // 남
            for (int i = s; i > 0; i--)
            {
                if (queue.Count > idxtable[N / 2 + i, N / 2])
                    queue.RemoveAt(idxtable[N / 2 + i, N / 2]);
            }
            break;
        case 3: // 서
            for (int i = s; i > 0; i--)
            {
                if (queue.Count > idxtable[N / 2, N / 2 - i])
                    queue.RemoveAt(idxtable[N / 2, N / 2 - i]);
            }
            break;
        case 4: // 동
            for (int i = s; i > 0; i--)
            {
                if (queue.Count > idxtable[N / 2, N / 2 + i])
                    queue.RemoveAt(idxtable[N / 2, N / 2 + i]);
            }
            break;
    }
}

void CreateIdxTable(int[,] idxtable)
{
    int n = 0;
    int m = 0;
    int idx = N * N - 2;
    while (idxtable[n, m] == 0)
    {
        while (m <= N - 1 && idxtable[n, m] == 0)
            idxtable[n, m++] = idx--;
        n++;
        m--;
        while (n <= N - 1 && idxtable[n, m] == 0)
            idxtable[n++, m] = idx--;
        n--;
        m--;
        while (m >= 0 && idxtable[n, m] == 0)
            idxtable[n, m--] = idx--;
        n--;
        m++;
        while (n >= 0 && idxtable[n, m] == 0)
            idxtable[n--, m] = idx--;
        n++;
        m++;
    }
    idxtable[N / 2, N / 2 - 1] = 0;
    idxtable[N / 2, N / 2] = 0;
}

void Print(List<int> queue, string name = "")
{
    Console.WriteLine(name);
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            if (i == N / 2 && j == N / 2) Console.Write(" 0 ");
            else Console.Write("{0,2} ", queue.Count > idxtable[i, j] ? queue[idxtable[i, j]] : 0);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

#elif other2
// #include <stdio.h>

//상,하,좌,우(d : 1~4)	인덱스 만들기

//int index[52][52] ;
//int dy[] = { 0,1,0,-1 };//좌하우상
//int dx[] = { -1, 0, 1, 0 };
//
//int dIdx[4][24] = { 7, 22, 45, 76, 115, 162, 217, 280, 351, 430, 517, 612, 715, 826, 945, 1072, 1207, 1350, 1501, 1660, 1827, 2002, 2185, 2376,
//3, 14, 33, 60, 95, 138, 189, 248, 315, 390, 473, 564, 663, 770, 885, 1008, 1139, 1278, 1425, 1580, 1743, 1914, 2093, 2280,
//1, 10, 27, 52, 85, 126, 175, 232, 297, 370, 451, 540, 637, 742, 855, 976, 1105, 1242, 1387, 1540, 1701, 1870, 2047, 2232,
//5, 18, 39, 68, 105, 150, 203, 264, 333, 410, 495, 588, 689, 798, 915, 1040, 1173, 1314, 1463, 1620, 1785, 1958, 2139, 2328, };
//
//void printdIdx() {
//	for (int i = 0; i < 4; ++i) {
//		for (int j = 0; j < 24; ++j) {
//			printf("%d, ", dIdx[i][j]);
//		}
//		printf("\n");
//	}
//}
//
//int main() {
	//int start = 27;
	//int size = 1;
	//int dIdx = 0;
	//int idx = 1;
	//int y, x, ny, nx;
	//y = x = start;
	//while (1) {
	//	for (int i = 0; i < size; ++i) {
	//		ny = y + dy[dIdx];
	//		nx = x + dx[dIdx];
	//		index[ny][nx] = idx++;
	//		y = ny;
	//		x = nx;
	//	}
	//	dIdx = (dIdx + 1) % 4;
	//	for (int i = 0; i < size; ++i) {
	//		ny = y + dy[dIdx];
	//		nx = x + dx[dIdx];
	//		index[ny][nx] = idx++;
	//		y = ny;
	//		x = nx;
	//	}
	//	dIdx = (dIdx + 1) % 4;
	//	size++;
	//	if (size == 50) break;
	//}
	///*for (int i = 0; i < 51; ++i) {
	//	for (int j = 0; j < 51; ++j) {
	//		printf("index[%d][%d]: %d\n", i, j, index[i][j]);
	//	}
	//}*/
	////상 출력
	//int ii = start;
	//printf("{");
	//for (int i = 1; i < 25; ++i) {
	//	printf("%d, ", index[ii - i][start]);
	//}
	//printf("\n");
	////하 출력
	//for (int i = 1; i < 25; ++i) {
	//	printf("%d, ", index[ii + i][start]);
	//}
	//printf("\n");
	////좌 출력
	//for (int i = 1; i < 25; ++i) {
	//	printf("%d, ", index[start][ii - i]);
	//}
	//printf("\n");
	////우 출력
	//for (int i = 1; i < 25; ++i) {
	//	printf("%d, ", index[start][ii + i]);
	//}
	//printf("}");
//	printdIdx();
//	return 0;
//}

//달팽이 방향
int dy[] = { 0,1,0,-1 };
int dx[] = { -1,0,1,0 };

struct Data {
	int num, cnt;
} stk[2600];

int n, k, d, s, map[51][51], arr[2600],stkCnt, bombn[4], start;
int dIdxList[4][24] = {7, 22, 45, 76, 115, 162, 217, 280, 351, 430, 517, 612, 715, 826, 945, 1072, 1207, 1350, 1501, 1660, 1827, 2002, 2185, 2376,
3, 14, 33, 60, 95, 138, 189, 248, 315, 390, 473, 564, 663, 770, 885, 1008, 1139, 1278, 1425, 1580, 1743, 1914, 2093, 2280,
1, 10, 27, 52, 85, 126, 175, 232, 297, 370, 451, 540, 637, 742, 855, 976, 1105, 1242, 1387, 1540, 1701, 1870, 2047, 2232,
5, 18, 39, 68, 105, 150, 203, 264, 333, 410, 495, 588, 689, 798, 915, 1040, 1173, 1314, 1463, 1620, 1785, 1958, 2139, 2328, };

//달팽이 순서를 일렬 리스트로 만들기
void makeList() {
	int idx = 0;
	start = n / 2 + 1;
	int y, x, ny, nx, dIdx,size;
	y = x = start;
	size = 1;
	dIdx = 0;
	while (1) {
		for (int i = 0; i < size; ++i) {
			ny = y + dy[dIdx];
			nx = x + dx[dIdx];
			arr[++idx] = map[ny][nx];
			y = ny;
			x = nx;
		}
		dIdx = (dIdx + 1) % 4;
		for (int i = 0; i < size; ++i) {
			ny = y + dy[dIdx];
			nx = x + dx[dIdx];
			arr[++idx] = map[ny][nx];
			y = ny;
			x = nx;
		}
		dIdx = (dIdx + 1) % 4;
		size++;
		if (size > n) break;
	}
}

int main() {
	scanf("%d %d", &n, &k);
	for (int i = 1; i <= n; ++i) {
		for (int j = 1; j <= n; ++j) {
			scanf("%d", map[i] + j);
		}
	}
	//리스트 일렬로 만들기
	makeList();
	for (int i = 0; i < k; ++i) {//블리자드 리스트, 1~4를 0~3으로.상하좌우
		scanf("%d %d", &d, &s);
		d--;
		//1. 블리자드 발동!
		//해당하는 곳 0으로 처리
		for (int i = 0; i < s; ++i) {
			arr[dIdxList[d][i]] = 0;
		}
		//2. shift
		//3. 폭발
		//stack 쌓으면서 함께 처리<--이렇게 하니까 문제의 흐름과 달라지는 부분이 생겼다
		//폭발 하나씩 처리 -> 전부 shjft 순으로 계속 반복해야 한다.
		//stk에 전부쌓고 -> ( 폭발 -> shift ) 반복
		int flag = false;
		stk[0] = { -1,-1 };
		stkCnt = 1;
		//stk 쌓기
		for (int i = 1; i < n * n; ++i) {
			//top이랑 이번에 들어오는 애 숫자가 동일하나?
			if (stk[stkCnt - 1].num == arr[i]) {
				//그럼 숫자를 1 늘려 줌
				stk[stkCnt - 1].cnt++;
			}
			else if (arr[i] && stk[stkCnt - 1].num != arr[i]) {//0은 skip
				stk[stkCnt++] = { arr[i],1 };
			}
		}
		while (1) {
			flag = false;
			//폭발
			for (int i = 1; i < stkCnt; ++i) {
				if (stk[i].cnt >= 4) {
					flag = true;
					bombn[stk[i].num] += stk[i].cnt;
					stk[i] = { -1,-1 };
				}

			}
			//shift
			//orgStkCnt
			int orgStkCnt = stkCnt;
			stkCnt = 1;
			for (int i = 1; i < orgStkCnt; ++i) {
				if (stk[i].num > 0) {
					if (stk[stkCnt - 1].num == stk[i].num) {
						stk[stkCnt - 1].cnt+= stk[i].cnt;
					}
					else {
						stk[stkCnt++] = { stk[i].num,stk[i].cnt };
					}
				}
			}
				
			if (!flag) break;
		}
		
		//4. 개수,번호,개수,번호 식으로 일렬로 다시 만들기
		int idx = 1;//인덱스 1부터 만든다. 상어 다음 첫 구슬 번호를 1로 지정했으니까.
		for (int i = 1; i < stkCnt; ++i) {
			arr[idx++] = stk[i].cnt;
			arr[idx++] = stk[i].num;
		}
		for (int i = idx; i < n * n; ++i) {
			arr[i] = 0;
		}

	}
	printf("%d", bombn[1] + bombn[2] * 2 + bombn[3] * 3);
	
	
	return 0;
}


#endif
}
