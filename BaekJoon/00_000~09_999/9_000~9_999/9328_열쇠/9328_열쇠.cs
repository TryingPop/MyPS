using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 7
이름 : 배성훈
내용 : 열쇠
    문제번호 : 9328번

    BFS 문제다.
    아이디어는 다음과 같다.
    최단 거리를 찾는게 아니다.
    키를 얻기 전에 문에 도착하면 해당 장소에 대기시켰다.
    그리고 키를 얻으면 해당 문을 열어 탐색했다.
    다만 키를 얻고 BFS에 넣지 않고, 문을 초기화 안해줘서 1번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1163
    {

        static void Main1163(string[] args)
        {

            // 벽과 방문은 같은 수를 둬서 재방문을 막았다.
            int WALL = -1;
            int VISIT = -1;     

            // 문을 0 ~ 26번, 키를 50 ~ 76으로 잡았기 때문에
            // 이동 가능한 칸과 문서는 100을 넘기는게 좋다.
            int MOVE = 100;
            int PAPER = 101;

            int MAX = 100;
            StreamReader sr;
            StreamWriter sw;

            Queue<(int r, int c)>[] doors;
            int row, col;
            int key;
            int[][] board;

            Queue<(int r, int c)> q;
            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Init();

                int t = int.Parse(sr.ReadLine());

                while (t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                q.Enqueue((0, 0));
                board[0][0] = VISIT;
                int ret = 0;
                while (q.Count > 0)
                {

                    var node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC] == VISIT) continue;

                        // 현재 칸 확인
                        int chk = board[nR][nC];
                        if (chk == PAPER) ret++;
                        // 재진입 방지
                        board[nR][nC] = VISIT;

                        // 문인 경우
                        if (chk < 50)
                        {

                            // 일치하는 키가 없으면 대기 장소에 저장
                            if (((1 << chk) & key) == 0)
                                doors[chk].Enqueue((nR, nC));
                            // 일치하는 키가 있으면 탐색
                            else
                                q.Enqueue((nR, nC));

                            continue;
                        }

                        // 키인 경우
                        if (chk < MOVE)
                        {

                            // 키획득하면 대기 장소를 큐에 넣어 탐색 시작시킨다.
                            chk -= 50;
                            key |= 1 << chk;
                            while (doors[chk].Count > 0)
                            {

                                q.Enqueue(doors[chk].Dequeue());
                            }
                        }

                        q.Enqueue((nR, nC));
                    }
                }

                sw.Write($"{ret}\n");
            }

            bool ChkInvalidPos(int r, int c) => r < 0 || c < 0 || r >= row + 2 || c >= col + 2;

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                board = new int[MAX + 2][];
                for (int r = 0; r < board.Length; r++)
                {

                    board[r] = new int[MAX + 2];
                    Array.Fill(board[r], MOVE);
                }

                q = new((MAX + 2) * (MAX + 2));
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                doors = new Queue<(int r, int c)>[26];
                for (int i = 0; i < 26; i++)
                {

                    doors[i] = new(MAX * MAX);
                }
            }

            void Input()
            {

                for (int i = 0; i < 26; i++)
                {

                    doors[i].Clear();
                }

                string[] inputSize = sr.ReadLine().Split();
                row = int.Parse(inputSize[0]);
                col = int.Parse(inputSize[1]);
                ReadBoard();

                string inputKey = sr.ReadLine();

                key = 0;
                if (inputKey[0] == '0') return;

                for (int i = 0; i < inputKey.Length; i++)
                {

                    key |= 1 << (inputKey[i] - 'a');
                }
            }

            void ReadBoard()
            {

                for (int r = 1; r <= row; r++)
                {

                    string str = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        int cur = str[c];

                        if (cur == '*') board[r][c + 1] = WALL;
                        else if (cur == '.') board[r][c + 1] = MOVE;
                        else if (cur == '$') board[r][c + 1] = PAPER;
                        else if (cur < 'a') board[r][c + 1] = cur - 'A';
                        else board[r][c + 1] = cur - 'a' + 50;

                    }

                    board[r][0] = MOVE;
                    board[r][col + 1] = MOVE;
                }

                for (int c = 0; c <= col + 1; c++)
                {

                    board[0][c] = MOVE;
                    board[row + 1][c] = MOVE;
                }
            }
        }
    }

#if other
// #include <stdio.h>
// #include <memory.h>
// #include <queue>
using namespace std;

struct pos
{
	int y, x;
};

int main()
{
	char ctmp, ikey[32], map[102][104];
	int n, m, itmp, T;
	struct pos c, tmp, d[4] = { {-1}, {0, 1}, {1}, {0, -1} };
	scanf("%d", &T);
	while (T)
	{
		bool akey[26] = { 0 };
		int res = 0, keys = 0, prev = -1;
		queue <struct pos> lum, door;
		memset(map, '.', sizeof(map));
		scanf("%d %d", &n, &m);
		for (int t = 1; t < n + 1; ++t)	scanf("%s", map[t] + 1);
		scanf("%s", ikey);
		if (ikey[0] != '0')
		{
			while (ikey[keys])
			{
				akey[ikey[keys] - 'a'] = 1;
				++keys;
			}
		}
		lum.push({ 0, 0 });
		while (prev != keys)
		{
			prev = keys;
			while (!lum.empty())
			{
				c = lum.front();
				lum.pop();
				for (int t = 0; t < 4; ++t)
				{
					tmp.y = c.y + d[t].y;
					tmp.x = c.x + d[t].x;
					if (tmp.y < 0 || tmp.y > n + 1 || tmp.x < 0 || tmp.x > m + 1)	continue;
					if ((ctmp = map[tmp.y][tmp.x]) == '*')	continue;
					if (ctmp >= 'A' && ctmp <= 'Z')
					{
						if (akey[ctmp - 'A'])	lum.push(tmp);
						else
						{
							door.push(tmp);
							continue;
						}
					}
					else
					{
						if (ctmp >= 'a' && ctmp <= 'z' && akey[ctmp - 'a'] == 0)
						{
							akey[ctmp - 'a'] = 1;
							++keys;
						}
						else if (ctmp == '$')	++res;
						lum.push(tmp);
					}
					map[tmp.y][tmp.x] = '*';
				}
			}
			itmp = door.size();
			while (itmp)
			{
				c = door.front();
				door.pop();
				if (akey[map[c.y][c.x] - 'A'])
				{
					map[c.y][c.x] = '*';
					lum.push(c);
				}
				else
				{
					door.push(c);
				}
				--itmp;
			}
		}
		printf("%d\n", res);
		--T;
	}
	return 0;
}
#endif
}
