using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : 나이트 투어
    문제번호 : 1331번

    구현, 시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1437
    {

        static void Main1437(string[] args)
        {

            Solve();
            void Solve()
            {

                string N = "Invalid";
                string Y = "Valid";

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                bool[][] board = new bool[6][];
                for (int i = 0; i < 6; i++) 
                {

                    board[i] = new bool[6];
                }

                string cur = sr.ReadLine();
                GetPos(cur, out int cR, out int cC);
                board[cR][cC] = true;
                int iR = cR, iC = cC;

                while (true)
                {

                    cur = sr.ReadLine();
                    if (cur == "" || cur == null) break;
                    GetPos(cur, out int nR, out int nC);

                    if (ChkNext(nR, nC) && !board[nR][nC])
                    {

                        board[nR][nC] = true;
                        cR = nR;
                        cC = nC;
                        continue;
                    }

                    Console.Write(N);
                    return;
                }

                for (int i = 0; i < 6; i++)
                {

                    for (int j = 0; j < 6; j++)
                    {

                        if (board[i][j]) continue;
                        Console.Write(N);
                        return;
                    }
                }
                if (ChkNext(iR, iC)) Console.Write(Y);
                else Console.Write(N);

                bool ChkNext(int _nR, int _nC)
                {

                    int sR = cR - _nR;
                    int sC = cC - _nC;
                    sR = sR < 0 ? -sR : sR;
                    sC = sC < 0 ? -sC : sC;

                    return (sR == 1 && sC == 2) || (sR == 2 && sC == 1);
                }

                void GetPos(string _str, out int _r, out int _c)
                {

                    _r = _str[0] - 'A';
                    _c = _str[1] - '1';
                }
            }
        }
    }
}
