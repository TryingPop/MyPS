using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 알고리즘 수업 - 알고리즘의 수행 시간 6
    문제번호 : 24267번

    시그마 공식을 이용해야한다
*/

namespace BaekJoon._16
{
    internal class _16_06
    {

        static void Main6(string[] args)
        {

            long input = long.Parse(Console.ReadLine());

            Console.WriteLine(((input) * (input - 1) * (input - 2)) / 6);       // for문을 이용해 그냥 실행횟수를 찾으려면
                                                                                // O(n^3)이므로 시간 초과뜬다
            Console.WriteLine(3);
        }


        public static long MenOfPassion(int[] arr, int n)
        {

            long sum = 0;
            // 시그마 공식을 이용하면 (n * (n - 1) * (n - 2)) / 6회 실행 하는 것을 알 수 있다
            for (int i = 1; i <= n - 2; i++)
            {

                // for 구문 안에는 코드를 ((i - 1) * i / 2) 회 실행한다
                for (int j = i + 1; j <= n - 1; j++)
                {

                    for (int k = j + 1; k <= n; k++)
                    {

                        sum += arr[i] * arr[j] * arr[k];
                    }
                }
            }

            return sum;
        }
    }
}
