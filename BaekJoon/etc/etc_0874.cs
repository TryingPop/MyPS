using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 11
이름 : 배성훈
내용 : 피보나치 수의 합
    문제번호 : 2086번

    수학, 분할 정복을 이용한 거듭제곱 문제다
    잘 알려진 피보나치 문제다

    f에서 b까지(f <= b) fibo나치 수열을 더하면
    개수가 홀수 짝수 나눠서 확인했더니
    fibo(b + 2) - fibo(f + 1) 가 성립한다
*/

namespace BaekJoon.etc
{
    internal class etc_0874
    {

        static void Main874(string[] args)
        {

            int MOD = 1_000_000_000;
            long f, b;

            Solve();
            void Solve()
            {

                Init();

                long ret;
                if (f == b) ret = GetFibo(f);
                else ret = (GetFibo(b + 2) - GetFibo(f + 1) + MOD) % MOD;

                Console.Write(ret);
            }

            void Init()
            {

                string[] temp = Console.ReadLine().Split();
                f = long.Parse(temp[0]);
                b = long.Parse(temp[1]);
            }

            (long x11, long x12, long x21, long x22) GetPow(long _exp)
            {

                (long x11, long x12, long x21, long x22) ret = (1L, 0L, 0L, 1L);
                (long x11, long x12, long x21, long x22) a = (1L, 1L, 1L, 0L);

                while(_exp > 0L)
                {

                    if ((_exp & 1L) == 1L) MulMat(ref ret, ref a);

                    MulMat(ref a, ref a);
                    _exp >>= 1;
                }

                return ret;
            }

            void MulMat(ref (long x11, long x12, long x21, long x22) _f, ref(long x11, long x12, long x21, long x22) _b)
            {

                long x11 = (_f.x11 * _b.x11 + _f.x12 * _b.x21) % MOD;
                long x12 = (_f.x11 * _b.x12 + _f.x12 * _b.x22) % MOD;
                long x21 = (_f.x21 * _b.x11 + _f.x22 * _b.x21) % MOD;
                long x22 = (_f.x21 * _b.x12 + _f.x22 * _b.x22) % MOD;

                _f = (x11, x12, x21, x22);
            }

            long GetFibo(long _idx)
            {

                var mat = GetPow(_idx);
                return mat.x21;
            }
        }
    }

#if other
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

long[] input = Array.ConvertAll(sr.ReadLine().Split(), long.Parse);
long A = input[0], B = input[1];
long P = 1000000000;

long[,] baseMat = { { 1, 1, 0 }, { 1, 0, 0 }, { 1, 0, 1 } };

sw.WriteLine((fiboSum(B) - fiboSum(A - 1) + P) % P);

sr.Close();
sw.Close();
//------------

long fiboSum(long n)
{
    if (n == 0)
        return 0;

    long[,] mat = fiboSumMat(n);
    return (long)(mat[0,1] + mat[2,1])%P;
}
long[,] fiboSumMat(long n)
{
    if (n == 1)
        return baseMat;

    long[,] half = fiboSumMat(n / 2);
    long[,] one = matMul(half, half, 3);
    if (n % 2 == 0)
        return one;
    else
        return matMul(one, baseMat, 3);
}
long[,] matMul(long[,] a, long[,] b, long n)
{
    long[,] retMat = new long[n, n];
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            for (int k = 0; k < n; k++)
                retMat[i, j] = (retMat[i, j] + a[i, k] * b[k, j]) % P;
    return retMat;
}
#endif
}
