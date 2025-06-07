using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 6
이름 : 배성훈
내용 : 1의 개수 세기
    문제번호 : 9527번

    수학, 누적 합, 비트마스킹 문제다.
    이진법 수를 변형한 뒤, 각 자릿수마다 얼마나 사용되었는지 확인했다.
    그러니 i번째 자리수는 (num / (1 << (i + 1))) * (1 << i) + (num % 1 << (i + 1)) - (1 << i) + 1개 존재함을 알 수 있다.
    그렇게 누적해가니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1160
    {

        static void Main1160(string[] args)
        {

            long a, b;

            Solve();
            void Solve()
            {

                Input();

                Console.Write(Cnt(b) - Cnt(a - 1));
            }

            long Cnt(long _find)
            {

                long ret = 0;
                for (int i = 0; i < 55; i++)
                {

                    long mul = 1L << i;
                    long div = 1L << (i + 1);

                    ret += (_find / div) * mul;
                    long chk = (_find % div) - mul + 1;
                    if (chk <= 0) continue;
                    ret += chk;
                }

                return ret;
            }


            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                a = long.Parse(temp[0]);
                b = long.Parse(temp[1]);
            }
        }
    }

#if other
// #include<cstdio>

long long calc(long long a, int t=53)
{
	long long x=1LL<<(t+1),r=0;
	for (int i = t; i > 0; i--)
	{
		x >>= 1;
		if (a&x)
		{
			r += (x>>1)*i;
			return r + (a-x+1) + calc(a - x, i-1);
		}
	}
	return a%2;
}

int main()
{
	long long a, b;
	scanf("%lld%lld", &a,&b);
	printf("%lld\n", calc(b)-calc(a-1));
}
#endif
}
