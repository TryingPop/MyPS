using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 15
이름 : 배성훈
내용 : 스티커
    문제번호 : 9465번

    dp 문제다
    그리디와 슬라이딩 윈도우 아이디어로 해결했다

    아이디어는 다음과 같다
    해당 카드를 포함했을 때, 최대 점수를 저장하며 진행한다
    점수가 양수이므로 마지막 줄은 항상 포함해야한다!

    해당 카드를 포함했을 때 최대값은 이전 스티커를 포함했을 때와, 2턴 전의 최대값과 비교해서 큰 쪽에 넣는다
    이전 값은 인접하면 안되므로 인접한 것은 비교 안한다!
    그리고 이를 갱신해가면서 쭉 진행한다

    2턴 전 비교이기에 현재, 이전, 2턴전 이렇게 3개의 자료만 저장하면 된다
    그리고 끝나고나서 끝을 포함한 것 중에 최대값을 찾아 반환하니 112ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0535
    {

        static void Main535(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 8);
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 1024 * 16);
            int[] u = new int[100_000];
            int[] d = new int[100_000];

            // 슬라이딩 윈도우
            int[,] dp = new int[3, 2];

            Solve();
            sr.Close();
            sw.Close();

            void Solve()
            {

                int test = ReadInt();
                
                while(test-- > 0)
                {

                    int n = ReadInt();

                    for (int i = 0; i < n; i++)
                    {

                        u[i] = ReadInt();
                    }

                    for (int i = 0; i < n; i++)
                    {

                        d[i] = ReadInt();
                    }

                    for (int i = 0; i < 3; i++)
                    {

                        for (int j = 0; j < 2; j++)
                        {

                            dp[i, j] = 0;
                        }
                    }


                    for (int i = 0; i < n; i++)
                    {

                        dp[2, 0] = Math.Max(dp[1, 1], dp[0, 1]) + u[i];
                        dp[2, 1] = Math.Max(dp[1, 0], dp[0, 0]) + d[i];

                        Back();
                    }

                    sw.WriteLine(Math.Max(dp[2, 0], dp[2, 1]));
                }
            }

            void Back()
            {

                for (int i = 1; i < 3; i++)
                {

                    for (int j = 0; j < 2; j++)
                    {

                        dp[i - 1, j] = dp[i, j];
                    }
                }
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
using System.Text;

public class Program
{
    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        StringBuilder sb = new();
        for (int i = 0; i < t; i++)
        {
            int n = int.Parse(Console.ReadLine());
            int[][] array = new int[2][];
            array[0] = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            array[1] = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[,] dp = new int[2, n];
            dp[0, 0] = array[0][0];
            dp[1, 0] = array[1][0];
            if (n > 1)
            {
                dp[0, 1] = dp[1, 0] + array[0][1];
                dp[1, 1] = dp[0, 0] + array[1][1];
            }
            for (int j = 2; j < n; j++)
            {
                dp[0, j] = Math.Max(dp[1, j - 1], dp[1, j - 2]) + array[0][j];
                dp[1, j] = Math.Max(dp[0, j - 1], dp[0, j - 2]) + array[1][j];
            }
            sb.Append(Math.Max(dp[0, n - 1], dp[1, n - 1]));
            if (i < t - 1)
                sb.Append('\n');
        }
        Console.Write(sb.ToString());
    }
}
#elif other2
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int t = int.Parse(sr.ReadLine());
while(t-- > 0)
{
    int n = int.Parse(sr.ReadLine());
    int[][] nums = new int[2][];
    nums[0] = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    nums[1] = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

    int[,] sum = new int[2, n + 2];
    sum[0, 0] = nums[0][0];
    sum[1, 0] = nums[1][0];
    if(n > 1)
    {
        sum[0, 1] = nums[0][1] + sum[1, 0];
        sum[1, 1] = nums[1][1] + sum[0, 0];
    }

    for(int i=2; i<n; i++)
    {
        sum[0, i] = nums[0][i] + Math.Max(Math.Max(sum[1, i - 1], sum[1, i - 2]), sum[0, i - 2]);
        sum[1, i] = nums[1][i] + Math.Max(Math.Max(sum[0, i - 1], sum[1, i - 2]), sum[0, i - 2]);
    }
    sw.WriteLine(Math.Max(sum[0, n - 1], sum[1, n - 1]));
}

sw.Flush();
sr.Close();
sw.Close();
#elif other3
var reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int T = int.Parse(reader.ReadLine());
while (T-- > 0)
{
    var N = int.Parse(reader.ReadLine());
    var stickers = new int[2][];

    stickers[0] = reader.ReadLine().Split().Select(int.Parse).ToArray();
    stickers[1] = reader.ReadLine().Split().Select(int.Parse).ToArray();

    var dp = new int[N, 3]; // 0: None, 1: Top, 2: Bottom

    dp[0, 0] = 0;
    dp[0, 1] = stickers[0][0];
    dp[0, 2] = stickers[1][0];

    for (int i = 1; i < N; i++)
    {
        dp[i, 0] = Math.Max(dp[i - 1, 0], Math.Max(dp[i - 1, 1], dp[i - 1, 2]));
        dp[i, 1] = Math.Max(dp[i - 1, 0], dp[i - 1, 2]) + stickers[0][i];
        dp[i, 2] = Math.Max(dp[i - 1, 0], dp[i - 1, 1]) + stickers[1][i];
    }

    writer.WriteLine(Math.Max(dp[N - 1, 0], Math.Max(dp[N - 1, 1], dp[N - 1, 2])));
}

reader.Close();
writer.Close();
#endif
}
