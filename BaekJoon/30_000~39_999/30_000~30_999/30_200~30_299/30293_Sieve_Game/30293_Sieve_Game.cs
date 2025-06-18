using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 4
이름 : 배성훈
내용 : Sieve Game
    문제번호 : 30293번

    그리디 문제다.
    i의 배수인 모든 j에 대해
    pj의 값을 1씩 늘리거나 1씩 줄일 수 있다.
    이 말은 i가 j의 배수이면 i에 영향을 받는다.
    또한 k > i이면 k는 i에 영향을 줄 수 없다.
    그래서 앞에서부터 하나씩 맞춰가야 하고, 이게 최소임이 그리디로 보장됨을 알 수 있다.
    입력이 n = 20만이고, 에라토스 테네스 체로 접근해도 1초안에 풀린다.
*/

namespace BaekJoon.etc
{
    internal class etc_1515
    {

        static void Main1515(string[] args)
        {

            int n;
            long[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (arr[i] == 0) continue;
                    ret += Math.Abs(arr[i]);

                    long sub = arr[i];
                    for (int j = i; j <= n; j += i)
                    {

                        arr[j] -= sub;
                    }
                }

                Console.Write(ret);
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

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
