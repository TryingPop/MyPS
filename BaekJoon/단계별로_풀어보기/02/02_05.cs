using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 2단계 5번 문제
 * 
 * 알람 시계
 * 시간을 입력받아 45분 전의 시간 출력하기
 */

namespace BaekJoon._02
{
    internal class _02_05
    {
        static void Main5(string[] args)
        {
            string[] strinputs = Console.ReadLine().Split(' ');
            int[] inputs = Array.ConvertAll(strinputs, int.Parse);

            inputs[1] -= 45;

            if (inputs[1] < 0)
            {
                inputs[0] -= 1;
                inputs[1] += 60;
            }

            if (inputs[0] < 0)
            {
                inputs[0] += 24;
            }

            Console.WriteLine("{0} {1}", inputs[0], inputs[1]);
        }
    }
}
