using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : 점프
    문제번호 : 1890번

    dp 문제다
    dp는 해당 좌표에서 결과로 가는 경우의 수를 모아놓았다
    그리고 좌표로 가는 길 탐색은 DFS로 했다

    다른 사람의 풀이를 보니 단방향이기에 이중 포문으로도 이동 구현이 가능해 보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0392
    {

        static void Main392(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            int[,] board = new int[n, n];
            long[,] dp = new long[n, n];

            for (int r = 0; r < n; r++)
            {

                for (int c = 0; c < n; c++)
                {

                    board[r, c] = ReadInt();
                    dp[r, c] = -1;
                }
            }

            sr.Close();

            int[] dirR = { 1, 0 };
            int[] dirC = { 0, 1 };

            long ret = DFS(0, 0);
            Console.WriteLine(ret);

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= n || _c >= n) return true;
                return false;
            }

            long DFS(int _r, int _c)
            {

                if (_r == n - 1 && _c == n - 1)
                {

                    return 1L;
                }

                if (dp[_r, _c] != -1) return dp[_r, _c];
                dp[_r, _c] = 0L;
                long ret = 0L;

                int j = board[_r, _c];
                for (int i = 0; i < 2; i++)
                {

                    int nextR = _r + dirR[i] * j;
                    int nextC = _c + dirC[i] * j;

                    if (ChkInvalidPos(nextR, nextC)) continue;
                    ret += DFS(nextR, nextC);
                }

                dp[_r, _c] = ret;
                return ret;
            }


            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
public static class PS
{
    private static int n;
    private static int[][] map;
    private static long[,] mem;

    static PS()
    {
        n = int.Parse(Console.ReadLine());
        map = new int[n][];
        mem = new long[n, n];
        mem[0, 0] = 1;

        for (int i = 0; i < n; i++)
        {
            map[i] = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        }
    }

    public static void Main()
    {
        (int row, int col) movePos;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (mem[i, j] != 0 && map[i][j] != 0)
                {
                    //Down
                    if (CanMove(movePos = (i + map[i][j], j)))
                        mem[movePos.row, movePos.col] += mem[i, j];

                    //Right
                    if (CanMove(movePos = (i, j + map[i][j])))
                        mem[movePos.row, movePos.col] += mem[i, j];
                }
            }
        }

        Console.Write(mem[n - 1, n - 1]);
    }

    private static bool CanMove((int row, int col) pos) =>
        0 <= pos.row && pos.row < n && 0 <= pos.col && pos.col < n;
}
#endif
}
