using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 1단계 8번 문제
 * 
 * A/B
 * A, B를 입력받아 A / B를 출력하기
 */

namespace BaekJoon._01
{
    internal class _01_08
    {
        static void Main8(string[] args)
        {
            string[] strinputs = Console.ReadLine().Split(' ');
            int[] intinputs = Array.ConvertAll(strinputs, e => int.Parse(e));

            if (intinputs[1] != 0)
            {
                Console.WriteLine((double)intinputs[0] / (double)intinputs[1]);
            }
            
        }
    }
}
