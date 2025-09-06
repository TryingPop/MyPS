using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 4
이름 : 배성훈
내용 : 선우의 셋리스트
    문제번호 : 25962번

    수학, dp, 분할 정복을 이용한 거듭제곱 문제다.
    아이디어가 안떠올라 해설집을 봤다.

    그러니 dp의 방법으로 푸는데 약간의 행렬식 아이디어가 필요하다.
    dp[n] = ∑dp[n - i] * c[i + 1] (1 ≤ i ≤ 5)
    을 얻을 수 있다.
    여기서 c[i + 1]은 노래 재생시간이 i + 1인 노래들의 개수이다.

    이를 행렬식으로 표현하면
    dp[n]       = | c1  c2  c3  c4  c5  |^(n-5) dp[5]
    dp[n-1]       | 1   0   0   0   0   |       dp[4]
    dp[n-2]       | 0   1   0   0   0   |       dp[3]
    dp[n-3]       | 0   0   1   0   0   |       dp[2]
    dp[n-4]       | 0   0   0   1   0   |       dp[1]

    를 얻을 수 있는다.
    그러면 분할정복을 이용한 거듭제곱 문제로 바뀐다.
    이를 해결하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1864
    {

        static void Main1864(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            long n;
            int m;
            long[,] mat;
            long[] init;

            Input();

            GetRet();

            void GetRet()
            {

                long MOD = 1_000_000_007;
                long[,] calc = new long[5, 5];

                init = new long[6];
                init[0] = 1;
                // 배낭
                for (int i = 1; i <= 5; i++)
                {

                    for (int j = 0; j < i; j++)
                    {

                        init[i] = (init[i] + mat[0, j] * init[i - j - 1]) % MOD;
                    }
                }

                if (n <= 5)
                {

                    Console.Write(init[n]);
                    return;
                }

                long ret = GetPow(n - 5);
                Console.Write(ret);

                long GetPow(long _exp)
                {

                    long[,] chk = new long[5, 5];
                    for (int i = 0; i < 5; i++)
                    {

                        chk[i, i] = 1;
                    }

                    while (_exp > 0)
                    {

                        if ((_exp & 1L) == 1L) MatMul(chk, mat);

                        _exp >>= 1;
                        MatMul(mat, mat);
                    }

                    long ret = 0;
                    for (int i = 0; i < 5; i++)
                    {

                        ret = (ret + init[5 - i] * chk[0, i]) % MOD;
                    }

                    return ret;
                }

                void MatMul(long[,] _a, long[,] _b)
                {

                    for (int i = 0; i < 5; i++)
                    {

                        for (int j = 0; j < 5; j++)
                        {

                            for(int k = 0; k < 5; k++)
                            {

                                calc[i, j] = (calc[i, j] + _a[i, k] * _b[k, j]) % MOD;
                            }
                        }
                    }

                    for (int i = 0; i < 5; i++)
                    {

                        for (int j = 0; j < 5; j++)
                        {

                            _a[i, j] = calc[i, j];
                            calc[i, j] = 0;
                        }
                    }
                }
            }

            void Input()
            {

                n = ReadLong();
                m = ReadInt();

                mat = new long[5, 5];

                for (int i = 0; i < m; i++)
                {

                    int cur = ReadInt() - 1;
                    mat[0, cur]++;
                }

                for (int i = 1; i < 5; i++)
                {

                    mat[i, i - 1] = 1;
                }

                long ReadLong()
                {

                    long ret = 0;

                    while (TryReadLong()) ;
                    return ret;

                    bool TryReadLong()
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
            }

        }
    }
}
