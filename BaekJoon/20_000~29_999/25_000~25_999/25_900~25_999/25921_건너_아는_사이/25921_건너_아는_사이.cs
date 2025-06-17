using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 24
이름 : 배성훈
내용 : 건너 아는 사이
    문제번호 : 25921번

    정수론, 에라토스테네스의 체 문제다.
    아이디어는 단순하다.
    소수인 경우 1과 잇는다.
    이제 2부터 2의 배수들을 2와 잇는게 최소이다.
    그래서 2로 잇는다.

    이젠 3의 배수 중 2의 배수가 아닌 것들을 3과 잇는게 최소이다.
    그래서 해당 3의 배수들을 3과 잇는다.

    이렇게 100만이하의 소수들을 모두 이어주면 된다.
    시행해보니 100만의 경우 오버플로우로 int 범위를 벗어난다.
    long으로 바꾸니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1570
    {

        static void Main1570(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            bool[] visit = new bool[n + 1];
            
            long ret = 0;
            for (int i = 2; i <= n; i++)
            {

                if (visit[i]) continue;
                ret += i;

                for (int j = i << 1; j <= n; j += i)
                {

                    if (visit[j]) continue;
                    visit[j] = true;
                    ret += i;
                }
            }

            Console.Write(ret);
        }
    }
}
