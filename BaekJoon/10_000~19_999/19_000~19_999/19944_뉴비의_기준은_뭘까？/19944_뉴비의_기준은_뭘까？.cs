using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 15
이름 : 배성훈
내용 : 뉴비의 기준은 뭘까?
    문제번호 : 19944번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1408
    {

        static void Main1408(string[] args)
        {

            string NB = "NEWBIE!";
            string OB = "OLDBIE!";
            string TLE = "TLE!";

            string[] input = Console.ReadLine().Split();
            int n = int.Parse(input[0]), m = int.Parse(input[1]);
            if (m <= 2) Console.Write(NB);
            else if (m <= n) Console.Write(OB);
            else Console.Write(TLE);
        }
    }
}
