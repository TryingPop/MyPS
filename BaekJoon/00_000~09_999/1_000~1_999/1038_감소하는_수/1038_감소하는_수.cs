using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 5
이름 : 배성훈
내용 : 감소하는 수
    문제번호 : 1038번

    브루트포스, 백트래킹 문제다.
    직접 이어붙여 나가는 식으로 만들었다.
    뒤에 고민해보니 백트래킹이 더 구현하기나 접근이 쉬운거 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1374
    {

        static void Main1374(string[] args)
        {

            // 아래 while문을 먼저 돌려 최대 길이를 찾았다.
            long[] arr = new long[1_023];

            for (int i = 1; i < 10; i++)
            {

                arr[i] = i;
            }

            int s = 1;
            int e = 9;

            while (s < e)
            {

                int idx = e + 1;
                for (int i = s; i <= e; i++)
                {

                    long end = arr[i] % 10;
                    long add = arr[i] * 10;
                    for (int j = 0; j < end; j++)
                    {

                        arr[idx++] = add + j;
                    }
                }

                s = e + 1;
                e = idx - 1;
            }

            int n = int.Parse(Console.ReadLine());

            if (n >= arr.Length) Console.Write(-1);
            else Console.Write(arr[n]);
        }
    }

#if other
// #include<stdio.h>

int main()
{
    int l,E=9;
    long long int DP[1024]={0,1,2,3,4,5,6,7,8,9};
    scanf("%d",&l);
    if(l>1022)
    {
        printf("-1");
        return 0;
    }
    for(int i=0 ; E<l ; i++)
    {
        for(int j=0 ; j<DP[i]%10 ; j++)
            DP[++E] = DP[i]*10+j;
    }
    printf("%lld",DP[l]);

}
#endif
}
