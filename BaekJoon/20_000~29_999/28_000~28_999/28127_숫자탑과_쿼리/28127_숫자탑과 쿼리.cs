using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 15
이름 : 배성훈
내용 : 숫자탑과 쿼리
    문제번호 : 28127번

    수학, 이분 탐색 문제다.
    f층의 가장 큰 숫자를 수식으로 하면
    fa + (1 + 2 + ... + f - 1)d가 된다.

    그래서 층의 끝값을 이용해 이분탐색으로 층을 찾고
    몇 번째인지 구했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1546
    {

        static void Main1546(string[] args)
        {

            int E = 1_414;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] arr = new int[E + 1];
            for (int i = 2, j = 1; i < arr.Length; i++, j++)
            {

                arr[i] = j + arr[j];
            }

            int q = ReadInt();

            while (q-- > 0)
            {

                long a = ReadInt();
                long d = ReadInt();
                long find = ReadInt();

                int x = BinarySearch();         // 찾은 층
                long y = find - GetEnd(x - 1);  // 순서

                sw.Write($"{x} {y}\n");

                // 가장 큰 값을 기준으로 층을 찾는다.
                int BinarySearch()
                {

                    int l = 1;
                    int r = E;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;

                        if (GetEnd(mid) < find) l = mid + 1;
                        else r = mid - 1;
                    }

                    return r + 1;
                }

                // 해당 층의 가장 큰 값 찾기
                long GetEnd(int _f)
                    => _f * a + arr[_f] * d;
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
                    if (c == ' ' || c == '\n') return true;

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
