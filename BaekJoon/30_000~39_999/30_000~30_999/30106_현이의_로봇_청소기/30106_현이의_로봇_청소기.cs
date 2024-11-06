using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 현이의 로봇 청소기
    문제번호 : 30106번

    구현 문제다

    높이 차로 이동 가능여부를 확인하기에
    A에서 B로 이동할 수 있다면 B에서 A로 이동할 수 있다
    즉 왕복 가능하다

    그리고 해당 지점에서 이동가능한 좌표들을 모아놓은 좌표들을 하나의 그룹이라하면
    서로다른 그룹의 개수가 정답이 된다

    그룹 탐색은 BFS로 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0115
    {

        static void Main115(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int k = ReadInt(sr);

            int[,] board = new int[row, col];

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = ReadInt(sr);
                }
            }

            sr.Close();

            bool[,] visit = new bool[row, col];
            Queue<(int r, int c)> q = new Queue<(int r, int c)>(row * col);

            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };
            int ret = 0;

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c <col; c++)
                {

                    // 서로 다른 그룹의 개수 찾기
                    if (visit[r, c]) continue;
                    visit[r, c] = true;
                    ret++;
                    q.Enqueue((r, c));

                    while(q.Count > 0)
                    {

                        var node = q.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = node.r + dirY[i];
                            int nextC = node.c + dirX[i];

                            if (ChkInvalidPos(nextR, nextC, row, col) || visit[nextR, nextC]) continue;

                            int chk = board[node.r, node.c] - board[nextR, nextC];
                            chk = chk < 0 ? -chk : chk;

                            if (chk > k) continue;
                            visit[nextR, nextC] = true;

                            q.Enqueue((nextR, nextC));
                        }
                    }
                }
            }

            Console.WriteLine(ret);
        }

        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            if (_r < 0 || _r >= _row || _c < 0 || _c >= _col) return true;
            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
