using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 30
이름 : 배성훈
내용 : Fenwick Tree
    문제번호 : 3576번

    비트마스킹, 해 구성하기 문제다.
    팬윅 트리에 값이 어떻게 저장되는지 알면 된다.
    
    i번 자리를 보면 i % 2 == 1인 경우 개인 저장된다.
    반면 i % 2 == 0인 경우 i - 1의 값과 더해줘야 한다.
    이후 i' = i / 2로 놓는다.
    여기서 i' % 2 == 1인 경우 종료하면 된다.
    반면 i' % 2 == 0인 경우 i - 2의 값을 누적해줘야 한다.
    i'' = i' / 2이라 하자.
    i'' % 2 == 1인 경우 종료한다.
    i'' % 2 == 0인 경우 i - 4를 누적한다.
    이렇게 탈출할 때까지 팬윅트리에 들어갈 값을 누적해주면 된다.

    이렇게 찾는 경우 많아야 인덱스가 1부터 시작하므로 log2 n 번 안에 끝남을 알 수 있다.
    그리고 팬윅트리를 만들면서 비교한다.

    이제 최소한으로 바꾸면서 일치하게 해야 한다.
    j < i인 경우 j는 i번에 영향을 줄 수 있으나 i는 j에 영향을 주지 않는다.
    이는 큰 값을 변경하는 경우 작은쪽에 영향을 주지 않는다.
    그래서 작은 값부터 바꿔가면서 확인한다.
    여기서 최소로 바꾸는 것은 팬윅 트리의 값과 arr의 값이 일치하게 만들기 위해 1개의 값만 바꿔주는 것이다.
    우선 홀수번 자리는 자기자신만 들어가므로 언제나 일치함을 알 수 있다.
    반면 짝수번 자리는 앞의 방법으로 복수개가 들어간다.
    그런데 현재 자리 - 1의 값은 언제나 들어간다.
    현재 값을 제외하고 나머지의 합이 0이되면 팬윅트리의 값과 배열이 일치하게 되므로 이전 값을 수정해서 0으로 맞춰줬다.

    이렇게 찾아갈 경우 바꿔야할 위치와 바꾸는 횟수가 일치하고 이게 최소한으로 바꾼 것이므로 그리디로 최소임을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1921
    {

        static void Main1921(string[] args)
        {

            int n;
            long[] arr;

            Input();

            GetRet();

            Output();

            void Output()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= n; i++)
                {

                    sw.Write($"{arr[i]} ");
                }
            }

            void GetRet()
            {

                int[] tPow = new int[18];
                tPow[0] = 1;
                for (int i = 1; i < tPow.Length; i++)
                {

                    tPow[i] = tPow[i - 1] << 1; 
                }

                long[] fenwick = new long[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    fenwick[i] = arr[i];
                    if ((i & 1) == 1) continue;

                    int cur = i;
                    int dis = 1;
                    long sum = 0;

                    while ((cur & 1) == 0)
                    {

                        sum += fenwick[i - dis];
                        dis <<= 1;
                        cur >>= 1;
                    }

                    fenwick[i - 1] -= sum;
                    arr[i - 1] -= sum;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new long[n + 1];
                for (int i = 1; i <= n; i++)
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
}
