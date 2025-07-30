using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 16
이름 : 배성훈
내용 : Улучшение успеваемости
    문제번호 : 20428번

    수학, 사칙연산 문제다.
    2점짜리 개수 a, 3점짜리 개수 b, 4점짜리 개수가 c이다.
    그리고 5점짜리 개수를 x개 추가하여 평균이 4가되게 해야 한다.
    소수점 부분은 반올림 한다. 즉 3.5이상이 되는 가장 작은 x를 찾아야 한다.

    C#의 Round함수는 우리가 아는 반올림이 아니고, double을 쓸 때 부동 소수점 오차가 존재한다.
    그래서 long을 이용해 연산을 했다.

    연산을 통해서 보면 초기 총점수 total = 2a + 3b + 3c > 0이고
    시험의 개수 sum = a + b + c > 0이다.
    
    그리고 우리가 찾을 값 x는
    (total + 5x) / (sum + x) >= 3.5를 만족해야 한다.
    => 2(total + 5x) >= 7(sum + x)
    => 3x >= 7sum - 2total
    을 얻는다.
    
    그래서 x >= 0이므로 7sum - 2total < 0인 경우 x = 0을 한다.
    반면 7sum - 2total / 3 <= x인 가장 작은 x를 찾아주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1770
    {

        static void Main1770(string[] args)
        {

            long a = long.Parse(Console.ReadLine());
            long b = long.Parse(Console.ReadLine());
            long c = long.Parse(Console.ReadLine());

            long score = a * 2 + b * 3 + c * 4;
            long sum = a + b + c;

            long tX = 7 * sum - 2 * score;


            long ret;
            if (tX > 0)
            {

                ret = tX / 3;
                if (tX % 3 != 0) ret++;
            }
            else ret = 0;
            
            Console.Write(ret);
        }
    }
}
