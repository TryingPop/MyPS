using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 18
이름 : 배성훈
내용 : Jack the Mole
    문제번호 : 23373번

    dp, 배낭, 비트집합 문제다.
    각 i에 대해 배낭으로 하면 n^2 x w로 시간 초과 나온다.
    반면 메모리를 써서 하면 n x w로 해결된다.
    대신 메모리는 n x w를 쓴다.

    아이디어는 p[i][j]인데 0 ~ i - 1번까지 써서 j를 만들 수 있는지 여부를 담는다.
    비슷하게 s[i][j] 를 i + 1 ~ n 까지 사용해서 j를 만들 수 있는지 여부를 담는다.
    그러면 각 i에 대해 0 ~ half 를 조사하면서 p[i][j] = true이고 s[i][half - j] = true인 경우 half를 만들 수 있다.
    이렇게 찾는 경우 n x sum에 찾을 수 있다.    
*/

namespace BaekJoon.etc
{
    internal class etc_1900
    {

        static void Main1900(string[] args)
        {

            // 23373 - Jack the Mole

#if TIME_OUT

            int n, sum;
            int[] arr;
            int ret1;
            bool[] ret2;

            Input();

            GetRet();

            Output();

            void GetRet()
            {

                ret2 = new bool[n];
                bool[] visit = new bool[1_001];
                int[] cnt = new int[1_001];
                for (int i = 0; i < n; i++)
                {

                    cnt[arr[i]]++;
                }

                bool[] dp = new bool[(sum / 2) + 1];
                for (int cur = 0; cur <= 1_000; cur++)
                {

                    int chk = (sum - cur);
                    if (cnt[cur] == 0
                        || (chk & 1) == 1) continue;
                    cnt[cur]--;

                    FillDp();
                    visit[cur] = dp[chk >> 1];
                    cnt[cur]++;
                }

                ret1 = 0;
                for (int i = 0; i < n; i++)
                {

                    ret2[i] = visit[arr[i]];
                    if (ret2[i]) ret1++;
                }

                void FillDp()
                {

                    Array.Fill(dp, false);
                    dp[0] = true;
                    int e = 0;

                    for (int i = 0; i <= 1_000; i++)
                    {

                        if (cnt[i] == 0) continue;
                        int prev = cnt[i];

                        for (int mul = 1; 0 <= prev - mul; mul <<= 1)
                        {

                            for (int k = e; k >= 0; k--)
                            {

                                if (!dp[k]) continue;
                                int next = k + mul * i;
                                if (next >= dp.Length) continue;
                                dp[next] = true;
                            }

                            e = Math.Min(dp.Length - 1, e + mul * i);
                            prev -= mul;
                        }

                        // 마지막 구간
                        for (int k = e; k >= 0; k--)
                        {

                            if (!dp[k]) continue;
                            int next = k * prev * i;
                            if (next >= dp.Length) continue;
                            dp[next] = true;
                        }
                    }
                }
            }

            void Output()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{ret1}\n");
                for (int i = 0; i < n; i++)
                {

                    if (!ret2[i]) continue;
                    sw.Write(i + 1);
                    sw.Write(' ');
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                sum = 0;
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                    sum += arr[i];
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
            }
#else

            int n, sum;
            int[] arr;
            int ret1;
            bool[] ret2;

            Input();

            GetRet();

            Output();

            void GetRet()
            {

                bool[][] p = new bool[n + 2][];
                bool[][] s = new bool[n + 2][];

                for (int i = 0; i < n + 2; i++)
                {

                    p[i] = new bool[sum + 1];
                    s[i] = new bool[sum + 1];
                }

                p[0][0] = true;
                int e = 0;
                for (int i = 1; i <= n + 1; i++)
                {

                    for (int j = e; j >= 0; j--)
                    {

                        if (!p[i - 1][j]) continue;
                        int next = j + arr[i - 1];
                        p[i][next] = true;
                        p[i][j] = true;
                    }

                    e += arr[i - 1];
                }

                s[n + 1][0] = true;
                e = 0;
                for (int i = n; i >= 0; i--)
                {

                    for (int j = e; j >= 0; j--)
                    {

                        if (!s[i + 1][j]) continue;
                        int next = j + arr[i + 1];
                        s[i][next] = true;
                        s[i][j] = true;
                    }

                    e += arr[i + 1];
                }

                ret1 = 0;
                ret2 = new bool[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    int chk = sum - arr[i];
                    if ((chk & 1) == 1) continue;
                    int half = chk >> 1;

                    for (int j = 0; j <= half; j++)
                    {

                        if (p[i][j] && s[i][half - j])
                        {

                            ret1++;
                            ret2[i] = true;
                            break;
                        }
                    }
                }
            }

            void Output()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{ret1}\n");
                for (int i = 1; i <= n; i++)
                {

                    if (!ret2[i]) continue;
                    sw.Write(i);
                    sw.Write(' ');
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                sum = 0;
                arr = new int[n + 2];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    sum += arr[i];
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
            }
#endif

        }
    }
}
