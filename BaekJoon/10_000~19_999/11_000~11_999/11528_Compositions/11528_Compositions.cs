using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 2
이름 : 배성훈
내용 : Compositions
    문제번호 : 11528번

    dp 문제다.
    조합 형식이면 배낭형식으로 가능하다.
    그런데 여기서 요구하는건 순열 형식이다.

    점화식이 안떠올라 gpt에 도움을 청했다.
    그러니 dp[i] = val를 i숫 자를 만드는 경우의 수 val로 담으면,
    dp형식으로 가능하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1605
    {

        static void Main1605(string[] args)
        {

#if CHAT_GPT


            int P = int.Parse(Console.ReadLine());

            for (int p = 0; p < P; p++)
            {
                string[] tokens = Console.ReadLine().Split();
                int K = int.Parse(tokens[0]);
                int n = int.Parse(tokens[1]);
                int m = int.Parse(tokens[2]);
                int k = int.Parse(tokens[3]);

                // 금지된 수 집합 만들기
                HashSet<int> forbidden = new HashSet<int>();
                for (int i = 0; m + i * k <= n; i++)
                {
                    forbidden.Add(m + i * k);
                }

                // dp[i]는 i를 만드는 조합의 수 (조건 만족하는)
                int[] dp = new int[n + 1];
                dp[0] = 1; // 0을 만드는 방법 1가지 (아무것도 선택하지 않음)

                for (int i = 1; i <= n; i++)
                {
                    dp[i] = 0;
                    for (int j = 1; j <= i; j++)
                    {
                        if (!forbidden.Contains(j))
                        {
                            dp[i] += dp[i - j];
                        }
                    }
                }

                Console.WriteLine($"{K} {dp[n]}");
            }
#else

            int MAX = 30;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] dp = new int[MAX + 1];
            bool[] ban = new bool[MAX + 1];
            int p, n, m, k;

            int t = ReadInt();

            while (t-- > 0)
            {

                Input();

                GetRet();

                sw.Write($"{p} {dp[n]}\n");
            }

            void Input()
            {

                p = ReadInt();
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                Array.Fill(ban, false, 0, n + 1);
                for (int i = m; i <= n; i += k)
                {

                    ban[i] = true;
                }
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }

            void GetRet()
            {

                dp[0] = 1;

                for (int i = 1; i <= n; i++)
                {

                    dp[i] = 0;
                    for (int j = 1; j <= i; j++)
                    {

                        if (ban[j]) continue;
                        dp[i] += dp[i - j];
                    }
                }
            }
#endif


        }
    }
}
