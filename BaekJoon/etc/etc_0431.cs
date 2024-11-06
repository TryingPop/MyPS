using System;
using System.IO;
using System.Collections.Generic;

/*
날짜 : 2024. 4. 2
이름 : 배성훈
내용 : 안전 영역
    문제번호 : 2468번

    브루트포스, DFS, BFS 문제다
    문제에서 요구하는 건 섬(영역?)의 개수다

    우선 ㄱ자 와 같은 모양이 있다고 치자
    높이가 1 상승할 때, 나올 수 있는 경우는 
        O O   ->   X O          O X             X X
          O          O   ,        O         ,     X
    이외에도 다른 모양이 더 있으나

    영역이 늘어날 수도, 줄어들 수도, 유지될 수도 있다!
    그래서 높이를 1씩 늘려갈 때, 증가나 감소 한 경우가 안되므로 이분탐색을 못한다
    이에 일일히 확인하는 브루트포스로 탐색을 했다

    이에 높이를 1씩 올려가면서 BFS 로 영역의 개수를 일일히 찾았다
    다만 모두 물에 잠기는 경우 이후에는 계속해서 같은 경우만 나오기에 탈출했다

    이렇게 제출하니 이상업싱 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0431
    {

        static void Main431(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            int[,] board = new int[n, n];

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    board[i, j] = ReadInt();
                }
            }

            sr.Close();
            Queue<(int r, int c)> q = new(n * n);

            bool[,] use = new bool[n, n];
            bool curUse = true;
            int ret = 1;

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            for (int h = 0; h <= 100; h++)
            {

                int curCnt = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        if (board[i, j] > h) continue;
                        use[i, j] = curUse;
                    }
                }

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        if (use[i, j] == curUse) continue;
                        use[i, j] = curUse;
                        curCnt++;
                        q.Enqueue((i, j));

                        while(q.Count > 0)
                        {

                            var node = q.Dequeue();

                            for (int k = 0; k < 4; k++)
                            {

                                int nextR = node.r + dirR[k];
                                int nextC = node.c + dirC[k];

                                if (ChkInValidPos(nextR, nextC) || use[nextR, nextC] == curUse) continue;
                                use[nextR, nextC] = curUse;
                                q.Enqueue((nextR, nextC));
                            }
                        }
                    }
                }
                curUse = !curUse;
                if (ret < curCnt) ret = curCnt;
                else if (curCnt == 0) break;
            }

            Console.WriteLine(ret);

            bool ChkInValidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= n || _c >= n) return true;
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

#if other
using System;

namespace 백준_알고리즘
{
    internal class Program
    {
        static int Result;
        static int N;
        static int[] dx = { 0, 0, 1, -1 };
        static int[] dy = { 1, -1, 0, 0 };
        static bool[,] visit;
        static int[,] Map;
        static void Main()
        {
            Result = 0;
            N = int.Parse(Console.ReadLine());
            Map = new int[N, N];
            int MaxHeight = 0;

            for (int i = 0; i < N; i++)
            {
                int[] Input = Array.ConvertAll(Console.ReadLine().Split(' '), s => int.Parse(s));
                for (int j = 0; j < N; j++) 
                { 
                    Map[i, j] = Input[j];
                    if (MaxHeight < Input[j]) { MaxHeight = Input[j]; }
                }
            }
           
            for (int i = 0; i < MaxHeight; i++)
            {
                int Cnt = 0;
                visit = new bool[N, N];
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        if (!visit[j,k] && i < Map[j , k]) { DFS(j, k, i); Cnt++; }
                    }
                }
                if (Result < Cnt) { Result = Cnt; }
            }

            Console.WriteLine(Result);
        }

        static void DFS(int x, int y, int h)
        {
            visit[x, y] = true;
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                if (nx >= 0 && ny >= 0 && nx < N && ny < N)
                {
                    if (!visit[nx, ny] && Map[nx, ny] > h)
                    {
                        DFS(nx, ny, h);
                    }
                }
            }
        }
    }
}
#endif
}
