using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 20
이름 : 배성훈
내용 : 아기 상어2
    문제번호 : 17086번

    8방향 이동 문제다
    BFS 탐색으로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0070
    {

        static void Main70(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[,] board = new int[row, col];
            bool[,] visit = new bool[row, col];
            Queue<(int row, int col)> q = new();
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    int chk = ReadInt(sr);
                    if (chk == 1) 
                    { 
                        
                        q.Enqueue((r, c)); 
                        visit[r, c] = true;
                    }
                }
            }

            sr.Close();

            int max = 0;
            int[] dirX = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] dirY = { 0, 0, -1, 1, -1, 1, -1, 1 };

            // BFS 탐색
            while(q.Count > 0)
            {

                var node = q.Dequeue();
                
                int val = board[node.row, node.col];
                if (val > max) max = val;

                for (int i = 0; i < 8; i++)
                {

                    int nextR = node.row + dirY[i];
                    int nextC = node.col + dirX[i];
                    if (ChkInvalidPos(nextR, nextC, row, col) || visit[nextR, nextC]) continue;

                    visit[nextR, nextC] = true;
                    board[nextR, nextC] = val + 1;
                    q.Enqueue((nextR, nextC));
                }
            }

            Console.WriteLine(max);
        }

        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            if (_r < 0 || _r >= _row) return true;
            if (_c < 0 || _c >= _col) return true;

            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while((c = _sr.Read()) != ' ' && c != '\n' && c != -1)
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
