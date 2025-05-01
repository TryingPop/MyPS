using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 30
이름 : 배성훈
내용 : Tricknology
    문제번호 : 28427번

    에라토스테네스의 체, 누적 합 문제다.
    아이디어는 다음과 같다.
    우선 x < y에 대해 y - x > 1인 경우 소수가 안됨 알 수 있다.
    만약 홀수인 경우 중앙값을 z라 하면 z - k, z - k + 1, ..., z - 1, z , z + 1, ..., z + k이고
    이를 모두 더하면 z * (2k + 1)이므로 2k + 1로 나눠떨어진다.

    이제 짝수인 경우를 보면, x, x + 1, ..., x + 2k - 1이다.
    이를 더하면 2k * x + (2k - 1) * k이다.
    길이가 4 이상이므로 k > 1이므로 k로 나눠떨어짐을 뜻한다.
    그리고 이는 합성수임을 뜻한다.
    그래서 길이 2인 경우에 한해서만 소수를 찾으면 된다.

    그러면 y는 최대 50만이고 길이 2의 최댓값은 999_999이다.
    이에 에라토스테네스로 100만 미만의 모든 소수를 찾고 누적 합 아이디어로 갯수를 누적해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1595
    {

        static void Main1595(string[] args)
        {

            int[] sum;

            SetPrime();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                int q = ReadInt();

                while (q-- > 0)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    f = GetIdx(f) - 1;
                    t = GetIdx(t - 1);

                    sw.Write($"{sum[t] - sum[f]}\n");
                }

                int GetIdx(int _num)
                {

                    return (_num << 1) | 1;
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

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }

            void SetPrime()
            {

                sum = new int[1_000_000];
                Array.Fill(sum, 1);
                sum[0] = 0;
                sum[1] = 0;

                for (int i = 2; i < sum.Length; i++)
                {

                    if (sum[i] == 0) continue;

                    for (int j = i << 1; j < sum.Length; j += i)
                    {

                        sum[j] = 0;
                    }
                }

                for (int i = 1; i < sum.Length; i++)
                {

                    sum[i] += sum[i - 1];
                }
            }
        }
    }
}
