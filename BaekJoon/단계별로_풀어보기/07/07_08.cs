using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.20
 * 내용 : 백준 7단계 8번 문제
 * 
 * 큰 수 A+B
 */

namespace BaekJoon._07
{
    internal class _07_08
    {
        static void Main8(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(" ");

            int len1 = inputs[0].Length;
            int len2 = inputs[1].Length;

            int len = Math.Max(len1, len2);
            len += 1;

            int[] input1 = new int[len];
            int[] input2 = new int[len];
            int[] result = new int[len];

            for (int i = 0; i < len1; i++)
            {
                input1[i] = (int)(inputs[0][len1 - 1 - i] - '0');
            }

            for (int i = 0; i < len2; i++)
            {
                input2[i] = (int)(inputs[1][len2 - 1 - i] - '0');
            }

            for (int i = 0; i < len; i++)
            {
                result[i] += input1[i] + input2[i];
                if (result[i] >= 10 && i < len - 1)
                {
                    result[i] -= 10;
                    result[i + 1] += 1;
                }
            }

            bool output_started = false;

            for (int i = len - 1; i >= 0; i--)
            {
                if (output_started)
                {
                    Console.Write("{0}", result[i]);
                }
                else if (result[i] == 0)
                {
                    continue;
                }
                else
                {
                    Console.Write(result[i]);
                    output_started = true;
                }
            }
            Console.WriteLine();
        }
    }
}

/*

            Console.WriteLine("num1");
            foreach (int num in num1)
            {
                Console.WriteLine(num);
            }

            Console.WriteLine("num2");
            foreach (int num in num2)
            {
                Console.WriteLine(num);
            }

            Console.WriteLine("ADD");
            foreach (int num in resultarr)
            {
                Console.WriteLine(num);
            }
*/
