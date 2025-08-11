using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 11
이름 : 배성훈
내용 : 유턴 싫어
    문제번호 : 2823번

    그래프 이론 문제다.
    막다른 길은 방향이 1개인 곳이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1817
    {

        static void Main1817(string[] args)
        {

            int row, col;
            bool[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] || ChkUTurn(r, c)) continue;

                        Console.Write(1);
                        return;
                    }
                }

                Console.Write(0);

                bool ChkUTurn(int _r, int _c)
                {

                    int cnt = 0;
                    for (int i = 0; i < 4; i++)
                    {

                        int nR = _r + dirR[i];
                        int nC = _c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || board[nR][nC]) continue;
                        cnt++;
                    }

                    return cnt != 1;
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();

                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);

                board = new bool[row][];
                for (int r = 0; r < row; r++)
                {

                    string str = sr.ReadLine();
                    board[r] = new bool[col];

                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = str[c] == 'X';
                    }
                }
            }
        }
    }
}
