using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 3단계 5번 문제
 * 
 * N 찍기
 * A를 입력받아 1부터 A까지 차례대로 출력하기
 */

namespace BaekJoon._03
{
    internal class _03_05
    {
        static void Main5(string[] args)
        {
            int input = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= input; i++)
            {
                sb.Append(i + "\n");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
