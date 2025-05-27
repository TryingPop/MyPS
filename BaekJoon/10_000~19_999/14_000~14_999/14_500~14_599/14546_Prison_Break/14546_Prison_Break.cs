using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 1
이름 : 배성훈
내용 : Prison Break
    문제번호 : 14546번

    BFS 문제다.
    해당 길을 갈 수 있는지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1603
    {

        static void Main1603(string[] args)
        {

            string Y = "YES\n";
            string N = "NO\n"; 

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t;
            int[][] board;

            int row, col;
            (int r, int c) s = (0, 0), e = (0, 0);
            Queue<(int r, int c)> q;
            int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };

            Init();

            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                q.Enqueue((s.r, s.c));
                board[s.r][s.c] += 10;

                while (q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();

                    int cur = board[node.r][node.c];
                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC] > 10) continue;
                        board[nR][nC] += 10;
                        if (board[nR][nC] == cur) q.Enqueue((nR, nC));
                    }
                }

                if (board[s.r][s.c] == board[e.r][e.c]) sw.Write(Y);
                else sw.Write(N);

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                col = ReadInt();
                row = ReadInt();

                s.c = ReadInt() - 1;
                s.r = ReadInt() - 1;

                e.c = ReadInt() - 1;
                e.r = ReadInt() - 1;

                for (int r = row - 1; r >= 0; r--)
                {

                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadOp();
                    }
                }
            }

            int ReadOp()
            {

                int ret = 0;

                while (TryReadOp()) ;
                return ret;

                bool TryReadOp()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c == '+' ? 1 : 2;
                    return false;
                }
            }

            void Init()
            {

                board = new int[10][];
                for (int i = 0; i < 10; i++)
                {

                    board[i] = new int[10];
                }

                q = new(100);
                t = ReadInt();
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

                    while((c = sr.Read()) !=-1 && c != ' ' && c != '\n')
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
