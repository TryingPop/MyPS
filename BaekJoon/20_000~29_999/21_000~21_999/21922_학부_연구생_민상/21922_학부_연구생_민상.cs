using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 학부 연구생 민상
    문제번호 : 21922번

    구현, 시뮬레이션 문제다
    시작 지점만 큐로 저장하고 시뮬레이션 돌렸다

    행과 열의 크기를 잘못 봐서, 한 번 틀렸다;
    세로 크기를 N이라 해서 세로의 길이로 봤는데;
    행의 개수를 의미하는 것이었다

    368ms로 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0297
    {

        static void Main297(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 1024);

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[,] board = new int[row, col];

            Queue<(int r, int c)> q = new(2000);
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    int cur = ReadInt(sr);

                    board[r, c] = cur;

                    if (cur == 9) q.Enqueue((r, c));
                }
            }

            sr.Close();

            int[] dirR = { -1, 0, 1, 0 };
            int[] dirC = { 0, 1, 0, -1 };

            bool[,] visit = new bool[row, col];
            while(q.Count > 0)
            {

                var cur = q.Dequeue();
                visit[cur.r, cur.c] = true;

                for (int i = 0; i < 4; i++)
                {

                    int dir = i;
                    var go = cur;

                    while (true)
                    {

                        // 찬 바람 이동
                        go.r += dirR[dir];
                        go.c += dirC[dir];

                        // 유효한 좌표?
                        if (ChkInvalidPos(go.r, go.c, row, col)) break;

                        // 방문 ++
                        visit[go.r, go.c] = true;
                        int val = board[go.r, go.c];
                        if (val != 0) dir = TurnDir(val, dir);
                        if (dir == -1) break;
                    }
                }
            }

            int ret = 0;
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    if (visit[r, c]) ret++;
                }
            }

            Console.WriteLine(ret);
        }
        
        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            if (_r < 0 || _c < 0 || _r >= _row || _c >= _col) return true;
            return false;
        }
        

        static int TurnDir(int _turn, int _dir)
        {

            if (_turn == 9) return -1;

            if (_turn == 1)
            {

                if (_dir == 0 || _dir == 2) return _dir;
                return -1;
            }
            else if (_turn == 2)
            {

                if (_dir == 0 || _dir == 2) return -1;
                return _dir;
            }
            else if (_turn == 3)
            {

                if (_dir == 0) return 1;
                else if (_dir == 1) return 0;
                else if (_dir == 2) return 3;
                else return 2;
            }
            else
            {

                if (_dir == 0) return 3;
                else if (_dir == 1) return 2;
                else if (_dir == 2) return 1;
                else return 0;
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
