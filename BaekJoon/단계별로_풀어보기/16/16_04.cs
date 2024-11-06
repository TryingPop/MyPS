using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 알고리즘 수업 - 알고리즘의 수행 시간 4
    문제번호 : 24265번
*/

namespace BaekJoon._16
{
    internal class _16_04
    {

        static void Main4(string[] args)
        {

            long input = long.Parse(Console.ReadLine());

            Console.WriteLine(((input + 1)* input) / 2);        // 0 + 1 + 2 + ... + n - 1 = (n * (n - 1)) / 2
            Console.WriteLine(2);                               // 다항식으로 표현하면 2차식이므로 2
        }

        public static long MenOfPassion(long[] array, int n)
        {

            long sum = 0;
            for (int i = 1; i < n; i++)
            {

                for (int j = i + 1; j <= n; j++)
                {

                    sum += array[i] * array[j];
                }
            }

            return sum;
        }
    }



}
