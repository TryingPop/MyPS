using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 4
이름 : 배성훈
내용 : 조별과제를 하려는데 조장이 사라졌다
    문제번호 : 15727번

    수학, 사칙연산 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0940
    {

        static void Main940(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int ret = n / 5;
            if (n % 5 > 0) ret++;
            Console.WriteLine(ret);
        }
    }
}
