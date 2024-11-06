using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 1단계 14번 문제
 * 
 * 두 수 비교하기
 * A, B를 입력받아 두수간 등호를 출력하기
 */

namespace BaekJoon._02
{
    internal class _02_01
    {
        static void Main1(string[] args)
        {
            string[] strinputs = Console.ReadLine().Split(' ');
            int[] inputs = Array.ConvertAll(strinputs, int.Parse);

            if (inputs[0] > inputs[1])
            {
                Console.WriteLine(">");
            }
            else if (inputs[0] == inputs[1])
            {
                Console.WriteLine("==");
            }
            else
            {
                Console.WriteLine("<");
            }
        }
        

    }
}
