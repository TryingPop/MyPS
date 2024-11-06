using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 21
이름 : 배성훈
내용 : 로마 카톨릭 미사
    문제번호 : 9518번

    구현, 브루트포스 문제다
    현재 앉은 사람들의 악수 횟수와
    빈 자리에 넣었을 때, 악수횟수를 함께 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0766
    {

        static void Main766(string[] args)
        {

            int[][] board;
            int[] dirR, dirC;
            int row, col;

            Solve();

            void Solve()
            {

                Input();

                int ret = 0;
                int max = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = GetMeet(r, c);
                        if (board[r][c] == 0) max = Math.Max(max, cur);
                        else ret += cur;
                    }
                }

                ret /= 2;
                Console.Write(ret + max);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string[] temp = sr.ReadLine().Split();

                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == '.') continue;
                        board[r][c] = 1;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();

                dirR = new int[8] { -1, -1, 0, 1, 1, 1, 0, -1 };
                dirC = new int[8] { 0, 1, 1, 1, 0, -1, -1, -1 };
            }

            int GetMeet(int _r, int _c)
            {

                int ret = 0;
                for (int i = 0; i < 8; i++)
                {

                    int nextR = _r + dirR[i];
                    int nextC = _c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] == 0) continue;
                    ret++;
                }

                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }
        }
    }
}
