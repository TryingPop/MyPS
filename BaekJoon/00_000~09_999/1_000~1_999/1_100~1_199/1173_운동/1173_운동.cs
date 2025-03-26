using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 25
이름 : 배성훈
내용 : 운동
    문제번호 : 1173번

    시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1467
    {

        static void Main1467(string[] args)
        {

            int N, m, M, T, R;

            Input();

            GetRet();

            void GetRet()
            {

                if (M - m < T)
                {

                    Console.Write(-1);
                    return;
                }

                int time = 0;
                int cur = m;
                int cnt = 0;

                while (true)
                {

                    time++;
                    if (cur + T <= M)
                    {

                        cur += T;
                        cnt++;

                        if (cnt == N) break;
                    }
                    else
                    {

                        cur -= R;
                        if (cur < m) cur = m;
                    }
                }

                Console.Write(time);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                N = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                M = int.Parse(temp[2]);
                T = int.Parse(temp[3]);
                R = int.Parse(temp[4]);
            }
        }
    }

#if other
// #include<cstdio>
int a,b,c,d,e,f,s;
int main(){
	scanf("%d%d%d%d%d",&a,&b,&c,&d,&e);
	if(c-b<d){
		printf("-1");
		return 0;
	}
	f=b;
	while(a){
		if(f+d>c){
			f-=e;
			if(f<b)f=b;
		}
		else f+=d,a--;
		s++;
	}
	printf("%d",s);
}
#endif
}
