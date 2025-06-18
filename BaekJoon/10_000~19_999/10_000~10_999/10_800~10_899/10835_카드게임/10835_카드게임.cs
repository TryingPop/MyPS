using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 27
이름 : 배성훈
내용 : 카드게임
    문제번호 : 10835번

    dp문제다
    2차원 배열을 dp로 해서 풀어야하는 문제다
    dp는 왼쪽에 l장, 오른쪽에 r장 꺼냈을 때 최고점수를 값으로 저장했다
    그리고 문제의 조건대로 DFS 탐색을 하면서 dp를 확인하고
    0, 0에서 최대값이 담기게 했다

    해당 아이디어로 푸니 280ms에 통과했다
    다른 사람의 풀이를 보니 이중 포문으로 했고 해당 방법이 2배이상 빠르다
*/

namespace BaekJoon.etc
{
    internal class etc_0353
    {

        static void Main353(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int[,] dp = new int[n, n];

            int[] left = new int[n];
            for (int i = 0; i < n; i++)
            {

                left[i] = ReadInt();
            }

            int[] right = new int[n];
            for (int i = 0; i < n; i++)
            {

                right[i] = ReadInt();
            }

            sr.Close();

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    dp[i, j] = -1;
                }
            }

            int ret = DFS(0, 0);
            Console.WriteLine(ret);

            int DFS(int _l, int _r)
            {

                if (_l >= n || _r >= n) return 0;

                if (dp[_l, _r] != -1) return dp[_l, _r];
                dp[_l, _r] = 0;

                int ret = 0;

                if (left[_l] > right[_r]) ret = DFS(_l, _r + 1) + right[_r];
                int calc = DFS(_l + 1, _r);
                ret = ret < calc ? calc : ret;

                calc = DFS(_l + 1, _r + 1);
                ret = ret < calc ? calc : ret;
                dp[_l, _r] = ret;

                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if other

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackJ
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            int N = int.Parse(sr.ReadLine());
            List<int> left = Array.ConvertAll(sr.ReadLine().Split(), int.Parse).ToList();
            List<int> right = Array.ConvertAll(sr.ReadLine().Split(), int.Parse).ToList();
            int[,] dp = new int[N + 1, N + 1];

            // 왼쪽 카드
            for (int i = N - 1; i >= 0; i--)
            {
                // 오른쪽 카드
                for (int j = N - 1; j >= 0; j--)
                {
                    if (right[j] < left[i])
                        dp[i, j] = Math.Max(dp[i, j + 1] + right[j], Math.Max(dp[i + 1, j], dp[i + 1, j + 1]));
                    else
                        dp[i, j] = Math.Max(dp[i + 1, j], dp[i + 1, j + 1]);
                }
            }

            sw.WriteLine(dp[0, 0]);

            sw.Flush();
            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
