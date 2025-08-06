using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 6
이름 : 배성훈
내용 : 별
    문제번호 : 16505번

    재귀 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1807
    {

        static void Main1807(string[] args)
        {

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(Console.ReadLine());

            int[] tPow = new int[n + 1];
            tPow[0] = 1;
            for (int i = 1; i <= n; i++)
            {

                tPow[i] = tPow[i - 1] << 1;
            }

            bool[][] board = new bool[tPow[n]][];
            for (int r = 0; r < board.Length; r++)
            {

                board[r] = new bool[tPow[n]];
            }

            DFS(n, 0, 0, true);

            for (int r = 0; r < board.Length; r++)
            {

                for (int c = 0; c < board.Length - r; c++)
                {

                    sw.Write(board[r][c] ? '*' : ' ');
                }

                sw.Write('\n');
            }

            void DFS(int _dep, int _r, int _c, bool _star)
            {

                if (_dep == 0)
                {

                    board[_r][_c] = _star;
                    return;
                }

                _dep--;
                DFS(_dep, _r, _c, _star & true);
                DFS(_dep, _r, _c + tPow[_dep], _star & true);
                DFS(_dep, _r + tPow[_dep], _c, _star & true);
                DFS(_dep, _r + tPow[_dep], _c + tPow[_dep], false);
            }
        }
    }
}
