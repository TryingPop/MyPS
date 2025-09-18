using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 18
이름 : 배성훈
내용 : Gaaaaaaaaaarden
    문제번호 : 18809번

    브루트포스, 시뮬레이션, BFS 문제다.
    우선 배양액을 놓을 수 있는 경우의 수는 10! / (r! g! (10-r-g)!)이다.
    이를 계산해보면 커봐야 10C5= 252이하임을 알 수 있다.
    
    그리고 배양액을 시뮬레이션 돌릴 시 약 맵의 크기만큼  50 x 50 = 2500번 연산을 한다.
    그리고 이 둘을 곱해도 252 x 2500 = 63만으로 충분히 시뮬레이션 돌릴만하다 판단했다.

    그래서 각 배양액을 선택하는 것은 DFS로 구현하고
    시뮬레이션은 BFS를 이용해 구현하니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1899
    {

        static void Main1899(string[] args)
        {

            int row, col, posLen, red, green;
            int[][] board;
            (int r, int c)[] pos;
            Input();

            GetRet();

            void GetRet()
            {

                Queue<(int r, int c)> rq = new(row * col), nq = new(row * col), gq = new(row * col);
                int[][] rVisit = new int[row][], gVisit = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    rVisit[r] = new int[col];
                    gVisit[r] = new int[col];
                }

                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                int[] select = new int[posLen];
                int ret = 0;
                DFSR();

                Console.Write(ret);

                void DFSR(int dep = 0, int sel = 0)
                {

                    if (sel == red)
                    {

                        DFSG();
                        return;
                    }
                    else if (posLen <= dep) return;

                    select[dep] = 1;
                    DFSR(dep + 1, sel + 1);
                    select[dep] = 0;

                    DFSR(dep + 1, sel);
                }

                void DFSG(int dep = 0, int sel = 0)
                {

                    if (sel == green)
                    {

                        FillPos();
                        ret = Math.Max(BFS(), ret);
                        return;
                    }
                    else if (posLen <= dep) return;

                    if (select[dep] == 0)
                    {

                        select[dep] = 2;
                        DFSG(dep + 1, sel + 1);
                        select[dep] = 0;
                    }

                    DFSG(dep + 1, sel);
                }

                void FillPos()
                {

                    rq.Clear();
                    gq.Clear();
                    nq.Clear();
                    for (int r = 0; r < row; r++)
                    {

                        Array.Fill(rVisit[r], -1);
                        Array.Fill(gVisit[r], -1);
                    }

                    for (int i = 0; i < posLen; i++)
                    {

                        if (select[i] == 0) continue;
                        int r = pos[i].r;
                        int c = pos[i].c;
                        if (select[i] == 1)
                        {

                            rVisit[r][c] = 0;
                            rq.Enqueue((r, c));
                        }
                        else
                        {

                            gVisit[r][c] = 0;
                            gq.Enqueue((r, c));
                        }
                    }
                }

                int BFS()
                {

                    int ret = 0;
                    int turn = 0;
                    while (rq.Count > 0 && gq.Count > 0)
                    {

                        while (rq.Count > 0)
                        {

                            (int r, int c) = rq.Dequeue();
                            // 빠져야할 건지 확인
                            if (rVisit[r][c] == -2) continue;

                            for (int i = 0; i < 4; i++)
                            {

                                int nR = r + dirR[i];
                                int nC = c + dirC[i];
                                if (ChkInvalidPos(nR, nC)
                                    || board[nR][nC] == 0
                                    || rVisit[nR][nC] != -1
                                    || gVisit[nR][nC] != -1) continue;

                                rVisit[nR][nC] = turn + 1;
                                nq.Enqueue((nR, nC));
                            }
                        }

                        Queue<(int r, int c)> temp = rq;
                        rq = nq;
                        nq = temp;

                        while (gq.Count > 0)
                        {

                            (int r, int c) = gq.Dequeue();

                            for (int i = 0; i < 4; i++)
                            {

                                int nR = r + dirR[i];
                                int nC = c + dirC[i];
                                if (ChkInvalidPos(nR, nC)
                                    || board[nR][nC] == 0) continue;

                                if (rVisit[nR][nC] == turn + 1 && gVisit[nR][nC] == -1)
                                {

                                    rVisit[nR][nC] = -2;
                                    gVisit[nR][nC] = -2;
                                    ret++;
                                    continue;
                                }
                                else if (rVisit[nR][nC] != -1 || gVisit[nR][nC] != -1) continue;

                                gVisit[nR][nC] = turn + 1;
                                nq.Enqueue((nR, nC));
                            }
                        }

                        temp = gq;
                        gq = nq;
                        nq = temp;

                        turn++;
                    }

                    return ret;
                }

                bool ChkInvalidPos(int r, int c)
                    => r < 0 || c < 0 || r >= row || c >= col;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();
                red = ReadInt();
                green = ReadInt();
                board = new int[row][];
                posLen = 0;
                pos = new (int r, int c)[10];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = ReadInt();
                        if (cur == 2)
                        {

                            board[r][c] = 1;
                            pos[posLen++] = (r, c);
                        }
                        else board[r][c] = cur;
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
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
}
