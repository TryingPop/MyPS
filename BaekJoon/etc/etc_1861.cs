using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 3
이름 : 배성훈
내용 : 점수를 최대로
    문제번호 : 29767번

    정렬, 그리디, 누적합 문제다.
    i번 학생이 j번 교실에 들어가는 경우 1 ~ j번까지 모두 방문한다.
    그래서 누적합을 이용하면 1 ~ j번 들어가는 점수를 O(1)에 찾을 수 있다.

    이제 최고 점수를 찾아야 한다.
    이는 가장 큰 k개를 더하면 그리디로 최대가 됨이 보장된다.
    그래서 정렬을 해서 최대 k개를 찾았다.

    정렬이 아니라면 우선순위 큐에 저장해
    k개를 꺼내도 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1861
    {

        static void Main1861(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();

            long[] sum = new long[n];
            for (int i = 0; i < n; i++)
            {

                sum[i] = ReadInt();
            }

            for (int i = 1; i < n; i++)
            {

                sum[i] += sum[i - 1];
            }

            Array.Sort(sum, (x, y) => y.CompareTo(x));
            long ret = 0;

            for (int i = 0; i < k; i++)
            {

                ret += sum[i];
            }

            Console.Write(ret);

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
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }
}
