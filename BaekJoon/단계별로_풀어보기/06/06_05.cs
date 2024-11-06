using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 6단계 5번 문제
 * 
 * 단어 공부
 */

namespace BaekJoon._06
{
    internal class _06_05
    {
        static void Main5(string[] args)
        {
            string inputs = Console.ReadLine();
            int start = 'A';
            int end = 'Z';
            int[] findarr = new int[26];
            int max;
            char result;

            inputs = inputs.ToUpper();

            for (int i = start; i <= end; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    if (inputs[j] == i)
                    {
                        findarr[(int)(i - start)] += 1;
                    }
                }
            }
            max = findarr.Max();

            if (Array.IndexOf(findarr, max) == Array.LastIndexOf(findarr, max))
            {
                result = (char)(start + Array.IndexOf(findarr, max));
                Console.WriteLine(result);
            }
            else
            {
                result = '?';
                Console.WriteLine(result);
            }
            
        }
    }
}
