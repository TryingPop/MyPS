using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 1단계 6번 문제
 * 
 * A-B
 * A, B를 입력받아 A - B를 출력하기
 */

namespace BaekJoon._01
{
    internal class _01_06
    {
        static void Main6(string[] args)
        {
            string[] strinputs = Console.ReadLine().Split(' ');
            int[] intinputs = Array.ConvertAll(strinputs, s => int.Parse(s));

            Console.WriteLine("{0}", intinputs[0] - intinputs[1]);
        }
    }
}
