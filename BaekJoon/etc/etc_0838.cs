using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 25
이름 : 배성훈
내용 : 마리오 파티
    문제번호 : 14550번

    dp, 배낭문제다
    초기화를 신경안써 arr의 n + 1의 값으로 
    예제에서 잠깐 막혔다
*/

namespace BaekJoon.etc
{
    internal class etc_0838
    {

        static void Main838(string[] args)
        {

            int MIN = -10_000_000;
            StreamReader sr;
            StreamWriter sw;

            int[] arr;
            int[] dp;
            int n, s, turn;

            Solve();
            void Solve()
            {

                Init();

                while(Input())
                {

                    arr[n + 1] = 0;
                    dp[n + 1] = MIN;

                    for (int t = 0; t < turn; t++)
                    {

                        for (int i = n; i >= 0; i--)
                        {

                            if (dp[i] == MIN) continue;

                            for (int j = s; j > 0; j--)
                            {

                                int next = i + j;
                                if (n + 1 < next) continue;
                                dp[next] = Math.Max(dp[next], dp[i] + arr[next]);
                            }
                        }
                    }

                    sw.Write($"{dp[n + 1]}\n");
                }

                sw.Close();
                sr.Close();
            }

            bool Input()
            {

                n = ReadInt();
                if (n == 0) return false;
                s = ReadInt();
                turn = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    dp[i] = MIN;
                }

                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new int[202];
                dp = new int[202];
                s = 0;
                turn = 0;
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using System;
using System.Text;
using System.Collections.Generic;

public class Program
{
    const int inf = -10000000;
    static void Main()
    {
        StringBuilder sb = new();
        while (true)
        {
            int[] nst = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            if (nst.Length == 1)
                break;
            int n = nst[0], s = nst[1], t = nst[2];
            List<int> list = new();
            while (list.Count < n)
            {
                list.AddRange(Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse));
            }
            list.Insert(0, 0);
            list.Add(0);
            int[,] dp = new int[t + 1, n + 2];
            for (int i = 1; i <= n + 1; i++)
            {
                dp[1, i] = i <= s ? list[i] : inf;
            }
            int answer = dp[1, n + 1];
            for (int i = 2; i <= t; i++)
            {
                for (int j = 1; j <= n + 1; j++)
                {
                    dp[i, j] = inf;
                    for (int k = 1; k <= s; k++)
                    {
                        if (j >= k)
                            dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j - k] + list[j]);
                    }
                }
                answer = Math.Max(answer, dp[i, n + 1]);
            }
            sb.Append(answer).Append('\n');
        }
        sb.Remove(sb.Length - 1, 1);
        Console.Write(sb.ToString());
    }
}
#endif
}
