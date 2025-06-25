using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 1
이름 : 배성훈
내용 : 배드민턴 대회    
    문제번호 : 20443번

    dp, 조합론 문제다.
    경우의 수를 찾아 풀었다.
    찾아보니 교란수열이라 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1505
    {

        static void Main1505(string[] args)
        {

            int MOD = 1_000_000_007;
            int n = int.Parse(Console.ReadLine());
            long[] dp = new long[n + 1];

            dp[2] = 1;
            dp[3] = 2;
            dp[4] = 9;

            // 점화식
            int e = n - (n % 4);
            for (int idx = 5; idx <= e; idx++)
            {

                long mul = 1;
                for (int j = idx - 1; j >= 2; j--)
                {

                    mul = (mul * j) % MOD;
                    dp[idx] = (dp[idx] + (mul * dp[j - 1]) % MOD) % MOD;
                }

                dp[idx] = (dp[idx] + mul) % MOD;
            }

            long ret = (Combi(n, n % 4) * dp[e]) % MOD;
            Console.Write(ret);

            long Combi(int _n, int _k)
            {

                long ret = 1;
                if (_k == 0) return ret;

                for (int i = 0; i < _k; i++, _n--)
                {

                    ret *= _n;
                }

                for (int i = _k; i > 0; i--)
                {

                    ret /= i;
                }

                return ret;
            }
        }
    }

#if other
// #include <stdio.h>

using namespace std;

long long dp[101];
long long mod=1000000007;

int main(){
    long long n, a;
    scanf("%lld", &a);
    dp[0]=1;
    dp[1]=0;
    for(long long i=2;i<=a-a%4;i++) dp[i]=((i-1)*(dp[i-1]+dp[i-2]))%mod;
    long long sum=1;
    for(long long i=a;i>a-a%4;i--) sum*=i;
    for(long long i=1;i<=a%4;i++) sum/=i;
    printf("%lld", (dp[a-a%4]*sum)%mod);
    return 0;
}
#endif
}
