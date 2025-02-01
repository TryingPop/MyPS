using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 19
이름 : 배성훈
내용 : 통나무 옮기기
    문제번호 : 1938번

    BFS 문제다.
    회전가능, 이동 가능한 배열을 먼저 만들고
    해당 장소로 이동하며 풀었다.

    통나무의 길이가 짧고 맵의 크기가 작아 맵마다 일일히 확인하며 풀었다.
    만약 통나무의 길이가 길 경우 1000 이하에서는 슬라이딩 윈도우와 우선순위 큐를 썼을것이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1206
    {

        static void Main1206(string[] args)
        {

            int B = 'B' - '0';
            int E = 'E' - '0';

            int NOT_VISIT = -1;
            int n;
            int[][] board;
            int[][] wMove, hMove;
            bool[][] chkRot, chkW, chkH;
            (int r, int c, bool isW) s, e;


            Solve();
            void Solve()
            {

                Input();

                SetArr();

                Find();

                BFS();
            }

            void BFS()
            {

                Queue<(int r, int c, bool isW)> q = new(n * n);
                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };

                if (s.isW) wMove[s.r][s.c] = 0;
                else hMove[s.r][s.c] = 0;

                q.Enqueue(s);

                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    
                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];
                        if (ChkInvalidPos(nR, nC, node.isW)) continue;

                        if (node.isW)
                        {

                            if (!chkW[nR][nC] || wMove[nR][nC] != NOT_VISIT) continue;
                            wMove[nR][nC] = wMove[node.r][node.c] + 1;
                            q.Enqueue((nR, nC, true));
                        }
                        else
                        {

                            if (!chkH[nR][nC] || hMove[nR][nC] != NOT_VISIT) continue;
                            hMove[nR][nC] = hMove[node.r][node.c] + 1;
                            q.Enqueue((nR, nC, false));
                        }
                    }

                    if (node.isW)
                    {

                        if (hMove[node.r][node.c] != NOT_VISIT
                            || !chkRot[node.r][node.c]
                            || ChkInvalidPos(node.r, node.c, false)) continue;
                        hMove[node.r][node.c] = wMove[node.r][node.c] + 1;
                        q.Enqueue((node.r, node.c, false));
                    }
                    else
                    {

                        if (wMove[node.r][node.c] != NOT_VISIT
                            || !chkRot[node.r][node.c]
                            || ChkInvalidPos(node.r, node.c, true)) continue;

                        wMove[node.r][node.c] = hMove[node.r][node.c] + 1;
                        q.Enqueue((node.r, node.c, true));
                    }
                }

                bool ChkInvalidPos(int _r, int _c, bool _isW)
                {

                    if (_isW) return _r < 0 || _c < 1 || _r >= n || _c >= n - 1;
                    else return _r < 1 || _c < 0 || _r >= n - 1 || _c >= n;
                }

                int ret = -1;
                if (e.isW) ret = wMove[e.r][e.c];
                else ret = hMove[e.r][e.c];
                if (ret == -1) ret = 0;
                Console.Write(ret);
            }

            void Find()
            {

                s = (-1, -1, false);

                for (int r = 0; r < n; r++)
                {

                    bool find = false;
                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] != B) continue;
                        find = true;

                        if (r + 1 < n && board[r + 1][c] == B)
                            s = (r + 1, c, false);
                        else
                            s = (r, c + 1, true);

                        break;
                    }

                    if (find) break;
                }

                e = (-1, -1, false);

                for (int r = 0; r < n; r++)
                {

                    bool find = false;
                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] != E) continue;
                        find = true;

                        if (r + 1 < n && board[r + 1][c] == E)
                            e = (r + 1, c, false);
                        else
                            e = (r, c + 1, true);

                        return;
                    }

                    if (find) return;
                }
            }

            void SetArr()
            {

                wMove = new int[n][];
                hMove = new int[n][];

                chkRot = new bool[n][];
                chkW = new bool[n][];
                chkH = new bool[n][];

                for (int i = 0; i < n; i++)
                {

                    wMove[i] = new int[n];
                    Array.Fill(wMove[i], NOT_VISIT);

                    hMove[i] = new int[n];
                    Array.Fill(hMove[i], NOT_VISIT);

                    chkRot[i] = new bool[n];
                    chkW[i] = new bool[n];
                    chkH[i] = new bool[n];
                }

                for (int r = 1; r < n - 1; r++)
                {

                    for (int c = 1; c < n - 1; c++)
                    {

                        bool rot = true;
                        for (int addR = -1; addR <= 1; addR++)
                        {

                            for (int addC = -1; addC <= 1; addC++)
                            {

                                if (board[r + addR][c + addC] != 1) continue;
                                rot = false;
                                break;
                            }

                            if (!rot) break;
                        }

                        chkRot[r][c] = rot;
                    }
                }


                for (int r = 0; r < n; r++)
                {
                    for (int c = 1; c < n - 1; c++)
                    {

                        if (board[r][c] == 1
                            || board[r][c - 1] == 1 
                            || board[r][c + 1] == 1) continue;

                        chkW[r][c] = true;
                    }
                }

                for (int r = 1; r < n - 1; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] == 1
                            || board[r - 1][c] == 1
                            || board[r + 1][c] == 1) continue;

                        chkH[r][c] = true;
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                board = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        board[i][j] = sr.Read() - '0';
                    }

                    if (sr.Read() == '\r') sr.Read();
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
    }
}
