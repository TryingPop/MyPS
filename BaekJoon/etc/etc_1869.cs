using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 5
이름 : 배성훈
내용 : 짜고 치는 가위바위보 (Small, Large)
    문제번호 : 30518번, 30519번

    dp 문제다.
    dp[i][j][k] = val를 다음과 같이 설정한다.
    smallant가 i번째 경우까지확인했다.
    그리고 이전에 smallant와 lighter의 승부 기록이 j가 된다.
    이후 k는 이전에 lighter가 낸 것 을 나타낸다.
    그리고 이때 가능한 경우를 val에 담는다.

    그러면 다음과 같은 점화식을 얻는다.
    dp[i + 1][j][k] += dp[i][j][k]이다.
    그리고 각 dp[i][j][k]에 대해 k와 lighter의 i번째 값 K 승부 J를 확인한다.
    여기서 j를 비기는 경우 0, smallant가 이긴 경우 1, lighter가 이긴 경우 2
    dp[i + 1][J][K] += dp[i][0][K] + dp[i][1][K] 가 성립한다.
    다만 J가 비기는 경우가 아니면 즉 J != 0인 경우 dp[i + 1][J][K] += dp[i][2][K]가 성립한다.
    반면 J가 비기는 겨웅면 이전에 lighter가 이기는 j = 2인 경우는 누적하면 안된다.

    이러한 규칙으로 dp[i][j][k]의 값을 채워간다.
    이 경우 i + 1의 경우와 i의 경우만 연관되므로 i의 크기를 str2의 크기로 할당하는게 아닌
    i = 0을 현재, i = 1을 다음으로 나타내면 메모리를 많이 절약할 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1869
    {

        static void Main1869(string[] args)
        {

            string str1, str2;

            Input();

            GetRet();

            void GetRet()
            {

                // dp[a][b][c] = val
                // 가능한 경우의 수 저장
                // a : 0 = 현재, 1 = 다음
                // b : 0 = 비긴 경우, 1 = s이 이긴 경우, 2 = l가 이긴경우
                // c : l이 이전에 냈던 것 묵 = 0, 찌 = 1, 빠 = 2
                int[][][] dp = new int[2][][];
                
                for(int i = 0; i < 2; i++)
                {

                    dp[i] = new int[3][];
                    for (int j = 0; j < 3; j++)
                    {

                        dp[i][j] = new int[3];
                    }
                }

                int MOD = 1_000_000_007;
                for (int i = 0; i < str2.Length; i++)
                {

                    int l = GetNum(str1[0]);
                    int s = GetNum(str2[i]);

                    // dp[a][b][c]
                    int win = IsWin(l, s);

                    dp[1][win][s]++;
                    for (int j = 0; j < 3; j++)
                    {

                        l = j;
                        win = IsWin(l, s);

                        dp[1][win][s] = (dp[1][win][s] + dp[0][0][j]) % MOD;
                        dp[1][win][s] = (dp[1][win][s] + dp[0][1][j]) % MOD;
                        if (win != 0) dp[1][win][s] = (dp[1][win][s] + dp[0][2][j]) % MOD;
                    }

                    for (int j = 0; j < 3; j++)
                    {

                        for (int k = 0; k < 3; k++)
                        {

                            dp[0][j][k] = (dp[0][j][k] + dp[1][j][k]) % MOD;
                            dp[1][j][k] = 0;
                        }
                    }
                }

                int ret = 0;
                for (int i = 0; i < 3; i++)
                {

                    for (int j = 0; j < 3; j++)
                    {

                        ret = (ret + dp[0][i][j]) % MOD;
                    }
                }

                Console.Write(ret);

                int IsWin(int _f, int _t)
                {

                    int ret = _f - _t;
                    if (ret < 0) ret += 3;

                    return ret;
                }

                int GetNum(char _rsp)
                {

                    if (_rsp == 'R') return 0;
                    else if (_rsp == 'S') return 1;
                    else return 2;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                str1 = sr.ReadLine();
                str2 = sr.ReadLine();
            }
        }
    }
}
