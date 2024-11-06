using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 21
이름 : 배성훈
내용 : 파스칼 삼각형
    문제번호 : 16395번

    수학, 조합론, dp문제다
    dp를 안쓰고 DFS로 해봤는데 시간이 280ms다
    반면 dp를 쓰니 64ms로 줄었다
*/

namespace BaekJoon.etc
{
    internal class etc_0586
    {

        static void Main586(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[,] dp = new int[31, 31];
            Solve();

            void Solve()
            {

                int ret = Pascal(input[0], input[1]);
                Console.WriteLine(ret);
            }

            int Pascal(int _n, int _m)
            {

                if (_n <= 1 || _m <= 1 || _n == _m) return 1;
                if (dp[_n, _m] != 0) return dp[_n, _m];
                int ret = Pascal(_n - 1, _m - 1) + Pascal(_n - 1, _m);
                dp[_n, _m] = ret;
                return ret;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backjoon
{
    internal class _16395
    {
        static void Main()
        {
            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[,] array = new int[31, 31];
            for (int i = 1; i <= 30; i++)
            {
                array[i, i] = 1;
                array[i, 1] = 1;
            }
            for (int i = 2; i < 31; i++)
            {
                for (int j = 2; j < 31; j++)
                {
                    array[i, j] = array[i - 1, j] + array[i-1, j - 1];
                }
            }
            Console.WriteLine(array[input[0], input[1]]);
        }
    }
}
#endif
}
