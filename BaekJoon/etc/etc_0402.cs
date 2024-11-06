using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 날짜 계산
    문제번호 : 1476번

    수학, 브루트포스, 정수론 문제다
    합동식에서 나머지 정리를 이용하면 풀 수 있다 방법은 다음과 같다
    우선 입력값에 1씩 뺀다
    15에서 28 * 19 의 역원, 28에서 15 * 19의 역원, 19에서 15 * 28의 역원을 찾아야한다
    그리고 각각에 입력 순서대로 곱해주고 합한 값을 15 * 19 * 28로 나눈 나머지에 + 1이 정답이 된다

    수의 크기가 작아 계산 시간 >> 컴퓨터 연산시간 이라서 브루트포스로 해결했다
    합동식의 나머지 정리 풀이는 다른 사람 코드로 첨부한다
*/

namespace BaekJoon.etc
{
    internal class etc_0402
    {

        static void Main402(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            for (int i = 0; i < 3; i++)
            {

                input[i]--;
            }

            int ret = 0;
            for (int i = 0; i < 15 * 28 * 19; i++)
            {

                if (i % 15 != input[0]) continue;
                if (i % 28 != input[1]) continue;
                if (i % 19 != input[2]) continue;
                ret = i;
                break;
            }

            Console.WriteLine(ret + 1);
        }
    }

#if other
/*
28*19*s1 % 15 == 1
15*19*s2 % 28 == 1
15*28*s3 % 19 == 1
s1 == 13, s2 == 17, s3 == 10

ans = (E*28*19*13 + S*15*19*17 + M*15*28*10)%(15*28*19)
    = (E*6916 + S*4845 + M*4200) % 7980
*/
// #include <stdio.h>
main(E,S,M,ans){
    setbuf(stdin, NULL); setbuf(stdout, NULL);
    scanf("%d%d%d", &E, &S, &M);
    ans = (E*6916 + S*4845 + M*4200) % 7980;
    printf("%d", ((ans != 0)? ans : 7980));
}
#endif
}
