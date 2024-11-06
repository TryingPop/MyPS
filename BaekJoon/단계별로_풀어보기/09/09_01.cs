using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 9단계 1번 문제
 * 
 * 팩토리얼
 */

namespace BaekJoon._09
{
    internal class _09_01
    {
        static void Main1(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            long result = 1;
            for (int i = 2;i <= num; i++)
            {
                result *= i;
            }
            Console.WriteLine(result);
        }
    }
}
