using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 10
이름 : 배성훈
내용 : 달팽이3
    문제번호 : 1959번

    수학, 많은 조건 분기 문제다
    예제를 몇 개 해보니, 6가지 경우로 구분할 수 있었다

    아이디어는 다음과 같다
    행과 열의 대소 관계로 3가지 경우가 나온다
    그리고 작은 쪽이 홀수 짝수로 2가지 나온다
    서로 독립변수이므로 총 6가지 경우가 나온다

    이에 해당하는 정답을 찾아 제출하니 이상없이 통과했다
    단위는 2배 연산을 하기에 21억 * 2 > int.MaxValue이므로 long으로 연산했다
*/

namespace BaekJoon.etc
{
    internal class etc_0496
    {

        static void Main496(string[] args)
        {

            long[] input = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

            long ret1 = 0;
            long ret2 = 0;
            long ret3 = 0;
            if (input[0] < input[1])
            {

                ret1 = (input[0] - 1) * 2;
                ret2 = input[0] / 2;

                if (input[0] % 2 == 0) ret3 = (input[0] / 2) - 1;
                else ret3 = input[1] - 1 - (input[0] / 2);
            }
            else if (input[1] < input[0])
            {

                ret1 = (input[1] * 2) - 1;
                ret3 = (input[1] - 1) / 2;

                if (input[1] % 2 == 0) ret2 = input[1] / 2;
                else ret2 = input[0] - 1 - (input[1] / 2);
            }
            else
            {

                ret1 = (input[0] - 1) * 2;
                ret2 = input[0] / 2;

                if (input[0] % 2 == 1) ret3 = input[0] / 2;
                else ret3 = (input[0] / 2) - 1;
            }

            Console.WriteLine(ret1);
            Console.Write($"{ret2 + 1} {ret3 + 1}");
        }
    }

#if other
import java.util.*;
import java.io.*;

public class Main {
	
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		long M = Integer.parseInt(st.nextToken());
		long N = Integer.parseInt(st.nextToken());
		long sum = 0;
		long r=0;
		long c=0;
		if(M <= N) {
			
			sum = (M-1) * 2;
			r = M/2 + 1;
			if(M % 2 == 0) {
				c = M/2;
			} else {
				c= N - M/2;
			}
			
		} else {
			
			sum = N*2 - 1;
			c = (N+1) / 2;
			if(N % 2 == 0) {
				r = N/2 + 1;
			} else {
				r = M - N/2;
			}
			
		}
		
		System.out.println(sum);
		System.out.print(r +" " + c);
		
	}
}
#elif other2
n, m = map(int,input().split())
d = (min(n, m) - 1) // 2
ans = 4*d
row = 1+d
col = 1+d
n -= 2*d
m -= 2*d

if n == 1:
    col += (m - 1)
elif m == 1:
    ans += 1
    row += (n - 1)
elif n == 2:
    ans += 2
    row += 1
else:
    ans += 3
    row += 1
print(ans)
print('%d %d' % (row, col))
#endif
}
