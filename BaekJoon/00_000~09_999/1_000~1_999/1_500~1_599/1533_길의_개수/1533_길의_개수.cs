using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 16
이름 : 배성훈
내용 : 길의 개수
    문제번호 : 1533

    그래프 이론 문제다.
    처음에는 n일차에 n + i 경로로 가는 경우를 누적해주면 되지 않을까 했다.
    예제는 맞았는데, 제출하니 틀렸다;
    
    그래서 검색을 하니 다른 방법으로 풀어야 했다.
    0일차, 1일차, 2일차, 3일차, 4일차 경로를 나누고
    4일차 -> 3일차 -> 2일차 -> 1일차 -> 0일차 형태로 단방향 경로를 놓아야한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1191
    {

        static void Main1191(string[] args)
        {

            long MOD = 1_000_003;
            long[][] calc, mat;
            int n, s, e, t;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                long[][] ret = GetPow(mat, t);
                sw.Write(ret[5 * s][5 * e]);
                sw.Close();
            }

            void Fill(long[][] _arr, long _val = 0)
            {

                for (int i = 0; i < 5 * n; i++)
                {

                    for (int j = 0; j < 5 * n; j++)
                    {

                        _arr[i][j] = _val;
                    }
                }
            }

            void Copy(long[][] _src, long[][] _dst)
            {

                for (int i = 0; i < 5 * n; i++)
                {

                    for (int j = 0; j < 5 * n; j++)
                    {

                        _dst[i][j] = _src[i][j];
                    }
                }
            }

            long[][] GetPow(long[][] _a, int _exp)
            {

                long[][] ret = new long[5 * n][];
                for (int i = 0; i < 5 * n; i++)
                {

                    ret[i] = new long[5 * n];
                    ret[i][i] = 1;
                }

                while(_exp > 0)
                {

                    if ((_exp & 1) == 1) MatMul(ret, _a);
                    MatMul(_a, _a);
                    _exp >>= 1;
                }

                return ret;
            }

            void MatMul(long[][] _a, long[][] _b)
            {

                for (int i = 0; i < 5 * n; i++)
                {

                    for (int j = 0; j < 5 * n; j++)
                    {

                        for (int k = 0; k < 5 * n; k++)
                        {

                            calc[i][j] = (calc[i][j] + _a[i][k] * _b[k][j]) % MOD;
                        }
                    }
                }

                Copy(calc, _a);
                Fill(calc);
            }

            void SetArr()
            {

                calc = new long[n * 5][];
                for (int i = 0; i < n * 5; i++)
                {

                    calc[i] = new long[n * 5];
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                s = ReadInt() - 1;
                e = ReadInt() - 1;
                t = ReadInt();

                mat = new long[n * 5][];
                for (int i = 0; i < mat.Length; i++)
                {

                    mat[i] = new long[n * 5];

                }

                for (int i = 0; i < n; i++)
                {

                    for (int j = 1; j < 5; j++)
                    {

                        // i일차를 i - 1일차로 이동 가능하게 한다.
                        mat[i * 5 + j][i * 5 + j - 1] = 1;
                    }
                }

                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        int cur = sr.Read() - '0';
                        if (cur == 0) continue;
                        mat[i * 5][j * 5 + cur - 1] = 1;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
                int ReadInt()
                {

                    int c, ret = 0;
                    while (( c= sr.Read()) != -1 && c != ' ' && c != '\n')
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
