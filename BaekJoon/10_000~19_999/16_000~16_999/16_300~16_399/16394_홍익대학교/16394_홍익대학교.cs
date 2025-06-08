using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 30
이름 : 배성훈
내용 : 홍익대학교
    문제번호 : 16394

    수학, 사칙연산 문제다.
    시작 년도와 몇년 차이인지 계산하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1596
    {

        static void Main1596(string[] args)
        {

            int S = 1_946;
            int n = int.Parse(Console.ReadLine());
            Console.Write(n - S);
        }
    }
}
