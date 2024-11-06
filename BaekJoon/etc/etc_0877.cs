using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 14
이름 : 배성훈
내용 : 가희와 클럽 오디션 1
    문제번호 : 30794번

    수학, 구현, 문자열, 사칙연산 문제다
    문제 설명으로 혼동되기 쉬운 문제 같다
    "게임 클럽 오디션의 scoring system과 문제에서의 scoring system이 다름에 주의하세요."
    -> 해당 문구를 무시해야한다!

    그리고 게임 클럽 오디션 scoring system 시스템을 따르는데
    이후 가희가 이전에 받은 판정과 다른 판정을 받았을 때를 가정해
    정답을 찾으라고 명시되어 해당 조건에 맞춰 풀면된다

    이는 perfect가 1연팩으로 보면 된다
    그리고 게임 클럽 오디션의 scoring system을 따라 점수를 판정하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0877
    {

        static void Main877(string[] args)
        {

            string[] input = Console.ReadLine().Split();

            int ret = 0;
            if (input[1] == "perfect") ret = 1000;
            else if (input[1] == "great") ret = 600;
            else if (input[1] == "cool") ret = 400;
            else if (input[1] == "bad") ret = 200;

            ret *= int.Parse(input[0]);

            Console.Write(ret);
        }
    }
}
