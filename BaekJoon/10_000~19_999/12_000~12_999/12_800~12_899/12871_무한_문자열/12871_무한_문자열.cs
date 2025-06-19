using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 27
이름 : 배성훈
내용 : 무한 문자열
    문제번호 : 12871번

    수학, 구현, 문자열 문제다
    큰 수 만들기 문제의 아이디어를 빌려왔다
*/

namespace BaekJoon.etc
{
    internal class etc_0362
    {

        static void Main362(string[] args)
        {

            string str1 = Console.ReadLine();
            string str2 = Console.ReadLine();

            if ((str1 + str2) == (str2 + str1)) Console.WriteLine(1);
            else Console.WriteLine(0);
        }
    }
}
