using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 10
이름 : 배성훈
내용 : 특별한 오름 등반
    문제번호 : 31671번

    BFS, dp 문제다.
    dp[i][j]를 이동한 좌표의 최대 높이가 담기게 dp를 설정하면 된다.
    그러면 x + 1, y - 1 또는 x + 1, y + 1의 방향으로만 이동하기에 현재 높이와 이전 항에서의 최댓값들 중 최대가 담기게 해서 풀었다.

    배열 2개로 dp가 해결되지만 이 경우 선생님이 있는 좌표의 정렬이 필요하다.
    맵의 크기가 2000 x 1000 으로 200만이므로 그냥 맵을 전체를 만들어 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1621
    {

        static void Main1621(string[] args)
        {

            int INF = 65_536;
            int n, m;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                board[0][0] = 0;
                for (int i = 1; i < board.Length; i++)
                {

                    
                    for (int j = 0; j < board[i - 1].Length; j++)
                    {

                        if (board[i - 1][j] == INF || board[i - 1][j] == -1) continue;
                        int next = j - 1;

                        if (next >= 0)
                            board[i][next] = Math.Max(board[i][next], board[i - 1][j]);

                        next = j + 1;
                        if (next < board[i].Length)
                            board[i][next] = Math.Max(board[i][next], Math.Max(board[i - 1][j], next));
                    }
                }

                if (board[2 * n][0] == INF) board[2 * n][0] = -1;
                Console.Write(board[2 * n][0]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                board = new int[n * 2 + 1][];
                for (int i = 0; i <= n; i++)
                {

                    board[i] = new int[i + 1];
                    Array.Fill(board[i], -1);
                }

                for (int i = n + 1, size = n; i < board.Length; i++, size--)
                {

                    board[i] = new int[size];
                    Array.Fill(board[i], -1);
                }

                for (int i = 0; i < m; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    board[x][y] = INF;
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
