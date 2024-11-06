using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : 음식물 피하기
    문제번호 : 1743번

    BFS, DFS 문제다
    BFS 대로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0335
    {

        static void Main335(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt();
            int col = ReadInt();

            int len = ReadInt();

            int[,] board = new int[row, col];

            while(len-- > 0)
            {

                int r = ReadInt() - 1;
                int c = ReadInt() - 1;

                board[r, c] = 1;
            }

            sr.Close();

            int ret = 0;
            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };
            Queue<(int r, int c)> q = new(row * col);
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c< col; c++)
                {

                    if (board[r, c] == 0) continue;
                    int trash = 1;
                    board[r, c] = 0;

                    q.Enqueue((r, c));
                    while(q.Count > 0)
                    {

                        (int r, int c) node = q.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = node.r + dirR[i];
                            int nextC = node.c + dirC[i];

                            if (ChkInvalidPos(nextR, nextC) || board[nextR, nextC] == 0) continue;
                            board[nextR, nextC] = 0;
                            trash++;
                            q.Enqueue((nextR, nextC));
                        }
                    }

                    if (ret < trash) ret = trash;
                }
            }

            Console.WriteLine(ret);

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
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
