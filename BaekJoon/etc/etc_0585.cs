using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 21
이름 : 배성훈
내용 : 다이나믹이 뭐예요?
    문제번호 : 14494번

    dp 문제다
    1, 1 -> n, m 가는 문제인데
    반대로 가는 경우에도 경우의 수가 같으므로
    n, m -> 1, 1로 가는 경우의 수를 찾아 풀었다
    1보다 작은 수로 가면 1,1로 못가므로 0을 반환하고,
    1, 1 -> 1, 1로 가는 경우는 존재한다는 의미로 1가지라 봤다
    이후로는 누적해서 찾아가니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0585
    {

        static void Main585(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[,] dp;

            Solve();

            void Solve()
            {

                dp = new int[info[0] + 1, info[1] + 1];
                for (int i = 0; i <= info[0]; i++)
                {

                    for (int j = 0; j <= info[1]; j++)
                    {

                        dp[i, j] = -1;
                    }
                }

                dp[1, 1] = 1;
                int ret = DFS(info[0], info[1]);
                Console.WriteLine(ret);
            }

            int DFS(int _n, int _m)
            {

                if (_n < 1 || _m < 1) return 0;

                if (dp[_n, _m] != -1) return dp[_n, _m];
                dp[_n, _m] = 0;

                int ret = DFS(_n - 1, _m);
                ret = (ret + DFS(_n, _m - 1)) % 1_000_000_007;
                ret = (ret + DFS(_n - 1, _m - 1)) % 1_000_000_007;

                dp[_n, _m] = ret;

                return ret;
            }
        }
    }

#if other
uint mod = 1_000_000_007;
var input = Console.ReadLine().Split();
uint width = uint.Parse(input[0]), height = uint.Parse(input[1]);
uint[,] dp = new uint[width+1,height+1];
dp[0,0] = 1;
for(int y=0;y<height;y++) {
    for(int x=0;x<width;x++) {
        uint me = dp[x,y] % mod;
        dp[x+1,y] += me;
        dp[x,y+1] += me;
        dp[x+1,y+1] += me;
    }
}
Console.Write(dp[width,height] % mod);
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOJ_1463
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');
            long.TryParse(input[0], out long x);
            long.TryParse(input[1], out long y);
            if (x < 1 || y < 1) return;
            long[,] map = new long[x, y];
            

            Console.WriteLine(Dp(map, x, y));

        }

        static long Dp(long[,] map, long x, long y)
        {
            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    if (i < 1 || j < 1)
                    {
                        map[i, j] = 1;
                    }
                    else
                    {
                        map[i, j] = (map[i - 1, j - 1] + map[i - 1, j] + map[i, j - 1]) % 1000000007;
                    }
                        //Console.Write(string.Format($"{map[i, j]}\t"));
                }
                //Console.WriteLine();
            }
            return map[x - 1, y - 1];
        }
    }
}

#endif
}
