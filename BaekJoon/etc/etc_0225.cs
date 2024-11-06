using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 14
이름 : 배성훈
내용 : 주식을 안전하게 (Easy)
    문제번호 : 29809번

    수학? 구현 문제다
    c의 오버 플로우로 한 번 틀렸다

    주된 아이디어는 다음과 같다

    D(k) = M_k - M_(k-1)이라 하자
    n >= p인 n에 대해
    D(n) + c * D(n-1) + c^2 * D(n-2) + ... + c^(p - 1) * D(n - p + 1) = 0
    을 만족한다

    그런데 다음항을 보면
    D(n + 1) + c * (D(n) + c * D(n - 1) + c^2 * D(n-2) + ... + c^(p - 1) * D(n - p + 1)) - c^p * D(n - p + 1) 
        = D(n + 1) + c * 0 -  - c^p * D(n - p + 1) = 0 
    이므로

    D(n + 1) = c^p * D(n - p + 1)
    임을 알 수 있다

    이식으로 D(k)를 찾았다
    다만 c가 최대 10이고 p가 최대 10이라 10^10 = 100억까지 갈 수 있어
    long으로 설정했다 여기서 c^p을 10억 + 7 로 나눈 나머지로 안해서 한 번 틀렸다;
    이외는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0225
    {

        static void Main225(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int p = ReadInt(sr);
            int c = ReadInt(sr);
            int k = ReadInt(sr);

            int[] m = new int[p];

            for (int i = 0; i < p; i++)
            {

                m[i] = ReadInt(sr);
            }

            sr.Close();

            long[] diff = new long[k + 1];
            for (int i = 1; i < p; i++)
            {

                diff[i] = m[i] - m[i - 1];
            }

            diff[p] = 0;

            long mul = c;
            for (int i = p - 1; i >= 1; i--)
            {

                diff[p] -= mul * diff[i];
                diff[p] %= 1_000_000_007;
                mul *= c;
                // 여기서 나머지 연산 안해도된다
                // p - 1이하인 인덱스에 대해서는 10^6이고,
                // mul의 최대값은 10^9이다
                // 둘이 곱해도 오버플로우 안나고 밑에서 해줘도 된다
                // 실제로 여기서연산하면 72ms, 밑에서 하면 68ms 나왔다
                // mul %= 1_000_000_007;
            }

            mul %= 1_000_000_007;

            for (int i = p + 1; i <= k; i++)
            {

                diff[i] = diff[i - p] * mul;
                diff[i] %= 1_000_000_007;
            }

            long ret = diff[k] < 0 ? -diff[k] : diff[k];
            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }

                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}
