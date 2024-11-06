using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.18
 * 내용 : 백준 4단계 1번 문제
 * 
 * 평균
 */

namespace BaekJoon._04
{
    internal class _04_05
    {
        static void Main5(string[] args)
        {
            double avg = 0;
            int lenght = int.Parse(Console.ReadLine());

            string[] strinputs = Console.ReadLine().Split(" ");
            double[] inputs = Array.ConvertAll(strinputs, double.Parse);
            inputs = Array.ConvertAll(inputs, s => 100 * s / inputs.Max());

            avg = inputs.Average();

            Console.WriteLine(avg);
        }
    }
}
