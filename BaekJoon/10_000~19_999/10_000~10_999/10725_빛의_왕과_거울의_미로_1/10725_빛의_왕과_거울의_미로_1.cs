using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 11
이름 : 배성훈
내용 : 빛의 왕과 거울의 미로 1
    문제번호 : 10725번

    브루트포스, 시뮬레이션, 백트래킹 문제다.
    ? 칸이 12개를 넘지 않는다고 한다.
    그래서 ?에 각각을 대입해도 3^12 = 531_441경우다.
    그리고 시뮬레이션 돌리는 경우 거치는 경우는 많아야 2번이므로 맵의 크기 x 2인 128이고
    둘을 곱해도 7000만을 넘지 않는다.
    
    다만 모든 경우를 배정해도 되지만 진행 경로만 바꿔도 풀 수 있다.
    DFS로 진행하는데, 이동 중에 만나지 않은 ? 칸 갯수 k만큼 해당 경로의 경우의 수가 3^k 경우가 된다.
    이렇게 진행 중에만 칸을 바꾸니 이상없이 통과한다.

    다만 중복으로 들린 경우를 배제해버려서 반례찾는다고 시간을 많이 썼다.
    반례는 다음과 같다.

        3 3 2 8
        ?.?
        //.
        \/?

    그러면 5는 두 번 방문하고 경우의 수는 27이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1536
    {

        static void Main1536(string[] args)
        {

            int MOD = 10_007;
            int CHANGE = 100;
            int MIRROR1 = 101;
            int MIRROR2 = 102;
            int EMPTY = 103;

            int n, m, x, y;
            int[][] board;
            int change;
            int[] cnt;

            Input();

            SetCnt();

            GetRet();

            void GetRet()
            {

                (int r, int c, int dir) s = (0, 0, 0);
                int[] dirR = { 1, 0, -1, 0 }, dirC = { 0, 1, 0, -1 };
                
                for (int i = 1; i <= m; i++)
                {

                    if (board[0][i] == x) s = (0, i, 0);
                    if (board[n + 1][i] == x) s = (n + 1, i, 2);
                }

                for (int j = 1; j <= n; j++)
                {

                    if (board[j][0] == x) s = (j, 0, 1);
                    if (board[j][m + 1] == x) s = (j, m + 1, 3);
                }

                s.r += dirR[s.dir];
                s.c += dirC[s.dir];

                int ret = DFS(s.r, s.c, s.dir, change);
                Console.Write(ret);

                int DFS(int _r, int _c, int _dir, int _change)
                {

                    ref int cur = ref board[_r][_c];
                    if (cur < 100) return cur == y ? cnt[_change] : 0;
                    
                    int ret = 0;

                    if (cur == CHANGE)
                    {

                        // 변환하며 진행
                        for (int TILE = MIRROR1; TILE <= EMPTY; TILE++)
                        {

                            cur = TILE;

                            int dir = NextDir(cur, _dir);
                            int nR = _r + dirR[dir];
                            int nC = _c + dirC[dir];

                            ret = (ret + DFS(nR, nC, dir, _change - 1)) % MOD;
                        }

                        cur = CHANGE;
                    }
                    else
                    {

                        // 그냥 진행
                        int dir = NextDir(cur, _dir);
                        int nR = _r + dirR[dir];
                        int nC = _c + dirC[dir];

                        ret = (ret + DFS(nR, nC, dir, _change)) % MOD;
                    }

                    return ret;
                }

                int NextDir(int _mirror, int _dir)
                {

                    // 다음 방향
                    if (_mirror == MIRROR1)
                    {

                        switch (_dir)
                        {

                            case 0:
                                return 1;

                            case 1:
                                return 0;

                            case 2:
                                return 3;

                            case 3:
                                return 2;
                        }
                    }
                    else if (_mirror == MIRROR2) 
                    {

                        switch (_dir)
                        {

                            case 0:
                                return 3;

                            case 1:
                                return 2;

                            case 2:
                                return 1;

                            case 3:
                                return 0;
                        }
                    }

                    return _dir;
                }
            }

            void SetCnt()
            {

                cnt = new int[change + 1];
                cnt[0] = 1;
                for (int i = 1; i <= change; i++)
                {

                    cnt[i] = (cnt[i - 1] * 3) % MOD;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                x = int.Parse(temp[2]);
                y = int.Parse(temp[3]);

                board = new int[n + 2][];
                for (int i = 0; i < board.Length; i++)
                {

                    board[i] = new int[m + 2];
                }

                change = 0;
                for (int i = 1; i <= n; i++)
                {

                    string input = sr.ReadLine();
                    for (int j = 0; j < m; j++)
                    {

                        switch (input[j])
                        {

                            case '?':
                                board[i][j + 1] = CHANGE;
                                change++;
                                break;

                            case '.':
                                board[i][j + 1] = EMPTY;
                                break;

                            case '\\':
                                board[i][j + 1] = MIRROR1;
                                break;

                            case '/':
                                board[i][j + 1] = MIRROR2;
                                break;
                        }
                    }
                }

                int idx = 1;
                for (int i = 1; i <= m; i++)
                {

                    board[0][i] = idx++;
                }

                for (int j = 1; j <= n; j++)
                {

                    board[j][0] = idx++;
                }

                for (int j = 1; j <= n; j++)
                {

                    board[j][m + 1] = idx++;
                }

                for (int i = 1; i <= m; i++)
                {

                    board[n + 1][i] = idx++;
                }
            }
        }
    }
}
