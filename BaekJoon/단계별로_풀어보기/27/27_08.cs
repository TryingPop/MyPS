using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 11
이름 : 배성훈
내용 : 피보나치 수 6
    문제번호 : 11444번

    행렬 제곱을 이용해 피보나치 수를 구한다

    Fn+2 = Fn+1 + Fn
    Fn+1 = Fn   + 0
    이므로
    | Fn+2 | = | 1  1 | | Fn+1 |
    | Fn+1 |   | 1  0 | | Fn   |
             = | 1  1 | ^2 | Fn   |
               | 1  0 |    | Fn-1 |
    이를 귀납적으로 적용해가면 

    | Fn+1 Fn | = | 1  1 |^ n
    | Fn   0  |   | 1  1 |
    이된다
*/

namespace BaekJoon._27
{
    internal class _27_08
    {

        static void Main8(string[] args)
        {

            // 입력
            long pow = long.Parse(Console.ReadLine());

            long[][] mat = new long[2][];
            long[][] calc = new long[2][];
            
            long[][] result = new long[2][];
            for (int i = 0; i < 2; i++)
            {

                mat[i] = new long[2];

                result[i] = new long[2];
                result[i][i] = 1;
            }

            mat[0][0] = 1;
            mat[0][1] = 1;
            mat[1][0] = 1;

            int mod = 1_000_000_007;

            // 행렬 제곱을 이용한 피보나치 수를 구하기에 행렬 제곱
            while (pow != 0)
            {

                if (pow % 2 == 1)
                {

                    MultipleMat(result, mat, calc, mod);
                    CopyMat(calc, result);
                }

                MultipleMat(mat, mat, calc, mod);
                CopyMat(calc, mat);
                pow /= 2;
            }

            Console.WriteLine(result[0][1]);
        }

        // 행렬곱
        public static void MultipleMat(long[][] _a, long[][] _b, long[][] _result, int _mod)
        {

            for (int i = 0; i < 2; i++)
            {

                long[] temp = new long[2];

                for (int j = 0; j < 2; j++)
                {


                    for (int k = 0; k < 2; k++)
                    {

                        temp[j] += _a[i][k] * _b[k][j];
                    }

                    temp[j] %= _mod;
                }

                _result[i] = temp;
            }
        }

        // 얕은 복사
        public static void CopyMat(long[][] _copy, long[][] _dest)
        {

            for (int i = 0; i < 2; i++)
            {

                _dest[i] = _copy[i];
            }
        }
    }
}
