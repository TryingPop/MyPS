using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 7단계 2번 문제
 * 
 * 벌집
 */

namespace BaekJoon._07
{
    internal class _07_02
    {
        static void Main2(string[] args)
        {
            int input = int.Parse(Console.ReadLine());

            int result = 1;

            while (input >= ((3 * result) * (result - 1)) + 2 && input != 1)
            {
                result++;
            }

            Console.WriteLine(result);
        }
    }
}
