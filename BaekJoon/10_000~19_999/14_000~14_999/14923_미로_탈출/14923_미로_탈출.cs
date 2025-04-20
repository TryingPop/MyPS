using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 미로 탈출
    문제번호 : 14923번

    그래프 이론, 그래프 탐색, BFS 문제다
    방문 어부로 하는게 아닌, 기록 숫자로 해결하려다가 시간초과, 메모리 초과 등 문제가 많이 생겼다
    코드만 복잡해져가기만해서 방문 여부 변수를 줘서 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0298
    {

        static void Main298(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[][,] board = new int[2][,];
            bool[][,] visit = new bool[2][,];
            (int r, int c) s = (ReadInt(sr) - 1, ReadInt(sr) - 1);
            (int r, int c) e = (ReadInt(sr) - 1, ReadInt(sr) - 1);

            for (int i = 0; i < 2; i++)
            {

                board[i] = new int[row, col];
                visit[i] = new bool[row, col];
            }

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[0][r, c] = -ReadInt(sr);
                }
            }

            sr.Close();

            Queue<(int r, int c, int broken)> q = new Queue<(int r, int c, int broken)>(row * col);

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            board[0][s.r, s.c] = 1;
            visit[0][s.r, s.c] = true;
            q.Enqueue((s.r, s.c, 0));
            while (q.Count > 0)
            {

                var node = q.Dequeue();

                int curVal = board[node.broken][node.r, node.c];
                for (int i = 0; i < 4; i++)
                {

                    int nextR = node.r + dirR[i];
                    int nextC = node.c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC, row, col) || visit[node.broken][nextR, nextC]) continue;

                    visit[node.broken][nextR, nextC] = true;
                    // 벽이 있는 경우
                    if (board[0][nextR, nextC] == -1)
                    {

                        // 한 번 뚫은 경우면 더 못 뚫으므로 탈출
                        if (node.broken > 0) continue;

                        // 벽 뚫고 진행
                        visit[1][nextR, nextC] = true;
                        board[1][nextR, nextC] = curVal + 1;
                        q.Enqueue((nextR, nextC, 1));
                        continue;
                    }

                    board[node.broken][nextR, nextC] = curVal + 1;
                    q.Enqueue((nextR, nextC, node.broken));
                }
            }

            int calc1 = board[0][e.r, e.c];
            int calc2 = board[1][e.r, e.c];
            
            if (calc1 <= 0 && calc2 <= 0) Console.WriteLine(-1);
            else if (calc1 <= 0 && calc2 > 0) Console.WriteLine(calc2 - 1);
            else Console.WriteLine(calc1 < calc2 ? calc1 - 1 : calc2 - 1);
        }

        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            if (_r < 0 || _c < 0 || _r >= _row || _c >= _col) return true;
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
