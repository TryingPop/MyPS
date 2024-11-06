using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 6단계 2번 문제
 * 
 * 숫자의 합
 */

namespace BaekJoon._06
{
    internal class _06_02
    {
        static void Main2(string[] args)
        {
            int result = 0;
            int length = int.Parse(Console.ReadLine());

            for (int i = 0; i < length; i++)
            {
                int input = Console.Read() - 48;
                result += input;
            }
            Console.WriteLine(result);
        }
    }
}
