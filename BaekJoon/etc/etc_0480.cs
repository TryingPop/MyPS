using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : 토너먼트
    문제번호 : 1057번
   
    수학, 브루트포스 문제다
    logN의 시간으로 매번 라운드를 진행시켜 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0480
    {

        static void Main480(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            info[1]--;
            info[2]--;

            int ret = 1;
            while (true)
            {

                if ((info[1] & 1) == 1) info[1]--;
                if ((info[2] & 1) == 1) info[2]--;

                if (info[1] == info[2]) break;

                info[1] /= 2;
                info[2] /= 2;
                ret++;
            }

            Console.WriteLine(ret);
        }
    }

#if other
// #include <cstdio>
int main()
{

    int n,a,b;
    scanf("%d%d%d",&n,&a,&b);
    for(n=0;a++-b++;a/=2,b/=2,n++);
    printf("%d",n);
}
#endif
}
