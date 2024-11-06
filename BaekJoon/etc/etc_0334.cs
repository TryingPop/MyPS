using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : 펭귄의 하루
    문제번호 : 29703번

    BFS 문제다
    visit 방문 처리를 안하고 BFS 탐색을 해서 시간초과가 떴다
    이후 visit을 추가하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0334
    {

        static void Main334(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] size = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            int[][][] board = new int[2][][];
            bool[][][] visit = new bool[2][][];
            Queue<(int r, int c, int get)> q = new(size[0] * size[1]);

            (int r, int c) end = (0, 0);
            board[0] = new int[size[0]][];
            board[1] = new int[size[0]][];
            visit[0] = new bool[size[0]][];
            visit[1] = new bool[size[0]][];
            for (int i = 0; i < size[0]; i++)
            {

                board[0][i] = new int[size[1]];
                board[1][i] = new int[size[1]];

                visit[0][i] = new bool[size[1]];
                visit[1][i] = new bool[size[1]];

                for (int j = 0; j < size[1]; j++)
                {

                    int cur = sr.Read();

                    if (cur == 'S')
                    {

                        q.Enqueue((i, j, 0));
                        visit[0][i][j] = true;
                        cur = 1;
                    }
                    else if (cur == 'E') cur = 0;
                    else if (cur == 'F') cur = -1;
                    else if (cur == 'D') cur = -2;
                    else if (cur == 'H') 
                    { 
                        
                        cur = -3;
                        end = (i, j);
                    }

                    board[0][i][j] = cur;
                }

                sr.ReadLine();
            }

            sr.Close();

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                int curVal = board[node.get][node.r][node.c];
                for (int i = 0; i < 4; i++)
                {

                    int nextR = node.r + dirR[i];
                    int nextC = node.c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC) || visit[node.get][nextR][nextC]) continue;
                    visit[node.get][nextR][nextC] = true;

                    if (board[0][nextR][nextC] == -2) continue;

                    int nextGet = node.get;
                    if (node.get == 0 && board[0][nextR][nextC] == -1)
                    {

                        nextGet = 1;
                        visit[1][nextR][nextC] = true;
                    }

                    int curNext = board[nextGet][nextR][nextC];
                    board[nextGet][nextR][nextC] = curVal + 1;
                    q.Enqueue((nextR, nextC, nextGet));
                }
            }

            int ret = board[1][end.r][end.c] - 1;
            Console.WriteLine(ret);

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= size[0] || _c >= size[1]) return true;
                return false;
            }
        }
    }
}
