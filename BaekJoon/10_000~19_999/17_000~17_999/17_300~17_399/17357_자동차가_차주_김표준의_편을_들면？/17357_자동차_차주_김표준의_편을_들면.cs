using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 24
이름 : 배성훈
내용 : 자동차가 차주 김표준의 편을 들면?
    문제번호 : 17357번

    수학, 통계학, 누적합 문제다.
    슬라이딩 윈도우를 이용해 N^2에 문제를 풀었다.
    표준편차^2 = 분산이고 표준편차는 음이아닌 실수이므로
    분산의 크기로 비교해도 된다.

    그리고 X를 표본 분산 V(X)은 분배법칙을 이용하면 
    V(X) = E(X^2) - (E(X))^2 이다.
    그리고 길이를 n이라 하면 n > 0이고
    n x V(X) 의 값으로 비교했다.
    그러면 n x V(X) = n x ∑(xi)^2 - (∑xi)^2이다.
    그리고 n의 범위가 1000 이하이고, xi ≤ 1_000_000이므로 
    long 범위안에 표현이 가능하다.
    이렇게 슬라이딩 윈도우로 총합을 찾아 풀었다.

    다 풀고 생각해보니 슬라이딩 윈도우 없이 누적합으로도 해결될거 같다.
    기존 값의 제곱에 대해 누적합을 미리 기록해두면 매 경우 O(1)에 n x V(X)를 찾을 수 있다.
    시간 복잡도는 O(n^2)으로 같다.
    반면 공간은 제곱의 누적합 배열을 더 할당해야 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1785
    {

        static void Main1785(string[] args)
        {

            int n;
            long[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int[] ret = new int[n];

                for (int i = 1; i <= n; i++)
                {

                    long sumPow = 0, sum = 0;
                    int e = 0;

                    for (; e < i; e++)
                    {

                        sumPow += i * arr[e] * arr[e];
                        sum += arr[e];
                    }

                    long max = sumPow - sum * sum;
                    for (; e < n; e++)
                    {

                        int prev = e - i;
                        sumPow += i * (arr[e] * arr[e] - arr[prev] * arr[prev]);
                        sum += arr[e] - arr[prev];

                        long chk = sumPow - sum * sum;
                        if (max < chk)
                        {

                            max = chk;
                            ret[i - 1] = prev + 1;
                        }
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    sw.Write(ret[i] + 1);
                    sw.Write('\n');
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                arr = new long[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
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
        }
    }
}
