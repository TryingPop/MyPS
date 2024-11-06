using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

/*
날짜 : 2024. 4. 17
이름 : 배성훈
내용 : 1, 2, 3 더하기 4
    문제번호 : 15989번

    dp 문제다
    동전 개수를 찾는 것이기에 동전으로 만들 수 있는 값들을 찾았다
    1원짜리는 무시하고, 2, 3원으로 만들 수 있는 경우를 세었다
    그리고 1원짜리를 보충만하면 다음 숫자를 만들 수 있기에 누적해서 진행했다

    이렇게 제출하니 이상없이 통과했다
    그런데 2, 3의 개수를 반복해서 하기에, 이부분도 dp를 하면 시간 절약이 가능해 보인다

    다른 사람 풀이를 보고 시간을 줄여봤는데 제출해보니 시간은 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0555
    {

        static void Main555(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] dp = new int[10_001];

            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                Init();
                int t = ReadInt();
                while(t-- > 0)
                {

                    int n = ReadInt();
                    sw.WriteLine(dp[n]);
                }
            }

            /*
            void Init()
            {

                // 여기가 시간 많이 잡아먹는 비효율적인 부분
                for (int i = 0; i <= 5_000; i++)
                {

                    for (int j = 0; j <= 3_334; j++)
                    {

                        int cur = 2 * i + 3 * j;
                        if (cur > 10_000) break;
                        dp[cur]++;
                    }
                }



                for (int i = 1; i <= 10_000; i++)
                {

                    dp[i] += dp[i - 1];
                }
            }
            */

            void Init()
            {

                dp[0] = 1;
                for (int i = 1; i <= 3; i++)
                {

                    for (int j = i; j <= 10_000; j++)
                    {

                        dp[j] += dp[j - i];
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
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
using System.Linq;
using System.Text;

namespace Baekjoon.silver
{
    internal class _15989
    {
        static void Main(string[] args)
        {
            StringBuilder stb = new StringBuilder();
            int t = int.Parse(Console.ReadLine());
            int[,] arr = new int[10001, 3];
            arr[1, 0] = arr[2, 0] = 1;
            for(int i = 2; i<10001; i++)
            {
                arr[i, 0] = 1;
                arr[i, 1] = 1 + arr[i-2,1];
                if (i >= 3)
                    arr[i, 2] = 1 + arr[i - 3, 1] + arr[i - 3, 2];
            }

            while(t-- > 0)
            {
                int n = int.Parse(Console.ReadLine());
                stb.AppendLine((1 + arr[n, 1] + arr[n, 2]).ToString());
            }
            Console.WriteLine(stb);
        }
    }
}

#elif other2
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BackJ
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            int repeat = int.Parse(sr.ReadLine());
            int[] dp = new int[10001];

            dp[0] = 0; dp[1] = 1; dp[2] = 2; dp[3] = 3; dp[4] = 4;

            for (int i = 4; i <= 10000; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2] - dp[i - 3];
                if (i % 3 == 0) dp[i] += 1;
            }

            for (int i = 0; i < repeat; i++)
            {
                int N = int.Parse(sr.ReadLine());
                sw.WriteLine(dp[N]);
            }

            sw.Flush();
            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
