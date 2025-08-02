using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 1
이름 : 배성훈
내용 : 조교의 맹연습
    문제번호 : 27114번

    dp문제다.
    dp[i][j] = val를 i방향에서 j힘을 썼을 때 최소 이동횟수로 잡으면 된다.
    그러면 i * 4 * 3의 시행으로 정답을 찾을 수 있다.
    다른 사람의 풀이를 보니 1바퀴 회전 하는 서로 다른 최소인 모든 경우를(기저) 찾고
    가능한 에너지에서 최소 에너지로 이동하며 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1506
    {

        static void Main1506(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            // 방향도 추가해야 한다.
            int[][] dp = new int[4][];
            for (int i = 0; i < 4; i++)
            {

                dp[i] = new int[input[3] + 1];
                Array.Fill(dp[i], -1);
            }
            dp[0][0] = 0;


            for (int i = 0; i <= input[3]; i++)
            {

                for (int j = 0; j < 4; j++)
                {

                    if (dp[j][i] == -1) continue;

                    for (int k = 0; k < 3; k++)
                    {

                        int nPow = i + input[k];
                        if (nPow > input[3]) continue;
                        int nDir = NextDir(j, k);
                        if (dp[nDir][nPow] == -1 || dp[j][i] + 1 < dp[nDir][nPow]) dp[nDir][nPow] = dp[j][i] + 1;
                    }
                }
            }

            Console.Write(dp[0][input[3]]);

            int NextDir(int _j, int _k)
            {

                if (_k == 0) _j += 1;
                else if (_k == 1) _j -= 1;
                else _j += 2;

                if (_j < 0) _j += 4;
                else if (4 <= _j) _j -= 4;

                return _j;
            }
        }
    }

#if other
// #include <iostream>
using namespace std;

int dp[1000001];

int main()
{
    ios::sync_with_stdio(0);
    cin.tie(0);
    
    fill(dp, dp+1000001, 1e9+10);
    
    int a, b, c, k;
    cin >> a >> b >> c >> k;
    
    int type[6] = {a+b, c*2, a*2+c, b*2+c, a*4, b*4};
    for(int i = 0; type[0]*i <= k; i++)
        dp[type[0]*i] = i*2;
    
    for(int i = 1; i < 6; i++)
        for(int j = 0; j <= k; j++)
            if(j - type[i] >= 0)
                dp[j] = min(dp[j], dp[j-type[i]] + i/2+2);
    
    cout << (dp[k] == 1e9+10 ? -1 : dp[k]) << '\n';
}
#endif
}
