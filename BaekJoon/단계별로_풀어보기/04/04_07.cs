using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.18
 * 내용 : 백준 4단계 1번 문제
 * 
 * 평균은 넘겠지
 */

namespace BaekJoon._03
{
    internal class _04_07
    {
        static void Main7(string[] args)
        {
            int length = int.Parse(Console.ReadLine());
            int num = 0;
            double avg = 0;
            double result = 0;

            for (int i = 0; i < length; i++)
            {
                string[] inputs = Console.ReadLine().Split();
                int[] input = Array.ConvertAll(inputs[1..], int.Parse);
                
                avg = input.Average();

                num = 0;

                for (int j = 1; j < inputs.Length; j++) 
                {
                    if (int.Parse(inputs[j]) > avg)
                    {
                        num++;
                    }
                }

                result = 100 * ((double)num) / ((double)input.Length);
                Console.WriteLine("{0:0.000}%", result);
            }
        }
    }
}
