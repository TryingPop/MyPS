using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 7
이름 : 배성훈
내용 : 본대 산책2
    문제번호 : 12850번

    그래프 이론 문제다.
    n개의 간선을 이용해 A 노드에서 -> B 노드로 가는 경우의 수는
    각 노드에서 다른 노드로 이어진 간선으로 행렬을 만든다.
    여기서 mat[i][j] = val는 i에서 j로 이어진 간선의 수이다.
    그리고 mat을 n 거듭제곱 한 값 mat^n[i][j] 의 값이 
    n개의 간선을 지나 i -> j로 가는 경우의 수와 일치한다.
    그래서 mat 으로 행렬을 만든 뒤 분할 정복을 이용한 거듭제곱으로 구했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1164
    {

        static void Main1164(string[] args)
        {

            long MOD = 1_000_000_007;
            int n = int.Parse(Console.ReadLine());

            long[][] mat = new long[8][]
            {

                // 0 : 정보과학관
                // 1 : 전산관
                // 2 : 미래관
                // 3 : 신양관
                // 4 : 한경직기념관
                // 5 : 진리관
                // 6 : 학생회관
                // 7 : 형남공학관
                new long[8] { 0, 1, 1, 0, 0, 0, 0, 0 }, // 정보과학관
                new long[8] { 1, 0, 1, 1, 0, 0, 0, 0 }, // 전산관
                new long[8] { 1, 1, 0, 1, 1, 0, 0, 0 }, // 미래관
                new long[8] { 0, 1, 1, 0, 1, 1, 0, 0 }, // 신양관
                new long[8] { 0, 0, 1, 1, 0, 1, 0, 1 }, // 한경직기념관
                new long[8] { 0, 0, 0, 1, 1, 0, 1, 0 }, // 진리관
                new long[8] { 0, 0, 0, 0, 0, 1, 0, 1 }, // 학생회관
                new long[8] { 0, 0, 0, 0, 1, 0, 1, 0 }  // 형남공학관
            };

            long[][] calc;

            Console.Write(GetPow(n));

            void MatMul(long[][] _a, long[][] _b)
            {

                for (int i = 0; i < 8; i++)
                {

                    for (int j = 0; j < 8; j++)
                    {

                        calc[i][j] = 0;
                        for (int k = 0; k < 8; k++)
                        {

                            calc[i][j] 
                                = (calc[i][j] + (_a[i][k] * _b[k][j])) % MOD;
                        }
                    }
                }

                Copy(calc, _a);
            }

            void Copy(long[][] _src, long[][] _dst)
            {

                for (int i = 0; i < 8; i++)
                {

                    for (int j = 0; j < 8; j++)
                    {

                        _dst[i][j] = _src[i][j];
                    }
                }
            }

            long GetPow(int _exp)
            {

                long[][] ret = new long[8][];
                calc = new long[8][];

                if (_exp <= 1) return 0;
                for (int i = 0; i < 8; i++)
                {

                    ret[i] = new long[8];
                    ret[i][i] = 1;
                    calc[i] = new long[8];
                }

                while (_exp > 0)
                {

                    if ((_exp & 1L) == 1L) MatMul(ret, mat);
                    MatMul(mat, mat);
                    _exp >>= 1;
                }

                return ret[0][0];
            }
        }
    }
}
