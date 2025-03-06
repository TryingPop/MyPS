using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 6
이름 : 배성훈
내용 : 그림 교환
    문제번호 : 1029번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1378
    {

        static void Main1378(string[] args)
        {

            // 그림 교환 dp 문제
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());

            int[][] prices = new int[n][];
            for (int i = 0; i < n; i++)
            {

                prices[i] = new int[n];
                string str = sr.ReadLine();

                for (int j = 0; j < n; j++)
                {

                    prices[i][j] = str[j] - '0';
                }
            }

            int[][][] dp = new int[n][][];
            for (int i = 0; i < n; i++)
            {

                dp[i] = new int[10][];
                for (int j = 0; j < 10; j++)
                {

                    dp[i][j] = new int[1 << n];
                }
            }

            Console.Write(DFS());

            int DFS(int _cur = 0, int _price = 0, int _state = 1 << 0)
            {

                if (dp[_cur][_price][_state] > 0) return dp[_cur][_price][_state];
                dp[_cur][_price][_state] = 1;

                for (int next = 1; next < n; next++)
                {

                    if ((_state & (1 << next)) != 0 || prices[_cur][next] < _price) continue;
                    dp[_cur][_price][_state] = Math.Max(dp[_cur][_price][_state], DFS(next, prices[_cur][next], _state | 1 << next) + 1);
                }

                return dp[_cur][_price][_state];
            }
        }
    }
}
