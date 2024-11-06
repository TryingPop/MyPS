using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.15
 * 내용 : 백준 3단계 12번 문제
 * 
 * 	A+B - 4
 * 	A, B를 입력받아 각 케이스마다 A + B를 출력하기
 */

namespace BaekJoon._03
{
    internal class _03_12
    {
        static void Main12(string[] args)
        {
            while (true)
            {
                string[] strinput = Console.ReadLine().Split(" ");
                int[] inputs = Array.ConvertAll(strinput, int.Parse);

                if (inputs[0] == 0 && inputs[1] == 0)
                {
                    break;
                }
                Console.WriteLine(inputs[0] + inputs[1]);
            }

        }
    }
}
