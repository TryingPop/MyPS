using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 26
이름 : 배성훈
내용 : 세 번 이내에 사과를 먹자
    문제번호 : 26169번

    DFS 문제다
    BFS 로 처음에 시도했으나 89%? 언저리에서 실패한다
    그래서 먼저 DFS로 풀었다
    이후 BFS로 시도하려는 중 안되는 이유를 알았다
    ㄷ자형태로 사과가 있을 때, 일반적인 BFS로는 못찾는다
*/

namespace BaekJoon.etc
{
    internal class etc_0354
    {

        static void Main354(string[] args)
        {

            int[][] board = new int[5][];
            StreamReader sr = new(Console.OpenStandardInput());

            for (int i = 0; i < 5; i++)
            {

                board[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            }

            int[] s = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            sr.Close();
            int ret = 0;

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

#if Wrong

            Queue<(int r, int c, int move, int eat)> q = new(9);

            q.Enqueue((s[0], s[1], 0, 0));
            while (q.Count > 0)
            {

                var node = q.Dequeue();

                for (int i = 0; i < 4; i++)
                {

                    int nextR = node.r + dirR[i];
                    int nextC = node.c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] == -1) continue;
                    int nextEat = node.eat;
                    if (board[nextR][nextC] == 1) nextEat++;

                    if (nextEat >= 2)
                    {

                        ret = 1;
                        q.Clear();
                        break;
                    }
                    else if (node.move < 2) q.Enqueue((nextR, nextC, node.move + 1, nextEat));
                }
            }

#else

            ret = DFS(s[0], s[1], 0, 0);
#endif
            Console.WriteLine(ret);

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= 5 || _c >= 5) return true;
                return false;
            }

            int DFS(int _r, int _c, int _move, int _eat)
            {

                if (_move == 3)
                {

                    if (_eat >= 2) return 1;
                    return 0;
                }

                int ret = 0;
                int cur = board[_r][_c];
                for (int i = 0; i < 4; i++)
                {

                    int nextR = _r + dirR[i];
                    int nextC = _c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] == -1) continue;
                    board[_r][_c] = -1;

                    int nextEat = _eat;
                    if (board[nextR][nextC] == 1) nextEat++;
                    ret += DFS(nextR, nextC, _move + 1, nextEat);
                    board[_r][_c] = cur;
                }

                if (ret > 0) return 1;
                return 0;
            }
        }
    }
}
