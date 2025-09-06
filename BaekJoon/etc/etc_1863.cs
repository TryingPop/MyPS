using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 4
이름 : 배성훈
내용 : Minion Walk
    문제번호 : 10196번

    DFS, BFS 문제다.
    시작지점이 X일 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1863
    {

        static void Main1863(string[] args)
        {

            string Y = "Minions can cross the room\n";
            string N = "Minions cannot cross the room\n";

            int MAX = 20;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int row, col;
            int[][] board = new int[MAX][];
            for (int r = 0; r < MAX; r++)
            {

                board[r] = new int[MAX];
            }
            Queue<(int r, int c)> q = new(MAX * MAX);
            int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };

            int t = ReadInt();

            for (int i = 1; i <= t; i++)
            {

                sw.Write($"Case: {i}\n");
                Input();

                BFS();

                Output();
            }

            void Output()
            {

                for (int r = 0; r < row; r++)
                {

                    DrawTop();

                    sw.Write('|');
                    for (int c = 0; c < col; c++)
                    {

                        char add;
                        if (board[r][c] == 0) add = ' ';
                        else if (board[r][c] == 1) add = 'M';
                        else add = 'X';

                        sw.Write($" {add} |");
                    }

                    sw.Write('\n');
                }

                DrawTop();

                sw.Write(board[row - 1][col - 1] == 1 ? Y : N);
                sw.Flush();

                void DrawTop()
                {

                    sw.Write('+');
                    for (int c = 0; c < col; c++)
                    {

                        sw.Write("---+");
                    }

                    sw.Write('\n');
                }
            }

            void BFS()
            {

                if (board[0][0] == -1) return;
                q.Enqueue((0, 0));
                board[0][0] = 1;

                while (q.Count > 0)
                {

                    (int r, int c) = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = r + dirR[i];
                        int nC = c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC] != 0) continue;
                        board[nR][nC] = 1;
                        q.Enqueue((nR, nC));
                    }
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = sr.Read() == 'O' ? 0 : -1;
                    }

                    while (sr.Read() != '\n') ;
                }
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
