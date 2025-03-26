using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 25
이름 : 배성훈
내용 : 컴백홈
    문제번호 : 1189번

    브루트포스, 백트래킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1468
    {

        static void Main1468(string[] args)
        {

            int row, col, k;
            bool[][] visit, board;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                visit[row - 1][0] = true;
                Console.Write(DFS(row - 1, 0));

                int DFS(int _r, int _c, int _dep = 1)
                {

                    if (_dep == k)
                    {

                        if (_r == 0 && _c == col - 1) return 1;
                        else return 0;
                    }

                    int ret = 0;
                    for (int i = 0; i < 4; i++)
                    {

                        int nR = _r + dirR[i];
                        int nC = _c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC] || visit[nR][nC]) continue;
                        visit[nR][nC] = true;
                        ret += DFS(nR, nC, _dep + 1);
                        visit[nR][nC] = false;
                    }

                    return ret;
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _r >= row || _c < 0 || _c >= col;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();
                k = ReadInt();

                board = new bool[row][];
                visit = new bool[row][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new bool[col];
                    visit[r] = new bool[col];
                }

                for (int r = 0; r < row; r++)
                {

                    string temp = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        if (temp[c] == 'T') board[r][c] = true;
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = ret * 10 + c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
