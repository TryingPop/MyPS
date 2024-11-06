using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 6
이름 : 배성훈
내용 : 막대기
    문제번호 : 1094번

    막대기가 몇개 합쳐졌는지 세는 문제다

    23cm 를 얻고 싶다고 가정하자
    처음 64cm가 있을 것이고 이후에 단위는 생략
    64 > 23이므로 64 = 32 + 32로 자른다
    이후에 32 > 23이므로
    32 하나를 버린다

    32 > 23이므로 다시 반으로 자른다
    32 = 16 + 16
    여기서 16 < 23이고 위에서 자른 16에 대해 절반을 8씩 자른다
    그러면 현재 16 + 8 + 8이다
    16 + 8 > 23이므로 8을 버린다
    
    현재 16 + 8 = 24 > 23이므로
    가장 작은 8 = 4 + 4로 자른다
    16 + 4 + 4이고 16 + 4 < 23이므로 4를 반으로 나눈다
    16 + 4 + 2 + 2이고 16 + 4 + 2 < 23이므로 다시 2를 1로 나눈다
    이제 16 + 4 + 2 + 1 == 23 이므로 16 + 4 + 2 + 1 + 1에서 1을 하나 버린다

    그래서 결국에는 2의 제곱수 들만 세어주면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0158
    {

        static void Main158(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int ret = 0;
            for (int i = 0; i < 7; i++)
            {

                if ((n & (1 << i)) == 0) continue;
                ret++;
            }

            Console.WriteLine(ret);
        }
    }
}
