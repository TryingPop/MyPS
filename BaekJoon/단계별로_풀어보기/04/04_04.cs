using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.18
 * 내용 : 백준 4단계 1번 문제
 * 
 * 나머지
 */

namespace BaekJoon._04
{
    internal class _04_04
    {
        static void Main4(string[] args)
        {
            int[] reminders = new int[10];
            int[] result = new int[42];


            for (int i = 0; i < 10; i++)
            {
                reminders[i] = (int.Parse(Console.ReadLine())) % 42;
            }

            foreach (int num in reminders)
            {
                result[num] = 1;
            }

            Console.WriteLine(result.Sum());
        }
    }
}
