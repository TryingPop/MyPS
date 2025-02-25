using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 19
이름 : 배성훈
내용 : 연속인가? ?
    문제번호 : 26517번

    수학 문제다.
    변하는 지점에서 값이 같은지만 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1347
    {

        static void Main1347(string[] args)
        {

            long k = int.Parse(Console.ReadLine());
            int[] abcd = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            if (abcd[0] * k + abcd[1] == abcd[2] * k + abcd[3])
                Console.Write($"Yes {abcd[0] * k + abcd[1]}");
            else Console.Write("No");
        }
    }
}
