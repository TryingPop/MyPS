using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 11
이름 : 배성훈
내용 : 행렬 제곱
    문제번호 : 10830
*/

namespace BaekJoon._27
{
    internal class _27_07
    {

        static void Main7(string[] args)
        {

            //입력
            // 입력 크기가 5*5 이하이므로 스트림리더 이용 X
            long[] inputs = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();
            int[][] mat = new int[inputs[0]][];
            int[][] result = new int[inputs[0]][];
            int mod = 1_000;

            for (int i = 0; i < inputs[0]; i++)
            {

                // 매트릭스 가져오기
                mat[i] = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                // result를 단위 행렬로 만든다!
                result[i] = new int[inputs[0]];
                result[i][i] = 1;
            }

            // 제곱 연산
            // 최대 1억번 제곱하므로 앞에서 푼 아이디어를 이용!

            // 계산용도
            int[][] calc = new int[inputs[0]][];
            while (inputs[1] != 0)
            {

                if (inputs[1] % 2 == 1)
                {

                    // 곱셈
                    MultipleMat(result, mat, calc, inputs[0], mod);
                    CopyMat(calc, result, inputs[0]);
                }

                // 곱하고 나누고
                inputs[1] /= 2;
                MultipleMat(mat, mat, calc, inputs[0], mod);
                CopyMat(calc, mat, inputs[0]);
            }

            // 출력
            for (int i = 0; i < inputs[0]; i++)
            {

                for (int j = 0; j < inputs[0]; j++)
                {

                    Console.Write($"{result[i][j]} ");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// 행렬 곱
        /// </summary>
        public static void MultipleMat(int[][] _a, int[][] _b, int[][] _result, long _size, int _mod)
        {

            for (int i = 0; i < _size; i++)
            {

                // 결과에 담을꺼
                // 초기에 0 으로 초기화 되어져 있다
                int[] temp = new int[_size];

                for (int j = 0; j < _size; j++)
                {

                    // 곱셈 연산
                    // 매번 mod 연산을 해서 느리다 long을 했다면 빠를거 같다
                    for (int k = 0;  k < _size; k++)
                    {

                        temp[j] += _a[i][k] * _b[k][j];
                        temp[j] %= _mod;
                    }
                }

                _result[i] = temp;
            }
        }

        public static void CopyMat(int[][] _copy, int[][] _dest, long _size)
        {

            for (int i = 0; i < _size; i++)
            {

                _dest[i] = _copy[i];
            }
        }
    }
}
