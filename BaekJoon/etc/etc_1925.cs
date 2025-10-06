using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 2
이름 : 배성훈
내용 : Hiding Merlin
    문제번호 : 16107번

    dp 문제다.
    아이디어는 다음과 같다.
    먼저 원하는 자리가 제곱수가될 수 있는지 확인한다.
    값의 범위가 10억 이하이므로 19자리까지만 확인하면 된다.
    이분 탐색으로 찾았다.

    다음으로 원하는 자리가 제곱수인지 판별할 수 있으므로 dp의 방법으로 풀었다.
    먼저 dp[i] = val를 i번 자리까지 확인했을 때 제곱수 합의 최소값으로 했다.
    그러면 i + 1 ~ j까지 수 a가 제곱수라면 dp[j] = Math.Min(dp[j], dp[i] + a)가 성립한다.
    이렇게 끝까지 진행했을 때 값이 차있다면 해당 값이 최솟값임이 그리디로 보장된다.
    반면 값이 없다면 해당 경우로 제곱수가 될 수 없으므로 -1을 출력했다.

    조건에서 0으로 시작될 수 없다고 한다.
    그래서 1이상 10억까지 이분탐색 범위를 설정했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1925
    {

        static void Main1925(string[] args)
        {

            string str;
            Input();

            GetRet();

            void GetRet()
            {

                int n = str.Length;

                long INF = 1_000_000_000 + 1;
                long[] dp = new long[n + 1];
                Array.Fill(dp, INF);

                dp[0] = 0;
                for (int s = 0; s < n; s++)
                {

                    if (dp[s] == INF) continue;
                    for (int len = 1; len < 20; len++)
                    {

                        long val = GetVal(s, len);

                        long sqrt = ChkPow(val);
                        if (sqrt == -1) continue;

                        dp[s + len] = Math.Min(sqrt + dp[s], dp[s + len]);
                    }
                }

                dp[n] = dp[n] == INF ? -1 : dp[n];
                Console.Write(dp[n]);

                long GetVal(int s, int len)
                {

                    if (str[s] == '0' || s + len > n) return -1;
                    long ret = 0;

                    for (int i = 0; i < len; i++)
                    {

                        ret = ret * 10 + str[s + i] - '0';
                    }

                    return ret;
                }
            }

            long ChkPow(long val)
            {

                if (val == -1) return -1;
                long l = 1;
                long r = 1_000_000_000;

                while (l <= r)
                {

                    long mid = (l + r) / 2;

                    long mul = mid * mid;
                    if (mul < val) l = mid + 1;
                    else if (mul > val) r = mid - 1;
                    else return mul;
                }

                return -1;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                str = sr.ReadLine();
            }
        }
    }

#if other
// #include <iostream>
// #include <cmath>
using namespace std;
using ll = long long;
int isPerfectSquare(int n){
    int sr = sqrt(n);
    return sr*sr == n;
}

int main(void){
    ios::sync_with_stdio(0); cin.tie(0);
    string s; cin >> s;
    for (auto &c:s)
        c -= 48;
    int n = s.size();

    ll dp[100'005];
    dp[0] = 0ll;
    fill_n(dp+1, n, -1ll);
    for (int i = 0; i < n; i++){
        ll tp = 1;
        ll v = 0;
        for (int j = i; j >= 0 && tp <= 100'000'000; j--,tp*=10){
            int d = s[j];
            if (d == 0)
                continue;
            v += tp * d;
            if (dp[j] == -1)
                continue;
            if (!isPerfectSquare(v))
                continue;
            if (dp[i+1] == -1){
                dp[i+1] = dp[j] + v;
            }
            else
                dp[i+1] = min(dp[i+1], dp[j]+v); 
        }
    }
    ll ans = dp[n];
    if (ans > 1'000'000'000)
        ans = -1;
    cout << ans << "\n";
}

#endif
}
