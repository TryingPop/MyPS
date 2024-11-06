using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 2단계 7번 문제
 * 
 * 주사위 세개
 * 주사위 3개의 값을 입력받아 결과값 출력하기
 */

namespace BaekJoon._02
{
    internal class _02_07
    {
        static void Main7(string[] args)
        {
            string[] strarray = Console.ReadLine().Split(' ');
            int[] inputs = Array.ConvertAll(strarray, int.Parse);

            if (inputs[0] == inputs[1]  && inputs[1] == inputs[2])
            {
                Console.WriteLine(1000*inputs[0]+10000);
            }
            else if (inputs[0] != inputs[1] && inputs[1] != inputs[2] && inputs[2] != inputs[0])
            {
                Console.WriteLine(inputs.Max()*100);
            }
            else
            {
                if (inputs[0] == inputs[1] || inputs[0] == inputs[2])
                {
                    Console.WriteLine(inputs[0] * 100 + 1000);
                }
                else
                {
                    Console.WriteLine(inputs[1] * 100 + 1000);
                }
            }

        }
    }
}
