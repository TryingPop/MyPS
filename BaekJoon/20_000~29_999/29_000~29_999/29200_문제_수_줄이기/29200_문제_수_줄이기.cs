using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 3
이름 : 배성훈
내용 : 문제 수 줄이기
    문제 번호 : 29200번

    dp, 애드 혹 문제다.
    접근자체를 못해 풀이를 봤다;
    그러니 4개 이하의 구간합에서 최대값이 나온다고 한다.
    그래서 최대 구간을 4개로 잡으니 dp로
    O(4 x 4 x N)의 시간에 해결되는 방법이 떠올랐고
    해당 방법으로 풀었다.

    이후 다른 사람 풀이와 기여를 보니
    a + b >= a ^ b 가 성립하기에 됨을 알 수 있었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1246
    {

        static void Main1246(string[] args)
        {

            int n;
            int[] xor;
            int[] arr;
            long[][] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dp[0][0] = 0;
                dp[1][1] = arr[1];
                SetXOR(1);

                for (int i = 2; i <= n; i++)
                {

                    SetXOR(i);

                    for (int j = 1; j < 5; j++)
                    {

                        long max = GetMax(i - j, j);
                        if (max == -1) continue;
                        dp[i][j] = max + xor[j];
                    }
                }

                Console.Write(GetMax(n));

                long GetMax(int _idx, int _k = 0)
                {

                    if (_idx < 0) return -1;
                    long ret = -1;
                    for (int i = 0; i < 5; i++)
                    {

                        if (i == _k) continue;
                        ret = Math.Max(ret, dp[_idx][i]);
                    }

                    return ret;
                }

                void SetXOR(int _idx)
                {

                    for (int i = 1; i < 5; i++)
                    {

                        xor[i] ^= arr[_idx];
                    }

                    for (int i = 1; i < 5; i++)
                    {

                        if (_idx - i < 1) break;
                        xor[i] ^= arr[_idx - i];
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                dp = new long[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new long[5];
                    Array.Fill(dp[i], -1);
                }

                arr = new int[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }

                xor = new int[5];
                sr.Close();

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;
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
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
// #define ll long long
const ll INF = 1e18;

int main() {
    cin.tie(0)->sync_with_stdio(0);
    int n;cin >>n;
    int a[n+1];
    for(int i=1;i<=n;i++)
        cin>>a[i];
    
    vector<array<ll,5>> dp(n+1);
    for(int i=1;i<=n;i++) {
        dp[i].fill(-INF);
        dp[i][1]=max({dp[i-1][2],dp[i-1][3],dp[i-1][4]})+(a[i]);
        if(i>=2) dp[i][2]=max({dp[i-2][1],dp[i-2][3],dp[i-2][4]})+(a[i-1]^a[i]);
        if(i>=3) dp[i][3]=max({dp[i-3][1],dp[i-3][2],dp[i-3][4]})+(a[i-2]^a[i-1]^a[i]);
        if(i>=4) dp[i][4]=max({dp[i-4][1],dp[i-4][2],dp[i-4][3]})+(a[i-3]^a[i-2]^a[i-1]^a[i]);
    }

    cout <<*max_element(dp[n].begin(),dp[n].end());
}
#endif
}
