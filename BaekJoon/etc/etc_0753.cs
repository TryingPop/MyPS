using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 6
이름 : 배성훈
내용 : 괄호
    문제번호 : 10422번

    수학, dp, 조합론 문제다
    카탈린 수와 관련된 문제다

    아이디어는 다음과 같다
    잘 만들어진 괄호 A, 잘 만들어진 괄호 B라 잡으면
    (A) B 형식으로 괄호를 형성하면 겹치지 않는 잘짜여진 괄호를 만들 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0753
    {

        static void Main753(string[] args)
        {

            int MOD = 1_000_000_007;

            StreamReader sr;
            StreamWriter sw;

            long[] dp;

            Solve();

            void Solve()
            {

                SetDp();

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int test = ReadInt();
                while(test-- > 0)
                {

                    int n = ReadInt();

                    if ((n & 1) == 1)
                    {

                        sw.Write("0\n");
                        continue;
                    }

                    sw.Write($"{dp[n]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void SetDp()
            {

                dp = new long[5_001];
                dp[2] = 1;

                for (int i = 4; i <= 5_000; i += 2)
                {

                    for (int j = 0; j < i; j += 2)
                    {

                        dp[i] += (dp[i - j - 2] * dp[j]) % MOD;
                    }

                    dp[i] %= MOD;
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
var sr = new StreamReader(Console.OpenStandardInput());
var sw = new StreamWriter(Console.OpenStandardOutput());

var N = int.Parse(sr.ReadLine());

var dp = new long[5001];
dp[0] = 1;
dp[2] = 1;

for (var i = 4; i <= 5000; i++)
{
    if (i % 2 == 1)
    {
        dp[i] = 0;
        continue;
    }

    for (var j = 2; j <= i; j += 2)
    {
        dp[i] += (dp[j - 2] * dp[i - j]) % 1000000007;
    }

    dp[i] %= 1000000007;
}

for (var n = 0; n < N; n++)
{
    var result = int.Parse(sr.ReadLine());
    sw.WriteLine(dp[result]);
}

sw.Flush();
sw.Close();
sr.Close();
#elif other2
// #include<stdio.h>
// #define ll long long
int main()
{

    int t;
    ll mod = 1000000007;
    scanf("%d",&t);
    int i;
    ll a[10001];
    a[0] = 1;
    for(i = 1;i <= 10000; i++)
    {
    
        a[i]= i * a[i-1];
        a[i] %= mod;
    }
    
    while (t--)
    {
    
        int n;
        scanf("%d",&n);
        if (n % 2)
        {
        
            printf("0\n");
            continue;
        }
        
        ll ex = mod - 2;
        ll re = 1;
        ll B=(a[n/2] * a[n/2+1]) % mod;
        
        while (ex)
        {
        
            if(ex % 2)
            {
            
                re *= B;
                re %= mod;
            }
            
            B *= B;
            B %= mod;
            ex /= 2;
        }
        
        printf("%lld\n",(re * a[n]) % mod);
    }
}
#endif
}
