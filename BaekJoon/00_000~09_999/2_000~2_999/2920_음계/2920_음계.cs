using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 14
이름 : 배성훈
내용 : 음계
    문제번호 : 2920번

    간단한 구현 문제
*/

namespace BaekJoon.etc
{
    internal class etc_0028
    {

        static void Main28(string[] args)
        {


            string mix = "mixed";
            string asc = "ascending";
            string dec = "descending";

            string str = Console.ReadLine();

            if (str == "8 7 6 5 4 3 2 1") Console.WriteLine(dec);
            else if (str == "1 2 3 4 5 6 7 8") Console.WriteLine(asc);
            else Console.WriteLine(mix);
        }
    }
}
