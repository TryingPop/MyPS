using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 31
이름 : 배성훈
내용 : 럭키 스트레이트
    문제번호 : 18406번

    구현, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1230
    {

        static void Main1230(string[] args)
        {

            string input = Console.ReadLine();
            int half = input.Length >> 1;
            int chk = 0;
            for (int i = 0; i < half; i++)
            {

                chk += input[i] - '0';
                chk -= input[half + i] - '0';
            }

            if (chk == 0) Console.Write("LUCKY");
            else Console.Write("READY");
        }
    }
}
