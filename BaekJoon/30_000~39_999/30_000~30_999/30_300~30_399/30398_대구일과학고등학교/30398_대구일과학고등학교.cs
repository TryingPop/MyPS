using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 16
이름 : 배성훈
내용 : 대구일과학고등학교
    문제번호 : 30398번

    수학, 조합론, 모듈로 곱셈 역원, 페르마 소정리 문제다.
    (p1, p2, ..., pn) -> (q1, q2, ..., qn)으로의 이동 경우의 수는
    (∑(qi - pi))! / (∏((qi - pi)!)) 이다.
    중복이 많을거 같아 계산한거는 저장해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1192
    {

        static void Main1192(string[] args)
        {

            long MOD = 1_000_000_007;

            StreamReader sr;

            long[] fac;
            long[] inv;

            int p, n, d;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                long[] curPos = new long[d];
                long[] nextPos = new long[d];
                Array.Fill(curPos, 1);

                long ret = 1;
                for (int i = 0; i < p; i++)
                {

                    for (int j = 0; j < d; j++)
                    {

                        nextPos[j] = ReadInt();
                    }

                    ret = (ret * Cnt()) % MOD;
                    Swap();
                }

                for (int j = 0; j < d; j++)
                {

                    nextPos[j] = n;
                }

                ret = (ret * Cnt()) % MOD;
                Console.Write(ret);
                sr.Close();

                void Swap()
                {

                    long[] temp = curPos;
                    curPos = nextPos;
                    nextPos = temp;
                }

                long Cnt()
                {

                    long ret = 1;
                    long n = 0;
                    for (int j = 0; j < d; j++)
                    {

                        long move = nextPos[j] - curPos[j];
                        if (inv[move] == -1)
                        {

                            long mf = fac[move];
                            inv[move] = GetPow(mf, MOD - 2);
                        }

                        n += move;
                        ret = (ret * inv[move]) % MOD;
                    }

                    ret = (ret * fac[n]) % MOD;
                    return ret;
                }
            }

            long GetPow(long _a, long _exp)
            {

                long ret = 1;
                while (_exp > 0)
                {

                    if ((_exp & 1L) == 1L) ret = (ret * _a) % MOD;
                    _a = (_a * _a) % MOD;
                    _exp >>= 1;
                }

                return ret;
            }

            void SetArr()
            {

                fac = new long[1_000_001];
                inv = new long[1_000_001];

                fac[0] = 1;
                for (int i = 1; i <= 1_000_000; i++)
                {

                    fac[i] = (fac[i - 1] * i) % MOD;
                    inv[i] = -1;
                }

                inv[0] = 1;
                inv[1] = 1;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                d = ReadInt();
                p = ReadInt();
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
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