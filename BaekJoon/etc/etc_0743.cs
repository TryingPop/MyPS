using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 30
이름 : 배성훈
내용 : Puyo Puyo
    문제번호 : 11559번

    BFS, 구현, 시뮬레이션 문제다
    처음에 돌이 모두 바닥에 닿은 상태로 시작한다기에 따로 내리는 연산은 안했다
    이후 매칭하고 -> 매칭이 되었을 시에 땅에 내리고 횟수추가 그리고 다시 매칭한다
    만약 매칭이 안되면 그대로 종료하고 횟수를 출력하게 하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0743
    {

        static void Main743(string[] args)
        {

            int ROW = 12;
            int COL = 6;

            int RED = 'R';
            int GREEN = 'G';
            int BLUE = 'B';
            int PURPLE = 'P';
            int YELLOW = 'Y';

            StreamReader sr;
            int[][] board;
            bool[][] visit;
            Queue<(int r, int c)> q1;
            Queue<(int r, int c)> q2;
            int[] dirR, dirC;

            Solve();

            void Solve()
            {

                Input();

                int ret = 0;

                while (Match())
                {

                    MOVE();
                    ret++;
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                board = new int[ROW][];
                visit = new bool[ROW][];

                for (int r = 0; r < ROW; r++)
                {

                    board[r] = new int[COL];
                    visit[r] = new bool[COL];

                    for (int c = 0; c < COL; c++) 
                    {

                        int cur = sr.Read();
                        if (cur == '.') continue;
                        if (cur == RED) board[r][c] = 1;
                        else if (cur == GREEN) board[r][c] = 2;
                        else if (cur == BLUE) board[r][c] = 3;
                        else if (cur == PURPLE) board[r][c] = 4;
                        else if (cur == YELLOW) board[r][c] = 5;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                q1 = new(ROW * COL);
                q2 = new(ROW * COL);

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                sr.Close();
            }

            void MOVE()
            {

                for (int c = 0; c < COL; c++)
                {

                    int idx = ROW - 1;
                    for (int r = ROW - 1; r >= 0; r--)
                    {

                        if (board[r][c] == 0) continue;
                        board[idx--][c] = board[r][c];
                    }

                    for (int r = idx; r >= 0; r--)
                    {

                        board[r][c] = 0;
                    }
                }
            }

            bool Match()
            {

                bool ret = false;
                for (int r = 0; r < ROW; r++)
                {

                    for (int c = 0; c < COL; c++)
                    {

                        if (visit[r][c]) continue;
                        visit[r][c] = true;
                        int color = board[r][c];
                        if (color == 0) continue;
                        q1.Enqueue((r, c));
                        ret |= BFS(color);
                    }
                }

                for (int r = 0; r < ROW; r++)
                {

                    for (int c = 0; c < COL; c++)
                    {

                        visit[r][c] = false;
                    }
                }
                return ret;
            }

            bool BFS(int _color)
            {

                while(q1.Count > 0)
                {

                    (int r, int c) node = q1.Dequeue();
                    q2.Enqueue(node);
                    
                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC]) continue;

                        if (board[nextR][nextC] != _color) continue;
                        visit[nextR][nextC] = true;
                        q1.Enqueue((nextR, nextC));
                    }
                }

                if (q2.Count < 4)
                {

                    q2.Clear();
                    return false;
                }

                while (q2.Count > 0)
                {

                    var node = q2.Dequeue();
                    board[node.r][node.c] = 0;
                }

                return true;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= ROW || _c >= COL) return true;
                return false;
            }
        }
    }

#if other
namespace ConsoleApp1
{
    internal class Program
    {
        static char[,] map = new char[12, 6];
        static int[] dy = new int[] { 1, 0, -1, 0 };
        static int[] dx = new int[] { 0, 1, 0, -1 };
        static bool[,] visit;
        static int answer;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            for(int i = 0; i < 12; i++)
            {
                string s = input.ReadLine();
                for(int j = 0; j < 6; j++)
                {
                    map[i,j] = s[j];
                }
            }
            int boom = 1;
            while (boom > 0)
            {
                boom = 0;
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (map[i, j] != '.')
                        {
                            boom += puyo(i, j);
                        }
                    }
                }
                if (boom > 0) answer++;
                down();
            }

            output.Write(answer);

            input.Close();
            output.Close();
        }
        static int puyo(int sr, int sc)
        {
            int count = 1;
            visit = new bool[12, 6];
            char c = map[sr, sc];
            Queue<(int, int)> q = new();
            q.Enqueue((sr, sc));
            visit[sr, sc] = true;
            while (q.Count > 0)
            {
                (int row, int col) = q.Dequeue();

                for (int k = 0; k < 4; k++)
                {
                    int nr = row + dy[k];
                    int nc = col + dx[k];
                    if (nr < 0 || nr == 12 || nc < 0 || nc == 6 || visit[nr, nc] || map[nr, nc] != c) continue;
                    q.Enqueue((nr,nc));
                    visit[nr, nc] = true;
                    count++;
                }
            }
            if(count >= 4)
            {
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (visit[i, j])
                            map[i, j] = '.';
                    }
                }
                return 1;
            }
            return 0;
        }
        static void down()
        {
            for (int i = 10; i >= 0; i--)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (map[i,j] != '.')
                    {
                        int row = i;
                        int col = j;
                        while (row + 1 != 12 && map[row + 1,col] == '.')
                        {
                            map[row + 1, col] = map[row, col];
                            map[row, col] = '.';
                            row++;
                        }
                    }
                }
            }
        }
    }
}
#endif
}
