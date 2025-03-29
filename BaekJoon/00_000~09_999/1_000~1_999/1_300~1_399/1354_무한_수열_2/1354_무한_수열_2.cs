using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 29
이름 : 배성훈
내용 : 무한 수열 2
    문제번호 : 1354번

    dp, 해시 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1492
    {

        static void Main1492(string[] args)
        {

            Dictionary<long, long> dp = new(100_000);
            // 15초 걸린다;
            // SortedDictionary<long, long> dp = new();
            long n, p, q, x, y;

            Input();

            Console.Write(DFS(n));

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = long.Parse(temp[0]);
                p = long.Parse(temp[1]);
                q = long.Parse(temp[2]);
                x = long.Parse(temp[3]);
                y = long.Parse(temp[4]);
            }

            long DFS(long _cur)
            {

                if (_cur <= 0) return 1;

                if (dp.ContainsKey(_cur)) return dp[_cur];
                dp[_cur] = DFS((_cur / p) - x) + DFS((_cur / q) - y);

                return dp[_cur];
            }
        }
    }
}
