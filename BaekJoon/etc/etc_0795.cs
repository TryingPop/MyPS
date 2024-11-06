using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 5
이름 : 배성훈
내용 : 비밀번호 만들기
    문제번호 : 17218번

    dp, 문자열 문제다
    lcs 알고리즘을 써서 풀었다
    그리고 문자열의 길이가 최대 40이기에
    따로 방향을 기록한게 아닌 비트마스킹으로 일치하는 문자열을 기록했다
*/

namespace BaekJoon.etc
{
    internal class etc_0795
    {

        static void Main795(string[] args)
        {

            string str1, str2;
            (int cnt, long before)[][] dp;

            Solve();

            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                long contains = dp[str1.Length - 1][str2.Length - 1].before;
                for (int i = 0; i < str1.Length; i++)
                {

                    if ((contains & 1L << i) == 0) continue;
                    sw.Write($"{str1[i]}");
                }

                sw.Close();
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput());
                str1 = sr.ReadLine();
                str2 = sr.ReadLine();

                dp = new (int cnt, long before)[str1.Length][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new (int cnt, long before)[str2.Length];
                }

                sr.Close();
            }

            void GetRet()
            {

                dp[0][0] = str1[0] == str2[0] ? (1, 1L) : (0, 0);

                for (int i = 1; i < str1.Length; i++)
                {

                    if (str1[i] == str2[0])
                    {

                        dp[i][0].cnt = 1;
                        dp[i][0].before = 1L << i;
                    }
                    else dp[i][0] = dp[i - 1][0];
                }

                for (int j = 1; j < str2.Length; j++)
                {

                    if (str1[0] == str2[j])
                    {

                        dp[0][j].cnt = 1;
                        if (dp[0][j - 1].cnt == 0) dp[0][j].before = 1L << 0;
                        else dp[0][j].before = dp[0][j - 1].before;
                    }
                    else dp[0][j] = dp[0][j - 1];
                }

                for (int i = 1; i < str1.Length; i++)
                {

                    for (int j = 1; j < str2.Length; j++)
                    {

                        int chk1 = dp[i - 1][j].cnt;
                        int chk2 = dp[i][j - 1].cnt;
                        int add = str1[i] == str2[j] ? 1 : 0;
                        int chk3 = dp[i - 1][j - 1].cnt + add;

                        if (chk2 < chk1 && chk3 < chk1) dp[i][j] = (chk1, dp[i - 1][j].before);
                        else if (chk1 < chk2 && chk3 < chk2) dp[i][j] = (chk2, dp[i][j - 1].before);
                        else 
                        {

                            long before;
                            if (add == 1) before = dp[i - 1][j - 1].before | 1L << i;
                            else before = dp[i - 1][j - 1].before;

                            dp[i][j] = (chk3, before); 
                        }
                    }
                }
            }
        }
    }

#if other
namespace _17218
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str1 = Console.ReadLine();
            string str2 = Console.ReadLine();

            string result = MakePassword(str1, str2);

            Console.WriteLine(result);
        }

        static string MakePassword(string str1, string str2)
        {
            int[,] dp = new int[str1.Length + 1, str2.Length + 1];

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            int len = dp[str1.Length, str2.Length];
            char[] lcs = new char[len];
            int idx = len - 1;

            int x = str1.Length;
            int y = str2.Length;

            while (x > 0 && y > 0)
            {
                if (str1[x - 1] == str2[y - 1])
                {
                    lcs[idx] = str1[x - 1];
                    x--;
                    y--;
                    idx--;
                }
                else if (dp[x - 1, y] > dp[x, y - 1])
                {
                    x--;
                }
                else
                {
                    y--;
                }
            }

            return new string (lcs);
        }
    }
}
#elif other2
using System;
using System.Collections.Generic;

public class Program
{
    struct Trace
    {
        public char c;
        public int pi, pj;
        public Trace(char c, int i, int j)
        {
            this.c = c; pi = i; pj = j;
        }
    }
    static void Main()
    {
        string a = Console.ReadLine(), b = Console.ReadLine();
        int n = a.Length, m = b.Length;
        int[,] dp = new int[n + 1, m + 1];
        Trace[,] trace = new Trace[n + 1, m + 1];
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= m; j++)
            {
                trace[i, j] = new('*', -1, -1);
            }
        }
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                if (dp[i - 1, j] > dp[i, j - 1])
                {
                    dp[i, j] = dp[i - 1, j];
                    trace[i, j] = trace[i - 1, j];
                }
                else
                {
                    dp[i, j] = dp[i, j - 1];
                    trace[i, j] = trace[i, j - 1];
                }
                if (a[i - 1] == b[j - 1] && dp[i - 1, j - 1] >= dp[i, j])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                    trace[i, j] = new(a[i - 1], i - 1, j - 1);
                }
            }
        }
        Trace cur = trace[n, m];
        Stack<char> stack = new();
        while (cur.pi != -1)
        {
            stack.Push(cur.c);
            cur = trace[cur.pi, cur.pj];
        }
        while (stack.Count > 0)
        {
            Console.Write(stack.Pop());
        }
    }
}
#endif
}
