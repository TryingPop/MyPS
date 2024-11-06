using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 오목, 이길 수 있을까?
    문제번호 : 16955번

    구현, 브루트포스 문제다
    빈 공간에 모두 놓아본다
    그리고 승리하는지 판별했다
*/

namespace BaekJoon.etc
{
    internal class etc_0441
    {

        static void Main441(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[,] board = new int[10, 10];

            for (int i = 0; i < 10; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    board[i, j] = sr.Read();
                }

                sr.ReadLine();
            }

            sr.Close();
            bool ret = false;
            for (int i = 0; i < 10; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    if (board[i, j] != '.') continue;
                    board[i, j] = 'X';
                    ret = Find(i, j);
                    if (ret) break;
                    board[i, j] = '.';
                }

                if (ret) break;
            }

            Console.WriteLine(ret ? 1 : 0);

            bool Find(int _r, int _c)
            {

                int link = 0;
                for (int i = _r - 1; i >= 0; i--)
                {

                    if (board[i, _c] != 'X') break;
                    link++;
                }

                for (int i = _r; i < 10; i++)
                {

                    if (board[i, _c] != 'X') break;
                    link++;
                }

                if (link >= 5) return true;
                link = 0;
                for (int i = _c - 1; i >= 0; i--)
                {

                    if (board[_r, i] != 'X') break;
                    link++;
                }

                for (int i = _c; i < 10; i++)
                {

                    if (board[_r, i] != 'X') break;
                    link++;
                }

                if (link >= 5) return true;
                link = 0;
                for (int i = 1; i <= 4; i++)
                {

                    int nextR = _r - i;
                    int nextC = _c - i;
                    if (ChkInValidPos(nextR, nextC) || board[nextR, nextC] != 'X') break;
                    link++;
                }

                for (int i = 0; i <= 4; i++)
                {

                    int nextR = _r + i;
                    int nextC = _c + i;
                    if (ChkInValidPos(nextR, nextC) || board[nextR, nextC] != 'X') break;
                    link++;
                }

                if (link >= 5) return true;
                link = 0;
                
                for (int i = 1; i <= 4; i++)
                {

                    int nextR = _r + i;
                    int nextC = _c - i;

                    if (ChkInValidPos(nextR, nextC) || board[nextR, nextC] != 'X') break;
                    link++;
                }

                for (int i = 0; i <= 4; i++)
                {

                    int nextR = _r - i;
                    int nextC = _c + i;

                    if (ChkInValidPos(nextR, nextC) || board[nextR, nextC] != 'X') break;
                    link++;
                }

                if (link >= 5) return true;
                return false;
            }

            bool ChkInValidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= 10 || _c >= 10) return true;
                return false;
            }
        }
    }
}
