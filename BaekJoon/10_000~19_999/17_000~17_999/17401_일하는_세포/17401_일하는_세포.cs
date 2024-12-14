using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 14
이름 : 배성훈
내용 : 일하는 세포
    문제번호 : 17401번

    수학, 분할 정복을 이용한 거듭제곱 문제다.
    문제에서 요구하는건 다음과 같다.
    t초 주기로 이동할 수 있는 문이 달라진다.
    문제에서 요구하는 정답은 행렬인데,
    mat[i][j]는 t초 후 i에서 j로 가는 경우의 수이다.
    이는 행렬곱과 같아진다.
*/

namespace BaekJoon.etc
{
    internal class etc_1188
    {

        static void Main1188(string[] args)
        {

            int MOD = 1_000_000_007;
            int n, t, d;

            long[][][] mat;
            long[][] calc;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                calc = new long[n][];
                for (int i = 0; i < n; i++)
                {

                    calc[i] = new long[n];
                }

                for (int i = 1; i <= t; i++)
                {

                    MatMul(mat[0], mat[i]);
                }

                mat[0] = GetPow(mat[0], d / t);

                int e = d % t;
                for (int i = 1; i <= e; i++)
                {

                    MatMul(mat[0], mat[i]);
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        sw.Write($"{mat[0][i][j]} ");
                    }

                    sw.Write('\n');
                }
            }

            void MatMul(long[][] _a, long[][] _b)
            {

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        for (int k = 0; k < n; k++)
                        {

                            calc[i][j] = (calc[i][j] + _a[i][k] * _b[k][j]) % MOD;
                        }
                    }
                }

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        _a[i][j] = calc[i][j];
                        calc[i][j] = 0;
                    }
                }
            }

            long[][] GetPow(long[][] _a, int _exp)
            {

                long[][] ret = new long[n][];
                for (int i = 0; i < n; i++)
                {

                    ret[i] = new long[n];
                    ret[i][i] = 1;
                }

                while (_exp > 0)
                {

                    if ((_exp & 1) == 1) MatMul(ret, _a);
                    MatMul(_a, _a);
                    _exp >>= 1;
                }

                return ret;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                t = ReadInt();
                n = ReadInt();
                d = ReadInt();

                mat = new long[t + 1][][];
                mat[0] = new long[n][];
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        mat[0][i] = new long[n];
                        mat[0][i][i] = 1;
                    }
                }
                int len;
                for (int i = 1; i <= t; i++)
                {

                    
                    mat[i] = new long[n][];
                    for (int j = 0; j < n; j++)
                    {

                        mat[i][j] = new long[n];
                    }

                    len = ReadInt();

                    for (int j = 0; j < len; j++)
                    {

                        int f = ReadInt() - 1;
                        int s = ReadInt() - 1;
                        int val = ReadInt();

                        mat[i][f][s] = val;
                    }
                }

                sr.Close();
                int ReadInt()
                {

                    int c, ret = 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }
}
