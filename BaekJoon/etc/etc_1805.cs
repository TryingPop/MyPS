using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 4
이름 : 배성훈
내용 : 꿀벌 승연이
    문제번호 : 23083번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1805
    {

        static void Main1805(string[] args)
        {

            int MOD = 1_000_000_007;
            int row, col;
            bool[][] hole;
            int[][] dp;

            Input();

            SetDp();

            GetRet();

            void GetRet()
            {

                int[] dirR1 = { 0, 1, 1 }, dirC1 = { 1, 1, 0 };
                int[] dirR2 = { -1, 0, 1 }, dirC2 = { 1, 1, 0 };

                for (int c = 1; c <= col; c++)
                {

                    for (int r = 1; r <= row; r++)
                    {

                        if ((c & 1) == 0) Move(r, c, dirR1, dirC1);
                        else Move(r, c, dirR2, dirC2);
                    }
                }

                Console.Write(dp[row][col]);

                void Move(int _r, int _c, int[] _dirR, int[] _dirC)
                {

                    for (int i = 0; i < 3; i++)
                    {

                        int nR = _r + _dirR[i];
                        int nC = _c + _dirC[i];

                        if (ChkInvalidPos(nR, nC) || hole[nR][nC]) continue;
                        dp[nR][nC] = (dp[nR][nC] + dp[_r][_c]) % MOD;
                    }
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 1 || _c < 1 || _r > row || _c > col;
            }

            void SetDp()
            {

                dp = new int[row + 1][];
                for (int r = 1; r <= row; r++)
                {

                    dp[r] = new int[col + 1];
                }

                dp[1][1] = 1;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                hole = new bool[row + 1][];
                for (int r = 1; r <= row; r++)
                {

                    hole[r] = new bool[col + 1];
                }

                int k = ReadInt();

                for (int i = 0; i < k; i++)
                {

                    int r = ReadInt();
                    int c = ReadInt();

                    hole[r][c] = true;
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
