using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 게임
    문제번호 : 1584번

    BFS 탐색으로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0198
    {

        static void Main198(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[,] board = new int[501, 501];
            int[,] memo = new int[501, 501];

            int len = ReadInt(sr);
            for (int i = 0; i < len; i++)
            {

                int x1 = ReadInt(sr);
                int y1 = ReadInt(sr);
                int x2 = ReadInt(sr);
                int y2 = ReadInt(sr);

                int minX = x1 < x2 ? x1 : x2;
                int maxX = x1 < x2 ? x2 : x1;

                int minY = y1 < y2 ? y1 : y2;
                int maxY = y1 < y2 ? y2 : y1;

                for (int r = minY; r <= maxY; r++)
                {

                    for (int c = minX; c <= maxX; c++)
                    {

                        board[r, c] = 1;
                    }
                }
            }

            len = ReadInt(sr);
            for (int i = 0; i < len; i++)
            {

                int x1 = ReadInt(sr);
                int y1 = ReadInt(sr);
                int x2 = ReadInt(sr);
                int y2 = ReadInt(sr);

                int minX = x1 < x2 ? x1 : x2;
                int maxX = x1 < x2 ? x2 : x1;

                int minY = y1 < y2 ? y1 : y2;
                int maxY = y1 < y2 ? y2 : y1;

                for (int r = minY; r <= maxY; r++)
                {

                    for (int c = minX; c <= maxX; c++)
                    {

                        board[r, c] = -1;
                    }
                }
            }

            sr.Close();

            Queue<(int r, int c)> q = new(501 * 2);
            q.Enqueue((0, 0));
            memo[0, 0] = 1;

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            while(q.Count > 0)
            {

                var node = q.Dequeue();

                for (int i = 0; i < 4; i++)
                {

                    int nextR = node.r + dirR[i];
                    int nextC = node.c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC) || board[nextR, nextC] == -1) continue;

                    int cur = memo[node.r, node.c];
                    if (board[nextR, nextC] == 1) cur += 1;

                    // memo[nextR, nextC] 갱신되면 q에 넣어 갱신해줘야한다
                    // 안하면 틀린다
                    // 이는 앞의 etc_0195와는 다르다!
                    // 왜냐하면 앞에서는 -> 방향으로 단방향이 보장되어 1번만 넣어도 된다
                    // 여기서는 상하좌우 이동이므로 돌아서 더 짧은 경로가 발견되면 해당 경로로 다시 탐색해야한다!
                    // 이 부분은 우선순위 큐를 이용해 성능향상을 시킬 수 있다
                    if (memo[nextR, nextC] == 0 || memo[nextR, nextC] > cur) 
                    { 
                        
                        q.Enqueue((nextR, nextC)); 
                        memo[nextR, nextC] = cur;
                    }
                }
            }

            Console.WriteLine(memo[500, 500] - 1);
        }

        static bool ChkInvalidPos(int _r, int _c)
        {

            if (_r < 0 || _r > 500 || _c < 0 || _c > 500) return true;
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
