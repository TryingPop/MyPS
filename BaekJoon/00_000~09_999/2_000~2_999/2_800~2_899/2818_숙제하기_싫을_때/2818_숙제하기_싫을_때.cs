using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 21
이름 : 배성훈
내용 : 숙제하기 싫을 때
    문제번호 : 2818번

    수학, 구현 문제다.
    우선 열이 4이면 무조건 합이 14임을 알 수 있다.
    그래서 4로 나눈 나머지부분만 실행하고 4개씩 14로 처리한다.
    이후 행은 10만으로 브루트포스가 가능하므로 브루트포스로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1561
    {

        static void Main1561(string[] args)
        {

            int[] size = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int up = 1;
            int front = 2;
            int right = 3;

            int add = size[1] / 4;
            long ret = 14L * (size[1] / 4) * size[0];
            size[1] %= 4;

            if (size[1] > 0)
            {

                for (int i = 0; i < size[0]; i++)
                {


                    ret += up;
                    if ((i & 1) == 0) RightMove();
                    else LeftMove();

                    int temp = 7 - front;
                    front = up;
                    up = temp;
                }
            }

            Console.Write(ret);

            void LeftMove()
            {

                for (int j = 1; j < size[1]; j++)
                {

                    int temp = 7 - up;
                    up = right;
                    right = temp;

                    ret += up;
                }
            }

            void RightMove()
            {

                for (int j = 1; j < size[1]; j++)
                {

                    int temp = 7 - right;
                    right = up;
                    up = temp;

                    ret += up;
                }
            }
        }
    }

#if other
// #include<stdio.h>
int a=5,b=6,c=4,d=1,e=3,f=2,t,R,r,C,k;
long long n=0;
int main()
{
	scanf("%d%d",&R,&C);
	for(r=0;r<R;r++)
	{
		n+=d;
		n+=14*((C-1)/4);
		for(k=0;k<(C-1)%4;k++)
		{
			if(r%2)
			{t=b;b=c;c=d;d=e;e=t;}
			else
			{t=e;e=d;d=c;c=b;b=t;}
			n+=d;
		}
		t=d;d=a;a=b;b=f;f=t;
	}
	printf("%lld",n);
}
#endif
}
