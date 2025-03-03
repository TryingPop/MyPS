using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 로봇
    문제번호 : 13901번

    구현, 시물레이션 문제다
    문제 조건에 맞게 구현하고 시물레이션 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0187
    {

        static void Main187(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[,] board = new int[row, col];

            int objs = ReadInt(sr);

            for (int i = 0; i < objs; i++)
            {

                int r = ReadInt(sr);
                int c = ReadInt(sr);

                board[r, c] = -1;
            }

            // 1칸씩 검사하기에 크기는 1
            Queue<(int r, int c)> q = new Queue<(int r, int c)>(1);
            int sRow = ReadInt(sr);
            int sCol = ReadInt(sr);
            board[sRow, sCol] = 1;
            q.Enqueue((sRow, sCol));

            int[] dirs = { 0, 0, 0, 0 };

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            dirs[0] = ReadInt(sr) - 1;
            dirs[1] = ReadInt(sr) - 1;
            dirs[2] = ReadInt(sr) - 1;
            dirs[3] = ReadInt(sr) - 1;

            sr.Close();

            int retR = sRow, retC = sCol;
            int curDir = 0;
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                int change = 0;

                // 방향 전환 4번해도 안나오면 종료
                while (change < 4)
                {

                    int nextR = node.r + dirR[dirs[curDir]];
                    int nextC = node.c + dirC[dirs[curDir]];

                    if (ChkInvalidPos(nextR, nextC, row, col) || board[nextR, nextC] != 0)
                    {

                        // 벽에 막히거나 장애물이 있거나 이미 지나온 장소일 시 방향 전환
                        change++;
                        curDir = curDir == 3 ? 0 : curDir + 1;
                        continue;
                    }

                    // 지나갈 수 있는 곳이면 이동하고 다음 단계로 간다 
                    board[nextR, nextC] = board[node.r, node.c] + 1;
                    q.Enqueue((nextR, nextC));
                    break;
                }

                if (change == 4)
                {

                    retR = node.r;
                    retC = node.c;
                }
            }

            Console.WriteLine($"{retR} {retC}");
        }

        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            if (_r < 0 || _r >= _row || _c < 0 || _c >= _col) return true;
            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
