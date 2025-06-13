using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 19
이름 : 배성훈
내용 : 연구소 3
    문제번호 : 17142번

    브루트포스 알고리즘, 너비 우선 탐색 문제다.
    아이디어는 다음과 같다.
    바이러스가 있는 곳 중 m개를 활성화 시킨다.
    1칸씩 이동하면서 모든 이동할 수 있는 곳을 감염시킬 수 있는지 확인하면 된다.
    이후 바이러스가 있는 경우 결과에 카운팅 안하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1123
    {

        static void Main1123(string[] args)
        {

            int INF = 65536;

            int[][] board;
            int[][] move;
            Queue<(int r, int c)> q;
            int[] dirR, dirC;

            int n, m;
            int virusLen;
            (int r, int c)[] virus;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                int[] select = new int[m];
                int ret = INF;

                DFS();

                Console.Write(ret == INF ? -1 : ret);

                void DFS(int _idx = 0, int _depth = 0)
                {

                    if (m == _depth)
                    {

                        for (int i = 0; i < m; i++)
                        {

                            int idx = select[i];
                            q.Enqueue(virus[idx]);
                            move[virus[idx].r][virus[idx].c] = 0;
                        }

                        ret = Math.Min(ret, BFS());
                        return;
                    }

                    for (int i = _idx; i < virusLen; i++)
                    {

                        select[_depth] = i;
                        DFS(i + 1, _depth + 1);
                    }
                }
            }

            int BFS()
            {

                while (q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();
                    int cur = move[node.r][node.c];

                    for (int dir = 0; dir < 4; dir++)
                    {

                        int nR = node.r + dirR[dir];
                        int nC = node.c + dirC[dir];

                        if (ChkInvalidPos(nR, nC) 
                            || move[nR][nC] != -1 
                            || board[nR][nC] == 1) continue;

                        move[nR][nC] = cur + 1;
                        q.Enqueue((nR, nC));
                    }
                }

                int ret = 0;
                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] == 1) continue;
                        int cur = move[r][c];
                        move[r][c] = -1;
                        if (board[r][c] == 2) continue;
                        else if (ret == INF) continue;
                        else if (cur == -1)
                        {

                            ret = INF;
                            continue;
                        }

                        ret = Math.Max(cur, ret);
                    }
                }

                return ret;

                bool ChkInvalidPos(int _r, int _c) => _r < 0 || _c < 0 || _r >= n || _c >= n;
            }

            void SetArr()
            {

                q = new(n * n);
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                virus = new (int r, int c)[virusLen];
                move = new int[n][];
                int idx = 0;
                for (int r = 0; r < n; r++)
                {

                    move[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        move[r][c] = -1;
                        if (board[r][c] == 2) virus[idx++] = (r, c);
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                board = new int[n][];
                virusLen = 0;
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        int cur = ReadInt();
                        board[r][c] = cur;
                        if (cur == 2) virusLen++;
                    }
                }

                sr.Close();
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
    }
}
