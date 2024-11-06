using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 22
이름 : 배성훈
내용 : 마법사 상어와 토네이도
    문제번호 : 20057번

    구현, 시뮬레이션 문제다
    조건대로 구현하니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_0990
    {

        static void Main990(string[] args)
        {

            StreamReader sr;

            int n;
            int[][] board;
            int[][] dir;
            int[] dirR, dirC;
            int[][] moveR, moveC;
            int[] val;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                // 시뮬레이션
                int curR = (n >> 1) + 2;
                int curC = curR;

                while (curR != 2 || curC != 2)
                {

                    // 다음 이동 방향과 현재 방향
                    int curDir = dir[curR][curC];
                    int nextR = curR + dirR[curDir];
                    int nextC = curC + dirC[curDir];

                    // 이동할 모래량
                    int sand = board[nextR][nextC];
                    board[nextR][nextC] = 0;

                    MoveSand(nextR, nextC, curDir, sand);

                    curR = nextR;
                    curC = nextC;
                }

                int ret = 0;
                for (int r = 0; r < n + 4; r++)
                {

                    for (int c = 0; c < n + 4; c++)
                    {

                        if (r < 2 || c < 2 || r > n + 1 || c > n + 1)
                            ret += board[r][c];
                    }
                }

                Console.Write(ret);
            }

            void MoveSand(int _r, int _c, int _dir, int _val)
            {

                // 모래 이동
                int pop = 0;
                for (int i = 0; i < 9; i++)
                {

                    int nR = _r + moveR[_dir][i];
                    int nC = _c + moveC[_dir][i];

                    int mVal = (val[i] * _val) / 100;

                    board[nR][nC] += mVal;
                    pop += mVal;
                }

                _val -= pop;

                int r = _r + moveR[_dir][9];
                int c = _c + moveC[_dir][9];
                board[r][c] += _val;
            }

            void SetArr()
            {

                moveR = new int[4][];
                moveC = new int[4][];

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                // 모래 이동 퍼센트
                val = new int[9] { 1, 1, 2, 2, 5, 7, 7, 10, 10 };

                // 이동 방향에 따른 좌표들 1%, 1%, 2%, 2%, 5%, 7%, 7%, 10%, 10%, alpha%의 상대 좌표가 있다
                // 북
                moveR[0] = new int[10] { 1, 1, 0, 0, -2, 0, 0, -1, -1, -1 };
                moveC[0] = new int[10] { -1, 1, -2, 2, 0, -1, 1, -1, 1, 0 };

                // 서
                moveR[1] = new int[10] { -1, 1, -2, 2, 0, -1, 1, -1, 1, 0 };
                moveC[1] = new int[10] { 1, 1, 0, 0, -2, 0, 0, -1, -1, -1 };

                // 남
                moveR[2] = new int[10] { -1, -1, 0, 0, 2, 0, 0, 1, 1, 1 };
                moveC[2] = new int[10] { -1, 1, -2, 2, 0, -1, 1, -1, 1, 0 };

                // 동
                moveR[3] = new int[10] { -1, 1, -2, 2, 0, -1, 1, -1, 1, 0 };
                moveC[3] = new int[10] { -1, -1, 0, 0, 2, 0, 0, 1, 1, 1 };

                // 회오리 방향 세팅
                int mid = (n >> 1) + 1;
                for (int r = 2; r < mid + 2; r++)
                {

                    for (int c = 2; c < r; c++)
                    {

                        dir[r][c] = 2;
                    }

                    int e = n + 4 - r;
                    for (int c = r; c < e; c++)
                    {

                        dir[r][c] = 1;
                    }

                    for (int c = e; c < n + 2; c++)
                    {

                        dir[r][c] = 0;
                    }
                }

                for (int r = mid + 2; r < n + 2; r++)
                {

                    int s = n + 3 - r;
                    for (int c = 2; c < s; c++)
                    {

                        dir[r][c] = 2;
                    }

                    int e = r;
                    for (int c = s; c < e; c++)
                    {

                        dir[r][c] = 3;
                    }

                    for (int c = e; c < n + 2; c++)
                    {

                        dir[r][c] = 0;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                board = new int[n + 4][];
                dir = new int[n + 4][];
                for (int i = 0; i < board.Length; i++)
                {

                    board[i] = new int[n + 4];
                    dir[i] = new int[n + 4];
                }

                for (int r = 2; r < n + 2; r++)
                {

                    for (int c = 2; c < n + 2; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                sr.Close();
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
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static int[] dy = new int[] { -1, 1, 0, 0 };
        static int[] dx = new int[] { 0, 0, -1, 1 };
        static int n;
        static int[,] map;
        static int sr;
        static int sc;
        static int answer = 0;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            n = int.Parse(input.ReadLine());
            sr = n / 2; sc = n / 2;
            map = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                for (int j = 0; j < n; j++)
                    map[i, j] = temp[j];
            }
            Move();

            output.Write(answer);

            input.Close();
            output.Close();
        }
        static void Move()
        {
            int[] move = new int[] { 2, 1, 3, 0 };
            int dir = 0;
            int row = sr;
            int col = sc;
            for (int i = 1; i < n; i++)
            {
                int len = 2;
                if (i == n - 1) len++;
                for (int j = 0; j < len; j++)
                {
                    for (int k = 0; k < i; k++)
                    {
                        row += dy[move[dir]];
                        col += dx[move[dir]];
                        sand(row, col, move[dir]);
                    }
                    dir = (dir + 1) % 4;
                }
            }
        }
        static void sand(int row,int col,int dir)
        {
            if(dir == 0 || dir == 1)
            {
                int temp = map[row, col];
                (int, int)[] pos = new (int, int)[] { (row + dy[dir] * 2,col),(row + dy[dir],col + dx[2]), (row + dy[dir], col + dx[3]), (row - dy[dir], col + dx[2]),(row - dy[dir], col + dx[3])
                , (row,col + dx[2]),(row,col + dx[2] * 2),(row,col + dx[3]),(row,col + dx[3] * 2)};
                float[] per = new float[] { 0.05f, 0.1f, 0.1f, 0.01f, 0.01f, 0.07f, 0.02f, 0.07f, 0.02f };
                for(int i = 0; i < 9; i++)
                {
                    (int r, int c) = pos[i];
                    int s = (int)(map[row, col] * per[i]);
                    if(r < 0 || r >= n || c < 0 || c >= n)
                    {
                        answer += s;                        
                    }
                    else
                        map[r, c] += s;
                    temp -= s;
                }
                if (row + dy[dir] < 0 || row + dy[dir] >= n)
                    answer += temp;
                else
                    map[row + dy[dir], col] += temp;
            }
            else
            {
                int temp = map[row, col];
                (int, int)[] pos = new (int, int)[] { (row,col + dx[dir] * 2),(row + dy[0],col + dx[dir]), (row + dy[1], col + dx[dir]), (row + dy[0], col - dx[dir]),(row + dy[1], col - dx[dir])
                , (row + dy[0],col),(row + dy[0] * 2,col),(row + dy[1],col),(row + dy[1] * 2,col)};
                float[] per = new float[] { 0.05f, 0.1f, 0.1f, 0.01f, 0.01f, 0.07f, 0.02f, 0.07f, 0.02f };
                for (int i = 0; i < 9; i++)
                {
                    (int r, int c) = pos[i];
                    int s = (int)(map[row, col] * per[i]);
                    if (r < 0 || r >= n || c < 0 || c >= n)
                    {
                        answer += s;
                    }
                    else
                        map[r, c] += s;
                    temp -= s;
                }
                if (col + dx[dir] < 0 || col + dx[dir] >= n)
                    answer += temp;
                else
                    map[row, col + dx[dir]] += temp;
            }
        }
    }
}
#endif
}
