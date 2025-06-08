using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 12
이름 : 배성훈
내용 : 카드 구매하기
    문제번호 : 11052번

    dp 문제다.
    dp[i] = val 를 i개를 살 때 최대 금액 val를 담는다.
    그러면 점화식은 dp[i] = max(dp[j] + dp[i - j])가 만들어진다.
*/

namespace BaekJoon.etc
{
    internal class etc_1269
    {

        static void Main1269(string[] args)
        {

            int n;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[] max = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    max[i] = arr[i];
                    for (int j = 1; j <= i; j++)
                    {

                        max[i] = Math.Max(max[i], max[i - j] + max[j]);
                    }
                }

                Console.Write(max[n]);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
                int ReadInt()
                {

                    int c, ret = 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }

#if other
// #include <stdio.h>


int main()
{
    int a=0, b=0, i=0, j=0, A[2000]= {0, };
    scanf("%d", &a);
    for(i=0;i<a;i++) scanf("%d", A+i);
    
    for(i=0;i<a;i++)
    {   

        for(j=0;j<i;j++)
        {

            if(A[i]<A[j]+A[i-1-j]) A[i]=A[j]+A[i-j-1];
        }
    }

    printf("%d", A[i-1]);
}

#endif
}
