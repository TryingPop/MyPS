using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 27
이름 : 배성훈
내용 : 킹
    문제번호 : 1063번

    구현, 시뮬레이션 문제다4
    명령은 50개 이하이므로 조건대로 시뮬레이션 돌려 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_1084
    {

        static void Main1084(string[] args)
        {

            StreamReader sr;

            Dictionary<string, (int r, int c)> move;
            int[][] board;
            int n;
            (int r, int c) king, dol;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 0; i < n; i++)
                {

                    string op = sr.ReadLine();

                    (int r, int c) dir = move[op];
                    int nR = king.r + dir.r;
                    int nC = king.c + dir.c;
                    if (ChkDol(nR, nC))
                    {

                        int dnR = dol.r + dir.r;
                        int dnC = dol.c + dir.c;
                        if (ChkInvalidPos(dnR, dnC)) continue;
                        dol.r = dnR;
                        dol.c = dnC;

                        king.r = nR;
                        king.c = nC;
                    }
                    else if (ChkInvalidPos(nR, nC)) continue;
                    else
                    {

                        king.r = nR;
                        king.c = nC;
                    }
                }

                sr.Close();
                Console.Write($"{(char)(king.c + 'A')}{8 - king.r}\n{(char)(dol.c + 'A')}{8 - dol.r}");
            }

            bool ChkDol(int _r, int _c)
            {

                return dol.r == _r && dol.c == _c;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                move = new(8);
                move["R"] = (0, 1);
                move["L"] = (0, -1);
                move["B"] = (1, 0);
                move["T"] = (-1, 0);
                move["RT"] = (-1, 1);
                move["LT"] = (-1, -1);
                move["RB"] = (1, 1);
                move["LB"] = (1, -1);

                board = new int[8][];
                for (int r = 0; r < 8; r++)
                {

                    board[r] = new int[8];
                }

                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[2]);

                king = GetPos(temp[0]);
                dol = GetPos(temp[1]);
            }

            (int r, int c) GetPos(string _pos)
            {

                return (8 - _pos[1] + '0', _pos[0] - 'A');
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= 8 || _c >= 8;
            }
        }
    }
}
