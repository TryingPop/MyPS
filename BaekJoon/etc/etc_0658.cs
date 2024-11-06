using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 짝수번째 피보나치 수의 합
    문제번호 : 11443번

    수학, 분할 정복을 이용한 거듭제곱 문제다
    아래 아이디어로 문제를 풀었다
    fibo[3] - 1 = fibo[0] + fibo[2]
    fibo[5] - 1 = fibo[0] + fibo[2] + fibo[4]
    ... 
    fibo[2 * n  + 1] - 1 = sig fibo[2 * i]
    그래서 fibo[2 * n + 1]인 피보나치 수를 찾아야한다

    행렬곱 아이디어를 이용
    fibo[n + 2] = fibo[n + 1] + fibo[n]
    fibo[n + 1] = fibo[n + 1]
    -> | fibo[n + 2] |  =   | 1  1 | | fibo[n + 1] |
       | fibo[n + 1] |      | 1  0 | | fibo[n]     |

        = | 1 1 | ^ (n + 1) | fibo[1] |
          | 1 0 |           | fibo[0] |

    -> | 1 1 |^ (n - 1) | f[1] |  = | f[n]     | 
       | 1 0 |          | f[0] |    | f[n - 1] |

    으로 f[n]을 찾는다
*/

namespace BaekJoon.etc
{
    internal class etc_0658
    {

        static void Main658(string[] args)
        {

            long MOD = 1_000_000_007;
            long n = long.Parse(Console.ReadLine());
            // n : 찾을 피보나찌!
            if (n % 2 == 0) n++;

            long[] mat = { 1, 1, 1, 0 };

            Solve();

            void Solve()
            {

                long[] ret = GetRet(n - 1);
                Console.WriteLine(ret[0] - 1);
            }

            long[] GetRet(long _n)
            {

                long[] ret = new long[4] { 1, 0, 0, 1 };

                while (_n > 0)
                {

                    if (_n % 2 == 1)
                    {

                        MatMul(ret, mat);
                    }

                    _n /= 2;
                    MatMul(mat, mat);
                }

                return ret;
            }

            void MatMul(long[] _f, long[] _b)
            {

                long v11 = (_f[0] * _b[0]) % MOD;
                v11 = (v11 + ((_f[1] * _b[2]) % MOD)) % MOD;

                long v12 = (_f[0] * _b[1]) % MOD;
                v12 = (v12 + ((_f[1] * _b[3]) % MOD)) % MOD;

                long v21 = (_f[2] * _b[0]) % MOD;
                v21 = (v21 + ((_f[3] * _b[2]) % MOD)) % MOD;

                long v22 = (_f[2] * _b[1]) % MOD;
                v22 = (v22 + ((_f[3] * _b[3]) % MOD)) % MOD;

                _f[0] = v11;
                _f[1] = v12;
                _f[2] = v21;
                _f[3] = v22;
            }
        }
    }

#if other
using System;

public class Program
{
    const int Mod = 1000000007;
    static void Main()
    {
        long n = long.Parse(Console.ReadLine());
        Matrix matrix = Matrix.ModPow(new(new long[,] { { 1, 1 }, { 1, 0 } }), n % 2 == 0 ? n + 1 : n);
        Console.Write(matrix[0, 1] - 1);
    }
    struct Matrix
    {
        int row, column;
        long[,] matrix;
        public Matrix(int r, int c)
        {
            row = r;
            column = c;
            matrix = new long[r, c];
        }
        public Matrix(long[,] array)
        {
            row = array.GetLength(0);
            column = array.GetLength(1);
            matrix = array;
        }
        public long this[int r, int c]
        {
            get => matrix[r, c];
            set => matrix[r, c] = value;
        }
        public static Matrix ModMultiply(Matrix a, Matrix b)
        {
            Matrix matrix = new(a.row, b.column);
            for (int i = 0; i < a.row; i++)
            {
                for (int j = 0; j < b.column; j++)
                {
                    for (int k = 0; k < a.column; k++)
                    {
                        matrix[i, j] += a[i, k] * b[k, j];
                    }
                    matrix[i, j] %= Mod;
                }
            }
            return matrix;
        }
        public static Matrix ModPow(Matrix matrix, long n)
        {
            if (n == 1)
                return matrix;
            Matrix pow = ModPow(matrix, n / 2);
            if (n % 2 == 0)
                return ModMultiply(pow, pow);
            return ModMultiply(ModMultiply(pow, pow), matrix);
        }
    }
}
#endif
}
