using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 4
이름 : 배성훈
내용 : Σ
    문제번호 : 13172번

    분할 정복을 이용한 거듭제곱 문제다.
    아이디어는 다음과 같다.
    문제에서 친절하게 역원 구하는 방법을 페르마 소정리를 쓰면 된다고 직접 알려준다.
    단순히 최대 공약수를 구해서 더하는 경우 20개의 서로 다른 소수를 분모로 입력되는 경우 가뿐히 long 범위를 벗어난다.
    그래서 다른 방법이 필요했고 모듈러 연산에서 다음 식을 이용했다.
        a x b^-1 + c x d^-1 = (da + bc) x (bd)^-1 (mod p)
    그리고 역원은 분할 정복을 이용한 거듭제곱을 이용했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1151
    {

        static void Main1151(string[] args)
        {

            int MOD = 1_000_000_007;

            int n;
            (int a, int b)[] dices;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long a = dices[0].a;
                long b = dices[0].b;

                for (int i = 1; i < n; i++)
                {

                    a = (a * dices[i].b + b * dices[i].a) % MOD;
                    b = (b * dices[i].b) % MOD;
                }

                long ret = (a * GetPow(b, MOD - 2)) % MOD;
                Console.Write(ret);
            }

            long GetPow(long _a, long _exp)
            {

                long ret = 1;
                while(_exp > 0)
                {

                    if ((_exp & 1L) == 1) ret = (ret * _a) % MOD;
                    _a = (_a * _a) % MOD;
                    _exp >>= 1;
                }

                return ret;
            }

            int GetGCD(int _a, int _b)
            {

                while (_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                dices = new (int a, int b)[n];
                for (int i = 0; i < n; i++)
                {

                    int n = ReadInt();
                    int s = ReadInt();

                    int gcd = GetGCD(n, s);
                    n /= gcd;
                    s /= gcd;
                    dices[i] = (s, n);
                }

                sr.Close();

                int ReadInt()
                {

                    int c, ret = 0;
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
