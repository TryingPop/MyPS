using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 244
이름 : 배성훈
내용 : 양 한마리... 양 두마리...
    문제번호 : 11123번

    플러드 필 문제다.
    양 무리를 찾는 문제다.
    인접한 양들은 같은 양 무리에 포함되었다고 보면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1834
    {

        static void Main1834(string[] args)
        {

            int SHEEP = '#';
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[][] board = new int[100][];
            for (int i = 0; i < 100; i++)
            {

                board[i] = new int[100];
            }

            int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
            Queue<(int r, int c)> q = new(100 * 100);
            int row, col;

            int t = ReadInt();
            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] != SHEEP) continue;
                        BFS(r, c);
                        ret++;
                    }
                }

                sw.Write(ret);
                sw.Write('\n');

                void BFS(int _r, int _c)
                {

                    board[_r][_c] = 0;
                    q.Enqueue((_r, _c));

                    while (q.Count > 0)
                    {

                        var node = q.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];

                            if (ChkInvalidPos(nR, nC) || board[nR][nC] != SHEEP) continue;
                            board[nR][nC] = 0;
                            q.Enqueue((nR, nC));
                        }
                    }

                    bool ChkInvalidPos(int _r, int _c)
                        => _r < 0 || _c < 0 || _r >= row || _c >= col;
                }
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = sr.Read();
                    }

                    while ((sr.Read()) != '\n') ;
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
