using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : 헌내기는 친구가 필요해
    문제번호 : 21736번

    BFS 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1057
    {

        static void Main1057(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] board;
            (int r, int c) h;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Queue<(int r, int c)> q = new(row * col);
                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                int ret = 0;
                q.Enqueue(h);
                while (q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC] == -1) continue;
                        if (board[nR][nC] == 1) ret++;
                        board[nR][nC] = -1;
                        q.Enqueue((nR, nC));
                    }
                }

                if (ret != 0) Console.Write(ret);
                else Console.Write("TT");
            }

            bool ChkInvalidPos(int _r, int _c) 
                => _r < 0 || _c < 0 || _r >= row || _c >= col;

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                h = (0, 0);
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == 'O') continue;
                        else if (cur == 'X') board[r][c] = -1;
                        else if (cur == 'I') h = (r, c);
                        else if (cur == 'P') board[r][c] = 1;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
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
public class Program
{
    static int n;
    static int m;
    static char[,] map;
    static bool[,] visited;

    static void Main()
    {
        int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        n = input[0];
        m = input[1];
        map = new char[n + 1, m + 1];
        visited = new bool[n + 1, m + 1];
        int[] start = new int[2];

        for (int i = 1; i <= n; i++)
        {
            string input2 = Console.ReadLine();
            for (int j = 1; j <= m; j++)
            {
                map[i, j] = input2[j-1];
                if(input2[j-1] == 'I')
                {
                    start[0] = i;
                    start[1] = j;
                }
            }
        }

        BFS(start[0], start[1]);
    }

    static void BFS(int x, int y)
    {
        Queue<(int, int)> q = new Queue<(int, int)>();
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };
        int count = 0;

        q.Enqueue((x, y));
        visited[x, y] = true;
        
        while (q.Count > 0)
        {
            var cur = q.Dequeue();
            int curX = cur.Item1;
            int curY = cur.Item2;

            for (int i = 0; i < 4; i++)
            {
                curX = cur.Item1 + dx[i];
                curY = cur.Item2 + dy[i];

                if (curX < 1 || curY < 1 || curX > n || curY > m) continue;
                if (map[curX, curY] == 'X' || visited[curX, curY] == true) continue;
                if (map[curX, curY] == 'P') count++;

                q.Enqueue((curX, curY));
                visited[curX, curY] = true;
            }
        }

        if (count > 0) Console.WriteLine(count);
        else Console.WriteLine("TT");
    }
}
#endif
}
