using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 7
이름 : 배성훈
내용 : 농부 비니
    문제번호 : 20957번

    수학, dp 문제다.
    mod n 의 n값을 잘못 설정해 1번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1685
    {

        static void Main1685(string[] args)
        {

            int MAX = 10_000;
            long[] dp;

            SetArr();

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int q = int.Parse(sr.ReadLine());

            while (q-- > 0)
            {

                int idx = int.Parse(sr.ReadLine());

                sw.Write(dp[idx]);
                sw.Write('\n');
            }

            void SetArr()
            {

                int SEVEN = 7;
                int TEN = 10;
                int MOD = 1_000_000_007;
                dp = new long[MAX + 2];
                long[] mul = new long[SEVEN], not = new long[SEVEN], zero = new long[SEVEN], nextMul = new long[SEVEN], nextNot = new long[SEVEN], nextZero = new long[SEVEN];

                // 0 ~ 9
                mul[0] = 1;     // 7
                zero[0] = 1;     // 0
                not[1] = 2;     // 1, 8
                not[2] = 2;     // 2, 9
                not[3] = 1;     // 3
                not[4] = 1;     // 4
                not[5] = 1;     // 5
                not[6] = 1;     // 6

                SetDp(1);

                for (int i = 2; i <= MAX; i++)
                {

                    Next();

                    SetDp(i);
                }

                void SetDp(int _idx)
                {

                    dp[_idx] = mul[0];
                }

                void Next()
                {

                    for (int i = 0; i < SEVEN; i++)
                    {

                        // 앞에 0 추가하면 모두 맨 앞이 0이된다!
                        nextZero[i] = zero[i] + not[i] + mul[i];

                        // 앞에 7 추가하면 모두 Mul이 된다!
                        nextMul[i] = zero[i] + not[i] + mul[i];
                    }

                    // 앞에 붙는 숫자를 i
                    for (int i = 1; i < TEN; i++)
                    {

                        if (i == SEVEN) continue;

                        for (int j = 0; j < SEVEN; j++)
                        {

                            int idx = (i + j) % SEVEN;

                            nextMul[idx] += mul[j] + zero[j];
                            nextNot[idx] += not[j];
                        }
                    }

                    for (int i = 0; i < SEVEN; i++)
                    {

                        mul[i] = nextMul[i] % MOD;
                        not[i] = nextNot[i] % MOD;
                        zero[i] = nextZero[i] % MOD;

                        nextMul[i] = 0;
                        nextNot[i] = 0;
                        nextZero[i] = 0;
                    }
                }
            }
        }
    }
}
