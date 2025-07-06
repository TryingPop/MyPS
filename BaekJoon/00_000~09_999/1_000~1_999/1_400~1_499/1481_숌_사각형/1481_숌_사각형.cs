using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 6
이름 : 배성훈
내용 : 숌 사각형
    문제번호 : 1481번

    백트래킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1750
    {

        static void Main1750(string[] args)
        {

            int n, d;
            int[][] ret;

            Input();

            GetRet();

            Output();

            void Output()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        sw.Write($"{ret[r][c]} ");
                    }

                    sw.Write('\n');
                }
            }

            void GetRet()
            {

                int[] row = new int[n], col = new int[n];
                int m = n - d + 1;

                ret = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    ret[i] = new int[n];
                    Array.Fill(ret[i], -1);
                }

                for (int i = 0; i < m; i++)
                {

                    for (int j = n - 1, val = d - 1; j >= 0; j--)
                    {

                        ret[i][j] = val;
                        ret[j][i] = val;

                        row[j] |= 1 << val;
                        col[j] |= 1 << val;
                        val = val == 0 ? val : val - 1;
                    }
                }

                DFS(m, m);

                bool DFS(int _r, int _c)
                {

                    if (_c == n)
                    {

                        _c = m;
                        _r++;
                    }

                    if (_r >= n) return true;

                    int cur = row[_r] | col[_c];
                    for (int i = 0; i < d; i++)
                    {

                        int b = 1 << i;
                        if ((cur & b) != 0) continue;
                        row[_r] ^= b;
                        col[_c] ^= b;

                        ret[_r][_c] = i;
                        if (DFS(_r, _c + 1)) return true;

                        row[_r] ^= b;
                        col[_c] ^= b;
                    }

                    return false;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                d = int.Parse(temp[1]);
            }
        }
    }
}
