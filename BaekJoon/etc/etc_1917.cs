using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 26
이름 : 배성훈
내용 : Bomb
    문제번호 : 26557번

    BFS 문제다.
    단순 6방향으로 이동하며 벽을 깨가면 된다.
    맵의 크기 제한이 없어 List를 이용해 구현했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1917
    {

        static void Main1917(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();
            int floor, row, col;
            List<List<List<int>>> move = new(10);
            List<List<List<bool>>> board = new(10);
            (int f, int r, int c) s, e;
            Queue<(int f, int r, int c)> cur = new(), next = new();
            int[] dirR = { -1, 1, 0, 0, 0, 0 }, dirC = { 0, 0, -1, 1, 0, 0 }, dirF = { 0, 0, 0, 0, -1, 1 };

            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                cur.Enqueue(s);
                move[s.f][s.r][s.c] = 0;

                while (cur.Count > 0)
                {

                    while (cur.Count > 0)
                    {

                        (int f, int r, int c) = cur.Dequeue();

                        for (int i = 0; i < 6; i++)
                        {

                            int nF = f + dirF[i];
                            int nR = r + dirR[i];
                            int nC = c + dirC[i];

                            if (ChkInvalidPos(nF, nR, nC)
                                || move[nF][nR][nC] != -1) continue;

                            if (board[nF][nR][nC])
                            {

                                move[nF][nR][nC] = move[f][r][c] + 1;
                                next.Enqueue((nF, nR, nC));
                            }
                            else
                            {

                                move[nF][nR][nC] = move[f][r][c];
                                cur.Enqueue((nF, nR, nC));
                            }
                        }
                    }

                    var temp = cur;
                    cur = next;
                    next = temp;
                }

                sw.Write($"{move[e.f][e.r][e.c]}\n");

                bool ChkInvalidPos(int f, int r, int c)
                    => f < 0 || r < 0 || c < 0 || f >= floor || r >= row || c >= col;
            }

            void Input()
            {

                s = (-1, -1, -1);
                e = (-1, -1, -1);

                floor = ReadInt();
                row = ReadInt();
                col = ReadInt();

                for (int f = 0; f < floor; f++)
                {

                    if (board.Count == f) 
                    {

                        board.Add(new());
                        move.Add(new());
                    }

                    if (board[f] == null) 
                    { 
                        
                        board[f] = new(); 
                        move[f] = new();
                    }
                    
                    for (int r = 0; r < row; r++)
                    {

                        if (board[f].Count == r)
                        {

                            board[f].Add(new());
                            move[f].Add(new());
                        }

                        if (board[f][r] == null)
                        {

                            board[f][r] = new();
                            move[f][r] = new();
                        }

                        for (int c = 0; c < col; c++)
                        {

                            int cur = ReadBoard();
                            if (cur == 1)
                            {

                                s = (f, r, c);
                                cur = 0;
                            }
                            else if (cur == 2)
                            {

                                e = (f, r, c);
                                cur = 0;
                            }

                            if (board[f][r].Count == c)
                            {

                                board[f][r].Add(cur == -1);
                                move[f][r].Add(-1);
                            }
                            else
                            {

                                board[f][r][c] = cur == -1;
                                move[f][r][c] = -1;
                            }
                        }
                    }
                }
            }

            int ReadBoard()
            {

                int c = sr.Read();
                while (c != 'S' && c != '#' && c != 'E' && c != '.') c = sr.Read();

                if (c == 'S') return 1;
                else if (c == 'E') return 2;
                else if (c == '.') return 0;
                else return -1;
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
