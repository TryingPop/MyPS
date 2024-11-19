using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 19
이름 : 배성훈
내용 : 성곽
    문제번호 : 2234번

    BFS, 비트마스킹 문제다.
    벽이 있는건 비트마스킹을 이용해 구하면 된다.
    BFS로 각 방의 개수와 방의 크기를 구한다.
    그리고 인접한 방에 대해 인원을 합치며 벽을 부순 최대 경우를 찾는다.
*/

namespace BaekJoon.etc
{
    internal class etc_1124
    {

        static void Main1124(string[] args)
        {

            int[][] board;
            int row, col;
            int[][] group;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret1 = 0;
                int ret2 = 0;
                int[] dirR = new int[4] { 0, -1, 0, 1 };
                int[] dirC = new int[4] { -1, 0, 1, 0 };

                Queue<(int r, int c)> q = new(row * col);
                int[] groupCnt = new int[row * col + 1];

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        // 방 찾기
                        if (group[r][c] > 0) continue;
                        // 방문하지 않은 노드 == 방 번호가 부여 안된 방
                        q.Enqueue((r, c));
                        ret1++;
                        group[r][c] = ret1;
                        // 방의 넓이
                        int cnt = SetGroup();
                        groupCnt[ret1] = cnt;
                        ret2 = Math.Max(ret2, cnt);
                    }
                }

                int ret3 = ret2;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = group[r][c];
                        for (int dir = 0; dir < 4; dir++)
                        {

                            int nR = r + dirR[dir];
                            int nC = c + dirC[dir];

                            if (ChkInvalidPos(nR, nC) 
                                || group[nR][nC] == cur) continue;

                            // 인접한 경우 방의 크기 조사
                            int next = group[nR][nC];
                            ret3 = Math.Max(ret3, groupCnt[cur] + groupCnt[next]);
                        }
                    }
                }

                Console.Write($"{ret1}\n{ret2}\n{ret3}");

                bool ChkWall(int _val, int _dir) => (_val & (1 << _dir)) != 0;
                bool ChkInvalidPos(int _r, int _c) => _r < 0 || _c < 0 || _r >= row || _c >= col;

                int SetGroup()
                {

                    int cnt = 0;
                    while (q.Count > 0)
                    {

                        (int r, int c) node = q.Dequeue();
                        cnt++;

                        for (int dir = 0; dir < 4; dir++)
                        {

                            int nR = node.r + dirR[dir];
                            int nC = node.c + dirC[dir];

                            if (ChkInvalidPos(nR, nC) 
                                || ChkWall(board[node.r][node.c], dir)
                                || group[nR][nC] != 0) continue;

                            group[nR][nC] = ret1;
                            q.Enqueue((nR, nC));
                        }
                    }

                    return cnt;
                }

            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                col = ReadInt();
                row = ReadInt();

                board = new int[row][];
                group = new int[row][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    group[r] = new int[col];

                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
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
