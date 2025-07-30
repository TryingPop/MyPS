using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaekJoon.etc
{
    internal class etc_1781
    {

        static void Main1781(string[] args)
        {

            // 23083 - 꿀벌 승연이
            // 현재 오답이다.
            int MOD = 1_000_000_007;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int row = ReadInt();
            int col = ReadInt();

            int k = ReadInt();
            (int r, int c)[] hole = new (int r, int c)[k + 1];
            for (int i = 0; i < k; i++)
            {

                hole[i] = (ReadInt(), ReadInt());
            }

            hole[k] = (col + 1, row + 1);

            Array.Sort(hole, (x, y) => 
            {

                int ret = x.c.CompareTo(y.c);
                if (ret == 0) ret = x.r.CompareTo(y.r);
                return ret;
            });

            int[][] dp = new int[2][];
            dp[0] = new int[row + 1];
            dp[1] = new int[row + 1];

            dp[0][1] = 1;
            int idx = 0;

            for (int c = 1; c < col; c++)
            {

                // 아래로 이동
                for (int r = 1; r <= row; r++)
                {

                    if (ChkHole(r, c)) 
                    {

                        idx++;
                        continue; 
                    }

                    // 오른쪽 이동
                    dp[1][r] = (dp[1][r] + dp[0][r]) % MOD;

                    if (r != row) dp[0][r + 1] = (dp[0][r + 1] + dp[0][r]) % MOD;

                    if ((c & 1) == 1) 
                    { 
                        
                        if (r > 1) dp[1][r - 1] = (dp[1][r - 1] + dp[0][r]) % MOD; 
                    }
                    else
                    {

                        if (r < row) dp[1][r + 1] = (dp[1][r + 1] + dp[0][r]) % MOD;
                    }
                }

                for (int r = 1; r <= row; r++)
                {

                    dp[0][r] = dp[1][r];
                    dp[1][r] = 0;
                }
            }

            for (int r = 1; r < row; r++)
            {

                if (ChkHole(r, col)) 
                {

                    idx++;
                    continue; 
                }
                dp[0][r + 1] = (dp[0][r + 1] + dp[0][r]) % MOD;
            }

            Console.Write(dp[0][row]);

            bool ChkHole(int _r, int _c)
                => hole[idx].r == _r && hole[idx].c == _c;

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
