using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 14
이름 : 배성훈
내용 : 검증수
    문제번호 : 2475번

    간단한 상황을 그대로 구현하는 문제
*/

namespace BaekJoon.etc
{
    internal class etc_0027
    {

        static void Main27(string[] args)
        {

            int sum = Console.ReadLine().Split(' ').Select(int.Parse).Select(x => x * x).Sum();
            sum %= 10;
            Console.WriteLine(sum);
        }
    }
}
