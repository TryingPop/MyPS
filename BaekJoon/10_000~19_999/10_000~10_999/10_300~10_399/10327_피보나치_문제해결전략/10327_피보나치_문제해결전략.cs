using System;
using System.IO;

/*
날짜 : 2025. 6. 25
이름 : 배성훈
내용 : 피보나치 문제해결전략
    문제번호 : 10327번

    확장 유클리드 호제법 문제다.
    여기서 i번째 피보나치 수열의 값을 fibo[i]라 하자.
    참고로 fibo[0] = 1, fibo[1] = 1이다.

    i > 0에 대해 fibo[i - 1] * a + fibo[i] * b = n인
    가장 작은 양수 b와 b > a인 양수 a를 찾아야 한다.

    피보나치 수열의 점화식과 유클리드 호제법을 보면 
    fibo[i - 1], fibo[i]의 최대 공약수는 1이다!

    그래서 모든 i > 0에 대해서 a * fibo[i - 1] + b * fibo[i] = 1인 a, b는 항상 존재한다!
    양변에 n을 곱하면 n * a * fibo[i - 1] + n * b * fibo[i] = n이다.
    이러한 a, b를 확장된 유클리드 호제법으로 찾는다.

    그러면 na(= n * a), nb(= n * b)의 값은 유일하지 않다.
    (na + fibo[i]) * fibo[i - 1] + (nb - fibo[i - 1]) * fibo[i] = n이성립한다.
    실제로 모든 정수 k에 대해 na' = na + k * fibo[i], nb' = nb - k * fibo[i - 1]이 성립한다.

    여기서 na' < nb'인 가장 작은 nb'를 찾아보자.
    na' = na + k * fibo[i], nb' = nb - k * fibo[i - 1]로
    na + k * fibo[i] < nb - k * fibo[i - 1]
    => k < (nb - na) / (fibo[i - 1] + fibo[i])의 식을 얻는다.
    그리고 k가 커질수록 nb'의 값이 작아짐은 자명하다.

    그래서 가장 큰 값을 찾아 na'와 nb'의 값을 구한다.
    그리고 0 < na' ≤ nb'를 만족하는지 찾으면 된다.
    그리고 가장 큰 i가 nb'의 값이 작아짐은 그리디로 확인 가능하다!
*/

namespace BaekJoon.etc
{
    internal class etc_1550
    {

        static void Main1550(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int LEN = 44;
            long[] fibo;

            SetFibo();

            int q = ReadInt();

            while (q-- > 0)
            {

                int n = ReadInt();

                for (int i = LEN - 1; i >= 1; i--)
                {

                    if (fibo[i] > n) continue;
                    ExtendEuclid(fibo[i - 1], fibo[i], out long a, out long b);

                    // fibo[i - 1] * a + fibo[i] * b = n
                    a *= n;
                    b *= n;

                    // a' = fibo[i] * k + a < fibo[i - 1] * k + b = b'
                    // 를 만족하는 가장 큰 a'찾기
                    long div = fibo[i] + fibo[i - 1];
                    long k = (b - a) / div;

                    a += k * fibo[i];
                    b -= k * fibo[i - 1];

                    if (a > b)
                    {

                        a -= fibo[i];
                        b += fibo[i - 1];
                    }

                    if (a <= 0 || b <= 0) continue;

                    sw.Write($"{a} {b}\n");
                    break;
                }
            }

            long ExtendEuclid(long _a, long _b, out long _u, out long _v)
            {

                // 확장 유클리드 호제법
                _u = 1;
                _v = 0;

                long pu = 0, pv = 1;

                while (_b > 0)
                {

                    long q = _a / _b;
                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;

                    temp = _u - q * pu;
                    _u = pu;
                    pu = temp;

                    temp = _v - q * pv;
                    _v = pv;
                    pv = temp;
                }

                return _a;
            }

            void SetFibo()
            {

                fibo = new long[LEN];

                fibo[0] = 1;
                fibo[1] = 1;

                for (int i = 2; i < LEN; i++)
                {

                    fibo[i] = fibo[i - 1] + fibo[i - 2];
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

                    while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
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
