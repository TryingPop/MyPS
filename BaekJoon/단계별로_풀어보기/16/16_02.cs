using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 알고리즘 수업 - 알고리즘 수행 시간 2
    문제번호 : 24263번
*/

namespace BaekJoon._16
{
    internal class _16_02
    {

        static void Main2(string[] args)
        {

            int input = int.Parse(Console.ReadLine());
            Console.WriteLine(input);                   // n을 입력받으면 for문에서 n번 코드를 실행하므로 실행 횟수는 n이다
            Console.WriteLine(1);                       // 다항식으로 나타내면 1차원이므로 1
        }

        // 주어진 알고리즘은 다음과 같다
        // 반환형식과 array 형식은 숫자 형이면 상관없다
        public static int MenOfPassion(int[] array, int n)
        {

            int sum = 0;
            for (int i = 1; i <= n; n++)
            {

                sum += array[i];
            }
            return sum;
        }
    }
}
