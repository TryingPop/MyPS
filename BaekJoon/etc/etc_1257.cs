using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 7
이름 : 배성훈
내용 : Cutting into Monotone Increasing Sequence
    문제번호 : 30321번

    문자열, dp 문제다.
    dp[i][j] = val 를 i번째 인덱스에서 길이 j개를 택할 때,
    가장 짧게 자른 것으로 잡은게 val가 되게 dp를 설정한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1257
    {

        static void Main1257(string[] args)
        {

            int INF = 123_456;

            string strX, strB;
            int n, m;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void SetDp()
            {

                dp = new int[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new int[m + 1];
                    Array.Fill(dp[i], INF);
                }
            }

            void GetRet()
            {

                if (n < m)
                {

                    Console.Write(0);
                    return;
                }

                SetDp();

                for (int i = 1; i < m; i++)
                {

                    if (strX[n - i] != '0') dp[n - i][i] = 0;
                }

                if (CompXB(n - m, m) <= 0) dp[n - m][m] = 0;
                if (strX == "0") dp[0][1] = 0;

                for (int i = n - 2; i >= 0; i--)
                {

                    if (strX[i] == '0') continue;
                    for (int j = 1; j < m && i + j + j <= n; j++)
                    {

                        if (CompXX(i, j, i + j, j) <= 0) dp[i][j] = Math.Min(dp[i][j], dp[i + j][j] + 1);

                        for (int k = j + 1; k < m && i + j + k <= n; k++)
                        {

                            dp[i][j] = Math.Min(dp[i][j], dp[i + j][k] + 1);
                        }

                        if (i + j + m <= n && CompXB(i + j, m) <= 0)
                            dp[i][j] = Math.Min(dp[i][j], dp[i + j][m] + 1);
                    }

                    if (i + m + m > n) continue;
                    if (CompXB(i + m, m) > 0 || CompXX(i, m, i + m, m) > 0) continue;
                    dp[i][m] = Math.Min(dp[i][m], dp[i + m][m] + 1);
                }

                int ret = INF;
                for (int i = 1; i <= m; i++)
                {

                    ret = Math.Min(ret, dp[0][i]);
                }

                if (ret < INF) Console.Write(ret);
                else Console.Write("NO WAY");

                int GetLen(int _s, int _l)
                {

                    if (_s + _l > n) _l -= ((_s + _l) - n);
                    return _l;
                }

                int CompXB(int _xs, int _xl)
                {

                    _xl = GetLen(_xs, _xl);
                    if (_xl != m) return _xl.CompareTo(m);

                    for (int i = 0; i < _xl; i++)
                    {

                        if (strX[_xs + i] == strB[i]) continue;
                        return strX[_xs + i].CompareTo(strB[i]);
                    }

                    return 0;
                }

                int CompXX(int _s1, int _l1, int _s2, int _l2)
                {

                    _l1 = GetLen(_s1, _l1);
                    _l2 = GetLen(_s2, _l2);

                    if (_l1 != _l2) return _l1.CompareTo(_l2);
                    
                    for (int i = 0; i < _l1; i++)
                    {

                        if (strX[_s1 + i] == strX[_s2 + i]) continue;
                        return strX[_s1 + i].CompareTo(strX[_s2 + i]);
                    }

                    return 0;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string[] input = sr.ReadLine().Split(); 
                strX = input[0];
                strB = input[1];

                n = strX.Length;
                m = strB.Length;
            }
        }
    }

#if other
// #include<cstdio>
// #include<cstring>
// #include<algorithm>

using namespace std;

int main() {
    static char b[32], s[100001];
    static int dp[100000][20];
    scanf("%s %s", &s, &b);
    int n = strlen(s), m = strlen(b);
    for (int i = 0; i < n; i++) 
        for(int j = 0; j <= m; j++)
            dp[i][j] = 1e7;
    for (int i = 1; b[i-1]; i++) 
        dp[n-i][i] = (s[n-i] != '0') ? 0 : 1e7;
    if (strncmp(s+n-m, b, m) > 0) 
        dp[n-m][m] = 1e7;
    if (strcmp("0", s) == 0) dp[0][1] = 0;
    for (int i = n-2; i >= 0; i--) {
        if (s[i] == '0') continue;
        for (int j = 1; j < m && i + j + j <= n; j++) {
            if (strncmp(s+i, s+i+j, j) <= 0) 
                dp[i][j] = min(dp[i][j], dp[i+j][j] + 1);
            for (int k = j+1; k < m && i + j + k <= n; k++) 
                dp[i][j] = min(dp[i][j], dp[i+j][k] + 1);
            if (i + j + m <= n && strncmp(s+i+j, b, m) <= 0)
                dp[i][j] = min(dp[i][j], dp[i+j][m] + 1);
        } 
        if (i + m + m > n)  continue;
        if (strncmp(s+i+m, b, m) > 0 || strncmp(s+i, s+i+m, m) > 0) continue;
        dp[i][m] = min(dp[i][m], dp[i+m][m] + 1);
    }
    int ans = 1e7;
    for (int i = 1; i <= m; i++) 
        ans = min(ans, dp[0][i]);
    if (ans < 1e7)
        printf("%d\n", ans);
    else 
        puts("NO WAY");;
    return 0;
}
#endif
}
