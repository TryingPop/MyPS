using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 10
이름 : 배성훈
내용 : Darius님 한타 안 함?
    문제번호 : 20499번

    수학, 사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1392
    {

        static void Main1392(string[] args)
        {

            // 20499
            int[] arr = Array.ConvertAll(Console.ReadLine().Split('/'), int.Parse);
            if (arr[1] == 0 || arr[0] + arr[2] < arr[1]) Console.Write("hasu");
            else Console.Write("gosu");
        }
    }
}
