using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 25
이름 : 배성훈
내용 : 직사각형으로 나누기
    문제번호 : 1451번

    브루트포스, 누적합 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1295
    {

        static void Main1295(string[] args)
        {

            int row, col;
            int[][] sums;

            Solve();
            void Solve()
            {

                Input();

                SetSums();

                GetRet();
            }

            void GetRet()
            {

                long ret = 0;
                // 만들 수 있는 형태는 크게 6가지이다.
                for (int r = 1; r < row; r++)
                {

                    for (int c = 1; c < col; c++)
                    {

                        ret = Math.Max(Chk1(r, c), ret);
                        ret = Math.Max(Chk2(r, c), ret);
                    }
                }

                for (int c = 1; c < col; c++)
                {

                    for (int r = 1; r < row; r++)
                    {

                        ret = Math.Max(Chk3(r, c), ret);
                        ret = Math.Max(Chk4(r, c), ret);
                    }
                }

                // 50 크기라 N^2 으로 찾는다.
                // 두 포인터 알고리즘 쓰면 N에 찾을 수 있다.
                for (int top = 1; top < row; top++)
                {

                    for (int bot = top + 1; bot < row; bot++)
                    {

                        long area = 1L * sums[top][col]
                            * (sums[bot][col] - sums[top][col])
                            * (sums[row][col] - sums[bot][col]);

                        ret = Math.Max(ret, area);
                    }
                }

                for (int left = 1; left < col; left++)
                {

                    for (int right = left + 1; right < col; right++)
                    {

                        long area = 1L * sums[row][left]
                            * (sums[row][right] - sums[row][left])
                            * (sums[row][col] - sums[row][right]);

                        ret = Math.Max(ret, area);
                    }
                }

                Console.Write(ret);

                long Chk1(int _r, int _c)
                {

                    long area1 = sums[_r][_c];
                    long area2 = sums[_r][col] - sums[_r][_c];
                    long area3 = sums[row][col] - sums[_r][col];

                    return area1 * area2 * area3;
                }

                long Chk2(int _r, int _c)
                {

                    long area1 = sums[_r][col];
                    long area2 = sums[row][_c] - sums[_r][_c];
                    long area3 = sums[row][col] - sums[_r][col] - area2;

                    return area1 * area2 * area3;
                }

                long Chk3(int _r, int _c)
                {

                    long area1 = sums[row][_c];
                    long area2 = sums[_r][col] - sums[_r][_c];
                    long area3 = sums[row][col] - sums[row][_c] - area2;

                    return area1 * area2 * area3;
                }

                long Chk4(int _r, int _c)
                {

                    long area1 = sums[_r][_c];
                    long area2 = sums[row][_c] - area1;
                    long area3 = sums[row][col] - sums[row][_c];

                    return area1 * area2 * area3;
                }
            }

            void SetSums()
            {

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        sums[r][c] += sums[r][c - 1];
                    }
                }

                for (int c = 1; c <= col; c++)
                {

                    for (int r = 1; r <= row; r++)
                    {

                        sums[r][c] += sums[r - 1][c];
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string temp = sr.ReadLine();
                int[] size = Array.ConvertAll(temp.Split(), int.Parse);

                row = size[0];
                col = size[1];
                sums = new int[row + 1][];
                sums[0] = new int[col + 1];
                for (int r = 1; r <= row; r++)
                {

                    sums[r] = new int[col + 1];
                    temp = sr.ReadLine();

                    for (int c = 0; c < col; c++)
                    {

                        sums[r][c + 1] = temp[c] - '0';
                    }
                }
            }
        }
    }
}
