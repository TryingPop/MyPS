using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.15
 * 내용 : 백준 4단계 3번 문제
 * 
 * 숫자의 개수
 */

namespace BaekJoon._04
{
    internal class _03
    {
        static void Main3(string[] args)
        {
            int[] ints = new int[10];
            int num;
            int A = int.Parse(Console.ReadLine());
            int B = int.Parse(Console.ReadLine());
            int C = int.Parse(Console.ReadLine());

            string D = (A * B * C).ToString();

            for (int i = 0; i < D.Length; i++)
            {
                num = int.Parse(D[i].ToString());
                ints[num] += 1;
            }
            foreach (int i in ints)
            {
                Console.WriteLine(i);
            }
        }
    }
}
