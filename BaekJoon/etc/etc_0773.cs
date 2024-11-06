using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 26
이름 : 배성훈
내용 : Welcome to SMUPC!
    문제번호 : 29699번

    구현, 문자열, 사칙연산 문제다
    그냥 전체 길이 14로 나눈 나머지를 계산하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0773
    {

        static void Main773(string[] args)
        {

            string STR = "WelcomeToSMUPC";
            int n = int.Parse(Console.ReadLine()) - 1;

            Console.Write(STR[n % 14]);
        }
    }
}
