using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 피자(Small), 피자(Large)
    문제번호 : 14606번, 14607번

    dp 문제다
    dp를 이용해 조건대로 구현했다

    처음에는 배열로 하다가 Large 문제를 보고 10^9승까지 들어오기에
    Dictionary로 dp를 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0192
    {

        static void Main192(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            Dictionary<long, long> dp = new(100);

            dp[2] = 1;
            dp[0] = 0;
            dp[1] = 0;

            long ret = DFS(dp, n);

            Console.WriteLine(ret);
        }

        static long DFS(Dictionary<long, long> _dp, long _cur)
        {

            if (_dp.ContainsKey(_cur))
            {

                return _dp[_cur];
            }

            long half = _cur / 2;
            long other = _cur - half;
            _dp[_cur] = half * other + DFS(_dp, half) + DFS(_dp, other);
            return _dp[_cur];
        }
    }
}
