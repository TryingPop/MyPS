using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 1단계 9번 문제
 * 
 * 사칙연산
 * A, B를 입력받아 A + B, A - B, A * B, A / B를 줄마다 각각 출력하기
 */

namespace BaekJoon._01
{
    internal class _01_09
    {
        static void Main9(string[] args)
        {
            string[] strarray = Console.ReadLine().Split(' ');
            int[] intarray = Array.ConvertAll(strarray, int.Parse);

            Console.WriteLine(intarray[0] + intarray[1]);
            Console.WriteLine(intarray[0] - intarray[1]);
            Console.WriteLine(intarray[0] * intarray[1]);
            Console.WriteLine(intarray[0] / intarray[1]);
            Console.WriteLine(intarray[0] % intarray[1]);

        }
    }
}
