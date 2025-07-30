using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 29
이름 : 배성훈
내용 : 미로 탈출하기
    문제번호 : 17090번

    그래프 이론 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1793
    {

        static void Main1793(string[] args)
        {


            int row, col;
            int[][] dir, go;
            int[] dirR, dirC;
            (int r, int c)[] stk;

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                int ret = 0;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (go[r][c] != 0) continue;
                        Move(r, c);
                    }
                }

                Console.Write(ret);

                void Move(int _r, int _c)
                {

                    int len = 0;
                    bool flag = false;
                    while (go[_r][_c] == 0)
                    {

                        stk[len++] = (_r, _c);
                        int d = dir[_r][_c];
                        go[_r][_c] = -1;
                        _r += dirR[d];
                        _c += dirC[d];

                        if (ChkInvalidPos(_r, _c))
                        {

                            flag = true;
                            break;
                        }
                    }

                    if (flag || go[_r][_c] == 1)
                    {

                        while (len-- > 0)
                        {

                            ret++;
                            (_r, _c) = stk[len];
                            go[_r][_c] = 1;
                        }
                    }

                    bool ChkInvalidPos(int _r, int _c)
                        => _r < 0 || _c < 0 || _r >= row || _c >= col;
                }
            }

            void SetArr()
            {

                stk = new (int r, int c)[row * col];
                dirR = new int[4] { -1, 1, 0, 0 };
                dirC = new int[4] { 0, 0, -1, 1 };

                go = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    go[r] = new int[col];
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string temp = sr.ReadLine();
                {

                    string[] spStr = temp.Split();
                    row = int.Parse(spStr[0]);
                    col = int.Parse(spStr[1]);
                }

                dir = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    temp = sr.ReadLine();
                    dir[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        switch (temp[c])
                        {

                            case 'U':
                                dir[r][c] = 0;
                                break;
                            case 'D':
                                dir[r][c] = 1;
                                break;
                            case 'L':
                                dir[r][c] = 2;
                                break;
                            default:
                                dir[r][c] = 3;
                                break;
                        }
                    }
                }
            }
        }
    }
}
