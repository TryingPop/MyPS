using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 30
이름 : 배성훈
내용 : 수 이어 쓰기 1
    문제번호 : 1748번

    수학, 구현 문제다
    아이디어는 다음과 같다
    1 ~ 9 : 1개가 9개
    10 ~ 99 : 2개가 90개
    100 ~ 999 : 3개가 900개
    ... 하면서

    해당 자릿수 이하의 수들을 자리수로 묶어서 계산한다
    해당 자리의 숫자는 앞자리 1을 빼고 1개를 더한게 개수가된다
        예를들어 123인 경우 3자리 숫자는 99를 뺀 값 24개가 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0396
    {

        static void Main396(string[] args)
        {

            string str = Console.ReadLine();
            int n = int.Parse(str);

            int ret = 0;
            
            int cur = 1;
            for (int i = 1; i <= str.Length; i++)
            {

                int calc = cur * 10;
                if (n >= calc) ret += 9 * cur * i;
                else
                {

                    ret += (n - cur + 1) * i;
                    break;
                }

                cur = calc;
            }

            Console.WriteLine(ret);
        }
    }
}
