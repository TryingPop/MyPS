using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 알고리즘 수업 - 알고리즘의 수행 시간 3
    문제번호 : 24264번
*/

namespace BaekJoon._16
{
    internal class _16_03
    {

        static void Main3(string[] args)
        {

            long input = long.Parse(Console.ReadLine());
            Console.WriteLine(input * input);           // for문을 중첩해서 도므로 코드실행은 n * n 번한다
            Console.WriteLine(2);                       // 시간 복잡도의 최고차항은 2이다
        }


        public static long MenOfPassion(long[] array, int n)
        {

            long sum = 0;
            for (int i = 1; i <= n; i++)
            {

                for (int j = 1; j <= n; j++)
                {

                    sum += array[i] * array[j];
                }
            }

            return sum;
        }
    }


}
