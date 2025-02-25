using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 3
이름 : 배성훈
내용 : 소수
    문제번호 : 1312번

    수학 문제다.
    초등학교에서 배운 나누기 방법을 구현하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1310
    {

        static void Main1310(string[] args)
        {

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            for (int i = 1; i < arr[2]; i++)
            {

                arr[0] = (arr[0] * 10) % arr[1];
            }

            Console.Write(((arr[0] * 10) / arr[1]) % 10);
        }
    }

#if other
// #include <cstdio>

int main(void) {
    long long A, B, N; scanf("%lld %lld %lld", &A, &B, &N);
    N--;

    long long div = 10 % B;
    long long result = 1;
    while(N){
		if(N % 2 == 1)result = result * div % B;
		div = div * div % B;
		N /= 2;
	}
    printf("%lld", A * result % B * 10 / B);
}
#endif
}
