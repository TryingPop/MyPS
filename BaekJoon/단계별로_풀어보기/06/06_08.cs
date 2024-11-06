using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 6단계 7번 문제
 * 
 * 다이얼
 */

namespace BaekJoon._06
{
    internal class _06_08
    {
        static void Main8(string[] args)
        {
            string inputs = Console.ReadLine();
            int num = 0;
            int result = 0;
            inputs = inputs.ToUpper();
            for (int i = 0; i < inputs.Length; i++)
            {
                num = (int)(inputs[i] - 'A');
                if (num < 15)
                {
                    num /= 3;
                }
                else if (num <= 18)
                {
                    num = 5;
                }
                else if (num <= 21)
                {
                    num = 6;
                }
                else
                {
                    num = 7;
                }
                num += 3;
                result += num;
            }
            Console.WriteLine(result);
        }
    }
}
