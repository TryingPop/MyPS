using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 21
이름 : 배성훈
내용 : Parentheses
    문제번호 : 15532번

    해 구성하기 문제다
    아이디어는 다음과 같다
    괄호 3개짜리를보자
    ()()(), ()(())는 이상없다
    
    반면 아래의 경우들은 다음과 같은 이동이 필요하다
    )))((( : 6번
    ))()(( : 5번
    )())(( : 4번
    // ()))(( : 3번 == ))(( 로 더 짧게 줄일 수 있다
    처럼 괄호를 구성하면 
    
    괄호 1개 -> 1 ~ 1
    괄호 2개 -> 2 ~ 3
    괄호 3개 -> 4 ~ 6
    괄호 4개 -> 7 ~ 10
    ...
    괄호 n개 -> (n * (n - 1) / 2) + 1 ~ n * (n + 1) / 2
    개의 이동으로 해결된다
    
    이를 제출하니 이상없이 통과된다
*/

namespace BaekJoon.etc
{
    internal class etc_1067
    {

        static void Main1067(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            // 최대 길이, 첫 (의 위치 찾기
            int a = 1, b = 2, c = 1;
            while (a < n)
            {

                a += b;
                b++;
                c++;
            }

            // 중심
            int d = a - n;
            char[] ret = new char[2 * c];
            for (int i = 0; i <= c; i++)
            {

                // 첫 (의 위치?
                if (i == c - d) ret[i] = '(';
                // 이외는 )
                else ret[i] = ')';
            }

            for (int i = c + 1; i < 2 * c; i++)
            {

                // 나머지는 (
                ret[i] = '(';
            }

            using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
            {

                for (int i = 0; i < ret.Length; i++)
                {

                    sw.Write(ret[i]);
                }
            }
        }
    }

#if other
// #include <stdio.h>

int main(void) {
    int a, s = 0;
    scanf("%d", &a);
    for (int i = 1;; i++) {
        s += i;
        if (s >= a) {
            s -= i;
            a -= s;
            for (int j = 0; j < a; j++) printf(")");
            printf("(");
            for (int j = 0; j < i - a; j++) printf(")");
            for (int j = 1; j < i; j++) printf("(");
            printf("\n");
            break;
        }
    }
    return 0;
}
#endif
}
