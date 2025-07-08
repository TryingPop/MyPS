using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 8
이름 : 배성훈
내용 : 상범 빌딩
    문제번호 : 6593번

    BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1754
    {

        static void Main1754(string[] args)
        {

            string NO = "Trapped!\n";
            string H = "Escaped in ";
            string T = " minute(s).\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int l, r, c;
            int[][][] board;
            (int l, int r, int c) s, e;
            Queue<(int l, int r, int c)> q;
            int[] dirL, dirR, dirC;
            Init();

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                q.Enqueue(s);
                board[s.l][s.r][s.c] = 1;

                while (q.Count > 0)
                {

                    (int l, int r, int c) node = q.Dequeue();
                    int cur = board[node.l][node.r][node.c];

                    for (int dir = 0; dir < 6; dir++)
                    {

                        int nL = node.l + dirL[dir];
                        int nR = node.r + dirR[dir];
                        int nC = node.c + dirC[dir];

                        if (ChkInvalidPos(nL, nR, nC) || board[nL][nR][nC] != 0) continue;
                        board[nL][nR][nC] = cur + 1;
                        q.Enqueue((nL, nR, nC));
                    }
                }

                int ret = board[e.l][e.r][e.c];
                if (ret == 0) sw.Write(NO);
                else
                {

                    sw.Write(H);
                    sw.Write(--ret);
                    sw.Write(T);
                }

                bool ChkInvalidPos(int _l, int _r, int _c)
                    => _l < 0 || _r < 0 || _c < 0 || _l >= l || _r >= r || _c >= c;
            }

            bool Input()
            {

                l = ReadInt();
                r = ReadInt();
                c = ReadInt();

                for (int i = 0; i < l; i++)
                {

                    for (int j = 0; j < r; j++)
                    {

                        for (int k = 0; k < c; k++)
                        {

                            int cur = ReadBoard();
                            if (cur == 'S') s = (i, j, k);
                            else if (cur == 'E') e = (i, j, k);

                            board[i][j][k] = cur == '#' ? -1 : 0;
                        }
                    }
                }

                return l != 0 || r != 0 || c != 0;
            }

            void Init()
            {

                int MAX = 30;
                board = new int[MAX][][];
                q = new(30 * 30 * 30);

                s = (0, 0, 0);
                e = (0, 0, 0);
                for (int i = 0; i < MAX; i++)
                {

                    board[i] = new int[MAX][];
                    for (int j = 0; j < MAX; j++)
                    {

                        board[i][j] = new int[MAX];
                    }
                }

                dirL = new int[6] { -1, 1, 0, 0, 0, 0 };
                dirR = new int[6] { 0, 0, -1, 1, 0, 0 };
                dirC = new int[6] { 0, 0, 0, 0, -1, 1 };
            }

            int ReadBoard()
            {

                int ret = 0;

                while (TryReadBoard()) ;
                return ret;

                bool TryReadBoard()
                {

                    int c = sr.Read();
                    if (c != '.' && c != '#' && c != 'S' && c != 'E') return true;
                    ret = c;
                    return false;
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
