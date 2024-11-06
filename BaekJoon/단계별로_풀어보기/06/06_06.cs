using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 6단계 6번 문제
 * 
 * 단어의 개수
 */

namespace BaekJoon._06
{
    internal class _06_06
    {
        static void Main6(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(" ");
            int length = inputs.Length;
            if (inputs[0] == "")
            {
                length -= 1;
            }
            if (inputs[inputs.Length - 1] == "")
            {
                length -= 1;
            }
            if (length < 0)
            {
                length = 0;
            }
            Console.WriteLine(length);
        }
    }
}
