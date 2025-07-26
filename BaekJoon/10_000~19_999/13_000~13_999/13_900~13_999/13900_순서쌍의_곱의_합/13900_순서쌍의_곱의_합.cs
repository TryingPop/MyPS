using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 23
이름 : 배성훈
내용 : 순서쌍의 곱의 합
    문제번호 : 13900번

    수학, 누적합 문제다.
    서로 다른 두 수를 뽑는 모든 경우 중 a를 뽑는 경우를 보면
    a와 a가 아닌 모든 경우를 뽑는 것이다.
    sum을 모든 원소의 총합이라 하면
    분배법칙으로 a x (sum - a) 임을 알 수 있다.
    이렇게 모두 찾아서 누적하면 2번씩 사용되므로 반으로 나누면 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1784
    {

        static void Main1784(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            long sum = 0;
            int[] arr = new int[n];

            for (int i= 0; i < n; i++)
            {

                int cur = ReadInt();
                arr[i] = cur;

                sum += cur;
            }

            long ret = 0;
            for (int i = 0; i < n; i++)
            {

                ret += (sum - arr[i]) * arr[i];
            }

            ret /= 2;
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
