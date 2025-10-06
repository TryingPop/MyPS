using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 30
이름 : 배성훈
내용 : 가희와 거북이 인형
    문제번호 : 22237번

    BFS, 물리학 문제다.
    거북이들이 많이 있을 수 있다.
    반면 장애물과 집의 개수는 21개이다.
    그래서 거북이들 몸통을 모두 이동하는게 아닌
    집과 장애물들을 이동하며 BFS 탐색을 하면 된다.

    다만 맵을 벗어나면 안된다.
    초기에 집을 이동시켜 찾아갔다.
    해당 경우 고민하니 범위가 맞지 않는다는 문제를 발견했다.
    
    그래서 집이 아닌 거북이들을 묶는 가장 작은 사각형의 좌표가 가장 낮은 부분으로 이동시키며 정답을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1922
    {

        static void Main1922(string[] args)
        {

            int row, col;
            bool[][] board;
            (int r, int c)[] obs;
            (int r, int c) h;
            int len;

            Input();

            GetRet();

            void GetRet()
            {

                int minR, minC, maxR, maxC;
                int[][] move, prev;
                (int r, int c) ret;
                Init();

                BFS();

                void Init()
                {

                    minR = row - 1;
                    maxR = 0;
                    minC = col - 1;
                    maxC = 0;

                    move = new int[row][];
                    prev = new int[row][];

                    for (int r = 0; r < row; r++)
                    {

                        move[r] = new int[col];
                        prev[r] = new int[col];
                        for (int c = 0; c < col; c++)
                        {

                            move[r][c] = -1;
                            prev[r][c] = -1;
                            if (!board[r][c]) continue;
                            minR = Math.Min(r, minR);
                            minC = Math.Min(c, minC);
                            maxR = Math.Max(r, maxR);
                            maxC = Math.Max(c, maxC);
                        }
                    }
                }

                void BFS()
                {

                    int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                    Queue<(int r, int c)> q = new(row * col);
                    q.Enqueue((minR, minC));
                    move[minR][minC] = 0;
                    ret = (-1, -1);

                    while (q.Count > 0)
                    {

                        (int r, int c) = q.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nR = r + dirR[i];
                            int nC = c + dirC[i];

                            if (ChkInvalidPos(nR, nC)
                                || ChkInvalidPos(maxR - minR + nR, maxC - minC + nC)
                                || ChkMeetObs(nR - minR, nC - minC)
                                || move[nR][nC] != -1) continue;

                            move[nR][nC] = move[r][c] + 1;
                            prev[nR][nC] = i;

                            q.Enqueue((nR, nC));
                            if (ChkEscape(nR - minR, nC - minC))
                            {

                                ret = (nR, nC);
                                q.Clear();
                                break;
                            }
                        }
                    }
                }

                Output();

                void Output()
                {

                    using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                    if (ret.r == -1 || h.r == -1)
                    {

                        sw.Write(-1);
                        return;
                    }

                    char[] stk = new char[move[ret.r][ret.c]];

                    for (int i = 0, idx = 0; i < stk.Length; i++, idx++)
                    {

                        int p = prev[ret.r][ret.c];
                        if (p == 0)
                        {

                            stk[idx] = 'U';
                            ret.r++;
                        }
                        else if (p == 1)
                        {

                            stk[idx] = 'L';
                            ret.c++;
                        }
                        else if (p == 2)
                        {

                            stk[idx] = 'D';
                            ret.r--;
                        }
                        else 
                        { 
                            
                            stk[idx] = 'R';
                            ret.c--;
                        }
                    }

                    for (int i = stk.Length - 1; i >= 0; i--)
                    {

                        sw.Write(stk[i]);
                    }
                }

                bool ChkInvalidPos(int r, int c)
                    => r < 0 || c < 0 || r >= row || c >= col;

                bool ChkMeetObs(int dr, int dc)
                {

                    for (int i = 0; i < len; i++)
                    {

                        int r = obs[i].r - dr;
                        int c = obs[i].c - dc;
                        if (ChkInvalidPos(r, c) || !board[r][c]) continue;
                        return true;
                    }

                    return false;
                }

                bool ChkEscape(int dr, int dc)
                    => !ChkInvalidPos(h.r - dr, h.c - dc) && board[h.r - dr][h.c - dc];
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                h = (-1, -1);
                board = new bool[row][];
                obs = new (int r, int c)[20];
                len = 0;
                for (int r = 0; r < row; r++)
                {

                    board[r] = new bool[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == 'T') board[r][c] = true;
                        else if (cur == '#') obs[len++] = (r, c);
                        else if (cur == 'H') h = (r, c);
                    }

                    while (sr.Read() != '\n') ;
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
