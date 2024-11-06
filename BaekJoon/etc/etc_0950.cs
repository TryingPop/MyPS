using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 7
이름 : 배성훈
내용 : Domino
    문제번호 : 9898번

    dp, 비트마스킹 문제다
    해당 사이트의 아이디어를 참고했다
    https://blog.naver.com/jinhan814/222139521293
*/

namespace BaekJoon.etc
{
    internal class etc_0950
    {

        static void Main950(string[] args)
        {

            int MOD = 1_000;
            int n = int.Parse(Console.ReadLine());

            // 1 << 0 맨 윗 칸 채워진 유무
            // 1 << 1 두 번째 칸 채워진 유무
            // 1 << 2 세 번째 칸 채워진 유무
            // 1 << 3 맨 밑 칸 채워진 유무
            // 13 인덱스인 경우
            // 13 = 1 + 4 + 8
            // 맨 윗 칸 차있고, 세 번째 칸 차 있고, 맨 밑 칸 차있는 경우다
            int[][] dp = new int[1 << 4][];
            for (int i = 0; i < dp.Length; i++)
            {

                dp[i] = new int[n + 1];
            }

            dp[0][0] = 1;

            for (int i = 1; i <= n; i++)
            {

                /*

                dp[0][i] += dp[0][i - 1];       // 일자로 두 개 세우는 경우
                dp[0][i] += dp[3][i - 1];
                dp[0][i] += dp[9][i - 1];
                dp[0][i] += dp[12][i - 1];
                dp[0][i] += dp[15][i - 1];

                dp[1][i] += dp[2][i - 1];
                dp[1][i] += dp[8][i - 1];
                dp[1][i] += dp[14][i - 1];



                dp[2][i] += dp[1][i - 1];       // 2번째 칸에 가로 3, 4는 세로
                dp[2][i] += dp[13][i - 1];

                dp[3][i] += dp[0][i - 1];       // 윗 두칸은 가로로 막대 2개 배치하고, 아래 두칸은 세로로 배치하면된다
                dp[3][i] += dp[12][i - 1];

                dp[4][i] += dp[8][i - 1];
                dp[4][i] += dp[11][i - 1];

                dp[5][i] += dp[10][i - 1];

                dp[6][i] += dp[9][i - 1];

                dp[7][i] += dp[8][i - 1];

                dp[8][i] += dp[1][i - 1];       // 2, 3번째 세로 4번째 가로
                dp[8][i] += dp[4][i - 1];
                dp[8][i] += dp[7][i - 1];

                dp[9][i] += dp[0][i - 1];       // 맨위와 맨 아래는 가로로 중앙 2칸은 세로로
                dp[9][i] += dp[6][i - 1];

                dp[10][i] += dp[5][i - 1];

                dp[11][i] += dp[4][i - 1];

                dp[12][i] += dp[0][i - 1];      // 밑에 2칸은 가로로 위에는 세로로 1개 세운다
                dp[12][i] += dp[3][i - 1];

                dp[13][i] += dp[2][i - 1];

                dp[14][i] += dp[1][i - 1];      // 2, 3, 4 가로

                dp[15][i] += dp[0][i - 1];      // 모든 곳에 가로로 세우기
                */
                
                dp[0][i] = (dp[0][i - 1] + dp[3][i - 1] + dp[9][i - 1] + dp[12][i - 1] + dp[15][i - 1]) % MOD;
                dp[1][i] = (dp[2][i - 1] + dp[8][i - 1] + dp[14][i - 1]) % MOD;
                dp[2][i] = (dp[1][i - 1] + dp[13][i - 1]) % MOD;
                dp[3][i] = (dp[0][i - 1] + dp[12][i - 1]) % MOD;
                dp[4][i] = (dp[8][i - 1] + dp[11][i - 1]) % MOD;
                dp[5][i] = dp[10][i - 1];
                dp[6][i] = dp[9][i - 1];
                dp[7][i] = dp[8][i - 1];
                dp[8][i] = (dp[1][i - 1] + dp[4][i - 1] + dp[7][i - 1]) % MOD;
                dp[9][i] = (dp[0][i - 1] + dp[6][i - 1]) % MOD;
                dp[10][i] = dp[5][i - 1];
                dp[11][i] = dp[4][i - 1];
                dp[12][i] = (dp[0][i - 1] + dp[3][i - 1]) % MOD;
                dp[13][i] = dp[2][i - 1];
                dp[14][i] = dp[1][i - 1];
                dp[15][i] = dp[0][i - 1];
            }

            Console.Write(dp[0][n]);
        }
    }

#if other
// #include <bits/stdc++.h>
// #define endl '\n'
using namespace std;
const int PRECISION = 0;
using ll = long long;
using pi2 = pair<int, int>;
// #define fr first
// #define sc second

const int mod = 1000;
int dp[1020] = {1, 1, 5};

void Main(){
    int n; cin >> n;
    for (int i = 3; i <= n; i++){
        dp[i] = dp[i-1] + 4*dp[i-2];
        for (int j = 3; j <= i; j++){ dp[i] += (3-j%2) * dp[i-j]; }
        dp[i] %= mod;
    } cout << dp[n];
}

int main(){
    ios_base::sync_with_stdio(0); cin.tie(0); cout.tie(0);
    cout.setf(ios::fixed); cout.precision(PRECISION); Main();
}
#endif
}
