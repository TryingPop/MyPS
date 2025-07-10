using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 숫자판 점프
    문제번호 : 2210번

    브루트포스, DFS 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0415
    {

        static void Main415(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            bool[] use = new bool[1_000_000];

            int[,] board = new int[5, 5];

            for (int r = 0; r < 5; r++)
            {

                for (int c = 0; c < 5; c++)
                {

                    board[r, c] = ReadInt();
                }
            }

            sr.Close();
            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            int ret = 0;
            for (int r = 0; r < 5; r++)
            {

                for (int c = 0; c < 5; c++)
                {

                    ret += DFS(r, c, 0, board[r, c]);
                }
            }

            Console.WriteLine(ret);

            int DFS(int _r, int _c, int _depth, int _n)
            {

                if (_depth == 5)
                {

                    if (use[_n]) return 0;
                    use[_n] = true;
                    return 1;
                }

                int ret = 0;
                for(int i = 0; i < 4; i++)
                {

                    int nR = _r + dirR[i];
                    int nC = _c + dirC[i];

                    if (ChkInvalidPos(nR, nC)) continue;
                    ret += DFS(nR, nC, _depth + 1, _n * 10 + board[nR, nC]);
                }

                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _r >= 5 || _c < 0 || _c >= 5) return true;
                return false;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
