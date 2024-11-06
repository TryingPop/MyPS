using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 9단계 2번 문제
 * 
 * 피보나치 수 5
 */

namespace BaekJoon._09
{
    internal class _09_02
    {
        static void Main2(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            static int Fibo(int n)
            {
                if (n <= 1)
                {
                    return n;
                }
                return Fibo(n - 1) + Fibo(n - 2);
            }

            int result = Fibo(num);
            Console.WriteLine(result);
        }
    }
}
