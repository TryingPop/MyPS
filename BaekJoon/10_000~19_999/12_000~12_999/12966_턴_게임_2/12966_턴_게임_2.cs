using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 2
이름 : 배성훈
내용 : 턴 게임 2
    문제번호 : 12966번

    수학, 그리디 문제다.    
    x + y는 제곱수여야 한다.
    2인 경우는 만들 수 없다.
    이외의 경우는 만들 수 있다.

    그리고 최솟값은 큰 값으로 빼가는데, 0으로 만드는 횟수와 같다.
    마지막에 1이 남는 경우 1을 한 번 더 뺀다고 가정하고 구하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1508
    {

        static void Main(string[] args)
        {

            long x, y;

            Input();

            GetRet();

            void GetRet()
            {

                long sqrt = (long)(Math.Sqrt(x + y) + 1e-9);
                int ret = -1;

                if (sqrt * sqrt == x + y && x != 2 && y != 2)
                {

                    ret = 0;
                    for (long i = sqrt; i >= 1; i--)
                    {

                        long score = i * 2 - 1;
                        if (score > x) continue;
                        x -= score;
                        ret++;
                    }

                    if (x != 0) ret++;
                }
                
                Console.Write(ret);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                x = long.Parse(temp[0]);
                y = long.Parse(temp[1]);
            }
        }
    }

#if other
// #include<stdio.h>
main(){
	long long x,y;scanf("%lld %lld",&x,&y);
	long long n = 0;
	while(n*n<x+y)n++;
	if(n*n==x+y){
		long long left = x;
		int cnt = 0;
		for(long long i = n-1;i>=0;i--){
			if(left>=(i<<1|1)&&left-(i<<1|1)!=2)left-=i<<1|1,cnt++;
		}
		if(left==0)printf("%d",cnt);
		else printf("-1");
	}else printf("-1");
}

#endif
}
