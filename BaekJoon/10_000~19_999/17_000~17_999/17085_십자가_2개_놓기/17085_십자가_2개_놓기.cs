using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 6
이름 : 배성훈
내용 : 십자가 2개 놓기
    문제번호 : 17085번

    브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1683
    {

        static void Main1683(string[] args)
        {

            int n, m;
            bool[][] board;
            int[][] use;

            Input();

            GetRet();

            void GetRet()
            {

                int MAX_SIZE = 7;
                int[] dirR = { -1, 0, 1, 0 }, dirC = { 0, -1, 0, 1 };
                int ret = 1;

                FindPos1();

                Console.Write(ret);

                void FindPos1()
                {

                    for (int r = 0; r < n; r++)
                    {

                        for (int c = 0; c < m; c++)
                        {

                            if (board[r][c]) FindPos2(r, c);
                        }
                    }
                }

                void FindPos2(int _r, int _c)
                {

                    for (int r = _r; r < n; r++)
                    {

                        int s = r == _r ? _c + 1 : 0;
                        for (int c = s; c < m; c++)
                        {

                            if (board[r][c]) Find(_r, _c, r, c);
                        }
                    }
                }

                void Find(int _r1, int _c1, int _r2, int _c2)
                {

                    use[_r1][_c1] = 1;

                    for (int i = 1; i <= MAX_SIZE; i++)
                    {

                        bool flag = false;
                        for (int j = 0; j < 4; j++)
                        {

                            int nR = _r1 + i * dirR[j];
                            int nC = _c1 + i * dirC[j];

                            if (ChkInvalidPos(nR, nC) || !board[nR][nC])
                            {

                                flag = true;
                                break;
                            }

                            use[nR][nC] = 1;
                        }

                        if (flag || use[_r2][_c2] == 1) break;
                        ret = Math.Max(ret, 4 * i + 1);

                        for (int j = 1; j <= MAX_SIZE; j++)
                        {

                            flag = false;
                            for (int k = 0; k < 4; k++)
                            {

                                int nR = _r2 + j * dirR[k];
                                int nC = _c2 + j * dirC[k];

                                if (ChkInvalidPos(nR, nC) || !board[nR][nC] || use[nR][nC] > 0)
                                {

                                    flag = true;
                                    break;
                                }
                            }

                            if (flag) break;
                            ret = Math.Max(ret, (4 * i + 1) * (4 * j + 1));
                        }
                    }

                    use[_r1][_c1] = 0;
                    for (int i = 1; i <= MAX_SIZE; i++)
                    {

                        bool flag = false;
                        for (int j = 0; j < 4; j++)
                        {

                            int nR = _r1 + i * dirR[j];
                            int nC = _c1 + i * dirC[j];

                            if (ChkInvalidPos(nR, nC) || use[nR][nC] == 0)
                            {

                                flag = true;
                                break;
                            }

                            use[nR][nC] = 0;
                        }

                        if (flag) break;
                    }

                    bool ChkInvalidPos(int _r, int _c)
                        => _r < 0 || _c < 0 || _r >= n || _c >= m;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string[] size = sr.ReadLine().Split();

                n = int.Parse(size[0]);
                m = int.Parse(size[1]);

                board = new bool[n][];
                use = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    board[i] = new bool[m];
                    use[i] = new int[m];
                    string temp = sr.ReadLine();
                    for (int j = 0; j < m; j++)
                    {

                        board[i][j] = temp[j] == '#';
                    }
                }
            }
        }
    }
}
