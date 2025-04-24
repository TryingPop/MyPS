using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 24
이름 : 배성훈
내용 : 러키☆한별
    문제번호 : 27314번

    BFS, 브루트포스 문제다.
    사람의 인원이 100명이고, 문의 갯수가 100개이다.
    사람을 기준으로 해도 되나 이경우 사람마다 문을 기록해줘야 한다.
    하지만 문을 기준으로 하면 필요 없어 문을 기준으로 BFS를 진행했다.
    
    먼저 한별이로 BFS를 시작한다.
    그래서 도달할 수 있는 문의 거리를 모두 찾는다.
    이제 도달할 수 있는 문에대해 다시 BFS를 한다.
    그리고 한별이보다 빨리오는 경우의 사람들에 한해 세어주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1572
    {

        static void Main1572(string[] args)
        {

            int row, col;
            string[] board;
            int[][] move;
            (int r, int c)[] person;
            (int r, int c, int dis)[] exit;
            Queue<(int r, int c)> q;
            int[] dirR, dirC;

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i < exit.Length; i++)
                {

                    if (exit[i].dis == -1) continue;

                    BFS(exit[i].r, exit[i].c);

                    int cur = 0;
                    for (int j = 0; j < person.Length; j++)
                    {

                        int p = move[person[j].r][person[j].c];
                        if (p == -1 || exit[i].dis < p) continue;
                        cur++;
                    }

                    ret = Math.Max(cur, ret);
                }

                Console.Write(ret);
            }

            void SetArr()
            {

                q = new(row * col);

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                int exitLen = 0;
                int personLen = 0;
                (int r, int c) h = (-1, -1);
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == 'H') h = (r, c);
                        else if (board[r][c] == 'P') personLen++;
                        else if (board[r][c] == '#') exitLen++;
                    }
                }

                BFS(h.r, h.c);

                exit = new (int r, int c, int dis)[exitLen];
                person = new (int r, int c)[personLen];

                personLen = 0;
                exitLen = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == 'P') person[personLen++] = (r, c);
                        else if (board[r][c] == '#') exit[exitLen++] = (r, c, move[r][c]);
                    }
                }
            }

            void BFS(int _r, int _c)
            {

                InitMove();

                move[_r][_c] = 0;

                q.Enqueue((_r, _c));

                while (q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();
                    int cur = move[node.r][node.c];

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || move[nR][nC] != -1) continue;

                        move[nR][nC] = cur + 1;
                        if (board[nR][nC] == 'X') continue;

                        q.Enqueue((nR, nC));
                    }
                }
            }

            void InitMove()
            {

                for (int i = 0; i < row; i++) Array.Fill(move[i], -1);
            }

            bool ChkInvalidPos(int _r, int _c)
                => _r < 0 || _c < 0 || _r >= row || _c >= col;

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string[] temp = sr.ReadLine().Split();
                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);

                board = new string[row];
                move = new int[row][];

                for (int i = 0; i < row; i++)
                {

                    board[i] = sr.ReadLine();
                    move[i] = new int[col];
                }
            }
        }
    }
}
