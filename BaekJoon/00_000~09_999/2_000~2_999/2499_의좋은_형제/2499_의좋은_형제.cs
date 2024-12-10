using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 10
이름 : 배성훈
내용 : 의좋은 형제
    문제번호 : 2499번

    dp 문제다.
    dp[r][c][x] = val를
    r, c의 행을 택했을 때, 무게가 x인 최소 차이 val를 담는다.
    그러면 dp[r][c][x] = Math.Min_(0 ≤ r' ≤ r) (dp[r'][c + 1][x + sum[r][c]]가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1174
    {

        static void Main1174(string[] args)
        {

            int MAX = 100;
            int INF = 50_000;
            int n;
            int[][] sum;
            int[][][] dp;
            int total;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int ret = DFS(n, 0, 0);
                sw.Write($"{ret}\n");

                int y = n;
                int cost = 0;
                for (int c = 1; c <= n; c++)
                {

                    for (int r = y; r >= 0; r--)
                    {

                        if (dp[r][c][cost + sum[r][c]] != ret) continue;
                        cost += sum[r][c];
                        y = r;
                        sw.Write($"{n - r} ");
                        break;
                    }
                }
                int DFS(int _r, int _c, int _x)
                {

                    ref int ret = ref dp[_r][_c][_x];
                    if (_c == n) return ret = Math.Abs(total - _x - _x);
                    if (ret != -1) return ret;
                    ret = INF;

                    for (int r = _r; r >= 0; r--)
                    {

                        ret = Math.Min(ret, DFS(r, _c + 1, _x + sum[r][_c + 1]));
                    }

                    return ret;
                }
            }

            void SetArr()
            {

                dp = new int[n + 1][][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new int[n + 1][];
                    for (int j = 0; j <= n; j++)
                    {

                        dp[i][j] = new int[n * n * MAX + 1];
                        Array.Fill(dp[i][j], -1);
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                sum = new int[n + 1][];
                
                sum[0] = new int[n + 1];
                total = 0;
                for (int r = 1; r <= n; r++)
                {

                    sum[r] = new int[n + 1];
                    for (int c = 1; c <= n; c++)
                    {

                        int cur = ReadInt();
                        sum[r][c] = cur + sum[r - 1][c];
                        total += cur;
                    }
                }

                sr.Close();
                int ReadInt()
                {

                    int ret = 0;
                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;

                        ret = c - '0';
                        while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
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
