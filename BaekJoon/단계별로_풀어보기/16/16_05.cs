using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 알고리즘 수업 - 알고리즘의 수행 시간 5
    문제번호 : 24266번
*/

namespace BaekJoon._16
{
    internal class _16_05
    {

        static void Main5(string[] args)
        {

            long input = long.Parse(Console.ReadLine());
            Console.WriteLine(input * input * input);           // n까지 도는 for문이 3개이므로 n^3을 반환
            Console.WriteLine(3);                               // 다항식의 최고 차수는 3
        }

        public static long MenOfPassion(int[] arr, int n)
        {

            long sum = 0;
            for (int i = 1; i <= n; i++)
            {

                for (int j = 1; j <= n; j++)
                {

                    for (int k = 1; k <= n; k++)
                    {

                        sum += arr[i] * arr[j] * arr[k];
                    }
                }
            }

            return sum;
        }
    }
}
