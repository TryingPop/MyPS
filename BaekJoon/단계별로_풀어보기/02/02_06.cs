using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 2단계 6번 문제
 * 
 * 오븐 시계
 * 시간과 대기 시간을 입력받아 결과 시간 출력하기
 */

namespace BaekJoon._02
{
    internal class _02_06
    {
        static void Main6(string[] args)
        {
            string[] strinputs = Console.ReadLine().Split(' ');
            int timeinput = int.Parse(Console.ReadLine());
            int[] inputs = Array.ConvertAll(strinputs, int.Parse);

            inputs[1] += timeinput;

            while (inputs[1] >= 60)
            {
                inputs[0] += 1;
                inputs[1] -= 60;
            }
            while (inputs[0] >= 24)
            {
                inputs[0] -= 24;
            }

            Console.WriteLine("{0} {1}", inputs[0], inputs[1]);
        }
    }
}
