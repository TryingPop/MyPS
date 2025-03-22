using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 무한 수열
    문제번호 : 1351번

    해시와 DFS 탐색으로 해결했다
    해시를 사용한 이유는 입력 수가 10^12승까지이므로 일반적인 dp로는 해결이안된다
*/

namespace BaekJoon.etc
{
    internal class etc_0181
    {

        static void Main181(string[] args)
        {

            long[] info = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            Dictionary<long, long> dp = new(1_000);
            dp[0] = 1;

            long ret = DFS(dp, info, info[0]);

            Console.WriteLine(ret);
        }

        static long DFS(Dictionary<long, long> _dp, long[] _info, long _cur)
        {

            if (_dp.ContainsKey(_cur)) return _dp[_cur];

            _dp[_cur] = 0;

            _dp[_cur] += DFS(_dp, _info, _cur / _info[1]);
            _dp[_cur] += DFS(_dp, _info, _cur / _info[2]);

            return _dp[_cur];
        }
    }
}
