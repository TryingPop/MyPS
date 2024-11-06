using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 28
이름 : 배성훈
내용 : 평균 점수
    문제번호 : 10039번

    수학, 사칙연산 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0845
    {

        static void Main845(string[] args)
        {

            int sum = 0;
            for (int i = 0; i < 5; i++)
            {

                int score = int.Parse(Console.ReadLine());
                if (score < 40) score = 40;
                sum += score;
            }

            Console.Write(sum / 5);
        }
    }
}
