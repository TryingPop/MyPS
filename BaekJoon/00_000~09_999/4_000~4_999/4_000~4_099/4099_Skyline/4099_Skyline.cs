using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 11
이름 : 배성훈
내용 : Skyline
    문제번호 : 4099번

    dp, 조합론 문제다.
    카탈란 수와 일치한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1760
    {

        static void Main1760(string[] args)
        {


            int MAX = 1_000;
            long[] ret;

            Init();

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;

            while((n = ReadInt()) > 0)
            {

                sw.Write(ret[n]);
                sw.Write('\n');
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

#if CHAT_GPT
            void Init()
            {

                int MOD = 1_000_000;
                ret = new long[MAX + 1];

                ret[0] = 1;
                ret[1] = 1;

                for (int i = 2; i <= MAX; i++)
                {

                    for (int j = 0; j < i; j++)
                    {

                        ret[i] = (ret[i] + ret[j] * ret[i - 1 - j]) % MOD;
                    }
                }
            }
#else

            void Init()
            {

                int MOD = 1_000_000;

                long[][] dp = new long[MAX + 1][];
                ret = new long[MAX + 1];

                for (int i = 0; i <= MAX; i++)
                {

                    dp[i] = new long[MAX + 1];
                }

                dp[1][1] = 1;

                for (int i = 2; i <= MAX; i++)
                {

                    long s = 0;
                    for (int j = i; j > 0; j--)
                    {

                        s += dp[i - 1][j];
                        dp[i][j] = (s + dp[i - 1][j - 1]) % MOD;
                    }
                }

                for (int i = 1; i <= MAX; i++)
                {

                    for (int j = i; j >= 0; j--)
                    {

                        ret[i] = (ret[i] + dp[i][j]) % MOD;
                    }
                }
            }
#endif
        }
    }

#if other
// #include <bits/stdc++.h>
// #define int long long
using namespace std;

main() {
    ios_base::sync_with_stdio(false);
    cin.tie(NULL), cout.tie(NULL);

    int dp[1001] = {1, 1};

    for(int i=2; i<=1000; i++)
        for(int j=0; j<i; j++) dp[i] = (dp[i] + dp[j] * dp[i-1-j]) % (int)(1e6);

    while(true) {
        int N; cin >> N;
        if(N == 0) break;

        cout << dp[N] << "\n";
    }
}

#elif other2
// #include <iostream>
using namespace std;
using ll = long long;
const int N = 1003;
const ll M = 1'000'000;

int main(void){
    ios::sync_with_stdio(0); cin.tie(0);
    ll dp[N][N]{0}; 
    dp[1][1] = 1;
    for (int i = 1; i < N-1; i++){
        ll s = 0;
        for (int j = i+1; j >= 1; j--){
            s += dp[i][j];
            dp[i+1][j] = (s + dp[i][j-1]) % M;  
        }
    }

    int n; cin >> n;
    while (n){
        int a = 0;
        for (int i = 1; i <= n; i++)
            a += dp[n][i];
        cout << a % M << "\n";
        cin >> n;
    }
}

#endif
}
